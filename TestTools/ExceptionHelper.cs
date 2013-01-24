using System;

namespace Vizistata.TestTools {
	/// <summary>
	/// Provides helper methods around exceptions.  This class may not be inherited.
	/// </summary>
	public static class ExceptionHelper {
	// Methods
		/// <summary>
		/// Creates the exception as a thrown exception which adds a stack trace.
		/// </summary>
		/// <typeparam name="T">The type of exception to create.</typeparam>
		/// <param name="exception">The exception to create as a thrown exception.</param>
		/// <returns>A reference to the thrown exception created.</returns>
		/// <exception cref="System.ArgumentNullException"><paramref name="exception"/> is a null reference.</exception>
		public static T AsThrown<T>(this T exception) where T : Exception {
			if (exception == null) {
				throw new ArgumentNullException("exception");
			}

			try {
				throw exception;
			}
			catch (T ex) {
				return ex;
			}
		}
	}
}
