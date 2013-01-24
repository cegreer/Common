using System;
using System.Data;
using System.Diagnostics.CodeAnalysis;

namespace Vizistata.Data {
	/// <summary>
	/// Provides extension methods for the <see cref="T:DbCommand"/> class.  This class may not be inherited.
	/// </summary>
	[SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "Db", Justification = "The correct casing is 'Db'.")]
	public static class DbCommandExtensions {
	// Methods
		/// <summary>
		/// Adds a parameter to a command with the value specified.
		/// </summary>
		/// <param name="command">The command to which to add the parameter.</param>
		/// <param name="name">The name of the parameter.</param>
		/// <param name="type">The type of the parameter.</param>
		/// <param name="value">The value of the parameter.</param>
		/// <returns>The <see cref="T:DbParameter"/> object created.</returns>
		/// <exception cref="System.ArgumentNullException"><paramref name="command"/> is a null reference.</exception>
		/// <exception cref="System.ArgumentOutOfRangeException"><paramref name="type"/> is not defined by <see cref="T:DbType"/>.</exception>
		public static IDbDataParameter AddParameterWithValue(this IDbCommand command, String name, DbType type, Object value) {
			return DbCommandExtensions.AddParameterWithValue(command, name, type, null, value, ParameterDirection.Input);
		}
		/// <summary>
		/// Adds a parameter to a command with the value specified.
		/// </summary>
		/// <param name="command">The command to which to add the parameter.</param>
		/// <param name="name">The name of the parameter.</param>
		/// <param name="type">The type of the parameter.</param>
		/// <param name="size">The size in bytes of the parameter's type.</param>
		/// <param name="value">The value of the parameter.</param>
		/// <returns>The <see cref="T:DbParameter"/> object created.</returns>
		/// <exception cref="System.ArgumentNullException"><paramref name="command"/> is a null reference.</exception>
		/// <exception cref="System.ArgumentOutOfRangeException"><paramref name="type"/> is not defined by <see cref="T:DbType"/>.</exception>
		public static IDbDataParameter AddParameterWithValue(this IDbCommand command, String name, DbType type, Int32 size, Object value) {
			return DbCommandExtensions.AddParameterWithValue(command, name, type, (Int32?)size, value, ParameterDirection.Input);
		}
		/// <summary>
		/// Adds a parameter to a command with the value specified.
		/// </summary>
		/// <param name="command">The command to which to add the parameter.</param>
		/// <param name="name">The name of the parameter.</param>
		/// <param name="type">The type of the parameter.</param>
		/// <param name="value">The value of the parameter.</param>
		/// <param name="direction">The direction of the parameter.</param>
		/// <returns>The <see cref="T:DbParameter"/> object created.</returns>
		/// <exception cref="System.ArgumentNullException"><paramref name="command"/> is a null reference.</exception>
		/// <exception cref="System.ArgumentOutOfRangeException"><paramref name="type"/> is not defined by <see cref="T:DbType"/>.
		/// -or- <paramref name="direction"/> is not defined by <see cref="T:ParameterDirection"/>.</exception>
		public static IDbDataParameter AddParameterWithValue(this IDbCommand command, String name, DbType type, Object value, ParameterDirection direction) {
			return DbCommandExtensions.AddParameterWithValue(command, name, type, null, value, direction);
		}
		/// <summary>
		/// Adds a parameter to a command with the value specified.
		/// </summary>
		/// <param name="command">The command to which to add the parameter.</param>
		/// <param name="name">The name of the parameter.</param>
		/// <param name="type">The type of the parameter.</param>
		/// <param name="size">The size in bytes of the parameter's type.</param>
		/// <param name="value">The value of the parameter.</param>
		/// <param name="direction">The direction of the parameter.</param>
		/// <returns>The <see cref="T:DbParameter"/> object created.</returns>
		/// <exception cref="System.ArgumentNullException"><paramref name="command"/> is a null reference.</exception>
		/// <exception cref="System.ArgumentOutOfRangeException"><paramref name="type"/> is not defined by <see cref="T:DbType"/>.
		/// -or- <paramref name="direction"/> is not defined by <see cref="T:ParameterDirection"/>.</exception>
		public static IDbDataParameter AddParameterWithValue(this IDbCommand command, String name, DbType type, Int32 size, Object value, ParameterDirection direction) {
			return DbCommandExtensions.AddParameterWithValue(command, name, type, (Int32?)size, value, direction);
		}
		/// <summary>
		/// Adds a parameter to a command with the value specified.
		/// </summary>
		/// <param name="command">The command to which to add the parameter.</param>
		/// <param name="name">The name of the parameter.</param>
		/// <param name="type">The type of the parameter.</param>
		/// <param name="size">The size in bytes of the parameter's type, or a null reference if the size is not used in the parameter.</param>
		/// <param name="value">The value of the parameter.</param>
		/// <param name="direction">The direction of the parameter.</param>
		/// <returns>The <see cref="T:DbParameter"/> object created.</returns>
		/// <exception cref="System.ArgumentNullException"><paramref name="command"/> is a null reference.</exception>
		/// <exception cref="System.ArgumentOutOfRangeException"><paramref name="type"/> is not defined by <see cref="T:DbType"/>.
		/// -or- <paramref name="direction"/> is not defined by <see cref="T:ParameterDirection"/>.</exception>
		private static IDbDataParameter AddParameterWithValue(IDbCommand command, String name, DbType type, Int32? size, Object value, ParameterDirection direction) {
			if (command == null) {
				throw new ArgumentNullException("command");
			}
			switch (type) {
				case DbType.AnsiString:
				case DbType.AnsiStringFixedLength:
				case DbType.Binary:
				case DbType.Boolean:
				case DbType.Byte:
				case DbType.Currency:
				case DbType.Date:
				case DbType.DateTime:
				case DbType.DateTime2:
				case DbType.DateTimeOffset:
				case DbType.Decimal:
				case DbType.Double:
				case DbType.Guid:
				case DbType.Int16:
				case DbType.Int32:
				case DbType.Int64:
				case DbType.Object:
				case DbType.SByte:
				case DbType.Single:
				case DbType.String:
				case DbType.StringFixedLength:
				case DbType.Time:
				case DbType.UInt16:
				case DbType.UInt32:
				case DbType.UInt64:
				case DbType.VarNumeric:
				case DbType.Xml:
					break;
				default:
					throw new ArgumentOutOfRangeException("type");
			}
			switch (direction) {
				case ParameterDirection.Input:
				case ParameterDirection.InputOutput:
				case ParameterDirection.Output:
				case ParameterDirection.ReturnValue:
					break;
				default:
					throw new ArgumentOutOfRangeException("direction");
			}

			IDbDataParameter parameter = command.CreateParameter();
			if (name != null) {
				parameter.ParameterName = name;
			}
			parameter.DbType = type;
			if (size != null) {
				parameter.Size = (Int32)size;
			}
			parameter.Value = value ?? DBNull.Value;
			parameter.Direction = direction;
			command.Parameters.Add(parameter);
			return parameter;
		}
	}
}
