using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Vizistata.Collections;

namespace Vizistata.Linq {
	/// <summary>
	/// This is a test class for <see cref="T:DictionaryExtensions"/> and is intended to contain all <see cref="T:DictionaryExtensions"/> Unit Tests.
	///</summary>
	[TestClass()]
	public class DictionaryExtensionsTests {
		#region Test Class Implementation
		/// <summary>
		/// Describes the context under which the current test is running.
		/// </summary>
		private TestContext _testContextInstance;

		/// <summary>
		/// Initializes a new instance of the <see cref="T:DictionaryExtensionsTests"/> class.
		/// </summary>
		public DictionaryExtensionsTests() : base() { }

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
		[Description("AsReadOnly<TKey, TValue>(IDictionary<TKey, TValue>) method for the optimal path.")]
		public void DictionaryExtensions_Unit_AsReadOnly_Optimal() {
			IDictionary<String, DateTime> dictionary = new Dictionary<String, DateTime>() {
				{ "One", DateTime.Today.AddDays(-1) },
				{ "Two", DateTime.Today },
				{ "Three", DateTime.Today.AddDays(1) } };

			ReadOnlyDictionary<String, DateTime> actual = DictionaryExtensions.AsReadOnly(dictionary);
			CollectionAssert.AreEquivalent(dictionary.ToArray(pair => pair), actual.ToArray(pair => pair));
		}
		[TestMethod()]
		[Description("AsReadOnly<TKey, TValue>(IDictionary<TKey, TValue>) method when 'dictionary' is a null reference.")]
		[ExpectedException(typeof(ArgumentNullException))]
		public void DictionaryExtensions_Unit_AsReadOnly_DictionaryIsNull() {
			IDictionary<String, DateTime> dictionary = null;
			DictionaryExtensions.AsReadOnly(dictionary);
		}

		[TestMethod()]
		[Description("GetValueOrDefault<TKey, TValue>(IDictionary<TKey, TValue>, TKey) method for the optimal path.")]
		public void DictionaryExtensions_Unit_GetValueOrDefault_Optimal() {
			IDictionary<String, DateTime> dictionary = new Dictionary<String, DateTime>() {
				{ "One", DateTime.Today.AddDays(-1) },
				{ "Two", DateTime.Today },
				{ "Three", DateTime.Today.AddDays(1) } };

			Int32 index = 1;
			String key = dictionary.ElementAt(index).Key;
			DateTime actual = DictionaryExtensions.GetValueOrDefault(dictionary, key);

			DateTime expected = dictionary.ElementAt(index).Value;
			Assert.AreEqual(expected, actual);
		}
		[TestMethod()]
		[Description("GetValueOrDefault<TKey, TValue>(IDictionary<TKey, TValue>, TKey) method when 'key' does not exist.")]
		public void DictionaryExtensions_Unit_GetValueOrDefault_KeyDoesNotExist() {
			IDictionary<String, DateTime> dictionary = new Dictionary<String, DateTime>() {
				{ "One", DateTime.Today.AddDays(-1) },
				{ "Two", DateTime.Today },
				{ "Three", DateTime.Today.AddDays(1) } };

			String key = "Twelve";
			DateTime actual = DictionaryExtensions.GetValueOrDefault(dictionary, key);

			DateTime expected = default(DateTime);
			Assert.AreEqual(expected, actual);
		}
		[TestMethod()]
		[Description("GetValueOrDefault<TKey, TValue>(IDictionary<TKey, TValue>, TKey) method when 'dictionary' is a null reference.")]
		[ExpectedException(typeof(ArgumentNullException))]
		public void DictionaryExtensions_Unit_GetValueOrDefault_DictionaryIsNull() {
			IDictionary<String, DateTime> dictionary = null;
			String key = "Test";
			DictionaryExtensions.GetValueOrDefault(dictionary, key);
		}
	}
}