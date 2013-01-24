using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Vizistata.Data {
	/// <summary>
	/// This is a test class for <see cref="T:DbDataReaderExtensions"/> and is intended to contain all <see cref="T:DbDataReaderExtensions"/> Unit Tests.
	///</summary>
	[TestClass()]
	public class DbDataReaderExtensionsTests {
		#region Test Class Implementation
		/// <summary>
		/// Describes the context under which the current test is running.
		/// </summary>
		private TestContext _testContextInstance;

		/// <summary>
		/// Initializes a new instance of the <see cref="T:DbDataReaderExtensionsTests"/> class.
		/// </summary>
		public DbDataReaderExtensionsTests() : base() { }

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
		/// Creates a target <see cref="T:IDataReader"/> object that can be tested.
		/// </summary>
		/// <param name="columnNames">The column names.</param>
		/// <param name="values">The values.</param>
		/// <returns>The <see cref="T:IDataReader"/> created.</returns>
		private static IDataReader CreateDataRecord(String[] columnNames, params IEnumerable<Object>[] values) {
			Debug.Assert(columnNames != null);
			Debug.Assert(columnNames.Length != 0);
			Debug.Assert(columnNames.All(columnName => columnName != null));

			using (DataTable dataTable = new DataTable()) {
				DataColumn[] dataColumns = columnNames
					.Select((columnName, i) => new DataColumn(columnName, (values != null && values.Length > 0 && values[0].ElementAt(i) != null) ? values[0].ElementAt(i).GetType() : typeof(String)))
					.ToArray();
				dataTable.Columns.AddRange(dataColumns);

				if (values != null && values.Length > 0) {
					foreach (Object[] rowValues in values) {
						dataTable.Rows.Add(rowValues);
					}
				}

				return dataTable.CreateDataReader();
			}
		}

	// Method Tests
		[TestMethod()]
		[Description("ReadAll(IDataReader) method for the optimal path.")]
		public void DbDataReaderExtensions_Unit_ReadAll1_Optimal() {
			IDataReader dataReader = DbDataReaderExtensionsTests.CreateDataRecord(new String[] { "FirstName", "LastName" }, new Object[] { "Chad", "Greer" }, new Object[] { "Test", "User" });

			IEnumerable<DataRecord> actual = DbDataReaderExtensions.ReadAll(dataReader);
			Assert.IsNotNull(actual);
			Assert.AreEqual(2, actual.Count());
		}
		[TestMethod()]
		[Description("ReadAll(IDataReader) method when 'dataReader' is a null reference.")]
		[ExpectedException(typeof(ArgumentNullException))]
		public void DbDataReaderExtensions_Unit_ReadAll1_DataReaderIsNull() {
			IDataReader dataReader = null;

			DbDataReaderExtensions.ReadAll(dataReader);
		}
		[TestMethod()]
		[Description("ReadAll(IDataReader) method when 'dataReader' is empty.")]
		public void DbDataReaderExtensions_Unit_ReadAll1_DataReaderIsEmpty() {
			IDataReader dataReader = DbDataReaderExtensionsTests.CreateDataRecord(new String[] { "FirstName", "LastName" });

			IEnumerable<DataRecord> actual = DbDataReaderExtensions.ReadAll(dataReader);
			Assert.IsNotNull(actual);
			Assert.AreEqual(0, actual.Count());
		}

		[TestMethod()]
		[Description("ReadAll<T>(IDataReader, Func<IDataRecord, T>) method for the optimal path.")]
		public void DbDataReaderExtensions_Unit_ReadAll2_Optimal() {
			IDataReader dataReader = DbDataReaderExtensionsTests.CreateDataRecord(new String[] { "FirstName", "LastName" }, new Object[] { "Chad", "Greer" }, new Object[] { "Test", "User" });
			Func<IDataRecord, String> selector = dataRecord => dataRecord[0] as String;

			IEnumerable<String> actual = DbDataReaderExtensions.ReadAll(dataReader, selector);
			Assert.IsNotNull(actual);
			Assert.AreEqual(2, actual.Count());
		}
		[TestMethod()]
		[Description("ReadAll<T>(IDataReader, Func<IDataRecord, T>) method .")]
		[ExpectedException(typeof(ArgumentNullException))]
		public void DbDataReaderExtensions_Unit_ReadAll2_DataReaderIsNull() {
			IDataReader dataReader = null;
			Func<IDataRecord, String> selector = dataRecord => dataRecord[0] as String;

			DbDataReaderExtensions.ReadAll(dataReader, selector);
		}
		[TestMethod()]
		[Description("ReadAll<T>(IDataReader, Func<IDataRecord, T>) method .")]
		[ExpectedException(typeof(ArgumentNullException))]
		public void DbDataReaderExtensions_Unit_ReadAll2_SelectorIsNull() {
			IDataReader dataReader = DbDataReaderExtensionsTests.CreateDataRecord(new String[] { "FirstName", "LastName" }, new Object[] { "Chad", "Greer" }, new Object[] { "Test", "User" });
			Func<IDataRecord, String> selector = null;

			DbDataReaderExtensions.ReadAll(dataReader, selector);
		}
		[TestMethod()]
		[Description("ReadAll<T>(IDataReader, Func<IDataRecord, T>) method .")]
		public void DbDataReaderExtensions_Unit_ReadAll2_DataReaderIsEmpty() {
			IDataReader dataReader = DbDataReaderExtensionsTests.CreateDataRecord(new String[] { "FirstName", "LastName" });
			Func<IDataRecord, String> selector = dataRecord => dataRecord[0] as String;

			IEnumerable<String> actual = DbDataReaderExtensions.ReadAll(dataReader, selector);
			Assert.IsNotNull(actual);
			Assert.AreEqual(0, actual.Count());
		}
	}
}