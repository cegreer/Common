using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Vizistata.Data {
	/// <summary>
	/// This is a test class for <see cref="T:DbCommandExtensions"/> and is intended to contain all <see cref="T:DbCommandExtensions"/> Unit Tests.
	///</summary>
	[TestClass()]
	public class DbCommandExtensionsTests {
		#region Test Class Implementation
		/// <summary>
		/// Describes the context under which the current test is running.
		/// </summary>
		private TestContext _testContextInstance;

		/// <summary>
		/// Initializes a new instance of the <see cref="T:DbCommandExtensionsTests"/> class.
		/// </summary>
		public DbCommandExtensionsTests() : base() { }

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
		/// Returns the type values for tests.
		/// </summary>
		/// <returns>The enumerable collection of types that can be used.</returns>
		private static IEnumerable<DbType> GetDbTypes() {
			foreach (DbType type in Enum.GetValues(typeof(DbType))) {
				if (type != DbType.SByte
					&& type != DbType.UInt16
					&& type != DbType.UInt32
					&& type != DbType.UInt64
					&& type != DbType.VarNumeric) {
					yield return type;
				}
			}
			yield break;
		}

	// Method Tests
		[TestMethod()]
		[Description("AddParameterWithValue(IDbCommand, String, DbType, Object) method for the optimal path.")]
		public void DbCommandExtensions_Unit_AddParameterWithValue1_Optimal() {
			using (IDbCommand command = new SqlCommand()) {
				String name = "@Name";
				DbType type = DbType.String;
				Object value = "Value";

				IDbDataParameter returnValue = command.AddParameterWithValue(name, type, value);
				Assert.IsNotNull(returnValue);
				Assert.AreEqual(1, command.Parameters.Count);
				Assert.AreEqual(returnValue, command.Parameters[0]);
				Assert.AreEqual(name, returnValue.ParameterName);
				Assert.AreEqual(type, returnValue.DbType);
				Assert.AreEqual(value, returnValue.Value);
				Assert.AreEqual(ParameterDirection.Input, returnValue.Direction);
			}
		}
		[TestMethod()]
		[Description("AddParameterWithValue(IDbCommand, String, DbType, Object) method when 'command' is a null reference.")]
		[ExpectedException(typeof(ArgumentNullException))]
		public void DbCommandExtensions_Unit_AddParameterWithValue1_CommandNull() {
			IDbCommand command = null;
			String name = "@Name";
			DbType type = DbType.String;
			Object value = "Value";

			command.AddParameterWithValue(name, type, value);
		}
		[TestMethod()]
		[Description("AddParameterWithValue(IDbCommand, String, DbType, Object) method when 'name' is a null reference.")]
		public void DbCommandExtensions_Unit_AddParameterWithValue1_NameNull() {
			using (IDbCommand command = new SqlCommand()) {
				String name = null;
				DbType type = DbType.String;
				Object value = "Value";

				IDbDataParameter returnValue = command.AddParameterWithValue(name, type, value);
				Assert.IsNotNull(returnValue);
				Assert.AreEqual(1, command.Parameters.Count);
				Assert.AreEqual(returnValue, command.Parameters[0]);
				Assert.AreEqual(type, returnValue.DbType);
				Assert.AreEqual(value, returnValue.Value);
				Assert.AreEqual(ParameterDirection.Input, returnValue.Direction);
			}
		}
		[TestMethod()]
		[Description("AddParameterWithValue(IDbCommand, String, DbType, Object) method for all of the valid DbType values.")]
		public void DbCommandExtensions_Unit_AddParameterWithValue1_AllValidDbTypes() {
			String name = "@Name";
			Object value = "Value";
			foreach (DbType type in DbCommandExtensionsTests.GetDbTypes()) {
				using (IDbCommand command = new SqlCommand()) {
					IDbDataParameter returnValue = command.AddParameterWithValue(name, type, value);
					Assert.IsNotNull(returnValue);
					Assert.AreEqual(1, command.Parameters.Count);
					Assert.AreEqual(returnValue, command.Parameters[0]);
					Assert.AreEqual(name, returnValue.ParameterName);
					Assert.AreEqual(value, returnValue.Value);
					Assert.AreEqual(ParameterDirection.Input, returnValue.Direction);
				}
			}
		}
		[TestMethod()]
		[Description("AddParameterWithValue(IDbCommand, String, DbType, Object) method for the optimal path.")]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void DbCommandExtensions_Unit_AddParameterWithValue1_InvalidDbType() {
			using (IDbCommand command = new SqlCommand()) {
				String name = "@Name";
				DbType type = (DbType)(-1);
				Object value = "Value";

				command.AddParameterWithValue(name, type, value);
			}
		}
		[TestMethod()]
		[Description("AddParameterWithValue(IDbCommand, String, DbType, Object) method for the optimal path.")]
		public void DbCommandExtensions_Unit_AddParameterWithValue1_ValueNull() {
			using (IDbCommand command = new SqlCommand()) {
				String name = "@Name";
				DbType type = DbType.String;
				Object value = null;

				IDbDataParameter returnValue = command.AddParameterWithValue(name, type, value);
				Assert.IsNotNull(returnValue);
				Assert.AreEqual(1, command.Parameters.Count);
				Assert.AreEqual(returnValue, command.Parameters[0]);
				Assert.AreEqual(name, returnValue.ParameterName);
				Assert.AreEqual(type, returnValue.DbType);
				Assert.AreEqual(DBNull.Value, returnValue.Value);
				Assert.AreEqual(ParameterDirection.Input, returnValue.Direction);
			}
		}

		[TestMethod()]
		[Description("AddParameterWithValue(IDbCommand, String, DbType, Int32, Object) method for the optimal path.")]
		public void DbCommandExtensions_Unit_AddParameterWithValue2_Optimal() {
			using (IDbCommand command = new SqlCommand()) {
				String name = "@Name";
				DbType type = DbType.String;
				Int32 size = 100;
				Object value = "Value";

				IDbDataParameter returnValue = command.AddParameterWithValue(name, type, size, value);
				Assert.IsNotNull(returnValue);
				Assert.AreEqual(1, command.Parameters.Count);
				Assert.AreEqual(returnValue, command.Parameters[0]);
				Assert.AreEqual(name, returnValue.ParameterName);
				Assert.AreEqual(type, returnValue.DbType);
				Assert.AreEqual(value, returnValue.Value);
				Assert.AreEqual(ParameterDirection.Input, returnValue.Direction);
			}
		}
		[TestMethod()]
		[Description("AddParameterWithValue(IDbCommand, String, DbType, Int32, Object) method when 'command' is a null reference.")]
		[ExpectedException(typeof(ArgumentNullException))]
		public void DbCommandExtensions_Unit_AddParameterWithValue2_CommandNull() {
			IDbCommand command = null;
			String name = "@Name";
			DbType type = DbType.String;
			Int32 size = 100;
			Object value = "Value";

			command.AddParameterWithValue(name, type, size, value);
		}
		[TestMethod()]
		[Description("AddParameterWithValue(IDbCommand, String, DbType, Int32, Object) method when 'name' is a null reference.")]
		public void DbCommandExtensions_Unit_AddParameterWithValue2_NameNull() {
			using (IDbCommand command = new SqlCommand()) {
				String name = null;
				DbType type = DbType.String;
				Int32 size = 100;
				Object value = "Value";

				IDbDataParameter returnValue = command.AddParameterWithValue(name, type, size, value);
				Assert.IsNotNull(returnValue);
				Assert.AreEqual(1, command.Parameters.Count);
				Assert.AreEqual(returnValue, command.Parameters[0]);
				Assert.AreEqual(type, returnValue.DbType);
				Assert.AreEqual(value, returnValue.Value);
				Assert.AreEqual(ParameterDirection.Input, returnValue.Direction);
			}
		}
		[TestMethod()]
		[Description("AddParameterWithValue(IDbCommand, String, DbType, Int32, Object) method for all of the valid DbType values.")]
		public void DbCommandExtensions_Unit_AddParameterWithValue2_AllValidDbTypes() {
			String name = "@Name";
			Int32 size = 100;
			Object value = "Value";
			foreach (DbType type in DbCommandExtensionsTests.GetDbTypes()) {
				using (IDbCommand command = new SqlCommand()) {
					IDbDataParameter returnValue = command.AddParameterWithValue(name, type, size, value);
					Assert.IsNotNull(returnValue);
					Assert.AreEqual(1, command.Parameters.Count);
					Assert.AreEqual(returnValue, command.Parameters[0]);
					Assert.AreEqual(name, returnValue.ParameterName);
					Assert.AreEqual(value, returnValue.Value);
					Assert.AreEqual(ParameterDirection.Input, returnValue.Direction);
				}
			}
		}
		[TestMethod()]
		[Description("AddParameterWithValue(IDbCommand, String, DbType, Int32, Object) method for the optimal path.")]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void DbCommandExtensions_Unit_AddParameterWithValue2_InvalidDbType() {
			using (IDbCommand command = new SqlCommand()) {
				String name = "@Name";
				DbType type = (DbType)(-1);
				Int32 size = 100;
				Object value = "Value";

				command.AddParameterWithValue(name, type, size, value);
			}
		}
		[TestMethod()]
		[Description("AddParameterWithValue(IDbCommand, String, DbType, Int32, Object) method for the optimal path.")]
		public void DbCommandExtensions_Unit_AddParameterWithValue2_ValueNull() {
			using (IDbCommand command = new SqlCommand()) {
				String name = "@Name";
				DbType type = DbType.String;
				Int32 size = 100;
				Object value = null;

				IDbDataParameter returnValue = command.AddParameterWithValue(name, type, size, value);
				Assert.IsNotNull(returnValue);
				Assert.AreEqual(1, command.Parameters.Count);
				Assert.AreEqual(returnValue, command.Parameters[0]);
				Assert.AreEqual(name, returnValue.ParameterName);
				Assert.AreEqual(type, returnValue.DbType);
				Assert.AreEqual(DBNull.Value, returnValue.Value);
				Assert.AreEqual(ParameterDirection.Input, returnValue.Direction);
			}
		}

		[TestMethod()]
		[Description("AddParameterWithValue(IDbCommand, String, DbType, Object, ParameterDirection) method for the optimal path.")]
		public void DbCommandExtensions_Unit_AddParameterWithValue3_Optimal() {
			using (IDbCommand command = new SqlCommand()) {
				String name = "@Name";
				DbType type = DbType.String;
				Object value = "Value";
				ParameterDirection direction = ParameterDirection.InputOutput;

				IDbDataParameter returnValue = command.AddParameterWithValue(name, type, value, direction);
				Assert.IsNotNull(returnValue);
				Assert.AreEqual(1, command.Parameters.Count);
				Assert.AreEqual(returnValue, command.Parameters[0]);
				Assert.AreEqual(name, returnValue.ParameterName);
				Assert.AreEqual(type, returnValue.DbType);
				Assert.AreEqual(value, returnValue.Value);
				Assert.AreEqual(direction, returnValue.Direction);
			}
		}
		[TestMethod()]
		[Description("AddParameterWithValue(IDbCommand, String, DbType, Object, ParameterDirection) method when 'command' is a null reference.")]
		[ExpectedException(typeof(ArgumentNullException))]
		public void DbCommandExtensions_Unit_AddParameterWithValue3_CommandNull() {
			IDbCommand command = null;
			String name = "@Name";
			DbType type = DbType.String;
			Object value = "Value";
			ParameterDirection direction = ParameterDirection.InputOutput;

			command.AddParameterWithValue(name, type, value, direction);
		}
		[TestMethod()]
		[Description("AddParameterWithValue(IDbCommand, String, DbType, Object, ParameterDirection) method when 'name' is a null reference.")]
		public void DbCommandExtensions_Unit_AddParameterWithValue3_NameNull() {
			using (IDbCommand command = new SqlCommand()) {
				String name = null;
				DbType type = DbType.String;
				Object value = "Value";
				ParameterDirection direction = ParameterDirection.InputOutput;

				IDbDataParameter returnValue = command.AddParameterWithValue(name, type, value, direction);
				Assert.IsNotNull(returnValue);
				Assert.AreEqual(1, command.Parameters.Count);
				Assert.AreEqual(returnValue, command.Parameters[0]);
				Assert.AreEqual(type, returnValue.DbType);
				Assert.AreEqual(value, returnValue.Value);
				Assert.AreEqual(direction, returnValue.Direction);
			}
		}
		[TestMethod()]
		[Description("AddParameterWithValue(IDbCommand, String, DbType, Object, ParameterDirection) method for all of the valid DbType values.")]
		public void DbCommandExtensions_Unit_AddParameterWithValue3_AllValidDbTypes() {
			String name = "@Name";
			Object value = "Value";
			ParameterDirection direction = ParameterDirection.InputOutput;
			foreach (DbType type in DbCommandExtensionsTests.GetDbTypes()) {
				using (IDbCommand command = new SqlCommand()) {
					IDbDataParameter returnValue = command.AddParameterWithValue(name, type, value, direction);
					Assert.IsNotNull(returnValue);
					Assert.AreEqual(1, command.Parameters.Count);
					Assert.AreEqual(returnValue, command.Parameters[0]);
					Assert.AreEqual(name, returnValue.ParameterName);
					Assert.AreEqual(value, returnValue.Value);
					Assert.AreEqual(direction, returnValue.Direction);
				}
			}
		}
		[TestMethod()]
		[Description("AddParameterWithValue(IDbCommand, String, DbType, Object, ParameterDirection) method for the optimal path.")]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void DbCommandExtensions_Unit_AddParameterWithValue3_InvalidDbType() {
			using (IDbCommand command = new SqlCommand()) {
				String name = "@Name";
				DbType type = (DbType)(-1);
				Object value = "Value";
				ParameterDirection direction = ParameterDirection.InputOutput;

				command.AddParameterWithValue(name, type, value, direction);
			}
		}
		[TestMethod()]
		[Description("AddParameterWithValue(IDbCommand, String, DbType, Object, ParameterDirection) method for the optimal path.")]
		public void DbCommandExtensions_Unit_AddParameterWithValue3_ValueNull() {
			using (IDbCommand command = new SqlCommand()) {
				String name = "@Name";
				DbType type = DbType.String;
				Object value = null;
				ParameterDirection direction = ParameterDirection.InputOutput;

				IDbDataParameter returnValue = command.AddParameterWithValue(name, type, value, direction);
				Assert.IsNotNull(returnValue);
				Assert.AreEqual(1, command.Parameters.Count);
				Assert.AreEqual(returnValue, command.Parameters[0]);
				Assert.AreEqual(name, returnValue.ParameterName);
				Assert.AreEqual(type, returnValue.DbType);
				Assert.AreEqual(DBNull.Value, returnValue.Value);
				Assert.AreEqual(direction, returnValue.Direction);
			}
		}
		[TestMethod()]
		[Description("AddParameterWithValue(IDbCommand, String, DbType, Object, ParameterDirection) method for all of the valid DbType values.")]
		public void DbCommandExtensions_Unit_AddParameterWithValue3_AllValidParameterDirections() {
			String name = "@Name";
			DbType type = DbType.String;
			Object value = "Value";
			foreach (ParameterDirection direction in Enum.GetValues(typeof(ParameterDirection))) {
				using (IDbCommand command = new SqlCommand()) {
					IDbDataParameter returnValue = command.AddParameterWithValue(name, type, value, direction);
					Assert.IsNotNull(returnValue);
					Assert.AreEqual(1, command.Parameters.Count);
					Assert.AreEqual(returnValue, command.Parameters[0]);
					Assert.AreEqual(name, returnValue.ParameterName);
					Assert.AreEqual(type, returnValue.DbType);
					Assert.AreEqual(value, returnValue.Value);
					Assert.AreEqual(direction, returnValue.Direction);
				}
			}
		}
		[TestMethod()]
		[Description("AddParameterWithValue(IDbCommand, String, DbType, Object, ParameterDirection) method for the optimal path.")]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void DbCommandExtensions_Unit_AddParameterWithValue3_InvalidParameterDirection() {
			using (IDbCommand command = new SqlCommand()) {
				String name = "@Name";
				DbType type = DbType.String;
				Object value = "Value";
				ParameterDirection direction = (ParameterDirection)0;

				command.AddParameterWithValue(name, type, value, direction);
			}
		}

		[TestMethod()]
		[Description("AddParameterWithValue(IDbCommand, String, DbType, Int32, Object, ParameterDirection) method for the optimal path.")]
		public void DbCommandExtensions_Unit_AddParameterWithValue4_Optimal() {
			using (IDbCommand command = new SqlCommand()) {
				String name = "@Name";
				DbType type = DbType.String;
				Int32 size = 100;
				Object value = "Value";
				ParameterDirection direction = ParameterDirection.InputOutput;

				IDbDataParameter returnValue = command.AddParameterWithValue(name, type, size, value, direction);
				Assert.IsNotNull(returnValue);
				Assert.AreEqual(1, command.Parameters.Count);
				Assert.AreEqual(returnValue, command.Parameters[0]);
				Assert.AreEqual(name, returnValue.ParameterName);
				Assert.AreEqual(type, returnValue.DbType);
				Assert.AreEqual(value, returnValue.Value);
				Assert.AreEqual(direction, returnValue.Direction);
			}
		}
		[TestMethod()]
		[Description("AddParameterWithValue(IDbCommand, String, DbType, Int32, Object, ParameterDirection) method when 'command' is a null reference.")]
		[ExpectedException(typeof(ArgumentNullException))]
		public void DbCommandExtensions_Unit_AddParameterWithValue4_CommandNull() {
			IDbCommand command = null;
			String name = "@Name";
			DbType type = DbType.String;
			Int32 size = 100;
			Object value = "Value";
			ParameterDirection direction = ParameterDirection.InputOutput;

			command.AddParameterWithValue(name, type, size, value, direction);
		}
		[TestMethod()]
		[Description("AddParameterWithValue(IDbCommand, String, DbType, Int32, Object, ParameterDirection) method when 'name' is a null reference.")]
		public void DbCommandExtensions_Unit_AddParameterWithValue4_NameNull() {
			using (IDbCommand command = new SqlCommand()) {
				String name = null;
				DbType type = DbType.String;
				Int32 size = 100;
				Object value = "Value";
				ParameterDirection direction = ParameterDirection.InputOutput;

				IDbDataParameter returnValue = command.AddParameterWithValue(name, type, size, value, direction);
				Assert.IsNotNull(returnValue);
				Assert.AreEqual(1, command.Parameters.Count);
				Assert.AreEqual(returnValue, command.Parameters[0]);
				Assert.AreEqual(type, returnValue.DbType);
				Assert.AreEqual(value, returnValue.Value);
				Assert.AreEqual(direction, returnValue.Direction);
			}
		}
		[TestMethod()]
		[Description("AddParameterWithValue(IDbCommand, String, DbType, Int32, Object, ParameterDirection) method for all of the valid DbType values.")]
		public void DbCommandExtensions_Unit_AddParameterWithValue4_AllValidDbTypes() {
			String name = "@Name";
			Int32 size = 100;
			Object value = "Value";
			ParameterDirection direction = ParameterDirection.InputOutput;
			foreach (DbType type in DbCommandExtensionsTests.GetDbTypes()) {
				using (IDbCommand command = new SqlCommand()) {
					IDbDataParameter returnValue = command.AddParameterWithValue(name, type, size, value, direction);
					Assert.IsNotNull(returnValue);
					Assert.AreEqual(1, command.Parameters.Count);
					Assert.AreEqual(returnValue, command.Parameters[0]);
					Assert.AreEqual(name, returnValue.ParameterName);
					Assert.AreEqual(value, returnValue.Value);
					Assert.AreEqual(direction, returnValue.Direction);
				}
			}
		}
		[TestMethod()]
		[Description("AddParameterWithValue(IDbCommand, String, DbType, Int32, Object, ParameterDirection) method for the optimal path.")]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void DbCommandExtensions_Unit_AddParameterWithValue4_InvalidDbType() {
			using (IDbCommand command = new SqlCommand()) {
				String name = "@Name";
				DbType type = (DbType)(-1);
				Int32 size = 100;
				Object value = "Value";
				ParameterDirection direction = ParameterDirection.InputOutput;

				command.AddParameterWithValue(name, type, size, value, direction);
			}
		}
		[TestMethod()]
		[Description("AddParameterWithValue(IDbCommand, String, DbType, Int32, Object, ParameterDirection) method for the optimal path.")]
		public void DbCommandExtensions_Unit_AddParameterWithValue4_ValueNull() {
			using (IDbCommand command = new SqlCommand()) {
				String name = "@Name";
				DbType type = DbType.String;
				Int32 size = 100;
				Object value = null;
				ParameterDirection direction = ParameterDirection.InputOutput;

				IDbDataParameter returnValue = command.AddParameterWithValue(name, type, size, value, direction);
				Assert.IsNotNull(returnValue);
				Assert.AreEqual(1, command.Parameters.Count);
				Assert.AreEqual(returnValue, command.Parameters[0]);
				Assert.AreEqual(name, returnValue.ParameterName);
				Assert.AreEqual(type, returnValue.DbType);
				Assert.AreEqual(DBNull.Value, returnValue.Value);
				Assert.AreEqual(direction, returnValue.Direction);
			}
		}
		[TestMethod()]
		[Description("AddParameterWithValue(IDbCommand, String, DbType, Int32, Object, ParameterDirection) method for all of the valid DbType values.")]
		public void DbCommandExtensions_Unit_AddParameterWithValue4_AllValidParameterDirections() {
			String name = "@Name";
			DbType type = DbType.String;
			Int32 size = 100;
			Object value = "Value";
			foreach (ParameterDirection direction in Enum.GetValues(typeof(ParameterDirection))) {
				using (IDbCommand command = new SqlCommand()) {
					IDbDataParameter returnValue = command.AddParameterWithValue(name, type, size, value, direction);
					Assert.IsNotNull(returnValue);
					Assert.AreEqual(1, command.Parameters.Count);
					Assert.AreEqual(returnValue, command.Parameters[0]);
					Assert.AreEqual(name, returnValue.ParameterName);
					Assert.AreEqual(type, returnValue.DbType);
					Assert.AreEqual(value, returnValue.Value);
					Assert.AreEqual(direction, returnValue.Direction);
				}
			}
		}
		[TestMethod()]
		[Description("AddParameterWithValue(IDbCommand, String, DbType, Int32, Object, ParameterDirection) method for the optimal path.")]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void DbCommandExtensions_Unit_AddParameterWithValue4_InvalidParameterDirection() {
			using (IDbCommand command = new SqlCommand()) {
				String name = "@Name";
				DbType type = DbType.String;
				Int32 size = 100;
				Object value = "Value";
				ParameterDirection direction = (ParameterDirection)0;

				command.AddParameterWithValue(name, type, size, value, direction);
			}
		}
	}
}