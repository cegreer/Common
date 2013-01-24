using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Vizistata.Collections {
	/// <summary>
	/// This is a test class for <see cref="T:ReverseComparer&lt;T&gt;"/> and is intended to contain all <see cref="T:ReverseComparer&lt;T&gt;"/> Unit Tests.
	///</summary>
	[TestClass()]
	public class ReverseComparerTests {
		#region Test Class Implementation
		/// <summary>
		/// Describes the context under which the current test is running.
		/// </summary>
		private TestContext _testContextInstance;

		/// <summary>
		/// Initializes a new instance of the <see cref="T:ReverseComparerTests"/> class.
		/// </summary>
		public ReverseComparerTests() : base() { }

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

	// Constructor Tests
		[TestMethod()]
		[Description(".ctor(IComparer<T>) constructor for the optimal path.")]
		public void ReverseComparer_Unit_Constructor_Optimal() {
			IComparer<String> comparer = StringComparer.OrdinalIgnoreCase;
			new ReverseComparer<String>(comparer);
		}
		[TestMethod()]
		[Description(".ctor(IComparer<T>) constructor when 'comparer' is a null reference.")]
		[ExpectedException(typeof(ArgumentNullException))]
		public void ReverseComparer_Unit_Constructor_ComparerIsNull() {
			IComparer<String> comparer = null;
			new ReverseComparer<String>(comparer);
		}

	// Property Tests
		[TestMethod()]
		[Description("Default property for the optimal path.")]
		public void ReverseComparer_Unit_Default_Optimal() {
			IComparer<String> actual = ReverseComparer<String>.Default;
			Assert.IsNotNull(actual);
		}

	// Methods
		[TestMethod()]
		[Description("Compare(T, T) method when 'x' equals 'y'.")]
		public void ReverseComparer_Unit_CompareTo_XEqualsY() {
			IComparer<String> comparer = StringComparer.OrdinalIgnoreCase;
			ReverseComparer<String> target = new ReverseComparer<String>(comparer);
			String x = "a";
			String y = "A";

			Int32 actual = target.Compare(x, y);
			Assert.AreEqual(0, actual);
		}
		[TestMethod()]
		[Description("Compare(T, T) method when 'x' is greater than 'y'.")]
		public void ReverseComparer_Unit_CompareTo_XIsGreaterThanY() {
			IComparer<String> comparer = StringComparer.OrdinalIgnoreCase;
			ReverseComparer<String> target = new ReverseComparer<String>(comparer);
			String x = "b";
			String y = "a";

			Int32 actual = target.Compare(x, y);
			Assert.IsTrue(actual < 0);
		}
		[TestMethod()]
		[Description("Compare(T, T) method when 'x' is less than 'y'.")]
		public void ReverseComparer_Unit_CompareTo_XIsLessThanY() {
			IComparer<String> comparer = StringComparer.OrdinalIgnoreCase;
			ReverseComparer<String> target = new ReverseComparer<String>(comparer);
			String x = "a";
			String y = "b";

			Int32 actual = target.Compare(x, y);
			Assert.IsTrue(actual > 0);
		}
		[TestMethod()]
		[Description("Compare(T, T) method when 'x' is a null reference.")]
		public void ReverseComparer_Unit_CompareTo_XIsNull() {
			IComparer<String> comparer = StringComparer.OrdinalIgnoreCase;
			ReverseComparer<String> target = new ReverseComparer<String>(comparer);
			String x = null;
			String y = "b";

			Int32 actual = target.Compare(x, y);
			Assert.IsTrue(actual > 0);
		}
		[TestMethod()]
		[Description("Compare(T, T) method when 'y' is a null reference.")]
		public void ReverseComparer_Unit_CompareTo_YIsNull() {
			IComparer<String> comparer = StringComparer.OrdinalIgnoreCase;
			ReverseComparer<String> target = new ReverseComparer<String>(comparer);
			String x = "a";
			String y = null;

			Int32 actual = target.Compare(x, y);
			Assert.IsTrue(actual < 0);
		}
		[TestMethod()]
		[Description("Compare(T, T) method when 'x' and 'y' are null references.")]
		public void ReverseComparer_Unit_CompareTo_XIsNullAndYIsNull() {
			IComparer<String> comparer = StringComparer.OrdinalIgnoreCase;
			ReverseComparer<String> target = new ReverseComparer<String>(comparer);
			String x = null;
			String y = null;

			Int32 actual = target.Compare(x, y);
			Assert.AreEqual(0, actual);
		}
	}
}