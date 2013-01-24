using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;

using IEnumerable = System.Collections.IEnumerable;
using IEnumerator = System.Collections.IEnumerator;

namespace Vizistata.Collections {
	/// <summary>
	/// Represents a dictionary that cannot have null values.  This class may not be inherited.
	/// </summary>
	/// <typeparam name="TKey">The type of keys in the dictionary.</typeparam>
	/// <typeparam name="TValue">The type of values in the dictionary.</typeparam>
	[Serializable()]
	public sealed class NonNullDictionary<TKey, TValue> : Object, IDictionary<TKey, TValue> where TValue : class {
	// Constants
		/// <summary>
		/// The default capacity = 16.
		/// </summary>
		private const Int32 DefaultCapacity = 16;

	// Fields
		/// <summary>
		/// The inner dictionary.  This field is read-only.
		/// </summary>
		/// <remarks>Do not use this field directly.  Access the <see cref="P:InnerDictionary"/> property instead.</remarks>
		private readonly Dictionary<TKey, TValue> _innerDictionary;

	// Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:NonNullDictionary&lt;TKey,TValue&gt;"/> class.
		/// </summary>
		public NonNullDictionary() : this(DefaultCapacity, (IEqualityComparer<TKey>)null) { }
		/// <summary>
		/// Initializes a new instance of the <see cref="T:NonNullDictionary&lt;TKey,TValue&gt;"/> class.
		/// </summary>
		/// <param name="capacity">The initial number of elements that the <see cref="T:NonNullDictionary&lt;TKey,TValue&gt;"/> can contain.</param>
		/// <exception cref="System.ArgumentOutOfRangeException"><paramref name="capacity"/> is less than 0.</exception>
		public NonNullDictionary(Int32 capacity) : this(capacity, (IEqualityComparer<TKey>)null) { }
		/// <summary>
		/// Initializes a new instance of the <see cref="T:NonNullDictionary&lt;TKey,TValue&gt;"/> class.
		/// </summary>
		/// <param name="comparer">The <see cref="T:IEqualityComparer&lt;T&gt;"/> implementation to use when comparing keys, or null to use the default <see cref="T:EqualityComparer&lt;T&gt;"/> for the type of the key.</param>
		public NonNullDictionary(IEqualityComparer<TKey> comparer) : this(DefaultCapacity, comparer) { }
		/// <summary>
		/// Initializes a new instance of the <see cref="T:NonNullDictionary&lt;TKey,TValue&gt;"/> class.
		/// </summary>
		/// <param name="capacity">The initial number of elements that the <see cref="T:NonNullDictionary&lt;TKey,TValue&gt;"/> can contain.</param>
		/// <param name="comparer">The <see cref="T:IEqualityComparer&lt;T&gt;"/> implementation to use when comparing keys, or null to use the default <see cref="T:EqualityComparer&lt;T&gt;"/> for the type of the key.</param>
		/// <exception cref="System.ArgumentOutOfRangeException"><paramref name="capacity"/> is less than 0.</exception>
		public NonNullDictionary(Int32 capacity, IEqualityComparer<TKey> comparer)
			: base() {
			this._innerDictionary = new Dictionary<TKey, TValue>(capacity, comparer);
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="T:NonNullDictionary&lt;TKey,TValue&gt;"/> class.
		/// </summary>
		/// <param name="dictionary">The <see cref="T:IDictionary&lt;TKey,TValue&gt;"/> whose elements are copied to the new <see cref="T:NonNullDictionary&lt;TKey,TValue&gt;"/>.</param>
		/// <exception cref="System.ArgumentNullException"><paramref name="dictionary"/> is a null reference.</exception>
		/// <exception cref="System.ArgumentException"><paramref name="dictionary"/> contains one or more duplicate keys.</exception>
		public NonNullDictionary(IDictionary<TKey, TValue> dictionary) : this(dictionary, (IEqualityComparer<TKey>)null) { }
		/// <summary>
		/// Initializes a new instance of the <see cref="T:NonNullDictionary&lt;TKey,TValue&gt;"/> class.
		/// </summary>
		/// <param name="dictionary">The <see cref="T:IDictionary&lt;TKey,TValue&gt;"/> whose elements are copied to the new <see cref="T:NonNullDictionary&lt;TKey,TValue&gt;"/>.</param>
		/// <param name="comparer">The <see cref="T:IEqualityComparer&lt;T&gt;"/> implementation to use when comparing keys, or null to use the default <see cref="T:EqualityComparer&lt;T&gt;"/> for the type of the key.</param>
		/// <exception cref="System.ArgumentNullException"><paramref name="dictionary"/> is a null reference.</exception>
		/// <exception cref="System.ArgumentException"><paramref name="dictionary"/> contains one or more duplicate keys.</exception>
		public NonNullDictionary(IDictionary<TKey, TValue> dictionary, IEqualityComparer<TKey> comparer)
			: base() {
			this._innerDictionary = new Dictionary<TKey, TValue>(dictionary, comparer);
		}

	// Properties
		/// <summary>
		/// Gets or sets the element with the specified key.
		/// </summary>
		/// <param name="key">The key of the element to get or set.</param>
		/// <returns>The element with the specified key.</returns>
		/// <exception cref="System.ArgumentNullException"><paramref name="key"/> is a null reference.
		/// -or- <paramref name="value"/> is a null reference.</exception>
		/// <exception cref="System.Collections.Generic.KeyNotFoundException"><paramref name="key"/> is not found.</exception>
		public TValue this[TKey key] {
			get { return this.InnerDictionary[key]; }
			set {
				if (value == null) {
					throw new ArgumentNullException("value");
				}
				this.InnerDictionary[key] = value;
			}
		}
		/// <summary>
		/// Gets the object that is used to determine equality of keys for the dictionary.
		/// </summary>
		public IEqualityComparer<TKey> Comparer {
			get { return this.InnerDictionary.Comparer; }
		}
		/// <summary>
		/// Gets the number of elements contained in the dictionary.
		/// </summary>
		public Int32 Count {
			get { return this.InnerDictionary.Count; }
		}
		/// <summary>
		/// Gets the inner dictionary.
		/// </summary>
		private Dictionary<TKey, TValue> InnerDictionary {
			get {
				Debug.Assert(this._innerDictionary != null);
				return this._innerDictionary;
			}
		}
		/// <summary>
		/// Gets a collection containing the keys in the dictionary.
		/// </summary>
		public Dictionary<TKey, TValue>.KeyCollection Keys {
			get { return this.InnerDictionary.Keys; }
		}
		/// <summary>
		/// Gets a collection containing the values in the dictionary.
		/// </summary>
		public Dictionary<TKey, TValue>.ValueCollection Values {
			get { return this.InnerDictionary.Values; }
		}

	// Methods
		/// <summary>
		/// Adds the specified key and value to the dictionary.
		/// </summary>
		/// <param name="key">The key of the element to add.</param>
		/// <param name="value">The value of the element to add.</param>
		/// <exception cref="System.ArgumentNullException"><paramref name="key"/> is a null reference.
		/// -or- <paramref name="value"/> is a null reference.</exception>
		/// <exception cref="System.ArgumentException">An element with the same key already exists in the dictionary.</exception>
		public void Add(TKey key, TValue value) {
			if (value == null) {
				throw new ArgumentNullException("value");
			}
			this.InnerDictionary.Add(key, value);
		}
		/// <summary>
		/// Removes all keys and values from the dictionary.
		/// </summary>
		public void Clear() {
			this.InnerDictionary.Clear();
		}
		/// <summary>
		/// Determines whether the dictionary contains an element with the specified key.
		/// </summary>
		/// <param name="key">The key to locate in the dictionary.</param>
		/// <returns><c>true</c> if the dictionary contains an element with the key; otherwise, <c>false</c>.</returns>
		/// <exception cref="System.ArgumentNullException"><paramref name="key"/> is a null reference.</exception>
		public Boolean ContainsKey(TKey key) {
			return this.InnerDictionary.ContainsKey(key);
		}
		/// <summary>
		/// Returns a value indicating if the dictionary contains a specific value.
		/// </summary>
		/// <param name="value">The value to locate in the dictionary.</param>
		/// <returns><c>true</c> if the dictionary contains an element with the specified value; otherwise, <c>false</c>.</returns>
		public Boolean ContainsValue(TValue value) {
			Boolean containsValue = this.InnerDictionary.ContainsValue(value);
			Debug.Assert((value == null && !containsValue) || (value != null));
			return containsValue;
		}
		/// <summary>
		/// Returns an enumerator that iterates through the collection.
		/// </summary>
		/// <returns>A object that can be used to iterate through the collection.</returns>
		public Dictionary<TKey, TValue>.Enumerator GetEnumerator() {
			return this.InnerDictionary.GetEnumerator();
		}
		/// <summary>
		/// Removes the value with the specified key from the dictionary.
		/// </summary>
		/// <param name="key">The key of the element to remove.</param>
		/// <returns><c>true</c> if the element is successfully found and removed; otherwise, <c>false</c>.  This method returns <c>false</c> if <paramref name="key"/> is not found in the dictionary.</returns>
		/// <exception cref="System.ArgumentNullException"><paramref name="key"/> is a null reference.</exception>
		public Boolean Remove(TKey key) {
			return this.InnerDictionary.Remove(key);
		}
		/// <summary>
		/// Gets the value associated with the specified key.
		/// </summary>
		/// <param name="key">The key whose value to get.</param>
		/// <param name="value">When this method returns, the value associated with the specified key, if the key is found; otherwise, the default value for the type of the value parameter.  This parameter is passed uninitialized.</param>
		/// <returns><c>true</c> if this instance contains an element with the specified key; otherwise, <c>false</c>.</returns>
		/// <exception cref="System.ArgumentNullException"><paramref name="key"/> is a null reference.</exception>
		public Boolean TryGetValue(TKey key, out TValue value) {
			return this.InnerDictionary.TryGetValue(key, out value);
		}

		#region IDictionary<TKey, TValue> Members (explicit)
		ICollection<TKey> IDictionary<TKey, TValue>.Keys {
			get { return this.Keys; }
		}
		ICollection<TValue> IDictionary<TKey, TValue>.Values {
			get { return this.Values; }
		}
		#endregion

		#region ICollection<KeyValuePair<TKey, TValue>> Members (explicit)
		Boolean ICollection<KeyValuePair<TKey, TValue>>.IsReadOnly {
			get { return false; }
		}

		void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> item) {
			this.Add(item.Key, item.Value);
		}
		Boolean ICollection<KeyValuePair<TKey, TValue>>.Contains(KeyValuePair<TKey, TValue> item) {
			return ((ICollection<KeyValuePair<TKey, TValue>>)this.InnerDictionary).Contains(item);
		}
		void ICollection<KeyValuePair<TKey, TValue>>.CopyTo(KeyValuePair<TKey, TValue>[] array, Int32 arrayIndex) {
			((IDictionary<TKey, TValue>)this.InnerDictionary).CopyTo(array, arrayIndex);
		}
		Boolean ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> item) {
			return ((ICollection<KeyValuePair<TKey, TValue>>)this.InnerDictionary).Remove(item);
		}
		#endregion

		#region IEnumerable<KeyValuePair<TKey, TValue>> Members (explicit)
		IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator() {
			return this.GetEnumerator();
		}
		#endregion

		#region IEnumerable Members (explicit)
		IEnumerator IEnumerable.GetEnumerator() {
			return this.GetEnumerator();
		}
		#endregion
	}
}
