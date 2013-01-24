using System;
using System.Diagnostics;
using System.Linq;

using Localizable = System.ComponentModel.LocalizableAttribute;
using Res = Vizistata.Properties.Resources;

namespace Vizistata.Diagnostics {
	/// <summary>
	/// Provides logging functionality for the application.  This class may not be inherited.
	/// </summary>
	public static class LoggingExtensions {
	// Fields
		/// <summary>
		/// The current indentation level.
		/// </summary>
		private static Int32 _indentLevel;
		/// <summary>
		/// The size of each indentation level in spaces.
		/// </summary>
		private static Int32 _indentSize = 2;
		/// <summary>
		/// The trace source to which to log information.  This field is read-only.
		/// </summary>
		private static readonly TraceSource _source = new TraceSource(LoggingExtensions.TraceSourceName, SourceLevels.Warning);
		/// <summary>
		/// Allows the operations in this instance to be thread-safe.
		/// </summary>
		private static readonly Object _synchronizationObject = new Object();
		/// <summary>
		/// Gets the name of the trace source used by this type.  This field is read-only.
		/// </summary>
		public static readonly String TraceSourceName = "Vizistata";

	// Properties
		/// <summary>
		/// Gets or sets the indentation level.
		/// </summary>
		public static Int32 IndentLevel {
			get { return LoggingExtensions._indentLevel; }
			set {
				if (value < 0) {
					throw new ArgumentOutOfRangeException("value");
				}
				LoggingExtensions._indentLevel = value;
			}
		}
		/// <summary>
		/// Gets or sets the number of spaces for an indentation level.
		/// </summary>
		public static Int32 IndentSize {
			get { return LoggingExtensions._indentSize; }
			set {
				if (value < 0) {
					throw new ArgumentOutOfRangeException("value");
				}
				LoggingExtensions._indentSize = value;
			}
		}
		/// <summary>
		/// Gets the object that can be locked to make this method's operations thread-safe.
		/// </summary>
		public static Object SynchronizationObject {
			get { return LoggingExtensions._synchronizationObject; }
		}

	// Methods
		/// <summary>
		/// Creates a new instance of the <see cref="T:PerformanceLoggingScope"/> class.
		/// </summary>
		/// <param name="source">The source of the scope.  This is the object (or type) to which the name of the scope belongs.</param>
		/// <param name="name">The name of the scope.  This is usually the property or method name.</param>
		///	<exception cref="System.ArgumentNullException"><paramref name="source"/> is a null reference.
		///	-or- <paramref name="name"/> is a null reference.</exception>
		public static PerformanceLoggingScope CreatePerformanceLoggingScope(this Object source, String name) {
			return new PerformanceLoggingScope(source, name);
		}
		/// <summary>
		/// Creates a new instance of the <see cref="T:PerformanceLoggingScope"/> class.
		/// </summary>
		/// <param name="source">The source of the scope.  This is the object (or type) to which the name of the scope belongs.</param>
		/// <param name="name">The name of the scope.  This is usually the property or method name.</param>
		/// <param name="eventType">The type of event type to log.</param>
		///	<exception cref="System.ArgumentNullException"><paramref name="source"/> is a null reference.
		///	-or- <paramref name="name"/> is a null reference.</exception>
		///	<exception cref="System.ArgumentOutOfRangeException"><paramref name="eventType"/> is not defined by <see cref="T:TraceEventType"/>.</exception>
		public static PerformanceLoggingScope CreatePerformanceLoggingScope(this Object source, String name, TraceEventType eventType) {
			return new PerformanceLoggingScope(source, name, eventType);
		}
		/// <summary>
		/// Indents the logging by one level.
		/// </summary>
		public static void Indent() {
			LoggingExtensions._indentLevel++;
		}
		/// <summary>
		/// Logs a mesage to the trace source.
		/// </summary>
		/// <param name="eventType">The event type to log.</param>
		/// <param name="instance">The object that caused the event.</param>
		/// <param name="message">The message or format to log.</param>
		/// <param name="args">The optional list of arguments to provide when formatting <paramref name="message"/>.</param>
		/// <exception cref="System.ArgumentNullException"><paramref name="instance"/> is a null reference.
		/// -or- <paramref name="message"/> is a null reference.</exception>
		internal static void Log(TraceEventType eventType, Object instance, String message, Object[] args) {
			if (instance == null) {
				throw new ArgumentNullException("instance");
			}
			if (message == null) {
				throw new ArgumentNullException("message");
			}

			Type type = (instance as Type) ?? instance.GetType();
			if (args == null || args.Length == 0) {
				LoggingExtensions._source.TraceEvent(eventType, type.GetHashCode(), message);
			}
			else {
				LoggingExtensions._source.TraceEvent(eventType, type.GetHashCode(), message, args);
			}
			LoggingExtensions._source.Flush();
		}
		/// <summary>
		/// Logs a critical message.
		/// </summary>
		/// <param name="instance">The instance on which to invoke the method.</param>
		/// <param name="message">The message or format to log.</param>
		/// <param name="args">The optional list of arguments to provide when formatting <paramref name="message"/>.</param>
		/// <exception cref="System.ArgumentNullException"><paramref name="instance"/> is a null reference.
		/// -or- <paramref name="message"/> is a null reference.</exception>
		[Localizable(false)]
		public static void LogCritical(this Object instance, String message, params Object[] args) {
			LoggingExtensions.Log(TraceEventType.Critical, instance, message, args);
		}
		/// <summary>
		/// Logs an error message.
		/// </summary>
		/// <param name="instance">The instance on which to invoke the method.</param>
		/// <param name="message">The message or format to log.</param>
		/// <param name="args">The optional list of arguments to provide when formatting <paramref name="message"/>.</param>
		/// <exception cref="System.ArgumentNullException"><paramref name="instance"/> is a null reference.
		/// -or- <paramref name="message"/> is a null reference.</exception>
		[Localizable(false)]
		public static void LogError(this Object instance, String message, params Object[] args) {
			LoggingExtensions.Log(TraceEventType.Error, instance, message, args);
		}
		/// <summary>
		/// Logs an information message.
		/// </summary>
		/// <param name="instance">The instance on which to invoke the method.</param>
		/// <param name="message">The message or format to log.</param>
		/// <param name="args">The optional list of arguments to provide when formatting <paramref name="message"/>.</param>
		/// <exception cref="System.ArgumentNullException"><paramref name="instance"/> is a null reference.
		/// -or- <paramref name="message"/> is a null reference.</exception>
		[Localizable(false)]
		public static void LogInformation(this Object instance, String message, params Object[] args) {
			LoggingExtensions.Log(TraceEventType.Information, instance, message, args);
		}
		/// <summary>
		/// Logs a verbose message.
		/// </summary>
		/// <param name="instance">The instance on which to invoke the method.</param>
		/// <param name="message">The message or format to log.</param>
		/// <param name="args">The optional list of arguments to provide when formatting <paramref name="message"/>.</param>
		/// <exception cref="System.ArgumentNullException"><paramref name="instance"/> is a null reference.
		/// -or- <paramref name="message"/> is a null reference.</exception>
		[Localizable(false)]
		public static void LogVerbose(this Object instance, String message, params Object[] args) {
			LoggingExtensions.Log(TraceEventType.Verbose, instance, message, args);
		}
		/// <summary>
		/// Logs a warning message.
		/// </summary>
		/// <param name="instance">The instance on which to invoke the method.</param>
		/// <param name="message">The message or format to log.</param>
		/// <param name="args">The optional list of arguments to provide when formatting <paramref name="message"/>.</param>
		/// <exception cref="System.ArgumentNullException"><paramref name="instance"/> is a null reference.
		/// -or- <paramref name="message"/> is a null reference.</exception>
		[Localizable(false)]
		public static void LogWarning(this Object instance, String message, params Object[] args) {
			LoggingExtensions.Log(TraceEventType.Warning, instance, message, args);
		}
		/// <summary>
		/// Tests a trace listener to ensure it can log correctly.
		/// </summary>
		/// <param name="instance">The instance on which to invoke the method.</param>
		/// <param name="message">The message to test.</param>
		/// <param name="category">The optional category to use to test.</param>
		/// <exception cref="Vizistata.Diagnostics.TraceListenerConfigurationException">The trace listener is not setup correctly or does not have sufficient privileges to log messages.</exception>
		private static void TestListener(this TraceListener instance, String message, String category) {
			Debug.Assert(instance != null);
			Debug.Assert(message != null);
			instance.Write(message, category);

			// TextWriterTraceListener, DelimitedListTraceListener, EventSchemaTraceListener, and XmlWriterTraceListener
			TextWriterTraceListener textWriterTraceListener = instance as TextWriterTraceListener;
			if (textWriterTraceListener != null) {
				if (textWriterTraceListener.Writer == null) {
					throw new TraceListenerConfigurationException(Res.TextWriterTraceListenerLoggingErrorMessage);
				}
				return;
			}
			EventLogTraceListener eventLogTraceListener = instance as EventLogTraceListener;
			if (eventLogTraceListener != null) {
				if (eventLogTraceListener.EventLog == null) {
					throw new TraceListenerConfigurationException(Res.EventLogTraceListenerLoggingErrorMessage);
				}
				return;
			}
		}
		/// <summary>
		/// Tests the logging implementation to ensure that it is working.
		/// </summary>
		/// <param name="message">The message to use for testing the logging functionality.</param>
		/// <exception cref="System.ArgumentNullException"><paramref name="message"/> is a null reference.</exception>
		/// <exception cref="Vizistata.Diagnostics.TraceListenerConfigurationException">The trace source has no listeners other than the default listener.
		/// -or- A trace listener is not setup correctly or does not have sufficient privileges to log messages.</exception>
		public static void TestLogging(String message) {
			if (message == null) {
				throw new ArgumentNullException("message");
			}
			TraceListener[] listeners = LoggingExtensions._source.Listeners
				.Cast<TraceListener>()
				.Where(listener => !(listener is DefaultTraceListener))
				.ToArray();
			if (listeners.Length == 0) {
				throw new TraceListenerConfigurationException(Res.NoTraceListenersInConfigFileMessage);
			}
			foreach (TraceListener listener in listeners) {
				listener.TestListener(message + Environment.NewLine, typeof(LoggingExtensions).FullName);
				listener.Flush();
			}
		}
		/// <summary>
		/// Unindents the logging by one level.
		/// </summary>
		/// <exception cref="System.InvalidOperationException">The value of <see cref="P:IndentLevel"/> is less than 1.</exception>
		public static void Unindent() {
			if (LoggingExtensions._indentLevel < 1) {
				throw new InvalidOperationException("The logging mechanism cannot be un-indented further.");
			}
			LoggingExtensions._indentLevel--;
		}
	}
}
