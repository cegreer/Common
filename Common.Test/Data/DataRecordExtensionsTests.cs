using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Vizistata.Mocks;

namespace Vizistata.Data {
	/// <summary>
	/// This is a test class for <see cref="T:DataRecordExtensions"/> and is intended to contain all <see cref="T:DataRecordExtensions"/> Unit Tests.
	///</summary>
	[TestClass()]
	public class DataRecordExtensionsTests {
		#region Test Class Implementation
		/// <summary>
		/// Describes the context under which the current test is running.
		/// </summary>
		private TestContext _testContextInstance;

		/// <summary>
		/// Initializes a new instance of the <see cref="T:DataRecordExtensionsTests"/> class.
		/// </summary>
		public DataRecordExtensionsTests() : base() { }

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

	// Method Tests
		[TestMethod()]
		[Description("Field<T>(IDataRecord, String) method for the optimal path.")]
		public void DataRecordExtensions_Unit_Field1_Optimal() {
			String key = "Name";
			String value = "This is a test";
			IDictionary<String, Object> values = new Dictionary<String, Object>() {
				{ key, value }
			};

			IDataRecord dataRecord = new MockDataRecord(values);
			String name = key;
			String actual = DataRecordExtensions.Field<String>(dataRecord, name);
			
			String expected = value;
			Assert.AreEqual(expected, actual);
		}
		[TestMethod()]
		[Description("Field<T>(IDataRecord, String) method when 'dataRecord' is a null reference.")]
		[ExpectedException(typeof(ArgumentNullException))]
		public void DataRecordExtensions_Unit_Field1_DataRecordIsNull() {
			IDataRecord dataRecord = null;
			String name = "Name";
			DataRecordExtensions.Field<String>(dataRecord, name);
		}
		[TestMethod()]
		[Description("Field<T>(IDataRecord, String) method when 'name' is a null reference.")]
		[ExpectedException(typeof(ArgumentNullException))]
		public void DataRecordExtensions_Unit_Field1_NameIsNull() {
			String key = "Name";
			String value = "This is a test";
			IDictionary<String, Object> values = new Dictionary<String, Object>() {
				{ key, value }
			};

			IDataRecord dataRecord = new MockDataRecord(values);
			String name = null;
			DataRecordExtensions.Field<String>(dataRecord, name);
		}
		[TestMethod()]
		[Description("Field<T>(IDataRecord, String) method when T is an invalid type.")]
		[ExpectedException(typeof(InvalidCastException))]
		public void DataRecordExtensions_Unit_Field1_TIsInvalid() {
			String key = "Name";
			String value = "This is a test";
			IDictionary<String, Object> values = new Dictionary<String, Object>() {
				{ key, value }
			};

			IDataRecord dataRecord = new MockDataRecord(values);
			String name = key;
			DataRecordExtensions.Field<DateTime>(dataRecord, name);
		}

		[TestMethod()]
		[Description("Field<T>(IDataRecord, Int32) method for the optimal path.")]
		public void DataRecordExtensions_Unit_Field2_Optimal() {
			String key = "Name";
			String value = "This is a test";
			IDictionary<String, Object> values = new Dictionary<String, Object>() {
				{ key, value }
			};

			IDataRecord dataRecord = new MockDataRecord(values);
			Int32 index = 0;
			String actual = DataRecordExtensions.Field<String>(dataRecord, index);

			String expected = value;
			Assert.AreEqual(expected, actual);
		}
		[TestMethod()]
		[Description("Field<T>(IDataRecord, Int32) method when 'dataRecord' is a null reference.")]
		[ExpectedException(typeof(ArgumentNullException))]
		public void DataRecordExtensions_Unit_Field2_DataRecordIsNull() {
			IDataRecord dataRecord = null;
			Int32 index = 0;
			DataRecordExtensions.Field<String>(dataRecord, index);
		}
		[TestMethod()]
		[Description("Field<T>(IDataRecord, Int32) method when 'index' is less than 0.")]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void DataRecordExtensions_Unit_Field2_IndexIsLessThan0() {
			String key = "Name";
			String value = "This is a test";
			IDictionary<String, Object> values = new Dictionary<String, Object>() {
				{ key, value }
			};

			IDataRecord dataRecord = new MockDataRecord(values);
			Int32 index = -1;
			DataRecordExtensions.Field<String>(dataRecord, index);
		}
		[TestMethod()]
		[Description("Field<T>(IDataRecord, Int32) method when 'index' is too large for the data record.")]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void DataRecordExtensions_Unit_Field2_IndexIsTooLarge() {
			String key = "Name";
			String value = "This is a test";
			IDictionary<String, Object> values = new Dictionary<String, Object>() {
				{ key, value }
			};

			IDataRecord dataRecord = new MockDataRecord(values);
			Int32 index = values.Count;
			DataRecordExtensions.Field<String>(dataRecord, index);
		}
		[TestMethod()]
		[Description("Field<T>(IDataRecord, Int32) method when T is an invalid type.")]
		[ExpectedException(typeof(InvalidCastException))]
		public void DataRecordExtensions_Unit_Field2_TIsInvalid() {
			String key = "Name";
			String value = "This is a test";
			IDictionary<String, Object> values = new Dictionary<String, Object>() {
				{ key, value }
			};

			IDataRecord dataRecord = new MockDataRecord(values);
			Int32 index = 0;
			DataRecordExtensions.Field<DateTime>(dataRecord, index);
		}
	}
}