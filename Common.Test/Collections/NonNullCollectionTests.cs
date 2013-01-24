using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Vizistata.Collections {
	/// <summary>
	/// This is a test class for <see cref="T:NonNullCollection&lt;T&gt;"/> and is intended to contain all <see cref="T:NonNullCollection&lt;T&gt;"/> Unit Tests.
	///</summary>
	[TestClass()]
	public class NonNullCollectionTests {
		#region Test Class Implementation
		/// <summary>
		/// Describes the context under which the current test is running.
		/// </summary>
		private TestContext _testContextInstance;

		/// <summary>
		/// Initializes a new instance of the <see cref="T:NonNullCollectionTests"/> class.
		/// </summary>
		public NonNullCollectionTests() : base() { }

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
		[Description(".ctor() constructor for the optimal path.")]
		public void NonNullCollection_Unit_Constructor1_Optimal() {
			new NonNullCollection<String>();
		}

		[TestMethod()]
		[Description(".ctor(IList<T>) constructor for the optimal path.")]
		public void NonNullCollection_Unit_Constructor2_Optimal() {
			IList<String> list = new List<String>() { "One", "Two", "Three" };
			new NonNullCollection<String>(list);
		}
		[TestMethod()]
		[Description(".ctor(IList<T>) constructor when 'list' is a null reference.")]
		[ExpectedException(typeof(ArgumentNullException))]
		public void NonNullCollection_Unit_Constructor2_ListIsNull() {
			IList<String> list = null;
			new NonNullCollection<String>(list);
		}
		[TestMethod()]
		[Description(".ctor(IList<T>) constructor when 'list' contains an element that is a null reference.")]
		[ExpectedException(typeof(ArgumentException))]
		public void NonNullCollection_Unit_Constructor2_ListHasNull() {
			IList<String> list = new List<String>() { "One", null, "Three" };
			new NonNullCollection<String>(list);
		}

	// Property Tests
		[TestMethod()]
		[Description("this[Int32] property for the optimal path.")]
		public void NonNullCollection_Unit_Indexer_Optimal() {
			IList<String> list = new List<String>() { "One", "Two", "Three" };
			NonNullCollection<String> target = new NonNullCollection<String>(list);
			String value = "Test";
			
			target[0] = value;
			Assert.AreEqual(value, target[0]);
		}
		[TestMethod()]
		[Description("this[Int32] property when 'value' is a null reference.")]
		[ExpectedException(typeof(ArgumentNullException))]
		public void NonNullCollection_Unit_Indexer_ValueIsNull() {
			IList<String> list = new List<String>() { "One", "Two", "Three" };
			NonNullCollection<String> target = new NonNullCollection<String>(list);
			String value = null;

			target[0] = value;
		}

	// Method Tests
		[TestMethod()]
		[Description("Add(T) method for the optimal path.")]
		public void NonNullCollection_Unit_Add_Optimal() {
			NonNullCollection<String> target = new NonNullCollection<String>();
			String item = "Test";

			target.Add(item);
			CollectionAssert.Contains(target, item);
		}
		[TestMethod()]
		[Description("Add(T) method when 'item' is a null reference.")]
		[ExpectedException(typeof(ArgumentNullException))]
		public void NonNullCollection_Unit_Add_ItemIsNull() {
			NonNullCollection<String> target = new NonNullCollection<String>();
			String item = null;

			target.Add(item);
		}

		[TestMethod()]
		[Description("Insert(Int32, T) method for the optimal path.")]
		public void NonNullCollection_Unit_Insert_Optimal() {
			NonNullCollection<String> target = new NonNullCollection<String>();
			Int32 index = 0;
			String item = "Test";

			target.Insert(index, item);
			CollectionAssert.Contains(target, item);
		}
		[TestMethod()]
		[Description("Insert(Int32, T) method when 'item' is a null reference.")]
		[ExpectedException(typeof(ArgumentNullException))]
		public void NonNullCollection_Unit_Insert_ItemIsNull() {
			NonNullCollection<String> target = new NonNullCollection<String>();
			Int32 index = 0;
			String item = null;

			target.Insert(index, item);
		}
	}
}