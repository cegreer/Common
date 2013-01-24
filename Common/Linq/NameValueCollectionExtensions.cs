using System;
using System.Diagnostics;

using NameValueCollection = System.Collections.Specialized.NameValueCollection;

namespace Vizistata.Linq {
	/// <summary>
	/// Provides extension methods for the <see cref="T:NameValueCollection"/> class.  This class may not be inherited.
	/// </summary>
	public static class NameValueCollectionExtensions {
		#region private private delegate Boolean Parse<T>(String s, out T result);
		/// <summary>
		/// Represents one of the 'TryParse' methods.
		/// </summary>
		/// <typeparam name="T">The <see cref="T:Type"/> being returned.</typeparam>
		/// <param name="s">The value to parse.</param>
		/// <param name="result">The object that contains the value after parsing.</param>
		/// <returns><c>true</c> if parsing was successful; otherwise, <c>false</c>.</returns>
		private delegate Boolean TryParse<T>(String s, out T result);
		#endregion

	// Methods
		/// <summary>
		/// Returns the named value specified.
		/// </summary>
		/// <typeparam name="T">The <see cref="T:Type"/> of value to return.</typeparam>
		/// <param name="collection">The collection from which to retrieve the value.</param>
		/// <param name="name">Specifies the value to retrieve.</param>
		/// <param name="defaultValue">The default value to use if the name specified is not in the collection of if the value does not represent a <see cref="T:Boolean"/> value.</param>
		/// <param name="tryParseMethod">Represents the parsing method to use.</param>
		/// <returns>The object representing the value specified by <paramref name="name"/>, or <paramref name="defaultValue"/> if the value is not present or the value does not represent an object of type <typeparamref name="T"/>.</returns>
		/// <exception cref="System.ArgumentNullException"><paramref name="collection"/> or <paramref name="name"/> is a null reference.</exception>
		private static T Get<T>(NameValueCollection collection, String name, T defaultValue, TryParse<T> tryParseMethod) {
			if (collection == null) {
				throw new ArgumentNullException("collection");
			}
			if (name == null) {
				throw new ArgumentNullException("name");
			}
			Debug.Assert(tryParseMethod != null);

			String value = collection[name];
			T result;
			if (tryParseMethod(value, out result)) {
				return result;
			}
			return defaultValue;
		}
		/// <summary>
		/// Removes the value specified from the name value collection and returns the value.
		/// </summary>
		/// <param name="instance">The instance from which to retrieve and remove the value.</param>
		/// <param name="name">Specifies the value to retrieve and remove.</param>
		/// <returns>The value contained, or a null reference if the value did not exist in the collection.</returns>
		/// <exception cref="System.ArgumentNullException"><paramref name="instance"/> is a null reference.
		/// -or- <paramref name="name"/> is a null reference.</exception>
		public static String GetAndRemove(this NameValueCollection instance, String name) {
			if (instance == null) {
				throw new ArgumentNullException("instance");
			}
			if (name == null) {
				throw new ArgumentNullException("name");
			}
			String value = instance[name];
			instance.Remove(name);
			return value;
		}
		/// <summary>
		/// Returns the named value specified as a <see cref="T:Boolean"/> value.
		/// </summary>
		/// <param name="collection">The collection from which to retrieve the value.</param>
		/// <param name="name">Specifies the value to retrieve.</param>
		/// <param name="defaultValue">The default value to use if the name specified is not in the collection of if the value does not represent a <see cref="T:Boolean"/> value.</param>
		/// <returns>The <see cref="T:Boolean"/> value representing the value specified by <paramref name="name"/>, or <paramref name="defaultValue"/> if the value is not present or the value does not represent an object of type <see cref="T:Boolean"/>.</returns>
		/// <exception cref="System.ArgumentNullException"><paramref name="collection"/> or <paramref name="name"/> is a null reference.</exception>
		public static Boolean GetBoolean(this NameValueCollection collection, String name, Boolean defaultValue) {
			return NameValueCollectionExtensions.Get(collection, name, defaultValue, Boolean.TryParse);
		}
		/// <summary>
		/// Returns the named value specified as a <see cref="T:Byte"/> value.
		/// </summary>
		/// <param name="collection">The collection from which to retrieve the value.</param>
		/// <param name="name">Specifies the value to retrieve.</param>
		/// <param name="defaultValue">The default value to use if the name specified is not in the collection of if the value does not represent a <see cref="T:Byte"/> value.</param>
		/// <returns>The <see cref="T:Byte"/> value representing the value specified by <paramref name="name"/>, or <paramref name="defaultValue"/> if the value is not present or the value does not represent an object of type <see cref="T:Byte"/>.</returns>
		/// <exception cref="System.ArgumentNullException"><paramref name="collection"/> or <paramref name="name"/> is a null reference.</exception>
		public static Byte GetByte(this NameValueCollection collection, String name, Byte defaultValue) {
			return NameValueCollectionExtensions.Get(collection, name, defaultValue, Byte.TryParse);
		}
		/// <summary>
		/// Returns the named value specified as a <see cref="T:Char"/> value.
		/// </summary>
		/// <param name="collection">The collection from which to retrieve the value.</param>
		/// <param name="name">Specifies the value to retrieve.</param>
		/// <param name="defaultValue">The default value to use if the name specified is not in the collection of if the value does not represent a <see cref="T:Char"/> value.</param>
		/// <returns>The <see cref="T:Char"/> value representing the value specified by <paramref name="name"/>, or <paramref name="defaultValue"/> if the value is not present or the value does not represent an object of type <see cref="T:Char"/>.</returns>
		/// <exception cref="System.ArgumentNullException"><paramref name="collection"/> or <paramref name="name"/> is a null reference.</exception>
		public static Char GetChar(this NameValueCollection collection, String name, Char defaultValue) {
			return NameValueCollectionExtensions.Get(collection, name, defaultValue, Char.TryParse);
		}
		/// <summary>
		/// Returns the named value specified as a <see cref="T:DateTime"/> value.
		/// </summary>
		/// <param name="collection">The collection from which to retrieve the value.</param>
		/// <param name="name">Specifies the value to retrieve.</param>
		/// <param name="defaultValue">The default value to use if the name specified is not in the collection of if the value does not represent a <see cref="T:DateTime"/> value.</param>
		/// <returns>The <see cref="T:DateTime"/> value representing the value specified by <paramref name="name"/>, or <paramref name="defaultValue"/> if the value is not present or the value does not represent an object of type <see cref="T:DateTime"/>.</returns>
		/// <exception cref="System.ArgumentNullException"><paramref name="collection"/> or <paramref name="name"/> is a null reference.</exception>
		public static DateTime GetDateTime(this NameValueCollection collection, String name, DateTime defaultValue) {
			return NameValueCollectionExtensions.Get(collection, name, defaultValue, DateTime.TryParse);
		}
		/// <summary>
		/// Returns the named value specified as a <see cref="T:Double"/> value.
		/// </summary>
		/// <param name="collection">The collection from which to retrieve the value.</param>
		/// <param name="name">Specifies the value to retrieve.</param>
		/// <param name="defaultValue">The default value to use if the name specified is not in the collection of if the value does not represent a <see cref="T:Double"/> value.</param>
		/// <returns>The <see cref="T:Double"/> value representing the value specified by <paramref name="name"/>, or <paramref name="defaultValue"/> if the value is not present or the value does not represent an object of type <see cref="T:Double"/>.</returns>
		/// <exception cref="System.ArgumentNullException"><paramref name="collection"/> or <paramref name="name"/> is a null reference.</exception>
		public static Double GetDouble(this NameValueCollection collection, String name, Double defaultValue) {
			return NameValueCollectionExtensions.Get(collection, name, defaultValue, Double.TryParse);
		}
		/// <summary>
		/// Returns the named value specified as a <see cref="T:Guid"/> value.
		/// </summary>
		/// <param name="collection">The collection from which to retrieve the value.</param>
		/// <param name="name">Specifies the value to retrieve.</param>
		/// <param name="defaultValue">The default value to use if the name specified is not in the collection of if the value does not represent a <see cref="T:Guid"/> value.</param>
		/// <returns>The <see cref="T:Guid"/> value representing the value specified by <paramref name="name"/>, or <paramref name="defaultValue"/> if the value is not present or the value does not represent an object of type <see cref="T:Guid"/>.</returns>
		/// <exception cref="System.ArgumentNullException"><paramref name="collection"/> or <paramref name="name"/> is a null reference.</exception>
		public static Guid GetGuid(this NameValueCollection collection, String name, Guid defaultValue) {
			return NameValueCollectionExtensions.Get(collection, name, defaultValue, Guid.TryParse);
		}
		/// <summary>
		/// Returns the named value specified as a <see cref="T:Int16"/> value.
		/// </summary>
		/// <param name="collection">The collection from which to retrieve the value.</param>
		/// <param name="name">Specifies the value to retrieve.</param>
		/// <param name="defaultValue">The default value to use if the name specified is not in the collection of if the value does not represent a <see cref="T:Int16"/> value.</param>
		/// <returns>The <see cref="T:Int16"/> value representing the value specified by <paramref name="name"/>, or <paramref name="defaultValue"/> if the value is not present or the value does not represent an object of type <see cref="T:Int16"/>.</returns>
		/// <exception cref="System.ArgumentNullException"><paramref name="collection"/> or <paramref name="name"/> is a null reference.</exception>
		public static Int16 GetInt16(this NameValueCollection collection, String name, Int16 defaultValue) {
			return NameValueCollectionExtensions.Get(collection, name, defaultValue, Int16.TryParse);
		}
		/// <summary>
		/// Returns the named value specified as a <see cref="T:Int32"/> value.
		/// </summary>
		/// <param name="collection">The collection from which to retrieve the value.</param>
		/// <param name="name">Specifies the value to retrieve.</param>
		/// <param name="defaultValue">The default value to use if the name specified is not in the collection of if the value does not represent a <see cref="T:Int32"/> value.</param>
		/// <returns>The <see cref="T:Int32"/> value representing the value specified by <paramref name="name"/>, or <paramref name="defaultValue"/> if the value is not present or the value does not represent an object of type <see cref="T:Int32"/>.</returns>
		/// <exception cref="System.ArgumentNullException"><paramref name="collection"/> or <paramref name="name"/> is a null reference.</exception>
		public static Int32 GetInt32(this NameValueCollection collection, String name, Int32 defaultValue) {
			return NameValueCollectionExtensions.Get(collection, name, defaultValue, Int32.TryParse);
		}
		/// <summary>
		/// Returns the named value specified as a <see cref="T:Int64"/> value.
		/// </summary>
		/// <param name="collection">The collection from which to retrieve the value.</param>
		/// <param name="name">Specifies the value to retrieve.</param>
		/// <param name="defaultValue">The default value to use if the name specified is not in the collection of if the value does not represent a <see cref="T:Int64"/> value.</param>
		/// <returns>The <see cref="T:Int64"/> value representing the value specified by <paramref name="name"/>, or <paramref name="defaultValue"/> if the value is not present or the value does not represent an object of type <see cref="T:Int64"/>.</returns>
		/// <exception cref="System.ArgumentNullException"><paramref name="collection"/> or <paramref name="name"/> is a null reference.</exception>
		public static Int64 GetInt64(this NameValueCollection collection, String name, Int64 defaultValue) {
			return NameValueCollectionExtensions.Get(collection, name, defaultValue, Int64.TryParse);
		}
		/// <summary>
		/// Returns the named value specified as a <see cref="T:SByte"/> value.
		/// </summary>
		/// <param name="collection">The collection from which to retrieve the value.</param>
		/// <param name="name">Specifies the value to retrieve.</param>
		/// <param name="defaultValue">The default value to use if the name specified is not in the collection of if the value does not represent a <see cref="T:SByte"/> value.</param>
		/// <returns>The <see cref="T:SByte"/> value representing the value specified by <paramref name="name"/>, or <paramref name="defaultValue"/> if the value is not present or the value does not represent an object of type <see cref="T:SByte"/>.</returns>
		/// <exception cref="System.ArgumentNullException"><paramref name="collection"/> or <paramref name="name"/> is a null reference.</exception>
		[CLSCompliant(false)]
		public static SByte GetSByte(this NameValueCollection collection, String name, SByte defaultValue) {
			return NameValueCollectionExtensions.Get(collection, name, defaultValue, SByte.TryParse);
		}
		/// <summary>
		/// Returns the named value specified as a <see cref="T:Single"/> value.
		/// </summary>
		/// <param name="collection">The collection from which to retrieve the value.</param>
		/// <param name="name">Specifies the value to retrieve.</param>
		/// <param name="defaultValue">The default value to use if the name specified is not in the collection of if the value does not represent a <see cref="T:Single"/> value.</param>
		/// <returns>The <see cref="T:Single"/> value representing the value specified by <paramref name="name"/>, or <paramref name="defaultValue"/> if the value is not present or the value does not represent an object of type <see cref="T:Single"/>.</returns>
		/// <exception cref="System.ArgumentNullException"><paramref name="collection"/> or <paramref name="name"/> is a null reference.</exception>
		public static Single GetSingle(this NameValueCollection collection, String name, Single defaultValue) {
			return NameValueCollectionExtensions.Get(collection, name, defaultValue, Single.TryParse);
		}
		/// <summary>
		/// Returns the named value specified as a <see cref="T:UInt16"/> value.
		/// </summary>
		/// <param name="collection">The collection from which to retrieve the value.</param>
		/// <param name="name">Specifies the value to retrieve.</param>
		/// <param name="defaultValue">The default value to use if the name specified is not in the collection of if the value does not represent a <see cref="T:UInt16"/> value.</param>
		/// <returns>The <see cref="T:UInt16"/> value representing the value specified by <paramref name="name"/>, or <paramref name="defaultValue"/> if the value is not present or the value does not represent an object of type <see cref="T:UInt16"/>.</returns>
		/// <exception cref="System.ArgumentNullException"><paramref name="collection"/> or <paramref name="name"/> is a null reference.</exception>
		[CLSCompliant(false)]
		public static UInt16 GetUInt16(this NameValueCollection collection, String name, UInt16 defaultValue) {
			return NameValueCollectionExtensions.Get(collection, name, defaultValue, UInt16.TryParse);
		}
		/// <summary>
		/// Returns the named value specified as a <see cref="T:UInt32"/> value.
		/// </summary>
		/// <param name="collection">The collection from which to retrieve the value.</param>
		/// <param name="name">Specifies the value to retrieve.</param>
		/// <param name="defaultValue">The default value to use if the name specified is not in the collection of if the value does not represent a <see cref="T:UInt32"/> value.</param>
		/// <returns>The <see cref="T:UInt32"/> value representing the value specified by <paramref name="name"/>, or <paramref name="defaultValue"/> if the value is not present or the value does not represent an object of type <see cref="T:UInt32"/>.</returns>
		/// <exception cref="System.ArgumentNullException"><paramref name="collection"/> or <paramref name="name"/> is a null reference.</exception>
		[CLSCompliant(false)]
		public static UInt32 GetUInt32(this NameValueCollection collection, String name, UInt32 defaultValue) {
			return NameValueCollectionExtensions.Get(collection, name, defaultValue, UInt32.TryParse);
		}
		/// <summary>
		/// Returns the named value specified as a <see cref="T:UInt64"/> value.
		/// </summary>
		/// <param name="collection">The collection from which to retrieve the value.</param>
		/// <param name="name">Specifies the value to retrieve.</param>
		/// <param name="defaultValue">The default value to use if the name specified is not in the collection of if the value does not represent a <see cref="T:UInt64"/> value.</param>
		/// <returns>The <see cref="T:UInt64"/> value representing the value specified by <paramref name="name"/>, or <paramref name="defaultValue"/> if the value is not present or the value does not represent an object of type <see cref="T:UInt64"/>.</returns>
		/// <exception cref="System.ArgumentNullException"><paramref name="collection"/> or <paramref name="name"/> is a null reference.</exception>
		[CLSCompliant(false)]
		public static UInt64 GetUInt64(this NameValueCollection collection, String name, UInt64 defaultValue) {
			return NameValueCollectionExtensions.Get(collection, name, defaultValue, UInt64.TryParse);
		}
	}
}
