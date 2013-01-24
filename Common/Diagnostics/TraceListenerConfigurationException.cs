using System;
using System.Runtime.Serialization;

namespace Vizistata.Diagnostics {
	/// <summary>
	/// Represents an error that occurs because of a trace listener's configuration.
	/// </summary>
	[Serializable()]
	public class TraceListenerConfigurationException : Exception {
	// Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:TraceListenerConfigurationException"/> class.
		/// </summary>
		public TraceListenerConfigurationException() : base() { }
		/// <summary>
		/// Initializes a new instance of the <see cref="T:TraceListenerConfigurationException"/> class.
		/// </summary>
		/// <param name="message">The message that describes the error.</param>
		public TraceListenerConfigurationException(String message) : base(message) { }
		/// <summary>
		/// Initializes a new instance of the <see cref="T:TraceListenerConfigurationException"/> class.
		/// </summary>
		/// <param name="message">The message that describes the error.</param>
		/// <param name="innerException">Contains additional information about the error.</param>
		public TraceListenerConfigurationException(String message, Exception innerException) : base(message, innerException) { }
		/// <summary>
		/// Initializes a new instance of the <see cref="T:TraceListenerConfigurationException"/> class.
		/// </summary>
		/// <param name="info">Contains information used to serialize/deserialize this instance.</param>
		/// <param name="context">Describes the source and destination of the serialization.</param>
		/// <exception cref="ArgumentNullException"><paramref name="info"/> is a null reference.</exception>
		/// <exception cref="SerializationException">Any errors occur during the deserialization process.</exception>
		protected TraceListenerConfigurationException(SerializationInfo info, StreamingContext context) : base(info, context) { }
	}
}
