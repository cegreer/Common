using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Vizistata.Linq;

using IEnumerable = System.Collections.IEnumerable;
using IEnumerator = System.Collections.IEnumerator;

namespace Vizistata.Windows.Forms {
	/// <summary>
	/// A strongly-typed collection of items used to manage "chosen" and "not chosen" items in a <see cref="T:DualListBox"/> control.  This class may not be inherited.
	/// </summary>
	public sealed class DualListBoxItemCollection : Object, IEnumerable<Object> {
	// Fields
		/// <summary>
		/// The list of indices that are currently chosen.  This field is read-only.
		/// </summary>
		private readonly List<Int32> _chosenIndexes = new List<Int32>();
		/// <summary>
		/// The comparer to use to sort items in the list.
		/// </summary>
		private IComparer<Object> _comparer;
		/// <summary>
		/// The list of all items.  This field is read-only.
		/// </summary>
		private readonly List<Object> _items = new List<Object>();
		/// <summary>
		/// Contains the delegates for the <see cref="E:ListChanged"/> event.
		/// </summary>
		private EventHandler _listChanged;

	// Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:DualListBoxItemCollection"/> using the default comparer.
		/// </summary>
		public DualListBoxItemCollection() : this(null) { }
		/// <summary>
		/// Initializes a new instance of the <see cref="T:DualListBoxItemCollection"/> using the comparer specified.
		/// </summary>
		/// <param name="comparer">The object used to sort items in the collection.</param>
		public DualListBoxItemCollection(IComparer<Object> comparer)
			: base() {
			this._comparer = comparer ?? Comparer<Object>.Default;
		}

	// Properties
		/// <summary>
		/// Gets the read-only collection of all items in the collection.
		/// </summary>
		public ReadOnlyCollection<Object> AllItems {
			get { return this._items.AsReadOnly(); }
		}
		/// <summary>
		/// Gets the read-only collection of items that have been chosen in the collection.
		/// </summary>
		public ReadOnlyCollection<Object> Chosen {
			get {
				IEnumerable<Object> chosen = this._items.Where((item, index) => this._chosenIndexes.Contains(index));
				ReadOnlyCollection<Object> chosenCollection = chosen.ToReadOnlyCollection();
				return chosenCollection;
			}
		}
		/// <summary>
		/// Gets or sets the comparer to use to sort the items in the collection.
		/// </summary>
		public IComparer<Object> Comparer {
			get { return this._comparer; }
			set {
				value = value ?? Comparer<Object>.Default;
				if (this._comparer != value) {
					this._comparer = value;
					this.Sort();
				}
			}
		}
		/// <summary>
		/// Gets the read-only collection of items that have not be chosen in the collection.
		/// </summary>
		public ReadOnlyCollection<Object> NotChosen {
			get {
				IEnumerable<Object> notChosen = this._items.Where((item, index) => !this._chosenIndexes.Contains(index));
				ReadOnlyCollection<Object> notChosenCollection = notChosen.ToReadOnlyCollection();
				return notChosenCollection;
			}
		}

	// Events
		/// <summary>
		/// Occurs when one or more of the lists changes.
		/// </summary>
		/// <exception cref="System.ArgumentNullException">The delegate value is a null reference.</exception>
		public event EventHandler ListChanged {
			add {
				if (value == null) {
					throw new ArgumentNullException("value");
				}
				this._listChanged = (EventHandler)Delegate.Combine(this._listChanged, value);
			}
			remove {
				if (value == null) {
					throw new ArgumentNullException("value");
				}
				this._listChanged = (EventHandler)Delegate.Remove(this._listChanged, value);
			}
		}

	// Methods
		/// <summary>
		/// Adds an item to the collection as "not chosen".
		/// </summary>
		/// <param name="item">The item to add.</param>
		/// <exception cref="System.ArgumentException">The item specified already exists in the collection.</exception>
		public void Add(Object item) {
			this.Add(item, false);
		}
		/// <summary>
		/// Adds an item to the collection.
		/// </summary>
		/// <param name="item">The item to add.</param>
		/// <param name="isChosen"><c>true</c> if the item is chosen; otherwise, <c>false</c>.</param>
		/// <exception cref="System.ArgumentException">The item specified already exists in the collection.</exception>
		public void Add(Object item, Boolean isChosen) {
			if (this._items.Contains(item)) {
				throw new ArgumentException("The item specified already exists in the collection.", "item");
			}

			this._items.Add(item);
			if (isChosen) {
				this._chosenIndexes.Add(this._items.Count - 1);
			}
			this.Sort();
		}
		/// <summary>
		/// Adds a range of objects to the collection.
		/// </summary>
		/// <param name="items">The items to add.</param>
		/// <exception cref="System.ArgumentException"><paramref name="items"/> contains an item that already exists in the collection.
		/// -or- <paramref name="items"/> contains duplicate items.</exception>
		public void AddRange(Object[] items) {
			this.AddRange(items, new Int32[0]);
		}
		/// <summary>
		/// Adds a range of objects to the collection.
		/// </summary>
		/// <param name="items">The items to add.</param>
		/// <param name="chosenIndexes">The indexes in the <paramref name="items"/> array that are chosen.</param>
		/// <exception cref="System.ArgumentException"><paramref name="items"/> contains an item that already exists in the collection.
		/// -or- <paramref name="items"/> contains duplicate items.
		/// -or- <paramref name="chosenIndexes"/> contains an index that is less than 0.
		/// -or- <paramref name="chosenIndexes"/> contains an index that is greater than or equal to the length of <paramref name="items"/>.</exception>
		public void AddRange(Object[] items, Int32[] chosenIndexes) {
			if (items == null || items.Length == 0) {
				return;
			}
			if (items.Any(item => this._items.Contains(item))) {
				throw new ArgumentException("One of the items specified already exists in the collection.", "items");
			}
			for (Int32 i = 0; i < items.Length - 1; i++) {
				for (Int32 j = i + 1; j < items.Length; j++) {
					if (Object.Equals(items[i], items[j])) {
						throw new ArgumentException("The items specified contains at least one duplicate item.", "items");
					}
				}
			}
			if (chosenIndexes != null) {
				if (chosenIndexes.Any(index => index < 0)) {
					throw new ArgumentException("One of the indexes specified is less than 0.", "chosenIndexes");
				}
				else if (chosenIndexes.Any(index => index >= items.Length)) {
					throw new ArgumentException("One of the indexes specified is greater than or equal to the length of the items array specified.", "chosenIndexes");
				}
			}

			for (Int32 i = 0; i < items.Length; i++) {
				Object item = items[i];
				Boolean isChosen = chosenIndexes != null && chosenIndexes.Contains(i);

				this._items.Add(item);
				if (isChosen) {
					this._chosenIndexes.Add(this._items.Count - 1);
				}
			}
			this.Sort();
		}
		/// <summary>
		/// Adds a range of objects to the collection.
		/// </summary>
		/// <param name="notChosenItems">The items that are not chosen.</param>
		/// <param name="chosenItems">The items that are chosen.</param>
		/// <exception cref="System.ArgumentException"><paramref name="notChosenItems"/> contains an item that already exists in the collection.
		/// -or- <paramref name="notChosenItems"/> contains duplicate items.
		/// -or- <paramref name="chosenItems"/> contains an item that already exists in the collection.
		/// -or- <paramref name="chosenItems"/> contains duplicate items.
		/// -or- <paramref name="chosenItems"/> contains an item that already exists in <paramref name="notChosenItems"/>.</exception>
		[SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Justification = "The argument is validated.")]
		[SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "1", Justification = "The argument is validated.")]
		public void AddRange(Object[] notChosenItems, Object[] chosenItems) {
			Boolean isNotChosenEmpty = notChosenItems == null || notChosenItems.Length == 0;
			Boolean isChosenEmpty = chosenItems == null || chosenItems.Length == 0;
			if (isNotChosenEmpty && isChosenEmpty) {
				return;
			}

			// Validate arguments.
			if (!isNotChosenEmpty) {
				if (notChosenItems.Any(item => this._items.Contains(item))) {
					throw new ArgumentException("One of the items specified already exists in the collection.", "notChosenItems");
				}
				if (notChosenItems.Count() != notChosenItems.Distinct().Count()) {
					throw new ArgumentException("The items specified contains at least one duplicate item.", "notChosenItems");
				}
			}
			if (!isChosenEmpty) {
				if (chosenItems.Any(item => this._items.Contains(item))) {
					throw new ArgumentException("One of the items specified already exists in the collection.", "chosenItems");
				}
				if (chosenItems.Count() != chosenItems.Distinct().Count()) {
					throw new ArgumentException("The items specified contains at least one duplicate item.", "chosenItems");
				}

				if (!isNotChosenEmpty) {
					if (notChosenItems.Intersect(chosenItems).Count() > 0) {
						throw new ArgumentException("An item has been duplicated between the two lists.", "chosenItems");
					}
				}
			}

			// Add the items.
			if (!isNotChosenEmpty) {
				foreach (Object item in notChosenItems) {
					this._items.Add(item);
				}
			}
			if (!isChosenEmpty) {
				foreach (Object item in chosenItems) {
					this._items.Add(item);
					this._chosenIndexes.Add(this._items.Count - 1);
				}
			}

			this.Sort();
		}
		/// <summary>
		/// Clears all items from the collections.
		/// </summary>
		public void Clear() {
			if (this._items.Count > 0) {
				this._chosenIndexes.Clear();
				this._items.Clear();
				this.OnListChanged(EventArgs.Empty);
			}
		}
		/// <summary>
		/// Returns a strongly-typed enumerator that can iterate through the items in this instance.
		/// </summary>
		/// <returns>The strongly-typed enumerator created.</returns>
		public IEnumerator<Object> GetEnumerator() {
			return this._items.GetEnumerator();
		}
		/// <summary>
		/// Moves all items to the "chosen" list.
		/// </summary>
		public void MoveAllToChosen() {
			Boolean hasNotChosenItems = this._chosenIndexes.Count < this._items.Count;
			if (hasNotChosenItems) {
				this._chosenIndexes.Clear();
				this._chosenIndexes.AddRange(Enumerable.Range(0, this._items.Count));
				this.OnListChanged(EventArgs.Empty);
			}
		}
		/// <summary>
		/// Moves all items to the "not chosen" list.
		/// </summary>
		public void MoveAllToNotChosen() {
			Boolean hasChosenItems = this._chosenIndexes.Count > 0;
			if (hasChosenItems) {
				this._chosenIndexes.Clear();
				this.OnListChanged(EventArgs.Empty);
			}
		}
		/// <summary>
		/// Moves the item specified to the "chosen" list.
		/// </summary>
		/// <param name="notChosenIndex">The index of the item in the "not chosen" list.</param>
		/// <exception cref="System.ArgumentOutOfRangeException"><paramref name="notChosenIndex"/> is less than 0.
		/// -or- <paramref name="notChosenIndex"/> is greater than or equal to the number of items in the collection in the <see cref="P:NotChosen"/> property.</exception>
		public void MoveToChosen(Int32 notChosenIndex) {
			if (notChosenIndex < 0) {
				throw new ArgumentOutOfRangeException("notChosenIndex");
			}
			ReadOnlyCollection<Object> notChosen = this.NotChosen;
			if (notChosenIndex >= notChosen.Count) {
				throw new ArgumentOutOfRangeException("notChosenIndex");
			}

			Int32 index = this._items.IndexOf(notChosen[notChosenIndex]);
			if (!this._chosenIndexes.Contains(index)) {
				this._chosenIndexes.Add(index);
				this._chosenIndexes.Sort();
				this.OnListChanged(EventArgs.Empty);
			}
		}
		/// <summary>
		/// Moves the item specified to the "not chosen" list.
		/// </summary>
		/// <param name="chosenIndex">The index of the item in the "chosen" list.</param>
		/// <exception cref="System.ArgumentOutOfRangeException"><paramref name="chosenIndex"/> is less than 0.
		/// -or- <paramref name="chosenIndex"/> is greater than or equal to the number of items in the collection in the <see cref="P:Chosen"/> property.</exception>
		public void MoveToNotChosen(Int32 chosenIndex) {
			if (chosenIndex < 0) {
				throw new ArgumentOutOfRangeException("chosenIndex");
			}
			ReadOnlyCollection<Object> chosen = this.Chosen;
			if (chosenIndex >= chosen.Count) {
				throw new ArgumentOutOfRangeException("chosenIndex");
			}

			Int32 index = this._items.IndexOf(chosen[chosenIndex]);
			if (this._chosenIndexes.Contains(index)) {
				this._chosenIndexes.Remove(index);
				this.OnListChanged(EventArgs.Empty);
			}
		}
		/// <summary>
		/// Raises the <see cref="E:ListChanged"/> event.
		/// </summary>
		/// <param name="e">Provides information about the event.</param>
		private void OnListChanged(EventArgs e) {
			EventHandler eventHandler = this._listChanged;
			if (eventHandler != null) {
				eventHandler(this, e);
			}
		}
		/// <summary>
		/// Removes the item specified.
		/// </summary>
		/// <param name="item">The item to remove.</param>
		/// <returns><c>true</c> if the item was found and removed; otherwise, <c>false</c>.</returns>
		public Boolean Remove(Object item) {
			if (this._items.Contains(item)) {
				List<Object> chosenList = this.Chosen.ToList();
				if (chosenList.Contains(item)) {
					chosenList.Remove(item);
				}

				this._items.Remove(item);

				this._chosenIndexes.Clear();
				foreach (Object chosenItem in chosenList) {
					this._chosenIndexes.Add(this._items.IndexOf(chosenItem));
				}
				this.OnListChanged(EventArgs.Empty);
				return true;
			}
			return false;
		}
		/// <summary>
		/// Sets the items specified to the "chosen" state.
		/// </summary>
		/// <param name="items">The items to set.</param>
		/// <exception cref="System.ArgumentException"><paramref name="items"/> contains an element that is not contained in the <see cref="P:NotChosen"/> property.</exception>
		public void SetChosen(params Object[] items) {
			if (items == null || items.Length == 0) {
				return;
			}

			IEnumerable<Object> distinctItems = items.Distinct();
			IEnumerable<Object> notChosenItems = this.NotChosen;
			if (distinctItems.Any(item => !notChosenItems.Contains(item))) {
				throw new ArgumentException("The array contains an element that is not contained in the list of items that are not chosen.", "items");
			}

			foreach (Object item in distinctItems) {
				Int32 index = this._items.IndexOf(item);
				if (!this._chosenIndexes.Contains(index)) {
					this._chosenIndexes.Add(index);
				}
			}
			this.OnListChanged(EventArgs.Empty);
		}
		/// <summary>
		/// Sorts the inner list.
		/// </summary>
		private void Sort() {
			Debug.Assert(this._comparer != null);

			IEnumerable<Object> previouslyChosenItems = this.Chosen;
			this._items.Sort(this._comparer);

			this._chosenIndexes.Clear();
			foreach (Object chosenItem in previouslyChosenItems) {
				this._chosenIndexes.Add(this._items.IndexOf(chosenItem));
			}
			this.OnListChanged(EventArgs.Empty);
		}

		#region IEnumerable Members (explicit)
		IEnumerator IEnumerable.GetEnumerator() {
			return this.GetEnumerator();
		}
		#endregion
	}
}
