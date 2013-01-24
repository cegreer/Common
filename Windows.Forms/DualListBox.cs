using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace Vizistata.Windows.Forms {
	/// <summary>
	/// Represents a pair of list boxes that allow a user to choose items by moving items between the boxes.
	/// </summary>
	public partial class DualListBox : UserControl {
	// Fields
		/// <summary>
		/// Contains the lists of items.  This field is read-only.
		/// </summary>
		private readonly DualListBoxItemCollection _items;

	// Constructors
		/// <summary>
		/// Initializes a default instance of the <see cref="T:DualListBox"/> class.
		/// </summary>
		public DualListBox() : this(null) { }
		/// <summary>
		/// Initializes an instance of the <see cref="T:DualListBox"/> using the comparer specified.
		/// </summary>
		/// <param name="comparer">The object used to compare objects in the lists.</param>
		public DualListBox(IComparer<Object> comparer)
			: base() {
			this.InitializeComponent();
			this._items = new DualListBoxItemCollection(comparer);
			this._items.ListChanged += this.Items_ListChanged;
		}

	// Properties
		/// <summary>
		/// Gets the lists of items in this instance.
		/// </summary>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		public DualListBoxItemCollection Items {
			get { return this._items; }
		}

	// Methods
		/// <summary>
		/// Chooses all items.
		/// </summary>
		private void ChooseAllItems() {
			if (this.Items.NotChosen.Count > 0) {
				this.Items.MoveAllToChosen();
			}
		}
		/// <summary>
		/// Chooses all items that are selected in the "not chosen" list box.
		/// </summary>
		private void ChooseSelectedItems() {
			if (this._notChosenListBox.SelectedIndices.Count > 0) {
				Int32[] selectedIndexes = this._notChosenListBox.SelectedIndices
					.Cast<Int32>()
					.OrderByDescending(value => value)
					.ToArray();
				foreach (Int32 i in selectedIndexes) {
					this.Items.MoveToChosen(i);
				}
			}
		}
		/// <summary>
		/// Refreshes the items displayed in the list boxes.
		/// </summary>
		private void RefreshListBoxes() {
			this._notChosenListBox.Items.Clear();
			this._notChosenListBox.Items.AddRange(this.Items.NotChosen.ToArray());

			this._chosenListBox.Items.Clear();
			this._chosenListBox.Items.AddRange(this.Items.Chosen.ToArray());

			this.SetButtonsEnabled();
		}
		/// <summary>
		/// Sets the <see cref="P:Control.Enabled"/> properties for the buttons.
		/// </summary>
		private void SetButtonsEnabled() {
			this._chooseAllButton.Enabled = this.Items.NotChosen.Count > 0;
			this._chooseSelectedButton.Enabled = this._notChosenListBox.SelectedIndices.Count > 0;
			this._removeAllChosenButton.Enabled = this.Items.Chosen.Count > 0;
			this._removeSelectedChosenButton.Enabled = this._chosenListBox.SelectedIndices.Count > 0;
		}
		/// <summary>
		/// Removes all chosen items.
		/// </summary>
		private void UnchooseAllItems() {
			if (this.Items.Chosen.Count > 0) {
				this.Items.MoveAllToNotChosen();
			}
		}
		/// <summary>
		/// Removes all items that are selected in the "chosen" list box.
		/// </summary>
		private void UnchooseSelectedItems() {
			if (this._chosenListBox.SelectedIndices.Count > 0) {
				Int32[] selectedIndexes = this._chosenListBox.SelectedIndices
					.Cast<Int32>()
					.OrderByDescending(value => value)
					.ToArray();
				foreach (Int32 i in selectedIndexes) {
					this.Items.MoveToNotChosen(i);
				}
			}
		}

	// Event Handler Methods
		/// <summary>
		/// Handles the <see cref="E:Button.Click"/> event for the "choose all items" button.
		/// </summary>
		/// <param name="sender">The "choose all items" button.</param>
		/// <param name="e">Provides information about the event.</param>
		private void _chooseAllButton_Click(Object sender, EventArgs e) {
			this.ChooseAllItems();
		}
		/// <summary>
		/// Handles the <see cref="E:Button.Click"/> event for the "choose selected items" button.
		/// </summary>
		/// <param name="sender">The "choose selected items" button.</param>
		/// <param name="e">Provides information about the event.</param>
		private void _chooseSelectedButton_Click(Object sender, EventArgs e) {
			this.ChooseSelectedItems();
		}
		/// <summary>
		/// Handles the <see cref="E:ListBox.SelectedIndexChanged"/> event for the "chosen items" list box.
		/// </summary>
		/// <param name="sender">The "chosen items" list box.</param>
		/// <param name="e">Provides information about the event.</param>
		private void _chosenListBox_SelectedIndexChanged(Object sender, EventArgs e) {
			this.SetButtonsEnabled();
		}
		/// <summary>
		/// Handles the <see cref="E:ListBox.SelectedIndexChanged"/> event for the "not chosen items" list box.
		/// </summary>
		/// <param name="sender">The "not chosen items" list box.</param>
		/// <param name="e">Provides information about the event.</param>
		private void _notChosenListBox_SelectedIndexChanged(Object sender, EventArgs e) {
			this.SetButtonsEnabled();
		}
		/// <summary>
		/// Handles the <see cref="E:Button.Click"/> event for the "remove all chosen items" button.
		/// </summary>
		/// <param name="sender">The "remove all chosen items" button.</param>
		/// <param name="e">Provides information about the event.</param>
		private void _removeAllChosenButton_Click(Object sender, EventArgs e) {
			this.UnchooseAllItems();
		}
		/// <summary>
		/// Handles the <see cref="E:Button.Click"/> event for the "remove all selected chosen items" button.
		/// </summary>
		/// <param name="sender">The "remove all selected chosen items" button.</param>
		/// <param name="e">Provides information about the event.</param>
		private void _removeSelectedChosenButton_Click(Object sender, EventArgs e) {
			this.UnchooseSelectedItems();
		}
		/// <summary>
		/// Handles the <see cref="E:DualListBoxItemCollection.ListChanged"/> event for the items.
		/// </summary>
		/// <param name="sender">The items collection.</param>
		/// <param name="e">Provides information about the event.</param>
		private void Items_ListChanged(Object sender, EventArgs e) {
			this.RefreshListBoxes();
		}
	}
}
