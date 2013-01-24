using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Vizistata.Collections {
	/// <summary>
	/// This is a test class for <see cref="T:NonNullDictionary&lt;TKey,TValue&gt;"/> and is intended to contain all <see cref="T:NonNullDictionary&lt;TKey,TValue&gt;"/> Unit Tests.
	///</summary>
	[TestClass()]
	public class NonNullDictionaryTests {
		#region Test Class Implementation
		/// <summary>
		/// Describes the context under which the current test is running.
		/// </summary>
		private TestContext _testContextInstance;

		/// <summary>
		/// Initializes a new instance of the <see cref="T:NonNullDictionaryTests"/> class.
		/// </summary>
		public NonNullDictionaryTests() : base() { }

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
		public void NonNullDictionary_Unit_Constructor1_Optimal() {
			new NonNullDictionary<String, String>();
		}

		[TestMethod()]
		[Description(".ctor(Int32) constructor for the optimal path.")]
		public void NonNullDictionary_Unit_Constructor2_Optimal() {
			Int32 capacity = 1;
			new NonNullDictionary<String, String>(capacity);
		}
		[TestMethod()]
		[Description(".ctor(Int32) constructor when 'capacity' is too small.")]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void NonNullDictionary_Unit_Constructor2_CapacityIsTooSmall() {
			Int32 capacity = -1;
			new NonNullDictionary<String, String>(capacity);
		}

		[TestMethod()]
		[Description(".ctor(IEqualityComparer<T>) constructor for the optimal path.")]
		public void NonNullDictionary_Unit_Constructor3_Optimal() {
			IEqualityComparer<String> comparer = StringComparer.Ordinal;
			new NonNullDictionary<String, String>(comparer);
		}
		[TestMethod()]
		[Description(".ctor(IEqualityComparer<T>) constructor when 'comparer' is a null reference.")]
		public void NonNullDictionary_Unit_Constructor3_ComparerIsNull() {
			IEqualityComparer<String> comparer = null;
			new NonNullDictionary<String, String>(comparer);
		}

		[TestMethod()]
		[Description(".ctor(Int32, IEqualityComparer<T>) constructor for the optimal path.")]
		public void NonNullDictionary_Unit_Constructor4_Optimal() {
			Int32 capacity = 1;
			IEqualityComparer<String> comparer = StringComparer.Ordinal;
			new NonNullDictionary<String, String>(capacity, comparer);
		}
		[TestMethod()]
		[Description(".ctor(Int32, IEqualityComparer<T>) constructor when 'capacity' is too small.")]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void NonNullDictionary_Unit_Constructor4_CapacityIsTooSmall() {
			Int32 capacity = -1;
			IEqualityComparer<String> comparer = StringComparer.Ordinal;
			new NonNullDictionary<String, String>(capacity, comparer);
		}
		[TestMethod()]
		[Description(".ctor(Int32, IEqualityComparer<T>) constructor when 'comparer' is a null reference.")]
		public void NonNullDictionary_Unit_Constructor4_ComparerIsNull() {
			Int32 capacity = 1;
			IEqualityComparer<String> comparer = null;
			new NonNullDictionary<String, String>(capacity, comparer);
		}

		[TestMethod()]
		[Description(".ctor(IDictionary<TKey,TValue>) constructor for the optimal path.")]
		public void NonNullDictionary_Unit_Constructor5_Optimal() {
			IDictionary<String, String> dictionary = new Dictionary<String, String>() {
				{ "Key1", "Value1" },
				{ "Key2", "Value2" },
				{ "Key3", "Value3" }
			};
			new NonNullDictionary<String, String>(dictionary);
		}
		[TestMethod()]
		[Description(".ctor(IDictionary<TKey,TValue>) constructor when 'dictionary' is a null reference.")]
		[ExpectedException(typeof(ArgumentNullException))]
		public void NonNullDictionary_Unit_Constructor5_DictionaryIsNull() {
			IDictionary<String, String> dictionary = null;
			new NonNullDictionary<String, String>(dictionary);
		}

		[TestMethod()]
		[Description(".ctor(IDictionary<TKey,TValue>, IEqualityComparer<T>) constructor for the optimal path.")]
		public void NonNullDictionary_Unit_Constructor6_Optimal() {
			IDictionary<String, String> dictionary = new Dictionary<String, String>() {
				{ "Key1", "Value1" },
				{ "Key2", "Value2" },
				{ "Key3", "Value3" }
			};
			IEqualityComparer<String> comparer = StringComparer.Ordinal;
			new NonNullDictionary<String, String>(dictionary, comparer);
		}
		[TestMethod()]
		[Description(".ctor(IDictionary<TKey,TValue>, IEqualityComparer<T>) constructor when 'dictionary' is a null reference.")]
		[ExpectedException(typeof(ArgumentNullException))]
		public void NonNullDictionary_Unit_Constructor6_DictionaryIsNull() {
			IDictionary<String, String> dictionary = null;
			IEqualityComparer<String> comparer = StringComparer.Ordinal;
			new NonNullDictionary<String, String>(dictionary, comparer);
		}
		[TestMethod()]
		[Description(".ctor(IDictionary<TKey,TValue>, IEqualityComparer<T>) constructor when 'comparer' is a null reference.")]
		public void NonNullDictionary_Unit_Constructor6_ComparerIsNull() {
			IDictionary<String, String> dictionary = new Dictionary<String, String>() {
				{ "Key1", "Value1" },
				{ "Key2", "Value2" },
				{ "Key3", "Value3" }
			};
			IEqualityComparer<String> comparer = null;
			new NonNullDictionary<String, String>(dictionary, comparer);
		}

	// Property Tests
		[TestMethod()]
		[Description("Indexer property for the optimal path.")]
		public void NonNullDictionary_Unit_Indexer_Optimal() {
			NonNullDictionary<String, String> target = new NonNullDictionary<String, String>();
			String key = "MyKey";
			String value = "MyValue";

			target[key] = value;
			String actual = target[key];
			Assert.AreEqual(value, actual);
		}
		[TestMethod()]
		[Description("Indexer property when 'key' is a null reference.")]
		[ExpectedException(typeof(ArgumentNullException))]
		public void NonNullDictionary_Unit_Indexer_KeyIsNull() {
			NonNullDictionary<String, String> target = new NonNullDictionary<String, String>();
			String key = null;
			String value = "MyValue";

			target[key] = value;
		}
		[TestMethod()]
		[Description("Indexer property when 'value' is a null reference.")]
		[ExpectedException(typeof(ArgumentNullException))]
		public void NonNullDictionary_Unit_Indexer_ValueIsNull() {
			NonNullDictionary<String, String> target = new NonNullDictionary<String, String>();
			String key = "MyKey";
			String value = null;

			target[key] = value;
		}
		[TestMethod()]
		[Description("Indexer property when 'key' does not exist.")]
		[ExpectedException(typeof(KeyNotFoundException))]
		public void NonNullDictionary_Unit_Indexer_KeyDoesNotExist() {
			NonNullDictionary<String, String> target = new NonNullDictionary<String, String>();
			String key = "MyKey";

			String actual = target[key];
		}

		[TestMethod()]
		[Description("Comparer property for the optimal path.")]
		public void NonNullDictionary_Unit_Comparer_Optimal() {
			IDictionary<String, String> dictionary = new Dictionary<String, String>() {
				{ "Key1", "Value1" },
				{ "Key2", "Value2" },
				{ "Key3", "Value3" }
			};
			IEqualityComparer<String> comparer = StringComparer.InvariantCultureIgnoreCase;
			NonNullDictionary<String, String> target = new NonNullDictionary<String, String>(dictionary, comparer);

			IEqualityComparer<String> actual = target.Comparer;
			Assert.AreEqual(comparer, actual);
		}

		[TestMethod()]
		[Description("Count property for the optimal path.")]
		public void NonNullDictionary_Unit_Count_Optimal() {
			IDictionary<String, String> dictionary = new Dictionary<String, String>() {
				{ "Key1", "Value1" },
				{ "Key2", "Value2" },
				{ "Key3", "Value3" }
			};
			NonNullDictionary<String, String> target = new NonNullDictionary<String, String>(dictionary);

			Int32 actual = target.Count;
			Assert.AreEqual(dictionary.Count, actual);
		}

		[TestMethod()]
		[Description("ICollection<KeyValuePair<TKey, TValue>>.IsReadOnly property for the optimal path.")]
		public void NonNullDictionary_Unit_IsReadOnly_Optimal() {
			ICollection<KeyValuePair<String, String>> target = new NonNullDictionary<String, String>();

			Boolean actual = target.IsReadOnly;
			Assert.IsFalse(actual);
		}

		[TestMethod()]
		[Description("Keys property for the optimal path.")]
		public void NonNullDictionary_Unit_Keys1_Optimal() {
			IDictionary<String, String> dictionary = new Dictionary<String, String>() {
				{ "Key1", "Value1" },
				{ "Key2", "Value2" },
				{ "Key3", "Value3" }
			};
			NonNullDictionary<String, String> target = new NonNullDictionary<String, String>(dictionary);

			String[] actual = target.Keys.ToArray();
			CollectionAssert.AreEquivalent(dictionary.Keys.ToArray(), actual);
		}

		[TestMethod()]
		[Description("IDictionary<TKey, TValue>.Keys property for the optimal path.")]
		public void NonNullDictionary_Unit_Keys2_Optimal() {
			IDictionary<String, String> dictionary = new Dictionary<String, String>() {
				{ "Key1", "Value1" },
				{ "Key2", "Value2" },
				{ "Key3", "Value3" }
			};
			IDictionary<String, String> target = new NonNullDictionary<String, String>(dictionary);

			String[] actual = target.Keys.ToArray();
			CollectionAssert.AreEquivalent(dictionary.Keys.ToArray(), actual);
		}

		[TestMethod()]
		[Description("Values property for the optimal path.")]
		public void NonNullDictionary_Unit_Values1_Optimal() {
			IDictionary<String, String> dictionary = new Dictionary<String, String>() {
				{ "Key1", "Value1" },
				{ "Key2", "Value2" },
				{ "Key3", "Value3" }
			};
			NonNullDictionary<String, String> target = new NonNullDictionary<String, String>(dictionary);

			String[] actual = target.Values.ToArray();
			CollectionAssert.AreEquivalent(dictionary.Values.ToArray(), actual);
		}

		[TestMethod()]
		[Description("IDictionary<TKey, TValue>.Values property for the optimal path.")]
		public void NonNullDictionary_Unit_Values2_Optimal() {
			IDictionary<String, String> dictionary = new Dictionary<String, String>() {
				{ "Key1", "Value1" },
				{ "Key2", "Value2" },
				{ "Key3", "Value3" }
			};
			IDictionary<String, String> target = new NonNullDictionary<String, String>(dictionary);

			String[] actual = target.Values.ToArray();
			CollectionAssert.AreEquivalent(dictionary.Values.ToArray(), actual);
		}

	// Method Tests
		[TestMethod()]
		[Description("Add(TKey, TValue) method for the optimal path.")]
		public void NonNullDictionary_Unit_Add1_Optimal() {
			NonNullDictionary<String, String> target = new NonNullDictionary<String, String>();
			String key = "MyKey";
			String value = "MyValue";

			target.Add(key, value);
			Assert.IsTrue(target[key] == value);
		}
		[TestMethod()]
		[Description("Add(TKey, TValue) method when 'key' is a null reference.")]
		[ExpectedException(typeof(ArgumentNullException))]
		public void NonNullDictionary_Unit_Add1_KeyIsNull() {
			NonNullDictionary<String, String> target = new NonNullDictionary<String, String>();
			String key = null;
			String value = "MyValue";

			target.Add(key, value);
		}
		[TestMethod()]
		[Description("Add(TKey, TValue) method when 'value' is a null reference.")]
		[ExpectedException(typeof(ArgumentNullException))]
		public void NonNullDictionary_Unit_Add1_ValueIsNull() {
			NonNullDictionary<String, String> target = new NonNullDictionary<String, String>();
			String key = "MyKey";
			String value = null;

			target.Add(key, value);
		}
		[TestMethod()]
		[Description("Add(TKey, TValue) method when 'key' exists.")]
		[ExpectedException(typeof(ArgumentException))]
		public void NonNullDictionary_Unit_Add1_KeyExists() {
			NonNullDictionary<String, String> target = new NonNullDictionary<String, String>();
			String key = "MyKey";
			String value = "MyValue";

			target.Add(key, value);
			target.Add(key, value);
		}

		[TestMethod()]
		[Description("ICollection<KeyValuePair<String, String>>.Add(KeyValuePair<TKey, TValue>) method for the optimal path.")]
		public void NonNullDictionary_Unit_Add2_Optimal() {
			ICollection<KeyValuePair<String, String>> target = new NonNullDictionary<String, String>();
			String key = "MyKey";
			String value = "MyValue";
			KeyValuePair<String, String> item = new KeyValuePair<String, String>(key, value);

			target.Add(item);
			Assert.IsTrue(target.Contains(item));
		}
		[TestMethod()]
		[Description("Collection<KeyValuePair<String, String>>.Add(KeyValuePair<TKey, TValue>) method when the key is a null reference.")]
		[ExpectedException(typeof(ArgumentNullException))]
		public void NonNullDictionary_Unit_Add2_KeyIsNull() {
			ICollection<KeyValuePair<String, String>> target = new NonNullDictionary<String, String>();
			String key = null;
			String value = "MyValue";
			KeyValuePair<String, String> item = new KeyValuePair<String, String>(key, value);

			target.Add(item);
		}
		[TestMethod()]
		[Description("Collection<KeyValuePair<String, String>>.Add(KeyValuePair<TKey, TValue>) method when the value is a null reference.")]
		[ExpectedException(typeof(ArgumentNullException))]
		public void NonNullDictionary_Unit_Add2_ValueIsNull() {
			ICollection<KeyValuePair<String, String>> target = new NonNullDictionary<String, String>();
			String key = "MyKey";
			String value = null;
			KeyValuePair<String, String> item = new KeyValuePair<String, String>(key, value);

			target.Add(item);
		}
		[TestMethod()]
		[Description("Collection<KeyValuePair<String, String>>.Add(KeyValuePair<TKey, TValue>) method when the key exists.")]
		[ExpectedException(typeof(ArgumentException))]
		public void NonNullDictionary_Unit_Add2_KeyExists() {
			ICollection<KeyValuePair<String, String>> target = new NonNullDictionary<String, String>();
			String key = "MyKey";
			String value = "MyValue";
			KeyValuePair<String, String> item = new KeyValuePair<String, String>(key, value);

			target.Add(item);
			target.Add(item);
		}

		[TestMethod()]
		[Description("Clear() method for the optimal path.")]
		public void NonNullDictionary_Unit_Clear_Optimal() {
			IDictionary<String, String> dictionary = new Dictionary<String, String>() {
				{ "Key1", "Value1" },
				{ "Key2", "Value2" },
				{ "Key3", "Value3" }
			};
			NonNullDictionary<String, String> target = new NonNullDictionary<String, String>(dictionary);
			Assert.AreEqual(dictionary.Count, target.Count);

			target.Clear();
			Assert.AreEqual(0, target.Count);
		}

		[TestMethod()]
		[Description("ICollection<KeyValuePair<String, String>>.Contains(KeyValuePair<TKey, TValue>) method for the optimal path.")]
		public void NonNullDictionary_Unit_Contains_Optimal() {
			IDictionary<String, String> dictionary = new Dictionary<String, String>() {
				{ "Key1", "Value1" },
				{ "Key2", "Value2" },
				{ "Key3", "Value3" }
			};
			ICollection<KeyValuePair<String, String>> target = new NonNullDictionary<String, String>(dictionary);
			KeyValuePair<String, String> item = new KeyValuePair<String, String>(dictionary.Keys.First(), dictionary[dictionary.Keys.First()]);

			Boolean actual = target.Contains(item);
			Assert.AreEqual(true, actual);
		}
		[TestMethod()]
		[Description("ICollection<KeyValuePair<String, String>>.Contains(KeyValuePair<TKey, TValue>) method when the key is a null reference.")]
		[ExpectedException(typeof(ArgumentNullException))]
		public void NonNullDictionary_Unit_Contains_KeyIsNull() {
			IDictionary<String, String> dictionary = new Dictionary<String, String>() {
				{ "Key1", "Value1" },
				{ "Key2", "Value2" },
				{ "Key3", "Value3" }
			};
			ICollection<KeyValuePair<String, String>> target = new NonNullDictionary<String, String>(dictionary);
			KeyValuePair<String, String> item = new KeyValuePair<String, String>(null, dictionary[dictionary.Keys.First()]);

			Boolean actual = target.Contains(item);
			Assert.AreEqual(false, actual);
		}
		[TestMethod()]
		[Description("ICollection<KeyValuePair<String, String>>.Contains(KeyValuePair<TKey, TValue>) method when the value is a null reference.")]
		public void NonNullDictionary_Unit_Contains_ValueIsNull() {
			IDictionary<String, String> dictionary = new Dictionary<String, String>() {
				{ "Key1", "Value1" },
				{ "Key2", "Value2" },
				{ "Key3", "Value3" }
			};
			ICollection<KeyValuePair<String, String>> target = new NonNullDictionary<String, String>(dictionary);
			KeyValuePair<String, String> item = new KeyValuePair<String, String>(dictionary.Keys.First(), null);

			Boolean actual = target.Contains(item);
			Assert.AreEqual(false, actual);
		}
		[TestMethod()]
		[Description("ICollection<KeyValuePair<String, String>>.Contains(KeyValuePair<TKey, TValue>) method when the key does not exist.")]
		public void NonNullDictionary_Unit_Contains_KeyDoesNotExist() {
			IDictionary<String, String> dictionary = new Dictionary<String, String>() {
				{ "Key1", "Value1" },
				{ "Key2", "Value2" },
				{ "Key3", "Value3" }
			};
			ICollection<KeyValuePair<String, String>> target = new NonNullDictionary<String, String>(dictionary);
			KeyValuePair<String, String> item = new KeyValuePair<String, String>("MyKey", "Value1");

			Boolean actual = target.Contains(item);
			Assert.AreEqual(false, actual);
		}
		[TestMethod()]
		[Description("ICollection<KeyValuePair<String, String>>.Contains(KeyValuePair<TKey, TValue>) method when the value does not exist.")]
		public void NonNullDictionary_Unit_Contains_ValueDoesNotExist() {
			IDictionary<String, String> dictionary = new Dictionary<String, String>() {
				{ "Key1", "Value1" },
				{ "Key2", "Value2" },
				{ "Key3", "Value3" }
			};
			ICollection<KeyValuePair<String, String>> target = new NonNullDictionary<String, String>(dictionary);
			KeyValuePair<String, String> item = new KeyValuePair<String, String>(dictionary.Keys.First(), "MyValue");

			Boolean actual = target.Contains(item);
			Assert.AreEqual(false, actual);
		}

		[TestMethod()]
		[Description("ContainsKey(TKey) method for the optimal path.")]
		public void NonNullDictionary_Unit_ContainsKey_Optimal() {
			IDictionary<String, String> dictionary = new Dictionary<String, String>() {
				{ "Key1", "Value1" },
				{ "Key2", "Value2" },
				{ "Key3", "Value3" }
			};
			NonNullDictionary<String, String> target = new NonNullDictionary<String, String>(dictionary);
			String key = dictionary.Keys.First();

			Boolean actual = target.ContainsKey(key);
			Assert.AreEqual(true, actual);
		}
		[TestMethod()]
		[Description("ContainsKey(TKey) method when 'key' is a null reference.")]
		[ExpectedException(typeof(ArgumentNullException))]
		public void NonNullDictionary_Unit_ContainsKey_KeyIsNull() {
			IDictionary<String, String> dictionary = new Dictionary<String, String>() {
				{ "Key1", "Value1" },
				{ "Key2", "Value2" },
				{ "Key3", "Value3" }
			};
			NonNullDictionary<String, String> target = new NonNullDictionary<String, String>(dictionary);
			String key = null;

			target.ContainsKey(key);
		}
		[TestMethod()]
		[Description("ContainsKey(TKey) method when 'key' does not exist.")]
		public void NonNullDictionary_Unit_ContainsKey_KeyDoesNotExist() {
			IDictionary<String, String> dictionary = new Dictionary<String, String>() {
				{ "Key1", "Value1" },
				{ "Key2", "Value2" },
				{ "Key3", "Value3" }
			};
			NonNullDictionary<String, String> target = new NonNullDictionary<String, String>(dictionary);
			String key = "MyKey";

			Boolean actual = target.ContainsKey(key);
			Assert.AreEqual(false, actual);
		}

		[TestMethod()]
		[Description("ContainsValue(TValue) method for the optimal path.")]
		public void NonNullDictionary_Unit_ContainsValue_Optimal() {
			IDictionary<String, String> dictionary = new Dictionary<String, String>() {
				{ "Key1", "Value1" },
				{ "Key2", "Value2" },
				{ "Key3", "Value3" }
			};
			NonNullDictionary<String, String> target = new NonNullDictionary<String, String>(dictionary);
			String value = dictionary[dictionary.Keys.First()];

			Boolean actual = target.ContainsValue(value);
			Assert.AreEqual(true, actual);
		}
		[TestMethod()]
		[Description("ContainsValue(TValue) method when 'key' is a null reference.")]
		public void NonNullDictionary_Unit_ContainsValue_KeyIsNull() {
			IDictionary<String, String> dictionary = new Dictionary<String, String>() {
				{ "Key1", "Value1" },
				{ "Key2", "Value2" },
				{ "Key3", "Value3" }
			};
			NonNullDictionary<String, String> target = new NonNullDictionary<String, String>(dictionary);
			String value = null;

			Boolean actual = target.ContainsValue(value);
			Assert.AreEqual(false, actual);
		}
		[TestMethod()]
		[Description("ContainsValue(TValue) method when 'key' does not exist.")]
		public void NonNullDictionary_Unit_ContainsValue_KeyDoesNotExist() {
			IDictionary<String, String> dictionary = new Dictionary<String, String>() {
				{ "Key1", "Value1" },
				{ "Key2", "Value2" },
				{ "Key3", "Value3" }
			};
			NonNullDictionary<String, String> target = new NonNullDictionary<String, String>(dictionary);
			String value = "MyKey";

			Boolean actual = target.ContainsValue(value);
			Assert.AreEqual(false, actual);
		}

		[TestMethod()]
		[Description("CopyTo(KeyValuePair<TKey, TValue>[], Int32) method for the optimal path.")]
		public void NonNullDictionary_Unit_CopyTo_Optimal() {
			IDictionary<String, String> dictionary = new Dictionary<String, String>() {
				{ "Key1", "Value1" },
				{ "Key2", "Value2" },
				{ "Key3", "Value3" }
			};
			ICollection<KeyValuePair<String, String>> target = new NonNullDictionary<String, String>(dictionary);
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
		public void NonNullDictionary_Unit_CopyTo_ArrayIsNull() {
			IDictionary<String, String> dictionary = new Dictionary<String, String>() {
				{ "Key1", "Value1" },
				{ "Key2", "Value2" },
				{ "Key3", "Value3" }
			};
			ICollection<KeyValuePair<String, String>> target = new NonNullDictionary<String, String>(dictionary);
			KeyValuePair<String, String>[] array = null;
			Int32 arrayIndex = 0;

			target.CopyTo(array, arrayIndex);
		}
		[TestMethod()]
		[Description("CopyTo(KeyValuePair<TKey, TValue>[], Int32) method when 'arrayIndex' is less than 0.")]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void NonNullDictionary_Unit_CopyTo_ArrayIndexIsLessThan0() {
			IDictionary<String, String> dictionary = new Dictionary<String, String>() {
				{ "Key1", "Value1" },
				{ "Key2", "Value2" },
				{ "Key3", "Value3" }
			};
			ICollection<KeyValuePair<String, String>> target = new NonNullDictionary<String, String>(dictionary);
			KeyValuePair<String, String>[] array = new KeyValuePair<String, String>[dictionary.Count];
			Int32 arrayIndex = -1;

			target.CopyTo(array, arrayIndex);
		}
		[TestMethod()]
		[Description("CopyTo(KeyValuePair<TKey, TValue>[], Int32) method when 'array' is too small to fit all elements.")]
		[ExpectedException(typeof(ArgumentException))]
		public void NonNullDictionary_Unit_CopyTo_ArrayIsTooSmall() {
			IDictionary<String, String> dictionary = new Dictionary<String, String>() {
				{ "Key1", "Value1" },
				{ "Key2", "Value2" },
				{ "Key3", "Value3" }
			};
			ICollection<KeyValuePair<String, String>> target = new NonNullDictionary<String, String>(dictionary);
			KeyValuePair<String, String>[] array = new KeyValuePair<String, String>[dictionary.Count - 2];
			Int32 arrayIndex = 1;

			target.CopyTo(array, arrayIndex);
		}

		[TestMethod()]
		[Description("GetEnumerator() method for the optimal path.")]
		public void NonNullDictionary_Unit_GetEnumerator1_Optimal() {
			IDictionary<String, String> dictionary = new Dictionary<String, String>() {
				{ "Key1", "Value1" },
				{ "Key2", "Value2" },
				{ "Key3", "Value3" }
			};
			NonNullDictionary<String, String> target = new NonNullDictionary<String, String>(dictionary);

			using (Dictionary<String, String>.Enumerator actual = target.GetEnumerator()) {
				Int32 count = 0;
				while (actual.MoveNext()) {
					count++;
					Assert.IsTrue(dictionary.Contains(actual.Current));
				}
				Assert.AreEqual(dictionary.Count, count);
			}
		}

		[TestMethod()]
		[Description("IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator() method for the optimal path.")]
		public void NonNullDictionary_Unit_GetEnumerator2_Optimal() {
			IDictionary<String, String> dictionary = new Dictionary<String, String>() {
				{ "Key1", "Value1" },
				{ "Key2", "Value2" },
				{ "Key3", "Value3" }
			};
			IEnumerable<KeyValuePair<String, String>> target = new NonNullDictionary<String, String>(dictionary);

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
		public void NonNullDictionary_Unit_GetEnumerator3_Optimal() {
			IDictionary<String, String> dictionary = new Dictionary<String, String>() {
				{ "Key1", "Value1" },
				{ "Key2", "Value2" },
				{ "Key3", "Value3" }
			};
			IEnumerable target = new NonNullDictionary<String, String>(dictionary);

			IEnumerator actual = target.GetEnumerator();
			Int32 count = 0;
			while (actual.MoveNext()) {
				count++;
				Assert.IsTrue(dictionary.Contains((KeyValuePair<String, String>)actual.Current));
			}
			Assert.AreEqual(dictionary.Count, count);
		}

		[TestMethod()]
		[Description("Remove(TKey) method for the optimal path.")]
		public void NonNullDictionary_Unit_Remove1_Optimal() {
			IDictionary<String, String> dictionary = new Dictionary<String, String>() {
				{ "Key1", "Value1" },
				{ "Key2", "Value2" },
				{ "Key3", "Value3" }
			};
			NonNullDictionary<String, String> target = new NonNullDictionary<String, String>(dictionary);
			String key = dictionary.Keys.First();

			Boolean actual = target.Remove(key);
			Assert.AreEqual(true, actual);
			Assert.IsFalse(target.ContainsKey(key));
		}
		[TestMethod()]
		[Description("Remove(TKey) method when 'key' is a null reference.")]
		[ExpectedException(typeof(ArgumentNullException))]
		public void NonNullDictionary_Unit_Remove1_KeyIsNull() {
			IDictionary<String, String> dictionary = new Dictionary<String, String>() {
				{ "Key1", "Value1" },
				{ "Key2", "Value2" },
				{ "Key3", "Value3" }
			};
			NonNullDictionary<String, String> target = new NonNullDictionary<String, String>(dictionary);
			String key = null;

			target.Remove(key);
		}
		[TestMethod()]
		[Description("Remove(TKey) method when 'key' does not exist.")]
		public void NonNullDictionary_Unit_Remove1_KeyDoesNotExist() {
			IDictionary<String, String> dictionary = new Dictionary<String, String>() {
				{ "Key1", "Value1" },
				{ "Key2", "Value2" },
				{ "Key3", "Value3" }
			};
			NonNullDictionary<String, String> target = new NonNullDictionary<String, String>(dictionary);
			String key = "MyKey";

			Boolean actual = target.Remove(key);
			Assert.AreEqual(false, actual);
		}

		[TestMethod()]
		[Description("ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue>) method for the optimal path.")]
		public void NonNullDictionary_Unit_Remove2_Optimal() {
			IDictionary<String, String> dictionary = new Dictionary<String, String>() {
				{ "Key1", "Value1" },
				{ "Key2", "Value2" },
				{ "Key3", "Value3" }
			};
			ICollection<KeyValuePair<String, String>> target = new NonNullDictionary<String, String>(dictionary);
			String key = dictionary.Keys.First();
			String value = dictionary[dictionary.Keys.First()];
			KeyValuePair<String, String> item = new KeyValuePair<String, String>(key, value);

			Boolean actual = target.Remove(item);
			Assert.AreEqual(true, actual);
			Assert.IsFalse(target.Contains(item));
		}
		[TestMethod()]
		[Description("ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue>) method when the key is a null reference.")]
		[ExpectedException(typeof(ArgumentNullException))]
		public void NonNullDictionary_Unit_Remove2_KeyIsNull() {
			IDictionary<String, String> dictionary = new Dictionary<String, String>() {
				{ "Key1", "Value1" },
				{ "Key2", "Value2" },
				{ "Key3", "Value3" }
			};
			ICollection<KeyValuePair<String, String>> target = new NonNullDictionary<String, String>(dictionary);
			String key = null;
			String value = dictionary[dictionary.Keys.First()];
			KeyValuePair<String, String> item = new KeyValuePair<String, String>(key, value);

			Boolean actual = target.Remove(item);
			Assert.AreEqual(false, actual);
		}
		[TestMethod()]
		[Description("ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue>) method when the key does not exist.")]
		public void NonNullDictionary_Unit_Remove2_KeyDoesNotExist() {
			IDictionary<String, String> dictionary = new Dictionary<String, String>() {
				{ "Key1", "Value1" },
				{ "Key2", "Value2" },
				{ "Key3", "Value3" }
			};
			ICollection<KeyValuePair<String, String>> target = new NonNullDictionary<String, String>(dictionary);
			String key = "MyKey";
			String value = dictionary[dictionary.Keys.First()];
			KeyValuePair<String, String> item = new KeyValuePair<String, String>(key, value);

			Boolean actual = target.Remove(item);
			Assert.AreEqual(false, actual);
		}
		[TestMethod()]
		[Description("ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue>) method when the value does not exist.")]
		public void NonNullDictionary_Unit_Remove2_ValueDoesNotExist() {
			IDictionary<String, String> dictionary = new Dictionary<String, String>() {
				{ "Key1", "Value1" },
				{ "Key2", "Value2" },
				{ "Key3", "Value3" }
			};
			ICollection<KeyValuePair<String, String>> target = new NonNullDictionary<String, String>(dictionary);
			String key = dictionary.Keys.First();
			String value = "MyValue";
			KeyValuePair<String, String> item = new KeyValuePair<String, String>(key, value);

			Boolean actual = target.Remove(item);
			Assert.AreEqual(false, actual);
		}

		[TestMethod()]
		[Description("TryGetValue(TKey, &TValue) method for the optimal path.")]
		public void NonNullDictionary_Unit_TryGetValue_Optimal() {
			IDictionary<String, String> dictionary = new Dictionary<String, String>() {
				{ "Key1", "Value1" },
				{ "Key2", "Value2" },
				{ "Key3", "Value3" }
			};
			NonNullDictionary<String, String> target = new NonNullDictionary<String, String>(dictionary);
			String key = dictionary.Keys.First();
			String value;

			Boolean actual = target.TryGetValue(key, out value);
			Assert.AreEqual(true, actual);
			Assert.AreEqual(dictionary[key], value);
		}
		[TestMethod()]
		[Description("TryGetValue(TKey, &TValue) method when 'key' is a null reference.")]
		[ExpectedException(typeof(ArgumentNullException))]
		public void NonNullDictionary_Unit_TryGetValue_KeyIsNull() {
			IDictionary<String, String> dictionary = new Dictionary<String, String>() {
				{ "Key1", "Value1" },
				{ "Key2", "Value2" },
				{ "Key3", "Value3" }
			};
			NonNullDictionary<String, String> target = new NonNullDictionary<String, String>(dictionary);
			String key = null;
			String value;

			target.TryGetValue(key, out value);
		}
		[TestMethod()]
		[Description("TryGetValue(TKey, &TValue) method when 'key' does not exist.")]
		public void NonNullDictionary_Unit_TryGetValue_KeyDoesNotExist() {
			IDictionary<String, String> dictionary = new Dictionary<String, String>() {
				{ "Key1", "Value1" },
				{ "Key2", "Value2" },
				{ "Key3", "Value3" }
			};
			NonNullDictionary<String, String> target = new NonNullDictionary<String, String>(dictionary);
			String key = "MyKey";
			String value;

			Boolean actual = target.TryGetValue(key, out value);
			Assert.AreEqual(false, actual);
			Assert.IsNull(value);
		}
	}
}