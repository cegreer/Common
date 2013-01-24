using System;

namespace Vizistata.Mocks {
	/// <summary>
	/// A mock object used to test reflection methods.  This class may not be inherited.
	/// </summary>
	/// <typeparam name="T">The type of values contained in this instance.</typeparam>
	internal sealed class MockReflectionTarget<T> : Object {
	// Fields
		/// <summary>
		/// The value of <see cref="P:ReadOnlyPropertyValue"/>.
		/// </summary>
		private T _readOnlyPropertyValue;
		/// <summary>
		/// The value of <see cref="P:ReadWritePropertyValue"/>.
		/// </summary>
		private T _readWritePropertyValue;
		/// <summary>
		/// The value of <see cref="P:WriteOnlyPropertyValue"/>.
		/// </summary>
		private T _writeOnlyPropertyValue;
		/// <summary>
		/// A test value for read-only fields.  This field is read-only.
		/// </summary>
		public readonly T ReadOnlyField;
		/// <summary>
		/// A test value for read/write fields.
		/// </summary>
		public T ReadWriteField;

	// Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:MockReflectionTarget&lt;T&gt;"/> class.
		/// </summary>
		public MockReflectionTarget() : this(default(T)) { }
		/// <summary>
		/// Initializes a new instance of the <see cref="T:MockReflectionTarget&lt;T&gt;"/> class.
		/// </summary>
		/// <param name="readOnlyFieldValue">The value for the <see cref="F:ReadOnlyFieldValue"/> field.</param>
		public MockReflectionTarget(T readOnlyFieldValue)
			: base() {
			this.ReadOnlyField = readOnlyFieldValue;
		}

	// Properties
		/// <summary>
		/// Gets or sets the method to use for <see cref="M:CallNonVoidMethod"/>.
		/// </summary>
		public Func<T> NonVoidMethod {
			get;
			set;
		}
		/// <summary>
		/// Gets the value of the read-only property.
		/// </summary>
		public T ReadOnlyProperty {
			get { return this._readOnlyPropertyValue; }
		}
		/// <summary>
		/// Gets or sets the value of the read/write property.
		/// </summary>
		public T ReadWriteProperty {
			get { return this._readWritePropertyValue; }
			set { this._readWritePropertyValue = value; }
		}
		/// <summary>
		/// Gets or sets the method to use for <see cref="M:CallVoidMethod"/>.
		/// </summary>
		public Action VoidMethod {
			get;
			set;
		}
		/// <summary>
		/// Sets the value of the write-only property.
		/// </summary>
		public T WriteOnlyProperty {
			set { this._writeOnlyPropertyValue = value; }
		}

	// Methods
		/// <summary>
		/// Invokes the non-void method.
		/// </summary>
		/// <returns>The value returned by the method.</returns>
		public T CallNonVoidMethod() {
			if (this.NonVoidMethod != null) {
				return this.NonVoidMethod();
			}
			return default(T);
		}
		/// <summary>
		/// Invokes the void method.
		/// </summary>
		public void CallVoidMethod() {
			if (this.VoidMethod != null) {
				this.VoidMethod();
			}
		}
		/// <summary>
		/// Returns the value of the write-only property.
		/// </summary>
		/// <returns>The value of the write-only property.</returns>
		public T GetWriteOnlyPropertyValue() {
			return this._writeOnlyPropertyValue;
		}
		/// <summary>
		/// Sets the value of the read-only property.
		/// </summary>
		/// <param name="value">The value of the read-only property.</param>
		public void SetReadOnlyPropertyValue(T value) {
			this._readOnlyPropertyValue = value;
		}
	}
}
