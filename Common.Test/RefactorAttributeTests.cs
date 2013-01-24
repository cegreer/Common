using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Vizistata {
	/// <summary>
	/// This is a test class for <see cref="T:RefactorAttribute"/> and is intended to contain all <see cref="T:RefactorAttribute"/> Unit Tests.
	///</summary>
	[TestClass()]
	public class RefactorAttributeTests {
		#region Test Class Implementation
		/// <summary>
		/// Describes the context under which the current test is running.
		/// </summary>
		private TestContext _testContextInstance;

		/// <summary>
		/// Initializes a new instance of the <see cref="T:RefactorAttributeTests"/> class.
		/// </summary>
		public RefactorAttributeTests() : base() { }

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
		[Description(".ctor(Boolean) constructor for the optimal path.")]
		public void RefactorAttribute_Unit_Constructor_Optimal() {
			String comments = "This is a test.";
			new RefactorAttribute(comments);
		}
		[TestMethod()]
		[Description(".ctor(Boolean) constructor when 'comments' is a null reference.")]
		public void RefactorAttribute_Unit_Constructor_CommentsIsNull() {
			String comments = null;
			new RefactorAttribute(comments);
		}

	// Property Tests
		[TestMethod()]
		[Description("Comments property for the optimal path.")]
		public void RefactorAttribute_Unit_Comments_Optimal() {
			String comments = "This is a test.";
			RefactorAttribute target = new RefactorAttribute(comments);

			String actual = target.Comments;
			Assert.AreEqual(comments, actual);
		}
	}
}