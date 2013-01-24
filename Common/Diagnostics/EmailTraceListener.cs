using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Configuration;
using System.Net.Mail;
using Vizistata.Configuration;

using Res = Vizistata.Properties.Resources;

namespace Vizistata.Diagnostics {
	/// <summary>
	/// A trace listener that sends e-mail messages with the contents of the messages.
	/// </summary>
	public class EmailTraceListener : TraceListener {
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
		/// The e-mail addresses to whom the messages will be sent.
		/// </summary>
		private MailAddressCollection _toAddresses;

	// Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:EmailTraceListener"/> class.
		/// </summary>
		public EmailTraceListener() : base() { }
		/// <summary>
		/// Initializes a new instance of the <see cref="T:EmailTraceListener"/> class.
		/// </summary>
		/// <param name="toAddress">The recipient e-mail address.</param>
		/// <exception cref="System.ArgumentNullException"><paramref name="toAddress"/> is a null reference.</exception>
		public EmailTraceListener(MailAddress toAddress) : this(toAddress, String.Empty) { }
		/// <summary>
		/// Initializes a new instance of the <see cref="T:EmailTraceListener"/> class.
		/// </summary>
		/// <param name="toAddress">The recipient e-mail address.</param>
		/// <param name="name">The name of the listener.</param>
		/// <exception cref="System.ArgumentNullException"><paramref name="toAddress"/> is a null reference.</exception>
		public EmailTraceListener(MailAddress toAddress, String name)
			: base(name) {
			if (toAddress == null) {
				throw new ArgumentNullException("toAddress");
			}
			this._toAddresses = new MailAddressCollection() {
				toAddress
			};
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="T:EmailTraceListener"/> class.
		/// </summary>
		/// <param name="toAddresses">The list of recipient e-mail addresses.</param>
		/// <exception cref="System.ArgumentNullException"><paramref name="toAddresses"/> is a null reference.</exception>
		/// <exception cref="System.ArgumentException"><paramref name="toAddresses"/> contains 0 elements.</exception>
		public EmailTraceListener(MailAddressCollection toAddresses) : this(toAddresses, String.Empty) { }
		/// <summary>
		/// Initializes a new instance of the <see cref="T:EmailTraceListener"/> class.
		/// </summary>
		/// <param name="toAddresses">The list of recipient e-mail addresses.</param>
		/// <param name="name">The name of the listener.</param>
		/// <exception cref="System.ArgumentNullException"><paramref name="toAddresses"/> is a null reference.</exception>
		/// <exception cref="System.ArgumentException"><paramref name="toAddresses"/> contains 0 elements.</exception>
		public EmailTraceListener(MailAddressCollection toAddresses, String name)
			: base(name) {
			if (toAddresses == null) {
				throw new ArgumentNullException("toAddresses");
			}
			if (toAddresses.Count == 0) {
				throw new ArgumentException(Res.CollectionMayNotBeEmptyMessage, "toAddresses");
			}
			this._toAddresses = toAddresses;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="T:EmailTraceListener"/> class.
		/// </summary>
		/// <param name="toAddresses">The list of recipient e-mail addresses separated by a comma character (',').</param>
		/// <exception cref="System.ArgumentNullException"><paramref name="toAddresses"/> is a null reference.</exception>
		/// <exception cref="System.ArgumentException"><paramref name="toAddresses"/> contains 0 characters.</exception>
		/// <exception cref="System.FormatException"><paramref name="toAddresses"/> contains an e-mail address that is invalid or not supported.</exception>
		public EmailTraceListener(String toAddresses) : this(toAddresses, String.Empty) { }
		/// <summary>
		/// Initializes a new instance of the <see cref="T:EmailTraceListener"/> class.
		/// </summary>
		/// <param name="toAddresses">The list of recipient e-mail addresses separated by a comma character (',').</param>
		/// <param name="name">The name of the listener.</param>
		/// <exception cref="System.ArgumentNullException"><paramref name="toAddresses"/> is a null reference.</exception>
		/// <exception cref="System.ArgumentException"><paramref name="toAddresses"/> contains 0 characters.</exception>
		/// <exception cref="System.FormatException"><paramref name="toAddresses"/> contains an e-mail address that is invalid or not supported.</exception>
		public EmailTraceListener(String toAddresses, String name)
			: base(name) {
			if (toAddresses == null) {
				throw new ArgumentNullException("toAddresses");
			}
			if (toAddresses.Length == 0) {
				throw new ArgumentException(Res.StringMayNotBeEmptyMessage, "toAddresses");
			}
			this._toAddresses = new MailAddressCollection() {
				toAddresses
			};
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
		protected virtual String CreateMailBody() {
			return this.Lines.ToArray().Join(Environment.NewLine);
		}
		/// <summary>
		/// Creates the e-mail subject line.
		/// </summary>
		/// <returns>The value that represents the subject line of the e-mail.</returns>
		protected virtual String CreateMailSubject() {
			DateTime now = DateTime.Now;
			return Res.DefaultTraceListenerSubjectFormat.FormatInvariant(this.Name, now.ToLongDateString(), now.ToLongTimeString());
		}
		/// <summary>
		/// Immediately sends any lines to the e-mail addresses in the recipient list.
		/// </summary>
		public override void Flush() {
			lock (this._linesDoor) {
				if (this.SendMailMessage()) {
					this._lines.Clear();
				}
			}
		}
		/// <summary>
		/// Attempts to send an e-mail message based on the information in this instance and returns a value indicating if a mail message was created and sent.
		/// </summary>
		/// <returns><c>true</c> if a mail message was created and sent; otherwise, <c>false</c>.</returns>
		protected Boolean SendMailMessage() {
			if (this._lines.Count == 0) {
				return false;
			}

			using (MailMessage message = new MailMessage()) {
				SmtpSection smtpSection = ConfigurationContext.Default.GetSection<SmtpSection>();
				MailAddress from = new MailAddress(smtpSection.From);

				message.From = from;
				foreach (MailAddress toAddress in this._toAddresses) {
					message.To.Add(toAddress);
				}
				message.Subject = this.CreateMailSubject();
				message.Body = this.CreateMailBody();

				using (SmtpClient client = new SmtpClient()) {
					client.Send(message);
				}
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
