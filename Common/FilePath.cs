using System;
using System.Diagnostics;
using System.IO;

using ImmutableObject = System.ComponentModel.ImmutableObjectAttribute;
using Res = Vizistata.Properties.Resources;

namespace Vizistata {
	/// <summary>
	/// Represents a valid file path.  This class may not be inherited.  Instances of this class are immutable.
	/// </summary>
	[Serializable()]
	[ImmutableObject(true)]
	public sealed class FilePath : Object, IEquatable<FilePath>, IEquatable<String> {
	// Fields
		/// <summary>
		/// The actual file path value.  This field is read-only.
		/// </summary>
		private readonly String _value;
		/// <summary>
		/// The maximum length of a file name.
		/// </summary>
		public static readonly Int32 MaximumFileLength = 260;

	// Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:FilePath"/> class.
		/// </summary>
		/// <param name="value">The value of the file path.</param>
		/// <exception cref="System.ArgumentNullException"><paramref name="value"/> is a null reference.</exception>
		/// <exception cref="System.ArgumentException"><paramref name="value"/> has a length of 0.
		/// -or- <paramref name="value"/> contains only white-space characters.
		/// -or- The <see cref="P:String.Length"/> property of <paramref name="value"/> exceeds <see cref="F:FileName.MaximumFileLength"/>.
		/// -or- <paramref name="value"/> contains any characters defined by <see cref="M:Path.GetInvalidPathChars"/>.
		/// -or- <paramref name="value"/> contains any characters defined by <see cref="M:Path.GetInvalidFileNameChars"/>.</exception>
		public FilePath(String value)
			: base() {
			if (value == null) {
				throw new ArgumentNullException("value");
			}
			if (value.Length == 0) {
				throw new ArgumentException(Res.ValueIsEmptyMessage, "value");
			}
			if (value.Trim().Length == 0) {
				throw new ArgumentException(Res.TrimmedValueIsEmptyMessage, "value");
			}
			if (value.Length > FilePath.MaximumFileLength) {
				throw new ArgumentException(Res.ValueLengthExceededFormat.FormatCurrentCulture(FilePath.MaximumFileLength), "value");
			}
			if (value.IndexOfAny(Path.GetInvalidPathChars()) > -1) {
				throw new ArgumentException(Res.FileNameContainsInvalidPathCharactersMessage, "value");
			}
			if (Path.GetFileName(value).IndexOfAny(Path.GetInvalidFileNameChars()) > -1) {
				throw new ArgumentException(Res.FileNameContainsInvalidFileNameCharactersMessage, "value");
			}
			
			this._value = value;
		}

	// Methods
		/// <summary>
		/// Determines if the two objects specified are equal.
		/// </summary>
		/// <param name="objA">The first object to compare.</param>
		/// <param name="objB">The second object to compare.</param>
		/// <returns><c>true</c> if the two objects are equal; otherwise, <c>false</c>.</returns>
		private static Boolean AreEqual(FilePath objA, FilePath objB) {
			if (Object.ReferenceEquals(objA, null)) {
				return Object.ReferenceEquals(objB, null);
			}
			else if (Object.ReferenceEquals(objB, null)) {
				return false;
			}

			return String.Equals(objA._value, objB._value, StringComparison.OrdinalIgnoreCase);
		}
		/// <summary>
		/// Determines if the two objects specified are equal.
		/// </summary>
		/// <param name="filePath">The first object to compare.</param>
		/// <param name="value">The second object to compare.</param>
		/// <returns><c>true</c> if the two objects are equal; otherwise, <c>false</c>.</returns>
		private static Boolean AreEqual(FilePath filePath, String value) {
			if (Object.ReferenceEquals(filePath, null)) {
				return Object.ReferenceEquals(value, null);
			}
			else if (Object.ReferenceEquals(value, null)) {
				return false;
			}

			return String.Equals(filePath._value, value, StringComparison.OrdinalIgnoreCase);
		}
		/// <summary>
		/// Returns a value indicating if the object specified is equal to this instance.
		/// </summary>
		/// <param name="obj">The object to compare to this instance.</param>
		/// <returns><c>true</c> if the object specified is equal to this instance; otherwise, <c>false</c>.</returns>
		public override Boolean Equals(Object obj) {
			return this.Equals(obj as FilePath) || this.Equals(obj as String);
		}
		/// <summary>
		/// Returns a value indicating if the object specified is equal to this instance.
		/// </summary>
		/// <param name="other">The object to compare to this instance.</param>
		/// <returns><c>true</c> if the object specified is equal to this instance; otherwise, <c>false</c>.</returns>
		public Boolean Equals(FilePath other) {
			if (other == null) {
				return false;
			}
			return FilePath.AreEqual(this, other);
		}
		/// <summary>
		/// Returns a value indicating if the object specified is equal to this instance.
		/// </summary>
		/// <param name="other">The object to compare to this instance.</param>
		/// <returns><c>true</c> if the object specified is equal to this instance; otherwise, <c>false</c>.</returns>
		public Boolean Equals(String other) {
			if (other == null) {
				return false;
			}
			return FilePath.AreEqual(this, other);
		}
		/// <summary>
		/// Returns a value indicating if the file exists and the current user has permission to access the file.
		/// </summary>
		/// <returns><c>true</c> if the file exists and the current user has permission to access the file; otherwise, <c>false</c>.</returns>
		public Boolean Exists() {
			return File.Exists(this._value);
		}
		/// <summary>
		/// Returns a value that can be used as a hash code for this instance.
		/// </summary>
		/// <returns>A value that can be used as a hash code for this instance.</returns>
		public override Int32 GetHashCode() {
			Debug.Assert(this._value != null);
			return this._value.ToUpperInvariant().GetHashCode();
		}
		/// <summary>
		/// Opens the file referenced by this instance.
		/// </summary>
		/// <param name="mode">Specifies the actions to take when opening the file.</param>
		/// <returns>A <see cref="T:FileStream"/> object that can be used to access the file.</returns>
		/// <exception cref="System.IO.DirectoryNotFoundException">The path specified by this instance is invalid, (for example, it is on an unmapped drive).</exception>
		/// <exception cref="System.IO.FileNotFoundException">The file specified by this instance was not found.</exception>
		/// <exception cref="System.IO.IOException">An I/O error occurred while opening the file.</exception>
		/// <exception cref="System.UnauthorizedAccessException">The path specified a file that is read-only and access is not Read.
		/// -or- The path specified a directory.
		/// -or- The caller does not have the required permission.</exception>
		public FileStream Open(FileMode mode) {
			return this.Open(mode, FileAccess.ReadWrite, FileShare.None);
		}
		/// <summary>
		/// Opens the file referenced by this instance.
		/// </summary>
		/// <param name="mode">Specifies the actions to take when opening the file.</param>
		/// <param name="access">Specifies how the file should be accessed.</param>
		/// <returns>A <see cref="T:FileStream"/> object that can be used to access the file.</returns>
		/// <exception cref="System.IO.DirectoryNotFoundException">The path specified by this instance is invalid, (for example, it is on an unmapped drive).</exception>
		/// <exception cref="System.IO.FileNotFoundException">The file specified by this instance was not found.</exception>
		/// <exception cref="System.IO.IOException">An I/O error occurred while opening the file.</exception>
		/// <exception cref="System.UnauthorizedAccessException">The path specified a file that is read-only and access is not Read.
		/// -or- The path specified a directory.
		/// -or- The caller does not have the required permission.</exception>
		public FileStream Open(FileMode mode, FileAccess access) {
			return this.Open(mode, access, FileShare.None);
		}
		/// <summary>
		/// Opens the file referenced by this instance.
		/// </summary>
		/// <param name="mode">Specifies the actions to take when opening the file.</param>
		/// <param name="access">Specifies how the file should be accessed.</param>
		/// <param name="share">Specifies actions to take for sharing access to the file.</param>
		/// <returns>A <see cref="T:FileStream"/> object that can be used to access the file.</returns>
		/// <exception cref="System.IO.DirectoryNotFoundException">The path specified by this instance is invalid, (for example, it is on an unmapped drive).</exception>
		/// <exception cref="System.IO.FileNotFoundException">The file specified by this instance was not found.</exception>
		/// <exception cref="System.IO.IOException">An I/O error occurred while opening the file.</exception>
		/// <exception cref="System.UnauthorizedAccessException">The path specified a file that is read-only and access is not Read.
		/// -or- The path specified a directory.
		/// -or- The caller does not have the required permission.</exception>
		public FileStream Open(FileMode mode, FileAccess access, FileShare share) {
			return File.Open(this._value, mode, access, share);
		}
		/// <summary>
		/// Opens the file referenced by this instance as read-only.
		/// </summary>
		/// <returns>A <see cref="T:FileStream"/> object that can be used to access the file.</returns>
		/// <exception cref="System.IO.DirectoryNotFoundException">The path specified by this instance is invalid, (for example, it is on an unmapped drive).</exception>
		/// <exception cref="System.IO.FileNotFoundException">The file specified by this instance was not found.</exception>
		/// <exception cref="System.IO.IOException">An I/O error occurred while opening the file.</exception>
		/// <exception cref="System.UnauthorizedAccessException">The path specified a file that is read-only and access is not Read.
		/// -or- The path specified a directory.
		/// -or- The caller does not have the required permission.</exception>
		public FileStream OpenRead() {
			return File.OpenRead(this._value);
		}
		/// <summary>
		/// Opens the file referenced by this instance for writing.
		/// </summary>
		/// <returns>A <see cref="T:FileStream"/> object that can be used to access the file.</returns>
		/// <exception cref="System.IO.DirectoryNotFoundException">The path specified by this instance is invalid, (for example, it is on an unmapped drive).</exception>
		/// <exception cref="System.IO.FileNotFoundException">The file specified by this instance was not found.</exception>
		/// <exception cref="System.IO.IOException">An I/O error occurred while opening the file.</exception>
		/// <exception cref="System.UnauthorizedAccessException">The path specified a file that is read-only and access is not Read.
		/// -or- The path specified a directory.
		/// -or- The caller does not have the required permission.</exception>
		public FileStream OpenWrite() {
			return File.OpenWrite(this._value);
		}
		/// <summary>
		/// Returns the string value of this instance.
		/// </summary>
		/// <returns>The string representation of this instance.</returns>
		public override String ToString() {
			return this._value;
		}

	// Operators
		/// <summary>
		/// Determines if the two objects specified are equal.
		/// </summary>
		/// <param name="objA">The first object to compare.</param>
		/// <param name="objB">The second object to compare.</param>
		/// <returns><c>true</c> if the two objects are equal; otherwise, <c>false</c>.</returns>
		public static Boolean operator ==(FilePath objA, FilePath objB) {
			return FilePath.AreEqual(objA, objB);
		}
		/// <summary>
		/// Determines if the two objects specified are equal.
		/// </summary>
		/// <param name="filePath">The first object to compare.</param>
		/// <param name="value">The second object to compare.</param>
		/// <returns><c>true</c> if the two objects are equal; otherwise, <c>false</c>.</returns>
		public static Boolean operator ==(FilePath filePath, String value) {
			return FilePath.AreEqual(filePath, value);
		}
		/// <summary>
		/// Determines if the two objects specified are equal.
		/// </summary>
		/// <param name="value">The second object to compare.</param>
		/// <param name="filePath">The first object to compare.</param>
		/// <returns><c>true</c> if the two objects are equal; otherwise, <c>false</c>.</returns>
		public static Boolean operator ==(String value, FilePath filePath) {
			return FilePath.AreEqual(filePath, value);
		}
		/// <summary>
		/// Determines if the two objects specified are not equal.
		/// </summary>
		/// <param name="objA">The first object to compare.</param>
		/// <param name="objB">The second object to compare.</param>
		/// <returns><c>true</c> if the two objects not are equal; otherwise, <c>false</c>.</returns>
		public static Boolean operator !=(FilePath objA, FilePath objB) {
			return !FilePath.AreEqual(objA, objB);
		}
		/// <summary>
		/// Determines if the two objects specified are not equal.
		/// </summary>
		/// <param name="filePath">The first object to compare.</param>
		/// <param name="value">The second object to compare.</param>
		/// <returns><c>true</c> if the two objects not are equal; otherwise, <c>false</c>.</returns>
		public static Boolean operator !=(FilePath filePath, String value) {
			return !FilePath.AreEqual(filePath, value);
		}
		/// <summary>
		/// Determines if the two objects specified are not equal.
		/// </summary>
		/// <param name="value">The second object to compare.</param>
		/// <param name="filePath">The first object to compare.</param>
		/// <returns><c>true</c> if the two objects not are equal; otherwise, <c>false</c>.</returns>
		public static Boolean operator !=(String value, FilePath filePath) {
			return !FilePath.AreEqual(filePath, value);
		}
		/// <summary>
		/// Implicitly casts a <see cref="T:FilePath"/> object into a <see cref="T:String"/> instance.
		/// </summary>
		/// <param name="instance">The object to cast.</param>
		/// <returns>The <see cref="T:String"/> representation of <paramref name="instance"/>.</returns>
		public static implicit operator String(FilePath instance) {
			if (instance == null) {
				return null;
			}
			return instance._value;
		}
		/// <summary>
		/// Explicitly casts a <see cref="T:String"/> object into a <see cref="T:FilePath"/> instance.
		/// </summary>
		/// <param name="instance">The object to cast.</param>
		/// <returns>The <see cref="T:FileName"/> representation of <paramref name="instance"/>.</returns>
		/// <exception cref="System.InvalidCastException"><paramref name="instance"/> is not valid for the <see cref="T:FilePath"/> class.</exception>
		public static explicit operator FilePath(String instance) {
			if (instance == null) {
				return null;
			}

			try {
				return new FilePath(instance);
			}
			catch (ArgumentException) {
				throw new InvalidCastException();
			}
		}
	}
}
