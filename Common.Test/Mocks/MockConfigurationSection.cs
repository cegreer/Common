using System;
using System.Configuration;

namespace Vizistata.Mocks {
	/// <summary>
	/// Represents a mock configuration section.  This class may not be inherited.
	/// </summary>
	internal sealed class MockConfigurationSection : ConfigurationSection {
	// Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:MockConfigurationSection"/> class.
		/// </summary>
		public MockConfigurationSection() : base() { }
	}
}
