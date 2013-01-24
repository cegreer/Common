using System;

namespace Vizistata {
	/// <summary>
	/// Provides extension methods for the <see cref="T:Array"/> class.  This class may not be inherited.
	/// </summary>
	public static class ArrayExtensions {
	// Methods
		/// <summary>
		/// Returns the element at the specified index.  If the index is out of the array's bound, the default value will be returned.
		/// </summary>
		/// <typeparam name="T">The type of element in the array.</typeparam>
		/// <param name="instance">The instance on which to call the method.</param>
		/// <param name="index">The 0-based index of the element in the array.</param>
		/// <returns>The element at the index specified. -or- The default value if <typeparamref name="T"/> if <paramref name="index"/> is out of bounds for the array.</returns>
		/// <exception cref="System.ArgumentNullException"><paramref name="instance"/> is a null reference.</exception>
		public static T GetElementOrDefaultAt<T>(this T[] instance, Int32 index) {
			if (instance == null) {
				throw new ArgumentNullException("instance");
			}
			if (index < 0 || index >= instance.Length) {
				return default(T);
			}
			return instance[index];
		}
	}
}
