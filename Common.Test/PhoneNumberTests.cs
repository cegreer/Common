using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Vizistata {
	/// <summary>
	/// This is a test class for <see cref="T:PhoneNumber"/> and is intended to contain all <see cref="T:PhoneNumber"/> Unit Tests.
	///</summary>
	[TestClass()]
	public class PhoneNumberTests {
		#region Test Class Implementation
		/// <summary>
		/// Describes the context under which the current test is running.
		/// </summary>
		private TestContext _testContextInstance;

		/// <summary>
		/// Initializes a new instance of the <see cref="T:PhoneNumberTests"/> class.
		/// </summary>
		public PhoneNumberTests() : base() { }

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

	// Properties
		/// <summary>
		/// Gets the enumerable collection of invalid phone number values to test.
		/// </summary>
		private static IEnumerable<String> InvalidValues {
			get {
				return new String[] {
					String.Empty,
					"2 800 867 5309",
					"867 5309",
					"1-800-CALL-ME",
					" \t \r \n + . - ",
					"8675309"
				};
			}
		}
		/// <summary>
		/// Gets the enumerable collection of valid phone number values to test.
		/// </summary>
		private static IEnumerable<String> ValidValues {
			get {
				return new String[] {
					"800 867 5309",
					"800-867-5309",
					"(800) 867-5309",
					"8008675309",
					"1 800 867 5309",
					"800-867-5309",
					"1 (800) 867-5309",
					"18008675309",
					"+1 (800) 867-5309",
					"800.867.5309",
					"1.800.867.5309",
					"1-800-CALL-NOW"
				};
			}
		}

	// Constructor Tests
		[TestMethod()]
		[Description(".ctor(String, String, String[]) constructor for the optimal path.")]
		public void PhoneNumber_Unit_Constructor_Optimal() {
			String countryCode = "1";
			String areaCode = "800";
			String[] subscriberNumberGroups = new String[] { "867", "5309" };
			new PhoneNumber(countryCode, areaCode, subscriberNumberGroups);
		}
		[TestMethod()]
		[Description(".ctor(String, String, String[]) constructor when 'countryCode' is a null reference.")]
		[ExpectedException(typeof(ArgumentNullException))]
		public void PhoneNumber_Unit_Constructor_CountryCodeIsNull() {
			String countryCode = null;
			String areaCode = "800";
			String[] subscriberNumberGroups = new String[] { "867", "5309" };
			new PhoneNumber(countryCode, areaCode, subscriberNumberGroups);
		}
		[TestMethod()]
		[Description(".ctor(String, String, String[]) constructor when 'areaCode' is a null reference.")]
		[ExpectedException(typeof(ArgumentNullException))]
		public void PhoneNumber_Unit_Constructor_AreaCodeIsNull() {
			String countryCode = "1";
			String areaCode = null;
			String[] subscriberNumberGroups = new String[] { "867", "5309" };
			new PhoneNumber(countryCode, areaCode, subscriberNumberGroups);
		}
		[TestMethod()]
		[Description(".ctor(String, String, String[]) constructor when 'subscriberNumberGroups' is a null reference.")]
		[ExpectedException(typeof(ArgumentNullException))]
		public void PhoneNumber_Unit_Constructor_SubscriberNumberGroupsIsNull() {
			String countryCode = "1";
			String areaCode = "800";
			String[] subscriberNumberGroups = null;
			new PhoneNumber(countryCode, areaCode, subscriberNumberGroups);
		}
		[TestMethod()]
		[Description(".ctor(String, String, String[]) constructor when 'countryCode' is empty.")]
		[ExpectedException(typeof(ArgumentException))]
		public void PhoneNumber_Unit_Constructor_CountryCodeIsEmpty() {
			String countryCode = String.Empty;
			String areaCode = "800";
			String[] subscriberNumberGroups = new String[] { "867", "5309" };
			new PhoneNumber(countryCode, areaCode, subscriberNumberGroups);
		}
		[TestMethod()]
		[Description(".ctor(String, String, String[]) constructor when 'countryCode' is invalid.")]
		[ExpectedException(typeof(NotSupportedException))]
		public void PhoneNumber_Unit_Constructor_CountryCodeIsInvalid() {
			String countryCode = "0";
			String areaCode = "800";
			String[] subscriberNumberGroups = new String[] { "867", "5309" };
			new PhoneNumber(countryCode, areaCode, subscriberNumberGroups);
		}
		[TestMethod()]
		[Description(".ctor(String, String, String[]) constructor when 'countryCode' is not numeric.")]
		[ExpectedException(typeof(ArgumentException))]
		public void PhoneNumber_Unit_Constructor_CountryCodeIsNotNumeric() {
			String countryCode = "a";
			String areaCode = "800";
			String[] subscriberNumberGroups = new String[] { "867", "5309" };
			new PhoneNumber(countryCode, areaCode, subscriberNumberGroups);
		}
		[TestMethod()]
		[Description(".ctor(String, String, String[]) constructor when 'areaCode' is empty.")]
		[ExpectedException(typeof(ArgumentException))]
		public void PhoneNumber_Unit_Constructor_AreaCodeIsEmpty() {
			String countryCode = "1";
			String areaCode = String.Empty;
			String[] subscriberNumberGroups = new String[] { "867", "5309" };
			new PhoneNumber(countryCode, areaCode, subscriberNumberGroups);
		}
		[TestMethod()]
		[Description(".ctor(String, String, String[]) constructor when 'areaCode' is not alphanumeric.")]
		[ExpectedException(typeof(ArgumentException))]
		public void PhoneNumber_Unit_Constructor_AreaCodeIsNotAlphanumeric() {
			String countryCode = "1";
			String areaCode = "8 8 8";
			String[] subscriberNumberGroups = new String[] { "867", "5309" };
			new PhoneNumber(countryCode, areaCode, subscriberNumberGroups);
		}
		[TestMethod()]
		[Description(".ctor(String, String, String[]) constructor when 'subscriberNumberGroups' is empty.")]
		[ExpectedException(typeof(ArgumentException))]
		public void PhoneNumber_Unit_Constructor_SubscriberNumberGroupsIsEmpty() {
			String countryCode = "1";
			String areaCode = "800";
			String[] subscriberNumberGroups = new String[0];
			new PhoneNumber(countryCode, areaCode, subscriberNumberGroups);
		}
		[TestMethod()]
		[Description(".ctor(String, String, String[]) constructor when 'subscriberNumberGroups' contains an element that is a null reference.")]
		[ExpectedException(typeof(ArgumentException))]
		public void PhoneNumber_Unit_Constructor_SubscriberNumberGroupsContainsNullElement() {
			String countryCode = "1";
			String areaCode = "800";
			String[] subscriberNumberGroups = new String[] { "867", null, "5309" };
			new PhoneNumber(countryCode, areaCode, subscriberNumberGroups);
		}
		[TestMethod()]
		[Description(".ctor(String, String, String[]) constructor when 'subscriberNumberGroups' contains an element that is empty.")]
		[ExpectedException(typeof(ArgumentException))]
		public void PhoneNumber_Unit_Constructor_SubscriberNumberGroupsContainsEmptyElement() {
			String countryCode = "1";
			String areaCode = "800";
			String[] subscriberNumberGroups = new String[] { "867", String.Empty, "5309" };
			new PhoneNumber(countryCode, areaCode, subscriberNumberGroups);
		}
		[TestMethod()]
		[Description(".ctor(String, String, String[]) constructor when 'subscriberNumberGroups' is not alphanumberic.")]
		[ExpectedException(typeof(ArgumentException))]
		public void PhoneNumber_Unit_Constructor_SubscriberNumberGroupsContainsNonAlphanumericElement() {
			String countryCode = "1";
			String areaCode = "800";
			String[] subscriberNumberGroups = new String[] { "8 6 7", "5309" };
			new PhoneNumber(countryCode, areaCode, subscriberNumberGroups);
		}

	// Property Tests
		[TestMethod()]
		[Description("AreaCode property for the optimal path.")]
		public void PhoneNumber_Unit_AreaCode_Optimal() {
			String countryCode = "1";
			String areaCode = "800";
			String[] subscriberNumberGroups = new String[] { "867", "5309" };
			PhoneNumber target = new PhoneNumber(countryCode, areaCode, subscriberNumberGroups);

			Assert.AreEqual(areaCode, target.AreaCode);
		}
		[TestMethod()]
		[Description("CountryCode property for the optimal path.")]
		public void PhoneNumber_Unit_CountryCode_Optimal() {
			String countryCode = "1";
			String areaCode = "800";
			String[] subscriberNumberGroups = new String[] { "867", "5309" };
			PhoneNumber target = new PhoneNumber(countryCode, areaCode, subscriberNumberGroups);

			Assert.AreEqual(countryCode, target.CountryCode);
		}
		[TestMethod()]
		[Description("SubscriberNumberGroups property for the optimal path.")]
		public void PhoneNumber_Unit_SubscriberNumberGroups_Optimal() {
			String countryCode = "1";
			String areaCode = "800";
			String[] subscriberNumberGroups = new String[] { "867", "5309" };
			PhoneNumber target = new PhoneNumber(countryCode, areaCode, subscriberNumberGroups);

			CollectionAssert.AreEquivalent(subscriberNumberGroups, target.SubscriberNumberGroups.ToArray());
		}

	// Method Tests
		[TestMethod()]
		[Description("Equals(Object) method when 'obj' is the same reference.")]
		public void PhoneNumber_Unit_Equals1_ObjIsSameReference() {
			String countryCode = "1";
			String areaCode = "800";
			String[] subscriberNumberGroups = new String[] { "867", "5309" };
			PhoneNumber target = new PhoneNumber(countryCode, areaCode, subscriberNumberGroups);
			Object obj = target;

			Boolean actual = target.Equals(obj);
			Assert.AreEqual(true, actual);
		}
		[TestMethod()]
		[Description("Equals(Object) method when 'obj' is an equal object.")]
		public void PhoneNumber_Unit_Equals1_ObjIsEqual() {
			String countryCode = "1";
			String areaCode = "800";
			String[] subscriberNumberGroups = new String[] { "867", "5309" };
			PhoneNumber target = new PhoneNumber(countryCode, areaCode, subscriberNumberGroups);
			Object obj = new PhoneNumber(countryCode, areaCode, subscriberNumberGroups);

			Boolean actual = target.Equals(obj);
			Assert.AreEqual(true, actual);
		}
		[TestMethod()]
		[Description("Equals(Object) method when 'obj' is an unequal object.")]
		public void PhoneNumber_Unit_Equals1_ObjIsUnequal1() {
			String countryCode = "1";
			String areaCode = "800";
			String[] subscriberNumberGroups = new String[] { "867", "5309" };
			PhoneNumber target = new PhoneNumber(countryCode, areaCode, subscriberNumberGroups);
			Object obj = new PhoneNumber(countryCode, "888", subscriberNumberGroups);

			Boolean actual = target.Equals(obj);
			Assert.AreEqual(false, actual);
		}
		[TestMethod()]
		[Description("Equals(Object) method when 'obj' is an unequal object.")]
		public void PhoneNumber_Unit_Equals1_ObjIsUnequal2() {
			String countryCode = "1";
			String areaCode = "800";
			String[] subscriberNumberGroups = new String[] { "867", "5309" };
			PhoneNumber target = new PhoneNumber(countryCode, areaCode, subscriberNumberGroups);
			Object obj = new PhoneNumber(countryCode, areaCode, new String[] { "abc", "defg" });

			Boolean actual = target.Equals(obj);
			Assert.AreEqual(false, actual);
		}
		[TestMethod()]
		[Description("Equals(Object) method when 'obj' is a null reference.")]
		public void PhoneNumber_Unit_Equals1_ObjIsNull() {
			String countryCode = "1";
			String areaCode = "800";
			String[] subscriberNumberGroups = new String[] { "867", "5309" };
			PhoneNumber target = new PhoneNumber(countryCode, areaCode, subscriberNumberGroups);
			Object obj = null;

			Boolean actual = target.Equals(obj);
			Assert.AreEqual(false, actual);
		}
		[TestMethod()]
		[Description("Equals(Object) method when 'obj' is a different type.")]
		public void PhoneNumber_Unit_Equals1_ObjIsDifferentType() {
			String countryCode = "1";
			String areaCode = "800";
			String[] subscriberNumberGroups = new String[] { "867", "5309" };
			PhoneNumber target = new PhoneNumber(countryCode, areaCode, subscriberNumberGroups);
			Object obj = countryCode + " " + areaCode + " " + subscriberNumberGroups.Join(' ');

			Boolean actual = target.Equals(obj);
			Assert.AreEqual(false, actual);
		}

		[TestMethod()]
		[Description("Equals(PhoneNumber) method when 'other' is the same reference.")]
		public void PhoneNumber_Unit_Equals2_ObjIsSameReference() {
			String countryCode = "1";
			String areaCode = "800";
			String[] subscriberNumberGroups = new String[] { "867", "5309" };
			PhoneNumber target = new PhoneNumber(countryCode, areaCode, subscriberNumberGroups);
			PhoneNumber other = target;

			Boolean actual = target.Equals(other);
			Assert.AreEqual(true, actual);
		}
		[TestMethod()]
		[Description("Equals(PhoneNumber) method when 'other' is an equal object.")]
		public void PhoneNumber_Unit_Equals2_ObjIsEqual() {
			String countryCode = "1";
			String areaCode = "800";
			String[] subscriberNumberGroups = new String[] { "867", "5309" };
			PhoneNumber target = new PhoneNumber(countryCode, areaCode, subscriberNumberGroups);
			PhoneNumber other = new PhoneNumber(countryCode, areaCode, subscriberNumberGroups);

			Boolean actual = target.Equals(other);
			Assert.AreEqual(true, actual);
		}
		[TestMethod()]
		[Description("Equals(PhoneNumber) method when 'other' is an unequal object.")]
		public void PhoneNumber_Unit_Equals2_ObjIsUnequal() {
			String countryCode = "1";
			String areaCode = "800";
			String[] subscriberNumberGroups = new String[] { "867", "5309" };
			PhoneNumber target = new PhoneNumber(countryCode, areaCode, subscriberNumberGroups);
			PhoneNumber other = new PhoneNumber(countryCode, "888", subscriberNumberGroups);

			Boolean actual = target.Equals(other);
			Assert.AreEqual(false, actual);
		}
		[TestMethod()]
		[Description("Equals(PhoneNumber) method when 'other' is a null reference.")]
		public void PhoneNumber_Unit_Equals2_ObjIsNull() {
			String countryCode = "1";
			String areaCode = "800";
			String[] subscriberNumberGroups = new String[] { "867", "5309" };
			PhoneNumber target = new PhoneNumber(countryCode, areaCode, subscriberNumberGroups);
			PhoneNumber other = null;

			Boolean actual = target.Equals(other);
			Assert.AreEqual(false, actual);
		}

		[TestMethod()]
		[Description("GetHashCode() method when the same reference is compared.")]
		public void PhoneNumber_Unit_GetHashCode_SameReferences() {
			String countryCode = "1";
			String areaCode = "800";
			String[] subscriberNumberGroups = new String[] { "867", "5309" };
			PhoneNumber phoneNumber = new PhoneNumber(countryCode, areaCode, subscriberNumberGroups);
			PhoneNumber target = phoneNumber;

			Int32 expected = phoneNumber.GetHashCode();
			Int32 actual = target.GetHashCode();
			Assert.AreEqual(expected, actual);
		}
		[TestMethod()]
		[Description("GetHashCode() method when equivalent objects are compared.")]
		public void PhoneNumber_Unit_GetHashCode_EquivalentObjects() {
			String countryCode = "1";
			String areaCode = "800";
			String[] subscriberNumberGroups = new String[] { "abc", "defg" };
			PhoneNumber phoneNumber = new PhoneNumber(countryCode, areaCode, subscriberNumberGroups);
			PhoneNumber target = new PhoneNumber(countryCode, areaCode, subscriberNumberGroups.Select(element => element.ToUpperInvariant()).ToArray());

			Int32 expected = phoneNumber.GetHashCode();
			Int32 actual = target.GetHashCode();
			Assert.AreEqual(expected, actual);
		}
		[TestMethod()]
		[Description("GetHashCode() method when unequivalent objects are compared.")]
		public void PhoneNumber_Unit_GetHashCode_UnequivalentObjects() {
			String countryCode = "1";
			String areaCode = "800";
			String[] subscriberNumberGroups = new String[] { "867", "5309" };
			PhoneNumber phoneNumber = new PhoneNumber(countryCode, areaCode, subscriberNumberGroups);
			PhoneNumber target = new PhoneNumber(countryCode, "888", subscriberNumberGroups);

			Int32 notExpected = phoneNumber.GetHashCode();
			Int32 actual = target.GetHashCode();
			Assert.AreNotEqual(notExpected, actual);
		}

		[TestMethod()]
		[Description("Parse(String) method for the optimal path.")]
		public void PhoneNumber_Unit_Parse_Optimal() {
			foreach (String value in PhoneNumberTests.ValidValues) {
				PhoneNumber actual = PhoneNumber.Parse(value);
				Assert.IsNotNull(actual, "The value {0} should be valid.", value);
			}
		}
		[TestMethod()]
		[Description("Parse(String) method when 'value' is a null reference.")]
		[ExpectedException(typeof(ArgumentNullException))]
		public void PhoneNumber_Unit_Parse_ValueIsNull() {
			String value = null;
			PhoneNumber.Parse(value);
		}
		[TestMethod()]
		[Description("Parse(String) method when 'value' is invalid.")]
		public void PhoneNumber_Unit_Parse_ValueIsInvalid() {
			foreach (String value in PhoneNumberTests.InvalidValues) {
				try {
					PhoneNumber.Parse(value);
					Assert.Fail("The value {0} should be invalid.", value);
				}
				catch (FormatException) {
					/* Success */
				}
			}
		}

		[TestMethod()]
		[Description("ToString() method for the optimal path.")]
		public void PhoneNumber_Unit_ToString_Optimal() {
			String countryCode = "1";
			String areaCode = "800";
			String[] subscriberNumberGroups = new String[] { "abc", "defg" };
			PhoneNumber target = new PhoneNumber(countryCode, areaCode, subscriberNumberGroups);

			String expected = new String[] { countryCode, areaCode }.Concat(subscriberNumberGroups).ToArray().Join(' ');
			String actual = target.ToString();
			Assert.AreEqual(expected, actual);
		}

		[TestMethod()]
		[Description("TryParse(String, &PhoneNumber) method for the optimal path.")]
		public void PhoneNumber_Unit_TryParse_Optimal() {
			foreach (String value in PhoneNumberTests.ValidValues) {
				PhoneNumber result;
				Boolean actual = PhoneNumber.TryParse(value, out result);
				Assert.IsTrue(actual, "The value {0} should be valid.", value);
				Assert.IsNotNull(result, "The value {0} should be valid.", value);
			}
		}
		[TestMethod()]
		[Description("TryParse(String, &PhoneNumber) method when 'value' is a null reference.")]
		public void PhoneNumber_Unit_TryParse_ValueIsNull() {
			String value = null;
			PhoneNumber result;
			
			Boolean actual = PhoneNumber.TryParse(value, out result);
			Assert.IsFalse(actual);
			Assert.IsNull(result);
		}
		[TestMethod()]
		[Description("TryParse(String, &PhoneNumber) method when 'value' is invalid.")]
		public void PhoneNumber_Unit_TryParse_ValueIsInvalid() {
			foreach (String value in PhoneNumberTests.InvalidValues) {
				PhoneNumber result;
				Boolean actual = PhoneNumber.TryParse(value, out result);
				Assert.IsFalse(actual, "The value {0} should be invalid.", value);
				Assert.IsNull(result, "The value {0} should be invalid.", value);
			}
		}

	// Operator Tests
		[TestMethod()]
		[Description("PhoneNumber==PhoneNumber operator when the same references are compared.")]
		public void PhoneNumber_Unit_EqualityOperator_SameReferences() {
			String countryCode = "1";
			String areaCode = "800";
			String[] subscriberNumberGroups = new String[] { "867", "5309" };
			PhoneNumber objA = new PhoneNumber(countryCode, areaCode, subscriberNumberGroups);
			PhoneNumber objB = objA;

			Boolean actual = objA == objB;
			Assert.AreEqual(true, actual);
		}
		[TestMethod()]
		[Description("PhoneNumber==PhoneNumber operator when equivalent objects are compared.")]
		public void PhoneNumber_Unit_EqualityOperator_EquivalentObjects() {
			String countryCode = "1";
			String areaCode = "800";
			String[] subscriberNumberGroups = new String[] { "867", "5309" };
			PhoneNumber objA = new PhoneNumber(countryCode, areaCode, subscriberNumberGroups);
			PhoneNumber objB = new PhoneNumber(countryCode, areaCode, subscriberNumberGroups);

			Boolean actual = objA == objB;
			Assert.AreEqual(true, actual);
		}
		[TestMethod()]
		[Description("PhoneNumber==PhoneNumber operator when unequivalent objects are compared.")]
		public void PhoneNumber_Unit_EqualityOperator_UnequivalentObjects() {
			String countryCode = "1";
			String areaCode = "800";
			String[] subscriberNumberGroups = new String[] { "867", "5309" };
			PhoneNumber objA = new PhoneNumber(countryCode, areaCode, subscriberNumberGroups);
			PhoneNumber objB = new PhoneNumber(countryCode, "888", subscriberNumberGroups);

			Boolean actual = objA == objB;
			Assert.AreEqual(false, actual);
		}
		[TestMethod()]
		[Description("PhoneNumber==PhoneNumber operator when 'objA' is a null reference.")]
		public void PhoneNumber_Unit_EqualityOperator_ObjAIsNull() {
			String countryCode = "1";
			String areaCode = "800";
			String[] subscriberNumberGroups = new String[] { "867", "5309" };
			PhoneNumber objA = new PhoneNumber(countryCode, areaCode, subscriberNumberGroups);
			PhoneNumber objB = null;

			Boolean actual = objA == objB;
			Assert.AreEqual(false, actual);
		}
		[TestMethod()]
		[Description("PhoneNumber==PhoneNumber operator when 'objB' is a null reference.")]
		public void PhoneNumber_Unit_EqualityOperator_ObjBIsNull() {
			String countryCode = "1";
			String areaCode = "800";
			String[] subscriberNumberGroups = new String[] { "867", "5309" };
			PhoneNumber objA = null;
			PhoneNumber objB = new PhoneNumber(countryCode, areaCode, subscriberNumberGroups);

			Boolean actual = objA == objB;
			Assert.AreEqual(false, actual);
		}
		[TestMethod()]
		[Description("PhoneNumber==PhoneNumber operator when 'objA' and 'objB' are null references.")]
		public void PhoneNumber_Unit_EqualityOperator_ObjAIsNullAndObjBIsNull() {
			PhoneNumber objA = null;
			PhoneNumber objB = null;

			Boolean actual = objA == objB;
			Assert.AreEqual(true, actual);
		}

		[TestMethod()]
		[Description("PhoneNumber!=PhoneNumber operator when the same references are compared.")]
		public void PhoneNumber_Unit_InequalityOperator_SameReferences() {
			String countryCode = "1";
			String areaCode = "800";
			String[] subscriberNumberGroups = new String[] { "867", "5309" };
			PhoneNumber objA = new PhoneNumber(countryCode, areaCode, subscriberNumberGroups);
			PhoneNumber objB = objA;

			Boolean actual = objA != objB;
			Assert.AreEqual(false, actual);
		}
		[TestMethod()]
		[Description("PhoneNumber!=PhoneNumber operator when equivalent objects are compared.")]
		public void PhoneNumber_Unit_InequalityOperator_EquivalentObjects() {
			String countryCode = "1";
			String areaCode = "800";
			String[] subscriberNumberGroups = new String[] { "867", "5309" };
			PhoneNumber objA = new PhoneNumber(countryCode, areaCode, subscriberNumberGroups);
			PhoneNumber objB = new PhoneNumber(countryCode, areaCode, subscriberNumberGroups);

			Boolean actual = objA != objB;
			Assert.AreEqual(false, actual);
		}
		[TestMethod()]
		[Description("PhoneNumber!=PhoneNumber operator when unequivalent objects are compared.")]
		public void PhoneNumber_Unit_InequalityOperator_UnequivalentObjects() {
			String countryCode = "1";
			String areaCode = "800";
			String[] subscriberNumberGroups = new String[] { "867", "5309" };
			PhoneNumber objA = new PhoneNumber(countryCode, areaCode, subscriberNumberGroups);
			PhoneNumber objB = new PhoneNumber(countryCode, "888", subscriberNumberGroups);

			Boolean actual = objA != objB;
			Assert.AreEqual(true, actual);
		}
		[TestMethod()]
		[Description("PhoneNumber!=PhoneNumber operator when 'objA' is a null reference.")]
		public void PhoneNumber_Unit_InequalityOperator_ObjAIsNull() {
			String countryCode = "1";
			String areaCode = "800";
			String[] subscriberNumberGroups = new String[] { "867", "5309" };
			PhoneNumber objA = new PhoneNumber(countryCode, areaCode, subscriberNumberGroups);
			PhoneNumber objB = null;

			Boolean actual = objA != objB;
			Assert.AreEqual(true, actual);
		}
		[TestMethod()]
		[Description("PhoneNumber!=PhoneNumber operator when 'objB' is a null reference.")]
		public void PhoneNumber_Unit_InequalityOperator_ObjBIsNull() {
			String countryCode = "1";
			String areaCode = "800";
			String[] subscriberNumberGroups = new String[] { "867", "5309" };
			PhoneNumber objA = null;
			PhoneNumber objB = new PhoneNumber(countryCode, areaCode, subscriberNumberGroups);

			Boolean actual = objA != objB;
			Assert.AreEqual(true, actual);
		}
		[TestMethod()]
		[Description("PhoneNumber!=PhoneNumber operator when 'objA' and 'objB' are null references.")]
		public void PhoneNumber_Unit_InequalityOperator_ObjAIsNullAndObjBIsNull() {
			PhoneNumber objA = null;
			PhoneNumber objB = null;

			Boolean actual = objA != objB;
			Assert.AreEqual(false, actual);
		}

		[TestMethod()]
		[Description("(String)PhoneNumber implicit operator for the optimal path.")]
		public void PhoneNumber_Unit_ImplicitStringCastOperator_Optimal() {
			String countryCode = "1";
			String areaCode = "800";
			String[] subscriberNumberGroups = new String[] { "867", "5309" };
			PhoneNumber target = new PhoneNumber(countryCode, areaCode, subscriberNumberGroups);

			String expected = new String[] { countryCode, areaCode }.Concat(subscriberNumberGroups).ToArray().Join(' ');
			String actual = target;
			Assert.AreEqual(expected, actual);
		}
		[TestMethod()]
		[Description("(String)PhoneNumber implicit operator when 'instance' is a null reference.")]
		public void PhoneNumber_Unit_ImplicitStringCastOperator_InstanceIsNull() {
			PhoneNumber target = null;

			String actual = target;
			Assert.IsNull(actual);
		}

		[TestMethod()]
		[Description("(PhoneNumber)String explicit operator for the optimal path.")]
		public void PhoneNumber_Unit_ExplicitPhoneNumberCastOperator_OptimalPath() {
			foreach (String instance in PhoneNumberTests.ValidValues) {
				PhoneNumber actual = (PhoneNumber)instance;
				Assert.IsNotNull(actual, "The value {0} should be valid.", instance);
			}
		}
		[TestMethod()]
		[Description("(PhoneNumber)String explicit operator when 'instance' is a null reference.")]
		public void PhoneNumber_Unit_ExplicitPhoneNumberCastOperator_InstanceIsNull() {
			String instance = null;

			PhoneNumber actual = (PhoneNumber)instance;
			Assert.IsNull(actual);
		}
		[TestMethod()]
		[Description("(PhoneNumber)String explicit operator when 'instance' is invalid.")]
		public void PhoneNumber_Unit_ExplicitPhoneNumberCastOperator_InstanceIsInvalid() {
			foreach (String instance in PhoneNumberTests.InvalidValues) {
				try {
					PhoneNumber actual = (PhoneNumber)instance;
					Assert.Fail("The value {0} should be invalid.", instance);
				}
				catch (InvalidCastException) {
					/* Success */
				}
			}
		}
	}
}