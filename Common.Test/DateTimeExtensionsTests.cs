using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Vizistata {
	/// <summary>
	/// This is a test class for <see cref="T:DateTimeExtensions"/> and is intended to contain all <see cref="T:DateTimeExtensions"/> Unit Tests.
	///</summary>
	[TestClass()]
	public class DateTimeExtensionsTests {
		#region Test Class Implementation
		/// <summary>
		/// Describes the context under which the current test is running.
		/// </summary>
		private TestContext _testContextInstance;

		/// <summary>
		/// Initializes a new instance of the <see cref="T:DateTimeExtensionsTests"/> class.
		/// </summary>
		public DateTimeExtensionsTests() : base() { }

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

	// Methods Tests
		[TestMethod()]
		[Description("IsValidForSqlServer(DateTime) method when 'dateTime' is valid.")]
		public void DateTimeExtensions_Unit_IsValidForSqlServer_DateTimeIsValid() {
			DateTime dateTime = new DateTime(1753, 1, 1);
			
			Boolean actual = dateTime.IsValidForSqlServer();
			Assert.AreEqual(true, actual);
		}
		[TestMethod()]
		[Description("IsValidForSqlServer(DateTime) method when 'dateTime' is not valid.")]
		public void DateTimeExtensions_Unit_IsValidForSqlServer_DateTimeIsNotValid() {
			DateTime dateTime = new DateTime(1753, 1, 1).AddTicks(-1);

			Boolean actual = dateTime.IsValidForSqlServer();
			Assert.AreEqual(false, actual);
		}
	}
}