using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Vizistata.Collections {
	/// <summary>
	/// This is a test class for <see cref="T:ReadOnlyDictionary&lt;TKey,TValue&gt;"/> and is intended to contain all <see cref="T:ReadOnlyDictionary&lt;TKey,TValue&gt;"/> Unit Tests.
	///</summary>
	[TestClass()]
	public class ReadOnlyDictionaryTests {
		#region Test Class Implementation
		/// <summary>
		/// Describes the context under which the current test is running.
		/// </summary>
		private TestContext _testContextInstance;

		/// <summary>
		/// Initializes a new instance of the <see cref="T:ReadOnlyDictionaryTests"/> class.
		/// </summary>
		public ReadOnlyDictionaryTests() : base() { }

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
		[Description(".ctor(IDictionary<TKey,TValue>) constructor for the optimal path.")]
		public void ReadOnlyDictionary_Unit_Constructor_Optimal() {
			IDictionary<String, String> dictionary = new Dictionary<String, String>() {
				{ "Key1", "Value1" },
				{ "Key2", "Value2" },
				{ "Key3", "Value3" }
			};
			new ReadOnlyDictionary<String, String>(dictionary);
		}
		[TestMethod()]
		[Description(".ctor(IDictionary<TKey,TValue>) constructor when 'dictionary' is a null reference.")]
		[ExpectedException(typeof(ArgumentNullException))]
		public void ReadOnlyDictionary_Unit_Constructor_DictionaryIsNull() {
			IDictionary<String, String> dictionary = null;
			new ReadOnlyDictionary<String, String>(dictionary);
		}

	// Property Tests
		[TestMethod()]
		[Description("Indexer property for the optimal path.")]
		public void ReadOnlyDictionary_Unit_Indexer1_Optimal() {
			IDictionary<String, String> dictionary = new Dictionary<String, String>() {
				{ "Key1", "Value1" },
				{ "Key2", "Value2" },
				{ "Key3", "Value3" }
			};
			ReadOnlyDictionary<String, String> target = new ReadOnlyDictionary<String, String>(dictionary);
			String key = dictionary.Keys.First();
			String value = dictionary[dictionary.Keys.First()];

			String actual = target[key];
			Assert.AreEqual(value, actual);
		}
		[TestMethod()]
		[Description("Indexer property when 'key' is a null reference.")]
		[ExpectedException(typeof(ArgumentNullException))]
		public void ReadOnlyDictionary_Unit_Indexer1_KeyIsNull() {
			IDictionary<String, String> dictionary = new Dictionary<String, String>() {
				{ "Key1", "Value1" },
				{ "Key2", "Value2" },
				{ "Key3", "Value3" }
			};
			ReadOnlyDictionary<String, String> target = new ReadOnlyDictionary<String, String>(dictionary);
			String key = null;

			String actual = target[key];
		}
		[TestMethod()]
		[Description("Indexer property when 'key' does not exist.")]
		[ExpectedException(typeof(KeyNotFoundException))]
		public void ReadOnlyDictionary_Unit_Indexer1_KeyDoesNotExist() {
			IDictionary<String, String> dictionary = new Dictionary<String, String>() {
				{ "Key1", "Value1" },
				{ "Key2", "Value2" },
				{ "Key3", "Value3" }
			};
			ReadOnlyDictionary<String, String> target = new ReadOnlyDictionary<String, String>(dictionary);
			String key = "MyKey";

			String actual = target[key];
		}

		[TestMethod()]
		[Description("IDictionary<TKey, TValue>.Indexer property for the optimal getter path.")]
		public void ReadOnlyDictionary_Unit_Indexer2_Get() {
			IDictionary<String, String> dictionary = new Dictionary<String, String>() {
				{ "Key1", "Value1" },
				{ "Key2", "Value2" },
				{ "Key3", "Value3" }
			};
			IDictionary<String, String> target = new ReadOnlyDictionary<String, String>(dictionary);
			String key = dictionary.Keys.First();
			String value = dictionary[dictionary.Keys.First()];

			String actual = target[key];
			Assert.AreEqual(value, actual);
		}
		[TestMethod()]
		[Description("IDictionary<TKey, TValue>.Indexer property for the optimal setter path.")]
		[ExpectedException(typeof(NotSupportedException))]
		public void ReadOnlyDictionary_Unit_Indexer2_Set() {
			IDictionary<String, String> dictionary = new Dictionary<String, String>() {
				{ "Key1", "Value1" },
				{ "Key2", "Value2" },
				{ "Key3", "Value3" }
			};
			IDictionary<String, String> target = new ReadOnlyDictionary<String, String>(dictionary);
			String key = "MyKey";
			String value = "MyValue";

			target[key] = value;
		}
		[TestMethod()]
		[Description("IDictionary<TKey, TValue>.Indexer property when 'key' is a null reference.")]
		[ExpectedException(typeof(ArgumentNullException))]
		public void ReadOnlyDictionary_Unit_Indexer2_KeyIsNull() {
			IDictionary<String, String> dictionary = new Dictionary<String, String>() {
				{ "Key1", "Value1" },
				{ "Key2", "Value2" },
				{ "Key3", "Value3" }
			};
			IDictionary<String, String> target = new ReadOnlyDictionary<String, String>(dictionary);
			String key = null;

			String actual = target[key];
		}
		[TestMethod()]
		[Description("IDictionary<TKey, TValue>.Indexer property when 'key' does not exist.")]
		[ExpectedException(typeof(KeyNotFoundException))]
		public void ReadOnlyDictionary_Unit_Indexer2_KeyDoesNotExist() {
			IDictionary<String, String> dictionary = new Dictionary<String, String>() {
				{ "Key1", "Value1" },
				{ "Key2", "Value2" },
				{ "Key3", "Value3" }
			};
			IDictionary<String, String> target = new ReadOnlyDictionary<String, String>(dictionary);
			String key = "MyKey";

			String actual = target[key];
		}

		[TestMethod()]
		[Description("Count property for the optimal path.")]
		public void ReadOnlyDictionary_Unit_Count_Optimal() {
			IDictionary<String, String> dictionary = new Dictionary<String, String>() {
				{ "Key1", "Value1" },
				{ "Key2", "Value2" },
				{ "Key3", "Value3" }
			};
			ReadOnlyDictionary<String, String> target = new ReadOnlyDictionary<String, String>(dictionary);

			Int32 actual = target.Count;
			Assert.AreEqual(dictionary.Count, actual);
		}

		[TestMethod()]
		[Description("ICollection<KeyValuePair<TKey, TValue>>.IsReadOnly property for the optimal path.")]
		public void ReadOnlyDictionary_Unit_IsReadOnly_Optimal() {
			IDictionary<String, String> dictionary = new Dictionary<String, String>() {
				{ "Key1", "Value1" },
				{ "Key2", "Value2" },
				{ "Key3", "Value3" }
			};
			ICollection<KeyValuePair<String, String>> target = new ReadOnlyDictionary<String, String>(dictionary);

			Boolean actual = target.IsReadOnly;
			Assert.IsTrue(actual);
		}

		[TestMethod()]
		[Description("Keys property for the optimal path.")]
		public void ReadOnlyDictionary_Unit_Keys_Optimal() {
			IDictionary<String, String> dictionary = new Dictionary<String, String>() {
				{ "Key1", "Value1" },
				{ "Key2", "Value2" },
				{ "Key3", "Value3" }
			};
			ReadOnlyDictionary<String, String> target = new ReadOnlyDictionary<String, String>(dictionary);

			String[] actual = target.Keys.ToArray();
			CollectionAssert.AreEquivalent(dictionary.Keys.ToArray(), actual);
		}

		[TestMethod()]
		[Description("Values property for the optimal path.")]
		public void ReadOnlyDictionary_Unit_Values_Optimal() {
			IDictionary<String, String> dictionary = new Dictionary<String, String>() {
				{ "Key1", "Value1" },
				{ "Key2", "Value2" },
				{ "Key3", "Value3" }
			};
			ReadOnlyDictionary<String, String> target = new ReadOnlyDictionary<String, String>(dictionary);

			String[] actual = target.Values.ToArray();
			CollectionAssert.AreEquivalent(dictionary.Values.ToArray(), actual);
		}

	// Method Tests
		[TestMethod()]
		[Description("IDictionary<TKey, TValue>.Add(TKey, TValue) method for the optimal path.")]
		[ExpectedException(typeof(NotSupportedException))]
		public void ReadOnlyDictionary_Unit_Add1_Optimal() {
			IDictionary<String, String> dictionary = new Dictionary<String, String>() {
				{ "Key1", "Value1" },
				{ "Key2", "Value2" },
				{ "Key3", "Value3" }
			};
			IDictionary<String, String> target = new ReadOnlyDictionary<String, String>(dictionary);
			String key = "MyKey";
			String value = "MyValue";

			target.Add(key, value);
		}

		[TestMethod()]
		[Description("ICollection<KeyValuePair<String, String>>.Add(KeyValuePair<TKey, TValue>) method for the optimal path.")]
		[ExpectedException(typeof(NotSupportedException))]
		public void ReadOnlyDictionary_Unit_Add2_Optimal() {
			IDictionary<String, String> dictionary = new Dictionary<String, String>() {
				{ "Key1", "Value1" },
				{ "Key2", "Value2" },
				{ "Key3", "Value3" }
			};
			ICollection<KeyValuePair<String, String>> target = new ReadOnlyDictionary<String, String>(dictionary);
			String key = "MyKey";
			String value = "MyValue";
			KeyValuePair<String, String> item = new KeyValuePair<String, String>(key, value);

			target.Add(item);
		}

		[TestMethod()]
		[Description("Clear() method for the optimal path.")]
		[ExpectedException(typeof(NotSupportedException))]
		public void ReadOnlyDictionary_Unit_Clear_Optimal() {
			IDictionary<String, String> dictionary = new Dictionary<String, String>() {
				{ "Key1", "Value1" },
				{ "Key2", "Value2" },
				{ "Key3", "Value3" }
			};
			IDictionary<String, String> target = new ReadOnlyDictionary<String, String>(dictionary);

			target.Clear();
		}

		[TestMethod()]
		[Description("Contains(KeyValuePair<TKey, TValue>) method for the optimal path.")]
		public void ReadOnlyDictionary_Unit_Contains_Optimal() {
			IDictionary<String, String> dictionary = new Dictionary<String, String>() {
				{ "Key1", "Value1" },
				{ "Key2", "Value2" },
				{ "Key3", "Value3" }
			};
			ReadOnlyDictionary<String, String> target = new ReadOnlyDictionary<String, String>(dictionary);
			KeyValuePair<String, String> item = new KeyValuePair<String, String>(dictionary.Keys.First(), dictionary[dictionary.Keys.First()]);

			Boolean actual = target.Contains(item);
			Assert.AreEqual(true, actual);
		}
		[TestMethod()]
		[Description("Contains(KeyValuePair<TKey, TValue>) method when the key is a null reference.")]
		[ExpectedException(typeof(ArgumentNullException))]
		public void ReadOnlyDictionary_Unit_Contains_KeyIsNull() {
			IDictionary<String, String> dictionary = new Dictionary<String, String>() {
				{ "Key1", "Value1" },
				{ "Key2", "Value2" },
				{ "Key3", "Value3" }
			};
			ReadOnlyDictionary<String, String> target = new ReadOnlyDictionary<String, String>(dictionary);
			KeyValuePair<String, String> item = new KeyValuePair<String, String>(null, dictionary[dictionary.Keys.First()]);

			Boolean actual = target.Contains(item);
			Assert.AreEqual(false, actual);
		}
		[TestMethod()]
		[Description("Contains(KeyValuePair<TKey, TValue>) method when the key does not exist.")]
		public void ReadOnlyDictionary_Unit_Contains_KeyDoesNotExist() {
			IDictionary<String, String> dictionary = new Dictionary<String, String>() {
				{ "Key1", "Value1" },
				{ "Key2", "Value2" },
				{ "Key3", "Value3" }
			};
			ReadOnlyDictionary<String, String> target = new ReadOnlyDictionary<String, String>(dictionary);
			KeyValuePair<String, String> item = new KeyValuePair<String, String>("MyKey", "Value1");

			Boolean actual = target.Contains(item);
			Assert.AreEqual(false, actual);
		}
		[TestMethod()]
		[Description("Contains(KeyValuePair<TKey, TValue>) method when the value does not exist.")]
		public void ReadOnlyDictionary_Unit_Contains_ValueDoesNotExist() {
			IDictionary<String, String> dictionary = new Dictionary<String, String>() {
				{ "Key1", "Value1" },
				{ "Key2", "Value2" },
				{ "Key3", "Value3" }
			};
			ReadOnlyDictionary<String, String> target = new ReadOnlyDictionary<String, String>(dictionary);
			KeyValuePair<String, String> item = new KeyValuePair<String, String>(dictionary.Keys.First(), "MyValue");

			Boolean actual = target.Contains(item);
			Assert.AreEqual(false, actual);
		}

		[TestMethod()]
		[Description("ContainsKey(TKey) method for the optimal path.")]
		public void ReadOnlyDictionary_Unit_ContainsKey_Optimal() {
			IDictionary<String, String> dictionary = new Dictionary<String, String>() {
				{ "Key1", "Value1" },
				{ "Key2", "Value2" },
				{ "Key3", "Value3" }
			};
			ReadOnlyDictionary<String, String> target = new ReadOnlyDictionary<String, String>(dictionary);
			String key = dictionary.Keys.First();

			Boolean actual = target.ContainsKey(key);
			Assert.AreEqual(true, actual);
		}
		[TestMethod()]
		[Description("ContainsKey(TKey) method when 'key' is a null reference.")]
		[ExpectedException(typeof(ArgumentNullException))]
		public void ReadOnlyDictionary_Unit_ContainsKey_KeyIsNull() {
			IDictionary<String, String> dictionary = new Dictionary<String, String>() {
				{ "Key1", "Value1" },
				{ "Key2", "Value2" },
				{ "Key3", "Value3" }
			};
			ReadOnlyDictionary<String, String> target = new ReadOnlyDictionary<String, String>(dictionary);
			String key = null;

			target.ContainsKey(key);
		}
		[TestMethod()]
		[Description("ContainsKey(TKey) method when 'key' does not exist.")]
		public void ReadOnlyDictionary_Unit_ContainsKey_KeyDoesNotExist() {
			IDictionary<String, String> dictionary = new Dictionary<String, String>() {
				{ "Key1", "Value1" },
				{ "Key2", "Value2" },
				{ "Key3", "Value3" }
			};
			ReadOnlyDictionary<String, String> target = new ReadOnlyDictionary<String, String>(dictionary);
			String key = "MyKey";

			Boolean actual = target.ContainsKey(key);
			Assert.AreEqual(false, actual);
		}

		[TestMethod()]
		[Description("CopyTo(KeyValuePair<TKey, TValue>[], Int32) method for the optimal path.")]
		public void ReadOnlyDictionary_Unit_CopyTo_Optimal() {
			IDictionary<String, String> dictionary = new Dictionary<String, String>() {
				{ "Key1", "Value1" },
				{ "Key2", "Value2" },
				{ "Key3", "Value3" }
			};
			ReadOnlyDictionary<String, String> target = new ReadOnlyDictionary<String, String>(dictionary);
			KeyValuePair<String, String>[] array = new KeyValuePair<String, String>[dictionary.Count];
			Int32 arrayIndex = 0;

			target.CopyTo(array, arrayIndex);
			Assert.AreEqual(dictionary.Count, array.Length);
			foreach (KeyValuePair<String, String> pair in array) {
				Assert.IsTrue(dictionary.Contains(pair));
			}
		}
		[TestMethod()]
		[Description("CopyTo(KeyValuePair<TKey, TValue>[], Int32) method when 'array' is a null reference.")]
		[ExpectedException(typeof(ArgumentNullException))]
		public void ReadOnlyDictionary_Unit_CopyTo_ArrayIsNull() {
			IDictionary<String, String> dictionary = new Dictionary<String, String>() {
				{ "Key1", "Value1" },
				{ "Key2", "Value2" },
				{ "Key3", "Value3" }
			};
			ReadOnlyDictionary<String, String> target = new ReadOnlyDictionary<String, String>(dictionary);
			KeyValuePair<String, String>[] array = null;
			Int32 arrayIndex = 0;

			target.CopyTo(array, arrayIndex);
		}
		[TestMethod()]
		[Description("CopyTo(KeyValuePair<TKey, TValue>[], Int32) method when 'arrayIndex' is less than 0.")]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void ReadOnlyDictionary_Unit_CopyTo_ArrayIndexIsLessThan0() {
			IDictionary<String, String> dictionary = new Dictionary<String, String>() {
				{ "Key1", "Value1" },
				{ "Key2", "Value2" },
				{ "Key3", "Value3" }
			};
			ReadOnlyDictionary<String, String> target = new ReadOnlyDictionary<String, String>(dictionary);
			KeyValuePair<String, String>[] array = new KeyValuePair<String, String>[dictionary.Count];
			Int32 arrayIndex = -1;

			target.CopyTo(array, arrayIndex);
		}
		[TestMethod()]
		[Description("CopyTo(KeyValuePair<TKey, TValue>[], Int32) method when 'array' is too small to fit all elements.")]
		[ExpectedException(typeof(ArgumentException))]
		public void ReadOnlyDictionary_Unit_CopyTo_ArrayIsTooSmall() {
			IDictionary<String, String> dictionary = new Dictionary<String, String>() {
				{ "Key1", "Value1" },
				{ "Key2", "Value2" },
				{ "Key3", "Value3" }
			};
			ReadOnlyDictionary<String, String> target = new ReadOnlyDictionary<String, String>(dictionary);
			KeyValuePair<String, String>[] array = new KeyValuePair<String, String>[dictionary.Count - 2];
			Int32 arrayIndex = 1;

			target.CopyTo(array, arrayIndex);
		}

		[TestMethod()]
		[Description("IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator() method for the optimal path.")]
		public void ReadOnlyDictionary_Unit_GetEnumerator1_Optimal() {
			IDictionary<String, String> dictionary = new Dictionary<String, String>() {
				{ "Key1", "Value1" },
				{ "Key2", "Value2" },
				{ "Key3", "Value3" }
			};
			ReadOnlyDictionary<String, String> target = new ReadOnlyDictionary<String, String>(dictionary);

			using (IEnumerator<KeyValuePair<String, String>> actual = target.GetEnumerator()) {
				Int32 count = 0;
				while (actual.MoveNext()) {
					count++;
					Assert.IsTrue(dictionary.Contains(actual.Current));
				}
				Assert.AreEqual(dictionary.Count, count);
			}
		}

		[TestMethod()]
		[Description("IEnumerable.GetEnumerator() method for the optimal path.")]
		public void ReadOnlyDictionary_Unit_GetEnumerator2_Optimal() {
			IDictionary<String, String> dictionary = new Dictionary<String, String>() {
				{ "Key1", "Value1" },
				{ "Key2", "Value2" },
				{ "Key3", "Value3" }
			};
			IEnumerable target = new ReadOnlyDictionary<String, String>(dictionary);

			IEnumerator actual = target.GetEnumerator();
			Int32 count = 0;
			while (actual.MoveNext()) {
				count++;
				Assert.IsTrue(dictionary.Contains((KeyValuePair<String, String>)actual.Current));
			}
			Assert.AreEqual(dictionary.Count, count);
		}

		[TestMethod()]
		[Description("IDictionary<String, String>.Remove(TKey) method for the optimal path.")]
		[ExpectedException(typeof(NotSupportedException))]
		public void ReadOnlyDictionary_Unit_Remove1_Optimal() {
			IDictionary<String, String> dictionary = new Dictionary<String, String>() {
				{ "Key1", "Value1" },
				{ "Key2", "Value2" },
				{ "Key3", "Value3" }
			};
			IDictionary<String, String> target = new ReadOnlyDictionary<String, String>(dictionary);
			String key = dictionary.Keys.First();

			target.Remove(key);
		}

		[TestMethod()]
		[Description("ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue>) method for the optimal path.")]
		[ExpectedException(typeof(NotSupportedException))]
		public void ReadOnlyDictionary_Unit_Remove2_Optimal() {
			IDictionary<String, String> dictionary = new Dictionary<String, String>() {
				{ "Key1", "Value1" },
				{ "Key2", "Value2" },
				{ "Key3", "Value3" }
			};
			ICollection<KeyValuePair<String, String>> target = new ReadOnlyDictionary<String, String>(dictionary);
			String key = dictionary.Keys.First();
			String value = dictionary[dictionary.Keys.First()];
			KeyValuePair<String, String> item = new KeyValuePair<String, String>(key, value);

			target.Remove(item);
		}

		[TestMethod()]
		[Description("TryGetValue(TKey, &TValue) method for the optimal path.")]
		public void ReadOnlyDictionary_Unit_TryGetValue_Optimal() {
			IDictionary<String, String> dictionary = new Dictionary<String, String>() {
				{ "Key1", "Value1" },
				{ "Key2", "Value2" },
				{ "Key3", "Value3" }
			};
			ReadOnlyDictionary<String, String> target = new ReadOnlyDictionary<String, String>(dictionary);
			String key = dictionary.Keys.First();
			String value;

			Boolean actual = target.TryGetValue(key, out value);
			Assert.AreEqual(true, actual);
			Assert.AreEqual(dictionary[key], value);
		}
		[TestMethod()]
		[Description("TryGetValue(TKey, &TValue) method when 'key' is a null reference.")]
		[ExpectedException(typeof(ArgumentNullException))]
		public void ReadOnlyDictionary_Unit_TryGetValue_KeyIsNull() {
			IDictionary<String, String> dictionary = new Dictionary<String, String>() {
				{ "Key1", "Value1" },
				{ "Key2", "Value2" },
				{ "Key3", "Value3" }
			};
			ReadOnlyDictionary<String, String> target = new ReadOnlyDictionary<String, String>(dictionary);
			String key = null;
			String value;

			target.TryGetValue(key, out value);
		}
		[TestMethod()]
		[Description("TryGetValue(TKey, &TValue) method when 'key' does not exist.")]
		public void ReadOnlyDictionary_Unit_TryGetValue_KeyDoesNotExist() {
			IDictionary<String, String> dictionary = new Dictionary<String, String>() {
				{ "Key1", "Value1" },
				{ "Key2", "Value2" },
				{ "Key3", "Value3" }
			};
			ReadOnlyDictionary<String, String> target = new ReadOnlyDictionary<String, String>(dictionary);
			String key = "MyKey";
			String value;

			Boolean actual = target.TryGetValue(key, out value);
			Assert.AreEqual(false, actual);
			Assert.IsNull(value);
		}
	}
}