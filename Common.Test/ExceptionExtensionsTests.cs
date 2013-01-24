using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Vizistata.TestTools;

namespace Vizistata {
	/// <summary>
	/// This is a test class for <see cref="T:ExceptionExtensions"/> and is intended to contain all <see cref="T:ExceptionExtensions"/> Unit Tests.
	///</summary>
	[TestClass()]
	public class ExceptionExtensionsTests {
		#region Test Class Implementation
		/// <summary>
		/// Describes the context under which the current test is running.
		/// </summary>
		private TestContext _testContextInstance;

		/// <summary>
		/// Initializes a new instance of the <see cref="T:ExceptionExtensionsTests"/> class.
		/// </summary>
		public ExceptionExtensionsTests() : base() { }

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
		[Description("CanBeHandledSafely(Exception) method for the optimal path.")]
		public void ExceptionExtensions_Unit_CanBeHandledSafely_Optimal() {
			Exception instance = new ApplicationException("This is a test.").AsThrown();
			Boolean expected = true;

			Boolean actual = ExceptionExtensions.CanBeHandledSafely(instance);
			Assert.AreEqual(expected, actual);
		}
		[TestMethod()]
		[Description("CanBeHandledSafely(Exception) method when 'exception' is unsafe.")]
		public void ExceptionExtensions_Unit_CanBeHandledSafely_UnsafeExceptions() {
			const BindingFlags AllInstanceConstructors = BindingFlags.CreateInstance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
			Exception[] unsafeExceptions = new Exception[] {
				new OutOfMemoryException(),
				new AppDomainUnloadedException(),
				new BadImageFormatException(),
				new CannotUnloadAppDomainException(),
				new InvalidProgramException(),
				(ThreadAbortException)typeof(ThreadAbortException).GetConstructor(AllInstanceConstructors, null, new Type[0], null).Invoke(null)
			};

			Boolean expected = false;
			foreach (Exception unsafeException in unsafeExceptions) {
				Boolean actual = ExceptionExtensions.CanBeHandledSafely(unsafeException);
				Assert.AreEqual(expected, actual);
			}
		}
		[TestMethod()]
		[Description("CanBeHandledSafely(Exception) method when 'exception' is a null reference.")]
		[ExpectedException(typeof(ArgumentNullException))]
		public void ExceptionExtensions_Unit_CanBeHandledSafely_ExceptionIsNull() {
			Exception instance = null;
			ExceptionExtensions.CanBeHandledSafely(instance);
		}

		[TestMethod()]
		[Description("ExtractMessages(Exception) method for the optimal path.")]
		public void ExceptionExtensions_Unit_ExtractMessages_Optimal() {
			Exception exception = new ApplicationException("This is a test.").AsThrown();

			IEnumerable<String> messages = ExceptionExtensions.ExtractMessages(exception);
			Assert.AreEqual(1, messages.Count());
			Assert.AreEqual(exception.Message, messages.Single());
		}
		[TestMethod()]
		[Description("ExtractMessages(Exception) method when 'exception' has inner exceptions.")]
		public void ExceptionExtensions_Unit_ExtractMessages_NestedExceptions() {
			Exception exception = new ApplicationException("This is a test.", new InvalidOperationException("This is an inner test.", new NotSupportedException("This is the inner-most test.").AsThrown()).AsThrown()).AsThrown();

			IEnumerable<String> messages = ExceptionExtensions.ExtractMessages(exception);
			Assert.AreEqual(3, messages.Count());
			Assert.AreEqual(exception.Message, messages.ElementAt(0));
			Assert.AreEqual(exception.InnerException.Message, messages.ElementAt(1));
			Assert.AreEqual(exception.InnerException.InnerException.Message, messages.ElementAt(2));
		}
		[TestMethod()]
		[Description("ExtractMessages(Exception) method when 'exception' is a null reference.")]
		[ExpectedException(typeof(ArgumentNullException))]
		public void ExceptionExtensions_Unit_ExtractMessages_ExceptionIsNull() {
			Exception exception = null;

			ExceptionExtensions.ExtractMessages(exception);
		}
	}
}