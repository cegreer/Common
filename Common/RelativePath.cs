using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

using ImmutableObject = System.ComponentModel.ImmutableObjectAttribute;
using Res = Vizistata.Properties.Resources;

namespace Vizistata {
	/// <summary>
	/// Represents a relative path along with associated node information.  This class may not be inherited.  Instances of this class are immutable.
	/// </summary>
	[Serializable()]
	[ImmutableObject(true)]
	public sealed class RelativePath : Object, IEquatable<RelativePath>, IEquatable<String>, IComparable<RelativePath>, IComparable {
	// Constants
		/// <summary>
		/// A back slash = '\'.
		/// </summary>
		public const Char Backslash = '\\';
		/// <summary>
		/// A forward slash = '/'.
		/// </summary>
		public const Char ForwardSlash = '/';

	// Fields
		/// <summary>
		/// The collection of nodes in the path.  This field is read-only.
		/// </summary>
		private readonly String[] _nodes;
		/// <summary>
		/// The separator for the nodes.
		/// </summary>
		private readonly Char _separator;

	// Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:RelativePath"/> class.
		/// </summary>
		/// <param name="path">The path from which to create this instance.</param>
		/// <exception cref="System.ArgumentNullException"><paramref name="path"/> is a null reference.</exception>
		/// <exception cref="System.ArgumentException"><paramref name="path"/> contains multiple valid separators.
		/// -or- <paramref name="path"/> contains no valid separators.
		/// -or- <paramref name="path"/> represents an invalid path.</exception>
		public RelativePath(String path)
			: base() {
			if (path == null) {
				throw new ArgumentNullException("path");
			}

			Char separator = (Char)RelativePath.InferSeparator(path, "path", true);
			String[] nodes = RelativePath.ValidateNodes(path.Split(separator), "path", false, true);
			this._nodes = nodes;
			this._separator = separator;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="T:RelativePath"/> class.
		/// </summary>
		/// <param name="path">The path from which to create this instance.</param>
		/// <param name="separator">The separator used in the path.</param>
		/// <exception cref="System.ArgumentNullException"><paramref name="path"/> is a null reference.</exception>
		/// <exception cref="System.ArgumentException"><paramref name="separator"/> is not defined by <see cref="P:ValidSeparators"/>.
		/// -or- <paramref name="path"/> contains a separator other than <paramref name="separator"/>.
		/// -or- <paramref name="path"/> represents an invalid path.</exception>
		public RelativePath(String path, Char separator) : this(path, separator, false) { }
		/// <summary>
		/// Initializes a new instance of the <see cref="T:RelativePath"/> class.
		/// </summary>
		/// <param name="path">The path from which to create this instance.</param>
		/// <param name="separator">The separator used in the path.</param>
		/// <param name="removeEmptyLastNode"><c>true</c> if the last node should be removed if it is empty; otherwise, <c>false</c>.</param>
		/// <exception cref="System.ArgumentNullException"><paramref name="path"/> is a null reference.</exception>
		/// <exception cref="System.ArgumentException"><paramref name="separator"/> is not defined by <see cref="P:ValidSeparators"/>.
		/// -or- <paramref name="path"/> contains a separator other than <paramref name="separator"/>.
		/// -or- <paramref name="path"/> represents an invalid path.</exception>
		public RelativePath(String path, Char separator, Boolean removeEmptyLastNode)
			: base() {
			if (path == null) {
				throw new ArgumentNullException("path");
			}
			if (!RelativePath.ValidSeparators.Contains(separator)) {
				throw new ArgumentException(Res.SeparatorNotValidMessage, "separator");
			}

			String[] nodes = RelativePath.ValidateNodes(path.Split(separator), "path", removeEmptyLastNode, true);
			this._nodes = nodes;
			this._separator = separator;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="T:RelativePath"/> class.
		/// </summary>
		/// <param name="nodes">Represents the nodes of the path.</param>
		/// <param name="separator">The separator used in the path.</param>
		/// <exception cref="System.ArgumentNullException"><paramref name="nodes"/> is a null reference.</exception>
		/// <exception cref="System.ArgumentException"><paramref name="separator"/> is not defined by <see cref="P:ValidSeparators"/>.
		/// -or- <paramref name="nodes"/> contains any element with a separator.
		/// -or- <paramref name="nodes"/> contains invalid pathing nodes.</exception>
		public RelativePath(String[] nodes, Char separator) : this(nodes, separator, false, false) { }
		/// <summary>
		/// Initializes a new instance of the <see cref="T:RelativePath"/> class.
		/// </summary>
		/// <param name="nodes">Represents the nodes of the path.</param>
		/// <param name="separator">The separator used in the path.</param>
		/// <param name="removeEmptyLastNode"><c>true</c> if the last node should be removed if it is empty; otherwise, <c>false</c>.</param>
		/// <exception cref="System.ArgumentNullException"><paramref name="nodes"/> is a null reference.</exception>
		/// <exception cref="System.ArgumentException"><paramref name="separator"/> is not defined by <see cref="P:ValidSeparators"/>.
		/// -or- <paramref name="nodes"/> contains any element with a separator.
		/// -or- <paramref name="nodes"/> contains invalid pathing nodes.</exception>
		public RelativePath(String[] nodes, Char separator, Boolean removeEmptyLastNode) : this(nodes, separator, removeEmptyLastNode, false) { }
		/// <summary>
		/// Initializes a new instance of the <see cref="T:RelativePath"/> class.
		/// </summary>
		/// <param name="nodes">Represents the nodes of the path.</param>
		/// <param name="separator">The separator used in the path.</param>
		/// <param name="removeEmptyLastNode"><c>true</c> if the last node should be removed if it is empty; otherwise, <c>false</c>.</param>
		/// <param name="skipValidation"><c>true</c> to skip argument validation; otherwise, <c>false</c>.</param>
		/// <exception cref="System.ArgumentNullException"><paramref name="nodes"/> is a null reference and <paramref name="skipValidation"/> is <c>false</c>.</exception>
		/// <exception cref="System.ArgumentException"><paramref name="separator"/> is not defined by <see cref="P:ValidSeparators"/> and <paramref name="skipValidation"/> is <c>false</c>.
		/// -or- <paramref name="nodes"/> contains any element with a separator and <paramref name="skipValidation"/> is <c>false</c>.
		/// -or- <paramref name="nodes"/> contains invalid pathing nodes and <paramref name="skipValidation"/> is <c>false</c>.</exception>
		private RelativePath(String[] nodes, Char separator, Boolean removeEmptyLastNode, Boolean skipValidation)
			: base() {
			String[] validNodes = nodes;
			if (!skipValidation) {
				if (nodes == null) {
					throw new ArgumentNullException("nodes");
				}
				if (!RelativePath.ValidSeparators.Contains(separator)) {
					throw new ArgumentException(Res.SeparatorNotValidMessage, "separator");
				}

				validNodes = RelativePath.ValidateNodes(validNodes, "nodes", removeEmptyLastNode, true);
			}

			this._nodes = validNodes;
			this._separator = separator;
		}

	// Properties
		/// <summary>
		/// Gets the collection of nodes in the path.
		/// </summary>
		public IEnumerable<String> Nodes {
			get { return (String[])this._nodes.Clone(); }
		}
		/// <summary>
		/// Gets the separator that divides the nodes in the path.
		/// </summary>
		public Char Separator {
			get { return this._separator; }
		}
		/// <summary>
		/// Gets the collection of valid separators.
		/// </summary>
		public static IEnumerable<Char> ValidSeparators {
			get { return new Char[] { ForwardSlash, Backslash }; }
		}

	// Methods
		/// <summary>
		/// Appends the path specified to this instance to create a new relative path.
		/// </summary>
		/// <param name="path">The path to append to this instance.</param>
		/// <returns>The new relative path created.</returns>
		/// <exception cref="System.ArgumentNullException"><paramref name="path"/> is a null reference.</exception>
		/// <exception cref="System.ArgumentException"><paramref name="path"/> contains other separators not used by this instance.
		/// -or- <paramref name="path"/> contains invalid pathing characters or nodes.</exception>
		public RelativePath Append(String path) {
			return this.Append(path, false);
		}
		/// <summary>
		/// Appends the path specified to this instance to create a new relative path.
		/// </summary>
		/// <param name="path">The path to append to this instance.</param>
		/// <param name="normalizeSeparators"><c>true</c> if the separators should be normalized to match this instance's separators; otherwise, <c>false</c>.</param>
		/// <returns>The new relative path created.</returns>
		/// <exception cref="System.ArgumentNullException"><paramref name="path"/> is a null reference.</exception>
		/// <exception cref="System.ArgumentException"><paramref name="path"/> contains other separators not used by this instance and <paramref name="normalizeSeparators"/> is <c>false</c>.
		/// -or- <paramref name="path"/> contains invalid pathing characters or nodes.</exception>
		public RelativePath Append(String path, Boolean normalizeSeparators) {
			if (path == null) {
				throw new ArgumentNullException("path");
			}
			String additionalPath = path.Trim();
			if (additionalPath.Length == 0) {
				return this;
			}

			Char[] otherSeparators = RelativePath.ValidSeparators
				.Except(new Char[] { this._separator })
				.ToArray();
			if (additionalPath.ContainsAny(otherSeparators)) {
				if (!normalizeSeparators) {
					throw new ArgumentException(Res.SeparatorNotSupportedMessage, "path");
				}
				foreach (Char otherSeparator in otherSeparators) {
					additionalPath = additionalPath.Replace(otherSeparator, this._separator);
				}
			}
			if (additionalPath[0] == this._separator) {
				additionalPath = additionalPath.Substring(1).Trim();
			}
			if (additionalPath.Length == 0) {
				return this;
			}

			String[] additionalNodes = additionalPath.Split(this._separator);
			String[] currentNodes = this._nodes;
			if (currentNodes.Length != 0 && String.IsNullOrEmpty(currentNodes.Last())) {
				currentNodes = currentNodes.Take(currentNodes.Length - 1).ToArray();
			}
			String[] nodes = currentNodes
				.Concat(additionalNodes)
				.ToArray();

			try {
				return new RelativePath(nodes, this._separator);
			}
			catch (Exception ex) {
				throw new ArgumentException(Res.PathContainsInvalidCharactersMessage, "path", ex);
			}
		}
		/// <summary>
		/// Returns a value indicating if the two objects specified are equal.
		/// </summary>
		/// <param name="objA">The first object.</param>
		/// <param name="objB">The second object.</param>
		/// <returns><c>true</c> if the two objects are equal; otherwise, <c>false</c>.</returns>
		private static Boolean AreEqual(RelativePath objA, RelativePath objB) {
			if (Object.ReferenceEquals(objA, null)) {
				return Object.ReferenceEquals(objB, null);
			}
			else if (Object.ReferenceEquals(objB, null)) {
				return false;
			}

			if (objA._separator != objB._separator) {
				return false;
			}
			if (objA._nodes.Length != objB._nodes.Length) {
				return false;
			}
			foreach (Int32 i in Enumerable.Range(0, objA._nodes.Length)) {
				if (!String.Equals(objA._nodes[i], objB._nodes[i], StringComparison.Ordinal)) {
					return false;
				}
			}
			return true;
		}
		/// <summary>
		/// Returns a value indicating if the two objects specified are equal.
		/// </summary>
		/// <param name="objA">The first object.</param>
		/// <param name="objB">The second object.</param>
		/// <returns><c>true</c> if the two objects are equal; otherwise, <c>false</c>.</returns>
		private static Boolean AreEqual(RelativePath objA, String objB) {
			if (Object.ReferenceEquals(objA, null)) {
				return Object.ReferenceEquals(objB, null);
			}
			else if (Object.ReferenceEquals(objB, null)) {
				return false;
			}

			return String.Equals(objA.ToString(), objB, StringComparison.Ordinal);
		}
		/// <summary>
		/// Compares the current object with another object of the same type.
		/// </summary>
		/// <param name="other">An object to compare with this object.</param>
		/// <returns>A value that indicates the relative order of the objects being compared.  The return value has the following meanings.
		/// Less than zero: This object is less than the other parameter.
		/// Zero: This object is equal to other.
		/// Greater than zero: This object is greater than other.</returns>
		public Int32 CompareTo(RelativePath other) {
			if (other == null) {
				return 1;
			}

			return String.CompareOrdinal(this.ToString(), other.ToString());
		}
		/// <summary>
		/// Returns a path based on the nodes and separator specified.
		/// </summary>
		/// <param name="separator">The separator to use.</param>
		/// <param name="nodes">The nodes to join.</param>
		/// <returns>The path created.</returns>
		private static String CreatePath(Char separator, String[] nodes) {
			Debug.Assert(nodes != null);
			return nodes.Join(separator);
		}
		/// <summary>
		/// Determines whether the object specified is equal to this instance.
		/// </summary>
		/// <param name="obj">The object to compare to this instance.</param>
		/// <returns><c>true</c> if the other object is equal to this instance; otherwise, <c>false</c>.</returns>
		public override Boolean Equals(Object obj) {
			return this.Equals(obj as RelativePath) || this.Equals(obj as String);
		}
		/// <summary>
		/// Determines whether the object specified is equal to this instance.
		/// </summary>
		/// <param name="other">The object to compare to this instance.</param>
		/// <returns><c>true</c> if the other object is equal to this instance; otherwise, <c>false</c>.</returns>
		public Boolean Equals(RelativePath other) {
			return RelativePath.AreEqual(this, other);
		}
		/// <summary>
		/// Determines whether the object specified is equal to this instance.
		/// </summary>
		/// <param name="other">The object to compare to this instance.</param>
		/// <returns><c>true</c> if the other object is equal to this instance; otherwise, <c>false</c>.</returns>
		public Boolean Equals(String other) {
			return RelativePath.AreEqual(this, other);
		}
		/// <summary>
		/// Returns a hash code value for this instance.
		/// </summary>
		/// <returns>The hash code value for this instance.</returns>
		public override Int32 GetHashCode() {
			return this.ToString().GetHashCode();
		}
		/// <summary>
		/// Attempts to infer the separator for a path and returns the value inferred.
		/// </summary>
		/// <param name="path">The path from which to infer the separator.</param>
		/// <param name="argumentName">The name of the argument being validated.</param>
		/// <param name="throwOnError"><c>true</c> to throw an exception if an error occurs; otherwise, <c>false</c>.</param>
		/// <returns>The separator for the path, or a null reference if one cannot be inferred.</returns>
		/// <exception cref="System.ArgumentException"><paramref name="path"/> has more than one valid separator and <paramref name="throwOnError"/> is <c>true</c>.
		/// -or- <paramref name="path"/> has no valid separators and <paramref name="throwOnError"/> is <c>true</c>.</exception>
		private static Char? InferSeparator(String path, String argumentName, Boolean throwOnError) {
			Debug.Assert(path != null);
			Char[] validSeparators = RelativePath.ValidSeparators.ToArray();
			foreach (Int32 i in Enumerable.Range(0, validSeparators.Length)) {
				Char separator = validSeparators[i];
				if (path.Contains(separator)) {
					Char[] otherSeparators = validSeparators.Skip(i + 1).ToArray();
					if (path.ContainsAny(otherSeparators)) {
						if (throwOnError) {
							throw new ArgumentException(Res.MultipleSeparatorsPreventsInferrenceMessage, argumentName);
						}
						return null;
					}
					return separator;
				}
			}

			if (throwOnError) {
				throw new ArgumentException(Res.LackOfSeparatorPreventsInferrenceMessage, argumentName);
			}
			return null;
		}
		/// <summary>
		/// Returns a value indicating if the node is valid.
		/// </summary>
		/// <param name="node">The node to validate.</param>
		/// <returns><c>true</c> if the node is valid; otherwise, <c>false</c>.</returns>
		private static Boolean IsValid(String node) {
			Debug.Assert(node != null);
			// Empty string is valid.
			if (node.Length == 0) {
				return true;
			}

			// Starting or trailing white-space is invalid.
			if (node.Trim().Length != node.Length) {
				return false;
			}

			// Any separator is invalid.
			if (node.ContainsAny(RelativePath.ValidSeparators.ToArray())) {
				return false;
			}

			return true;
		}
		/// <summary>
		/// Navigates up a single node and returns the relative path from that point.
		/// </summary>
		/// <returns>The relative path from the point specified.  An empty path will be returned if the node count exceeds the number of nodes in the path.</returns>
		public RelativePath NavigateUp() {
			return this.NavigateUp(1);
		}
		/// <summary>
		/// Navigates up a number of nodes and returns the relative path from that point.
		/// </summary>
		/// <param name="nodeCount">The number of nodes to move up.</param>
		/// <returns>The relative path from the point specified.  An empty path will be returned if the node count exceeds the number of nodes in the path.</returns>
		/// <exception cref="System.ArgumentOutOfRangeException"><paramref name="nodeCount"/> is less than 1.</exception>
		public RelativePath NavigateUp(Int32 nodeCount) {
			if (nodeCount < 1) {
				throw new ArgumentOutOfRangeException("nodeCount");
			}
			if (nodeCount >= this._nodes.Length) {
				return new RelativePath(new String[0], this._separator);
			}

			Int32 nodesToKeep = this._nodes.Length - nodeCount;
			String[] nodes = this._nodes.Take(nodesToKeep).ToArray();
			return new RelativePath(nodes, this._separator);
		}
		/// <summary>
		/// Returns the string representation of this instance.
		/// </summary>
		/// <returns>The string representation of this instance.</returns>
		public override String ToString() {
			Debug.Assert(this._nodes != null);
			String path = RelativePath.CreatePath(this._separator, this._nodes);
			return path;
		}
		/// <summary>
		/// Validates the nodes specified.
		/// </summary>
		/// <param name="nodes">The nodes to validate.</param>
		/// <param name="argumentName">The name of the argument being validated.</param>
		/// <param name="removeEmptyLastNode"><c>true</c> if the method should remove the last node if it is empty; otherwise, <c>false</c>.</param>
		/// <param name="throwOnError"><c>true</c> to throw an exception if an error occurs; otherwise, <c>false</c>.</param>
		/// <returns>The validated nodes, or a null reference.</returns>
		/// <exception cref="System.ArgumentException"><paramref name="nodes"/> contains an invalid node and <paramref name="throwOnError"/> is <c>true</c>.</exception>
		private static String[] ValidateNodes(String[] nodes, String argumentName, Boolean removeEmptyLastNode, Boolean throwOnError) {
			Debug.Assert(nodes != null);
			// Make sure no node is null.
			if (nodes.Any(node => node == null)) {
				if (throwOnError) {
					throw new ArgumentException(Res.CollectionContainsNullReferenceMessage, argumentName);
				}
				return null;
			}

			// Make sure no node is empty except the first and last.
			if (nodes.Length > 2) {
				if (nodes.Skip(1).Take(nodes.Length - 2).Any(node => node.Length == 0)) {
					if (throwOnError) {
						throw new ArgumentException(Res.CollectionContainsEmptyStringBeforeLastElementMessage, argumentName);
					}
					return null;
				}
			}
			
			// Make sure each node is valid.
			if (nodes.Any(node => !RelativePath.IsValid(node))) {
				if (throwOnError) {
					throw new ArgumentException(Res.CollectionContainsElementWithInvalidCharactersMessage, argumentName);
				}
				return null;
			}
			
			// If the first node is empty, remove it.
			if (nodes.Length > 1 && nodes[0].Length == 0) {
				nodes = nodes.Skip(1).ToArray();
			}

			// If the last node is empty, deal with it properly.
			if (removeEmptyLastNode && nodes.Length > 0) {
				String lastNode = nodes.Last();
				if (lastNode.Length == 0) {
					return nodes.Take(nodes.Length - 1).ToArray();
				}
			}
			
			// Return the validated nodes.
			return nodes;
		}

	// Operators
		/// <summary>
		/// Returns a value indicating if the two objects specified are equal.
		/// </summary>
		/// <param name="objA">The first object.</param>
		/// <param name="objB">The second object.</param>
		/// <returns><c>true</c> if the two objects are equal; otherwise, <c>false</c>.</returns>
		public static Boolean operator ==(RelativePath objA, RelativePath objB) {
			return RelativePath.AreEqual(objA, objB);
		}
		/// <summary>
		/// Returns a value indicating if the two objects specified are equal.
		/// </summary>
		/// <param name="objA">The first object.</param>
		/// <param name="objB">The second object.</param>
		/// <returns><c>true</c> if the two objects are equal; otherwise, <c>false</c>.</returns>
		public static Boolean operator ==(RelativePath objA, String objB) {
			return RelativePath.AreEqual(objA, objB);
		}
		/// <summary>
		/// Returns a value indicating if the two objects specified are equal.
		/// </summary>
		/// <param name="objA">The first object.</param>
		/// <param name="objB">The second object.</param>
		/// <returns><c>true</c> if the two objects are equal; otherwise, <c>false</c>.</returns>
		public static Boolean operator ==(String objA, RelativePath objB) {
			return RelativePath.AreEqual(objB, objA);
		}
		/// <summary>
		/// Returns a value indicating if the two objects specified are not equal.
		/// </summary>
		/// <param name="objA">The first object.</param>
		/// <param name="objB">The second object.</param>
		/// <returns><c>true</c> if the two objects are not equal; otherwise, <c>false</c>.</returns>
		public static Boolean operator !=(RelativePath objA, RelativePath objB) {
			return !RelativePath.AreEqual(objA, objB);
		}
		/// <summary>
		/// Returns a value indicating if the two objects specified are not equal.
		/// </summary>
		/// <param name="objA">The first object.</param>
		/// <param name="objB">The second object.</param>
		/// <returns><c>true</c> if the two objects are not equal; otherwise, <c>false</c>.</returns>
		public static Boolean operator !=(RelativePath objA, String objB) {
			return !RelativePath.AreEqual(objA, objB);
		}
		/// <summary>
		/// Returns a value indicating if the two objects specified are not equal.
		/// </summary>
		/// <param name="objA">The first object.</param>
		/// <param name="objB">The second object.</param>
		/// <returns><c>true</c> if the two objects are not equal; otherwise, <c>false</c>.</returns>
		public static Boolean operator !=(String objA, RelativePath objB) {
			return !RelativePath.AreEqual(objB, objA);
		}
		/// <summary>
		/// Returns a value indicating if an object is less than another object.
		/// </summary>
		/// <param name="objA">The first object.</param>
		/// <param name="objB">The second object.</param>
		/// <returns><c>true</c> if the first object is less than the second object; otherwise, <c>false</c>.</returns>
		public static Boolean operator <(RelativePath objA, RelativePath objB) {
			if (Object.ReferenceEquals(objA, null)) {
				return !Object.ReferenceEquals(objB, null);
			}
			else if (Object.ReferenceEquals(objB, null)) {
				return false;
			}

			return objA.CompareTo(objB) < 0;
		}
		/// <summary>
		/// Returns a value indicating if an object is less than or equal to another object.
		/// </summary>
		/// <param name="objA">The first object.</param>
		/// <param name="objB">The second object.</param>
		/// <returns><c>true</c> if the first object is less than or equal to the second object; otherwise, <c>false</c>.</returns>
		public static Boolean operator <=(RelativePath objA, RelativePath objB) {
			if (Object.ReferenceEquals(objA, null)) {
				return true;
			}
			else if (Object.ReferenceEquals(objB, null)) {
				return false;
			}

			return objA.CompareTo(objB) <= 0;
		}
		/// <summary>
		/// Returns a value indicating if an object is greater than another object.
		/// </summary>
		/// <param name="objA">The first object.</param>
		/// <param name="objB">The second object.</param>
		/// <returns><c>true</c> if the first object is greater than the second object; otherwise, <c>false</c>.</returns>
		public static Boolean operator >(RelativePath objA, RelativePath objB) {
			if (Object.ReferenceEquals(objA, null)) {
				return false;
			}
			else if (Object.ReferenceEquals(objB, null)) {
				return true;
			}

			return objA.CompareTo(objB) > 0;
		}
		/// <summary>
		/// Returns a value indicating if an object is greater than or equal to another object.
		/// </summary>
		/// <param name="objA">The first object.</param>
		/// <param name="objB">The second object.</param>
		/// <returns><c>true</c> if the first object is greater than or equal to the second object; otherwise, <c>false</c>.</returns>
		public static Boolean operator >=(RelativePath objA, RelativePath objB) {
			if (Object.ReferenceEquals(objA, null)) {
				return Object.ReferenceEquals(objB, null);
			}
			else if (Object.ReferenceEquals(objB, null)) {
				return true;
			}

			return objA.CompareTo(objB) >= 0;
		}
		/// <summary>
		/// Implicitly converts a <see cref="T:RelativePath"/> object to a <see cref="T:String"/> object.
		/// </summary>
		/// <param name="instance">The instance to convert.</param>
		/// <returns>The <see cref="T:String"/> that represents the converted object.</returns>
		public static implicit operator String(RelativePath instance) {
			if (instance == null) {
				return null;
			}
			return instance.ToString();
		}
		/// <summary>
		/// Explicitly converts a <see cref="T:String"/> object to a <see cref="T:RelativePath"/> object.
		/// </summary>
		/// <param name="instance">The instance to convert.</param>
		/// <returns>The <see cref="T:RelativePath"/> that represents the converted object.</returns>
		/// <exception cref="System.InvalidCastException">The object cannot be converted.</exception>
		public static explicit operator RelativePath(String instance) {
			if (instance == null) {
				return null;
			}

			Char? inferredSeparator = RelativePath.InferSeparator(instance, "instance", false);
			if (inferredSeparator == null) {
				throw new InvalidCastException();
			}

			Char separator = (Char)inferredSeparator;
			String[] nodes = RelativePath.ValidateNodes(instance.Split(separator), "instance", false, false);
			if (nodes == null) {
				throw new InvalidCastException();
			}

			return new RelativePath(nodes, separator, false, true);
		}

		#region IComparable Members (explicit)
		Int32 IComparable.CompareTo(Object obj) {
			return this.CompareTo(obj as RelativePath);
		}
		#endregion
	}
}
