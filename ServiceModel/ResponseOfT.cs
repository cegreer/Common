using System;
using System.Runtime.Serialization;

namespace Vizistata.ServiceModel {
	/// <summary>
	/// A strongly-typed response used for service calls that return values.  This class may not be inherited.
	/// </summary>
	/// <typeparam name="T">The type of value to return.</typeparam>
	[DataContract()]
	public sealed class Response<T> : Response {
	// Fields
		/// <summary>
		/// The return value.
		/// </summary>
		[DataMember(Name = "ReturnValue")]
		private T _returnValue;

	// Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:Response&lt;T&gt;"/> class as a successful response.
		/// </summary>
		/// <param name="returnValue">The return value of the service call.</param>
		public Response(T returnValue)
			: base() {
			this._returnValue = returnValue;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="T:Response&lt;T&gt;"/> class as a failure response.
		/// </summary>
		/// <param name="exception">The error that occurred.</param>
		/// <exception cref="System.ArgumentNullException"><paramref name="exception"/>is a null reference.</exception>
		public Response(Exception exception) : base(exception) { }
		/// <summary>
		/// Initializes a new instance of the <see cref="T:Response&lt;T&gt;"/> class as a failure response.
		/// </summary>
		/// <param name="error">The error that occurred.</param>
		/// <exception cref="System.ArgumentNullException"><paramref name="error"/>is a null reference.</exception>
		public Response(ServiceError error) : base(error) { }

	// Properties
		/// <summary>
		/// Gets the return value from the service call.
		/// </summary>
		public T ReturnValue {
			get { return this._returnValue; }
		}
	}
}
