using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Vizistata.Linq;

using ImmutableObject = System.ComponentModel.ImmutableObjectAttribute;

namespace Vizistata {
	/// <summary>
	/// Represents the well-known formats for <see cref="T:DateTime"/> objects.  This class may not be inherited.  Instances of this class are immutable.
	/// </summary>
	[Serializable()]
	[ImmutableObject(true)]
	public sealed class DateTimeFormat : Object, IEquatable<DateTimeFormat>, IEquatable<String> {
	// Fields
		/// <summary>
		/// Represents all known formats.  This field is read-only.
		/// </summary>
		private static readonly IDictionary<String, DateTimeFormat> _allFormats = new Dictionary<String, DateTimeFormat>(StringComparer.Ordinal) {
			{ String.Empty, new DateTimeFormat("General", null) },
			{ "t", new DateTimeFormat("ShortTime", "t") },
			{ "d", new DateTimeFormat("ShortDate", "d") },
			{ "T", new DateTimeFormat("LongTime", "T") },
			{ "D", new DateTimeFormat("LongDate", "D") },
			{ "f", new DateTimeFormat("LongDateShortTime", "f") },
			{ "F", new DateTimeFormat("LongDateLongTime", "F") },
			{ "g", new DateTimeFormat("ShortDateShortTime", "g") },
			{ "G", new DateTimeFormat("ShortDateLongTime", "G") },
			{ "m", new DateTimeFormat("MonthDay", "m") },
			{ "M", new DateTimeFormat("MonthDay", "m") },
			{ "y", new DateTimeFormat("YearMonth", "y") },
			{ "Y", new DateTimeFormat("YearMonth", "y") },
			{ "r", new DateTimeFormat("Rfc1123", "r") },
			{ "R", new DateTimeFormat("Rfc1123", "r") },
			{ "s", new DateTimeFormat("SortableDateTime", "s") },
			{ "u", new DateTimeFormat("UniversalSortableDateTime", "u") }
		}.AsReadOnly();
		/// <summary>
		/// The format string represented by this instance.  This field is read-only.
		/// </summary>
		private readonly String _formatString;
		/// <summary>
		/// The name of the format.  This field is read-only.
		/// </summary>
		private readonly String _name;

	// Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:DateTimeFormat"/> class.
		/// </summary>
		/// <param name="name">The name of the format.</param>
		/// <param name="formatString">The format string represented by this instance.</param>
		private DateTimeFormat(String name, String formatString)
			: base() {
			this._name = name;
			this._formatString = formatString;
		}

	// Properties
		/// <summary>
		/// Gets the format string represented by this instance.
		/// </summary>
		public String FormatString {
			get { return this._formatString; }
		}
		/// <summary>
		/// Gets the generic date/time format.  This is the equivalent of not specifying a format.
		/// </summary>
		public static DateTimeFormat General {
			get { return DateTimeFormat._allFormats[String.Empty]; }
		}
		/// <summary>
		/// Gets the long date format, equivalent to "D".
		/// </summary>
		public static DateTimeFormat LongDate {
			get { return DateTimeFormat._allFormats["D"]; }
		}
		/// <summary>
		/// Gets the long date + long time format, equivalent to "F".
		/// </summary>
		public static DateTimeFormat LongDateLongTime {
			get { return DateTimeFormat._allFormats["F"]; }
		}
		/// <summary>
		/// Gets the long date + short time format, equivalent to "f".
		/// </summary>
		public static DateTimeFormat LongDateShortTime {
			get { return DateTimeFormat._allFormats["f"]; }
		}
		/// <summary>
		/// Gets the long time format, equivalent to "T".
		/// </summary>
		public static DateTimeFormat LongTime {
			get { return DateTimeFormat._allFormats["T"]; }
		}
		/// <summary>
		/// Gets the month/day format, equivalent to "m" or "M".
		/// </summary>
		public static DateTimeFormat MonthDay {
			get { return DateTimeFormat._allFormats["m"]; }
		}
		/// <summary>
		/// Gets the name of the format.
		/// </summary>
		public String Name {
			get { return this._name; }
		}
		/// <summary>
		/// Gets the RFC1123 format, equivalent to "r" or "R".
		/// </summary>
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Rfc", Justification = "The term is spelled correctly.")]
		public static DateTimeFormat Rfc1123 {
			get { return DateTimeFormat._allFormats["r"]; }
		}
		/// <summary>
		/// Gets the short date format, equivalent to "d".
		/// </summary>
		public static DateTimeFormat ShortDate {
			get { return DateTimeFormat._allFormats["d"]; }
		}
		/// <summary>
		/// Gets the short date + long time format, equivalent to "G".
		/// </summary>
		public static DateTimeFormat ShortDateLongTime {
			get { return DateTimeFormat._allFormats["G"]; }
		}
		/// <summary>
		/// Gets the short date + short time format, equivalent to "g".
		/// </summary>
		public static DateTimeFormat ShortDateShortTime {
			get { return DateTimeFormat._allFormats["g"]; }
		}
		/// <summary>
		/// Gets the short time format, equivalent to "t".
		/// </summary>
		public static DateTimeFormat ShortTime {
			get { return DateTimeFormat._allFormats["t"]; }
		}
		/// <summary>
		/// Gets the sortable date/time format, equivalent to "s".
		/// </summary>
		public static DateTimeFormat SortableDateTime {
			get { return DateTimeFormat._allFormats["s"]; }
		}
		/// <summary>
		/// Gets the universal sortable date/time format, equivalent to "u".
		/// </summary>
		public static DateTimeFormat UniversalSortableDateTime {
			get { return DateTimeFormat._allFormats["u"]; }
		}
		/// <summary>
		/// Gets the year/month format, equivalent to "y" or "Y".
		/// </summary>
		public static DateTimeFormat YearMonth {
			get { return DateTimeFormat._allFormats["y"]; }
		}

	// Methods
		/// <summary>
		/// Returns a value indicating if this instance is equivalent to the object specified.
		/// </summary>
		/// <param name="obj">The object to compare to this instance.</param>
		/// <returns><c>true</c> if this instance is equivalent to the object specified; otherwise, <c>false</c>.</returns>
		public override Boolean Equals(Object obj) {
			return this.Equals(obj as DateTimeFormat) || this.Equals(obj as String);
		}
		/// <summary>
		/// Returns a value indicating if this instance is equivalent to the object specified.
		/// </summary>
		/// <param name="other">The object to compare to this instance.</param>
		/// <returns><c>true</c> if this instance is equivalent to the object specified; otherwise, <c>false</c>.</returns>
		public Boolean Equals(DateTimeFormat other) {
			if (other != null) {
				return String.Equals(this._formatString, other._formatString, StringComparison.Ordinal);
			}
			return false;
		}
		/// <summary>
		/// Returns a value indicating if this instance is equivalent to the object specified.
		/// </summary>
		/// <param name="other">The object to compare to this instance.</param>
		/// <returns><c>true</c> if this instance is equivalent to the object specified; otherwise, <c>false</c>.</returns>
		public Boolean Equals(String other) {
			return String.Equals(this._formatString, other, StringComparison.Ordinal);
		}
		/// <summary>
		/// Returns a value that can be used as a hash code for this instance.
		/// </summary>
		/// <returns>A value that can be used as a hash code for this instance.</returns>
		public override Int32 GetHashCode() {
			if (this._formatString == null) {
				return 0;
			}
			return this._formatString.GetHashCode();
		}
		/// <summary>
		/// Returns the string representation of this instance.
		/// </summary>
		/// <returns>The string representation of this instance.</returns>
		public override String ToString() {
			return this._name;
		}

	// Operators
		/// <summary>
		/// Implicitly converts an object of type <see cref="T:DateTimeFormat"/> to an object of type <see cref="T:String"/>
		/// </summary>
		/// <param name="dateTimeFormat">The object to convert.</param>
		/// <returns>The converted value of the object.</returns>
		public static implicit operator String(DateTimeFormat dateTimeFormat) {
			if (dateTimeFormat == null) {
				return null;
			}
			return dateTimeFormat._formatString;
		}
		/// <summary>
		/// Explicitly converts an object of type <see cref="T:String"/> to an object of type <see cref="T:DateTimeFormat"/>
		/// </summary>
		/// <param name="format">The object to convert.</param>
		/// <returns>The converted value of the object.</returns>
		/// <exception cref="System.InvalidCastException"><paramref name="format"/> is not a valid format for formatting <see cref="T:DateTime"/> objects.</exception>
		public static explicit operator DateTimeFormat(String format) {
			if (String.IsNullOrEmpty(format)) {
				return DateTimeFormat.General;
			}

			if (!DateTimeFormat._allFormats.ContainsKey(format)) {
				throw new InvalidCastException();
			}

			return DateTimeFormat._allFormats[format];
		}
	}
}
