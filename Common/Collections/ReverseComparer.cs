using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

using ImmutableObject = System.ComponentModel.ImmutableObjectAttribute;

namespace Vizistata.Collections {
	/// <summary>
	/// A comparer that reverses the order of the default comparer.  This class may not be inherited.
	/// </summary>
	[Serializable()]
	[ImmutableObject(true)]
	public sealed class ReverseComparer<T> : Object, IComparer<T> {
	// Fields
		/// <summary>
		/// The comparer to reverse.  This field is read-only.
		/// </summary>
		private readonly IComparer<T> _comparer;

	// Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:ReverseComparer"/> class.
		/// </summary>
		/// <param name="comparer">The comparer to reverse.</param>
		/// <exception cref="System.ArgumentNullException"><paramref name="comparer"/> is a null reference.</exception>
		public ReverseComparer(IComparer<T> comparer)
			: base() {
			if (comparer == null) {
				throw new ArgumentNullException("comparer");
			}
			this._comparer = comparer;
		}

	// Properties
		/// <summary>
		/// Gets the default reversed sort order comparer.
		/// </summary>
		[SuppressMessage("Microsoft.Design", "CA1000:DoNotDeclareStaticMembersOnGenericTypes", Justification = "This member follows the pattern defined by Comparer<T>.")]
		public static IComparer<T> Default {
			get { return new ReverseComparer<T>(Comparer<T>.Default); }
		}

	// Methods
		/// <summary>
		/// Compares two objects and returns a value indicating whether one is less than, equal to, or greater than the other.
		/// </summary>
		/// <param name="x">The first object to compare.</param>
		/// <param name="y">The second object to compare.</param>
		/// <returns>A signed integer that indicates the relative values of <paramref name="x"/> and <paramref name="y"/>, as shown in the following information.
		/// Less than zero: <paramref name="x"/> is less than <paramref name="y"/>.
		/// Zero: <paramref name="x"/> equals <paramref name="y"/>.
		/// Greater than zero: <paramref name="x"/> is greater than <paramref name="y"/>.</returns>
		public Int32 Compare(T x, T y) {
			return -this._comparer.Compare(x, y);
		}
	}
}
