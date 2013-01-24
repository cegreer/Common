using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;

using ImmutableObject = System.ComponentModel.ImmutableObjectAttribute;
using Regex = System.Text.RegularExpressions.Regex;
using Res = Vizistata.Properties.Resources;

namespace Vizistata {
	/// <summary>
	/// Represents a phone number in the United States.  This class may not be inherited.  This class is immutable.
	/// </summary>
	/// <remarks>
	/// See the "E.123" entry at Wikipedia (http://en.wikipedia.org/wiki/E.123) for information on business rules.
	/// </remarks>
	[Serializable()]
	[ImmutableObject(true)]
	public sealed class PhoneNumber : Object, IEquatable<PhoneNumber> {
	// Fields
		/// <summary>
		/// The country code for this phone number.  This field is read-only.
		/// </summary>
		private readonly String _countryCode;
		/// <summary>
		/// The area code for this phone number.  This field is read-only.
		/// </summary>
		private readonly String _areaCode;
		/// <summary>
		/// The subscriber number groups for this phone number.  This field is read-only.
		/// </summary>
		private readonly String[] _subscriberNumberGroups;

	// Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:PhoneNumber"/> class.
		/// </summary>
		/// <param name="countryCode">A string containing only numbers representing the country code.  For example, 1 in the United States or 41 in Switzerland.</param>
		/// <param name="areaCode">A string containing numbers and/or letters representing the area code.</param>
		/// <param name="subscriberNumberGroups">The array of strings containing numbers and/or letters which represent the groupings of digits for the subscriber number.</param>
		/// <exception cref="System.ArgumentNullException"><paramref name="countryCode"/> is a null reference.
		/// -or- <paramref name="areaCode"/> is a null reference.
		/// -or- <paramref name="subscriberNumberGroups"/> is a null reference.</exception>
		/// <exception cref="System.ArgumentException"><paramref name="countryCode"/> is empty.
		/// -or- <paramref name="countryCode"/> is not a valid country code.
		/// -or- <paramref name="countryCode"/> contains non-numeric characters.
		/// -or- <paramref name="areaCode"/> is empty or contains non-alphanumeric characters.
		/// -or- <paramref name="subscriberNumberGroups"/> is empty.
		/// -or- <paramref name="subscriberNumberGroups"/> contains en element that is a null reference.
		/// -or- <paramref name="subscriberNumberGroups"/> contains en element which has non-alphanumeric characters.</exception>
		/// <exception cref="System.NotSupportedException">The country code is valid but is not supported.  Currently, only US phone numbers are supported.</exception>
		public PhoneNumber(String countryCode, String areaCode, params String[] subscriberNumberGroups)
			: base() {
			if (countryCode == null) {
				throw new ArgumentNullException("countryCode");
			}
			if (areaCode == null) {
				throw new ArgumentNullException("areaCode");
			}
			if (subscriberNumberGroups == null) {
				throw new ArgumentNullException("subscriberNumberGroups");
			}

			// Check the country code.
			Int32[] supportedCountryCodes = new Int32[] { 1 };
			Int32 countryCodeValue;
			if (!Int32.TryParse(countryCode, NumberStyles.None, CultureInfo.InvariantCulture, out countryCodeValue)) {
				throw new ArgumentException(Res.PhoneNumberCountryCodeInvalidMessage, "countryCode");
			}
			else if (!supportedCountryCodes.Contains(countryCodeValue)) {
				throw new NotSupportedException(Res.PhoneNumberSupportsUSOnly);
			}

			// Check the area code.
			if (areaCode.Length != 3) {
				throw new ArgumentException(Res.PhoneNumberAreaCodeLengthInvalidMessage, "areaCode");
			}
			else if (!Regex.IsMatch(areaCode, "^[A-Za-z0-9]{3}$")) {
				throw new ArgumentException(Res.PhoneNumberContainsInvalidCharactersMessage, "areaCode");
			}

			// Check the subscriber groups.
			if (subscriberNumberGroups.Any(group => group == null)) {
				throw new ArgumentException(Res.PhoneNumberSubscriberGroupsContainsNullMessage, "subscriberNumberGroups");
			}
			if (subscriberNumberGroups.Length == 0) {
				throw new ArgumentException(Res.PhoneNumberSubscriberGroupsInvalidLengthMessage, "subscriberNumberGroups");
			}
			if (subscriberNumberGroups.Any(group => !Regex.IsMatch(group, "^[A-Za-z0-9]+$"))) {
				throw new ArgumentException(Res.PhoneNumberContainsInvalidCharactersMessage, "subscriberNumberGroups");
			}
			Int32 digitCount = subscriberNumberGroups.Sum(group => group.Length);
			if (digitCount < 7 || digitCount > 15) {
				throw new ArgumentException(Res.PhoneNumberSubscriberGroupsDigitsLengthInvalidMessage);
			}

			// We're good!  Save the values.
			this._countryCode = countryCode;
			this._areaCode = areaCode;
			this._subscriberNumberGroups = subscriberNumberGroups;
		}

	// Properties
		/// <summary>
		/// Gets the area code for the phone number.
		/// </summary>
		public String AreaCode {
			get { return this._areaCode; }
		}
		/// <summary>
		/// Gets the country code part of the phone number.
		/// </summary>
		public String CountryCode {
			get { return this._countryCode; }
		}
		/// <summary>
		/// Gets the subcriber or local part of the phone number divided into any logical groups.
		/// </summary>
		public IEnumerable<String> SubscriberNumberGroups {
			get { return (String[])this._subscriberNumberGroups.Clone(); }
		}

	// Methods
		/// <summary>
		/// Returns a value indicating if the two objects are equal.
		/// </summary>
		/// <param name="objA">The first <see cref="T:PhoneNumber"/> to compare.</param>
		/// <param name="objB">The second <see cref="T:PhoneNumber"/> to compare.</param>
		/// <returns><c>true</c> if <paramref name="objA"/> equals <paramref name="objB"/>; otherwise, <c>false</c>.</returns>
		private static Boolean AreEqual(PhoneNumber objA, PhoneNumber objB) {
			if (Object.ReferenceEquals(objA, objB)) {
				return true;
			}
			else if (Object.ReferenceEquals(objA, null) || Object.ReferenceEquals(objB, null)) {
				return false;
			}

			if (!String.Equals(objA._countryCode, objB._countryCode, StringComparison.OrdinalIgnoreCase)) {
				return false;
			}
			else if (!String.Equals(objA._areaCode, objB._areaCode, StringComparison.OrdinalIgnoreCase)) {
				return false;
			}

			for (Int32 i = 0; i < objA._subscriberNumberGroups.Length; i++) {
				if (!String.Equals(objA._subscriberNumberGroups[i], objB._subscriberNumberGroups[i], StringComparison.OrdinalIgnoreCase)) {
					return false;
				}
			}
			return true;
		}
		/// <summary>
		/// Determines whether the specified <see cref="T:Object"/> is equal to the current <see cref="T:PhoneNumber"/>.
		/// </summary>
		/// <param name="obj">The <see cref="T:Object"/> to compare with the current <see cref="T:PhoneNumber"/>.</param>
		/// <returns><c>true</c> if the specified <see cref="T:Object"/> is equal to the current <see cref="T:PhoneNumber"/>; otherwise, <c>false</c>.</returns>
		public override Boolean Equals(Object obj) {
			PhoneNumber other = obj as PhoneNumber;
			return this.Equals(other);
		}
		/// <summary>
		/// Indicates whether the current object is equal to another object of the same type.
		/// </summary>
		/// <param name="other">An object to compare with this object.</param>
		/// <returns><c>true</c> if the current object is equal to the other parameter; otherwise, <c>false</c>.</returns>
		public Boolean Equals(PhoneNumber other) {
			return PhoneNumber.AreEqual(this, other);
		}
		/// <summary>
		/// Serves as a hash function for a particular type. <see cref="M:Object.GetHashCode"/> is suitable for use in hashing algorithms and data structures like a hash table.
		/// </summary>
		/// <returns>A hash code for the current <see cref="T:PhoneNumber"/>.</returns>
		public override Int32 GetHashCode() {
			return this.ToString().ToUpperInvariant().GetHashCode();
		}
		/// <summary>
		/// Converts the String representation of a phone number to its <see cref="T:PhoneNumber"/> equivalent.
		/// </summary>
		/// <param name="value">The String representation of the phone number to convert.</param>
		/// <returns>The <see cref="T:PhoneNumber"/> equivalent to the phone number contained in <paramref name="value"/>.</returns>
		/// <exception cref="System.ArgumentNullException"><paramref name="value"/> is a null reference.</exception>
		/// <exception cref="System.FormatException"><paramref name="value"/> is not in the correct format.</exception>
		public static PhoneNumber Parse(String value) {
			return PhoneNumber.Parse(value, true);
		}
		/// <summary>
		/// Converts the String representation of a phone number to its <see cref="T:PhoneNumber"/> equivalent.
		/// </summary>
		/// <param name="value">The String representation of the phone number to convert.</param>
		/// <param name="throwOnError"><c>true</c> to throw an exception on error; otherwise, <c>false</c>.</param>
		/// <returns>The <see cref="T:PhoneNumber"/> equivalent to the phone number contained in <paramref name="value"/>.</returns>
		/// <exception cref="System.ArgumentNullException"><paramref name="value"/> is a null reference and <paramref name="throwOnError"/> is <c>true</c>.</exception>
		/// <exception cref="System.FormatException"><paramref name="value"/> is not in the correct format and <paramref name="throwOnError"/> is <c>true</c>.</exception>
		private static PhoneNumber Parse(String value, Boolean throwOnError) {
			if (value == null) {
				if (throwOnError) {
					throw new ArgumentNullException("value");
				}
				return null;
			}

			String countryCode = null;
			String areaCode = null;
			String[] subscriberNumberGroups = null;

			String[] parts = value.Split(" \t\r\n.,:;<>()|{}\\/+-*^".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
			if (parts.Length == 0) {
				if (throwOnError) {
					throw new FormatException(Res.PhoneNumberContainsInvalidCharactersMessage);
				}
				return null;
			}

			if (parts.Length == 1) {
				// We assume US for the country code.  We also assume the first three characters is the area code.
				if (parts[0].Length < 10) {
					if (throwOnError) {
						throw new FormatException(Res.PhoneNumberUnitedStatesNumberHasInvalidLengthMessage);
					}
					return null;
				}
				countryCode = "1";
				areaCode = parts[0].Substring(0, 3);
				subscriberNumberGroups = new String[] { parts[0].Substring(3) };
			}
			else if (parts.Length == 2) {
				// We assume US for the country code.  Assume the subscriber number groups are all run together (like 1234567) instead of having separators.
				countryCode = "1";
				areaCode = parts[0];
				subscriberNumberGroups = parts.Skip(1).ToArray();
			}
			else {
				// Assume the country code is in the first group.
				countryCode = parts[0];
				areaCode = parts[1];
				subscriberNumberGroups = parts.Skip(2).ToArray();
			}

			try {
				return new PhoneNumber(countryCode, areaCode, subscriberNumberGroups);
			}
			catch (Exception ex) {
				if (!ex.CanBeHandledSafely()) {
					throw;
				}

				if (parts.Length > 2) {
					// Let's assume US for the country code, and try again.
					countryCode = "1";
					areaCode = parts[0];
					subscriberNumberGroups = parts.Skip(1).ToArray();
					try {
						return new PhoneNumber(countryCode, areaCode, subscriberNumberGroups);
					}
					catch (Exception ex2) {
						if (!ex2.CanBeHandledSafely()) {
							throw;
						}
						/* This obviously failed.  Throw the previous error. */
					}
				}
				if (throwOnError) {
					throw new FormatException(ex.Message);
				}
				return null;
			}
		}
		/// <summary>
		/// Returns a <see cref="T:String"/> that represents the current <see cref="T:PhoneNumber"/>.
		/// </summary>
		/// <returns>A <see cref="T:String"/> that represents the current <see cref="T:PhoneNumber"/>.</returns>
		public override String ToString() {
			Debug.Assert(this._subscriberNumberGroups != null);
			return this._countryCode + " " + this._areaCode + " " + String.Join(" ", this._subscriberNumberGroups);
		}
		/// <summary>
		/// Converts the String representation of a phone number to its <see cref="T:PhoneNumber"/> equivalent.  A return value indicates whether the conversion succeeded or failed.
		/// </summary>
		/// <param name="value">The String representation of the phone number to convert.</param>
		/// <param name="result">When this method returns, contains the <see cref="T:PhoneNumber"/> that is equivalent to the phone numeric value contained in <paramref name="value"/>, if the conversion succeeded, or a null reference if the conversion failed.  The conversion fails if the <paramref name="value"/> parameter is a null reference or is not a phone number in a valid format.  This parameter is passed uninitialized.</param>
		/// <returns><c>true</c> if <paramref name="value"/> was converted successfully; otherwise, <c>false</c>.</returns>
		public static Boolean TryParse(String value, out PhoneNumber result) {
			result = PhoneNumber.Parse(value, false);
			if (result == null) {
				return false;
			}
			return true;
		}

	// Operators
		/// <summary>
		/// Determines whether two instances of the <see cref="T:PhoneNumber"/> class are equal.
		/// </summary>
		/// <param name="objA">The first <see cref="T:PhoneNumber"/> to compare.</param>
		/// <param name="objB">The second <see cref="T:PhoneNumber"/> to compare.</param>
		/// <returns><c>true</c> if <paramref name="objA"/> equals <paramref name="objB"/>; otherwise, <c>false</c>.</returns>
		public static Boolean operator ==(PhoneNumber objA, PhoneNumber objB) {
			return PhoneNumber.AreEqual(objA, objB);
		}
		/// <summary>
		/// Determines whether two instances of the <see cref="T:PhoneNumber"/> class are not equal.
		/// </summary>
		/// <param name="objA">The first <see cref="T:PhoneNumber"/> to compare.</param>
		/// <param name="objB">The second <see cref="T:PhoneNumber"/> to compare.</param>
		/// <returns><c>true</c> if <paramref name="objA"/> does not equal <paramref name="objB"/>; otherwise, <c>false</c>.</returns>
		public static Boolean operator !=(PhoneNumber objA, PhoneNumber objB) {
			return !PhoneNumber.AreEqual(objA, objB);
		}
		/// <summary>
		/// Implicitly converts a <see cref="T:PhoneNumber"/> value to a <see cref="T:String"/> value.
		/// </summary>
		/// <param name="instance">The object to convert.</param>
		/// <returns>The <see cref="T:String"/> representation of <paramref name="instance"/>.</returns>
		public static implicit operator String(PhoneNumber instance) {
			if (instance != null) {
				return instance.ToString();
			}
			return null;
		}
		/// <summary>
		/// Explicitly converts a <see cref="T:String"/> value to a <see cref="T:PhoneNumber"/> value.
		/// </summary>
		/// <param name="instance">The object to convert.</param>
		/// <returns>The <see cref="T:PhoneNumber"/> representation of <paramref name="instance"/>.</returns>
		/// <exception cref="System.InvalidCastException"><paramref name="instance"/> cannot be converted to a <see cref="T:PhoneNumber"/> object.</exception>
		public static explicit operator PhoneNumber(String instance) {
			if (instance != null) {
				PhoneNumber phoneNumber = PhoneNumber.Parse(instance, false);
				if (phoneNumber == null) {
					throw new InvalidCastException();
				}
				return phoneNumber;
			}
			return null;
		}
	}
}
