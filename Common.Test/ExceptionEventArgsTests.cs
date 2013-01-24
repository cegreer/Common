using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Vizistata.TestTools;

namespace Vizistata {
	/// <summary>
	/// This is a test class for <see cref="T:ExceptionEventArgs"/> and is intended to contain all <see cref="T:ExceptionEventArgs"/> Unit Tests.
	///</summary>
	[TestClass()]
	public class ExceptionEventArgsTests {
		#region Test Class Implementation
		/// <summary>
		/// Describes the context under which the current test is running.
		/// </summary>
		private TestContext _testContextInstance;

		/// <summary>
		/// Initializes a new instance of the <see cref="T:ExceptionEventArgsTests"/> class.
		/// </summary>
		public ExceptionEventArgsTests() : base() { }

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
		[Description(".ctor(Exception) constructor for the optimal path.")]
		public void ExceptionEventArgs_Unit_Constructor1_Optimal() {
			Exception exception = new ApplicationException("This is a test.").AsThrown();
			new ExceptionEventArgs(exception);
		}
		[TestMethod()]
		[Description(".ctor(Exception) constructor when 'exception' is a null reference.")]
		[ExpectedException(typeof(ArgumentNullException))]
		public void ExceptionEventArgs_Unit_Constructor1_ExceptionIsNull() {
			Exception exception = null;
			new ExceptionEventArgs(exception);
		}

		[TestMethod()]
		[Description(".ctor(Exception) constructor for the optimal path when 'bubbleException' is true.")]
		public void ExceptionEventArgs_Unit_Constructor2_OptimalAndBubbleExceptionIsTrue() {
			Exception exception = new ApplicationException("This is a test.").AsThrown();
			Boolean bubbleException = true;
			new ExceptionEventArgs(exception, bubbleException);
		}
		[TestMethod()]
		[Description(".ctor(Exception) constructor for the optimal path when 'bubbleException' is false.")]
		public void ExceptionEventArgs_Unit_Constructor2_OptimalAndBubbleExceptionIsFalse() {
			Exception exception = new ApplicationException("This is a test.").AsThrown();
			Boolean bubbleException = false;
			new ExceptionEventArgs(exception, bubbleException);
		}
		[TestMethod()]
		[Description(".ctor(Exception) constructor when 'exception' is a null reference.")]
		[ExpectedException(typeof(ArgumentNullException))]
		public void ExceptionEventArgs_Unit_Constructor2_ExceptionIsNull() {
			Exception exception = null;
			Boolean bubbleException = true;
			new ExceptionEventArgs(exception, bubbleException);
		}

	// Property Tests
		[TestMethod()]
		[Description("BubbleException property when 'value' is true.")]
		public void ExceptionEventArgs_Unit_BubbleException_True() {
			Exception exception = new ApplicationException("This is a test.").AsThrown();
			ExceptionEventArgs target = new ExceptionEventArgs(exception);
			Boolean value = true;

			target.BubbleException = value;
			Assert.AreEqual(value, target.BubbleException);
		}
		[TestMethod()]
		[Description("BubbleException property when 'value' is false.")]
		public void ExceptionEventArgs_Unit_BubbleException_False() {
			Exception exception = new ApplicationException("This is a test.").AsThrown();
			ExceptionEventArgs target = new ExceptionEventArgs(exception);
			Boolean value = false;

			target.BubbleException = value;
			Assert.AreEqual(value, target.BubbleException);
		}

		[TestMethod()]
		[Description("Exception property for the optimal path.")]
		public void ExceptionEventArgs_Unit_Exception_Optimal() {
			Exception exception = new ApplicationException("This is a test.").AsThrown();
			ExceptionEventArgs target = new ExceptionEventArgs(exception);
			Assert.AreEqual(exception, target.Exception);
		}

	// Serialization Tests
		[TestMethod()]
		[Description("Serializability of the class for the optimal path.")]
		public void ExceptionEventArgs_Integration_Serialization_Optimal() {
			Exception exception = new ApplicationException("This is a test.").AsThrown();
			Boolean bubbleException = true;
			ExceptionEventArgs original = new ExceptionEventArgs(exception, bubbleException);

			ExceptionEventArgs clone = original.SerializeBinary();
			Assert.AreNotSame(original, clone);
			Assert.AreEqual(original.BubbleException, clone.BubbleException);
			Assert.AreEqual(original.Exception.GetType(), clone.Exception.GetType());
			Assert.AreEqual(original.Exception.Message, clone.Exception.Message);
			Assert.AreEqual(original.Exception.StackTrace, clone.Exception.StackTrace);
		}
	}
}