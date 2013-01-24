using System;

namespace Vizistata {
	/// <summary>
	/// Represents the various well-known Multipurpose Internet Mail Extensions (MIME) types.  This class may not be inherited.
	/// </summary>
	public static class MimeTypes {
	// Properties
		/// <summary>
		/// Represents HTML text.  This field is read-only.
		/// </summary>
		public static readonly String Html = "text/html";
		/// <summary>
		/// Represents plain text.  This field is read-only.
		/// </summary>
		public static readonly String Plaintext = "text/plain";
		/// <summary>
		/// Represents the Rich Site Summary (RSS) XML format.  This field is read-only.
		/// </summary>
		public static readonly String RichSiteSummary = "application/rss+xml";
	}
}
