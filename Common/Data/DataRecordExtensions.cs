using System;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace Vizistata.Data {
	/// <summary>
	/// Provides extension methods for the <see cref="T:IDataRecord"/> interface.  This class may not be inherited.
	/// </summary>
	public static class DataRecordExtensions {
		#region private sealed class DataRecordConverter<T> : Object {...}
		/// <summary>
		/// Converts data record values to a specific type.  Nullable types are supported.  This class may not be inherited.
		/// </summary>
		/// <typeparam name="T">The type of value to which to convert the objects provided to instances of this class.</typeparam>
		private sealed class DataRecordConverter<T> : Object {
			#region private sealed class Nested : Object {...}
			/// <summary>
			/// This class is used to make the pattern fully lazy.  This class may not be inherited.
			/// </summary>
			private sealed class Nested : Object {
			// Fields
				/// <summary>
				/// The sole use of the Nested class is to provide the lazy, thread-safe instance of the <see cref="T:MyClass"/> object.
				/// </summary>
				internal static readonly DataRecordConverter<T> Instance = new DataRecordConverter<T>();

			// Constructors
				/// <summary>
				/// Initializes a new instance of the <see cref="T:Nested"/> class.
				/// </summary>
				private Nested() : base() { }
				/// <summary>
				/// Required in order to mark the type with 'beforefieldinit'.
				/// </summary>
				[SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline", Justification = "The static constructor is needed to mark the type with 'beforefieldinit'.")]
				static Nested() { }
			}
			#endregion

		// Fields
			/// <summary>
			/// Converts values for this instance.  This field is read-only.
			/// </summary>
			private readonly Converter<Object, T> _converter;

		// Constructors
			/// <summary>
			/// Hides the default constructor for the <see cref="T:DataRecordConverter&lt;T&gt;"/> class.
			/// </summary>
			private DataRecordConverter()
				: base() {
				Type type = typeof(T);
				if (!type.IsValueType) {
					this._converter = DataRecordConverter<T>.ConvertFromReferenceTypeValue;
				}
				else if (type.IsGenericType && !type.IsGenericTypeDefinition && typeof(Nullable<>) == type.GetGenericTypeDefinition()) {
					MethodInfo createNullableValueTypeMethod = typeof(DataRecordConverter<T>)
						.GetMethod("CreateNullableValueType", BindingFlags.Static | BindingFlags.NonPublic)
						.MakeGenericMethod(typeof(T).GetGenericArguments()[0]);
					this._converter = (Converter<Object, T>)Delegate.CreateDelegate(typeof(Converter<Object, T>), createNullableValueTypeMethod);
				}
				else {
					this._converter = DataRecordConverter<T>.ConvertFromValueTypeValue;
				}
			}

		// Properties
			/// <summary>
			/// Gets the sole instance of the <see cref="T:MyClass"/> class.
			/// </summary>
			public static DataRecordConverter<T> Instance {
				get { return DataRecordConverter<T>.Nested.Instance; }
			}

		// Methods
			/// <summary>
			/// Converts the value specified to a specific type.
			/// </summary>
			/// <param name="value">The value to convert.</param>
			/// <returns>The converted value.</returns>
			/// <exception cref="System.InvalidCastException"><paramref name="value"/> cannot be converted to the type specified by <typeparamref name="T"/>.</exception>
			public T Convert(Object value) {
				return this._converter(value);
			}
			/// <summary>
			/// Converts a value to a reference type.
			/// </summary>
			/// <param name="value">The value to convert.</param>
			/// <returns>The converted value.</returns>
			/// <exception cref="System.InvalidCastException"><paramref name="value"/> cannot be converted to the type specified by <typeparamref name="T"/>.</exception>
			private static T ConvertFromReferenceTypeValue(Object value) {
				if (value != DBNull.Value) {
					return (T)value;
				}
				return default(T);
			}
			/// <summary>
			/// Converts a value to a value type.
			/// </summary>
			/// <param name="value">The value to convert.</param>
			/// <returns>The converted value.</returns>
			/// <exception cref="System.InvalidCastException"><paramref name="value"/> cannot be converted to the type specified by <typeparamref name="T"/>.</exception>
			private static T ConvertFromValueTypeValue(Object value) {
				if (value == DBNull.Value) {
					throw new InvalidCastException("Cannot cast DBNull.Value to type '{0}'.  Please use a nullable type.".FormatInvariant(typeof(T)));
				}
				return (T)value;
			}
			/// <summary>
			/// Creates a nullable value.
			/// </summary>
			/// <param name="value">The value to convert.</param>
			/// <returns>The converted value.</returns>
			/// <exception cref="System.InvalidCastException"><paramref name="value"/> cannot be converted to the type specified by <typeparamref name="T"/>.</exception>
			private static TElement? CreateNullableValueType<TElement>(Object value) where TElement : struct {
				if (value == DBNull.Value) {
					return null;
				}
				return (TElement)value;
			}
		}
		#endregion

	// Methods
		/// <summary>
		/// Provides strongly-typed access to each of the values in the specified data record.  This method supports nullable types.
		/// </summary>
		/// <typeparam name="T">Specifies the return type of the column.</typeparam>
		/// <param name="dataRecord">The input data record for which a value should be retrieved.</param>
		/// <param name="name">The name of the value to return.</param>
		/// <returns>The value specified by <paramref name="name"/>.</returns>
		/// <exception cref="System.ArgumentNullException"><paramref name="dataRecord"/> is a null reference.
		/// -or- <paramref name="name"/> is a null reference.</exception>
		/// <exception cref="System.InvalidCastException">The value specified is not of type <typeparamref name="T"/>.</exception>
		public static T Field<T>(this IDataRecord dataRecord, String name) {
			if (dataRecord == null) {
				throw new ArgumentNullException("dataRecord");
			}
			if (name == null) {
				throw new ArgumentNullException("name");
			}
			return DataRecordConverter<T>.Instance.Convert(dataRecord[name]);
		}
		/// <summary>
		/// Provides strongly-typed access to each of the values in the specified data record.  This method supports nullable types.
		/// </summary>
		/// <typeparam name="T">Specifies the return type of the column.</typeparam>
		/// <param name="dataRecord">The input data record for which a value should be retrieved.</param>
		/// <param name="index">The 0-based index of the value to return.</param>
		/// <returns>The value specified by <paramref name="index"/>.</returns>
		/// <exception cref="System.ArgumentNullException"><paramref name="dataRecord"/> is a null reference.</exception>
		/// <exception cref="System.ArgumentOutOfRangeException"><paramref name="index"/> is less than 0.
		/// -or- <paramref name="index"/> is greater than or equal to the <see cref="P:IDataRecord.FieldCount"/> property of <paramref name="dataRecord"/>.</exception>
		/// <exception cref="System.InvalidCastException">The value specified is not of type <typeparamref name="T"/>.</exception>
		public static T Field<T>(this IDataRecord dataRecord, Int32 index) {
			if (dataRecord == null) {
				throw new ArgumentNullException("dataRecord");
			}
			if (index < 0 || index >= dataRecord.FieldCount) {
				throw new ArgumentOutOfRangeException("index");
			}
			return DataRecordConverter<T>.Instance.Convert(dataRecord[index]);
		}
	}
}
