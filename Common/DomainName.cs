using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

using ImmutableObject = System.ComponentModel.ImmutableObjectAttribute;
using Res = Vizistata.Properties.Resources;

namespace Vizistata {
	/// <summary>
	/// Represents a domain name in DNS.  This class may not be inherited.  Instances of this class are immutable.
	/// </summary>
	/// <remarks>For business rules regarding this class, see the Wikipedia article here: http://en.wikipedia.org/wiki/Domain_name. </remarks>
	[Serializable()]
	[ImmutableObject(true)]
	public sealed class DomainName : Object, IEquatable<DomainName>, IEquatable<String> {
	// Fields
		/// <summary>
		/// The first-level domain label.  This field is read-only.
		/// </summary>
		private readonly String _firstLevelLabel;
		/// <summary>
		/// The second-level domain label.  This field is read-only.
		/// </summary>
		private readonly String _secondLevelLabel;
		/// <summary>
		/// The sub-domain labels.  This field is read-only.
		/// </summary>
		private readonly String[] _subDomainLabels;

	// Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:DomainName"/> class.
		/// </summary>
		/// <param name="firstLevelLabel">The first-level label.  For example, in the domain name "www.example.com", the first-level label is "com".</param>
		/// <param name="secondLevelLabel">The second-level label.  For example, in the domain name "www.example.com", the second-level label is "example".</param>
		/// <param name="subDomainLabels">The sub-domain labels.  For example, in the domain name "www.example.com", an array with the value "www" would be returned.  Also, in the domain name "somewhere.else.example.com", an ordered array with the values "else" and "somewhere" would be returned.</param>
		/// <exception cref="System.ArgumentNullException"><paramref name="firstLevelLabel"/> is a null reference.
		/// -or- <paramref name="secondLevelLabel"/> is a null reference.</exception>
		/// <exception cref="System.ArgumentException"><paramref name="firstLevelLabel"/> is empty.
		/// -or- <paramref name="firstLevelLabel"/> contains characters not defined by <see cref="M:GetValidDomainLabelCharacters"/>.
		/// -or- <paramref name="secondLevelLabel"/> is empty.
		/// -or- <paramref name="secondLevelLabel"/> contains characters not defined by <see cref="M:GetValidDomainLabelCharacters"/>.
		/// -or- <paramref name="subDomainLabels"/> contains an element that is a null reference.
		/// -or- <paramref name="subDomainLabels"/> contains an element that is empty.
		/// -or- <paramref name="subDomainLabels"/> contains an element that contains characters not defined by <see cref="M:GetValidDomainLabelCharacters"/>.</exception>
		/// <exception cref="System.FormatException">The length of the domain name would exceed 253 characters.</exception>
		public DomainName(String firstLevelLabel, String secondLevelLabel, params String[] subDomainLabels)
			: base() {
			// Check for nulls.
			if (firstLevelLabel == null) {
				throw new ArgumentNullException("firstLevelLabel");
			}
			if (secondLevelLabel == null) {
				throw new ArgumentNullException("secondLevelLabel");
			}
			subDomainLabels = subDomainLabels ?? new String[0];
			if (subDomainLabels.Any(label => label == null)) {
				throw new ArgumentException(Res.CollectionContainsNullReferenceMessage, "subDomainLabels");
			}
			
			// Check for 127 labels or fewer.
			if (subDomainLabels.Length > 125) {
				throw new ArgumentException(Res.DomainNameHasTooManyLabelsMessage, "subDomainLabels");
			}

			// Check that the length of each label is at least 1.
			if (firstLevelLabel.Length == 0) {
				throw new ArgumentException(Res.DomainLabelIsEmptyMessage, "firstLevelLabel");
			}
			if (secondLevelLabel.Length == 0) {
				throw new ArgumentException(Res.DomainLabelIsEmptyMessage, "secondLevelLabel");
			}
			if (subDomainLabels.Any(label => label.Length == 0)) {
				throw new ArgumentException(Res.DomainLabelIsEmptyMessage, "subDomainLabels");
			}
			
			// Check for 253 characters or fewer.
			Int32 numberOfPeriods = subDomainLabels.Length + 1;
			Int32 characterCount = firstLevelLabel.Length
				+ secondLevelLabel.Length
				+ subDomainLabels.Sum(label => label.Length)
				+ numberOfPeriods;
			if (characterCount > 253) {
				throw new FormatException(Res.DomainNameLengthTooLargeMessage);
			}
			
			// Check for invalid characters.
			Char[] validDomainLabelCharacters = DomainName.GetValidDomainLabelCharacters();
			Action<String, String> validateLabel = (label, argumentName) => {
				for (Int32 i = 0; i < label.Length; i++) {
					Char character = label[i];
					if (!validDomainLabelCharacters.Contains(character)) {
						throw new ArgumentException(Res.InvalidCharacterAtSpecifiedPositionFormat.FormatInvariant(character, i), argumentName);
					}
				}
			};
			validateLabel(firstLevelLabel, "firstLevelLabel");
			validateLabel(secondLevelLabel, "secondLevelLabel");
			foreach (String subDomainLabel in subDomainLabels) {
				validateLabel(subDomainLabel, "subDomainLabels");
			}
			
			// We are good!  Save the values.
			this._firstLevelLabel = firstLevelLabel;
			this._secondLevelLabel = secondLevelLabel;
			this._subDomainLabels = subDomainLabels;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="T:DomainName"/> class.
		/// </summary>
		/// <param name="labels">The domain labels.</param>
		private DomainName(IEnumerable<String> labels)
			: base() {
			Debug.Assert(labels != null);
			Debug.Assert(labels.Count() >= 2);

			this._firstLevelLabel = labels.ElementAt(0);
			this._secondLevelLabel = labels.ElementAt(1);
			this._subDomainLabels = labels.Skip(2).ToArray();
		}

	// Properties
		/// <summary>
		/// Gets the enumerable collection of all labels in the domain name, starting with the top-level.
		/// </summary>
		public IEnumerable<String> AllLabels {
            get { return new String[] { this._firstLevelLabel, this._secondLevelLabel }.Concat(this._subDomainLabels); }
		}
		/// <summary>
		/// Gets the first-level domain label.  For example, in the domain name "www.example.com", the first-level label is "com".
		/// </summary>
		public String FirstLevelLabel {
			get { return this._firstLevelLabel; }
		}
		/// <summary>
		/// Gets the second level domain label.  For example, in the domain name "www.example.com", the second-level label is "example".
		/// </summary>
		public String SecondLevelLabel {
			get { return this._secondLevelLabel; }
		}
		/// <summary>
		/// Gets the sub-domain labels.  For example, in the domain name "www.example.com", an array with the value "www" would be returned.  Also, in the domain name "somewhere.else.example.com", an ordered array with the values "else" and "somewhere" would be returned.
		/// </summary>
		public IEnumerable<String> SubDomainLabels {
			get { return (String[])this._subDomainLabels.Clone(); }
		}

	// Methods
		/// <summary>
		/// Returns a value indicating if the two objects specified are equal.
		/// </summary>
		/// <param name="objA">The first object.</param>
		/// <param name="objB">The second object.</param>
		/// <returns><c>true</c> if the two objects are equal; otherwise, <c>false</c>.</returns>
		private static Boolean AreEqual(DomainName objA, DomainName objB) {
			if (Object.ReferenceEquals(objA, objB)) {
				return true;
			}
			else if (Object.ReferenceEquals(objA, null)) {
				return Object.ReferenceEquals(objB, null);
			}
			else if (Object.ReferenceEquals(objB, null)) {
				return false;
			}

			if (!String.Equals(objA._firstLevelLabel, objB._firstLevelLabel, StringComparison.OrdinalIgnoreCase)) {
				return false;
			}
			if (!String.Equals(objA._secondLevelLabel, objB._secondLevelLabel, StringComparison.OrdinalIgnoreCase)) {
				return false;
			}
			if (objA._subDomainLabels.Length != objB._subDomainLabels.Length) {
				return false;
			}
			for (Int32 i = 0; i < objA._subDomainLabels.Length; i++) {
				if (!String.Equals(objA._subDomainLabels[i], objB._subDomainLabels[i], StringComparison.OrdinalIgnoreCase)) {
					return false;
				}
			}
			return true;
		}
		/// <summary>
		/// Returns a value indicating if the two objects specified are equal.
		/// </summary>
		/// <param name="objA">The first object.</param>
		/// <param name="objB">The second object.</param>
		/// <returns><c>true</c> if the two objects are equal; otherwise, <c>false</c>.</returns>
		private static Boolean AreEqual(DomainName objA, String objB) {
			if (Object.ReferenceEquals(objA, null)) {
				return Object.ReferenceEquals(objB, null);
			}
			else if (Object.ReferenceEquals(objB, null)) {
				return false;
			}

			return String.Equals(objA.ToString(), objB, StringComparison.OrdinalIgnoreCase);
		}
		/// <summary>
		/// Returns a value indicating if the object specified is equal to this instance.
		/// </summary>
		/// <param name="obj">The object to compare to this instance.</param>
		/// <returns><c>true</c> if the object specified is equal to this instance; otherwise, <c>false</c>.</returns>
		public override Boolean Equals(Object obj) {
			return this.Equals(obj as DomainName) || this.Equals(obj as String);
		}
		/// <summary>
		/// Returns a value indicating if the object specified is equal to this instance.
		/// </summary>
		/// <param name="other">The object to compare to this instance.</param>
		/// <returns><c>true</c> if the object specified is equal to this instance; otherwise, <c>false</c>.</returns>
		public Boolean Equals(DomainName other) {
			return DomainName.AreEqual(this, other);
		}
		/// <summary>
		/// Returns a value indicating if the object specified is equal to this instance.
		/// </summary>
		/// <param name="other">The object to compare to this instance.</param>
		/// <returns><c>true</c> if the object specified is equal to this instance; otherwise, <c>false</c>.</returns>
		public Boolean Equals(String other) {
			return DomainName.AreEqual(this, other);
		}
		/// <summary>
		/// Returns a value that can serve as a hash code for this instance.
		/// </summary>
		/// <returns>The value that can serve as a hash code for this instance.</returns>
		public override Int32 GetHashCode() {
			return this.ToString().ToUpperInvariant().GetHashCode();
		}
		/// <summary>
		/// Returns the array of valid characters for a domain label.
		/// </summary>
		/// <returns>The array of valid characters for a domain label.</returns>
		public static Char[] GetValidDomainLabelCharacters() {
			//Uppercase and lowercase English letters (a-z, A-Z) 
			//Digits 0 to 9 
			//Characters ! # $ % & ' * + / = ? ^ _ ` { | } ~ ARE NOT ALLOWED!
			//Character . (dot, period, full stop) is allowed in a domain name, but not in a domain label as this is the label separator.
			Char[] chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-".ToCharArray();
			return chars;
		}
		/// <summary>
		/// Parses the string specified and returns an instance of the <see cref="T:DomainName"/> class.
		/// </summary>
		/// <param name="value">The value to parse.</param>
		/// <returns>A reference to the <see cref="T:DomainName"/> created from <paramref name="value"/>.</returns>
		/// <exception cref="System.ArgumentNullException"><paramref name="value"/> is a null reference.</exception>
		/// <exception cref="System.FormatException"><paramref name="value"/> does not represent a valid <see cref="T:DomainName"/> object.</exception>
		public static DomainName Parse(String value) {
			return DomainName.Parse(value, true);
		}
		/// <summary>
		/// Parses the string specified and returns an instance of the <see cref="T:DomainName"/> class.
		/// </summary>
		/// <param name="value">The value to parse.</param>
		/// <param name="throwOnError"><c>true</c> to throw an exception if <paramref name="value"/> is invalid; otherwise, <c>false</c>.</param>
		/// <returns>A reference to the <see cref="T:DomainName"/> created from <paramref name="value"/>.
		/// -or- A null reference if <paramref name="value"/> is invalid and <paramref name="throwOnError"/> is <c>false</c>.</returns>
		/// <exception cref="System.ArgumentNullException"><paramref name="value"/> is a null reference and <paramref name="throwOnError"/> is <c>true</c>.</exception>
		/// <exception cref="System.FormatException"><paramref name="value"/> does not represent a valid <see cref="T:DomainName"/> object and <paramref name="throwOnError"/> is <c>true</c>.</exception>
		private static DomainName Parse(String value, Boolean throwOnError) {
			if (value == null) {
				if (throwOnError) {
					throw new ArgumentNullException("value");
				}
				return null;
			}

			// Check the length <= 253.
			if (value.Length > 253) {
				if (throwOnError) {
					throw new FormatException(Res.DomainNameLengthTooLargeMessage);
				}
				return null;
			}

			// Check for invalid characters in each label.
			Char[] validDomainLabelCharacters = DomainName.GetValidDomainLabelCharacters();
			for (Int32 i = 0; i < value.Length; i++) {
				Char character = value[i];
				if (character == '.') {
					continue;
				}
				if (!validDomainLabelCharacters.Contains(character)) {
					if (throwOnError) {
						throw new FormatException(Res.InvalidCharacterAtSpecifiedPositionFormat.FormatInvariant(character, i));
					}
					return null;
				}
			}

			// Check the label count between 2 and 127.
			String[] labels = value.Split('.');
			if (labels.Length < 2) {
				if (throwOnError) {
					throw new FormatException(Res.DomainNameHasTooFewLabelsMessage);
				}
				return null;
			}
			else if (labels.Length > 127) {
				if (throwOnError) {
					throw new FormatException(Res.DomainNameHasTooManyLabelsMessage);
				}
				return null;
			}

			// Check for empty labels.
			if (labels.Any(label => label.Length == 0)) {
				if (throwOnError) {
					throw new FormatException(Res.DomainLabelIsEmptyMessage);
				}
				return null;
			}

			// We are good!  Return a DomainName object.  We must reverse the order because we call the constructor with top level domain first.
			return new DomainName(labels.Reverse());
		}
		/// <summary>
		/// Returns the string representation of this instance.
		/// </summary>
		/// <returns>The string representation of this instance.</returns>
		public override String ToString() {
			IEnumerable<String> labels = new String[] { this._firstLevelLabel, this._secondLevelLabel }
				.Concat(this._subDomainLabels)
				.Reverse();
			return labels.ToArray().Join('.');
		}
		/// <summary>
		/// Attempts to parse the string specified and returns a value indicating if the operation was successful.
		/// </summary>
		/// <param name="value">The value to parse.</param>
		/// <param name="result">When the method returns, this will contain the <see cref="T:DomainName"/> created, or a null reference if the method returns <c>false</c>.</param>
		/// <returns><c>true</c> if <paramref name="value"/> was parsed successfully; otherwise, <c>false</c>.</returns>
		public static Boolean TryParse(String value, out DomainName result) {
			result = DomainName.Parse(value, false);
			return result != null;
		}

	// Operators
		/// <summary>
		/// Returns a value indicating if the two objects specified are equal.
		/// </summary>
		/// <param name="objA">The first object.</param>
		/// <param name="objB">The second object.</param>
		/// <returns><c>true</c> if the two objects are equal; otherwise, <c>false</c>.</returns>
		public static Boolean operator ==(DomainName objA, DomainName objB) {
			return DomainName.AreEqual(objA, objB);
		}
		/// <summary>
		/// Returns a value indicating if the two objects specified are equal.
		/// </summary>
		/// <param name="objA">The first object.</param>
		/// <param name="objB">The second object.</param>
		/// <returns><c>true</c> if the two objects are equal; otherwise, <c>false</c>.</returns>
		public static Boolean operator ==(DomainName objA, String objB) {
			return DomainName.AreEqual(objA, objB);
		}
		/// <summary>
		/// Returns a value indicating if the two objects specified are equal.
		/// </summary>
		/// <param name="objA">The first object.</param>
		/// <param name="objB">The second object.</param>
		/// <returns><c>true</c> if the two objects are equal; otherwise, <c>false</c>.</returns>
		public static Boolean operator ==(String objA, DomainName objB) {
			return DomainName.AreEqual(objB, objA);
		}
		/// <summary>
		/// Returns a value indicating if the two objects specified are not equal.
		/// </summary>
		/// <param name="objA">The first object.</param>
		/// <param name="objB">The second object.</param>
		/// <returns><c>true</c> if the two objects not are equal; otherwise, <c>false</c>.</returns>
		public static Boolean operator !=(DomainName objA, DomainName objB) {
			return !DomainName.AreEqual(objA, objB);
		}
		/// <summary>
		/// Returns a value indicating if the two objects specified are not equal.
		/// </summary>
		/// <param name="objA">The first object.</param>
		/// <param name="objB">The second object.</param>
		/// <returns><c>true</c> if the two objects not are equal; otherwise, <c>false</c>.</returns>
		public static Boolean operator !=(DomainName objA, String objB) {
			return !DomainName.AreEqual(objA, objB);
		}
		/// <summary>
		/// Returns a value indicating if the two objects specified are not equal.
		/// </summary>
		/// <param name="objA">The first object.</param>
		/// <param name="objB">The second object.</param>
		/// <returns><c>true</c> if the two objects not are equal; otherwise, <c>false</c>.</returns>
		public static Boolean operator !=(String objA, DomainName objB) {
			return !DomainName.AreEqual(objB, objA);
		}
		/// <summary>
		/// Implicitly converts the <see cref="T:DomainName"/> specified to an instance of the <see cref="T:String"/> class.
		/// </summary>
		/// <param name="instance">The object to convert.</param>
		/// <returns>The <see cref="T:String"/> object created, or a null reference if <paramref name="instance"/> is a null reference.</returns>
		public static implicit operator String(DomainName instance) {
			if (instance == null) {
				return null;
			}
			return instance.ToString();
		}
		/// <summary>
		/// Explicitly converts the <see cref="T:String"/> specified to an instance of the <see cref="T:DomainName"/> class.
		/// </summary>
		/// <param name="instance">The object to convert.</param>
		/// <returns>The <see cref="T:DomainName"/> object created, or a null reference if <paramref name="instance"/> is a null reference.</returns>
		/// <exception cref="System.InvalidCastException"><paramref name="instance"/> is not valid for the <see cref="T:DomainName"/> class.</exception>
		public static explicit operator DomainName(String instance) {
			if (instance == null) {
				return null;
			}

			DomainName value = DomainName.Parse(instance, false);
			if (value == null) {
				throw new InvalidCastException();
			}
			return value;
		}
	}
}
