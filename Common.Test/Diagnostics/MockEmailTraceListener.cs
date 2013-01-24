using System;
using System.Collections.Generic;

namespace Vizistata.Diagnostics {
	/// <summary>
	/// A mock implementation of the <see cref="T:EmailTraceListener"/> class.  This class may not be inherited.
	/// </summary>
	internal sealed class MockEmailTraceListener : EmailTraceListener {
	// Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:MockEmailTraceListener"/> class.
		/// </summary>
		public MockEmailTraceListener() : base() { }
		/// <summary>
		/// Initializes a new instance of the <see cref="T:MockEmailTraceListener"/> class.
		/// </summary>
		/// <param name="toAddresses">The list of recipient e-mail addresses separated by a comma character (',').</param>
		/// <param name="name">The name of the listener.</param>
		/// <exception cref="System.ArgumentNullException"><paramref name="toAddresses"/> is a null reference.</exception>
		/// <exception cref="System.ArgumentException"><paramref name="toAddresses"/> contains 0 characters.</exception>
		/// <exception cref="System.FormatException"><paramref name="toAddresses"/> contains an e-mail address that is invalid or not supported.</exception>
		public MockEmailTraceListener(String toAddresses, String name) : base(toAddresses, name) { }

	// Properties
		/// <summary>
		/// Gets the value of the <see cref="P:EmailTraceListener.Lines"/> property.
		/// </summary>
		public IEnumerable<String> LinesDerived {
			get { return base.Lines; }
		}
		/// <summary>
		/// Gets or sets the function to use for the <see cref="M:EmailTraceListener.CreateMailBody"/> method, or a null reference to use the base implementation.
		/// </summary>
		public Func<MockEmailTraceListener, String> CreateMailBodyMethod {
			get;
			set;
		}
		/// <summary>
		/// Gets or sets the function to use for the <see cref="M:EmailTraceListener.CreateMailSubject"/> method, or a null reference to use the base implementation.
		/// </summary>
		public Func<MockEmailTraceListener, String> CreateMailSubjectMethod {
			get;
			set;
		}

	// Methods
		/// <summary>
		/// Creates the e-mail body.
		/// </summary>
		/// <returns>The value that represents the body of the e-mail.</returns>
		protected override String CreateMailBody() {
			if (this.CreateMailBodyMethod != null) {
				return this.CreateMailBodyMethod(this);
			}
			return base.CreateMailBody();
		}
		/// <summary>
		/// Returns the return value of the <see cref="M:EmailTraceListener.CreateMailBody"/> method.
		/// </summary>
		/// <returns>The return value of the <see cref="M:EmailTraceListener.CreateMailBody"/> method.</returns>
		public String CreateMailBodyDerived() {
			return this.CreateMailBody();
		}
		/// <summary>
		/// Creates the e-mail subject line.
		/// </summary>
		/// <returns>The value that represents the subject line of the e-mail.</returns>
		protected override String CreateMailSubject() {
			if (this.CreateMailSubjectMethod != null) {
				return this.CreateMailSubjectMethod(this);
			}
			return base.CreateMailSubject();
		}
		/// <summary>
		/// Returns the return value of the <see cref="M:EmailTraceListener.CreateMailSubject"/> method.
		/// </summary>
		/// <returns>The return value of the <see cref="M:EmailTraceListener.CreateMailSubject"/> method.</returns>
		public String CreateMailSubjectDerived() {
			return this.CreateMailSubject();
		}
		/// <summary>
		/// Returns the return value of the <see cref="M:EmailTraceListener.SendMailMessage"/> method.
		/// </summary>
		/// <returns>The return value of the <see cref="M:EmailTraceListener.SendMailMessage"/> method.</returns>
		public Boolean SendMailMessageDerived() {
			return this.SendMailMessage();
		}
	}
}
