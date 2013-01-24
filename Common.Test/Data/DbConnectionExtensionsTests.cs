using System;
using System.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Vizistata.TestTools;

namespace Vizistata.Data {
	/// <summary>
	/// This is a test class for <see cref="T:DbConnectionExtensions"/> and is intended to contain all <see cref="T:DbConnectionExtensions"/> Unit Tests.
	///</summary>
	[TestClass()]
	public class DbConnectionExtensionsTests {
		#region Test Class Implementation
		/// <summary>
		/// Describes the context under which the current test is running.
		/// </summary>
		private TestContext _testContextInstance;

		/// <summary>
		/// Initializes a new instance of the <see cref="T:DbConnectionExtensionsTests"/> class.
		/// </summary>
		public DbConnectionExtensionsTests() : base() { }

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
		[Description("CreateCommand(IDbConnection, CommandType, String) method for the optimal path.")]
		public void DbConnectionExtensions_Integration_CreateCommand1_Optimal() {
			using (IDbConnection connection = new MockConnection()) {
				CommandType commandType = CommandType.Text;
				String commandText = "SELECT * FROM MyTable";

				using (IDbCommand actual = DbConnectionExtensions.CreateCommand(connection, commandType, commandText)) {
					Assert.IsNotNull(actual);
				}
			}
		}
		[TestMethod()]
		[Description("CreateCommand(IDbConnection, CommandType, String) method when 'connection' is a null reference.")]
		[ExpectedException(typeof(ArgumentNullException))]
		public void DbConnectionExtensions_Unit_CreateCommand1_ConnectionIsNull() {
			IDbConnection connection = null;
			CommandType commandType = CommandType.Text;
			String commandText = "SELECT * FROM MyTable";

			DbConnectionExtensions.CreateCommand(connection, commandType, commandText);
		}

		[TestMethod()]
		[Description("CreateCommand(IDbCommand, CommandType, String, IDbTransaction) method for the optimal path.")]
		public void DbConnectionExtensions_Integration_CreateCommand2_Optimal() {
			using (IDbConnection connection = new MockConnection()) {
				CommandType commandType = CommandType.Text;
				String commandText = "SELECT * FROM MyTable";
				using (IDbTransaction transaction = connection.BeginTransaction()) {

					using (IDbCommand actual = DbConnectionExtensions.CreateCommand(connection, commandType, commandText, transaction)) {
						Assert.IsNotNull(actual);
					}
				}
			}
		}
		[TestMethod()]
		[Description("CreateCommand(IDbCommand, CommandType, String, IDbTransaction) method when 'connection' is a null reference.")]
		[ExpectedException(typeof(ArgumentNullException))]
		public void DbConnectionExtensions_Unit_CreateCommand2_ConnectionIsNull() {
			IDbConnection connection = null;
			CommandType commandType = CommandType.Text;
			String commandText = "SELECT * FROM MyTable";
			using (IDbTransaction transaction = new MockConnection().BeginTransaction()) {

				DbConnectionExtensions.CreateCommand(connection, commandType, commandText, transaction);
			}
		}
		[TestMethod()]
		[Description("CreateCommand(IDbCommand, CommandType, String, IDbTransaction) method when 'transaction' is a null reference.")]
		public void DbConnectionExtensions_Integration_CreateCommand2_TransactionIsNull() {
			using (IDbConnection connection = new MockConnection()) {
				CommandType commandType = CommandType.Text;
				String commandText = "SELECT * FROM MyTable";
				IDbTransaction transaction = null;

				using (IDbCommand actual = DbConnectionExtensions.CreateCommand(connection, commandType, commandText, transaction)) {
					Assert.IsNotNull(actual);
				}
			}
		}
	}
}