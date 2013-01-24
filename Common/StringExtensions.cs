using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

using CultureInfo = System.Globalization.CultureInfo;

namespace Vizistata {
	/// <summary>
	/// Provides extension methods for the <see cref="T:String"/> class.  This class may not be inherited.
	/// </summary>
	public static class StringExtensions {
	// Methods
		/// <summary>
		/// Determines whether the string contains any of the characters specified.
		/// </summary>
		/// <param name="instance">The instance on which to call the method.</param>
		/// <param name="anyOf">A Unicode character array containing one or more characters to seek.</param>
		/// <returns><c>true</c> if the string contains any of the characters specified; otherwise, <c>false</c>.</returns>
		/// <exception cref="System.ArgumentNullException"><paramref name="instance"/> is a null reference.
		/// -or- <paramref name="anyOf"/> is a null reference.</exception>
		public static Boolean ContainsAny(this String instance, params Char[] anyOf) {
			if (instance == null) {
				throw new ArgumentNullException("instance");
			}
			if (anyOf == null) {
				throw new ArgumentNullException("anyOf");
			}

			return instance.IndexOfAny(anyOf) > -1;
		}
		/// <summary>
		/// Returns a formatted string using the current thread's culture.
		/// </summary>
		/// <param name="format">The <see cref="T:String"/> to use as the string format.</param>
		/// <param name="args">The array of string format arguments to provide.</param>
		/// <returns>The formatted string.</returns>
		/// <exception cref="System.ArgumentNullException"><paramref name="format"/> is a null reference.</exception>
		/// <exception cref="System.FormatException">The format item in <paramref name="format"/> is invalid.
		/// -or- The number indicating an argument to format is less than zero, or greater than or equal to the number of specified objects to format.</exception>
		public static String FormatCurrentCulture(this String format, params Object[] args) {
			return String.Format(CultureInfo.CurrentCulture, format, args);
		}
		/// <summary>
		/// Returns a formatted string using the current thread's UI culture.
		/// </summary>
		/// <param name="format">The <see cref="T:String"/> to use as the string format.</param>
		/// <param name="args">The array of string format arguments to provide.</param>
		/// <returns>The formatted string.</returns>
		/// <exception cref="System.ArgumentNullException"><paramref name="format"/> is a null reference.</exception>
		/// <exception cref="System.FormatException">The format item in <paramref name="format"/> is invalid.
		/// -or- The number indicating an argument to format is less than zero, or greater than or equal to the number of specified objects to format.</exception>
		[SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.IFormatProvider,System.String,System.Object[])", Justification = "This is by design.")]
		public static String FormatCurrentUICulture(this String format, params Object[] args) {
			return String.Format(CultureInfo.CurrentUICulture, format, args);
		}
		/// <summary>
		/// Returns a formatted string using the current thread's culture.
		/// </summary>
		/// <param name="format">The <see cref="T:String"/> to use as the string format.</param>
		/// <param name="args">The array of string format arguments to provide.</param>
		/// <returns>The formatted string.</returns>
		/// <exception cref="System.ArgumentNullException"><paramref name="format"/> is a null reference.</exception>
		/// <exception cref="System.FormatException">The format item in <paramref name="format"/> is invalid.
		/// -or- The number indicating an argument to format is less than zero, or greater than or equal to the number of specified objects to format.</exception>
		public static String FormatInvariant(this String format, params Object[] args) {
			return String.Format(CultureInfo.InvariantCulture, format, args);
		}
		/// <summary>
		/// Concatenates a specified separator string between each element of a specified array, yielding a single concatenated string.
		/// </summary>
		/// <param name="instance">The array of elements to join.</param>
		/// <param name="separator">The separator to apply between each two elements.</param>
		/// <returns>A string consisting of the elements of value interspersed with the separator string.</returns>
		/// <exception cref="System.ArgumentNullException"><paramref name="instance"/> is a null reference.</exception>
		public static String Join(this String[] instance, String separator) {
			if (instance == null) {
				throw new ArgumentNullException("instance");
			}
			return String.Join(separator, instance);
		}
		/// <summary>
		/// Concatenates a specified separator string between each element of a specified array, yielding a single concatenated string.
		/// </summary>
		/// <param name="instance">The array of elements to join.</param>
		/// <param name="separator">The separator to apply between each two elements.</param>
		/// <returns>A string consisting of the elements of value interspersed with the separator string.</returns>
		/// <exception cref="System.ArgumentNullException"><paramref name="instance"/> is a null reference.</exception>
		public static String Join(this String[] instance, Char separator) {
			if (instance == null) {
				throw new ArgumentNullException("instance");
			}
			return String.Join(separator.ToString(), instance);
		}
		/// <summary>
		/// Reverses the characters in the string and returns the result.
		/// </summary>
		/// <param name="value">The <see cref="T:String"/> whose characters should be reversed.</param>
		/// <returns>A <see cref="T:String"/> containing the reverse order of the characters in <paramref name="value"/>.</returns>
		/// <exception cref="System.ArgumentNullException"><paramref name="value"/> is a null reference.</exception>
		public static String Reverse(this String value) {
			if (value == null) {
				throw new ArgumentNullException("value");
			}

			if (value.Length != 0) {
				Char[] characters = value.ToCharArray();

				Char[] reversedCharacters = characters
					.Reverse()
					.ToArray();

				String reversedString = new String(reversedCharacters);
				return reversedString;
			}
			return value;
		}
	}
}
