using System;
using System.Collections.Generic;
using Vizistata.Collections;

namespace Vizistata.Linq {
	/// <summary>
	/// Provides extension methods for the <see cref="T:IDictionary&lt;TKey,TValue&gt;"/> type.  This class may not be inherited.
	/// </summary>
	public static class DictionaryExtensions {
	// Methods
		/// <summary>
		/// Returns a read-only version of the dictionary.
		/// </summary>
		/// <typeparam name="TKey">The type of keys in the dictionary.</typeparam>
		/// <typeparam name="TValue">The type of values in the dictionary.</typeparam>
		/// <param name="dictionary">The dictionary to return as read-only.</param>
		/// <returns>An instance of the <see cref="T:ReadOnlyDictionary&lt;TKey,TValue&gt;"/> created.</returns>
		/// <exception cref="System.ArgumentNullException"><paramref name="dictionary"/> is a null reference.</exception>
		public static ReadOnlyDictionary<TKey, TValue> AsReadOnly<TKey, TValue>(this IDictionary<TKey, TValue> dictionary) {
			if (dictionary == null) {
				throw new ArgumentNullException("dictionary");
			}

			return new ReadOnlyDictionary<TKey, TValue>(dictionary);
		}
		/// <summary>
		/// Returns the value for the key specified or the default value for <typeparamref name="TValue"/>.
		/// </summary>
		/// <typeparam name="TKey">The type used as the key.</typeparam>
		/// <typeparam name="TValue">The type used as the value.</typeparam>
		/// <param name="dictionary">The instance on which to invoke the method.</param>
		/// <param name="key">Specifies the value to retrive.</param>
		/// <returns>The value for the key specified.  If the value doesn't exist, the default value for <typeparamref name="TValue"/> is returned instead.</returns>
		/// <exception cref="System.ArgumentNullException"><paramref name="dictionary"/> is a null reference.</exception>
		public static TValue GetValueOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key) {
			if (dictionary == null) {
				throw new ArgumentNullException("dictionary");
			}

			if (key != null) {
				if (dictionary.ContainsKey(key)) {
					return dictionary[key];
				}
			}
			return default(TValue);
		}
	}
}
