using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using CultureInfo = System.Globalization.CultureInfo;

namespace Vizistata {
	/// <summary>
	/// This is a test class for <see cref="T:Int32Extensions"/> and is intended to contain all <see cref="T:Int32Extensions"/> Unit Tests.
	///</summary>
	[TestClass()]
	public class Int32ExtensionsTests {
		#region Test Class Implementation
		/// <summary>
		/// Describes the context under which the current test is running.
		/// </summary>
		private TestContext _testContextInstance;

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Int32ExtensionsTests"/> class.
		/// </summary>
		public Int32ExtensionsTests() : base() { }

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
		[Description("FormatCurrentCulture(Int32) method for the optimal path.")]
		public void Int32Extensions_Unit_FormatCurrentCulture1_Optimal() {
			Int32 value = 1234567;

			String expected = value.ToString(CultureInfo.CurrentCulture);
			String actual = Int32Extensions.FormatCurrentCulture(value);

			Assert.AreEqual(expected, actual);
		}

		[TestMethod()]
		[Description("FormatCurrentCulture(Int32, String) method for the optimal path.")]
		public void Int32Extensions_Unit_FormatCurrentCulture2_Optimal() {
			Int32 value = 1234567;
			String format = "c";

			String expected = value.ToString(format, CultureInfo.CurrentCulture);
			String actual = Int32Extensions.FormatCurrentCulture(value, format);

			Assert.AreEqual(expected, actual);
		}

		[TestMethod()]
		[Description("FormatCurrentCulture(Int32?) method for the optimal path.")]
		public void Int32Extensions_Unit_FormatCurrentCulture3_Optimal() {
			Int32? value = 1234567;

			String expected = value.Value.ToString(CultureInfo.CurrentCulture);
			String actual = Int32Extensions.FormatCurrentCulture(value);

			Assert.AreEqual(expected, actual);
		}
		[TestMethod()]
		[Description("FormatCurrentCulture(Int32?) method when 'value' is a null reference.")]
		public void Int32Extensions_Unit_FormatCurrentCulture3_ValueIsNull() {
			Int32? value = null;

			String expected = String.Empty;
			String actual = Int32Extensions.FormatCurrentCulture(value);

			Assert.AreEqual(expected, actual);
		}

		[TestMethod()]
		[Description("FormatCurrentCulture(Int32?, String) method for the optimal path.")]
		public void Int32Extensions_Unit_FormatCurrentCulture4_Optimal() {
			Int32? value = 1234567;
			String format = "c";

			String expected = value.Value.ToString(format, CultureInfo.CurrentCulture);
			String actual = Int32Extensions.FormatCurrentCulture(value, format);

			Assert.AreEqual(expected, actual);
		}
		[TestMethod()]
		[Description("FormatCurrentCulture(Int32?, String) method when 'value' is a null reference.")]
		public void Int32Extensions_Unit_FormatCurrentCulture4_ValueIsNull() {
			Int32? value = null;
			String format = "c";

			String expected = String.Empty;
			String actual = Int32Extensions.FormatCurrentCulture(value, format);

			Assert.AreEqual(expected, actual);
		}

		[TestMethod()]
		[Description("FormatInvariant(Int32) method for the optimal path.")]
		public void Int32Extensions_Unit_FormatInvariant1_Optimal() {
			Int32 value = 1234567;

			String expected = value.ToString(CultureInfo.InvariantCulture);
			String actual = Int32Extensions.FormatInvariant(value);

			Assert.AreEqual(expected, actual);
		}

		[TestMethod()]
		[Description("FormatInvariant(Int32, String) method for the optimal path.")]
		public void Int32Extensions_Unit_FormatInvariant2_Optimal() {
			Int32 value = 1234567;
			String format = "c";

			String expected = value.ToString(format, CultureInfo.InvariantCulture);
			String actual = Int32Extensions.FormatInvariant(value, format);

			Assert.AreEqual(expected, actual);
		}

		[TestMethod()]
		[Description("FormatInvariant(Int32?) method for the optimal path.")]
		public void Int32Extensions_Unit_FormatInvariant3_Optimal() {
			Int32? value = 1234567;

			String expected = value.Value.ToString(CultureInfo.InvariantCulture);
			String actual = Int32Extensions.FormatInvariant(value);

			Assert.AreEqual(expected, actual);
		}
		[TestMethod()]
		[Description("FormatInvariant(Int32?) method when 'value' is a null reference.")]
		public void Int32Extensions_Unit_FormatInvariant3_ValueIsNull() {
			Int32? value = null;

			String expected = String.Empty;
			String actual = Int32Extensions.FormatInvariant(value);

			Assert.AreEqual(expected, actual);
		}

		[TestMethod()]
		[Description("FormatInvariant(Int32?, String) method for the optimal path.")]
		public void Int32Extensions_Unit_FormatInvariant4_Optimal() {
			Int32? value = 1234567;
			String format = "c";

			String expected = value.Value.ToString(format, CultureInfo.InvariantCulture);
			String actual = Int32Extensions.FormatInvariant(value, format);

			Assert.AreEqual(expected, actual);
		}
		[TestMethod()]
		[Description("FormatInvariant(Int32?, String) method when 'value' is a null reference.")]
		public void Int32Extensions_Unit_FormatInvariant4_ValueIsNull() {
			Int32? value = null;
			String format = "c";

			String expected = String.Empty;
			String actual = Int32Extensions.FormatInvariant(value, format);

			Assert.AreEqual(expected, actual);
		}
	}
}