using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Vizistata.TestTools;

namespace Vizistata {
	/// <summary>
	/// This is a test class for <see cref="T:EmailAddress"/> and is intended to contain all <see cref="T:EmailAddress"/> Unit Tests.
	///</summary>
	[TestClass()]
	public class EmailAddressTests {
		#region Test Class Implementation
		/// <summary>
		/// Describes the context under which the current test is running.
		/// </summary>
		private TestContext _testContextInstance;

		/// <summary>
		/// Initializes a new instance of the <see cref="T:EmailAddressTests"/> class.
		/// </summary>
		public EmailAddressTests() : base() { }

		/// <summary>
		/// Gets or sets the test context which provides information about and functionality for the current test run.
		///</summary>
		public TestContext TestContext {
			get { return this._testContextInstance; }
			set { this._testContextInstance = value; }
		}
		#region Additional test attributes
		// 
		//You can use the following additional attributes as you write your tests:
		//
		//Use ClassInitialize to run code before running the first test in the class
		//
		//[ClassInitialize()]
		//public static void MyClassInitialize(TestContext testContext)
		//{
		//}
		//
		//Use ClassCleanup to run code after all tests in a class have run
		//
		//[ClassCleanup()]
		//public static void MyClassCleanup()
		//{
		//}
		//
		//Use TestInitialize to run code before running each test
		//
		//[TestInitialize()]
		//public void MyTestInitialize()
		//{
		//}
		//
		//Use TestCleanup to run code after each test has run
		//
		//[TestCleanup()]
		//public void MyTestCleanup()
		//{
		//}
		//
		#endregion
		#endregion

	// Methods
		/// <summary>
		/// Returns the list of ASCII characters that are invalid for the domain.
		/// </summary>
		/// <returns>The list of ASCII characters that are invalid for the domain.</returns>
		private static IEnumerable<Char> GetInvalidDomainAsciiCharacters() {
			Char[] validCharacters = EmailAddress.GetValidDomainCharacters();
			foreach (Char character in Enumerable.Range(1, 254).Select(i => (Char)i)) {
				if (!validCharacters.Contains(character)) {
					yield return character;
				}
			}
			yield break;
		}
		/// <summary>
		/// Returns the list of ASCII characters that are invalid for the local part.
		/// </summary>
		/// <returns>The list of ASCII characters that are invalid for the local part.</returns>
		private static IEnumerable<Char> GetInvalidLocalPartAsciiCharacters() {
			Char[] validCharacters = EmailAddress.GetValidLocalPartCharacters();
			foreach (Char character in Enumerable.Range(1, 254).Select(i => (Char)i)) {
				if (!validCharacters.Contains(character)) {
					yield return character;
				}
			}
			yield break;
		}
		/// <summary>
		/// Returns an enumerable collection of valid e-mail address parts.
		/// </summary>
		/// <returns>The enumerable collection of valid e-mail address parts.</returns>
		private static IEnumerable<Pair<String>> GetValidEmailAddresses() {
			return new Pair<String>[] {
				new	Pair<String>("Chad.Greer", "ParivedaSolutions.com")
			};
		}

	// Constructor Tests
		[TestMethod()]
		[Description(".ctor(String, String) constructor for the optimal path.")]
		public void EmailAddress_Unit_Constructor_Optimal() {
			foreach (Pair<String> emailAddressPair in EmailAddressTests.GetValidEmailAddresses()) {
				new EmailAddress(emailAddressPair.First, emailAddressPair.Second);
			}
		}
		[TestMethod()]
		[Description(".ctor(String, String) constructor when 'localPart' is a null reference.")]
		[ExpectedException(typeof(ArgumentNullException))]
		public void EmailAddress_Unit_Constructor_LocalPartIsNull() {
			String localPart = null;
			String domain = "ParivedaSolutions.com";
			new EmailAddress(localPart, domain);
		}
		[TestMethod()]
		[Description(".ctor(String, String) constructor when 'domain' is a null reference.")]
		[ExpectedException(typeof(ArgumentNullException))]
		public void EmailAddress_Unit_Constructor_DomainIsNull() {
			String localPart = "Chad.Greer";
			String domain = null;
			new EmailAddress(localPart, domain);
		}
		[TestMethod()]
		[Description(".ctor(String, String) constructor when 'localPart' has a length of 0.")]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void EmailAddress_Unit_Constructor_LocalPartHasLengthEqualTo0() {
			String localPart = String.Empty;
			String domain = "ParivedaSolutions.com";
			new EmailAddress(localPart, domain);
		}
		[TestMethod()]
		[Description(".ctor(String, String) constructor when 'localPart' has a length greater than 64.")]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void EmailAddress_Unit_Constructor_LocalPartHasLengthGreaterThan64() {
			String localPart = new String('A', 65);
			String domain = "ParivedaSolutions.com";
			new EmailAddress(localPart, domain);
		}
		[TestMethod()]
		[Description(".ctor(String, String) constructor when 'domain' has a length of 0.")]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void EmailAddress_Unit_Constructor_DomainHasLengthEqualTo0() {
			String localPart = "Chad.Greer";
			String domain = String.Empty;
			new EmailAddress(localPart, domain);
		}
		[TestMethod()]
		[Description(".ctor(String, String) constructor when 'domain' has a length greater than 253.")]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void EmailAddress_Unit_Constructor_DomainHasLengthGreaterThan253() {
			String localPart = "A";
			String domain = new String('A', 254);
			new EmailAddress(localPart, domain);
		}
		[TestMethod()]
		[Description(".ctor(String, String) constructor when the combined length of 'localPart' and 'domain' is greater than 254.")]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void EmailAddress_Unit_Constructor_LocalPartAndDomainHaveCombinedLengthGreaterThan254() {
			String localPart = new String('A', 51);
			String domain = new String('A', 200) + ".com";
			new EmailAddress(localPart, domain);
		}
		[TestMethod()]
		[Description(".ctor(String, String) constructor when 'localPart' contains invalid characters.")]
		public void EmailAddress_Unit_Constructor_LocalPartContainsInvalidCharacters() {
			String domain = "ParivedaSolutions.com";
			foreach (Char invalidCharacter in EmailAddressTests.GetInvalidLocalPartAsciiCharacters()) {
				String localPart = "A" + invalidCharacter + "A";
				try {
					new EmailAddress(localPart, domain);
					Assert.Fail("The character {0} should cause an exception.", invalidCharacter);
				}
				catch (ArgumentNullException) {
					throw;
				}
				catch (ArgumentOutOfRangeException) {
					throw;
				}
				catch (ArgumentException) {
					/* Success */
				}
			}
		}
		[TestMethod()]
		[Description(".ctor(String, String) constructor when 'localPart' starts with a period.")]
		[ExpectedException(typeof(ArgumentException))]
		public void EmailAddress_Unit_Constructor_LocalPartStartsWithPeriod() {
			String localPart = ".Chad.Greer";
			String domain = "ParivedaSolutions.com";
			new EmailAddress(localPart, domain);
		}
		[TestMethod()]
		[Description(".ctor(String, String) constructor when 'localPart' ends with a period.")]
		[ExpectedException(typeof(ArgumentException))]
		public void EmailAddress_Unit_Constructor_LocalPartEndsWithPeriod() {
			String localPart = "Chad.Greer.";
			String domain = "ParivedaSolutions.com";
			new EmailAddress(localPart, domain);
		}
		[TestMethod()]
		[Description(".ctor(String, String) constructor when 'localPart' contains consecutive periods.")]
		[ExpectedException(typeof(ArgumentException))]
		public void EmailAddress_Unit_Constructor_LocalPartContainsConsecutivePeriods() {
			String localPart = "Chad..Greer";
			String domain = "ParivedaSolutions.com";
			new EmailAddress(localPart, domain);
		}
		[TestMethod()]
		[Description(".ctor(String, String) constructor when 'domain' contains invalid characters.")]
		public void EmailAddress_Unit_Constructor_DomainContainsInvalidCharacters() {
			String localPart = "Chad.Greer";
			foreach (Char invalidCharacter in EmailAddressTests.GetInvalidDomainAsciiCharacters()) {
				String domain = "A" + invalidCharacter + "A.com";
				try {
					new EmailAddress(localPart, domain);
					Assert.Fail("The character {0} should cause an exception.", invalidCharacter);
				}
				catch (ArgumentNullException) {
					throw;
				}
				catch (ArgumentOutOfRangeException) {
					throw;
				}
				catch (ArgumentException) {
					/* Success */
				}
			}
		}
		[TestMethod()]
		[Description(".ctor(String, String) constructor when 'domain' starts with a hyphen.")]
		[ExpectedException(typeof(ArgumentException))]
		public void EmailAddress_Unit_Constructor_DomainStartsWithHyphen() {
			String localPart = "Chad.Greer";
			String domain = "-ParivedaSolutions.com";
			new EmailAddress(localPart, domain);
		}
		[TestMethod()]
		[Description(".ctor(String, String) constructor when 'domain' ends with a hyphen.")]
		[ExpectedException(typeof(ArgumentException))]
		public void EmailAddress_Unit_Constructor_DomainEndsWithHyphen() {
			String localPart = "Chad.Greer";
			String domain = "ParivedaSolutions.com-";
			new EmailAddress(localPart, domain);
		}
		[TestMethod()]
		[Description(".ctor(String, String) constructor when 'domain' starts with a period.")]
		[ExpectedException(typeof(ArgumentException))]
		public void EmailAddress_Unit_Constructor_DomainStartsWithPeriod() {
			String localPart = "Chad.Greer";
			String domain = ".ParivedaSolutions.com";
			new EmailAddress(localPart, domain);
		}
		[TestMethod()]
		[Description(".ctor(String, String) constructor when 'domain' ends with a period.")]
		[ExpectedException(typeof(ArgumentException))]
		public void EmailAddress_Unit_Constructor_DomainEndsWithPeriod() {
			String localPart = "Chad.Greer";
			String domain = "ParivedaSolutions.com.";
			new EmailAddress(localPart, domain);
		}
		[TestMethod()]
		[Description(".ctor(String, String) constructor when 'domain' contains consecutive periods.")]
		[ExpectedException(typeof(ArgumentException))]
		public void EmailAddress_Unit_Constructor_DomainContainsConsecutive() {
			String localPart = "Chad.Greer";
			String domain = "ParivedaSolutions..com";
			new EmailAddress(localPart, domain);
		}
		[TestMethod()]
		[Description(".ctor(String, String) constructor when 'domain' contains a period followed by a hyphen.")]
		[ExpectedException(typeof(ArgumentException))]
		public void EmailAddress_Unit_Constructor_DomainContainsPeriodHyphen() {
			String localPart = "Chad.Greer";
			String domain = "ParivedaSolutions.-com";
			new EmailAddress(localPart, domain);
		}
		[TestMethod()]
		[Description(".ctor(String, String) constructor when 'domain' contains a hyphen followed by a period.")]
		[ExpectedException(typeof(ArgumentException))]
		public void EmailAddress_Unit_Constructor_DomainContainsHyphenPeriod() {
			String localPart = "Chad.Greer";
			String domain = "ParivedaSolutions-.com";
			new EmailAddress(localPart, domain);
		}

	// Property Tests
		[TestMethod()]
		[Description("Domain property for the optimal path.")]
		public void EmailAddress_Unit_Domain_Optimal() {
			String localPart = "Chad.Greer";
			String domain = "ParivedaSolutions.com";
			EmailAddress target = new EmailAddress(localPart, domain);

			Assert.AreEqual(domain, target.Domain);
		}

		[TestMethod()]
		[Description("LocalPart property for the optimal path.")]
		public void EmailAddress_Unit_LocalPart_Optimal() {
			String localPart = "Chad.Greer";
			String domain = "ParivedaSolutions.com";
			EmailAddress target = new EmailAddress(localPart, domain);

			Assert.AreEqual(localPart, target.LocalPart);
		}

	// Method Tests
		[TestMethod()]
		[Description("Equals(Object) method when 'obj' is the same object.")]
		public void EmailAddress_Unit_Equals1_ObjIsSameObject() {
			String localPart = "Chad.Greer";
			String domain = "ParivedaSolutions.com";
			EmailAddress target = new EmailAddress(localPart, domain);
			Object obj = target;

			Boolean actual = target.Equals(obj);
			Assert.AreEqual(true, actual);
		}
		[TestMethod()]
		[Description("Equals(Object) method when 'obj' is an equivalent object.")]
		public void EmailAddress_Unit_Equals1_ObjIsEquivalentObject() {
			String localPart = "Chad.Greer";
			String domain = "ParivedaSolutions.com";
			EmailAddress target = new EmailAddress(localPart, domain);
			Object obj = new EmailAddress(localPart.ToUpperInvariant(), domain.ToUpperInvariant());

			Boolean actual = target.Equals(obj);
			Assert.AreEqual(true, actual);
		}
		[TestMethod()]
		[Description("Equals(Object) method when 'obj' has a different domain.")]
		public void EmailAddress_Unit_Equals1_ObjHasDifferentDomain() {
			String localPart = "Chad.Greer";
			String domain = "ParivedaSolutions.com";
			EmailAddress target = new EmailAddress(localPart, domain);
			Object obj = new EmailAddress(localPart, "Test" + domain);

			Boolean actual = target.Equals(obj);
			Assert.AreEqual(false, actual);
		}
		[TestMethod()]
		[Description("Equals(Object) method when 'obj' has a different local part.")]
		public void EmailAddress_Unit_Equals1_ObjHasDifferentLocalPart() {
			String localPart = "Chad.Greer";
			String domain = "ParivedaSolutions.com";
			EmailAddress target = new EmailAddress(localPart, domain);
			Object obj = new EmailAddress(localPart + "Test", domain);

			Boolean actual = target.Equals(obj);
			Assert.AreEqual(false, actual);
		}
		[TestMethod()]
		[Description("Equals(Object) method when 'obj' is a null reference.")]
		public void EmailAddress_Unit_Equals1_ObjIsNull() {
			String localPart = "Chad.Greer";
			String domain = "ParivedaSolutions.com";
			EmailAddress target = new EmailAddress(localPart, domain);
			Object obj = null;

			Boolean actual = target.Equals(obj);
			Assert.AreEqual(false, actual);
		}
		[TestMethod()]
		[Description("Equals(Object) method when 'obj' is a different type.")]
		public void EmailAddress_Unit_Equals1_ObjIsDifferentType() {
			String localPart = "Chad.Greer";
			String domain = "ParivedaSolutions.com";
			EmailAddress target = new EmailAddress(localPart, domain);
			Object obj = new Object();

			Boolean actual = target.Equals(obj);
			Assert.AreEqual(false, actual);
		}

		[TestMethod()]
		[Description("Equals(EmailAddress) method when 'obj' is the same object.")]
		public void EmailAddress_Unit_Equals2_ObjIsSameObject() {
			String localPart = "Chad.Greer";
			String domain = "ParivedaSolutions.com";
			EmailAddress target = new EmailAddress(localPart, domain);
			EmailAddress other = target;

			Boolean actual = target.Equals(other);
			Assert.AreEqual(true, actual);
		}
		[TestMethod()]
		[Description("Equals(EmailAddress) method when 'obj' is an equivalent object.")]
		public void EmailAddress_Unit_Equals2_ObjIsEquivalentObject() {
			String localPart = "Chad.Greer";
			String domain = "ParivedaSolutions.com";
			EmailAddress target = new EmailAddress(localPart, domain);
			EmailAddress other = new EmailAddress(localPart.ToUpperInvariant(), domain.ToUpperInvariant());

			Boolean actual = target.Equals(other);
			Assert.AreEqual(true, actual);
		}
		[TestMethod()]
		[Description("Equals(EmailAddress) method when 'obj' has a different domain.")]
		public void EmailAddress_Unit_Equals2_ObjHasDifferentDomain() {
			String localPart = "Chad.Greer";
			String domain = "ParivedaSolutions.com";
			EmailAddress target = new EmailAddress(localPart, domain);
			EmailAddress other = new EmailAddress(localPart, "Test" + domain);

			Boolean actual = target.Equals(other);
			Assert.AreEqual(false, actual);
		}
		[TestMethod()]
		[Description("Equals(EmailAddress) method when 'obj' has a different local part.")]
		public void EmailAddress_Unit_Equals2_ObjHasDifferentLocalPart() {
			String localPart = "Chad.Greer";
			String domain = "ParivedaSolutions.com";
			EmailAddress target = new EmailAddress(localPart, domain);
			EmailAddress other = new EmailAddress(localPart + "Test", domain);

			Boolean actual = target.Equals(other);
			Assert.AreEqual(false, actual);
		}
		[TestMethod()]
		[Description("Equals(EmailAddress) method when 'obj' is a null reference.")]
		public void EmailAddress_Unit_Equals2_ObjIsNull() {
			String localPart = "Chad.Greer";
			String domain = "ParivedaSolutions.com";
			EmailAddress target = new EmailAddress(localPart, domain);
			EmailAddress other = null;

			Boolean actual = target.Equals(other);
			Assert.AreEqual(false, actual);
		}

		[TestMethod()]
		[Description("GetHashCode() method when compared to the same instance.")]
		public void EmailAddress_Unit_GetHashCode_SameInstances() {
			String localPart = "Chad.Greer";
			String domain = "ParivedaSolutions.com";
			EmailAddress first = new EmailAddress(localPart, domain);
			EmailAddress second = first;

			Assert.AreEqual(first.GetHashCode(), second.GetHashCode());
		}
		[TestMethod()]
		[Description("GetHashCode() method when compared to an equivalent instance.")]
		public void EmailAddress_Unit_GetHashCode_EquivalentInstances() {
			String localPart = "Chad.Greer";
			String domain = "ParivedaSolutions.com";
			EmailAddress first = new EmailAddress(localPart, domain);
			EmailAddress second = new EmailAddress(localPart.ToUpperInvariant(), domain.ToUpperInvariant());

			Assert.AreEqual(first.GetHashCode(), second.GetHashCode());
		}
		[TestMethod()]
		[Description("GetHashCode() method when compared to an e-mail address with a different domain.")]
		public void EmailAddress_Unit_GetHashCode_DifferentDomains() {
			String localPart = "Chad.Greer";
			String domain = "ParivedaSolutions.com";
			EmailAddress first = new EmailAddress(localPart, domain);
			EmailAddress second = new EmailAddress(localPart, "Test" + domain);

			Assert.AreNotEqual(first.GetHashCode(), second.GetHashCode());
		}
		[TestMethod()]
		[Description("GetHashCode() method when compared to an e-mail address with a different local part.")]
		public void EmailAddress_Unit_GetHashCode_DifferentLocalParts() {
			String localPart = "Chad.Greer";
			String domain = "ParivedaSolutions.com";
			EmailAddress first = new EmailAddress(localPart, domain);
			EmailAddress second = new EmailAddress(localPart + "Test", domain);

			Assert.AreNotEqual(first.GetHashCode(), second.GetHashCode());
		}

		[TestMethod()]
		[Description("Parse(String) method for the optimal path.")]
		public void EmailAddress_Unit_Parse_Optimal() {
			foreach (Pair<String> emailAddressPair in EmailAddressTests.GetValidEmailAddresses()) {
				String s = emailAddressPair.First + "@" + emailAddressPair.Second;
				EmailAddress target = EmailAddress.Parse(s);
				Assert.IsNotNull(target);
			}
		}
		[TestMethod()]
		[Description("Parse(String) method when 's' is a null reference.")]
		[ExpectedException(typeof(ArgumentNullException))]
		public void EmailAddress_Unit_Parse_SIsNull() {
			String s = null;
			EmailAddress.Parse(s);
		}
		[TestMethod()]
		[Description("Parse(String) method when 's' is invalid.")]
		public void EmailAddress_Unit_Parse_SIsInvalid() {
			foreach (Char invalidCharacter in EmailAddressTests.GetInvalidLocalPartAsciiCharacters()) {
				String s = "A" + invalidCharacter + "A@ParivedaSolutions.com";
				try {
					EmailAddress.Parse(s);
					Assert.Fail("The character {0} should cause an exception.", invalidCharacter);
				}
				catch (FormatException) {
					/* Success */
				}
			}

			foreach (Char invalidCharacter in EmailAddressTests.GetInvalidDomainAsciiCharacters()) {
				String s = "Chad.Greer@A" + invalidCharacter + "A.com";
				try {
					EmailAddress.Parse(s);
					Assert.Fail("The character {0} should cause an exception.", invalidCharacter);
				}
				catch (FormatException) {
					/* Success */
				}
			}
		}

		[TestMethod()]
		[Description("ToString() method for the optimal path.")]
		public void EmailAddress_Unit_ToString_Optimal() {
			String localPart = "Chad.Greer";
			String domain = "ParivedaSolutions.com";
			EmailAddress target = new EmailAddress(localPart, domain);
			String expected = localPart + "@" + domain;

			String actual = target.ToString();
			Assert.AreEqual(expected, actual);
		}

		[TestMethod()]
		[Description("TryParse(String, &EmailAddress) method for the optimal path.")]
		public void EmailAddress_Unit_TryParse_Optimal() {
			foreach (Pair<String> emailAddressPair in EmailAddressTests.GetValidEmailAddresses()) {
				String s = emailAddressPair.First + "@" + emailAddressPair.Second;
				EmailAddress result;

				Boolean actual = EmailAddress.TryParse(s, out result);
				Assert.AreEqual(true, actual);
				Assert.IsNotNull(result);
			}
		}
		[TestMethod()]
		[Description("TryParse(String, &EmailAddress) method when 's' is a null reference.")]
		public void EmailAddress_Unit_TryParse_SIsNull() {
			String s = null;
			EmailAddress result;

			Boolean actual = EmailAddress.TryParse(s, out result);
			Assert.AreEqual(false, actual);
			Assert.IsNull(result);
		}
		[TestMethod()]
		[Description("TryParse(String, &EmailAddress) method when 's' is invalid.")]
		public void EmailAddress_Unit_TryParse_SIsInvalid() {
			EmailAddress result;

			foreach (Char invalidCharacter in EmailAddressTests.GetInvalidLocalPartAsciiCharacters()) {
				String s = "A" + invalidCharacter + "A@ParivedaSolutions.com";
				Boolean actual = EmailAddress.TryParse(s, out result);
				Assert.AreEqual(false, actual);
				Assert.IsNull(result);
			}

			foreach (Char invalidCharacter in EmailAddressTests.GetInvalidDomainAsciiCharacters()) {
				String s = "Chad.Greer@A" + invalidCharacter + "A.com";
				Boolean actual = EmailAddress.TryParse(s, out result);
				Assert.AreEqual(false, actual);
				Assert.IsNull(result);
			}
		}

	// Operator Tests
		[TestMethod()]
		[Description("EmailAddress==EmailAddress operator when 'objB' is the same object.")]
		public void EmailAddress_Unit_EqualityOperator_SameReferences() {
			String localPart = "Chad.Greer";
			String domain = "ParivedaSolutions.com";
			EmailAddress objA = new EmailAddress(localPart, domain);
			EmailAddress objB = objA;

			Boolean actual = objA == objB;
			Assert.AreEqual(true, actual);
		}
		[TestMethod()]
		[Description("EmailAddress==EmailAddress operator when 'objB' is an equivalent object.")]
		public void EmailAddress_Unit_EqualityOperator_EquivalentObjects() {
			String localPart = "Chad.Greer";
			String domain = "ParivedaSolutions.com";
			EmailAddress objA = new EmailAddress(localPart, domain);
			EmailAddress objB = new EmailAddress(localPart.ToUpperInvariant(), domain.ToUpperInvariant());

			Boolean actual = objA == objB;
			Assert.AreEqual(true, actual);
		}
		[TestMethod()]
		[Description("EmailAddress==EmailAddress operator when 'objB' has a different domain.")]
		public void EmailAddress_Unit_EqualityOperator_UnequivalentObjects1() {
			String localPart = "Chad.Greer";
			String domain = "ParivedaSolutions.com";
			EmailAddress objA = new EmailAddress(localPart, domain);
			EmailAddress objB = new EmailAddress(localPart, "Test" + domain);

			Boolean actual = objA == objB;
			Assert.AreEqual(false, actual);
		}
		[TestMethod()]
		[Description("EmailAddress==EmailAddress operator when 'objB' has a different local part.")]
		public void EmailAddress_Unit_EqualityOperator_UnequivalentObjects2() {
			String localPart = "Chad.Greer";
			String domain = "ParivedaSolutions.com";
			EmailAddress objA = new EmailAddress(localPart, domain);
			EmailAddress objB = new EmailAddress(localPart + "Test", domain);

			Boolean actual = objA == objB;
			Assert.AreEqual(false, actual);
		}
		[TestMethod()]
		[Description("EmailAddress==EmailAddress operator when 'objA' is a null reference.")]
		public void EmailAddress_Unit_EqualityOperator_ObjAIsNull() {
			String localPart = "Chad.Greer";
			String domain = "ParivedaSolutions.com";
			EmailAddress objA = null;
			EmailAddress objB = new EmailAddress(localPart, domain);

			Boolean actual = objA == objB;
			Assert.AreEqual(false, actual);
		}
		[TestMethod()]
		[Description("EmailAddress==EmailAddress operator when 'objB' is a null reference.")]
		public void EmailAddress_Unit_EqualityOperator_ObjBIsNull() {
			String localPart = "Chad.Greer";
			String domain = "ParivedaSolutions.com";
			EmailAddress objA = new EmailAddress(localPart, domain);
			EmailAddress objB = null;

			Boolean actual = objA == objB;
			Assert.AreEqual(false, actual);
		}
		[TestMethod()]
		[Description("EmailAddress==EmailAddress operator when 'objA' and 'objB' are null references.")]
		public void EmailAddress_Unit_EqualityOperator_ObjAIsNullAndObjBIsNull() {
			EmailAddress objA = null;
			EmailAddress objB = null;

			Boolean actual = objA == objB;
			Assert.AreEqual(true, actual);
		}

		[TestMethod()]
		[Description("EmailAddress!=EmailAddress operator when 'objB' is the same object.")]
		public void EmailAddress_Unit_InequalityOperator_SameReferences() {
			String localPart = "Chad.Greer";
			String domain = "ParivedaSolutions.com";
			EmailAddress objA = new EmailAddress(localPart, domain);
			EmailAddress objB = objA;

			Boolean actual = objA != objB;
			Assert.AreEqual(false, actual);
		}
		[TestMethod()]
		[Description("EmailAddress!=EmailAddress operator when 'objB' is an equivalent object.")]
		public void EmailAddress_Unit_InequalityOperator_EquivalentObjects() {
			String localPart = "Chad.Greer";
			String domain = "ParivedaSolutions.com";
			EmailAddress objA = new EmailAddress(localPart, domain);
			EmailAddress objB = new EmailAddress(localPart.ToUpperInvariant(), domain.ToUpperInvariant());

			Boolean actual = objA != objB;
			Assert.AreEqual(false, actual);
		}
		[TestMethod()]
		[Description("EmailAddress!=EmailAddress operator when 'objB' has a different domain.")]
		public void EmailAddress_Unit_InequalityOperator_UnequivalentObjects1() {
			String localPart = "Chad.Greer";
			String domain = "ParivedaSolutions.com";
			EmailAddress objA = new EmailAddress(localPart, domain);
			EmailAddress objB = new EmailAddress(localPart, "Test" + domain);

			Boolean actual = objA != objB;
			Assert.AreEqual(true, actual);
		}
		[TestMethod()]
		[Description("EmailAddress!=EmailAddress operator when 'objB' has a different local part.")]
		public void EmailAddress_Unit_InequalityOperator_UnequivalentObjects2() {
			String localPart = "Chad.Greer";
			String domain = "ParivedaSolutions.com";
			EmailAddress objA = new EmailAddress(localPart, domain);
			EmailAddress objB = new EmailAddress(localPart + "Test", domain);

			Boolean actual = objA != objB;
			Assert.AreEqual(true, actual);
		}
		[TestMethod()]
		[Description("EmailAddress!=EmailAddress operator when 'objA' is a null reference.")]
		public void EmailAddress_Unit_InequalityOperator_ObjAIsNull() {
			String localPart = "Chad.Greer";
			String domain = "ParivedaSolutions.com";
			EmailAddress objA = null;
			EmailAddress objB = new EmailAddress(localPart, domain);

			Boolean actual = objA != objB;
			Assert.AreEqual(true, actual);
		}
		[TestMethod()]
		[Description("EmailAddress!=EmailAddress operator when 'objB' is a null reference.")]
		public void EmailAddress_Unit_InequalityOperator_ObjBIsNull() {
			String localPart = "Chad.Greer";
			String domain = "ParivedaSolutions.com";
			EmailAddress objA = new EmailAddress(localPart, domain);
			EmailAddress objB = null;

			Boolean actual = objA != objB;
			Assert.AreEqual(true, actual);
		}
		[TestMethod()]
		[Description("EmailAddress!=EmailAddress operator when 'objA' and 'objB' are null references.")]
		public void EmailAddress_Unit_InequalityOperator_ObjAIsNullAndObjBIsNull() {
			EmailAddress objA = null;
			EmailAddress objB = null;

			Boolean actual = objA != objB;
			Assert.AreEqual(false, actual);
		}

		[TestMethod()]
		[Description("Implicit (String)EmailAddress operator for the optimal path.")]
		public void EmailAddress_Unit_ImplicitStringCastOperator_Optimal() {
			foreach (Pair<String> validEmailAddressPair in EmailAddressTests.GetValidEmailAddresses()) {
				String localPart = validEmailAddressPair.First;
				String domain = validEmailAddressPair.Second;
				EmailAddress value = new EmailAddress(localPart, domain);
				String expected = localPart + "@" + domain;

				String actual = value;
				Assert.AreEqual(expected, actual);
			}
		}
		[TestMethod()]
		[Description("Implicit (String)EmailAddress operator when 'value' is a null reference.")]
		public void EmailAddress_Unit_ImplicitStringCastOperator_ValueIsNull() {
			EmailAddress value = null;
			String expected = null;

			String actual = value;
			Assert.AreEqual(expected, actual);
		}

		[TestMethod()]
		[Description("Explicit (EmailAddress)String operator for the optimal path.")]
		public void EmailAddress_Unit_ExplicitEmailAddressCastOperator_Optimal() {
			foreach (Pair<String> validEmailAddressPair in EmailAddressTests.GetValidEmailAddresses()) {
				String localPart = validEmailAddressPair.First;
				String domain = validEmailAddressPair.Second;
				String value = localPart + "@" + domain;
				EmailAddress expected = new EmailAddress(localPart, domain);

				EmailAddress actual = (EmailAddress)value;
				Assert.AreEqual(expected, actual);
			}
		}
		[TestMethod()]
		[Description("Explicit (EmailAddress)String operator when 'value' is a null reference.")]
		public void EmailAddress_Unit_ExplicitEmailAddressCastOperator_ValueIsNull() {
			String value = null;
			EmailAddress expected = null;

			EmailAddress actual = (EmailAddress)value;
			Assert.AreEqual(expected, actual);
		}
		[TestMethod()]
		[Description("Explicit (EmailAddress)String operator when 'value' is invalid.")]
		public void EmailAddress_Unit_ExplicitEmailAddressCastOperator_ValueIsInvalid() {
			foreach (Char invalidCharacter in EmailAddressTests.GetInvalidLocalPartAsciiCharacters()) {
				String localPart = "A" + invalidCharacter + "A";
				String domain = "ParivedaSolutions.com";
				String value = localPart + "@" + domain;
				try {
					EmailAddress actual = (EmailAddress)value;
				}
				catch (InvalidCastException) {
					/* Success */
				}
			}

			foreach (Char invalidCharacter in EmailAddressTests.GetInvalidDomainAsciiCharacters()) {
				String localPart = "Chad.Greer";
				String domain = "A" + invalidCharacter + "A.com";
				String value = localPart + "@" + domain;
				try {
					EmailAddress actual = (EmailAddress)value;
				}
				catch (InvalidCastException) {
					/* Success */
				}
			}
		}

	// Serialization Tests
		[TestMethod()]
		[Description("Serializability of the class for the optimal path.")]
		public void EmailAddress_Integration_Serialization_Optimal() {
			String localPart = "Chad.Greer";
			String domain = "ParivedaSolutions.com";
			EmailAddress original = new EmailAddress(localPart, domain);

			EmailAddress clone = original.SerializeBinary();
			Assert.AreNotSame(original, clone);
			Assert.AreEqual(original, clone);
			Assert.AreEqual(original.GetHashCode(), clone.GetHashCode());
			Assert.AreEqual(original.LocalPart, clone.LocalPart);
			Assert.AreEqual(original.Domain, clone.Domain);
		}
	}
}