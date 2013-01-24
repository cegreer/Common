using System;
using System.Data;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using ICustomTypeDescriptor = System.ComponentModel.ICustomTypeDescriptor;
using PropertyDescriptor = System.ComponentModel.PropertyDescriptor;

namespace Vizistata.Data {
	/// <summary>
	/// This is a test class for <see cref="T:DataRecord"/> and is intended to contain all <see cref="T:DataRecord"/> Unit Tests.
	///</summary>
	[TestClass()]
	public class DataRecordTests {
		#region Test Class Implementation
		/// <summary>
		/// Describes the context under which the current test is running.
		/// </summary>
		private TestContext _testContextInstance;

		/// <summary>
		/// Initializes a new instance of the <see cref="T:DataRecordTests"/> class.
		/// </summary>
		public DataRecordTests() : base() { }

		/// <summary>
		/// Gets or sets the test context which provides information about and functionality for the current test run.
		///</summary>
		public TestContext TestContext {
			get { return this._testContextInstance; }
			set { this._testContextInstance = value; }
		}
		#region Additional test attributes
		// 
		//You can use the following additional attributes as you write your tests:
		//
		//Use ClassInitialize to run code before running the first test in the class
		//
		//[ClassInitialize()]
		//public static void MyClassInitialize(TestContext testContext)
		//{
		//}
		//
		//Use ClassCleanup to run code after all tests in a class have run
		//
		//[ClassCleanup()]
		//public static void MyClassCleanup()
		//{
		//}
		//
		//Use TestInitialize to run code before running each test
		//
		//[TestInitialize()]
		//public void MyTestInitialize()
		//{
		//}
		//
		//Use TestCleanup to run code after each test has run
		//
		//[TestCleanup()]
		//public void MyTestCleanup()
		//{
		//}
		//
		#endregion
		#endregion

	// Methods
		/// <summary>
		/// Creates a target <see cref="T:DataRecord"/> object that can be tested.
		/// </summary>
		/// <param name="columnNames">The column names.</param>
		/// <param name="values">The values.</param>
		/// <returns>The <see cref="T:DataRecord"/> created.</returns>
		private static DataRecord CreateDataRecord(String[] columnNames, Object[] values) {
			Debug.Assert(columnNames != null);
			Debug.Assert(columnNames.Length != 0);
			Debug.Assert(columnNames.All(columnName => columnName != null));
			Debug.Assert(values != null);
			Debug.Assert(values.Length != 0);
			Debug.Assert(columnNames.Length == values.Length);

			using (DataTable dataTable = new DataTable()) {
				DataColumn[] dataColumns = columnNames
					.Select((columnName, i) => new DataColumn(columnName, values[i] != null ? values[i].GetType() : typeof(String)))
					.ToArray();
				dataTable.Columns.AddRange(dataColumns);
				dataTable.Rows.Add(values);

				return dataTable.CreateDataReader().ReadAll().First();
			}
		}

	// Property Tests
		[TestMethod()]
		[Description("this[Int32] property for the optimal path.")]
		public void DataRecord_Unit_Indexer1_Optimal() {
			Object firstName = "Chad";
			DataRecord target = DataRecordTests.CreateDataRecord(new String[] { "FirstName", "LastName" }, new Object[] { firstName, "Greer" });
			Int32 i = 0;

			Object actual = target[i];
			Assert.AreEqual(firstName, actual);
		}
		[TestMethod()]
		[Description("this[Int32] property when 'i' is too small.")]
		[ExpectedException(typeof(IndexOutOfRangeException))]
		public void DataRecord_Unit_Indexer1_IIsTooSmall() {
			DataRecord target = DataRecordTests.CreateDataRecord(new String[] { "FirstName", "LastName" }, new Object[] { "Chad", "Greer" });
			Int32 i = -1;

			Object actual = target[i];
		}
		[TestMethod()]
		[Description("this[Int32] property when 'i' is too large.")]
		[ExpectedException(typeof(IndexOutOfRangeException))]
		public void DataRecord_Unit_Indexer1_IIsTooLarge() {
			DataRecord target = DataRecordTests.CreateDataRecord(new String[] { "FirstName", "LastName" }, new Object[] { "Chad", "Greer" });
			Int32 i = 2;

			Object actual = target[i];
		}

		[TestMethod()]
		[Description("this[String] property for the optimal path.")]
		public void DataRecord_Unit_Indexer2_Optimal() {
			String firstNameColumnName = "FirstName";
			Object firstName = "Chad";
			DataRecord target = DataRecordTests.CreateDataRecord(new String[] { firstNameColumnName, "LastName" }, new Object[] { firstName, "Greer" });
			String name = firstNameColumnName;

			Object actual = target[name];
			Assert.AreEqual(firstName, actual);
		}
		[TestMethod()]
		[Description("this[String] property when 'name' does not exist.")]
		[ExpectedException(typeof(IndexOutOfRangeException))]
		public void DataRecord_Unit_Indexer2_NameDoesNotExist() {
			DataRecord target = DataRecordTests.CreateDataRecord(new String[] { "FirstName", "LastName" }, new Object[] { "Chad", "Greer" });
			String name = "NoColumn";

			Object actual = target[name];
		}
		[TestMethod()]
		[Description("this[String] property when 'name' is a null reference.")]
		[ExpectedException(typeof(ArgumentNullException))]
		public void DataRecord_Unit_Indexer2_NameIsNull() {
			DataRecord target = DataRecordTests.CreateDataRecord(new String[] { "FirstName", "LastName" }, new Object[] { "Chad", "Greer" });
			String name = null;

			Object actual = target[name];
		}

		[TestMethod()]
		[Description("FieldCount property for the optimal path.")]
		public void DataRecord_Unit_FieldCount_Optimal() {
			Object[] values = new Object[] { "Chad", "Greer" };
			DataRecord target = DataRecordTests.CreateDataRecord(new String[] { "FirstName", "LastName" }, values);

			Int32 expected = values.Length;
			Int32 actual = target.FieldCount;
			Assert.AreEqual(expected, actual);
		}

	// Method Tests
		[TestMethod()]
		[Description("GetBoolean(Int32) method for the optimal path.")]
		public void DataRecord_Unit_GetBoolean_Optimal() {
			Boolean value = true;
			DataRecord target = DataRecordTests.CreateDataRecord(new String[] { "FirstName", "LastName", "MyValue" }, new Object[] { "Chad", "Greer", value });
			Int32 i = 2;

			Boolean actual = target.GetBoolean(i);
			Assert.AreEqual(value, actual);
		}
		[TestMethod()]
		[Description("GetBoolean(Int32) method when 'i' is too small.")]
		[ExpectedException(typeof(IndexOutOfRangeException))]
		public void DataRecord_Unit_GetBoolean_IIsTooSmall() {
			DataRecord target = DataRecordTests.CreateDataRecord(new String[] { "FirstName", "LastName", "MyValue" }, new Object[] { "Chad", "Greer", true });
			Int32 i = -1;

			target.GetBoolean(i);
		}
		[TestMethod()]
		[Description("GetBoolean(Int32) method when 'i' is too large.")]
		[ExpectedException(typeof(IndexOutOfRangeException))]
		public void DataRecord_Unit_GetBoolean_IIsTooLarge() {
			DataRecord target = DataRecordTests.CreateDataRecord(new String[] { "FirstName", "LastName", "MyValue" }, new Object[] { "Chad", "Greer", true });
			Int32 i = 3;

			target.GetBoolean(i);
		}
		[TestMethod()]
		[Description("GetBoolean(Int32) method when the value referenced by 'i' is not of the correct type.")]
		[ExpectedException(typeof(InvalidCastException))]
		public void DataRecord_Unit_GetBoolean_IReferencesIncorrectType() {
			DataRecord target = DataRecordTests.CreateDataRecord(new String[] { "FirstName", "LastName", "MyValue" }, new Object[] { "Chad", "Greer", "True" });
			Int32 i = 2;

			target.GetBoolean(i);
		}

		[TestMethod()]
		[Description("GetByte(Int32) method for the optimal path.")]
		public void DataRecord_Unit_GetByte_Optimal() {
			Byte value = 1;
			DataRecord target = DataRecordTests.CreateDataRecord(new String[] { "FirstName", "LastName", "MyValue" }, new Object[] { "Chad", "Greer", value });
			Int32 i = 2;

			Byte actual = target.GetByte(i);
			Assert.AreEqual(value, actual);
		}
		[TestMethod()]
		[Description("GetByte(Int32) method when 'i' is too small.")]
		[ExpectedException(typeof(IndexOutOfRangeException))]
		public void DataRecord_Unit_GetByte_IIsTooSmall() {
			DataRecord target = DataRecordTests.CreateDataRecord(new String[] { "FirstName", "LastName", "MyValue" }, new Object[] { "Chad", "Greer", (Byte)1 });
			Int32 i = -1;

			target.GetByte(i);
		}
		[TestMethod()]
		[Description("GetByte(Int32) method when 'i' is too large.")]
		[ExpectedException(typeof(IndexOutOfRangeException))]
		public void DataRecord_Unit_GetByte_IIsTooLarge() {
			DataRecord target = DataRecordTests.CreateDataRecord(new String[] { "FirstName", "LastName", "MyValue" }, new Object[] { "Chad", "Greer", (Byte)1 });
			Int32 i = 3;

			target.GetByte(i);
		}
		[TestMethod()]
		[Description("GetByte(Int32) method when the value referenced by 'i' is not of the correct type.")]
		[ExpectedException(typeof(InvalidCastException))]
		public void DataRecord_Unit_GetByte_IReferencesIncorrectType() {
			DataRecord target = DataRecordTests.CreateDataRecord(new String[] { "FirstName", "LastName", "MyValue" }, new Object[] { "Chad", "Greer", "1" });
			Int32 i = 2;

			target.GetByte(i);
		}

		[TestMethod()]
		[Description("GetBytes(Int32, Int64, Byte[], Int32, Int32) method for the optimal path.")]
		public void DataRecord_Unit_GetBytes_Optimal() {
			Byte[] value = new Byte[] { 1, 2, 3, 4, 5 };
			DataRecord target = DataRecordTests.CreateDataRecord(new String[] { "FirstName", "LastName", "MyValue" }, new Object[] { "Chad", "Greer", value });
			Int32 i = 2;
			Int64 dataIndex = 0;
			Byte[] buffer = new Byte[value.Length];
			Int32 bufferIndex = 0;
			Int32 length = value.Length;

			Int64 expected = value.Length;
			Int64 actual = target.GetBytes(i, dataIndex, buffer, bufferIndex, length);
			Assert.AreEqual(expected, actual);
			CollectionAssert.AreEquivalent(value, buffer);
		}
		[TestMethod()]
		[Description("GetBytes(Int32, Int64, Byte[], Int32, Int32) method when 'i' is too small.")]
		[ExpectedException(typeof(IndexOutOfRangeException))]
		public void DataRecord_Unit_GetBytes_IIsTooSmall() {
			Byte[] value = new Byte[] { 1, 2, 3, 4, 5 };
			DataRecord target = DataRecordTests.CreateDataRecord(new String[] { "FirstName", "LastName", "MyValue" }, new Object[] { "Chad", "Greer", value });
			Int32 i = -1;
			Int64 dataIndex = 0;
			Byte[] buffer = new Byte[value.Length];
			Int32 bufferIndex = 0;
			Int32 length = value.Length;

			target.GetBytes(i, dataIndex, buffer, bufferIndex, length);
		}
		[TestMethod()]
		[Description("GetBytes(Int32, Int64, Byte[], Int32, Int32) method when 'i' is too large.")]
		[ExpectedException(typeof(IndexOutOfRangeException))]
		public void DataRecord_Unit_GetBytes_IIsTooLarge() {
			Byte[] value = new Byte[] { 1, 2, 3, 4, 5 };
			DataRecord target = DataRecordTests.CreateDataRecord(new String[] { "FirstName", "LastName", "MyValue" }, new Object[] { "Chad", "Greer", value });
			Int32 i = 3;
			Int64 dataIndex = 0;
			Byte[] buffer = new Byte[value.Length];
			Int32 bufferIndex = 0;
			Int32 length = value.Length;

			target.GetBytes(i, dataIndex, buffer, bufferIndex, length);
		}
		[TestMethod()]
		[Description("GetBytes(Int32, Int64, Byte[], Int32, Int32) method when the value referenced by 'i' is not of the correct type.")]
		[ExpectedException(typeof(InvalidCastException))]
		public void DataRecord_Unit_GetBytes_IReferencesIncorrectType() {
			Byte[] value = new Byte[] { 1, 2, 3, 4, 5 };
			DataRecord target = DataRecordTests.CreateDataRecord(new String[] { "FirstName", "LastName", "MyValue" }, new Object[] { "Chad", "Greer", value });
			Int32 i = 0;
			Int64 dataIndex = 0;
			Byte[] buffer = new Byte[value.Length];
			Int32 bufferIndex = 0;
			Int32 length = value.Length;

			target.GetBytes(i, dataIndex, buffer, bufferIndex, length);
		}
		[TestMethod()]
		[Description("GetBytes(Int32, Int64, Byte[], Int32, Int32) method when 'dataIndex' is too small.")]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void DataRecord_Unit_GetBytes_DataIndexIsTooSmall() {
			Byte[] value = new Byte[] { 1, 2, 3, 4, 5 };
			DataRecord target = DataRecordTests.CreateDataRecord(new String[] { "FirstName", "LastName", "MyValue" }, new Object[] { "Chad", "Greer", value });
			Int32 i = 2;
			Int64 dataIndex = -1;
			Byte[] buffer = new Byte[value.Length];
			Int32 bufferIndex = 0;
			Int32 length = value.Length;

			target.GetBytes(i, dataIndex, buffer, bufferIndex, length);
		}
		[TestMethod()]
		[Description("GetBytes(Int32, Int64, Byte[], Int32, Int32) method when 'dataIndex' is too large for the buffer.")]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void DataRecord_Unit_GetBytes_DataIndexIsTooLarge1() {
			Byte[] value = new Byte[] { 1, 2, 3, 4, 5 };
			DataRecord target = DataRecordTests.CreateDataRecord(new String[] { "FirstName", "LastName", "MyValue" }, new Object[] { "Chad", "Greer", value });
			Int32 i = 2;
			Int64 dataIndex = (Int64)Int32.MaxValue + 1;
			Byte[] buffer = new Byte[value.Length];
			Int32 bufferIndex = 0;
			Int32 length = value.Length;

			target.GetBytes(i, dataIndex, buffer, bufferIndex, length);
		}
		[TestMethod()]
		[Description("GetBytes(Int32, Int64, Byte[], Int32, Int32) method when 'dataIndex' is too large for the value.")]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void DataRecord_Unit_GetBytes_DataIndexIsTooLarge2() {
			Byte[] value = new Byte[] { 1, 2, 3, 4, 5 };
			DataRecord target = DataRecordTests.CreateDataRecord(new String[] { "FirstName", "LastName", "MyValue" }, new Object[] { "Chad", "Greer", value });
			Int32 i = 2;
			Int64 dataIndex = value.Length;
			Byte[] buffer = new Byte[value.Length];
			Int32 bufferIndex = 0;
			Int32 length = value.Length;

			target.GetBytes(i, dataIndex, buffer, bufferIndex, length);
		}
		[TestMethod()]
		[Description("GetBytes(Int32, Int64, Byte[], Int32, Int32) method when 'bufferIndex' is too small.")]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void DataRecord_Unit_GetBytes_BufferIndexIsTooSmall() {
			Byte[] value = new Byte[] { 1, 2, 3, 4, 5 };
			DataRecord target = DataRecordTests.CreateDataRecord(new String[] { "FirstName", "LastName", "MyValue" }, new Object[] { "Chad", "Greer", value });
			Int32 i = 2;
			Int64 dataIndex = 0;
			Byte[] buffer = new Byte[value.Length];
			Int32 bufferIndex = -1;
			Int32 length = value.Length;

			target.GetBytes(i, dataIndex, buffer, bufferIndex, length);
		}
		[TestMethod()]
		[Description("GetBytes(Int32, Int64, Byte[], Int32, Int32) method when 'bufferIndex' is too large.")]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void DataRecord_Unit_GetBytes_BufferIndexIsTooLarge() {
			Byte[] value = new Byte[] { 1, 2, 3, 4, 5 };
			DataRecord target = DataRecordTests.CreateDataRecord(new String[] { "FirstName", "LastName", "MyValue" }, new Object[] { "Chad", "Greer", value });
			Int32 i = 2;
			Int64 dataIndex = 0;
			Byte[] buffer = new Byte[value.Length];
			Int32 bufferIndex = value.Length;
			Int32 length = value.Length;

			target.GetBytes(i, dataIndex, buffer, bufferIndex, length);
		}
		[TestMethod()]
		[Description("GetBytes(Int32, Int64, Byte[], Int32, Int32) method when 'length' is too small.")]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void DataRecord_Unit_GetBytes_LengthIsTooSmall() {
			Byte[] value = new Byte[] { 1, 2, 3, 4, 5 };
			DataRecord target = DataRecordTests.CreateDataRecord(new String[] { "FirstName", "LastName", "MyValue" }, new Object[] { "Chad", "Greer", value });
			Int32 i = 2;
			Int64 dataIndex = 0;
			Byte[] buffer = new Byte[value.Length];
			Int32 bufferIndex = 0;
			Int32 length = -1;

			target.GetBytes(i, dataIndex, buffer, bufferIndex, length);
		}
		[TestMethod()]
		[Description("GetBytes(Int32, Int64, Byte[], Int32, Int32) method when the length of 'buffer' is too small.")]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void DataRecord_Unit_GetBytes_BufferIsTooSmall() {
			Byte[] value = new Byte[] { 1, 2, 3, 4, 5 };
			DataRecord target = DataRecordTests.CreateDataRecord(new String[] { "FirstName", "LastName", "MyValue" }, new Object[] { "Chad", "Greer", value });
			Int32 i = 2;
			Int64 dataIndex = 0;
			Byte[] buffer = new Byte[value.Length - 1];
			Int32 bufferIndex = 0;
			Int32 length = value.Length;

			target.GetBytes(i, dataIndex, buffer, bufferIndex, length);
		}
		[TestMethod()]
		[Description("GetBytes(Int32, Int64, Byte[], Int32, Int32) method when 'buffer' is a null reference.")]
		public void DataRecord_Unit_GetBytes_BufferIsNull() {
			Byte[] value = new Byte[] { 1, 2, 3, 4, 5 };
			DataRecord target = DataRecordTests.CreateDataRecord(new String[] { "FirstName", "LastName", "MyValue" }, new Object[] { "Chad", "Greer", value });
			Int32 i = 2;
			Int64 dataIndex = 0;
			Byte[] buffer = null;
			Int32 bufferIndex = 0;
			Int32 length = value.Length;

			Int64 expected = value.Length;
			Int64 actual = target.GetBytes(i, dataIndex, buffer, bufferIndex, length);
			Assert.AreEqual(expected, actual);
		}
		[TestMethod()]
		[Description("GetBytes(Int32, Int64, Byte[], Int32, Int32) method when the requested length would exceed the data size.")]
		public void DataRecord_Unit_GetBytes_LengthExceedsDataSize() {
			Byte[] value = new Byte[] { 1, 2, 3, 4, 5 };
			DataRecord target = DataRecordTests.CreateDataRecord(new String[] { "FirstName", "LastName", "MyValue" }, new Object[] { "Chad", "Greer", value });
			Int32 i = 2;
			Int64 dataIndex = 4;
			Byte[] buffer = new Byte[value.Length];
			Int32 bufferIndex = 0;
			Int32 length = value.Length - (Int32)dataIndex + 1;

			Int64 expected = value.Length - (Int32)dataIndex;
			Int64 actual = target.GetBytes(i, dataIndex, buffer, bufferIndex, length);
			Assert.AreEqual(expected, actual);
			CollectionAssert.AreEquivalent(value.Skip((Int32)dataIndex).Concat(Enumerable.Range(0, (Int32)dataIndex).Select(j => (Byte)0)).ToArray(), buffer);
		}

		[TestMethod()]
		[Description("GetChar(Int32) method for the optimal path.")]
		public void DataRecord_Unit_GetChar_Optimal() {
			Char value = 'A';
			DataRecord target = DataRecordTests.CreateDataRecord(new String[] { "FirstName", "LastName", "MyValue" }, new Object[] { "Chad", "Greer", value });
			Int32 i = 2;

			Char actual = target.GetChar(i);
			Assert.AreEqual(value, actual);
		}
		[TestMethod()]
		[Description("GetChar(Int32) method when 'i' is too small.")]
		[ExpectedException(typeof(IndexOutOfRangeException))]
		public void DataRecord_Unit_GetChar_IIsTooSmall() {
			DataRecord target = DataRecordTests.CreateDataRecord(new String[] { "FirstName", "LastName", "MyValue" }, new Object[] { "Chad", "Greer", 'A' });
			Int32 i = -1;

			target.GetChar(i);
		}
		[TestMethod()]
		[Description("GetChar(Int32) method when 'i' is too large.")]
		[ExpectedException(typeof(IndexOutOfRangeException))]
		public void DataRecord_Unit_GetChar_IIsTooLarge() {
			DataRecord target = DataRecordTests.CreateDataRecord(new String[] { "FirstName", "LastName", "MyValue" }, new Object[] { "Chad", "Greer", 'A' });
			Int32 i = 3;

			target.GetChar(i);
		}
		[TestMethod()]
		[Description("GetChar(Int32) method when the value referenced by 'i' is not of the correct type.")]
		[ExpectedException(typeof(InvalidCastException))]
		public void DataRecord_Unit_GetChar_IReferencesIncorrectType() {
			DataRecord target = DataRecordTests.CreateDataRecord(new String[] { "FirstName", "LastName", "MyValue" }, new Object[] { "Chad", "Greer", 1 });
			Int32 i = 2;

			target.GetChar(i);
		}
		[TestMethod()]
		[Description("GetChar(Int32) method when the value is a String.")]
		public void DataRecord_Unit_GetChar_ValueIsString() {
			String value = "A";
			DataRecord target = DataRecordTests.CreateDataRecord(new String[] { "FirstName", "LastName", "MyValue" }, new Object[] { "Chad", "Greer", value });
			Int32 i = 2;

			Char actual = target.GetChar(i);
			Assert.AreEqual(value[0], actual);
		}

		[TestMethod()]
		[Description("GetChars(Int32, Int64, Char[], Int32, Int32) method for the optimal path.")]
		public void DataRecord_Unit_GetChars_Optimal() {
			String value = "Test";
			DataRecord target = DataRecordTests.CreateDataRecord(new String[] { "FirstName", "LastName", "MyValue" }, new Object[] { "Chad", "Greer", value });
			Int32 i = 2;
			Int64 dataIndex = 0;
			Char[] buffer = new Char[value.Length];
			Int32 bufferIndex = 0;
			Int32 length = value.Length;

			Int64 expected = value.Length;
			Int64 actual = target.GetChars(i, dataIndex, buffer, bufferIndex, length);
			Assert.AreEqual(expected, actual);
			CollectionAssert.AreEquivalent(value.ToArray(), buffer);
		}
		[TestMethod()]
		[Description("GetChars(Int32, Int64, Char[], Int32, Int32) method when 'i' is too small.")]
		[ExpectedException(typeof(IndexOutOfRangeException))]
		public void DataRecord_Unit_GetChars_IIsTooSmall() {
			String value = "Test";
			DataRecord target = DataRecordTests.CreateDataRecord(new String[] { "FirstName", "LastName", "MyValue" }, new Object[] { "Chad", "Greer", value });
			Int32 i = -1;
			Int64 dataIndex = 0;
			Char[] buffer = new Char[value.Length];
			Int32 bufferIndex = 0;
			Int32 length = value.Length;

			target.GetChars(i, dataIndex, buffer, bufferIndex, length);
		}
		[TestMethod()]
		[Description("GetChars(Int32, Int64, Char[], Int32, Int32) method when 'i' is too large.")]
		[ExpectedException(typeof(IndexOutOfRangeException))]
		public void DataRecord_Unit_GetChars_IIsTooLarge() {
			String value = "Test";
			DataRecord target = DataRecordTests.CreateDataRecord(new String[] { "FirstName", "LastName", "MyValue" }, new Object[] { "Chad", "Greer", value });
			Int32 i = 3;
			Int64 dataIndex = 0;
			Char[] buffer = new Char[value.Length];
			Int32 bufferIndex = 0;
			Int32 length = value.Length;

			target.GetChars(i, dataIndex, buffer, bufferIndex, length);
		}
		[TestMethod()]
		[Description("GetChars(Int32, Int64, Char[], Int32, Int32) method when the value referenced by 'i' is not of the correct type.")]
		[ExpectedException(typeof(InvalidCastException))]
		public void DataRecord_Unit_GetChars_IReferencesIncorrectType() {
			Int32 value = 2;
			DataRecord target = DataRecordTests.CreateDataRecord(new String[] { "FirstName", "LastName", "MyValue" }, new Object[] { "Chad", "Greer", value });
			Int32 i = 2;
			Int64 dataIndex = 0;
			Char[] buffer = new Char[value.ToString().Length];
			Int32 bufferIndex = 0;
			Int32 length = value.ToString().Length;

			target.GetChars(i, dataIndex, buffer, bufferIndex, length);
		}
		[TestMethod()]
		[Description("GetChars(Int32, Int64, Char[], Int32, Int32) method when 'dataIndex' is too small.")]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void DataRecord_Unit_GetChars_DataIndexIsTooSmall() {
			String value = "Test";
			DataRecord target = DataRecordTests.CreateDataRecord(new String[] { "FirstName", "LastName", "MyValue" }, new Object[] { "Chad", "Greer", value });
			Int32 i = 2;
			Int64 dataIndex = -1;
			Char[] buffer = new Char[value.Length];
			Int32 bufferIndex = 0;
			Int32 length = value.Length;

			target.GetChars(i, dataIndex, buffer, bufferIndex, length);
		}
		[TestMethod()]
		[Description("GetChars(Int32, Int64, Char[], Int32, Int32) method when 'dataIndex' is too large for the buffer.")]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void DataRecord_Unit_GetChars_DataIndexIsTooLarge1() {
			String value = "Test";
			DataRecord target = DataRecordTests.CreateDataRecord(new String[] { "FirstName", "LastName", "MyValue" }, new Object[] { "Chad", "Greer", value });
			Int32 i = 2;
			Int64 dataIndex = (Int64)Int32.MaxValue + 1;
			Char[] buffer = new Char[value.Length];
			Int32 bufferIndex = 0;
			Int32 length = value.Length;

			target.GetChars(i, dataIndex, buffer, bufferIndex, length);
		}
		[TestMethod()]
		[Description("GetChars(Int32, Int64, Char[], Int32, Int32) method when 'dataIndex' is too large for the value.")]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void DataRecord_Unit_GetChars_DataIndexIsTooLarge2() {
			String value = "Test";
			DataRecord target = DataRecordTests.CreateDataRecord(new String[] { "FirstName", "LastName", "MyValue" }, new Object[] { "Chad", "Greer", value });
			Int32 i = 2;
			Int64 dataIndex = value.Length;
			Char[] buffer = new Char[value.Length];
			Int32 bufferIndex = 0;
			Int32 length = value.Length;

			target.GetChars(i, dataIndex, buffer, bufferIndex, length);
		}
		[TestMethod()]
		[Description("GetChars(Int32, Int64, Char[], Int32, Int32) method when 'bufferIndex' is too small.")]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void DataRecord_Unit_GetChars_BufferIndexIsTooSmall() {
			String value = "Test";
			DataRecord target = DataRecordTests.CreateDataRecord(new String[] { "FirstName", "LastName", "MyValue" }, new Object[] { "Chad", "Greer", value });
			Int32 i = 2;
			Int64 dataIndex = 0;
			Char[] buffer = new Char[value.Length];
			Int32 bufferIndex = -1;
			Int32 length = value.Length;

			target.GetChars(i, dataIndex, buffer, bufferIndex, length);
		}
		[TestMethod()]
		[Description("GetChars(Int32, Int64, Char[], Int32, Int32) method when 'bufferIndex' is too large.")]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void DataRecord_Unit_GetChars_BufferIndexIsTooLarge() {
			String value = "Test";
			DataRecord target = DataRecordTests.CreateDataRecord(new String[] { "FirstName", "LastName", "MyValue" }, new Object[] { "Chad", "Greer", value });
			Int32 i = 2;
			Int64 dataIndex = 0;
			Char[] buffer = new Char[value.Length];
			Int32 bufferIndex = value.Length;
			Int32 length = value.Length;

			target.GetChars(i, dataIndex, buffer, bufferIndex, length);
		}
		[TestMethod()]
		[Description("GetChars(Int32, Int64, Char[], Int32, Int32) method when 'length' is too small.")]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void DataRecord_Unit_GetChars_LengthIsTooSmall() {
			String value = "Test";
			DataRecord target = DataRecordTests.CreateDataRecord(new String[] { "FirstName", "LastName", "MyValue" }, new Object[] { "Chad", "Greer", value });
			Int32 i = 2;
			Int64 dataIndex = 0;
			Char[] buffer = new Char[value.Length];
			Int32 bufferIndex = 0;
			Int32 length = -1;

			target.GetChars(i, dataIndex, buffer, bufferIndex, length);
		}
		[TestMethod()]
		[Description("GetChars(Int32, Int64, Char[], Int32, Int32) method when the length of 'buffer' is too small.")]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void DataRecord_Unit_GetChars_BufferIsTooSmall() {
			String value = "Test";
			DataRecord target = DataRecordTests.CreateDataRecord(new String[] { "FirstName", "LastName", "MyValue" }, new Object[] { "Chad", "Greer", value });
			Int32 i = 2;
			Int64 dataIndex = 0;
			Char[] buffer = new Char[value.Length - 1];
			Int32 bufferIndex = 0;
			Int32 length = value.Length;

			target.GetChars(i, dataIndex, buffer, bufferIndex, length);
		}
		[TestMethod()]
		[Description("GetChars(Int32, Int64, Char[], Int32, Int32) method when 'buffer' is a null reference.")]
		public void DataRecord_Unit_GetChars_BufferIsNull() {
			String value = "Test";
			DataRecord target = DataRecordTests.CreateDataRecord(new String[] { "FirstName", "LastName", "MyValue" }, new Object[] { "Chad", "Greer", value });
			Int32 i = 2;
			Int64 dataIndex = 0;
			Char[] buffer = null;
			Int32 bufferIndex = 0;
			Int32 length = value.Length;

			Int64 expected = value.Length;
			Int64 actual = target.GetChars(i, dataIndex, buffer, bufferIndex, length);
			Assert.AreEqual(expected, actual);
		}
		[TestMethod()]
		[Description("GetChars(Int32, Int64, Char[], Int32, Int32) method when the requested length would exceed the data size.")]
		public void DataRecord_Unit_GetChars_LengthExceedsDataSize() {
			String value = "Test";
			DataRecord target = DataRecordTests.CreateDataRecord(new String[] { "FirstName", "LastName", "MyValue" }, new Object[] { "Chad", "Greer", value });
			Int32 i = 2;
			Int64 dataIndex = 3;
			Char[] buffer = new Char[value.Length];
			Int32 bufferIndex = 0;
			Int32 length = value.Length - (Int32)dataIndex + 1;

			Int64 expected = value.Length - (Int32)dataIndex;
			Int64 actual = target.GetChars(i, dataIndex, buffer, bufferIndex, length);
			Assert.AreEqual(expected, actual);
		}

		[TestMethod()]
		[Description("GetDataTypeName(Int32) method for the optimal path.")]
		public void DataRecord_Unit_GetDataTypeName_Optimal() {
			DateTime value = DateTime.Now;
			DataRecord target = DataRecordTests.CreateDataRecord(new String[] { "FirstName", "LastName", "MyValue" }, new Object[] { "Chad", "Greer", value });
			Int32 i = 2;

			String expected = "DateTime";
			String actual = target.GetDataTypeName(i);
			Assert.AreEqual(expected, actual);
		}
		[TestMethod()]
		[Description("GetDataTypeName(Int32) method when 'i' is too small.")]
		[ExpectedException(typeof(IndexOutOfRangeException))]
		public void DataRecord_Unit_GetDataTypeName_IIsTooSmall() {
			DateTime value = DateTime.Now;
			DataRecord target = DataRecordTests.CreateDataRecord(new String[] { "FirstName", "LastName", "MyValue" }, new Object[] { "Chad", "Greer", value });
			Int32 i = -1;

			target.GetDataTypeName(i);
		}
		[TestMethod()]
		[Description("GetDataTypeName(Int32) method when 'i' is too large.")]
		[ExpectedException(typeof(IndexOutOfRangeException))]
		public void DataRecord_Unit_GetDataTypeName_IIsTooLarge() {
			DateTime value = DateTime.Now;
			DataRecord target = DataRecordTests.CreateDataRecord(new String[] { "FirstName", "LastName", "MyValue" }, new Object[] { "Chad", "Greer", value });
			Int32 i = 3;

			target.GetDataTypeName(i);
		}

		[TestMethod()]
		[Description("GetDateTime(Int32) method for the optimal path.")]
		public void DataRecord_Unit_GetDateTime_Optimal() {
			DateTime value = DateTime.Now;
			DataRecord target = DataRecordTests.CreateDataRecord(new String[] { "FirstName", "LastName", "MyValue" }, new Object[] { "Chad", "Greer", value });
			Int32 i = 2;

			DateTime actual = target.GetDateTime(i);
			Assert.AreEqual(value, actual);
		}
		[TestMethod()]
		[Description("GetDateTime(Int32) method when 'i' is too small.")]
		[ExpectedException(typeof(IndexOutOfRangeException))]
		public void DataRecord_Unit_GetDateTime_IIsTooSmall() {
			DataRecord target = DataRecordTests.CreateDataRecord(new String[] { "FirstName", "LastName", "MyValue" }, new Object[] { "Chad", "Greer", DateTime.Now });
			Int32 i = -1;

			target.GetDateTime(i);
		}
		[TestMethod()]
		[Description("GetDateTime(Int32) method when 'i' is too large.")]
		[ExpectedException(typeof(IndexOutOfRangeException))]
		public void DataRecord_Unit_GetDateTime_IIsTooLarge() {
			DataRecord target = DataRecordTests.CreateDataRecord(new String[] { "FirstName", "LastName", "MyValue" }, new Object[] { "Chad", "Greer", DateTime.Now });
			Int32 i = 3;

			target.GetDateTime(i);
		}
		[TestMethod()]
		[Description("GetDateTime(Int32) method when the value referenced by 'i' is not of the correct type.")]
		[ExpectedException(typeof(InvalidCastException))]
		public void DataRecord_Unit_GetDateTime_IReferencesIncorrectType() {
			DataRecord target = DataRecordTests.CreateDataRecord(new String[] { "FirstName", "LastName", "MyValue" }, new Object[] { "Chad", "Greer", "1" });
			Int32 i = 2;

			target.GetDateTime(i);
		}

		[TestMethod()]
		[Description("GetDecimal(Int32) method for the optimal path.")]
		public void DataRecord_Unit_GetDecimal_Optimal() {
			Decimal value = 1;
			DataRecord target = DataRecordTests.CreateDataRecord(new String[] { "FirstName", "LastName", "MyValue" }, new Object[] { "Chad", "Greer", value });
			Int32 i = 2;

			Decimal actual = target.GetDecimal(i);
			Assert.AreEqual(value, actual);
		}
		[TestMethod()]
		[Description("GetDecimal(Int32) method when 'i' is too small.")]
		[ExpectedException(typeof(IndexOutOfRangeException))]
		public void DataRecord_Unit_GetDecimal_IIsTooSmall() {
			DataRecord target = DataRecordTests.CreateDataRecord(new String[] { "FirstName", "LastName", "MyValue" }, new Object[] { "Chad", "Greer", (Decimal)1 });
			Int32 i = -1;

			target.GetDecimal(i);
		}
		[TestMethod()]
		[Description("GetDecimal(Int32) method when 'i' is too large.")]
		[ExpectedException(typeof(IndexOutOfRangeException))]
		public void DataRecord_Unit_GetDecimal_IIsTooLarge() {
			DataRecord target = DataRecordTests.CreateDataRecord(new String[] { "FirstName", "LastName", "MyValue" }, new Object[] { "Chad", "Greer", (Decimal)1 });
			Int32 i = 3;

			target.GetDecimal(i);
		}
		[TestMethod()]
		[Description("GetDecimal(Int32) method when the value referenced by 'i' is not of the correct type.")]
		[ExpectedException(typeof(InvalidCastException))]
		public void DataRecord_Unit_GetDecimal_IReferencesIncorrectType() {
			DataRecord target = DataRecordTests.CreateDataRecord(new String[] { "FirstName", "LastName", "MyValue" }, new Object[] { "Chad", "Greer", "1" });
			Int32 i = 2;

			target.GetDecimal(i);
		}

		[TestMethod()]
		[Description("GetDouble(Int32) method for the optimal path.")]
		public void DataRecord_Unit_GetDouble_Optimal() {
			Double value = 1;
			DataRecord target = DataRecordTests.CreateDataRecord(new String[] { "FirstName", "LastName", "MyValue" }, new Object[] { "Chad", "Greer", value });
			Int32 i = 2;

			Double actual = target.GetDouble(i);
			Assert.AreEqual(value, actual);
		}
		[TestMethod()]
		[Description("GetDouble(Int32) method when 'i' is too small.")]
		[ExpectedException(typeof(IndexOutOfRangeException))]
		public void DataRecord_Unit_GetDouble_IIsTooSmall() {
			DataRecord target = DataRecordTests.CreateDataRecord(new String[] { "FirstName", "LastName", "MyValue" }, new Object[] { "Chad", "Greer", (Double)1 });
			Int32 i = -1;

			target.GetDouble(i);
		}
		[TestMethod()]
		[Description("GetDouble(Int32) method when 'i' is too large.")]
		[ExpectedException(typeof(IndexOutOfRangeException))]
		public void DataRecord_Unit_GetDouble_IIsTooLarge() {
			DataRecord target = DataRecordTests.CreateDataRecord(new String[] { "FirstName", "LastName", "MyValue" }, new Object[] { "Chad", "Greer", (Double)1 });
			Int32 i = 3;

			target.GetDouble(i);
		}
		[TestMethod()]
		[Description("GetDouble(Int32) method when the value referenced by 'i' is not of the correct type.")]
		[ExpectedException(typeof(InvalidCastException))]
		public void DataRecord_Unit_GetDouble_IReferencesIncorrectType() {
			DataRecord target = DataRecordTests.CreateDataRecord(new String[] { "FirstName", "LastName", "MyValue" }, new Object[] { "Chad", "Greer", "1" });
			Int32 i = 2;

			target.GetDouble(i);
		}

		[TestMethod()]
		[Description("GetFieldType(Int32) method for the optimal path.")]
		public void DataRecord_Unit_GetFieldType_Optimal() {
			DateTime value = DateTime.Now;
			DataRecord target = DataRecordTests.CreateDataRecord(new String[] { "FirstName", "LastName", "MyValue" }, new Object[] { "Chad", "Greer", value });
			Int32 i = 2;

			Type expected = typeof(DateTime);
			Type actual = target.GetFieldType(i);
			Assert.AreEqual(expected, actual);
		}
		[TestMethod()]
		[Description("GetFieldType(Int32) method when 'i' is too small.")]
		[ExpectedException(typeof(IndexOutOfRangeException))]
		public void DataRecord_Unit_GetFieldType_IIsTooSmall() {
			DateTime value = DateTime.Now;
			DataRecord target = DataRecordTests.CreateDataRecord(new String[] { "FirstName", "LastName", "MyValue" }, new Object[] { "Chad", "Greer", value });
			Int32 i = -1;

			target.GetFieldType(i);
		}
		[TestMethod()]
		[Description("GetFieldType(Int32) method when 'i' is too large.")]
		[ExpectedException(typeof(IndexOutOfRangeException))]
		public void DataRecord_Unit_GetFieldType_IIsTooLarge() {
			DateTime value = DateTime.Now;
			DataRecord target = DataRecordTests.CreateDataRecord(new String[] { "FirstName", "LastName", "MyValue" }, new Object[] { "Chad", "Greer", value });
			Int32 i = 3;

			target.GetFieldType(i);
		}

		[TestMethod()]
		[Description("GetFloat(Int32) method for the optimal path.")]
		public void DataRecord_Unit_GetFloat_Optimal() {
			Single value = 1;
			DataRecord target = DataRecordTests.CreateDataRecord(new String[] { "FirstName", "LastName", "MyValue" }, new Object[] { "Chad", "Greer", value });
			Int32 i = 2;

			Single actual = target.GetFloat(i);
			Assert.AreEqual(value, actual);
		}
		[TestMethod()]
		[Description("GetFloat(Int32) method when 'i' is too small.")]
		[ExpectedException(typeof(IndexOutOfRangeException))]
		public void DataRecord_Unit_GetFloat_IIsTooSmall() {
			DataRecord target = DataRecordTests.CreateDataRecord(new String[] { "FirstName", "LastName", "MyValue" }, new Object[] { "Chad", "Greer", (Single)1 });
			Int32 i = -1;

			target.GetFloat(i);
		}
		[TestMethod()]
		[Description("GetFloat(Int32) method when 'i' is too large.")]
		[ExpectedException(typeof(IndexOutOfRangeException))]
		public void DataRecord_Unit_GetFloat_IIsTooLarge() {
			DataRecord target = DataRecordTests.CreateDataRecord(new String[] { "FirstName", "LastName", "MyValue" }, new Object[] { "Chad", "Greer", (Single)1 });
			Int32 i = 3;

			target.GetFloat(i);
		}
		[TestMethod()]
		[Description("GetFloat(Int32) method when the value referenced by 'i' is not of the correct type.")]
		[ExpectedException(typeof(InvalidCastException))]
		public void DataRecord_Unit_GetFloat_IReferencesIncorrectType() {
			DataRecord target = DataRecordTests.CreateDataRecord(new String[] { "FirstName", "LastName", "MyValue" }, new Object[] { "Chad", "Greer", "1" });
			Int32 i = 2;

			target.GetFloat(i);
		}

		[TestMethod()]
		[Description("GetGuid(Int32) method for the optimal path.")]
		public void DataRecord_Unit_GetGuid_Optimal() {
			Guid value = Guid.NewGuid();
			DataRecord target = DataRecordTests.CreateDataRecord(new String[] { "FirstName", "LastName", "MyValue" }, new Object[] { "Chad", "Greer", value });
			Int32 i = 2;

			Guid actual = target.GetGuid(i);
			Assert.AreEqual(value, actual);
		}
		[TestMethod()]
		[Description("GetGuid(Int32) method when 'i' is too small.")]
		[ExpectedException(typeof(IndexOutOfRangeException))]
		public void DataRecord_Unit_GetGuid_IIsTooSmall() {
			DataRecord target = DataRecordTests.CreateDataRecord(new String[] { "FirstName", "LastName", "MyValue" }, new Object[] { "Chad", "Greer", Guid.NewGuid() });
			Int32 i = -1;

			target.GetGuid(i);
		}
		[TestMethod()]
		[Description("GetGuid(Int32) method when 'i' is too large.")]
		[ExpectedException(typeof(IndexOutOfRangeException))]
		public void DataRecord_Unit_GetGuid_IIsTooLarge() {
			DataRecord target = DataRecordTests.CreateDataRecord(new String[] { "FirstName", "LastName", "MyValue" }, new Object[] { "Chad", "Greer", Guid.NewGuid() });
			Int32 i = 3;

			target.GetGuid(i);
		}
		[TestMethod()]
		[Description("GetGuid(Int32) method when the value referenced by 'i' is not of the correct type.")]
		[ExpectedException(typeof(InvalidCastException))]
		public void DataRecord_Unit_GetGuid_IReferencesIncorrectType() {
			DataRecord target = DataRecordTests.CreateDataRecord(new String[] { "FirstName", "LastName", "MyValue" }, new Object[] { "Chad", "Greer", "1" });
			Int32 i = 2;

			target.GetGuid(i);
		}

		[TestMethod()]
		[Description("GetInt16(Int32) method for the optimal path.")]
		public void DataRecord_Unit_GetInt16_Optimal() {
			Int16 value = 1;
			DataRecord target = DataRecordTests.CreateDataRecord(new String[] { "FirstName", "LastName", "MyValue" }, new Object[] { "Chad", "Greer", value });
			Int32 i = 2;

			Int16 actual = target.GetInt16(i);
			Assert.AreEqual(value, actual);
		}
		[TestMethod()]
		[Description("GetInt16(Int32) method when 'i' is too small.")]
		[ExpectedException(typeof(IndexOutOfRangeException))]
		public void DataRecord_Unit_GetInt16_IIsTooSmall() {
			DataRecord target = DataRecordTests.CreateDataRecord(new String[] { "FirstName", "LastName", "MyValue" }, new Object[] { "Chad", "Greer", (Int16)1 });
			Int32 i = -1;

			target.GetInt16(i);
		}
		[TestMethod()]
		[Description("GetInt16(Int32) method when 'i' is too large.")]
		[ExpectedException(typeof(IndexOutOfRangeException))]
		public void DataRecord_Unit_GetInt16_IIsTooLarge() {
			DataRecord target = DataRecordTests.CreateDataRecord(new String[] { "FirstName", "LastName", "MyValue" }, new Object[] { "Chad", "Greer", (Int16)1 });
			Int32 i = 3;

			target.GetInt16(i);
		}
		[TestMethod()]
		[Description("GetInt16(Int32) method when the value referenced by 'i' is not of the correct type.")]
		[ExpectedException(typeof(InvalidCastException))]
		public void DataRecord_Unit_GetInt16_IReferencesIncorrectType() {
			DataRecord target = DataRecordTests.CreateDataRecord(new String[] { "FirstName", "LastName", "MyValue" }, new Object[] { "Chad", "Greer", "1" });
			Int32 i = 2;

			target.GetInt16(i);
		}

		[TestMethod()]
		[Description("GetInt32(Int32) method for the optimal path.")]
		public void DataRecord_Unit_GetInt32_Optimal() {
			Int32 value = 1;
			DataRecord target = DataRecordTests.CreateDataRecord(new String[] { "FirstName", "LastName", "MyValue" }, new Object[] { "Chad", "Greer", value });
			Int32 i = 2;

			Int32 actual = target.GetInt32(i);
			Assert.AreEqual(value, actual);
		}
		[TestMethod()]
		[Description("GetInt32(Int32) method when 'i' is too small.")]
		[ExpectedException(typeof(IndexOutOfRangeException))]
		public void DataRecord_Unit_GetInt32_IIsTooSmall() {
			DataRecord target = DataRecordTests.CreateDataRecord(new String[] { "FirstName", "LastName", "MyValue" }, new Object[] { "Chad", "Greer", (Int32)1 });
			Int32 i = -1;

			target.GetInt32(i);
		}
		[TestMethod()]
		[Description("GetInt32(Int32) method when 'i' is too large.")]
		[ExpectedException(typeof(IndexOutOfRangeException))]
		public void DataRecord_Unit_GetInt32_IIsTooLarge() {
			DataRecord target = DataRecordTests.CreateDataRecord(new String[] { "FirstName", "LastName", "MyValue" }, new Object[] { "Chad", "Greer", (Int32)1 });
			Int32 i = 3;

			target.GetInt32(i);
		}
		[TestMethod()]
		[Description("GetInt32(Int32) method when the value referenced by 'i' is not of the correct type.")]
		[ExpectedException(typeof(InvalidCastException))]
		public void DataRecord_Unit_GetInt32_IReferencesIncorrectType() {
			DataRecord target = DataRecordTests.CreateDataRecord(new String[] { "FirstName", "LastName", "MyValue" }, new Object[] { "Chad", "Greer", "1" });
			Int32 i = 2;

			target.GetInt32(i);
		}

		[TestMethod()]
		[Description("GetInt64(Int32) method for the optimal path.")]
		public void DataRecord_Unit_GetInt64_Optimal() {
			Int64 value = 1;
			DataRecord target = DataRecordTests.CreateDataRecord(new String[] { "FirstName", "LastName", "MyValue" }, new Object[] { "Chad", "Greer", value });
			Int32 i = 2;

			Int64 actual = target.GetInt64(i);
			Assert.AreEqual(value, actual);
		}
		[TestMethod()]
		[Description("GetInt64(Int32) method when 'i' is too small.")]
		[ExpectedException(typeof(IndexOutOfRangeException))]
		public void DataRecord_Unit_GetInt64_IIsTooSmall() {
			DataRecord target = DataRecordTests.CreateDataRecord(new String[] { "FirstName", "LastName", "MyValue" }, new Object[] { "Chad", "Greer", (Int64)1 });
			Int32 i = -1;

			target.GetInt64(i);
		}
		[TestMethod()]
		[Description("GetInt64(Int32) method when 'i' is too large.")]
		[ExpectedException(typeof(IndexOutOfRangeException))]
		public void DataRecord_Unit_GetInt64_IIsTooLarge() {
			DataRecord target = DataRecordTests.CreateDataRecord(new String[] { "FirstName", "LastName", "MyValue" }, new Object[] { "Chad", "Greer", (Int64)1 });
			Int32 i = 3;

			target.GetInt64(i);
		}
		[TestMethod()]
		[Description("GetInt64(Int32) method when the value referenced by 'i' is not of the correct type.")]
		[ExpectedException(typeof(InvalidCastException))]
		public void DataRecord_Unit_GetInt64_IReferencesIncorrectType() {
			DataRecord target = DataRecordTests.CreateDataRecord(new String[] { "FirstName", "LastName", "MyValue" }, new Object[] { "Chad", "Greer", "1" });
			Int32 i = 2;

			target.GetInt64(i);
		}

		[TestMethod()]
		[Description("GetName(Int32) method for the optimal path.")]
		public void DataRecord_Unit_GetName_Optimal() {
			String columnName = "MyValue";
			DataRecord target = DataRecordTests.CreateDataRecord(new String[] { "FirstName", "LastName", columnName }, new Object[] { "Chad", "Greer", DateTime.Now });
			Int32 i = 2;

			String expected = columnName;
			String actual = target.GetName(i);
			Assert.AreEqual(expected, actual);
		}
		[TestMethod()]
		[Description("GetName(Int32) method when 'i' is too small.")]
		[ExpectedException(typeof(IndexOutOfRangeException))]
		public void DataRecord_Unit_GetName_IIsTooSmall() {
			String columnName = "MyValue";
			DataRecord target = DataRecordTests.CreateDataRecord(new String[] { "FirstName", "LastName", columnName }, new Object[] { "Chad", "Greer", DateTime.Now });
			Int32 i = -1;

			target.GetName(i);
		}
		[TestMethod()]
		[Description("GetName(Int32) method when 'i' is too large.")]
		[ExpectedException(typeof(IndexOutOfRangeException))]
		public void DataRecord_Unit_GetName_IIsTooLarge() {
			String columnName = "MyValue";
			DataRecord target = DataRecordTests.CreateDataRecord(new String[] { "FirstName", "LastName", columnName }, new Object[] { "Chad", "Greer", DateTime.Now });
			Int32 i = 3;

			target.GetName(i);
		}

		[TestMethod()]
		[Description("GetOrdinal(String) method for the optimal path.")]
		public void DataRecord_Unit_GetOrdinal_Optimal() {
			String columnName = "MyValue";
			DataRecord target = DataRecordTests.CreateDataRecord(new String[] { "FirstName", "LastName", columnName }, new Object[] { "Chad", "Greer", DateTime.Now });
			String name = columnName;

			Int32 expected = 2;
			Int32 actual = target.GetOrdinal(name);
			Assert.AreEqual(expected, actual);
		}
		[TestMethod()]
		[Description("GetOrdinal(String) method when 'name' does not exist.")]
		[ExpectedException(typeof(IndexOutOfRangeException))]
		public void DataRecord_Unit_GetOrdinal_NameDoesNotExist() {
			String columnName = "MyValue";
			DataRecord target = DataRecordTests.CreateDataRecord(new String[] { "FirstName", "LastName", columnName }, new Object[] { "Chad", "Greer", DateTime.Now });
			String name = "NoColumn";

			target.GetOrdinal(name);
		}
		[TestMethod()]
		[Description("GetOrdinal(String) method when 'name' is a null reference.")]
		[ExpectedException(typeof(ArgumentNullException))]
		public void DataRecord_Unit_GetOrdinal_NameIsNull() {
			String columnName = "MyValue";
			DataRecord target = DataRecordTests.CreateDataRecord(new String[] { "FirstName", "LastName", columnName }, new Object[] { "Chad", "Greer", DateTime.Now });
			String name = null;

			target.GetOrdinal(name);
		}

		[TestMethod()]
		[Description("GetString(Int32) method for the optimal path.")]
		public void DataRecord_Unit_GetString_Optimal() {
			String value = "Test";
			DataRecord target = DataRecordTests.CreateDataRecord(new String[] { "FirstName", "LastName", "MyValue" }, new Object[] { "Chad", "Greer", value });
			Int32 i = 2;

			String actual = target.GetString(i);
			Assert.AreEqual(value, actual);
		}
		[TestMethod()]
		[Description("GetString(Int32) method when 'i' is too small.")]
		[ExpectedException(typeof(IndexOutOfRangeException))]
		public void DataRecord_Unit_GetString_IIsTooSmall() {
			DataRecord target = DataRecordTests.CreateDataRecord(new String[] { "FirstName", "LastName", "MyValue" }, new Object[] { "Chad", "Greer", "Test" });
			Int32 i = -1;

			target.GetString(i);
		}
		[TestMethod()]
		[Description("GetString(Int32) method when 'i' is too large.")]
		[ExpectedException(typeof(IndexOutOfRangeException))]
		public void DataRecord_Unit_GetString_IIsTooLarge() {
			DataRecord target = DataRecordTests.CreateDataRecord(new String[] { "FirstName", "LastName", "MyValue" }, new Object[] { "Chad", "Greer", "Test" });
			Int32 i = 3;

			target.GetString(i);
		}
		[TestMethod()]
		[Description("GetString(Int32) method when the value referenced by 'i' is not of the correct type.")]
		[ExpectedException(typeof(InvalidCastException))]
		public void DataRecord_Unit_GetString_IReferencesIncorrectType() {
			DataRecord target = DataRecordTests.CreateDataRecord(new String[] { "FirstName", "LastName", "MyValue" }, new Object[] { "Chad", "Greer", 1 });
			Int32 i = 2;

			target.GetString(i);
		}


		[TestMethod()]
		[Description("GetValue(Int32) method for the optimal path.")]
		public void DataRecord_Unit_GetValue_Optimal() {
			String value = "Test";
			DataRecord target = DataRecordTests.CreateDataRecord(new String[] { "FirstName", "LastName", "MyValue" }, new Object[] { "Chad", "Greer", value });
			Int32 i = 2;

			Object actual = target.GetValue(i);
			Assert.AreEqual(value, actual);
		}
		[TestMethod()]
		[Description("GetValue(Int32) method when 'i' is too small.")]
		[ExpectedException(typeof(IndexOutOfRangeException))]
		public void DataRecord_Unit_GetValue_IIsTooSmall() {
			DataRecord target = DataRecordTests.CreateDataRecord(new String[] { "FirstName", "LastName", "MyValue" }, new Object[] { "Chad", "Greer", "Test" });
			Int32 i = -1;

			target.GetValue(i);
		}
		[TestMethod()]
		[Description("GetValue(Int32) method when 'i' is too large.")]
		[ExpectedException(typeof(IndexOutOfRangeException))]
		public void DataRecord_Unit_GetValue_IIsTooLarge() {
			DataRecord target = DataRecordTests.CreateDataRecord(new String[] { "FirstName", "LastName", "MyValue" }, new Object[] { "Chad", "Greer", "Test" });
			Int32 i = 3;

			target.GetValue(i);
		}

		[TestMethod()]
		[Description("GetValues(Object[]) method for the optimal path.")]
		public void DataRecord_Unit_GetValues_Optimal() {
			Object[] originalValues = new Object[] { "Chad", "Greer", "Test" };
			DataRecord target = DataRecordTests.CreateDataRecord(new String[] { "FirstName", "LastName", "MyValue" }, originalValues);
			Object[] values = Enumerable.Range(0, originalValues.Length).Select(i => (Object)Guid.NewGuid()).ToArray();

			Int32 expected = originalValues.Length;
			Int32 actual = target.GetValues(values);
			Assert.AreEqual(expected, actual);
			CollectionAssert.AreEquivalent(originalValues, values);
		}
		[TestMethod()]
		[Description("GetValues(Object[]) method when 'values' is a null reference.")]
		[ExpectedException(typeof(ArgumentNullException))]
		public void DataRecord_Unit_GetValues_ValuesIsNull() {
			Object[] originalValues = new Object[] { "Chad", "Greer", "Test" };
			DataRecord target = DataRecordTests.CreateDataRecord(new String[] { "FirstName", "LastName", "MyValue" }, originalValues);
			Object[] values = null;

			target.GetValues(values);
		}
		[TestMethod()]
		[Description("GetValues(Object[]) method when 'values' is too small.")]
		public void DataRecord_Unit_GetValues_ValuesIsTooSmall() {
			Object[] originalValues = new Object[] { "Chad", "Greer", "Test" };
			DataRecord target = DataRecordTests.CreateDataRecord(new String[] { "FirstName", "LastName", "MyValue" }, originalValues);
			Object[] values = Enumerable.Range(0, originalValues.Length - 1).Select(i => (Object)Guid.NewGuid()).ToArray();

			Int32 expected = values.Length;
			Int32 actual = target.GetValues(values);
			Assert.AreEqual(expected, actual);
			CollectionAssert.IsSubsetOf(values, originalValues);
		}
		[TestMethod()]
		[Description("GetValues(Object[]) method when 'values' is too large.")]
		public void DataRecord_Unit_GetValues_ValuesIsTooLarge() {
			Object[] originalValues = new Object[] { "Chad", "Greer", "Test" };
			DataRecord target = DataRecordTests.CreateDataRecord(new String[] { "FirstName", "LastName", "MyValue" }, originalValues);
			Object[] values = Enumerable.Range(0, originalValues.Length + 1).Select(i => (Object)Guid.NewGuid()).ToArray();

			Int32 expected = originalValues.Length;
			Int32 actual = target.GetValues(values);
			Assert.AreEqual(expected, actual);
			CollectionAssert.IsSubsetOf(originalValues, values);
		}
		[TestMethod()]
		[Description("GetValues(Object[]) method when 'values' is empty.")]
		public void DataRecord_Unit_GetValues_ValuesIsEmpty() {
			Object[] originalValues = new Object[] { "Chad", "Greer", "Test" };
			DataRecord target = DataRecordTests.CreateDataRecord(new String[] { "FirstName", "LastName", "MyValue" }, originalValues);
			Object[] values = new Object[0];

			Int32 expected = 0;
			Int32 actual = target.GetValues(values);
			Assert.AreEqual(expected, actual);
		}

		[TestMethod()]
		[Description("IsDBNull(Int32) method when the value is a null reference.")]
		public void DataRecord_Unit_IsDBNull_Null() {
			Object value = null;
			DataRecord target = DataRecordTests.CreateDataRecord(new String[] { "FirstName", "LastName", "MyValue" }, new Object[] { "Chad", "Greer", value });
			Int32 i = 2;

			Boolean expected = true;
			Boolean actual = target.IsDBNull(i);
			Assert.AreEqual(expected, actual);
		}
		[TestMethod()]
		[Description("IsDBNull(Int32) method when 'i' is too small.")]
		[ExpectedException(typeof(IndexOutOfRangeException))]
		public void DataRecord_Unit_IsDBNull_IIsTooSmall() {
			Object value = null;
			DataRecord target = DataRecordTests.CreateDataRecord(new String[] { "FirstName", "LastName", "MyValue" }, new Object[] { "Chad", "Greer", value });
			Int32 i = -1;

			target.IsDBNull(i);
		}
		[TestMethod()]
		[Description("IsDBNull(Int32) method when 'i' is too large.")]
		[ExpectedException(typeof(IndexOutOfRangeException))]
		public void DataRecord_Unit_IsDBNull_IIsTooLarge() {
			Object value = null;
			DataRecord target = DataRecordTests.CreateDataRecord(new String[] { "FirstName", "LastName", "MyValue" }, new Object[] { "Chad", "Greer", value });
			Int32 i = 3;

			target.IsDBNull(i);
		}

	// ICustomTypeDescriptor Member Tests
		[TestMethod()]
		[Description("ICustomTypeDescriptor.GetAttributes() method for the optimal path.")]
		public void DataRecord_Unit_GetAttributes_Optimal() {
			ICustomTypeDescriptor target = DataRecordTests.CreateDataRecord(new String[] { "FirstName", "LastName", "MyValue" }, new Object[] { "Chad", "Greer", "Test" });
			target.GetAttributes();
		}

		[TestMethod()]
		[Description("ICustomTypeDescriptor.GetClassName() method for the optimal path.")]
		public void DataRecord_Unit_GetClassName_Optimal() {
			ICustomTypeDescriptor target = DataRecordTests.CreateDataRecord(new String[] { "FirstName", "LastName", "MyValue" }, new Object[] { "Chad", "Greer", "Test" });
			target.GetClassName();
		}

		[TestMethod()]
		[Description("ICustomTypeDescriptor.GetComponentName() method for the optimal path.")]
		public void DataRecord_Unit_GetComponentName_Optimal() {
			ICustomTypeDescriptor target = DataRecordTests.CreateDataRecord(new String[] { "FirstName", "LastName", "MyValue" }, new Object[] { "Chad", "Greer", "Test" });
			target.GetComponentName();
		}

		[TestMethod()]
		[Description("ICustomTypeDescriptor.GetConverter() method for the optimal path.")]
		public void DataRecord_Unit_GetConverter_Optimal() {
			ICustomTypeDescriptor target = DataRecordTests.CreateDataRecord(new String[] { "FirstName", "LastName", "MyValue" }, new Object[] { "Chad", "Greer", "Test" });
			target.GetConverter();
		}

		[TestMethod()]
		[Description("ICustomTypeDescriptor.GetDefaultEvent() method for the optimal path.")]
		public void DataRecord_Unit_GetDefaultEvent_Optimal() {
			ICustomTypeDescriptor target = DataRecordTests.CreateDataRecord(new String[] { "FirstName", "LastName", "MyValue" }, new Object[] { "Chad", "Greer", "Test" });
			target.GetDefaultEvent();
		}

		[TestMethod()]
		[Description("ICustomTypeDescriptor.GetDefaultProperty() method for the optimal path.")]
		public void DataRecord_Unit_GetDefaultProperty_Optimal() {
			ICustomTypeDescriptor target = DataRecordTests.CreateDataRecord(new String[] { "FirstName", "LastName", "MyValue" }, new Object[] { "Chad", "Greer", "Test" });
			target.GetDefaultProperty();
		}

		[TestMethod()]
		[Description("ICustomTypeDescriptor.GetEditor(Type) method for the optimal path.")]
		public void DataRecord_Unit_GetEditor_Optimal() {
			ICustomTypeDescriptor target = DataRecordTests.CreateDataRecord(new String[] { "FirstName", "LastName", "MyValue" }, new Object[] { "Chad", "Greer", "Test" });
			Type editorBaseType = null;
			target.GetEditor(editorBaseType);
		}

		[TestMethod()]
		[Description("ICustomTypeDescriptor.GetEvents() method for the optimal path.")]
		public void DataRecord_Unit_GetEvents1_Optimal() {
			ICustomTypeDescriptor target = DataRecordTests.CreateDataRecord(new String[] { "FirstName", "LastName", "MyValue" }, new Object[] { "Chad", "Greer", "Test" });
			target.GetEvents();
		}

		[TestMethod()]
		[Description("ICustomTypeDescriptor.GetEvents(Attribute[]) method for the optimal path.")]
		public void DataRecord_Unit_GetEvents2_Optimal() {
			ICustomTypeDescriptor target = DataRecordTests.CreateDataRecord(new String[] { "FirstName", "LastName", "MyValue" }, new Object[] { "Chad", "Greer", "Test" });
			Attribute[] attributes = null;
			target.GetEvents(attributes);
		}

		[TestMethod()]
		[Description("ICustomTypeDescriptor.GetProperties() method for the optimal path.")]
		public void DataRecord_Unit_GetProperties1_Optimal() {
			ICustomTypeDescriptor target = DataRecordTests.CreateDataRecord(new String[] { "FirstName", "LastName", "MyValue" }, new Object[] { "Chad", "Greer", "Test" });
			target.GetProperties();
		}

		[TestMethod()]
		[Description("ICustomTypeDescriptor.GetProperties(Attribute[]) method for the optimal path.")]
		public void DataRecord_Unit_GetProperties2_Optimal() {
			ICustomTypeDescriptor target = DataRecordTests.CreateDataRecord(new String[] { "FirstName", "LastName", "MyValue" }, new Object[] { "Chad", "Greer", "Test" });
			Attribute[] attributes = null;
			target.GetProperties(attributes);
		}

		[TestMethod()]
		[Description("ICustomTypeDescriptor.GetPropertyOwner(PropertyDescriptor) method for the optimal path.")]
		public void DataRecord_Unit_GetPropertyOwner_Optimal() {
			ICustomTypeDescriptor target = DataRecordTests.CreateDataRecord(new String[] { "FirstName", "LastName", "MyValue" }, new Object[] { "Chad", "Greer", "Test" });
			PropertyDescriptor pd = null;
			target.GetPropertyOwner(pd);
		}
	}
}
