using System;
using System.Runtime.Serialization;

namespace Vizistata.ServiceModel {
	/// <summary>
	/// Represents a strongly-typed response from a service call.
	/// </summary>
	[DataContract()]
	public class Response : Object {
	// Fields
		/// <summary>
		/// The error that occurred, or a null reference.
		/// </summary>
		[DataMember(Name = "Error")]
		private ServiceError _error;
		/// <summary>
		/// <c>true</c> if the operation was successful; otherwise, <c>false</c>.
		/// </summary>
		[DataMember(Name = "IsSuccessful")]
		private Boolean _isSuccessful;

	// Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:Response"/> class as a successful response.
		/// </summary>
		public Response()
			: base() {
			this._isSuccessful = true;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="T:Response"/> class as a failure response.
		/// </summary>
		/// <param name="exception">The error that occurred</param>
		/// <exception cref="System.ArgumentNullException"><paramref name="exception"/> is a null reference.</exception>
		public Response(Exception exception)
			: base() {
			if (exception == null) {
				throw new ArgumentNullException("exception");
			}
			
			this._error = new ServiceError(exception);
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="T:Response"/> class as a failure response.
		/// </summary>
		/// <param name="error">The error that occurred.</param>
		/// <exception cref="System.ArgumentNullException"><paramref name="error"/>is a null reference.</exception>
		public Response(ServiceError error)
			: base() {
			if (error == null) {
				throw new ArgumentNullException("error");
			}

			this._error = error;
		}

	// Properties
		/// <summary>
		/// Gets the error that occurred, or a null reference if <see cref="P:IsSuccessful"/> is <c>true</c>.
		/// </summary>
		public ServiceError Error {
			get { return this._error; }
		}
		/// <summary>
		/// Gets a value indicating if this instance represents a successful response.
		/// </summary>
		public Boolean IsSuccessful {
			get { return this._isSuccessful; }
		}
	}
}
