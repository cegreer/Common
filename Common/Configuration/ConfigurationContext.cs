using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Net.Configuration;

using NameValueCollection = System.Collections.Specialized.NameValueCollection;
using RuntimeHelpers = System.Runtime.CompilerServices.RuntimeHelpers;
using Res = Vizistata.Properties.Resources;

namespace Vizistata.Configuration {
	/// <summary>
	/// Provides contextual information about the configuration of the current application or process.  This class may not be inherited.
	/// </summary>
	public sealed class ConfigurationContext : Object {
		#region private sealed class ConfigurationSectionType : Object, IEquatable<ConfigurationSectionType>, IEquatable<Type> {...}
		/// <summary>
		/// Represents a specific configuration section type.  This class may not be inherited.
		/// </summary>
		private sealed class ConfigurationSectionType : Object, IEquatable<ConfigurationSectionType>, IEquatable<Type> {
		// Fields
			/// <summary>
			/// The actual type represented.  This field is read-only.
			/// </summary>
			private readonly Type _actualType;

		// Constructors
			/// <summary>
			/// Initializes a new instance of the <see cref="T:ConfigurationSectionType"/> class.
			/// </summary>
			/// <param name="type">The configuration section type.</param>
			private ConfigurationSectionType(Type type)
				: base() {
				this._actualType = type;
			}

		// Methods
			/// <summary>
			/// Creates and returns an instance of the <see cref="T:ConfigurationSectionType"/> class.
			/// </summary>
			/// <param name="type">The configuration section type.</param>
			/// <returns>A reference to the <see cref="T:ConfigurationSectionType"/> created.</returns>
			/// <exception cref="System.ArgumentNullException"><paramref name="type"/> is a null reference.</exception>
			/// <exception cref="System.ArgumentException"><paramref name="type"/> does not derive from <see cref="T:ConfigurationSection"/>.
			/// -or- <paramref name="type"/> is abstract.</exception>
			public static ConfigurationSectionType Create(Type type) {
				return ConfigurationSectionType.Create(type, false);
			}
			/// <summary>
			/// Creates and returns an instance of the <see cref="T:ConfigurationSectionType"/> class.
			/// </summary>
			/// <param name="type">The configuration section type.</param>
			/// <param name="suppressErrors"><c>true</c> if a null reference should be returned instead of throwing exceptions if <paramref name="type"/> is invalid; otherwise, <c>false</c>.</param>
			/// <returns>A reference to the <see cref="T:ConfigurationSectionType"/> created, or a null reference if <paramref name="suppressErrors"/> is <c>true</c> and <paramref name="type"/> is invalid.</returns>
			/// <exception cref="System.ArgumentNullException"><paramref name="suppressErrors"/> is <c>false</c>, and <paramref name="type"/> is a null reference.</exception>
			/// <exception cref="System.ArgumentException"><paramref name="suppressErrors"/> is <c>false</c>, and <paramref name="type"/> does not derive from <see cref="T:ConfigurationSection"/>.
			/// -or- <paramref name="suppressErrors"/> is <c>false</c>, and <paramref name="type"/> is abstract.</exception>
			public static ConfigurationSectionType Create(Type type, Boolean suppressErrors) {
				if (type == null) {
					if (!suppressErrors) {
						throw new ArgumentNullException("type");
					}
					return null;
				}
				if (!typeof(ConfigurationSection).IsAssignableFrom(type)) {
					if (!suppressErrors) {
						throw new ArgumentException(Res.TypeNeedsToHaveSpecificBaseClassFormat.FormatInvariant(typeof(ConfigurationSection).FullName), "type");
					}
					return null;
				}
				if (type.IsAbstract) {
					if (!suppressErrors) {
						throw new ArgumentException(Res.TypeMayNotBeAbstractMessage, "type");
					}
					return null;
				}
				return new ConfigurationSectionType(type);
			}
			/// <summary>
			/// Returns a value indicating if this instance is equal to the object specified.
			/// </summary>
			/// <param name="obj">The object to compare to this instance.</param>
			/// <returns><c>true</c> if the object specified is equal to this instance; otherwise, <c>false</c>.</returns>
			public override Boolean Equals(Object obj) {
				return this.Equals(obj as ConfigurationSectionType)
					|| this.Equals(obj as Type);
			}
			/// <summary>
			/// Returns a value indicating if this instance is equal to the object specified.
			/// </summary>
			/// <param name="other">The object to compare to this instance.</param>
			/// <returns><c>true</c> if the object specified is equal to this instance; otherwise, <c>false</c>.</returns>
			public Boolean Equals(ConfigurationSectionType other) {
				if (other == null) {
					return false;
				}
				return Object.Equals(this._actualType, other._actualType);
			}
			/// <summary>
			/// Returns a value indicating if this instance is equal to the object specified.
			/// </summary>
			/// <param name="other">The object to compare to this instance.</param>
			/// <returns><c>true</c> if the object specified is equal to this instance; otherwise, <c>false</c>.</returns>
			public Boolean Equals(Type other) {
				if (other == null) {
					return false;
				}
				return Object.Equals(this._actualType, other);
			}
			/// <summary>
			/// Returns a value that serves as a hashed value for this instance.
			/// </summary>
			/// <returns>The hash code value for this instance.</returns>
			public override Int32 GetHashCode() {
				return this._actualType.GetHashCode();
			}
			/// <summary>
			/// Returns the string representation of this instance.
			/// </summary>
			/// <returns>The string representation of this instance.</returns>
			public override String ToString() {
				return this._actualType.ToString();
			}

		// Operators
			/// <summary>
			/// Implicitly converts a <see cref="T:ConfigurationSectionType"/> object to a <see cref="T:Type"/> object.
			/// </summary>
			/// <param name="instance">The object to convert.</param>
			/// <returns>A reference to the <see cref="T:Type"/> created.</returns>
			public static implicit operator Type(ConfigurationSectionType instance) {
				if (instance == null) {
					return null;
				}
				return instance._actualType;
			}
			/// <summary>
			/// Implicitly converts a <see cref="T:Type"/> object to a <see cref="T:ConfigurationSectionType"/> object.
			/// </summary>
			/// <param name="type">The object to convert.</param>
			/// <returns>A reference to the <see cref="T:ConfigurationSectionType"/> created.</returns>
			/// <exception cref="System.InvalidCastException"><paramref name="type"/> is not convertible to <see cref="T:ConfigurationSectionType"/>.</exception>
			public static explicit operator ConfigurationSectionType(Type type) {
				ConfigurationSectionType instance = ConfigurationSectionType.Create(type, true);
				if (instance == null) {
					throw new InvalidCastException();
				}
				return instance;
			}
		}
		#endregion

	// Fields
		/// <summary>
		/// The cache for the appSettings section.
		/// </summary>
		private NameValueCollection _appSettings;
		/// <summary>
		/// <c>true</c> if information should be cached; otherwise, <c>false</c>.  This field is read-only.
		/// </summary>
		private readonly Boolean _cacheInformation;
		/// <summary>
		/// The cache for the connectionStrings section.
		/// </summary>
		private ConnectionStringSettingsCollection _connectionStrings;
		/// <summary>
		/// The default instance.  This field is read-only.
		/// </summary>
		private static readonly ConfigurationContext _default = new ConfigurationContext(true);
		/// <summary>
		/// Represents the <see cref="E:SectionMappingUnknown"/> event.  This field is read-only.
		/// </summary>
		private readonly EventObject<SectionMappingEventArgs> _sectionMappingUnknownEvent = new EventObject<SectionMappingEventArgs>();
		/// <summary>
		/// The collection of paths for the sections keyed by the type of the section.  This field is read-only.
		/// </summary>
		private readonly IDictionary<ConfigurationSectionType, String> _sectionPaths = new Dictionary<ConfigurationSectionType, String>() {
			{ (ConfigurationSectionType)typeof(SmtpSection), "system.net/mailSettings/smtp" }
		};
		/// <summary>
		/// The cache for the configuration sections.
		/// </summary>
		private readonly IDictionary<RelativePath, ConfigurationSection> _sections = new Dictionary<RelativePath, ConfigurationSection>();

	// Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:ConfigurationContext"/> class.
		/// </summary>
		public ConfigurationContext() : this(false) { }
		/// <summary>
		/// Initializes a new instance of the <see cref="T:ConfigurationContext"/> class.
		/// </summary>
		/// <param name="cacheInformation"><c>true</c> if information should be cached; otherwise, <c>false</c>.</param>
		public ConfigurationContext(Boolean cacheInformation)
			: base() {
			this._cacheInformation = cacheInformation;
		}

	// Properties
		/// <summary>
		/// Gets the key-value pairs for the appSettings section.
		/// </summary>
		/// <exception cref="System.Configuration.ConfigurationErrorsException">A <see cref="T:NameValueCollection"/> object could not be created with the application settings data.</exception>
		public NameValueCollection AppSettings {
			get {
				if (!this._cacheInformation) {
					return ConfigurationManager.AppSettings;
				}

				if (this._appSettings == null) {
					this._appSettings = ConfigurationManager.AppSettings;
				}
				return this._appSettings;
			}
		}
		/// <summary>
		/// Gets the connection strings in the connectionStrings section.
		/// </summary>
		/// <exception cref="System.Configuration.ConfigurationErrorsException">A <see cref="T:ConnectionStringSettingsCollection"/> object could not be retrieved.</exception>
		public ConnectionStringSettingsCollection ConnectionStrings {
			get {
				if (!this._cacheInformation) {
					return ConfigurationManager.ConnectionStrings;
				}

				if (this._connectionStrings == null) {
					this._connectionStrings = ConfigurationManager.ConnectionStrings;
				}
				return this._connectionStrings;
			}
		}
		/// <summary>
		/// Gets the default instance for the <see cref="T:ConfigurationContext"/> class.
		/// </summary>
		public static ConfigurationContext Default {
			get { return ConfigurationContext._default; }
		}

	// Events
		/// <summary>
		/// Occurs when a section mapping is unknown.
		/// </summary>
		/// <exception cref="System.ArgumentNullException">The event handler delegate specified is a null reference.</exception>
		public event EventHandler<SectionMappingEventArgs> SectionMappingUnknown {
			add { this._sectionMappingUnknownEvent.AddHandler(value); }
			remove { this._sectionMappingUnknownEvent.RemoveHandler(value); }
		}

	// Methods
		/// <summary>
		/// Adds a path to the list of known paths for the configuration section type specified.
		/// </summary>
		/// <typeparam name="T">Specifies the type of the configuration section.</typeparam>
		/// <param name="path">The path of the configuration section.</param>
		/// <exception cref="System.ArgumentNullException"><paramref name="path"/> is a null reference.</exception>
		/// <exception cref="System.ArgumentException"><paramref name="path"/> is not a valid configuration path.
		/// -or- <typeparamref name="T"/> is already known.</exception>
		[SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "The design allows for non-generic method calls.")]
		public void AddPath<T>(String path) where T : ConfigurationSection {
			ConfigurationSectionType sectionType = ConfigurationSectionType.Create(typeof(T));
			RelativePath relativePath = new RelativePath(path, RelativePath.ForwardSlash);
			this.AddSectionPath(sectionType, relativePath);
		}
		/// <summary>
		/// Adds a path to the list of known paths for the configuration section type specified.
		/// </summary>
		/// <param name="type">The type of the configuration section.</param>
		/// <param name="path">The path of the configuration section.</param>
		/// <exception cref="System.ArgumentNullException"><paramref name="type"/> is a null reference.
		/// -or- <paramref name="path"/> is a null reference.</exception>
		/// <exception cref="System.ArgumentException"><paramref name="type"/> does not derive from <see cref="T:System.Configuration.ConfigurationSection"/>.
		/// -or- <paramref name="path"/> is not a valid configuration path.
		/// -or- <paramref name="type"/> is already known.</exception>
		public void AddPath(Type type, String path) {
			ConfigurationSectionType sectionType = ConfigurationSectionType.Create(type);
			RelativePath relativePath = new RelativePath(path, RelativePath.ForwardSlash);
			this.AddSectionPath(sectionType, relativePath);
		}
		/// <summary>
		/// Adds a section path to the list of known paths.
		/// </summary>
		/// <param name="type">The type of the configuration section.</param>
		/// <param name="path">The path to the configuration section.</param>
		/// <exception cref="System.ArgumentException">The type is already known.</exception>
		private void AddSectionPath(ConfigurationSectionType type, RelativePath path) {
			Debug.Assert(type != null);
			Debug.Assert(path != null);

			if (this._sectionPaths.ContainsKey(type)) {
				throw new ArgumentException(Res.PathForTypeAlreadyKnownMessage);
			}

			this._sectionPaths[type] = path;
		}
		/// <summary>
		/// Returns the known configuration section specified.
		/// </summary>
		/// <typeparam name="T">The type of configuration section to return.  If the configuration section type specified is not known, an exception will be thrown.</typeparam>
		/// <returns>The configuration section object found, or a null reference if it does not exist in the configuration file(s).</returns>
		/// <exception cref="System.InvalidOperationException">The configuration section is not known.</exception>
		/// <exception cref="System.Configuration.ConfigurationErrorsException">A configuration file could not be loaded.</exception>
		/// <exception cref="System.InvalidCastException">The configuration section is mapped to a path that is incorrect in the configuration file(s).</exception>
		public T GetSection<T>() where T : ConfigurationSection {
			if (!this.IsPathKnown<T>()) {
				RuntimeHelpers.RunClassConstructor(typeof(T).TypeHandle);
				SectionMappingEventArgs e = new SectionMappingEventArgs(typeof(T));
				this.OnSectionMappingUnknown(e);

				if (e.Path == null) {
					throw new InvalidOperationException(Res.ConfigurationSectionTypeNotKnownMessage);
				}
				this.AddPath<T>(e.Path);
			}
			ConfigurationSectionType sectionType = ConfigurationSectionType.Create(typeof(T));
			return (T)this.GetSection(this._sectionPaths[sectionType]);
		}
		/// <summary>
		/// Returns the known configuration section specified.
		/// </summary>
		/// <param name="type">The type of configuration section to return.  If the configuration section type specified is not known, an exception will be thrown.</param>
		/// <returns>The configuration section object found, or a null reference if it does not exist in the configuration file(s).</returns>
		/// <exception cref="System.ArgumentNullException"><paramref name="type"/> is a null reference.</exception>
		/// <exception cref="System.ArgumentException"><paramref name="type"/> does not derive from <see cref="T:ConfigurationSection"/>.
		/// -or- <paramref name="type"/> is abstract.</exception>
		/// <exception cref="System.InvalidOperationException">The configuration section is not known.</exception>
		/// <exception cref="System.Configuration.ConfigurationErrorsException">A configuration file could not be loaded.</exception>
		/// <exception cref="System.InvalidCastException">The configuration section is mapped to a path that is incorrect in the configuration file(s).</exception>
		public ConfigurationSection GetSection(Type type) {
			if (type == null) {
				throw new ArgumentNullException("type");
			}

			ConfigurationSectionType sectionType = ConfigurationSectionType.Create(type);
			if (!this.IsPathKnown(sectionType)) {
				RuntimeHelpers.RunClassConstructor(type.TypeHandle);
				SectionMappingEventArgs e = new SectionMappingEventArgs(type);
				this.OnSectionMappingUnknown(e);

				if (e.Path == null) {
					throw new InvalidOperationException(Res.ConfigurationSectionTypeNotKnownMessage);
				}
				this.AddPath(type, e.Path);
			}
			return (ConfigurationSection)this.GetSection(this._sectionPaths[sectionType]);
		}
		/// <summary>
		/// Returns the configuration section specified.
		/// </summary>
		/// <typeparam name="T">The type of configuration section to return.</typeparam>
		/// <param name="path">The path to the configuration section.</param>
		/// <returns>The configuration section object found, or a null reference if it does not exist in the configuration file(s).</returns>
		/// <exception cref="System.ArgumentNullException"><paramref name="path"/> is a null reference.</exception>
		/// <exception cref="System.ArgumentException"><paramref name="path"/> is invalid.</exception>
		/// <exception cref="System.Configuration.ConfigurationErrorsException">A configuration file could not be loaded.</exception>
		/// <exception cref="System.InvalidCastException">The configuration section is mapped to a path that is incorrect in the configuration file(s).</exception>
		public T GetSection<T>(String path) where T : ConfigurationSection {
			return (T)this.GetSection(path);
		}
		/// <summary>
		/// Returns the configuration section specified.
		/// </summary>
		/// <param name="path">The path to the configuration section.</param>
		/// <returns>The configuration section object found, or a null reference if it does not exist in the configuration file(s).</returns>
		/// <exception cref="System.ArgumentNullException"><paramref name="path"/> is a null reference.</exception>
		/// <exception cref="System.ArgumentException"><paramref name="path"/> is invalid.</exception>
		/// <exception cref="System.Configuration.ConfigurationErrorsException">A configuration file could not be loaded.</exception>
		/// <exception cref="System.InvalidCastException">The configuration section is mapped to a path that is incorrect in the configuration file(s).</exception>
		public ConfigurationSection GetSection(String path) {
			RelativePath relativePath = new RelativePath(path, RelativePath.ForwardSlash);

			if (!this._cacheInformation) {
				return (ConfigurationSection)ConfigurationManager.GetSection(relativePath);
			}

			if (!this._sections.ContainsKey(relativePath)) {
				this._sections[relativePath] = (ConfigurationSection)ConfigurationManager.GetSection(relativePath);
			}
			return this._sections[relativePath];
		}
		/// <summary>
		/// Returns a value indicating if the path is known for the configuration section type specified.
		/// </summary>
		/// <typeparam name="T">The type of configuration section.</typeparam>
		/// <returns><c>true</c> if the configuration section type's path is known; otherwise, <c>false</c>.</returns>
		[SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "The design allows for non-generic method calls as well.")]
		public Boolean IsPathKnown<T>() where T : ConfigurationSection {
			ConfigurationSectionType sectionType = ConfigurationSectionType.Create(typeof(T), true);
			return this.IsPathKnown(sectionType);
		}
		/// <summary>
		/// Returns a value indicating if the path is known for the configuration section type specified.
		/// </summary>
		/// <param name="type">The type of configuration section.</param>
		/// <returns><c>true</c> if the configuration section type's path is known; otherwise, <c>false</c>.</returns>
		public Boolean IsPathKnown(Type type) {
			ConfigurationSectionType sectionType = ConfigurationSectionType.Create(type, true);
			return this.IsPathKnown(sectionType);
		}
		/// <summary>
		/// Returns a value indicating if the path is known for the configuration section type specified.
		/// </summary>
		/// <param name="sectionType">The type of configuration section.</param>
		/// <returns><c>true</c> if the configuration section type's path is known; otherwise, <c>false</c>.</returns>
		private Boolean IsPathKnown(ConfigurationSectionType sectionType) {
			if (sectionType == null) {
				return false;
			}
			return this._sectionPaths.ContainsKey(sectionType);
		}
		/// <summary>
		/// Raises the <see cref="E:SectionMappingUnknown"/> event.
		/// </summary>
		/// <param name="e">Provides information about the event.</param>
		private void OnSectionMappingUnknown(SectionMappingEventArgs e) {
			this._sectionMappingUnknownEvent.RaiseEvent(this, e);
		}
	}
}
