﻿using System;

using CultureInfo = System.Globalization.CultureInfo;

namespace Vizistata {
	/// <summary>
	/// Provides extension methods for the <see cref="T:Double"/> class.  This class may not be inherited.
	/// </summary>
	public static class DoubleExtensions {
	// Methods
		/// <summary>
		/// Returns the string representation of a number using the current culture as a format provider.
		/// </summary>
		/// <param name="value">The value whose string representation should be returned.</param>
		/// <returns>The string representation of the number.</returns>
		public static String FormatCurrentCulture(this Double value) {
			return value.ToString(CultureInfo.CurrentCulture);
		}
		/// <summary>
		/// Returns the string representation of a number using the current culture as a format provider.
		/// </summary>
		/// <param name="value">The value whose string representation should be returned.</param>
		/// <param name="format">The string format to use.</param>
		/// <returns>The string representation of the number.</returns>
		public static String FormatCurrentCulture(this Double value, String format) {
			return value.ToString(format, CultureInfo.CurrentCulture);
		}
		/// <summary>
		/// Returns the string representation of a number using the current culture as a format provider.
		/// </summary>
		/// <param name="value">The value whose string representation should be returned.</param>
		/// <returns>The string representation of the number.</returns>
		public static String FormatCurrentCulture(this Double? value) {
			if (value.HasValue) {
				return value.Value.FormatCurrentCulture();
			}
			return String.Empty;
		}
		/// <summary>
		/// Returns the string representation of a number using the current culture as a format provider.
		/// </summary>
		/// <param name="value">The value whose string representation should be returned.</param>
		/// <param name="format">The string format to use.</param>
		/// <returns>The string representation of the number.</returns>
		public static String FormatCurrentCulture(this Double? value, String format) {
			if (value.HasValue) {
				return value.Value.FormatCurrentCulture(format);
			}
			return String.Empty;
		}
		/// <summary>
		/// Returns the string representation of a number using the invariant culture as a format provider.
		/// </summary>
		/// <param name="value">The value whose string representation should be returned.</param>
		/// <returns>The string representation of the number.</returns>
		public static String FormatInvariant(this Double value) {
			return value.ToString(CultureInfo.InvariantCulture);
		}
		/// <summary>
		/// Returns the string representation of a number using the invariant culture as a format provider.
		/// </summary>
		/// <param name="value">The value whose string representation should be returned.</param>
		/// <param name="format">The string format to use.</param>
		/// <returns>The string representation of the number.</returns>
		public static String FormatInvariant(this Double value, String format) {
			return value.ToString(format, CultureInfo.InvariantCulture);
		}
		/// <summary>
		/// Returns the string representation of a number using the invariant culture as a format provider.
		/// </summary>
		/// <param name="value">The value whose string representation should be returned.</param>
		/// <returns>The string representation of the number.</returns>
		public static String FormatInvariant(this Double? value) {
			if (value.HasValue) {
				return value.Value.FormatInvariant();
			}
			return String.Empty;
		}
		/// <summary>
		/// Returns the string representation of a number using the invariant culture as a format provider.
		/// </summary>
		/// <param name="value">The value whose string representation should be returned.</param>
		/// <param name="format">The string format to use.</param>
		/// <returns>The string representation of the number.</returns>
		public static String FormatInvariant(this Double? value, String format) {
			if (value.HasValue) {
				return value.Value.FormatInvariant(format);
			}
			return String.Empty;
		}
	}
}
