using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Vizistata.Linq {
	/// <summary>
	/// Provides extensions for the <see cref="T:IEnumerable&lt;T&gt;"/> interface.  This class may not be inherited.
	/// </summary>
	public static class EnumerableExtensions {
	// Methods
		/// <summary>
		/// Creates an array from an <see cref="T:IEnumerable&lt;T&gt;"/>.
		/// </summary>
		/// <typeparam name="TSource">The type of element in the source.</typeparam>
		/// <typeparam name="TResult">The type of element in the array created.</typeparam>
		/// <param name="instance">The enumerable collection to convert to an array.</param>
		/// <param name="selector">The projection function to invoke to convert the elements in the collection.</param>
		/// <returns>The array of objects created.</returns>
		/// <exception cref="System.ArgumentNullException"><paramref name="instance"/> is a null reference.
		/// -or- <paramref name="selector"/> is a null reference.</exception>
		public static TResult[] ToArray<TSource, TResult>(this IEnumerable<TSource> instance, Func<TSource, TResult> selector) {
			if (instance == null) {
				throw new ArgumentNullException("instance");
			}
			if (selector == null) {
				throw new ArgumentNullException("selector");
			}

			return instance.Select(selector).ToArray();
		}
		/// <summary>
		/// Creates a <see cref="T:Collection&lt;T&gt;"/> from an <see cref="T:IEnumerable&lt;T&gt;"/>.
		/// </summary>
		/// <typeparam name="T">The type of element.</typeparam>
		/// <param name="instance">The object on which to invoke the method.</param>
		/// <returns>A reference to the collection created.</returns>
		/// <exception cref="System.ArgumentNullException"><paramref name="instance"/> is a null reference.</exception>
		public static Collection<T> ToCollection<T>(this IEnumerable<T> instance) {
			if (instance == null) {
				throw new ArgumentNullException("instance");
			}
			return new Collection<T>(instance.ToList());
		}
		/// <summary>
		/// Creates a <see cref="T:ReadOnlyCollection&lt;Tgt;"/> from the enumerable collection.
		/// </summary>
		/// <typeparam name="T">The type of element in the collection.</typeparam>
		/// <param name="instance">The enumerable collection from which to create the <see cref="T:ReadOnlyCollection&lt;Tgt;"/>.</param>
		/// <returns>A reference to the <see cref="T:ReadOnlyCollection&lt;Tgt;"/> created.</returns>
		/// <exception cref="System.ArgumentNullException"><paramref name="instance"/> is a null reference.</exception>
		public static ReadOnlyCollection<T> ToReadOnlyCollection<T>(this IEnumerable<T> instance) {
			if (instance == null) {
				throw new ArgumentNullException("instance");
			}
			return new ReadOnlyCollection<T>(instance.ToArray());
		}
	}
}
