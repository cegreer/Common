using System;

namespace Vizistata.Configuration {
	/// <summary>
	/// Provides information about the mapping for a configuration section.
	/// </summary>
	public class SectionMappingEventArgs : EventArgs {
	// Fields
		/// <summary>
		/// The path to which the configuration section maps.
		/// </summary>
		private String _path;
		/// <summary>
		/// The type of configuration section.  This field is read-only.
		/// </summary>
		private readonly Type _configurationSectionType;

	// Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:SectionMappingEventArgs"/> class.
		/// </summary>
		/// <param name="configurationSectionType">The type of configuration section.</param>
		public SectionMappingEventArgs(Type configurationSectionType)
			: base() {
			this._configurationSectionType = configurationSectionType;
		}

	// Properties
		/// <summary>
		/// Gets the type of configuration section.
		/// </summary>
		public Type ConfigurationSectionType {
			get { return this._configurationSectionType; }
		}
		/// <summary>
		/// Gets or sets the path to which the configuration section maps.
		/// </summary>
		public String Path {
			get { return this._path; }
			set { this._path = value; }
		}
	}
}
