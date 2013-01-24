using System;
using System.Data;
using System.Data.Common;
using System.Diagnostics;

namespace Vizistata.TestTools {
	/// <summary>
	/// Represents a mock implementation of the <see cref="T:IDbTransaction"/> interface.  This class may not be inherited.
	/// </summary>
	public sealed class MockTransaction : DbTransaction {
	// Fields
		/// <summary>
		/// The connection for this instance.  This field is read-only.
		/// </summary>
		private readonly MockConnection _connection;
		/// <summary>
		/// <c>true</c> if this instance has been committed; otherwise, <c>false</c>.
		/// </summary>
		private Boolean _isCommitted;
		/// <summary>
		/// The isolation level of the transaction.  This field is read-only.
		/// </summary>
		private readonly IsolationLevel _isolationLevel;
		/// <summary>
		/// <c>true</c> if this instance has been rolled back; otherwise, <c>false</c>.
		/// </summary>
		private Boolean _isRolledBack;

	// Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:MockTransaction"/> class.
		/// </summary>
		/// <param name="connection">The connection for this instance.</param>
		/// <param name="isolationLevel">The isolation level of the transaction.</param>
		internal MockTransaction(MockConnection connection, IsolationLevel isolationLevel)
			: base() {
			Debug.Assert(connection != null);

			this._connection = connection;
			this._isolationLevel = isolationLevel;
		}

	// Properties
		/// <summary>
		/// Gets the connection that created this instance.
		/// </summary>
		protected override DbConnection DbConnection {
			get { return this._connection; }
		}
		/// <summary>
		/// Gets the isolation level of the transaction.
		/// </summary>
		public override IsolationLevel IsolationLevel {
			get { return this._isolationLevel; }
		}

	// Methods
		/// <summary>
		/// Commits any changes within this instance's scope.
		/// </summary>
		/// <exception cref="System.InvalidOperationException">This instance has already been committed or rolled back.</exception>
		public override void Commit() {
			if (this._isCommitted || this._isRolledBack) {
				throw new InvalidOperationException();
			}
			this._isCommitted = true;
		}
		/// <summary>
		/// Reverts any changes within this instance's scope.
		/// </summary>
		public override void Rollback() {
			if (!this._isCommitted && !this._isRolledBack) {
				this._isRolledBack = true;
			}
		}
	}
}
