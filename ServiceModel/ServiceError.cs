using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Vizistata.Reflection;

using DictionaryEntry = System.Collections.DictionaryEntry;
using IDictionary = System.Collections.IDictionary;

namespace Vizistata.ServiceModel {
	/// <summary>
	/// Represents an error that occurs during a service call.  This class may not be inherited.
	/// </summary>
	[DataContract()]
	public sealed class ServiceError : Object {
	// Fields
		/// <summary>
		/// The name of the class for the exception.
		/// </summary>
		[DataMember(Name = "ClassName")]
		private String _className;
		/// <summary>
		/// Contains the collection of data from the exception.
		/// </summary>
		[DataMember(Name = "Data")]
		private IDictionary _data;
		/// <summary>
		/// The HResults of the exception.
		/// </summary>
		[DataMember(Name = "HResult")]
		private Int32 _hResult;
		/// <summary>
		/// The inner error, or a null reference.
		/// </summary>
		[DataMember(Name = "InnerError")]
		private ServiceError _innerError;
		/// <summary>
		/// The exception message.
		/// </summary>
		[DataMember(Name = "Message")]
		private String _message;
		/// <summary>
		/// The stack trace for the exception.
		/// </summary>
		[DataMember(Name = "StackTrace")]
		private String _stackTrace;
		/// <summary>
		/// The full type name of the exception.
		/// </summary>
		[DataMember(Name = "TypeName")]
		private String _typeName;

	// Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:ServiceError"/> class.
		/// </summary>
		/// <param name="exception">The exception to wrap.</param>
		/// <exception cref="System.ArgumentNullException"><paramref name="exception"/> is a null reference.</exception>
		public ServiceError(Exception exception)
			: base() {
			if (exception == null) {
				throw new ArgumentNullException("exception");
			}

			this._className = exception.GetType().Name;
			this._typeName = exception.GetType().FullName;
			this._message = exception.Message;
			this._stackTrace = exception.StackTrace;
			this._data = exception.Data;
			this._hResult = (Int32)exception.GetPropertyValue("HResult");
			this._innerError = exception.InnerException != null ? new ServiceError(exception.InnerException) : null;
		}

	// Properties
		/// <summary>
		/// Gets the collection key/value pairs taht provide additional user-defined information about the error.
		/// </summary>
		public IDictionary<Object, Object> Data {
			get {
				return this._data
					.Cast<DictionaryEntry>()
					.ToDictionary(entry => entry.Key, entry => entry.Value);
			}
		}
		/// <summary>
		/// Gets the <see cref="T:Error"/> that caused the current error.
		/// </summary>
		public ServiceError InnerError {
			get { return this._innerError; }
		}
		/// <summary>
		/// Gets a message that describes the current error.
		/// </summary>
		public String Message {
			get { return this._message; }
		}
		/// <summary>
		/// Gets a string representation of the immediate frames on the call stack.
		/// </summary>
		public String StackTrace {
			get { return this._stackTrace; }
		}
		/// <summary>
		/// Gets the fully qualified name of the <see cref="T:Type"/>, including the namespace of the <see cref="T:Type"/> but not the assembly.
		/// </summary>
		public String TypeName {
			get { return this._typeName; }
		}

	// Methods
		/// <summary>
		/// Returns the <see cref="T:Exception"/> represented by this instance.
		/// </summary>
		/// <returns>The <see cref="T:Exception"/> represented by this instance.</returns>
		public Exception ToException() {
			Type exceptionType = Type.GetType(this._typeName);
			
			SerializationInfo info = new SerializationInfo(exceptionType, new FormatterConverter());
			info.AddValue("ClassName", this._className, typeof(String));
			info.AddValue("Message", this._message, typeof(String));
			info.AddValue("Data", this._data, typeof(IDictionary));
			info.AddValue("HelpURL", null, typeof(String));
			info.AddValue("StackTraceString", null, typeof(String));
			info.AddValue("RemoteStackTraceString", this._stackTrace, typeof(String));
			info.AddValue("RemoteStackIndex", 0, typeof(Int32));
			info.AddValue("ExceptionMethod", null, typeof(String));
			info.AddValue("HResult", this._hResult, typeof(Int32));
			info.AddValue("Source", null, typeof(String));
			Exception innerException = this._innerError != null ? this._innerError.ToException() : null;
			if (innerException != null) {
				info.AddValue("InnerException", innerException, typeof(Exception));
			}

			StreamingContext context = new StreamingContext(StreamingContextStates.All);
			Exception exception = (Exception)exceptionType.CreateInstance(info, context);
			return exception;
		}
	}
}
