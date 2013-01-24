using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Vizistata.Mocks;

namespace Vizistata.Reflection {
	/// <summary>
	/// This is a test class for <see cref="T:DisposableExtensions"/> and is intended to contain all <see cref="T:DisposableExtensions"/> Unit Tests.
	///</summary>
	[TestClass()]
	public class DisposableExtensionsTests {
		#region Test Class Implementation
		/// <summary>
		/// Describes the context under which the current test is running.
		/// </summary>
		private TestContext _testContextInstance;

		/// <summary>
		/// Initializes a new instance of the <see cref="T:DisposableExtensionsTests"/> class.
		/// </summary>
		public DisposableExtensionsTests() : base() { }

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
		[Description("InvokeDispose(IDisposable) method for the optimal path.")]
		public void DisposableExtensions_Unit_InvokeDispose_Optimal() {
			Object nonDisposableObject = new Object();
			DisposableBase disposableObject = new MockDisposableBase();
			IEnumerable<DisposableBase> disposableCollection = new List<DisposableBase>() {
				new MockDisposableBase(),
				new MockDisposableBase(),
				new MockDisposableBase()
			};
			IntPtr intPtr = new IntPtr(12345);
			try {
				using (MockReflectionDisposable disposable = new MockReflectionDisposable(nonDisposableObject, disposableObject, disposableCollection, intPtr)) {
					DisposableExtensions.InvokeDispose(disposable);

					Assert.IsNull(disposable.DisposableObject);
					Assert.IsTrue(disposableObject.IsDisposed);
					Assert.IsTrue(disposable.DisposableCollection.All(item => item.IsDisposed));
					Assert.AreEqual(IntPtr.Zero, disposable.IntPtr);
				}

				using (MockReflectionDisposable disposable = new MockReflectionDisposable(null, null, null, IntPtr.Zero)) {
					DisposableExtensions.InvokeDispose(disposable);
				}
			}
			finally {
				if (intPtr != IntPtr.Zero) {
					intPtr = IntPtr.Zero;
				}
			}
		}
		[TestMethod()]
		[Description("InvokeDispose(IDisposable) method when 'disposable' is a null reference.")]
		[ExpectedException(typeof(ArgumentNullException))]
		public void DisposableExtensions_Unit_InvokeDispose_DisposableIsNull() {
			DisposableExtensions.InvokeDispose(null);
		}

		[TestMethod()]
		[Description("InvokeFinalize(IDisposable) method for the optimal path.")]
		public void DisposableExtensions_Unit_InvokeFinalize_Optimal() {
			Object nonDisposableObject = new Object();
			DisposableBase disposableObject = new MockDisposableBase();
			IEnumerable<DisposableBase> disposableCollection = new List<DisposableBase>() {
				new MockDisposableBase(),
				new MockDisposableBase(),
				new MockDisposableBase()
			};
			IntPtr intPtr = new IntPtr(12345);
			try {
				using (MockReflectionDisposable disposable = new MockReflectionDisposable(nonDisposableObject, disposableObject, disposableCollection, intPtr)) {
					DisposableExtensions.InvokeFinalize(disposable);

					Assert.IsTrue(!disposable.DisposableObject.IsDisposed);
					Assert.IsTrue(disposable.DisposableCollection.All(item => !item.IsDisposed));
					Assert.AreEqual(IntPtr.Zero, disposable.IntPtr);
				}

				using (MockReflectionDisposable disposable = new MockReflectionDisposable(null, null, null, IntPtr.Zero)) {
					DisposableExtensions.InvokeDispose(disposable);
				}
			}
			finally {
				if (intPtr != IntPtr.Zero) {
					intPtr = IntPtr.Zero;
				}
			}
		}
		[TestMethod()]
		[Description("InvokeFinalize(IDisposable) method when 'disposable' is a null reference.")]
		[ExpectedException(typeof(ArgumentNullException))]
		public void DisposableExtensions_Unit_InvokeFinalize_DisposableIsNull() {
			DisposableExtensions.InvokeFinalize(null);
		}
	}
}