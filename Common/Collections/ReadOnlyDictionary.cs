using System;
using System.Collections.Generic;
using System.Diagnostics;

using IEnumerable = System.Collections.IEnumerable;
using IEnumerator = System.Collections.IEnumerator;
using ImmutableObject = System.ComponentModel.ImmutableObjectAttribute;

namespace Vizistata.Collections {
	/// <summary>
	/// Represents a read-only dictionary.
	/// </summary>
	/// <typeparam name="TKey">The type of object used as keys.</typeparam>
	/// <typeparam name="TValue">The type of object used as values.</typeparam>
	[Serializable()]
	[ImmutableObject(true)]
	public class ReadOnlyDictionary<TKey, TValue> : Object, IDictionary<TKey, TValue> {
	// Fields
		/// <summary>
		/// Contains the actual elements for the read-only dictionary.  This field is read-only.
		/// </summary>
		private readonly IDictionary<TKey, TValue> _innerDictionary;

	// Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:ReadOnlyDictionary&lt;TKey,TValue&gt;"/> class.
		/// </summary>
		/// <param name="dictionary">The dictionary to wrap.</param>
		/// <exception cref="System.ArgumentNullException"><paramref name="dictionary"/> is a null reference.</exception>
		public ReadOnlyDictionary(IDictionary<TKey, TValue> dictionary)
			: base() {
			if (dictionary == null) {
				throw new ArgumentNullException("dictionary");
			}

			this._innerDictionary = dictionary;
		}

	// Properties
		/// <summary>
		/// Gets the element with the specified key.
		/// </summary>
		/// <param name="key">The key of the element to get.</param>
		/// <returns>The element with the specified key.</returns>
		/// <exception cref="System.ArgumentNullException"><paramref name="key"/> is a null reference.</exception>
		/// <exception cref="System.Collections.Generic.KeyNotFoundException"><paramref name="key"/> is not found.</exception>
		public TValue this[TKey key] {
			get { return this.InnerDictionary[key]; }
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
		protected IDictionary<TKey, TValue> InnerDictionary {
			get {
				Debug.Assert(this._innerDictionary != null);
				return this._innerDictionary;
			}
		}
		/// <summary>
		/// Gets an <see cref="T:ICollection&lt;T&gt;"/> containing the keys of the dictionary.
		/// </summary>
		public ICollection<TKey> Keys {
			get { return this.InnerDictionary.Keys; }
		}
		/// <summary>
		/// Gets an <see cref="T:ICollection&lt;T&gt;"/> containing the values in the dictionary.
		/// </summary>
		public ICollection<TValue> Values {
			get { return this.InnerDictionary.Values; }
		}

	// Methods
		/// <summary>
		/// Determines whether the dictionary contains a specific value.
		/// </summary>
		/// <param name="item">The object to locate in the dictionary.</param>
		/// <returns><c>true</c> if item is found in the dictionary; otherwise, <c>false</c>.</returns>
		public Boolean Contains(KeyValuePair<TKey, TValue> item) {
			return this.InnerDictionary.Contains(item);
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
		/// Copies the elements of the dictionary to an <see cref="T:Array"/>, starting at a particular <see cref="T:Array"/> index.
		/// </summary>
		/// <param name="array">The one-dimensional <see cref="T:Array"/> that is the destination of the elements copied from dictionary.  The <see cref="T:Array"/> must have zero-based indexing.</param>
		/// <param name="arrayIndex">The zero-based index in array at which copying begins.</param>
		/// <exception cref="System.ArgumentNullException"><paramref name="array"/> is a null reference.</exception>
		/// <exception cref="System.ArgumentOutOfRangeException"><paramref name="arrayIndex"/> is less than 0.</exception>
		/// <exception cref="System.ArgumentException"><paramref name="array"/> is multidimensional.
		/// -or- The number of elements in the source dictionary is greater than the available space from <paramref name="arrayIndex"/> to the end of the destination array.</exception>
		public void CopyTo(KeyValuePair<TKey, TValue>[] array, Int32 arrayIndex) {
			this.InnerDictionary.CopyTo(array, arrayIndex);
		}
		/// <summary>
		/// Returns an enumerator that iterates through the collection.
		/// </summary>
		/// <returns>A <see cref="T:IEnumerator&lt;T&gt;"/> that can be used to iterate through the collection.</returns>
		public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator() {
			return this.InnerDictionary.GetEnumerator();
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

		#region IDictionary<TKey,TValue> Members (explicit)
		TValue IDictionary<TKey, TValue>.this[TKey key] {
			get { return this[key]; }
			set { throw new NotSupportedException(); }
		}

		void IDictionary<TKey, TValue>.Add(TKey key, TValue value) {
			throw new NotSupportedException();
		}
		Boolean IDictionary<TKey, TValue>.Remove(TKey key) {
			throw new NotSupportedException();
		}
		#endregion

		#region ICollection<KeyValuePair<TKey,TValue>> Members (explicit)
		Boolean ICollection<KeyValuePair<TKey,TValue>>.IsReadOnly {
			get { return true; }
		}

		void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> item) {
			throw new NotSupportedException();
		}
		void ICollection<KeyValuePair<TKey,TValue>>.Clear() {
			throw new NotSupportedException();
		}
		bool ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> item) {
			throw new NotSupportedException();
		}
		#endregion

		#region IEnumerable Members (explicit)
		IEnumerator IEnumerable.GetEnumerator() {
			return this.GetEnumerator();
		}
		#endregion
	}
}
