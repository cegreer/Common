using System;
using System.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Vizistata.Mocks;

using NameValueCollection = System.Collections.Specialized.NameValueCollection;

namespace Vizistata.Configuration {
	/// <summary>
	/// This is a test class for <see cref="T:ConfigurationContext"/> and is intended to contain all <see cref="T:ConfigurationContext"/> Unit Tests.
	///</summary>
	[TestClass()]
	public class ConfigurationContextTests {
		#region Test Class Implementation
		/// <summary>
		/// Describes the context under which the current test is running.
		/// </summary>
		private TestContext _testContextInstance;

		/// <summary>
		/// Initializes a new instance of the <see cref="T:ConfigurationContextTests"/> class.
		/// </summary>
		public ConfigurationContextTests() : base() { }

		/// <summary>
		/// Gets or sets the test context which provides information about and functionality for the current test run.
		///</summary>
		public TestContext TestContext {
			get { return this._testContextInstance; }
			set { this._testContextInstance = value; }
		}
		#region Additional test attributes
		// 
		//You can use the following additional attributes as you write your tests:
		//
		//Use ClassInitialize to run code before running the first test in the class
		//
		//[ClassInitialize()]
		//public static void MyClassInitialize(TestContext testContext)
		//{
		//}
		//
		//Use ClassCleanup to run code after all tests in a class have run
		//
		//[ClassCleanup()]
		//public static void MyClassCleanup()
		//{
		//}
		//
		//Use TestInitialize to run code before running each test
		//
		//[TestInitialize()]
		//public void MyTestInitialize()
		//{
		//}
		//
		//Use TestCleanup to run code after each test has run
		//
		//[TestCleanup()]
		//public void MyTestCleanup()
		//{
		//}
		//
		#endregion
		#endregion

	// Constructor Tests
		[TestMethod()]
		[Description(".ctor() constructor for the optimal path.")]
		public void ConfigurationContext_Unit_Constructor1_Optimal() {
			new ConfigurationContext();
		}

		[TestMethod()]
		[Description(".ctor(Boolean) constructor when 'cacheInformation' is true.")]
		public void ConfigurationContext_Unit_Constructor2_CacheInformationIsTrue() {
			Boolean cacheInformation = true;
			new ConfigurationContext(cacheInformation);
		}
		[TestMethod()]
		[Description(".ctor(Boolean) constructor when 'cacheInformation' is false.")]
		public void ConfigurationContext_Unit_Constructor2_CacheInformationIsFalse() {
			Boolean cacheInformation = false;
			new ConfigurationContext(cacheInformation);
		}

	// Property Tests
		[TestMethod()]
		[Description("AppSettings property for the optimal path.")]
		public void ConfigurationContext_Unit_AppSettings_Optimal() {
			ConfigurationContext target = new ConfigurationContext();

			NameValueCollection actual = target.AppSettings;
			Assert.IsNotNull(actual);
		}
		[TestMethod()]
		[Description("AppSettings property when caching is enabled but the values are not cached.")]
		public void ConfigurationContext_Unit_AppSettings_NotCached() {
			ConfigurationContext target = new ConfigurationContext(true);

			NameValueCollection actual = target.AppSettings;
			Assert.IsNotNull(actual);
		}

		[TestMethod()]
		[Description("ConnectionStrings property for the optimal path.")]
		public void ConfigurationContext_Unit_ConnectionStrings_Optimal() {
			ConfigurationContext target = new ConfigurationContext();

			ConnectionStringSettingsCollection actual = target.ConnectionStrings;
			Assert.IsNotNull(actual);
		}
		[TestMethod()]
		[Description("ConnectionStrings property when caching is enabled but the values are not cached.")]
		public void ConfigurationContext_Unit_ConnectionStrings_NotCached() {
			ConfigurationContext target = new ConfigurationContext(true);

			ConnectionStringSettingsCollection actual = target.ConnectionStrings;
			Assert.IsNotNull(actual);
		}

		[TestMethod()]
		[Description("Default property for the optimal path.")]
		public void ConfigurationContext_Unit_Default_Optimal() {
			ConfigurationContext actual = ConfigurationContext.Default;
			Assert.IsNotNull(actual);
		}

	// Method Tests
		[TestMethod()]
		[Description("AddPath<T>(String) method for the optimal path.")]
		public void ConfigurationContext_Unit_AddPath1_Optimal() {
			ConfigurationContext target = new ConfigurationContext();

			String path = "vizistata.diagnostics/test";
			target.AddPath<MockConfigurationSection>(path);
		}
		[TestMethod()]
		[Description("AddPath<T>(String) method when 'path' is a null reference.")]
		[ExpectedException(typeof(ArgumentNullException))]
		public void ConfigurationContext_Unit_AddPath1_PathIsNull() {
			ConfigurationContext target = new ConfigurationContext();

			String path = null;
			target.AddPath<MockConfigurationSection>(path);
		}
		[TestMethod()]
		[Description("AddPath<T>(String) method when 'path' is invalid.")]
		[ExpectedException(typeof(ArgumentException))]
		public void ConfigurationContext_Unit_AddPath1_PathIsInvalid() {
			ConfigurationContext target = new ConfigurationContext();

			String path = "\\This is / not a valid path!";
			target.AddPath<MockConfigurationSection>(path);
		}
		[TestMethod()]
		[Description("AddPath<T>(String) method when 'T' is already known.")]
		[ExpectedException(typeof(ArgumentException))]
		public void ConfigurationContext_Unit_AddPath1_TIsKnown() {
			ConfigurationContext target = new ConfigurationContext();

			String path = "vizistata.diagnostics/test";
			target.AddPath<MockConfigurationSection>(path);
			target.AddPath<MockConfigurationSection>(path);
		}

		[TestMethod()]
		[Description("AddPath(Type, String) method for the optimal path.")]
		public void ConfigurationContext_Unit_AddPath2_Optimal() {
			ConfigurationContext target = new ConfigurationContext();

			Type type = typeof(MockConfigurationSection);
			String path = "vizistata.diagnostics/test";
			target.AddPath(type, path);
		}
		[TestMethod()]
		[Description("AddPath(Type, String) method when 'type' is a null reference.")]
		[ExpectedException(typeof(ArgumentNullException))]
		public void ConfigurationContext_Unit_AddPath2_TypeIsNull() {
			ConfigurationContext target = new ConfigurationContext();

			Type type = null;
			String path = "vizistata.diagnostics/test";
			target.AddPath(type, path);
		}
		[TestMethod()]
		[Description("AddPath(Type, String) method when 'path' is a null reference.")]
		[ExpectedException(typeof(ArgumentNullException))]
		public void ConfigurationContext_Unit_AddPath2_PathIsNull() {
			ConfigurationContext target = new ConfigurationContext();

			Type type = typeof(MockConfigurationSection);
			String path = null;
			target.AddPath(type, path);
		}
		[TestMethod()]
		[Description("AddPath(Type, String) method when 'type' has an invalid base type.")]
		[ExpectedException(typeof(ArgumentException))]
		public void ConfigurationContext_Unit_AddPath2_TypeHasInvalidBaseType() {
			ConfigurationContext target = new ConfigurationContext();

			Type type = typeof(String);
			String path = "vizistata.diagnostics/test";
			target.AddPath(type, path);
		}
		[TestMethod()]
		[Description("AddPath(Type, String) method when 'path' is invalid..")]
		[ExpectedException(typeof(ArgumentException))]
		public void ConfigurationContext_Unit_AddPath2_PathIsInvalid() {
			ConfigurationContext target = new ConfigurationContext();

			Type type = typeof(MockConfigurationSection);
			String path = "\\This is / not a valid path!";
			target.AddPath(type, path);
		}
		[TestMethod()]
		[Description("AddPath(Type, String) method when 'type' is already known.")]
		[ExpectedException(typeof(ArgumentException))]
		public void ConfigurationContext_Unit_AddPath2_TypeIsKnown() {
			ConfigurationContext target = new ConfigurationContext();

			Type type = typeof(MockConfigurationSection);
			String path = "vizistata.diagnostics/test";
			target.AddPath(type, path);
			target.AddPath(type, path);
		}

		[TestMethod()]
		[Description("GetSection<T>() method for the optimal path.")]
		public void ConfigurationContext_System_GetSection1_Optimal() {
			ConfigurationContext target = new ConfigurationContext();

			String path = "vizistata.diagnostics/test";
			target.AddPath<MockConfigurationSection>(path);

			MockConfigurationSection actual = target.GetSection<MockConfigurationSection>();
			Assert.IsNotNull(actual);
		}
		[TestMethod()]
		[Description("GetSection<T>() method when 'T' is not known.")]
		[ExpectedException(typeof(InvalidOperationException))]
		public void ConfigurationContext_Unit_GetSection1_TIsNotKnown() {
			ConfigurationContext target = new ConfigurationContext();

			target.GetSection<MockConfigurationSection>();
		}
		[TestMethod()]
		[Description("GetSection<T>() method when 'T' has a path that is mapped to another type.")]
		[ExpectedException(typeof(InvalidCastException))]
		public void ConfigurationContext_System_GetSection1_TIsMappedToAnotherType() {
			ConfigurationContext target = new ConfigurationContext();

			String path = "vizistata.diagnostics/test";
			target.AddPath<System.Net.Configuration.AuthenticationModulesSection>(path);

			target.GetSection<System.Net.Configuration.AuthenticationModulesSection>();
		}

		[TestMethod()]
		[Description("GetSection(Type) method for the optimal path.")]
		public void ConfigurationContext_System_GetSection2_Optimal() {
			ConfigurationContext target = new ConfigurationContext();

			String path = "vizistata.diagnostics/test";
			target.AddPath<MockConfigurationSection>(path);

			Type type = typeof(MockConfigurationSection);
			ConfigurationSection actual = target.GetSection(type);
			Assert.IsNotNull(actual);
		}
		[TestMethod()]
		[Description("GetSection(Type) method when 'type' is a null reference.")]
		[ExpectedException(typeof(ArgumentNullException))]
		public void ConfigurationContext_Unit_GetSection2_TypeIsNull() {
			ConfigurationContext target = new ConfigurationContext();
			
			Type type = null;
			target.GetSection(type);
		}
		[TestMethod()]
		[Description("GetSection(Type) method when 'type' has an invalid base type.")]
		[ExpectedException(typeof(ArgumentException))]
		public void ConfigurationContext_Unit_GetSection2_TypeHasInvalidBaseType() {
			ConfigurationContext target = new ConfigurationContext();

			Type type = typeof(Object);
			target.GetSection(type);
		}
		[TestMethod()]
		[Description("GetSection(Type) method when 'type' is abstract.")]
		[ExpectedException(typeof(ArgumentException))]
		public void ConfigurationContext_Unit_GetSection2_TypeIsAbstract() {
			ConfigurationContext target = new ConfigurationContext();

			Type type = typeof(ConfigurationSection);
			target.GetSection(type);
		}
		[TestMethod()]
		[Description("GetSection(Type) method when 'type' is not known.")]
		[ExpectedException(typeof(InvalidOperationException))]
		public void ConfigurationContext_Unit_GetSection2_TypeIsNotKnown() {
			ConfigurationContext target = new ConfigurationContext();

			Type type = typeof(MockConfigurationSection);
			target.GetSection(type);
		}

		[TestMethod()]
		[Description("GetSection<T>(String) method for the optimal path.")]
		public void ConfigurationContext_System_GetSection3_Optimal() {
			ConfigurationContext target = new ConfigurationContext();

			String path = "vizistata.diagnostics/test";
			MockConfigurationSection actual = target.GetSection<MockConfigurationSection>(path);
			Assert.IsNotNull(actual);
		}
		[TestMethod()]
		[Description("GetSection<T>(String) method when 'path' is a null reference.")]
		[ExpectedException(typeof(ArgumentNullException))]
		public void ConfigurationContext_System_GetSection3_PathIsNull() {
			ConfigurationContext target = new ConfigurationContext();

			String path = null;
			target.GetSection<MockConfigurationSection>(path);
		}
		[TestMethod()]
		[Description("GetSection<T>(String) method when 'path' is invalid.")]
		[ExpectedException(typeof(ArgumentException))]
		public void ConfigurationContext_System_GetSection3_PathIsInvalid() {
			ConfigurationContext target = new ConfigurationContext();

			String path = "\\This is / not a valid path!";
			target.GetSection<MockConfigurationSection>(path);
		}
		[TestMethod()]
		[Description("GetSection<T>(String) method when 'path' is mapped to another type.")]
		[ExpectedException(typeof(InvalidCastException))]
		public void ConfigurationContext_System_GetSection3_PathIsMappedToAnotherType() {
			ConfigurationContext target = new ConfigurationContext();

			String path = "vizistata.diagnostics/test";
			target.GetSection<System.Net.Configuration.AuthenticationModulesSection>(path);
		}

		[TestMethod()]
		[Description("GetSection(String) method for the optimal path.")]
		public void ConfigurationContext_System_GetSection4_Optimal() {
			ConfigurationContext target = new ConfigurationContext();

			String path = "vizistata.diagnostics/test";
			ConfigurationSection actual = target.GetSection(path);
			Assert.IsNotNull(actual);
		}
		[TestMethod()]
		[Description("GetSection(String) method when 'path' is a null reference.")]
		[ExpectedException(typeof(ArgumentNullException))]
		public void ConfigurationContext_System_GetSection4_PathIsNull() {
			ConfigurationContext target = new ConfigurationContext();

			String path = null;
			target.GetSection(path);
		}
		[TestMethod()]
		[Description("GetSection(String) method when 'path' is invalid.")]
		[ExpectedException(typeof(ArgumentException))]
		public void ConfigurationContext_System_GetSection4_PathIsInvalid() {
			ConfigurationContext target = new ConfigurationContext();

			String path = "\\This is / not a valid path!";
			target.GetSection(path);
		}
		[TestMethod()]
		[Description("GetSection(String) method when caching is enabled but the section is not cached.")]
		public void ConfigurationContext_System_GetSection4_ConfigurationSectionNotCached() {
			ConfigurationContext target = new ConfigurationContext(true);

			String path = "vizistata.diagnostics/test";
			ConfigurationSection actual = target.GetSection(path);
			Assert.IsNotNull(actual);
		}

		[TestMethod()]
		[Description("IsPathKnown<T>() method when the path is known.")]
		public void ConfigurationContext_Unit_IsPathKnown1_True() {
			ConfigurationContext target = new ConfigurationContext();

			String path = "vizistata.diagnostics/test";
			target.AddPath<MockConfigurationSection>(path);

			Boolean expected = true;
			Boolean actual = target.IsPathKnown<MockConfigurationSection>();
			Assert.AreEqual(expected, actual);
		}
		[TestMethod()]
		[Description("IsPathKnown<T>() method when the path is not known.")]
		public void ConfigurationContext_Unit_IsPathKnown1_False() {
			ConfigurationContext target = new ConfigurationContext();

			Boolean expected = false;
			Boolean actual = target.IsPathKnown<MockConfigurationSection>();
			Assert.AreEqual(expected, actual);
		}

		[TestMethod()]
		[Description("IsPathKnown(Type) method when the path is known.")]
		public void ConfigurationContext_Unit_IsPathKnown2_True() {
			ConfigurationContext target = new ConfigurationContext();

			String path = "vizistata.diagnostics/test";
			target.AddPath<MockConfigurationSection>(path);

			Boolean expected = true;
			Type type = typeof(MockConfigurationSection);
			Boolean actual = target.IsPathKnown(type);
			Assert.AreEqual(expected, actual);
		}
		[TestMethod()]
		[Description("IsPathKnown(Type) method when the path is not known.")]
		public void ConfigurationContext_Unit_IsPathKnown2_False() {
			ConfigurationContext target = new ConfigurationContext();

			Boolean expected = false;
			Type type = typeof(MockConfigurationSection);
			Boolean actual = target.IsPathKnown(type);
			Assert.AreEqual(expected, actual);
		}
		[TestMethod()]
		[Description("IsPathKnown(Type) method when 'type' is a null reference.")]
		public void ConfigurationContext_Unit_IsPathKnown2_TypeIsNull() {
			ConfigurationContext target = new ConfigurationContext();

			Boolean expected = false;
			Type type = null;
			Boolean actual = target.IsPathKnown(type);
			Assert.AreEqual(expected, actual);
		}
	}
}
