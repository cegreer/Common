using System;
using System.ComponentModel;
using System.Data;

namespace Vizistata.Data {
	/// <summary>
	/// A strongly-typed property descriptor for the <see cref="T:DataRecord"/> class.  This class may not be inherited.
	/// </summary>
	internal sealed class DataRecordPropertyDescriptor : PropertyDescriptor {
	// Fields
		/// <summary>
		/// The 0-based ordinal of the property.  This field is read-only.
		/// </summary>
		private Int32 _ordinal;
		/// <summary>
		/// The type of the property.  This field is read-only.
		/// </summary>
		private Type _type;

	// Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:DataRecordPropertyDescriptor"/> class.
		/// </summary>
		/// <param name="ordinal">The 0-based ordinal of the property.</param>
		/// <param name="name">The name of the property.</param>
		/// <param name="type">The type of the property.</param>
		internal DataRecordPropertyDescriptor(Int32 ordinal, String name, Type type)
			: base(name, null) {
			this._ordinal = ordinal;
			this._type = type;
		}

	// Properties
		/// <summary>
		/// Gets the type of the component this property is bound to.
		/// </summary>
		public override Type ComponentType {
			get { return typeof(IDataRecord); }
		}
		/// <summary>
		/// Gets a value indicating whether this property is read-only.
		/// </summary>
		public override Boolean IsReadOnly {
			get { return true; }
		}
		/// <summary>
		/// Gets the type of the property.
		/// </summary>
		public override Type PropertyType {
			get { return this._type; }
		}

	// Methods
		/// <summary>
		/// Returns whether resetting an object changes its value.
		/// </summary>
		/// <param name="component">The component to test for reset capability.</param>
		/// <returns><c>true</c> if resetting the component changes its value; otherwise, <c>false</c>.</returns>
		public override Boolean CanResetValue(Object component) {
			return false;
		}
		/// <summary>
		/// Gets the current value of the property on a component.
		/// </summary>
		/// <param name="component">The component with the property for which to retrieve the value.</param>
		/// <returns>The value of a property for a given component.</returns>
		public override Object GetValue(Object component) {
			return ((IDataRecord)component)[this._ordinal];
		}
		/// <summary>
		/// Resets the value for this property of the component to the default value.
		/// </summary>
		/// <param name="component">The component with the property value that is to be reset to the default value.</param>
		public override void ResetValue(Object component) {
			throw new NotSupportedException();
		}
		/// <summary>
		/// Sets the value of the component to a different value.
		/// </summary>
		/// <param name="component">The component with the property value that is to be set.</param>
		/// <param name="value">The new value.</param>
		public override void SetValue(Object component, Object value) {
			throw new NotSupportedException();
		}
		/// <summary>
		/// Determines a value indicating whether the value of this property needs to be persisted.
		/// </summary>
		/// <param name="component">The component with the property to be examined for persistence.</param>
		/// <returns><c>true</c> if the property should be persisted; otherwise, <c>false</c>.</returns>
		public override Boolean ShouldSerializeValue(Object component) {
			return false;
		}
	}
}
