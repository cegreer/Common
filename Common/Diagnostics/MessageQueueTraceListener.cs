using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Messaging;
using System.Xml.Linq;

using Res = Vizistata.Properties.Resources;

namespace Vizistata.Diagnostics {
	/// <summary>
	/// A trace listener that sends messages to a Microsoft Message Queue.
	/// </summary>
	public class MessageQueueTraceListener : TraceListener {
	// Fields
		/// <summary>
		/// The current line that is being created, or a null reference.
		/// </summary>
		private String _currentLine;
		/// <summary>
		/// Controls write access to the <see cref="F:_lines"/> field.  This field is read-only.
		/// </summary>
		private readonly Object _linesDoor = new Object();
		/// <summary>
		/// The list of lines that have been created.  This field is read-only.
		/// </summary>
		private readonly List<String> _lines = new List<String>(100);
		/// <summary>
		/// The message queue to use.
		/// </summary>
		private MessageQueue _messageQueue;
		/// <summary>
		/// The path to the message queue.  This field is read-only.
		/// </summary>
		private readonly String _path;

	// Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:MessageQueueTraceListener"/> class.
		/// </summary>
		public MessageQueueTraceListener() : base() { }
		/// <summary>
		/// Initializes a new instance of the <see cref="T:MessageQueueTraceListener"/> class.
		/// </summary>
		/// <param name="messageQueue">The message queue to use.</param>
		/// <exception cref="System.ArgumentNullException"><paramref name="messageQueue"/> is a null reference.</exception>
		/// <exception cref="System.ArgumentException">The <see cref="P:MessageQueue.CanWrite"/> property is set to <c>false</c> for <paramref name="messageQueue"/>.</exception>
		public MessageQueueTraceListener(MessageQueue messageQueue) : this(messageQueue, String.Empty) { }
		/// <summary>
		/// Initializes a new instance of the <see cref="T:MessageQueueTraceListener"/> class.
		/// </summary>
		/// <param name="messageQueue">The message queue to use.</param>
		/// <param name="name">The name of the trace listener.</param>
		/// <exception cref="System.ArgumentNullException"><paramref name="messageQueue"/> is a null reference.</exception>
		/// <exception cref="System.ArgumentException">The <see cref="P:MessageQueue.CanWrite"/> property is set to <c>false</c> for <paramref name="messageQueue"/>.</exception>
		public MessageQueueTraceListener(MessageQueue messageQueue, String name)
			: base(name) {
			if (messageQueue == null) {
				throw new ArgumentNullException("messageQueue");
			}
			if (!messageQueue.CanWrite) {
				throw new ArgumentException(Res.MessageQueueIsNotWriteableMessage, "messageQueue");
			}
			this._messageQueue = messageQueue;
			this._path = messageQueue.Path;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="T:MessageQueueTraceListener"/> class.
		/// </summary>
		/// <param name="path">The path to the queue.</param>
		/// <exception cref="System.ArgumentNullException"><paramref name="path"/> is a null reference.</exception>
		/// <exception cref="System.ArgumentException"><paramref name="path"/> is invalid.</exception>
		public MessageQueueTraceListener(String path) : this(path, String.Empty) { }
		/// <summary>
		/// Initializes a new instance of the <see cref="T:MessageQueueTraceListener"/> class.
		/// </summary>
		/// <param name="path">The path to the queue.</param>
		/// <param name="name">The name of the trace listener.</param>
		/// <exception cref="System.ArgumentNullException"><paramref name="path"/> is a null reference.</exception>
		/// <exception cref="System.ArgumentException"><paramref name="path"/> is invalid.</exception>
		public MessageQueueTraceListener(String path, String name)
			: base(name) {
			if (path == null) {
				throw new ArgumentNullException("path");
			}
			this._messageQueue = new MessageQueue(path);
			this._path = path;
		}

	// Properties
		/// <summary>
		/// Gets the enumerable collection of lines that have not yet been sent.
		/// </summary>
		protected IEnumerable<String> Lines {
			get { return this._lines.ToArray(); }
		}

	// Methods
		/// <summary>
		/// Creates the e-mail body.
		/// </summary>
		/// <returns>The value that represents the body of the e-mail.</returns>
		protected virtual String CreateBody() {
			XElement messagesElement = new XElement("messages");
			messagesElement.Add(this.Lines.Select(line => new XElement("message", line)));
			return messagesElement.ToString();
		}
		/// <summary>
		/// Creates the e-mail subject line.
		/// </summary>
		/// <returns>The value that represents the subject line of the e-mail.</returns>
		protected virtual String CreateLabel() {
			DateTime now = DateTime.Now;
			return Res.DefaultTraceListenerSubjectFormat.FormatInvariant(this.Name, now.ToLongDateString(), now.ToLongTimeString());
		}
		/// <summary>
		/// Disposes of any managed resources held by this instance.
		/// </summary>
		/// <param name="disposing"><c>true</c> if called from the <see cref="M:IDisposable.Dispose"/> method; otherwise, <c>false</c>.</param>
		protected override void Dispose(Boolean disposing) {
			base.Dispose(disposing);
			if (disposing) {
				if (this._messageQueue != null) {
					this._messageQueue.Dispose();
					this._messageQueue = null;
				}
			}
		}
		/// <summary>
		/// Ensures that the message queue object exists.
		/// </summary>
		private void EnsureMessageQueue() {
			if (this._messageQueue == null) {
				this._messageQueue = new MessageQueue(this._path, false, false, QueueAccessMode.Send);
			}
		}
		/// <summary>
		/// Immediately sends any lines to the e-mail addresses in the recipient list.
		/// </summary>
		public override void Flush() {
			lock (this._linesDoor) {
				if (this.SendMessage()) {
					this._lines.Clear();
				}
			}
		}
		/// <summary>
		/// Attempts to send a message to a message queue and returns a value indicating if a message was actually created and sent.
		/// </summary>
		/// <returns><c>true</c> if a message was sent; otherwise, <c>false</c>.</returns>
		protected Boolean SendMessage() {
			if (this._lines.Count == 0) {
				return false;
			}

			using (Message message = new Message()) {
				message.Formatter = new XmlMessageFormatter(new Type[] { typeof(String) });
				message.Label = this.CreateLabel();
				message.Body = this.CreateBody();
				this.EnsureMessageQueue();
				this._messageQueue.Send(message);
			}
			return true;
		}
		/// <summary>
		/// Writes a message.  A parameter determines whether the line is complete after the message is written.
		/// </summary>
		/// <param name="message">The message to write.</param>
		/// <param name="endOfLine"><c>true</c> if this message is the end of the current line; otherwise, <c>false</c>.</param>
		private void Write(String message, Boolean endOfLine) {
			lock (this._linesDoor) {
				String currentLine;
				if (this._currentLine != null) {
					currentLine = this._currentLine + message;
				}
				else {
					currentLine = message;
				}

				if (!endOfLine) {
					this._currentLine = currentLine;
				}
				else {
					this._currentLine = null;
					this._lines.Add(currentLine);
				}
			}

			if (Trace.AutoFlush || this._lines.Count >= 100) {
				this.Flush();
			}
		}
		/// <summary>
		/// Writes a message.
		/// </summary>
		/// <param name="message">The message to write.</param>
		public override void Write(String message) {
			this.Write(message, false);
		}
		/// <summary>
		/// Writes a message and ends with a new-line character.
		/// </summary>
		/// <param name="message">The message to write.</param>
		public override void WriteLine(String message) {
			this.Write(message, true);
		}
	}
}
