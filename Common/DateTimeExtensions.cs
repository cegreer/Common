using System;

using CultureInfo = System.Globalization.CultureInfo;

namespace Vizistata {
	/// <summary>
	/// Provides extension methods for the <see cref="T:DateTime"/> struct.  This class may not be inherited.
	/// </summary>
	public static class DateTimeExtensions {
	// Methods
		/// <summary>
		/// Formats the date and time object using the current culture.
		/// </summary>
		/// <param name="dateTime">The date and time to format.</param>
		/// <returns>The formatted string for the date and time object.</returns>
		public static String FormatCurrentCulture(this DateTime dateTime) {
			return dateTime.FormatCurrentCulture(DateTimeFormat.General);
		}
		/// <summary>
		/// Formats the date and time object using the current culture.
		/// </summary>
		/// <param name="dateTime">The date and time to format.</param>
		/// <param name="format">The string format to use for formatting.</param>
		/// <returns>The formatted string for the date and time object.</returns>
		/// <exception cref="System.FormatException">The length of <paramref name="format"/> is 1, and it is not one of the format specifier characters defined for <see cref="T:DateTimeFormatInfo"/>.
		/// -or- <paramref name="format"/> does not contain a valid custom format pattern.</exception>
		public static String FormatCurrentCulture(this DateTime dateTime, String format) {
			return dateTime.ToString(format, CultureInfo.CurrentCulture);
		}
		/// <summary>
		/// Formats the date and time object using the current culture.
		/// </summary>
		/// <param name="dateTime">The date and time to format.</param>
		/// <returns>The formatted string for the date and time object.</returns>
		public static String FormatCurrentCulture(this DateTime? dateTime) {
			return dateTime.FormatCurrentCulture(DateTimeFormat.General);
		}
		/// <summary>
		/// Formats the date and time object using the current culture.
		/// </summary>
		/// <param name="dateTime">The date and time to format.</param>
		/// <param name="format">The string format to use for formatting.</param>
		/// <returns>The formatted string for the date and time object.</returns>
		/// <exception cref="System.FormatException">The length of <paramref name="format"/> is 1, and it is not one of the format specifier characters defined for <see cref="T:DateTimeFormatInfo"/>.
		/// -or- <paramref name="format"/> does not contain a valid custom format pattern.</exception>
		public static String FormatCurrentCulture(this DateTime? dateTime, String format) {
			if (dateTime.HasValue) {
				return dateTime.Value.FormatCurrentCulture(format);
			}
			return String.Empty;
		}
		/// <summary>
		/// Formats the date and time object using the invariant culture.
		/// </summary>
		/// <param name="dateTime">The date and time to format.</param>
		/// <returns>The formatted string for the date and time object.</returns>
		public static String FormatInvariant(this DateTime dateTime) {
			return dateTime.FormatInvariant(DateTimeFormat.General);
		}
		/// <summary>
		/// Formats the date and time object using the invariant culture.
		/// </summary>
		/// <param name="dateTime">The date and time to format.</param>
		/// <param name="format">The string format to use for formatting.</param>
		/// <returns>The formatted string for the date and time object.</returns>
		/// <exception cref="System.FormatException">The length of <paramref name="format"/> is 1, and it is not one of the format specifier characters defined for <see cref="T:DateTimeFormatInfo"/>.
		/// -or- <paramref name="format"/> does not contain a valid custom format pattern.</exception>
		public static String FormatInvariant(this DateTime dateTime, String format) {
			return dateTime.ToString(format, CultureInfo.InvariantCulture);
		}
		/// <summary>
		/// Formats the date and time object using the invariant culture.
		/// </summary>
		/// <param name="dateTime">The date and time to format.</param>
		/// <returns>The formatted string for the date and time object.</returns>
		public static String FormatInvariant(this DateTime? dateTime) {
			return dateTime.FormatInvariant(DateTimeFormat.General);
		}
		/// <summary>
		/// Formats the date and time object using the invariant culture.
		/// </summary>
		/// <param name="dateTime">The date and time to format.</param>
		/// <param name="format">The string format to use for formatting.</param>
		/// <returns>The formatted string for the date and time object.</returns>
		/// <exception cref="System.FormatException">The length of <paramref name="format"/> is 1, and it is not one of the format specifier characters defined for <see cref="T:DateTimeFormatInfo"/>.
		/// -or- <paramref name="format"/> does not contain a valid custom format pattern.</exception>
		public static String FormatInvariant(this DateTime? dateTime, String format) {
			if (dateTime.HasValue) {
				return dateTime.Value.FormatInvariant(format);
			}
			return String.Empty;
		}
		/// <summary>
		/// Returns a value indicating if the date/time value is valid for SQL Server.
		/// </summary>
		/// <param name="dateTime">The date/time value to check.</param>
		/// <returns><c>true</c> if the date/time value is valid for SQL Server; otherwise, <c>false</c>.</returns>
		public static Boolean IsValidForSqlServer(this DateTime dateTime) {
			return (dateTime >= new DateTime(1753, 1, 1) && dateTime <= new DateTime(9999, 12, 31, 23, 59, 59, 997));
		}
	}
}
