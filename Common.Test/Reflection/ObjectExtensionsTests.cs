using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Vizistata.Mocks;

namespace Vizistata.Reflection {
	/// <summary>
	/// This is a test class for <see cref="T:ObjectExtensions"/> and is intended to contain all <see cref="T:ObjectExtensions"/> Unit Tests.
	///</summary>
	[TestClass()]
	public class ObjectExtensionsTests {
		#region Test Class Implementation
		/// <summary>
		/// Describes the context under which the current test is running.
		/// </summary>
		private TestContext _testContextInstance;

		/// <summary>
		/// Initializes a new instance of the <see cref="T:ObjectExtensionsTests"/> class.
		/// </summary>
		public ObjectExtensionsTests() : base() { }

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

	// Method Tests
		[TestMethod()]
		[Description("GetFieldValue(Object, String) method when 'name' points to a read-only field.")]
		public void ObjectExtensions_Unit_GetFieldValue_NameIsReadOnlyField() {
			String value = "Test";
			MockReflectionTarget<String> instance = new MockReflectionTarget<String>(value);

			String actual = (String)ObjectExtensions.GetFieldValue(instance, "ReadOnlyField");
			Assert.AreEqual(value, actual);
		}
		[TestMethod()]
		[Description("GetFieldValue(Object, String) method when 'name' points to a read/write field.")]
		public void ObjectExtensions_Unit_GetFieldValue_NameIsReadWriteField() {
			String value = "Test";
			MockReflectionTarget<String> instance = new MockReflectionTarget<String>();
			instance.ReadWriteField = value;

			String actual = (String)ObjectExtensions.GetFieldValue(instance, "ReadWriteField");
			Assert.AreEqual(value, actual);
		}
		[TestMethod()]
		[Description("GetFieldValue(Object, String) method when 'instance' is a null reference.")]
		[ExpectedException(typeof(ArgumentNullException))]
		public void ObjectExtensions_Unit_GetFieldValue_InstanceIsNull() {
			MockReflectionTarget<String> instance = null;
			ObjectExtensions.GetFieldValue(instance, "ReadOnlyField");
		}
		[TestMethod()]
		[Description("GetFieldValue(Object, String) method when 'name' is a null reference.")]
		[ExpectedException(typeof(ArgumentNullException))]
		public void ObjectExtensions_Unit_GetFieldValue_NameIsNull() {
			String value = "Test";
			MockReflectionTarget<String> instance = new MockReflectionTarget<String>(value);

			ObjectExtensions.GetFieldValue(instance, null);
		}
		[TestMethod()]
		[Description("GetFieldValue(Object, String) method when 'name' points to a field that does not exist.")]
		[ExpectedException(typeof(MissingMemberException))]
		public void ObjectExtensions_Unit_GetFieldValue_NameDoesNotExist() {
			String value = "Test";
			MockReflectionTarget<String> instance = new MockReflectionTarget<String>(value);

			String actual = (String)ObjectExtensions.GetFieldValue(instance, "DoesNotExist");
			Assert.AreEqual(value, actual);
		}

		[TestMethod()]
		[Description("GetPropertyValue(Object, String) method when 'name' points to a read-only property.")]
		public void ObjectExtensions_Unit_GetPropertyValue_NameIsReadOnlyProperty() {
			String value = "Test";
			MockReflectionTarget<String> instance = new MockReflectionTarget<String>();
			instance.SetReadOnlyPropertyValue(value);

			String actual = (String)ObjectExtensions.GetPropertyValue(instance, "ReadOnlyProperty");
			Assert.AreEqual(value, actual);
		}
		[TestMethod()]
		[Description("GetPropertyValue(Object, String) method when 'name' points to a read/write property.")]
		public void ObjectExtensions_Unit_GetPropertyValue_NameIsReadWriteProperty() {
			String value = "Test";
			MockReflectionTarget<String> instance = new MockReflectionTarget<String>();
			instance.ReadWriteProperty = value;

			String actual = (String)ObjectExtensions.GetPropertyValue(instance, "ReadWriteProperty");
			Assert.AreEqual(value, actual);
		}
		[TestMethod()]
		[Description("GetPropertyValue(Object, String) method when 'name' points to a write-only property.")]
		[ExpectedException(typeof(InvalidOperationException))]
		public void ObjectExtensions_Unit_GetPropertyValue_NameIsWriteOnlyProperty() {
			MockReflectionTarget<String> instance = new MockReflectionTarget<String>();

			ObjectExtensions.GetPropertyValue(instance, "WriteOnlyProperty");
		}
		[TestMethod()]
		[Description("GetPropertyValue(Object, String) method when 'instance' is a null reference.")]
		[ExpectedException(typeof(ArgumentNullException))]
		public void ObjectExtensions_Unit_GetPropertyValue_InstanceIsNull() {
			MockReflectionTarget<String> instance = null;
			ObjectExtensions.GetPropertyValue(instance, "ReadOnlyProperty");
		}
		[TestMethod()]
		[Description("GetPropertyValue(Object, String) method when 'name' is a null reference.")]
		[ExpectedException(typeof(ArgumentNullException))]
		public void ObjectExtensions_Unit_GetPropertyValue_NameIsNull() {
			String value = "Test";
			MockReflectionTarget<String> instance = new MockReflectionTarget<String>(value);

			ObjectExtensions.GetPropertyValue(instance, null);
		}
		[TestMethod()]
		[Description("GetPropertyValue(Object, String) method when 'name' points to a property that does not exist.")]
		[ExpectedException(typeof(MissingMemberException))]
		public void ObjectExtensions_Unit_GetPropertyValue_NameDoesNotExist() {
			String value = "Test";
			MockReflectionTarget<String> instance = new MockReflectionTarget<String>(value);

			String actual = (String)ObjectExtensions.GetPropertyValue(instance, "DoesNotExist");
			Assert.AreEqual(value, actual);
		}
	}
}