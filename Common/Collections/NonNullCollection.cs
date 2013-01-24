using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

using Res = Vizistata.Properties.Resources;

namespace Vizistata.Collections {
	/// <summary>
	/// A strongly-typed collection of objects which doesn't allow nulls.  This class may not be inherited.
	/// </summary>
	[Serializable()]
	public sealed class NonNullCollection<T> : Collection<T> where T : class {
	// Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:NonNullCollection"/> class.
		/// </summary>
		public NonNullCollection() : base() { }
		/// <summary>
		/// Initializes a new instance of the <see cref="T:NonNullCollection"/> class.
		/// </summary>
		/// <param name="list">The list that is wrapped by the new collection.</param>
		/// <exception cref="System.ArgumentNullException"><paramref name="list"/> is a null reference.</exception>
		/// <exception cref="System.ArgumentException"><paramref name="list"/> contains a null reference.</exception>
		[SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Justification = "The argument is validated in the base class.")]
		public NonNullCollection(IList<T> list)
			: base(list) {
			Debug.Assert(list != null);

			if (list.Contains(null)) {
				throw new ArgumentException(Res.CollectionContainsNullReferenceMessage, "list");
			}
		}

	// Methods
		/// <summary>
		/// Inserts an element into the <see cref="T:Collection`1"/> at the specified index.
		/// </summary>
		/// <param name="index">The 0-based index at which item should be inserted.</param>
		/// <param name="item">The object to insert.</param>
		/// <exception cref="System.ArgumentNullException"><paramref name="item"/> is a null reference.</exception>
		/// <exception cref="System.ArgumentOutOfRangeException"><paramref name="index"/> is less than zero.
		/// -or- <paramref name="index"/> is greater than <see cref="P:Collection`1.Count"/>.</exception>
		protected override void InsertItem(Int32 index, T item) {
			if (item == null) {
				throw new ArgumentNullException("item");
			}
			base.InsertItem(index, item);
		}
		/// <summary>
		/// Replaces the element at the specified index.
		/// </summary>
		/// <param name="index">The 0-based index of the item which should be set.</param>
		/// <param name="item">The object to set.</param>
		/// <exception cref="System.ArgumentNullException"><paramref name="item"/> is a null reference.</exception>
		/// <exception cref="System.ArgumentOutOfRangeException"><paramref name="index"/> is less than zero.
		/// -or- <paramref name="index"/> is greater than <see cref="P:Collection`1.Count"/>.</exception>
		protected override void SetItem(Int32 index, T item) {
			if (item == null) {
				throw new ArgumentNullException("item");
			}
			base.SetItem(index, item);
		}
	}
}
