using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Vizistata {
	/// <summary>
	/// This is a test class for <see cref="T:ReusableAttribute"/> and is intended to contain all <see cref="T:ReusableAttribute"/> Unit Tests.
	///</summary>
	[TestClass()]
	public class ReusableAttributeTests {
		#region Test Class Implementation
		/// <summary>
		/// Describes the context under which the current test is running.
		/// </summary>
		private TestContext _testContextInstance;

		/// <summary>
		/// Initializes a new instance of the <see cref="T:ReusableAttributeTests"/> class.
		/// </summary>
		public ReusableAttributeTests() : base() { }

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
		[Description(".ctor(Boolean) constructor when the value is true.")]
		public void ReusableAttribute_Unit_Constructor_IsReusableIsTrue() {
			Boolean isReusable = true;
			new ReusableAttribute(isReusable);
		}
		[TestMethod()]
		[Description(".ctor(Boolean) constructor when the value is false.")]
		public void ReusableAttribute_Unit_Constructor_IsReusableIsFalse() {
			Boolean isReusable = false;
			new ReusableAttribute(isReusable);
		}

	// Property Tests
		[TestMethod()]
		[Description("Approver property for the optimal path.")]
		public void ReusableAttribute_Unit_Approver_Optimal() {
			Boolean isReusable = true;
			ReusableAttribute target = new ReusableAttribute(isReusable);
			String value = "Chad Greer";

			target.Approver = value;
			String actual = target.Approver;
			Assert.AreEqual(value, actual);
		}
		[TestMethod()]
		[Description("Comments property for the optimal path.")]
		public void ReusableAttribute_Unit_Comments_Optimal() {
			Boolean isReusable = true;
			ReusableAttribute target = new ReusableAttribute(isReusable);
			String value = "This is a test.";

			target.Comments = value;
			String actual = target.Comments;
			Assert.AreEqual(value, actual);
		}
		[TestMethod()]
		[Description("IsReusable property for the optimal path.")]
		public void ReusableAttribute_Unit_IsReusable_Optimal() {
			Boolean isReusable = true;
			ReusableAttribute target = new ReusableAttribute(isReusable);

			Boolean actual = target.IsReusable;
			Assert.AreEqual(isReusable, actual);
		}
	}
}