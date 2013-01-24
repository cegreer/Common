using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Vizistata.Data {
	/// <summary>
	/// Provides extension methods for the <see cref="T:IDataReader"/> interface and <see cref="T:DbDataReader"/> class.  This class may not be inherited.
	/// </summary>
	[SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "Db", Justification = "The term 'Db' is cased properly.")]
	public static class DbDataReaderExtensions {
	// Methods
		/// <summary>
		/// Reads all records in the data reader and returns a collection of objects from the records.
		/// </summary>
		/// <param name="dataReader">The data reader whose records will be read.</param>
		/// <returns>The enumerable collection of objects representing the records.</returns>
		/// <exception cref="System.ArgumentNullException"><paramref name="dataReader"/> is a null reference.</exception>
		public static IEnumerable<DataRecord> ReadAll(this IDataReader dataReader) {
			if (dataReader == null) {
				throw new ArgumentNullException("dataReader");
			}

			return DbDataReaderExtensions.ReadAllCore(dataReader);
		}
		/// <summary>
		/// Reads all records in the data reader and returns a collection of objects from the records.
		/// </summary>
		/// <typeparam name="T">The type of object returned.</typeparam>
		/// <param name="dataReader">The data reader whose records will be read.</param>
		/// <param name="selector">The function that converts an <see cref="T:IDataRecord"/> into an object of type <typeparamref name="T"/>.</param>
		/// <returns>The enumerable collection of objects representing the records.</returns>
		/// <exception cref="System.ArgumentNullException"><paramref name="dataReader"/> is a null reference.
		/// -or- <paramref name="selector"/> is a null reference.</exception>
		public static IEnumerable<T> ReadAll<T>(this IDataReader dataReader, Func<IDataRecord, T> selector) {
			if (dataReader == null) {
				throw new ArgumentNullException("dataReader");
			}
			if (selector == null) {
				throw new ArgumentNullException("selector");
			}

			return DbDataReaderExtensions.ReadAllCore(dataReader, selector);
		}
		/// <summary>
		/// Reads all records in the data reader and returns a collection of objects from the records.
		/// </summary>
		/// <param name="dataReader">The data reader whose records will be read.</param>
		/// <returns>The enumerable collection of objects representing the records.</returns>
		private static IEnumerable<DataRecord> ReadAllCore(IDataReader dataReader) {
			Debug.Assert(dataReader != null);

			Int32 fieldCount = dataReader.FieldCount;
			ColumnInfo[] columnInfos = new ColumnInfo[fieldCount];
			for (Int32 i = 0; i < fieldCount; i++) {
				columnInfos[i] = new ColumnInfo(i, dataReader.GetName(i), dataReader.GetFieldType(i), dataReader.GetDataTypeName(i));
			}

			while (dataReader.Read()) {
				Object[] values = new Object[fieldCount];
				dataReader.GetValues(values);
				yield return new DataRecord(columnInfos, values);
			}
			yield break;
		}
		/// <summary>
		/// Reads all records in the data reader and returns a collection of objects from the records.
		/// </summary>
		/// <typeparam name="T">The type of object returned.</typeparam>
		/// <param name="dataReader">The data reader whose records will be read.</param>
		/// <param name="selector">The function that converts an <see cref="T:IDataRecord"/> into an object of type <typeparamref name="T"/>.</param>
		/// <returns>The enumerable collection of objects representing the records.</returns>
		private static IEnumerable<T> ReadAllCore<T>(IDataReader dataReader, Func<IDataRecord, T> selector) {
			Debug.Assert(dataReader != null);
			Debug.Assert(selector != null);

			while (dataReader.Read()) {
				yield return selector(dataReader);
			}
			yield break;
		}
	}
}
