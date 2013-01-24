using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Vizistata {
	/// <summary>
	/// This is a test class for <see cref="T:ArrayExtensions"/> and is intended to contain all <see cref="T:ArrayExtensions"/> Unit Tests.
	///</summary>
	[TestClass()]
	public class ArrayExtensionsTests {
		#region Test Class Implementation
		/// <summary>
		/// Describes the context under which the current test is running.
		/// </summary>
		private TestContext _testContextInstance;

		/// <summary>
		/// Initializes a new instance of the <see cref="T:ArrayExtensionsTests"/> class.
		/// </summary>
		public ArrayExtensionsTests() : base() { }

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
		[Description("GetElementOrDefaultAt(T[], Int32) method for the optimal path.")]
		public void ArrayExtensions_Unit_GetElementOrDefaultAt_Optimal() {
			String[] instance = new String[] { "Zero", "One", "Two", "Three" };
			Int32 index = 0;
			String expected = instance[index];

			String actual = ArrayExtensions.GetElementOrDefaultAt(instance, index);
			Assert.AreEqual(expected, actual);
		}
		[TestMethod()]
		[Description("GetElementOrDefaultAt(T[], Int32) method when 'instance' is a null reference.")]
		[ExpectedException(typeof(ArgumentNullException))]
		public void ArrayExtensions_Unit_GetElementOrDefaultAt_InstanceNull() {
			ArrayExtensions.GetElementOrDefaultAt<String>(null, 0);
		}
		[TestMethod()]
		[Description("GetElementOrDefaultAt(T[], Int32) method when 'index' is less than 0.")]
		public void ArrayExtensions_Unit_GetElementOrDefaultAt_IndexLessThan0() {
			String[] instance = new String[] { "Zero", "One", "Two", "Three" };
			Int32 index = -1;
			String expected = null;

			String actual = ArrayExtensions.GetElementOrDefaultAt(instance, index);
			Assert.AreEqual(expected, actual);
		}
		[TestMethod()]
		[Description("GetElementOrDefaultAt(T[], Int32) method when 'index' is greater than the length of 'instance'.")]
		public void ArrayExtensions_Unit_GetElementOrDefaultAt_IndexGreaterThanLength() {
			String[] instance = new String[] { "Zero", "One", "Two", "Three" };
			Int32 index = instance.Length;
			String expected = null;

			String actual = ArrayExtensions.GetElementOrDefaultAt(instance, index);
			Assert.AreEqual(expected, actual);
		}
	}
}
