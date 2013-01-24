using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using CultureInfo = System.Globalization.CultureInfo;

namespace Vizistata {
	/// <summary>
	/// This is a test class for <see cref="T:DoubleExtensions"/> and is intended to contain all <see cref="T:DoubleExtensions"/> Unit Tests.
	///</summary>
	[TestClass()]
	public class DoubleExtensionsTests {
		#region Test Class Implementation
		/// <summary>
		/// Describes the context under which the current test is running.
		/// </summary>
		private TestContext _testContextInstance;

		/// <summary>
		/// Initializes a new instance of the <see cref="T:DoubleExtensionsTests"/> class.
		/// </summary>
		public DoubleExtensionsTests() : base() { }

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
		[Description("FormatCurrentCulture(Double) method for the optimal path.")]
		public void DoubleExtensions_Unit_FormatCurrentCulture1_Optimal() {
			Double value = 1234567;

			String expected = value.ToString(CultureInfo.CurrentCulture);
			String actual = DoubleExtensions.FormatCurrentCulture(value);

			Assert.AreEqual(expected, actual);
		}

		[TestMethod()]
		[Description("FormatCurrentCulture(Double, String) method for the optimal path.")]
		public void DoubleExtensions_Unit_FormatCurrentCulture2_Optimal() {
			Double value = 1234567;
			String format = "c";

			String expected = value.ToString(format, CultureInfo.CurrentCulture);
			String actual = DoubleExtensions.FormatCurrentCulture(value, format);

			Assert.AreEqual(expected, actual);
		}

		[TestMethod()]
		[Description("FormatCurrentCulture(Double?) method for the optimal path.")]
		public void DoubleExtensions_Unit_FormatCurrentCulture3_Optimal() {
			Double? value = 1234567;

			String expected = value.Value.ToString(CultureInfo.CurrentCulture);
			String actual = DoubleExtensions.FormatCurrentCulture(value);

			Assert.AreEqual(expected, actual);
		}
		[TestMethod()]
		[Description("FormatCurrentCulture(Double?) method when 'value' is a null reference.")]
		public void DoubleExtensions_Unit_FormatCurrentCulture3_ValueIsNull() {
			Double? value = null;

			String expected = String.Empty;
			String actual = DoubleExtensions.FormatCurrentCulture(value);

			Assert.AreEqual(expected, actual);
		}

		[TestMethod()]
		[Description("FormatCurrentCulture(Double?, String) method for the optimal path.")]
		public void DoubleExtensions_Unit_FormatCurrentCulture4_Optimal() {
			Double? value = 1234567;
			String format = "c";

			String expected = value.Value.ToString(format, CultureInfo.CurrentCulture);
			String actual = DoubleExtensions.FormatCurrentCulture(value, format);

			Assert.AreEqual(expected, actual);
		}
		[TestMethod()]
		[Description("FormatCurrentCulture(Double?, String) method when 'value' is a null reference.")]
		public void DoubleExtensions_Unit_FormatCurrentCulture4_ValueIsNull() {
			Double? value = null;
			String format = "c";

			String expected = String.Empty;
			String actual = DoubleExtensions.FormatCurrentCulture(value, format);

			Assert.AreEqual(expected, actual);
		}

		[TestMethod()]
		[Description("FormatInvariant(Double) method for the optimal path.")]
		public void DoubleExtensions_Unit_FormatInvariant1_Optimal() {
			Double value = 1234567;

			String expected = value.ToString(CultureInfo.InvariantCulture);
			String actual = DoubleExtensions.FormatInvariant(value);

			Assert.AreEqual(expected, actual);
		}

		[TestMethod()]
		[Description("FormatInvariant(Double, String) method for the optimal path.")]
		public void DoubleExtensions_Unit_FormatInvariant2_Optimal() {
			Double value = 1234567;
			String format = "c";

			String expected = value.ToString(format, CultureInfo.InvariantCulture);
			String actual = DoubleExtensions.FormatInvariant(value, format);

			Assert.AreEqual(expected, actual);
		}

		[TestMethod()]
		[Description("FormatInvariant(Double?) method for the optimal path.")]
		public void DoubleExtensions_Unit_FormatInvariant3_Optimal() {
			Double? value = 1234567;

			String expected = value.Value.ToString(CultureInfo.InvariantCulture);
			String actual = DoubleExtensions.FormatInvariant(value);

			Assert.AreEqual(expected, actual);
		}
		[TestMethod()]
		[Description("FormatInvariant(Double?) method when 'value' is a null reference.")]
		public void DoubleExtensions_Unit_FormatInvariant3_ValueIsNull() {
			Double? value = null;

			String expected = String.Empty;
			String actual = DoubleExtensions.FormatInvariant(value);

			Assert.AreEqual(expected, actual);
		}

		[TestMethod()]
		[Description("FormatInvariant(Double?, String) method for the optimal path.")]
		public void DoubleExtensions_Unit_FormatInvariant4_Optimal() {
			Double? value = 1234567;
			String format = "c";

			String expected = value.Value.ToString(format, CultureInfo.InvariantCulture);
			String actual = DoubleExtensions.FormatInvariant(value, format);

			Assert.AreEqual(expected, actual);
		}
		[TestMethod()]
		[Description("FormatInvariant(Double?, String) method when 'value' is a null reference.")]
		public void DoubleExtensions_Unit_FormatInvariant4_ValueIsNull() {
			Double? value = null;
			String format = "c";

			String expected = String.Empty;
			String actual = DoubleExtensions.FormatInvariant(value, format);

			Assert.AreEqual(expected, actual);
		}
	}
}