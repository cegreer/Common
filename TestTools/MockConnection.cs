using System;
using System.Data;
using System.Data.Common;

namespace Vizistata.TestTools {
	/// <summary>
	/// Represents a mock object for the <see cref="T:IDbConnection"/> interface.  This class may not be inherited.
	/// </summary>
	public sealed class MockConnection : DbConnection {
	// Fields
		/// <summary>
		/// Builds the connection string in this instance.  This field is read-only.
		/// </summary>
		private readonly DbConnectionStringBuilder _connectionStringBuilder;
		/// <summary>
		/// The state of the connection.
		/// </summary>
		private ConnectionState _state = ConnectionState.Closed;

	// Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:MockConnection"/> class.
		/// </summary>
		public MockConnection() : this(null) { }
		/// <summary>
		/// Initializes a new instance of the <see cref="T:MockConnection"/> class.
		/// </summary>
		/// <param name="connectionString">The connection string to use.</param>
		public MockConnection(String connectionString) : this(connectionString, false) { }
		/// <summary>
		/// Initializes a new instance of the <see cref="T:MockConnection"/> class.
		/// </summary>
		/// <param name="useOdbcRules"><c>true</c> to use {} to delimit fields; <c>false</c> to use quotation marks.</param>
		public MockConnection(Boolean useOdbcRules) : this(null, useOdbcRules) { }
		/// <summary>
		/// Initializes a new instance of the <see cref="T:MockConnection"/> class.
		/// </summary>
		/// <param name="connectionString">The connection string to use.</param>
		/// <param name="useOdbcRules"><c>true</c> to use {} to delimit fields; <c>false</c> to use quotation marks.</param>
		public MockConnection(String connectionString, Boolean useOdbcRules)
			: base() {
			DbConnectionStringBuilder connectionStringBuilder = new DbConnectionStringBuilder(useOdbcRules);
			if (connectionString != null) {
				connectionStringBuilder.ConnectionString = connectionString;
			}
			this._connectionStringBuilder = connectionStringBuilder;
		}

	// Properties
		/// <summary>
		/// Gets or sets the connection string.
		/// </summary>
		/// <exception cref="System.ArgumentException">An invalid connection string argument has been supplied.</exception>
		public override String ConnectionString {
			get { return this._connectionStringBuilder.ToString(); }
			set { this._connectionStringBuilder.ConnectionString = value; }
		}
		/// <summary>
		/// Gets the server version for the connection.
		/// </summary>
		public override String ServerVersion {
			get { return "1.0"; }
		}
		/// <summary>
		/// Gets the state of the connection.
		/// </summary>
		public override ConnectionState State {
			get { return this._state; }
		}
		/// <summary>
		/// Gets the data source (or server) to which this instance is connected.
		/// </summary>
		public override String DataSource {
			get {
				if (this._connectionStringBuilder.ContainsKey("Data Source")) {
					return (String)this._connectionStringBuilder["Data Source"];
				}
				else if (this._connectionStringBuilder.ContainsKey("Server")) {
					return (String)this._connectionStringBuilder["Server"];
				}
				return null;
			}
		}
		/// <summary>
		/// Gets the database to which this instance is connected.
		/// </summary>
		public override String Database {
			get {
				if (this._connectionStringBuilder.ContainsKey("Database")) {
					return (String)this._connectionStringBuilder["Database"];
				}
				else if (this._connectionStringBuilder.ContainsKey("Initial Catalog")) {
					return (String)this._connectionStringBuilder["Initial Catalog"];
				}
				return null;
			}
		}

	// Methods
		/// <summary>
		/// Begins a database transaction with the specified isolation level.
		/// </summary>
		/// <param name="isolationLevel">The isolation level for the transaction.</param>
		/// <returns>The database transaction object created.</returns>
		protected override DbTransaction BeginDbTransaction(IsolationLevel isolationLevel) {
			return new MockTransaction(this, isolationLevel);
		}
		/// <summary>
		/// Changes the database to which this instance is connected.
		/// </summary>
		/// <param name="databaseName">The name of the database.</param>
		public override void ChangeDatabase(String databaseName) {
			if (databaseName != this.Database) {
				if (!this._connectionStringBuilder.ContainsKey("Database")) {
					this._connectionStringBuilder["Initial Catalog"] = databaseName;
				}
				else {
					this._connectionStringBuilder["Database"] = databaseName;
				}
			}
		}
		/// <summary>
		/// Closes the connection if it is open.
		/// </summary>
		public override void Close() {
			if (this._state != ConnectionState.Closed) {
				this._state = ConnectionState.Closed;
			}
		}
		/// <summary>
		/// Creates a database command object for this instnace.
		/// </summary>
		/// <returns>The database command object created.</returns>
		protected override DbCommand CreateDbCommand() {
			return new MockCommand(this);
		}
		/// <summary>
		/// Opens the connection if it is closed.
		/// </summary>
		/// <exception cref="System.InvalidOperationException">This instance is not closed.</exception>
		public override void Open() {
			if (this._state != ConnectionState.Closed) {
				throw new InvalidOperationException();
			}
			this._state = ConnectionState.Open;
		}
	}
}
