using System;

namespace Vizistata {
	/// <summary>
	/// Represents a value that can be rolled back to its original value or committed.  This class may not be inherited.
	/// </summary>
	/// <typeparam name="T">The type of value.</typeparam>
	[Serializable()]
	public sealed class Transactional<T> : Object, IEquatable<Transactional<T>>, IEquatable<T> {
	// Fields
		/// <summary>
		/// The current value.
		/// </summary>
		private T _currentValue;
		/// <summary>
		/// The original value.
		/// </summary>
		private T _originalValue;

	// Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:Transactional&lt;T&gt;"/> class.
		/// </summary>
		public Transactional() : this(default(T), default(T)) { }
		/// <summary>
		/// Initializes a new instance of the <see cref="T:Transactional&lt;T&gt;"/> class.
		/// </summary>
		/// <param name="value">The value.  This will also be used as the original value.</param>
		public Transactional(T value) : this(value, value) { }
		/// <summary>
		/// Initializes a new instance of the <see cref="T:Transactional&lt;T&gt;"/> class.
		/// </summary>
		/// <param name="currentValue">The current value.</param>
		/// <param name="originalValue">The original value.</param>
		public Transactional(T currentValue, T originalValue)
			: base() {
			this._currentValue = currentValue;
			this._originalValue = originalValue;
		}

	// Properties
		/// <summary>
		/// Gets the current value.
		/// </summary>
		public T CurrentValue {
			get { return this._currentValue; }
		}
		/// <summary>
		/// Gets a value indicating if this instance has been changed from its original value.
		/// </summary>
		public Boolean IsDirty {
			get { return !Object.Equals(this._currentValue, this._originalValue); }
		}
		/// <summary>
		/// Gets the original value.
		/// </summary>
		public T OriginalValue {
			get { return this._originalValue; }
		}

	// Methods
		/// <summary>
		/// Returns a value indicating if the two objects specified are equal.
		/// </summary>
		/// <param name="objA">The first object to compare.</param>
		/// <param name="objB">The second object to compare.</param>
		/// <returns><c>true</c> if the two objects specified are equal; otherwise, <c>false</c>.</returns>
		private static Boolean AreEqual(Transactional<T> objA, Transactional<T> objB) {
			if (Object.ReferenceEquals(objA, null)) {
				return Object.ReferenceEquals(objB, null);
			}
			else if (Object.ReferenceEquals(objB, null)) {
				return false;
			}

			return Object.Equals(objA._currentValue, objB._currentValue)
				&& Object.Equals(objA._originalValue, objB._originalValue);
		}
		/// <summary>
		/// Returns a value indicating if the two objects specified are equal.
		/// </summary>
		/// <param name="objA">The first object to compare.</param>
		/// <param name="objB">The second object to compare.</param>
		/// <returns><c>true</c> if the two objects specified are equal; otherwise, <c>false</c>.</returns>
		private static Boolean AreEqual(Transactional<T> objA, T objB) {
			if (Object.ReferenceEquals(objA, null)) {
				return Object.ReferenceEquals(objB, null);
			}
			else if (Object.ReferenceEquals(objB, null)) {
				return Object.ReferenceEquals(objA._currentValue, null);
			}

			return Object.Equals(objA._currentValue, objB);
		}
		/// <summary>
		/// Commits any changes in this instance.
		/// </summary>
		public void Commit() {
			this._originalValue = this._currentValue;
		}
		/// <summary>
		/// Returns a value indicating if the object specified is equal to this instance.
		/// </summary>
		/// <param name="obj">The object to compare to this instance.</param>
		/// <returns><c>true</c> if the object specified is equal to this instance; otherwise, <c>false</c>.</returns>
		public override Boolean Equals(Object obj) {
			if (this.Equals(obj as Transactional<T>)) {
				return true;
			}

			if (obj is T) {
				return this.Equals((T)obj);
			}
			return false;
		}
		/// <summary>
		/// Returns a value indicating if the object specified is equal to this instance.
		/// </summary>
		/// <param name="other">The object to compare to this instance.</param>
		/// <returns><c>true</c> if the object specified is equal to this instance; otherwise, <c>false</c>.</returns>
		public Boolean Equals(Transactional<T> other) {
			return Transactional<T>.AreEqual(this, other);
		}
		/// <summary>
		/// Returns a value indicating if the object specified is equal to this instance.
		/// </summary>
		/// <param name="other">The object to compare to this instance.</param>
		/// <returns><c>true</c> if the object specified is equal to this instance; otherwise, <c>false</c>.</returns>
		public Boolean Equals(T other) {
			return Transactional<T>.AreEqual(this, other);
		}
		/// <summary>
		/// Returns a value that serves as a hash code for this instance.
		/// </summary>
		/// <returns>The value that serves as the hash code for this instance.</returns>
		public override Int32 GetHashCode() {
			Int32 currentValueHashCode = this._currentValue != null ? this._currentValue.GetHashCode() : 0;
			Int32 originalValueHashCode = this._originalValue != null ? this._originalValue.GetHashCode() : 0;
			return currentValueHashCode ^ originalValueHashCode;
		}
		/// <summary>
		/// Rolls back any changes made to this instance.
		/// </summary>
		public void Rollback() {
			this._currentValue = this._originalValue;
		}
		/// <summary>
		/// Sets the current value of this instance.
		/// </summary>
		/// <param name="value">The value to set.</param>
		public void Set(T value) {
			this.Set(value, false);
		}
		/// <summary>
		/// Sets the current value of this instance.
		/// </summary>
		/// <param name="value">The value to set.</param>
		/// <param name="commit">If <c>true</c> the value will be immediately committed.</param>
		public void Set(T value, Boolean commit) {
			if (!Object.Equals(this._currentValue, value)) {
				this._currentValue = value;
				if (commit) {
					this.Commit();
				}
			}
		}
		/// <summary>
		/// Returns the string representation of this instance.
		/// </summary>
		/// <returns>The string representation of this instance.</returns>
		public override String ToString() {
			if (this._currentValue != null) {
				return this._currentValue.ToString();
			}
			return base.ToString();
		}

	// Operators
		/// <summary>
		/// Returns a value indicating if the two objects specified are equal.
		/// </summary>
		/// <param name="objA">The first object to compare.</param>
		/// <param name="objB">The second object to compare.</param>
		/// <returns><c>true</c> if the two objects specified are equal; otherwise, <c>false</c>.</returns>
		public static Boolean operator ==(Transactional<T> objA, Transactional<T> objB) {
			return Transactional<T>.AreEqual(objA, objB);
		}
		/// <summary>
		/// Returns a value indicating if the two objects specified are equal.
		/// </summary>
		/// <param name="objA">The first object to compare.</param>
		/// <param name="objB">The second object to compare.</param>
		/// <returns><c>true</c> if the two objects specified are equal; otherwise, <c>false</c>.</returns>
		public static Boolean operator ==(Transactional<T> objA, T objB) {
			return Transactional<T>.AreEqual(objA, objB);
		}
		/// <summary>
		/// Returns a value indicating if the two objects specified are equal.
		/// </summary>
		/// <param name="objA">The first object to compare.</param>
		/// <param name="objB">The second object to compare.</param>
		/// <returns><c>true</c> if the two objects specified are equal; otherwise, <c>false</c>.</returns>
		public static Boolean operator ==(T objA, Transactional<T> objB) {
			return Transactional<T>.AreEqual(objB, objA);
		}
		/// <summary>
		/// Returns a value indicating if the two objects specified are not equal.
		/// </summary>
		/// <param name="objA">The first object to compare.</param>
		/// <param name="objB">The second object to compare.</param>
		/// <returns><c>true</c> if the two objects specified are not equal; otherwise, <c>false</c>.</returns>
		public static Boolean operator !=(Transactional<T> objA, Transactional<T> objB) {
			return !Transactional<T>.AreEqual(objA, objB);
		}
		/// <summary>
		/// Returns a value indicating if the two objects specified are not equal.
		/// </summary>
		/// <param name="objA">The first object to compare.</param>
		/// <param name="objB">The second object to compare.</param>
		/// <returns><c>true</c> if the two objects specified are not equal; otherwise, <c>false</c>.</returns>
		public static Boolean operator !=(Transactional<T> objA, T objB) {
			return !Transactional<T>.AreEqual(objA, objB);
		}
		/// <summary>
		/// Returns a value indicating if the two objects specified are not equal.
		/// </summary>
		/// <param name="objA">The first object to compare.</param>
		/// <param name="objB">The second object to compare.</param>
		/// <returns><c>true</c> if the two objects specified are not equal; otherwise, <c>false</c>.</returns>
		public static Boolean operator !=(T objA, Transactional<T> objB) {
			return !Transactional<T>.AreEqual(objB, objA);
		}
		/// <summary>
		/// Implicitly converts an object of type <see cref="T:Transactional&lt;T&gt;"/> to type <typeparamref name="T"/>.
		/// </summary>
		/// <param name="instance">The object to convert.</param>
		/// <returns>The converted value.</returns>
		public static implicit operator T(Transactional<T> instance) {
			if (instance == null) {
				return default(T);
			}
			return instance._currentValue;
		}
		/// <summary>
		/// Implicitly converts an object of type <typeparamref name="T"/> to type <see cref="T:Transactional&lt;T&gt;"/>.
		/// </summary>
		/// <param name="instance">The object to convert.</param>
		/// <returns>The converted value.</returns>
		public static implicit operator Transactional<T>(T instance) {
			return new Transactional<T>(instance);
		}
	}
}
