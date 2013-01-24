using System;
using System.Diagnostics;

using Res = Vizistata.Properties.Resources;

namespace Vizistata.Diagnostics {
	/// <summary>
	/// Logs the performance of a particular scope.  This class may not be inherited.
	/// </summary>
	public sealed class PerformanceLoggingScope : Object, IDisposable {
	// Fields
		/// <summary>
		/// The type of event.  This field is read-only.
		/// </summary>
		private readonly TraceEventType _eventType;
		/// <summary>
		/// <c>true</c> if this instance has been disposed; otherwise, <c>false</c>.
		/// </summary>
		private Boolean _isDisposed;
		/// <summary>
		/// The name of the scope.  This field is read-only.
		/// </summary>
		private readonly String _name;
		/// <summary>
		/// The source of the scope.  This field is read-only.
		/// </summary>
		private readonly Object _source;
		/// <summary>
		/// The start time of the scope.  This field is read-only.
		/// </summary>
		private readonly DateTime _start;
		
	// Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:PerformanceLoggingScope"/> class.
		/// </summary>
		/// <param name="source">The source of the scope.  This is the object (or type) to which the name of the scope belongs.</param>
		/// <param name="name">The name of the scope.  This is usually the property or method name.</param>
		///	<exception cref="System.ArgumentNullException"><paramref name="source"/> is a null reference.
		///	-or- <paramref name="name"/> is a null reference.</exception>
		public PerformanceLoggingScope(Object source, String name) : this(source, name, TraceEventType.Information) { }
		/// <summary>
		/// Initializes a new instance of the <see cref="T:PerformanceLoggingScope"/> class.
		/// </summary>
		/// <param name="source">The source of the scope.  This is the object (or type) to which the name of the scope belongs.</param>
		/// <param name="name">The name of the scope.  This is usually the property or method name.</param>
		/// <param name="eventType">The type of event type to log.</param>
		///	<exception cref="System.ArgumentNullException"><paramref name="source"/> is a null reference.
		///	-or- <paramref name="name"/> is a null reference.</exception>
		///	<exception cref="System.ArgumentOutOfRangeException"><paramref name="eventType"/> is not defined by <see cref="T:TraceEventType"/>.</exception>
		public PerformanceLoggingScope(Object source, String name, TraceEventType eventType)
			: base() {
			if (source == null) {
				throw new ArgumentNullException("source");
			}
			if (name == null) {
				throw new ArgumentNullException("name");
			}
			switch (eventType) {
				case TraceEventType.Critical:
				case TraceEventType.Error:
				case TraceEventType.Information:
				case TraceEventType.Resume:
				case TraceEventType.Start:
				case TraceEventType.Stop:
				case TraceEventType.Suspend:
				case TraceEventType.Transfer:
				case TraceEventType.Verbose:
				case TraceEventType.Warning:
					break;
				default:
					throw new ArgumentOutOfRangeException("eventType");
			}

			this._source = source;
			this._name = name;
			this._eventType = eventType;
			this._start = DateTime.Now;
			this.WriteStart();
		}

	// Methods
		/// <summary>
		/// Disposes of any resources held by this instance.
		/// </summary>
		public void Dispose() {
			if (!this._isDisposed) {
				this._isDisposed = true;
				this.WriteFinish();
			}
		}
		/// <summary>
		/// Writes the finish log event.
		/// </summary>
		private void WriteFinish() {
			lock (LoggingExtensions.SynchronizationObject) {
				try {
					DateTime finish = DateTime.Now;
					TimeSpan duration = finish - this._start;
					LoggingExtensions.Log(this._eventType, this._source, Res.PerformanceLoggingScopeFinishFormat, new Object[] { (this._source as Type ?? this._source.GetType()).Name, this._name, finish, duration });
				}
				finally {
					try {
						LoggingExtensions.Unindent();
					}
					catch (InvalidOperationException) { /* Someone already unindented. */ }
				}
			}
		}
		/// <summary>
		/// Writes the start log event.
		/// </summary>
		private void WriteStart() {
			lock (LoggingExtensions.SynchronizationObject) {
				LoggingExtensions.Log(this._eventType, this._source, Res.PerformanceLoggingScopeStartFormat, new Object[] { (this._source as Type ?? this._source.GetType()).Name, this._name, this._start });
				LoggingExtensions.Indent();
			}
		}
	}
}
