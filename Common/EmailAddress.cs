using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

using ImmutableObject = System.ComponentModel.ImmutableObjectAttribute;
using Regex = System.Text.RegularExpressions.Regex;
using Res = Vizistata.Properties.Resources;

namespace Vizistata {
	/// <summary>
	/// Represents an e-mail address.  This class may not be inherited.  This class is immutable.
	/// </summary>
	/// <remarks>
	/// See the "E-mail address" entry at Wikipedia (http://en.wikipedia.org/wiki/E-mail_address) for information on business rules.
	/// </remarks>
	[Serializable()]
	[ImmutableObject(true)]
	public sealed class EmailAddress : Object, IEquatable<EmailAddress> {
	// Fields
		/// <summary>
		/// The domain part of the e-mail address.  This field is read-only.
		/// </summary>
		private readonly String _domain;
		/// <summary>
		/// The local part of the e-mail address.  This field is read-only.
		/// </summary>
		private readonly String _localPart;

	// Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:EmailAddress"/> class.
		/// </summary>
		/// <param name="localPart">The local part of the e-mail address (the part before the @ symbol).</param>
		/// <param name="domain">The host name of the e-mail address (the part after the @ symbol).</param>
		/// <exception cref="System.ArgumentNullException"><paramref name="localPart"/> is a null reference.
		/// -or- <paramref name="domain"/> is a null reference.</exception>
		/// <exception cref="System.ArgumentOutOfRangeException"><paramref name="localPart"/> has a length of 0 characters.
		/// -or- <paramref name="localPart"/> as a length greater than 64 characters.
		/// -or- <paramref name="domain"/> has a length of 0 characters.
		/// -or- <paramref name="domain"/> has a length greater than 253 characters.
		/// -or- The combined length of <paramref name="localPart"/> and <paramref name="domain"/> is greater than 254 characters.</exception>
		/// <exception cref="System.ArgumentException"><paramref name="localPart"/> contains characters not defined by <see cref="M:GetValidLocalPartCharacters"/>.
		/// -or- <paramref name="localPart"/> starts with the period character ('.').
		/// -or- <paramref name="localPart"/> ends with the period character ('.').
		/// -or- <paramref name="domain"/> contains characters not defined by <see cref="M:GetValidDomainCharacters"/>.
		/// -or- <paramref name="domain"/> starts with the period character ('.').
		/// -or- <paramref name="domain"/> ends with the period character ('.').</exception>
		public EmailAddress(String localPart, String domain)
			: base() {
			if (localPart == null) {
				throw new ArgumentNullException("localPart");
			}
			if (domain == null) {
				throw new ArgumentNullException("domain");
			}

			// Check the lengths.
			Int32 localPartLength = localPart.Length;
			Int32 domainLength = domain.Length;
			if (localPartLength == 0 || localPartLength > 64) {
				throw new ArgumentOutOfRangeException("localPart", Res.EmailAddressLocalPartLengthInvalidMessage);
			}
			if (domainLength == 0 || domainLength > 253) {
				throw new ArgumentOutOfRangeException("domain", Res.EmailAddressDomainLengthInvalidMessage);
			}
			if (localPartLength + domainLength > 254) {
				throw new ArgumentOutOfRangeException("localPart", Res.EmailAddressLocalPartAndDomainTooLongMessage);
			}

			// Check for invalid characters.
			String validLocalPartCharacters = new String(EmailAddress.GetValidLocalPartCharacters());
			foreach (Char character in localPart) {
				if (validLocalPartCharacters.IndexOf(character) == -1) {
					throw new ArgumentException(Res.EmailAddressLocalPartInvalidCharactersMessage, "localPart");
				}
			}
			if (localPart[0] == '.') {
				throw new ArgumentException(Res.EmailAddressLocalPartStartsWithDotMessage, "localPart");
			}
			else if (localPart[localPart.Length - 1] == '.') {
				throw new ArgumentException(Res.EmailAddressLocalPartEndsWithDotMessage, "localPart");
			}
			else if (localPart.Contains("..")) {
				throw new ArgumentException(Res.EmailAddressLocalPartContainsConsecutiveDotsMessage, "localPart");
			}
			String validDomainCharacters = new String(EmailAddress.GetValidDomainCharacters());
			foreach (Char character in domain) {
				if (validDomainCharacters.IndexOf(character) == -1) {
					throw new ArgumentException(Res.EmailAddressDomainPartInvalidCharactersMessage, "domain");
				}
			}
			if (domain[0] == '.') {
				throw new ArgumentException(Res.EmailAddressDomainStartsWithDotMessage, "domain");
			}
			if (domain[0] == '-') {
				throw new ArgumentException("A host name label may not start or end with a hyphen (-).", "domain");
			}
			else if (domain[domain.Length - 1] == '.') {
				throw new ArgumentException(Res.EmailAddressDomainEndsWithDotMessage, "domain");
			}
			else if (domain[domain.Length - 1] == '-') {
				throw new ArgumentException("A host name label may not start or end with a hyphen (-).", "domain");
			}
			else if (domain.Contains("..")) {
				throw new ArgumentException(Res.EmailAddressDomainContainsConsecutiveDotsMessage, "domain");
			}
			else if (domain.Contains(".-") || domain.Contains("-.")) {
				throw new ArgumentException("A host name label may not start or end with a hyphen (-).", "domain");
			}

			// We're good!  Save the values.
			this._localPart = localPart;
			this._domain = domain;
		}

	// Properties
		/// <summary>
		/// Gets the domain part of the e-mail address (the part after the @ symbol).
		/// </summary>
		public String Domain {
			get { return this._domain; }
		}
		/// <summary>
		/// Gets the local part of the e-mail address (the part before the @ symbol).
		/// </summary>
		public String LocalPart {
			get { return this._localPart; }
		}

	// Methods
		/// <summary>
		/// Gets a value indicating if the two objects are equal.
		/// </summary>
		/// <param name="objA">The first <see cref="T:EmailAddress"/> to compare.</param>
		/// <param name="objB">The second <see cref="T:EmailAddress"/> to compare.</param>
		/// <returns><c>true</c> if <paramref name="objA"/> equals <paramref name="objB"/>; otherwise, <c>false</c>.</returns>
		private static Boolean AreEqual(EmailAddress objA, EmailAddress objB) {
			if (Object.ReferenceEquals(objA, objB)) {
				return true;
			}
			if (Object.ReferenceEquals(objA, null) || Object.ReferenceEquals(objB, null)) {
				return false;
			}
			return ((IEquatable<EmailAddress>)objA).Equals(objB);
		}
		/// <summary>
		/// Determines whether the specified <see cref="T:Object"/> is equal to the current <see cref="T:EmailAddress"/>.
		/// </summary>
		/// <param name="obj">The <see cref="T:Object"/> to compare with the current <see cref="T:EmailAddress"/>.</param>
		/// <returns><c>true</c> if the specified <see cref="T:Object"/> is equal to the current <see cref="T:EmailAddress"/>; otherwise, <c>false</c>.</returns>
		public override Boolean Equals(Object obj) {
			EmailAddress other = obj as EmailAddress;
			return this.Equals(other);
		}
		/// <summary>
		/// Indicates whether the current object is equal to another object of the same type.
		/// </summary>
		/// <param name="other">An object to compare with this object.</param>
		/// <returns><c>true</c> if the current object is equal to the other parameter; otherwise, <c>false</c>.</returns>
		public Boolean Equals(EmailAddress other) {
			if (other != null) {
				return String.Equals(this._localPart, other._localPart, StringComparison.OrdinalIgnoreCase)
					&& String.Equals(this._domain, other._domain, StringComparison.OrdinalIgnoreCase);
			}
			return false;
		}
		/// <summary>
		/// Serves as a hash function for a particular type. <see cref="M:Object.GetHashCode"/> is suitable for use in hashing algorithms and data structures like a hash table.
		/// </summary>
		/// <returns>A hash code for the current <see cref="T:EmailAddress"/>.</returns>
		public override Int32 GetHashCode() {
			Debug.Assert(this._localPart != null);
			Debug.Assert(this._domain != null);
			return this._localPart.ToUpperInvariant().GetHashCode() ^ this._domain.ToUpperInvariant().GetHashCode();
		}
		/// <summary>
		/// Returns the array of valid characters for a domain name.
		/// </summary>
		/// <returns>The array of characters that are valid for a domain name.</returns>
		public static Char[] GetValidDomainCharacters() {
			//The domain part of the e-mail address may use any of these ASCII characters:
			//Uppercase and lowercase English letters (a-z, A-Z) 
			//Digits 0 to 9 
			//Characters ! # $ % & ' * + / = ? ^ _ ` { | } ~ ARE NOT ALLOWED!
			//Character . (dot, period, full stop) provided that it is not the first or last character, and provided also that it does not appear two or more times consecutively.
			Char[] chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-.".ToCharArray();
			return chars;
		}
		/// <summary>
		/// Returns the array of valid characters for the local part of an e-mail address.
		/// </summary>
		/// <returns>The array of characters that are valid for the local part of an e-mail address.</returns>
		public static Char[] GetValidLocalPartCharacters() {
			//The local-part of the e-mail address may use any of these ASCII characters:
			//Uppercase and lowercase English letters (a-z, A-Z) 
			//Digits 0 to 9 
			//Characters ! # $ % & ' * + - / = ? ^ _ ` { | } ~ 
			//Character . (dot, period, full stop) provided that it is not the first or last character, and provided also that it does not appear two or more times consecutively.
			Char[] chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!#$%&'*+-/=?^_`{|}~.".ToCharArray();
			return chars;

		}
		/// <summary>
		/// Converts the String representation of an e-mail address to its <see cref="T:EmailAddress"/> equivalent.
		/// </summary>
		/// <param name="s">The <see cref="T:String"/> representation of an e-mail address to convert.</param>
		/// <returns>The <see cref="T:EmailAddress"/> equivalent to the value contained in <paramref name="s"/>.</returns>
		/// <exception cref="System.ArgumentNullException"><paramref name="s"/> is a null reference.</exception>
		/// <exception cref="System.FormatException"><paramref name="s"/> is not in the correct format.</exception>
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "s", Justification = "Using the argument name 's' follows the naming conventions for Parse() and TryParse() methods.")]
		public static EmailAddress Parse(String s) {
			return EmailAddress.Parse(s, true);
		}
		/// <summary>
		/// Converts the String representation of an e-mail address to its <see cref="T:EmailAddress"/> equivalent.
		/// </summary>
		/// <param name="s">The <see cref="T:String"/> representation of an e-mail address to convert.</param>
		/// <param name="throwOnError"><c>true</c> to throw an exception on error; otherwise, <c>false</c>.</param>
		/// <returns>The <see cref="T:EmailAddress"/> equivalent to the value contained in <paramref name="s"/>.</returns>
		/// <exception cref="System.ArgumentNullException"><paramref name="s"/> is a null reference and <paramref name="throwOnError"/> is <c>true</c>.</exception>
		/// <exception cref="System.FormatException"><paramref name="s"/> is not in the correct format and <paramref name="throwOnError"/> is <c>true</c>.</exception>
		private static EmailAddress Parse(String s, Boolean throwOnError) {
			// Check for null.
			if (s == null) {
				if (throwOnError) {
					throw new ArgumentNullException("s");
				}
				return null;
			}

			// Split the e-mail address into its parts.
			String[] emailAddressParts = s.Split('@');
			if (emailAddressParts.Length != 2) {
				if (throwOnError) {
					throw new FormatException(Res.EmailAddressHasTooManyPartsMessage);
				}
				return null;
			}
			String localPart = emailAddressParts[0];
			String domain = emailAddressParts[1];

			try {
				return new EmailAddress(localPart, domain);
			}
			catch (Exception ex) {
				if (throwOnError) {
					throw new FormatException(ex.Message);
				}
				return null;
			}
		}
		/// <summary>
		/// Returns a <see cref="T:String"/> that represents the current <see cref="T:EmailAddress"/>.
		/// </summary>
		/// <returns>A <see cref="T:String"/> that represents the current <see cref="T:EmailAddress"/>.</returns>
		public override String ToString() {
			return this._localPart + "@" + this._domain;
		}
		/// <summary>
		/// Converts the String representation of an e-mail address to its <see cref="T:EmailAddress"/> equivalent.  A return value indicates whether the conversion succeeded or failed.
		/// </summary>
		/// <param name="s">The String representation of an e-mail address to convert.</param>
		/// <param name="result">When this method returns, contains the <see cref="T:EmailAddress"/> that is equivalent to the value contained in <paramref name="s"/>, if the conversion succeeded, or a null reference if the conversion failed.  The conversion fails if the <paramref name="s"/> parameter is a null reference or is not an e-mail address in a valid format.  This parameter is passed uninitialized.</param>
		/// <returns><c>true</c> if <paramref name="s"/> was converted successfully; otherwise, <c>false</c>.</returns>
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "s", Justification = "Using the argument name 's' follows the naming conventions for Parse() and TryParse() methods.")]
		public static Boolean TryParse(String s, out EmailAddress result) {
			result = EmailAddress.Parse(s, false);
			if (result == null) {
				return false;
			}
			return true;
		}

	// Operators
		/// <summary>
		/// Determines whether two instances of the <see cref="T:EmailAddress"/> class are equal.
		/// </summary>
		/// <param name="objA">The first <see cref="T:EmailAddress"/> to compare.</param>
		/// <param name="objB">The second <see cref="T:EmailAddress"/> to compare.</param>
		/// <returns><c>true</c> if <paramref name="objA"/> equals <paramref name="objB"/>; otherwise, <c>false</c>.</returns>
		public static Boolean operator ==(EmailAddress objA, EmailAddress objB) {
			return EmailAddress.AreEqual(objA, objB);
		}
		/// <summary>
		/// Determines whether two instances of the <see cref="T:EmailAddress"/> class are not equal.
		/// </summary>
		/// <param name="objA">The first <see cref="T:EmailAddress"/> to compare.</param>
		/// <param name="objB">The second <see cref="T:EmailAddress"/> to compare.</param>
		/// <returns><c>true</c> if <paramref name="objA"/> does not equal <paramref name="objB"/>; otherwise, <c>false</c>.</returns>
		public static Boolean operator !=(EmailAddress objA, EmailAddress objB) {
			return !EmailAddress.AreEqual(objA, objB);
		}
		/// <summary>
		/// Implicitly converts a <see cref="T:EmailAddress"/> value to a <see cref="T:String"/> value.
		/// </summary>
		/// <param name="value">The object to convert.</param>
		/// <returns>The <see cref="T:String"/> representation of <paramref name="value"/>.</returns>
		public static implicit operator String(EmailAddress value) {
			if (value != null) {
				return value.ToString();
			}
			return null;
		}
		/// <summary>
		/// Explicitly converts a <see cref="T:String"/> value to a <see cref="T:EmailAddress"/> value.
		/// </summary>
		/// <param name="value">The object to convert.</param>
		/// <returns>The <see cref="T:EmailAddress"/> representation of <paramref name="value"/>.</returns>
		/// <exception cref="InvalidCastException"><paramref name="value"/> cannot be converted to an <see cref="T:EmailAddress"/> object.</exception>
		public static explicit operator EmailAddress(String value) {
			if (value != null) {
				EmailAddress result = EmailAddress.Parse(value, false);
				if (result == null) {
					throw new InvalidCastException();
				}
				return result;
			}
			return null;
		}
	}
}
