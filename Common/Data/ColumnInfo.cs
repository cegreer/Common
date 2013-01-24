using System;

using PropertyDescriptor = System.ComponentModel.PropertyDescriptor;

namespace Vizistata.Data {
	/// <summary>
	/// Provides information about a column.  This class may not be inherited.
	/// </summary>
	internal sealed class ColumnInfo : Object {
	// Fields
		/// <summary>
		/// The name of the column.  This field is read-only.
		/// </summary>
		private readonly String _name;
		/// <summary>
		/// The 0-based ordinal of the column.  This field is read-only.
		/// </summary>
		private readonly Int32 _ordinal;
		/// <summary>
		/// The property descriptor for the column.  This field is read-only.
		/// </summary>
		private readonly PropertyDescriptor _propertyDescriptor;
		/// <summary>
		/// The type of the column.  This field is read-only.
		/// </summary>
		private readonly Type _type;
		/// <summary>
		/// The type name for the column.  This field is read-only.
		/// </summary>
		private readonly String _typeName;

	// Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:ColumnInfo"/> class.
		/// </summary>
		/// <param name="ordinal">The 0-based ordinal of the column.</param>
		/// <param name="name">The name of the column.</param>
		/// <param name="type">The type of the column.</param>
		/// <param name="typeName">The type name for the column.</param>
		public ColumnInfo(Int32 ordinal, String name, Type type, String typeName)
			: base() {
			this._ordinal = ordinal;
			this._name = name;
			this._type = type;
			this._typeName = typeName;
			this._propertyDescriptor = new DataRecordPropertyDescriptor(ordinal, name, type);
		}

	// Properties
		/// <summary>
		/// Gets the name of the column.
		/// </summary>
		public String Name {
			get { return this._name; }
		}
		/// <summary>
		/// Gets the 0-based ordinal of the column.
		/// </summary>
		public Int32 Ordinal {
			get { return this._ordinal; }
		}
		/// <summary>
		/// Gets the property descriptor for the column.
		/// </summary>
		public PropertyDescriptor PropertyDescriptor {
			get { return this._propertyDescriptor; }
		}
		/// <summary>
		/// Gets the type of the column.
		/// </summary>
		public Type Type {
			get { return this._type; }
		}
		/// <summary>
		/// Gets the type name for the column.
		/// </summary>
		public String TypeName {
			get { return this._typeName; }
		}
	}
}
