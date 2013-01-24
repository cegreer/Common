using System;
using System.Collections.Generic;
using System.Linq;

using ThreadAbortException = System.Threading.ThreadAbortException;

namespace Vizistata {
	/// <summary>
	/// Provides extension methods for the <see cref="T:Exception"/> class.  This class may not be inherited.
	/// </summary>
	public static class ExceptionExtensions {
	// Fields
		/// <summary>
		/// The list of critical exception types which cannot be handled safely.  This field is read-only.
		/// </summary>
		private static readonly IEnumerable<Type> _criticalExceptionTypes = new Type[] {
			typeof(OutOfMemoryException),
			typeof(AppDomainUnloadedException),
			typeof(BadImageFormatException),
			typeof(CannotUnloadAppDomainException),
			// typeof(ExecutionEngineException),  This type has been deprecated in .NET 4.0.  If compiled for .NET 3.5 or earlier, uncomment this line.
			typeof(InvalidProgramException),
			typeof(ThreadAbortException)
		};

	// Methods
		/// <summary>
		/// Returns a value indicating if the exception can be handled safely within an application.
		/// </summary>
		/// <param name="instance">The exception to check.</param>
		/// <returns><c>true</c> if the exception can be handled safely; otherwise, <c>false</c>.</returns>
		/// <exception cref="System.ArgumentNullException"><paramref name="instance"/> is a null reference.</exception>
		public static Boolean CanBeHandledSafely(this Exception instance) {
			if (instance == null) {
				throw new ArgumentNullException("instance");
			}

			Type exceptionType = instance.GetType();
			if (ExceptionExtensions._criticalExceptionTypes.Contains(exceptionType)) {
				return false;
			}
			return true;
		}
		/// <summary>
		/// Flattens the list of exceptions and returns the message for each.
		/// </summary>
		/// <param name="exception">The exception from which to extract the messages.</param>
		/// <returns>The enumerable collection of messages.</returns>
		/// <exception cref="System.ArgumentNullException"><paramref name="exception"/> is a null reference.</exception>
		public static IEnumerable<String> ExtractMessages(this Exception exception) {
			if (exception == null) {
				throw new ArgumentNullException("exception");
			}

			return ExceptionExtensions.WalkExceptions(exception).Select(ex => ex.Message);
		}
		/// <summary>
		/// Walks an exception tree returning the exception and any inner exceptions in the object.
		/// </summary>
		/// <param name="exception">The exception to walk.</param>
		/// <returns>The enumerable collection of exceptions.</returns>
		private static IEnumerable<Exception> WalkExceptions(Exception exception) {
			Exception current = exception;
			while (current != null) {
				yield return current;
				current = current.InnerException;
			}
			yield break;
		}
	}
}
