using System;

namespace Vizistata {
	/// <summary>
	/// Provides context about an exception that occurs.  This class may not be inherited.
	/// </summary>
	[Serializable()]
	public sealed class ExceptionEventArgs : EventArgs {
	// Fields
		/// <summary>
		/// <c>true</c> if the exeption should be bubbled up; otherwise, <c>false</c>.
		/// </summary>
		private Boolean _bubbleException;
		/// <summary>
		/// The exception.
		/// </summary>
		private readonly Exception _exception;

	// Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:ExceptionEventArgs"/> class.
		/// </summary>
		/// <param name="exception">The exception that occurred.</param>
		/// <exception cref="System.ArgumentNullException"><paramref name="exception"/> is a null reference.</exception>
		public ExceptionEventArgs(Exception exception) : this(exception, false) { }
		/// <summary>
		/// Initializes a new instance of the <see cref="T:ExceptionEventArgs"/> class.
		/// </summary>
		/// <param name="exception">The exception that occurred.</param>
		/// <param name="bubbleException"><c>true</c> if the default behavior is that the exception should be propagated; otherwise, <c>false</c>.</param>
		/// <exception cref="System.ArgumentNullException"><paramref name="exception"/> is a null reference.</exception>
		public ExceptionEventArgs(Exception exception, Boolean bubbleException)
			: base() {
			if (exception == null) {
				throw new ArgumentNullException("exception");
			}
			this._exception = exception;
			this._bubbleException = bubbleException;
		}

	// Properties
		/// <summary>
		/// Gets or sets a value indicating whether or not the exception should be propagated.
		/// </summary>
		public Boolean BubbleException {
			get { return this._bubbleException; }
			set { this._bubbleException = value; }
		}
		/// <summary>
		/// Gets the exception that occurred.
		/// </summary>
		public Exception Exception {
			get { return this._exception; }
		}
	}
}
