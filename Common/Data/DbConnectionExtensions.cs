using System;
using System.Data;
using System.Data.Common;
using System.Diagnostics.CodeAnalysis;

namespace Vizistata.Data {
	/// <summary>
	/// Provides extension methods for the <see cref="T:IDbConnection"/> interface and <see cref="T:DbConnection"/> class.  This class may not be inherited.
	/// </summary>
	[SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "Db", Justification = "The term 'Db' is cased properly.")]
	public static class DbConnectionExtensions {
	// Methods
		/// <summary>
		/// Creates and returns a new database command object.
		/// </summary>
		/// <param name="connection">The connection that will create and own the command.</param>
		/// <param name="commandType">The type of command.</param>
		/// <param name="commandText">The text or name of the stored procedure for the command.</param>
		/// <returns>A reference to the <see cref="T:IDbCommand"/> object created.</returns>
		/// <exception cref="System.ArgumentNullException"><paramref name="connection"/> is a null reference.</exception>
		public static IDbCommand CreateCommand(this IDbConnection connection, CommandType commandType, String commandText) {
			return connection.CreateCommand(commandType, commandText, (IDbTransaction)null);
		}
		/// <summary>
		/// Creates and returns a new database command object.
		/// </summary>
		/// <param name="connection">The connection that will create and own the command.</param>
		/// <param name="commandType">The type of command.</param>
		/// <param name="commandText">The text or name of the stored procedure for the command.</param>
		/// <param name="transaction">The database transaction that has been enlisted, or a null reference if the command is not part of a transaction.</param>
		/// <returns>A reference to the <see cref="T:IDbCommand"/> object created.</returns>
		/// <exception cref="System.ArgumentNullException"><paramref name="connection"/> is a null reference.</exception>
		[SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities", Justification = "This is a pass-through method.")]
		public static IDbCommand CreateCommand(this IDbConnection connection, CommandType commandType, String commandText, IDbTransaction transaction) {
			if (connection == null) {
				throw new ArgumentNullException("connection");
			}

			IDbCommand command = connection.CreateCommand();
			command.CommandType = commandType;
			command.CommandText = commandText;
			if (transaction != null) {
				command.Transaction = transaction;
			}
			return command;
		}
	}
}
