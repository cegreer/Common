using System;
using System.Data;
using System.Data.Common;

namespace Vizistata.TestTools {
	/// <summary>
	/// Represents a mock implementation of the <see cref="T:IDbCommand"/> interface.  This class may not be inherited.
	/// </summary>
	public sealed class MockCommand : DbCommand {
	// Fields
		/// <summary>
		/// The connection for this instance.
		/// </summary>
		private MockConnection _connection;
		/// <summary>
		/// The transaction for this instance.
		/// </summary>
		private MockTransaction _transaction;

	// Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:MockCommand"/> class.
		/// </summary>
		public MockCommand() : this(null) { }
		/// <summary>
		/// Initializes a new instance of the <see cref="T:MockCommand"/> class.
		/// </summary>
		/// <param name="connection">The connection for this instance.</param>
		public MockCommand(MockConnection connection)
			: base() {
			this._connection = connection;
		}

	// Properties
		/// <summary>
		/// Gets or sets the command text.
		/// </summary>
		public override String CommandText {
			get;
			set;
		}
		/// <summary>
		/// Gets or sets the command timeout in seconds.
		/// </summary>
		public override Int32 CommandTimeout {
			get;
			set;
		}
		/// <summary>
		/// Gets or sets the type of command.
		/// </summary>
		public override CommandType CommandType {
			get;
			set;
		}
		/// <summary>
		/// Gets or sets the database connection.
		/// </summary>
		/// <exception cref="System.InvalidCastException"><paramref name="value"/> is not of type <see cref="T:MockConnection"/>.</exception>
		protected override DbConnection DbConnection {
			get { return this._connection; }
			set {
				MockConnection connection = (MockConnection)value;
				this._connection = connection;
			}
		}
		/// <summary>
		/// Gets the collection of database parameters.
		/// </summary>
		/// <exception cref="System.NotSupportedException">Always thrown.</exception>
		protected override DbParameterCollection DbParameterCollection {
			get { throw new NotSupportedException(); }
		}
		/// <summary>
		/// Gets or sets the database transaction.
		/// </summary>
		/// <exception cref="System.InvalidCastException"><paramref name="value"/> is not of type <see cref="T:MockTransaction"/>.</exception>
		protected override DbTransaction DbTransaction {
			get { return this._transaction; }
			set {
				MockTransaction transaction = (MockTransaction)value;
				this._transaction = transaction;
			}
		}
		/// <summary>
		/// Gets or sets a value indicating whether the component is visible at design time.
		/// </summary>
		public override Boolean DesignTimeVisible {
			get;
			set;
		}
		/// <summary>
		/// Gets or sets the value that indicates how query command results are applied to the row being updated.
		/// </summary>
		public override UpdateRowSource UpdatedRowSource {
			get;
			set;
		}

	// Methods
		/// <summary>
		/// Cancels the current execution.
		/// </summary>
		/// <exception cref="System.NotSupportedException">Always thrown.</exception>
		public override void Cancel() {
			throw new NotSupportedException();
		}
		/// <summary>
		/// Creates a database parameter object.
		/// </summary>
		/// <returns>The database parameter object created.</returns>
		/// <exception cref="System.NotSupportedException">Always thrown.</exception>
		protected override DbParameter CreateDbParameter() {
			throw new NotSupportedException();
		}
		/// <summary>
		/// Executes this instance and returns an object that can read the database results.
		/// </summary>
		/// <param name="behavior">The behavior of the command.</param>
		/// <returns>The object that can read the results.</returns>
		/// <exception cref="System.NotSupportedException">Always thrown.</exception>
		protected override DbDataReader ExecuteDbDataReader(CommandBehavior behavior) {
			throw new NotSupportedException();
		}
		/// <summary>
		/// Executes this instance as a non-query and returns the number of rows affected.
		/// </summary>
		/// <returns>The number of rows affected.</returns>
		/// <exception cref="System.NotSupportedException">Always thrown.</exception>
		public override Int32 ExecuteNonQuery() {
			throw new NotSupportedException();
		}
		/// <summary>
		/// Executes this instance and returns the first value of the first row and column from the results.
		/// </summary>
		/// <returns>The value of the first row and column from the results.</returns>
		/// <exception cref="System.NotSupportedException">Always thrown.</exception>
		public override Object ExecuteScalar() {
			throw new NotSupportedException();
		}
		/// <summary>
		/// Prepares this instance for execution.
		/// </summary>
		/// <exception cref="System.NotSupportedException">Always thrown.</exception>
		public override void Prepare() {
			throw new NotSupportedException();
		}
	}
}
