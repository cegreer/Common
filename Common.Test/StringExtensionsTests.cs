using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Vizistata {
	/// <summary>
	/// This is a test class for <see cref="T:StringExtensions"/> and is intended to contain all <see cref="T:StringExtensions"/> Unit Tests.
	///</summary>
	[TestClass()]
	public class StringExtensionsTests {
		#region Test Class Implementation
		/// <summary>
		/// Describes the context under which the current test is running.
		/// </summary>
		private TestContext _testContextInstance;

		/// <summary>
		/// Initializes a new instance of the <see cref="T:StringExtensionsTests"/> class.
		/// </summary>
		public StringExtensionsTests() : base() { }

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

	// Method Tests
		[TestMethod()]
		[Description("ContainsAny(Char[]) method when 'instance' contains a value from 'anyOf'.")]
		public void StringExtensions_Unit_ContainsAny_OptimalTrue() {
			String instance = "abcdefg";
			Char[] anyOf = { 'c' };
			Assert.IsTrue(StringExtensions.ContainsAny(instance, anyOf));
		}
		[TestMethod()]
		[Description("ContainsAny(Char[]) method when 'instance' does not contain a value from 'anyOf'.")]
		public void StringExtensions_Unit_ContainsAny_OptimalFalse() {
			String instance = "abcdefg";
			Char[] anyOf = { 'm' };
			Assert.IsFalse(StringExtensions.ContainsAny(instance, anyOf));
		}
		[TestMethod()]
		[Description("ContainsAny(Char[]) method when 'anyOf' is a null reference.")]
		[ExpectedException(typeof(ArgumentNullException))]
		public void StringExtensions_Unit_ContainsAny_AnyOfIsNull() {
			String instance = "abcdefg";
			Char[] anyOf = null;
			StringExtensions.ContainsAny(instance, anyOf);
		}
		[TestMethod()]
		[Description("ContainsAny(Char[]) method when 'instance' is a null reference.")]
		[ExpectedException(typeof(ArgumentNullException))]
		public void StringExtensions_Unit_ContainsAny_InstanceIsNull() {
			String target = null;
			Char[] anyOf = { 'c' };
			StringExtensions.ContainsAny(target, anyOf);
		}

		[TestMethod()]
		[Description("FormatCurrentCulture(String, Object[]) method for the optimal path.")]
		public void StringExtensions_Unit_FormatCurrentCulture_Optimal() {
			String format = "This is a format:{0},{1}";
			Object[] args = new Object[] { 1, "test" };
			String actual = StringExtensions.FormatCurrentCulture(format, args);
			Assert.AreEqual("This is a format:1,test", actual);
		}
		[TestMethod()]
		[Description("FormatCurrentCulture(String, Object[]) method when format is a null reference.")]
		[ExpectedException(typeof(ArgumentNullException))]
		public void StringExtensions_Unit_FormatCurrentCulture_FormatNull() {
			String format = null;
			Object[] args = new Object[] { 1, "test" };
			StringExtensions.FormatCurrentCulture(format, args);
		}
		[TestMethod()]
		[Description("FormatCurrentCulture(String, Object[]) method when format is invalid.")]
		[ExpectedException(typeof(FormatException))]
		public void StringExtensions_Unit_FormatCurrentCulture_FormatInvalid() {
			String format = "This is a format:{yes},{huh?";
			Object[] args = new Object[] { 1, "test" };
			StringExtensions.FormatCurrentCulture(format, args);
		}
		[TestMethod()]
		[Description("FormatCurrentCulture(String, Object[]) method when format includes an argument that does not exist.")]
		[ExpectedException(typeof(FormatException))]
		public void StringExtensions_Unit_FormatCurrentCulture_FormatArgumentTooHigh() {
			String format = "This is a format:{0},{1},{2}";
			Object[] args = new Object[] { 1, "test" };
			StringExtensions.FormatCurrentCulture(format, args);
		}

		[TestMethod()]
		[Description("FormatCurrentUICulture(String, Object[]) method for the optimal path.")]
		public void StringExtensions_Unit_FormatCurrentUICulture_Optimal() {
			String format = "This is a format:{0},{1}";
			Object[] args = new Object[] { 1, "test" };
			String actual = StringExtensions.FormatCurrentUICulture(format, args);
			Assert.AreEqual("This is a format:1,test", actual);
		}
		[TestMethod()]
		[Description("FormatCurrentUICulture(String, Object[]) method when format is a null reference.")]
		[ExpectedException(typeof(ArgumentNullException))]
		public void StringExtensions_Unit_FormatCurrentUICulture_FormatNull() {
			String format = null;
			Object[] args = new Object[] { 1, "test" };
			StringExtensions.FormatCurrentUICulture(format, args);
		}
		[TestMethod()]
		[Description("FormatCurrentUICulture(String, Object[]) method when format is invalid.")]
		[ExpectedException(typeof(FormatException))]
		public void StringExtensions_Unit_FormatCurrentUICulture_FormatInvalid() {
			String format = "This is a format:{yes},{huh?";
			Object[] args = new Object[] { 1, "test" };
			StringExtensions.FormatCurrentUICulture(format, args);
		}
		[TestMethod()]
		[Description("FormatCurrentUICulture(String, Object[]) method when format includes an argument that does not exist.")]
		[ExpectedException(typeof(FormatException))]
		public void StringExtensions_Unit_FormatCurrentUICulture_FormatArgumentTooHigh() {
			String format = "This is a format:{0},{1},{2}";
			Object[] args = new Object[] { 1, "test" };
			StringExtensions.FormatCurrentUICulture(format, args);
		}

		[TestMethod()]
		[Description("FormatInvariant(String, Object[]) method for the optimal path.")]
		public void StringExtensions_Unit_FormatInvariant_Optimal() {
			String format = "This is a format:{0},{1}";
			Object[] args = new Object[] { 1, "test" };
			String actual = StringExtensions.FormatInvariant(format, args);
			Assert.AreEqual("This is a format:1,test", actual);
		}
		[TestMethod()]
		[Description("FormatInvariant(String, Object[]) method when format is a null reference.")]
		[ExpectedException(typeof(ArgumentNullException))]
		public void StringExtensions_Unit_FormatInvariant_FormatNull() {
			String format = null;
			Object[] args = new Object[] { 1, "test" };
			StringExtensions.FormatInvariant(format, args);
		}
		[TestMethod()]
		[Description("FormatInvariant(String, Object[]) method when format is invalid.")]
		[ExpectedException(typeof(FormatException))]
		public void StringExtensions_Unit_FormatInvariant_FormatInvalid() {
			String format = "This is a format:{yes},{huh?";
			Object[] args = new Object[] { 1, "test" };
			StringExtensions.FormatInvariant(format, args);
		}
		[TestMethod()]
		[Description("FormatInvariant(String, Object[]) method when format includes an argument that does not exist.")]
		[ExpectedException(typeof(FormatException))]
		public void StringExtensions_Unit_FormatInvariant_FormatArgumentTooHigh() {
			String format = "This is a format:{0},{1},{2}";
			Object[] args = new Object[] { 1, "test" };
			StringExtensions.FormatInvariant(format, args);
		}

		[TestMethod()]
		[Description("Join(String[], String) method for the optimal path.")]
		public void StringExtensions_Unit_Join1_Optimal() {
			String[] instance = new String[] { "one", "two", "three" };
			String separator = "|";
			String actual = StringExtensions.Join(instance, separator);
			Assert.AreEqual("one|two|three", actual);
		}
		[TestMethod()]
		[Description("Join(String[], String) method when instance is a null reference.")]
		[ExpectedException(typeof(ArgumentNullException))]
		public void StringExtensions_Unit_Join1_InstanceNull() {
			String[] instance = null;
			String separator = "|";
			StringExtensions.Join(instance, separator);
		}
		[TestMethod()]
		[Description("Join(String[], String) method when separator is a null reference.")]
		public void StringExtensions_Unit_Join1_SeparatorNull() {
			String[] instance = new String[] { "one", "two", "three" };
			String separator = null;
			String actual = StringExtensions.Join(instance, separator);
			Assert.AreEqual("onetwothree", actual);
		}

        [TestMethod()]
        [Description("Join(String[], Char) method for the optimal path.")]
        public void StringExtensions_Unit_Join2_Optimal() {
            String[] instance = new String[] { "one", "two", "three" };
            Char separator = '|';
            String actual = StringExtensions.Join(instance, separator);
            Assert.AreEqual("one|two|three", actual);
        }
        [TestMethod()]
        [Description("Join(String[], Char) method when instance is a null reference.")]
        [ExpectedException(typeof(ArgumentNullException))]
        public void StringExtensions_Unit_Join2_InstanceNull() {
            String[] instance = null;
            Char separator = '|';
            StringExtensions.Join(instance, separator);
        }

        [TestMethod()]
        [Description("Reverse(String) method optimal behavior.")]
        public void StringExtensions_Unit_Reverse_Optimal() {
            String target = "Reverse Me";
            String expected = "eM esreveR";
            String actual = StringExtensions.Reverse(target);
            Assert.AreEqual(expected, actual);
        }
        [TestMethod()]
        [Description("Reverse(String) method with null value.")]
        [ExpectedException(typeof(ArgumentNullException))]
        public void StringExtensions_Unit_Reverse_NullValue() {
            String target = null;
            String actual = StringExtensions.Reverse(target);
        }
        [TestMethod()]
        [Description("Reverse(String) method with empty string.")]
        public void StringExtensions_Unit_Reverse_EmptyString() {
            String target = String.Empty;
            String expected = String.Empty;
            String actual = StringExtensions.Reverse(target);
            Assert.AreEqual(expected, actual);
        }
	}
}
