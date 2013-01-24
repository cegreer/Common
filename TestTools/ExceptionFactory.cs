using System;
using System.Runtime.Serialization;
using Vizistata.Reflection;

namespace Vizistata.TestTools {
	/// <summary>
	/// Used to create exceptions.  This class may not be inherited.
	/// </summary>
	/// <typeparam name="T">The type of exception to create.</typeparam>
	[Serializable()]
	public sealed class ExceptionFactory<T> : Object where T : Exception {
	// Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:ExceptionFactory&lt;T&gt;"/> class.
		/// </summary>
		public ExceptionFactory() : base() { }

	// Methods
		/// <summary>
		/// Creates and returns an instance of the exceptions represented by this type.
		/// </summary>
		/// <param name="info">Contains the data needed to create the exception.</param>
		/// <param name="context">Describes the source and destination of the deserialization.</param>
		/// <returns>A reference to the exception created.</returns>
		public T CreateInstance(SerializationInfo info, StreamingContext context) {
			Type[] argTypes = new Type[] { typeof(SerializationInfo), typeof(StreamingContext) };
			Object[] args = new Object[] { info, context };
			Object instance = typeof(T).CreateInstance(argTypes, args);
			return (T)instance;
		}
	}
}
