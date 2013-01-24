using System;

namespace Vizistata {
	/// <summary>
	/// Indicates that a type is reusable and can be promoted to a common class library or framework.  This class may not be inherited.
	/// </summary>
	[Serializable()]
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Delegate | AttributeTargets.Enum | AttributeTargets.Interface | AttributeTargets.Struct, AllowMultiple = false, Inherited = false)]
	public sealed class ReusableAttribute : Attribute {
	// Fields
		/// <summary>
		/// The name of the approver.  This field is read-only.
		/// </summary>
		private String _approver;
		/// <summary>
		/// Any comments about the reusability.  This field is read-only.
		/// </summary>
		private String _comments;
		/// <summary>
		/// <c>true</c> if the type is reusable; otherwise, <c>false</c>.  This field is read-only.
		/// </summary>
		private readonly Boolean _isReusable;

	// Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:ReusableAttribute"/> class.
		/// </summary>
		/// <param name="isReusable"><c>true</c> if the type is reusable and eligible for promotion; otherwise, <c>false</c>.</param>
		public ReusableAttribute(Boolean isReusable)
			: base() {
			this._isReusable = isReusable;
		}

	// Properties
		/// <summary>
		/// Gets or sets the name of the approver.
		/// </summary>
		public String Approver {
			get { return this._approver; }
			set { this._approver = value; }
		}
		/// <summary>
		/// Gets or sets the comments about the reusability.
		/// </summary>
		public String Comments {
			get { return this._comments; }
			set { this._comments = value; }
		}
		/// <summary>
		/// Gets a value indicating if the type is reusable.
		/// </summary>
		public Boolean IsReusable {
			get { return this._isReusable; }
		}
	}
}
