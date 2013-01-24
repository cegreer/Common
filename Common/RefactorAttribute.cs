using System;

namespace Vizistata {
	/// <summary>
	/// Indicates that the code needs to be refactored in some way.  This class may not be inherited.
	/// </summary>
	[Serializable()]
	[Obsolete("The code needs to be refactored.  See the comments for details.")]
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true, Inherited = false)]
	public sealed class RefactorAttribute : Attribute {
	// Fields
		/// <summary>
		/// Any comments that describe the need for, or type of, refactoring.  This field is read-only.
		/// </summary>
		private String _comments;

	// Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:RefactorAttribute"/> class.
		/// </summary>
		/// <param name="comments">Any comments that describe the need for, or type of, refactoring.</param>
		public RefactorAttribute(String comments)
			: base() {
			this._comments = comments;
		}

	// Properties
		/// <summary>
		/// Gets the comments that describe the need for, or type of, refactoring.
		/// </summary>
		public String Comments {
			get { return this._comments; }
		}
	}
}
