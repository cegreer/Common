using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Vizistata.Mocks {
	/// <summary>
	/// Represents a mock data record object.  This class may not be inherited.
	/// </summary>
	internal sealed class MockDataRecord : DbDataRecord, ICustomTypeDescriptor {
	// Fields
		/// <summary>
		/// The values in this instance.  This field is read-only.
		/// </summary>
		private readonly IDictionary<String, Object> _values;

	// Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:DataRecord"/> class.
		/// </summary>
		/// <param name="values">The values for this instance.</param>
		public MockDataRecord(IDictionary<String, Object> values)
			: base() {
			if (values == null) {
				throw new ArgumentNullException("values");
			}

			this._values = values;
		}

	// Properties
		/// <summary>
		/// Gets the value at the specified column in its native format given the column ordinal.
		/// </summary>
		/// <param name="i">The column ordinal.</param>
		/// <returns>The value at the specified column in its native format.</returns>
		/// <exception cref="System.IndexOutOfRangeException"><paramref name="i"/> is less than 0.
		/// -or- <paramref name="i"/> is greater than or equal to <see cref="P:IDataRecord.FieldCount"/>.</exception>
		public override Object this[Int32 i] {
			get { return this.GetValue(i); }
		}
		/// <summary>
		/// Gets the value at the specified column in its native format given the column name.
		/// </summary>
		/// <param name="name">The column name.</param>
		/// <returns>The value at the specified column in its native format.</returns>
		///	<exception cref="System.ArgumentNullException"><paramref name="name"/> is a null reference.</exception>
		/// <exception cref="System.IndexOutOfRangeException"><paramref name="name"/> is not in this instance.</exception>
		public override Object this[String name] {
			get { return this.GetValue(this.GetOrdinal(name)); }
		}
		/// <summary>
		/// Gets the number of fields within the current record.
		/// </summary>
		public override Int32 FieldCount {
			get { return this._values.Count; }
		}

	// Methods
		/// <summary>
		/// Returns the value of the specified column as a <see cref="T:Boolean"/>.
		/// </summary>
		/// <param name="i">The column ordinal.</param>
		/// <returns>The value of the column.</returns>
		/// <exception cref="System.IndexOutOfRangeException"><paramref name="i"/> is less than 0.
		/// -or- <paramref name="i"/> is greater than or equal to <see cref="P:IDataRecord.FieldCount"/>.</exception>
		/// <exception cref="System.InvalidCastException">The value is not of type <see cref="T:Boolean"/>.</exception>
		public override Boolean GetBoolean(Int32 i) {
			return (Boolean)this._values.ElementAt(i).Value;
		}
		/// <summary>
		/// Returns the value of the specified column as a <see cref="T:Byte"/>.
		/// </summary>
		/// <param name="i">The column ordinal.</param>
		/// <returns>The value of the column.</returns>
		/// <exception cref="System.IndexOutOfRangeException"><paramref name="i"/> is less than 0.
		/// -or- <paramref name="i"/> is greater than or equal to <see cref="P:IDataRecord.FieldCount"/>.</exception>
		/// <exception cref="System.InvalidCastException">The value is not of type <see cref="T:Byte"/>.</exception>
		public override Byte GetByte(Int32 i) {
			return (Byte)this._values.ElementAt(i).Value;
		}
		/// <summary>
		/// Returns the value of the specified column as a byte array.
		/// </summary>
		/// <param name="i">The zero-based column ordinal.</param>
		/// <param name="dataIndex">The index within the field from which to start the read operation.</param>
		/// <param name="buffer">The buffer into which to read the stream of bytes.</param>
		/// <param name="bufferIndex">The index for buffer to start the read operation.</param>
		/// <param name="length">The number of bytes to read.</param>
		/// <returns>The value of the specified column.</returns>
		/// <exception cref="System.IndexOutOfRangeException"><paramref name="i"/> is less than 0.
		/// -or- <paramref name="i"/> is greater than or equal to <see cref="P:IDataRecord.FieldCount"/>.</exception>
		/// <exception cref="System.ArgumentOutOfRangeException"><paramref name="dataIndex"/> is greater than 2,147,483,647.
		/// -or- <paramref name="dataIndex"/> is less than 0.
		/// -or- <paramref name="dataIndex"/> is greater than or equal to the length of the array referenced by <paramref name="i"/>.
		/// -or- <paramref name="bufferIndex"/> is less than 0.
		/// -or- <paramref name="bufferIndex"/> is greater than or equal to the length of <paramref name="buffer"/>.
		/// -or- <paramref name="length"/> is less than 0.
		/// -or- The combined value of the length of the byte array referenced by <paramref name="i"/> and <paramref name="bufferIndex"/> is greater than the length of <paramref name="buffer"/>.</exception>
		/// <exception cref="System.InvalidCastException">The value is not an array of <see cref="T:Byte"/>.</exception>
		public override Int64 GetBytes(Int32 i, Int64 dataIndex, Byte[] buffer, Int32 bufferIndex, Int32 length) {
			Byte[] byteArray = (Byte[])this._values.ElementAt(i).Value;
			Int32 dataLength = byteArray.Length;
			if (buffer == null) {
				return dataLength;
			}

			if (dataIndex > 2147483647L) {
				throw new ArgumentOutOfRangeException("dataIndex", "The data index specified is too large for the buffer array.");
			}
			if (dataIndex < 0L || dataIndex >= dataLength) {
				throw new ArgumentOutOfRangeException("dataIndex", "The data index must be 0 or greater and must be less than the size of the data.");
			}
			if (bufferIndex < 0 || bufferIndex >= buffer.Length) {
				throw new ArgumentOutOfRangeException("bufferIndex", "The buffer index must be 0 or greater and must be less than the length of the buffer.");
			}
			if (length < 0) {
				throw new ArgumentOutOfRangeException("length", "The length must be 0 or greater.");
			}
			if (dataLength + bufferIndex > buffer.Length) {
				throw new ArgumentOutOfRangeException("bufferIndex", "The buffer size is not large enough for the ranges specified.");
			}

			Int32 remainingLength = dataLength;
			Int32 int32DataIndex = (Int32)dataIndex;
			if (int32DataIndex < dataLength) {
				if (int32DataIndex + length > remainingLength) {
					remainingLength -= int32DataIndex;
				}
				else {
					remainingLength = length;
				}
			}
			Array.Copy(byteArray, int32DataIndex, buffer, bufferIndex, remainingLength);
			return remainingLength;
		}
		/// <summary>
		/// Returns the value of the specified column as a <see cref="T:Char"/>.
		/// </summary>
		/// <param name="i">The column ordinal.</param>
		/// <returns>The value of the column.</returns>
		/// <exception cref="System.IndexOutOfRangeException"><paramref name="i"/> is less than 0.
		/// -or- <paramref name="i"/> is greater than or equal to <see cref="P:IDataRecord.FieldCount"/>.</exception>
		/// <exception cref="System.InvalidCastException">The value is not of type <see cref="T:Char"/>.</exception>
		public override Char GetChar(Int32 i) {
			Object value = this._values.ElementAt(i).Value;
			if (value is Char) {
				return (Char)value;
			}
			String text = value as String;
			if (text != null) {
				return text[0];
			}
			throw new InvalidCastException();
		}
		/// <summary>
		/// Returns the value of the specified column as a character array.
		/// </summary>
		/// <param name="i">Column ordinal.</param>
		/// <param name="dataIndex">Buffer to copy data into.</param>
		/// <param name="buffer">Maximum length to copy into the buffer.</param>
		/// <param name="bufferIndex">Point to start from within the buffer.</param>
		/// <param name="length">Point to start from within the source data.</param>
		/// <returns>The value of the specified column.</returns>
		/// <exception cref="System.IndexOutOfRangeException"><paramref name="i"/> is less than 0.
		/// -or- <paramref name="i"/> is greater than or equal to <see cref="P:IDataRecord.FieldCount"/>.</exception>
		/// <exception cref="System.ArgumentOutOfRangeException"><paramref name="dataIndex"/> is greater than 2,147,483,647.
		/// -or- <paramref name="dataIndex"/> is less than 0.
		/// -or- <paramref name="dataIndex"/> is greater than or equal to the length of the array referenced by <paramref name="i"/>.
		/// -or- <paramref name="bufferIndex"/> is less than 0.
		/// -or- <paramref name="bufferIndex"/> is greater than or equal to the length of <paramref name="buffer"/>.
		/// -or- <paramref name="length"/> is less than 0.
		/// -or- The combined value of the length of the byte array referenced by <paramref name="i"/> and <paramref name="bufferIndex"/> is greater than the length of <paramref name="buffer"/>.</exception>
		/// <exception cref="System.InvalidCastException">The value is not an array of <see cref="T:Char"/>.</exception>
		public override Int64 GetChars(Int32 i, Int64 dataIndex, Char[] buffer, Int32 bufferIndex, Int32 length) {
			String value = (String)this._values.ElementAt(i).Value;
			Int32 dataLength = value.Length;
			if (buffer == null) {
				return dataLength;
			}

			if (dataIndex > 2147483647L) {
				throw new ArgumentOutOfRangeException("dataIndex", "The data index specified is too large for the buffer array.");
			}
			if (dataIndex < 0L || dataIndex >= dataLength) {
				throw new ArgumentOutOfRangeException("dataIndex", "The data index must be 0 or greater and must be less than the size of the data.");
			}
			if (bufferIndex < 0 || bufferIndex >= buffer.Length) {
				throw new ArgumentOutOfRangeException("bufferIndex", "The buffer index must be 0 or greater and must be less than the length of the buffer.");
			}
			if (length < 0) {
				throw new ArgumentOutOfRangeException("length", "The length must be 0 or greater.");
			}
			if (dataLength + bufferIndex > buffer.Length) {
				throw new ArgumentOutOfRangeException("bufferIndex", "The buffer size is not large enough for the ranges specified.");
			}

			Int32 remainingLength = dataLength;
			Int32 int32DataIndex = (Int32)dataIndex;
			if (int32DataIndex < remainingLength) {
				if (int32DataIndex + length > remainingLength) {
					remainingLength -= int32DataIndex;
				}
				else {
					remainingLength = length;
				}
			}
			Array.Copy(value.ToCharArray(), int32DataIndex, buffer, bufferIndex, remainingLength);
			return remainingLength;
		}
		/// <summary>
		/// Returns the name of the back-end data type.
		/// </summary>
		/// <param name="i">The column ordinal.</param>
		/// <returns>The name of the back-end data type.</returns>
		/// <exception cref="System.IndexOutOfRangeException"><paramref name="i"/> is less than 0.
		/// -or- <paramref name="i"/> is greater than or equal to <see cref="P:IDataRecord.FieldCount"/>.</exception>
		public override String GetDataTypeName(Int32 i) {
			Object value = this._values.ElementAt(i).Value;
			if (value == null) {
				return "String";
			}
			return value.GetType().Name;
		}
		/// <summary>
		/// Returns the value of the specified column as a <see cref="T:DateTime"/>.
		/// </summary>
		/// <param name="i">The column ordinal.</param>
		/// <returns>The value of the column.</returns>
		/// <exception cref="System.IndexOutOfRangeException"><paramref name="i"/> is less than 0.
		/// -or- <paramref name="i"/> is greater than or equal to <see cref="P:IDataRecord.FieldCount"/>.</exception>
		/// <exception cref="System.InvalidCastException">The value is not of type <see cref="T:DateTime"/>.</exception>
		public override DateTime GetDateTime(Int32 i) {
			return (DateTime)this._values.ElementAt(i).Value;
		}
		/// <summary>
		/// Returns the value of the specified column as a <see cref="T:Decimal"/>.
		/// </summary>
		/// <param name="i">The column ordinal.</param>
		/// <returns>The value of the column.</returns>
		/// <exception cref="System.IndexOutOfRangeException"><paramref name="i"/> is less than 0.
		/// -or- <paramref name="i"/> is greater than or equal to <see cref="P:IDataRecord.FieldCount"/>.</exception>
		/// <exception cref="System.InvalidCastException">The value is not of type <see cref="T:Decimal"/>.</exception>
		public override Decimal GetDecimal(Int32 i) {
			return (Decimal)this._values.ElementAt(i).Value;
		}
		/// <summary>
		/// Returns the value of the specified column as a <see cref="T:Double"/>.
		/// </summary>
		/// <param name="i">The column ordinal.</param>
		/// <returns>The value of the column.</returns>
		/// <exception cref="System.IndexOutOfRangeException"><paramref name="i"/> is less than 0.
		/// -or- <paramref name="i"/> is greater than or equal to <see cref="P:IDataRecord.FieldCount"/>.</exception>
		/// <exception cref="System.InvalidCastException">The value is not of type <see cref="T:Double"/>.</exception>
		public override Double GetDouble(Int32 i) {
			return (Double)this._values.ElementAt(i).Value;
		}
		/// <summary>
		/// Returns the <see cref="T:Type"/> that is the data type of the object.
		/// </summary>
		/// <param name="i">The column ordinal.</param>
		/// <returns>The <see cref="T:Type"/> that is the data type of the object.</returns>
		/// <exception cref="System.IndexOutOfRangeException"><paramref name="i"/> is less than 0.
		/// -or- <paramref name="i"/> is greater than or equal to <see cref="P:IDataRecord.FieldCount"/>.</exception>
		public override Type GetFieldType(Int32 i) {
			Object value = this._values.ElementAt(i).Value;
			if (value == null) {
				return typeof(String);
			}
			return value.GetType();
		}
		/// <summary>
		/// Returns the value of the specified column as a <see cref="T:Single"/>.
		/// </summary>
		/// <param name="i">The column ordinal.</param>
		/// <returns>The value of the column.</returns>
		/// <exception cref="System.IndexOutOfRangeException"><paramref name="i"/> is less than 0.
		/// -or- <paramref name="i"/> is greater than or equal to <see cref="P:IDataRecord.FieldCount"/>.</exception>
		/// <exception cref="System.InvalidCastException">The value is not of type <see cref="T:Single"/>.</exception>
		public override Single GetFloat(Int32 i) {
			return (Single)this._values.ElementAt(i).Value;
		}
		/// <summary>
		/// Returns the value of the specified column as a <see cref="T:Guid"/>.
		/// </summary>
		/// <param name="i">The column ordinal.</param>
		/// <returns>The value of the column.</returns>
		/// <exception cref="System.IndexOutOfRangeException"><paramref name="i"/> is less than 0.
		/// -or- <paramref name="i"/> is greater than or equal to <see cref="P:IDataRecord.FieldCount"/>.</exception>
		/// <exception cref="System.InvalidCastException">The value is not of type <see cref="T:Guid"/>.</exception>
		public override Guid GetGuid(Int32 i) {
			return (Guid)this._values.ElementAt(i).Value;
		}
		/// <summary>
		/// Returns the value of the specified column as a <see cref="T:Int16"/>.
		/// </summary>
		/// <param name="i">The column ordinal.</param>
		/// <returns>The value of the column.</returns>
		/// <exception cref="System.IndexOutOfRangeException"><paramref name="i"/> is less than 0.
		/// -or- <paramref name="i"/> is greater than or equal to <see cref="P:IDataRecord.FieldCount"/>.</exception>
		/// <exception cref="System.InvalidCastException">The value is not of type <see cref="T:Int16"/>.</exception>
		public override Int16 GetInt16(Int32 i) {
			return (Int16)this._values.ElementAt(i).Value;
		}
		/// <summary>
		/// Returns the value of the specified column as a <see cref="T:Int32"/>.
		/// </summary>
		/// <param name="i">The column ordinal.</param>
		/// <returns>The value of the column.</returns>
		/// <exception cref="System.IndexOutOfRangeException"><paramref name="i"/> is less than 0.
		/// -or- <paramref name="i"/> is greater than or equal to <see cref="P:IDataRecord.FieldCount"/>.</exception>
		/// <exception cref="System.InvalidCastException">The value is not of type <see cref="T:Int32"/>.</exception>
		public override Int32 GetInt32(Int32 i) {
			return (Int32)this._values.ElementAt(i).Value;
		}
		/// <summary>
		/// Returns the value of the specified column as a <see cref="T:Int64"/>.
		/// </summary>
		/// <param name="i">The column ordinal.</param>
		/// <returns>The value of the column.</returns>
		/// <exception cref="System.IndexOutOfRangeException"><paramref name="i"/> is less than 0.
		/// -or- <paramref name="i"/> is greater than or equal to <see cref="P:IDataRecord.FieldCount"/>.</exception>
		/// <exception cref="System.InvalidCastException">The value is not of type <see cref="T:Int64"/>.</exception>
		public override Int64 GetInt64(Int32 i) {
			return (Int64)this._values.ElementAt(i).Value;
		}
		/// <summary>
		/// Returns the name of the specified column.
		/// </summary>
		/// <param name="i">The column ordinal.</param>
		/// <returns>The name of the specified column.</returns>
		/// <exception cref="System.IndexOutOfRangeException"><paramref name="i"/> is less than 0.
		/// -or- <paramref name="i"/> is greater than or equal to <see cref="P:IDataRecord.FieldCount"/>.</exception>
		public override String GetName(Int32 i) {
			return this._values.ElementAt(i).Key;
		}
		/// <summary>
		/// Returns the column ordinal, given the name of the column.
		/// </summary>
		/// <param name="name">The name of the column.</param>
		/// <returns>The column ordinal.</returns>
		///	<exception cref="System.ArgumentNullException"><paramref name="name"/> is a null reference.</exception>
		/// <exception cref="System.IndexOutOfRangeException"><paramref name="name"/> is not in this instance.</exception>
		[SuppressMessage("Microsoft.Usage", "CA2201:DoNotRaiseReservedExceptionTypes", Justification = "IndexOutOfRangeException is specified in the interface contract.")]
		public override Int32 GetOrdinal(String name) {
			if (name == null) {
				throw new ArgumentNullException("name");
			}

			Int32[] indexes = this._values
				.Select((pair, i) => new { Ordinal = i, Name = pair.Key, Value = pair.Value })
				.Where(a => a.Name == name)
				.Select(a => a.Ordinal)
				.ToArray();
			if (indexes.Length == 0) {
				throw new IndexOutOfRangeException("The column name is not in the list of columns.");
			}
			return indexes[0];
		}
		/// <summary>
		/// Returns the value of the specified column as a <see cref="T:String"/>.
		/// </summary>
		/// <param name="i">The column ordinal.</param>
		/// <returns>The value of the column.</returns>
		/// <exception cref="System.IndexOutOfRangeException"><paramref name="i"/> is less than 0.
		/// -or- <paramref name="i"/> is greater than or equal to <see cref="P:IDataRecord.FieldCount"/>.</exception>
		/// <exception cref="System.InvalidCastException">The value is not of type <see cref="T:String"/>.</exception>
		public override String GetString(Int32 i) {
			return (String)this._values.ElementAt(i).Value;
		}
		/// <summary>
		/// Returns the value of the specified column.
		/// </summary>
		/// <param name="i">The column ordinal.</param>
		/// <returns>The value of the column.</returns>
		/// <exception cref="System.IndexOutOfRangeException"><paramref name="i"/> is less than 0.
		/// -or- <paramref name="i"/> is greater than or equal to <see cref="P:IDataRecord.FieldCount"/>.</exception>
		public override Object GetValue(Int32 i) {
			return this._values.ElementAt(i).Value;
		}
		/// <summary>
		/// Populates an array of objects with the column values of the current record.
		/// </summary>
		/// <param name="values">An array to copy the attribute fields into.</param>
		/// <returns>The number of instances of values copied into the array.</returns>
		/// <exception cref="System.ArgumentNullException"><paramref name="values"/> is a null reference.</exception>
		public override Int32 GetValues(Object[] values) {
			if (values == null) {
				throw new ArgumentNullException("values");
			}

			Int32 itemsToCopyCount = Math.Min(values.Length, this._values.Count);
			for (Int32 i = 0; i < itemsToCopyCount; i++) {
				values[i] = this._values.ElementAt(i).Value;
			}
			return itemsToCopyCount;
		}
		/// <summary>
		/// Used to indicate nonexistent values.
		/// </summary>
		/// <param name="i">The column ordinal.</param>
		/// <returns><c>true</c> if the specified column is equivalent to System.DBNull; otherwise <c>false</c>.</returns>
		/// <exception cref="System.IndexOutOfRangeException"><paramref name="i"/> is less than 0.
		/// -or- <paramref name="i"/> is greater than or equal to <see cref="P:IDataRecord.FieldCount"/>.</exception>
		public override Boolean IsDBNull(Int32 i) {
			Object obj = this._values.ElementAt(i).Value;
			return obj == null || Convert.IsDBNull(obj);
		}

		#region ICustomTypeDescriptor Members (explicit)
		AttributeCollection ICustomTypeDescriptor.GetAttributes() {
			return new AttributeCollection(null);
		}
		String ICustomTypeDescriptor.GetClassName() {
			return null;
		}
		String ICustomTypeDescriptor.GetComponentName() {
			return null;
		}
		TypeConverter ICustomTypeDescriptor.GetConverter() {
			return null;
		}
		EventDescriptor ICustomTypeDescriptor.GetDefaultEvent() {
			return null;
		}
		PropertyDescriptor ICustomTypeDescriptor.GetDefaultProperty() {
			return null;
		}
		Object ICustomTypeDescriptor.GetEditor(Type editorBaseType) {
			return null;
		}
		EventDescriptorCollection ICustomTypeDescriptor.GetEvents() {
			return new EventDescriptorCollection(null);
		}
		EventDescriptorCollection ICustomTypeDescriptor.GetEvents(Attribute[] attributes) {
			return new EventDescriptorCollection(null);
		}
		PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties() {
			return ((ICustomTypeDescriptor)this).GetProperties(null);
		}
		PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties(Attribute[] attributes) {
			return null;
		}
		Object ICustomTypeDescriptor.GetPropertyOwner(PropertyDescriptor pd) {
			return this;
		}
		#endregion	
	}
}
