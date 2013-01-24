using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using NameValueCollection = System.Collections.Specialized.NameValueCollection;

namespace Vizistata.Linq {
	/// <summary>
	/// This is a test class for <see cref="T:NameValueCollectionExtensions"/> and is intended to contain all <see cref="T:NameValueCollectionExtensions"/> Unit Tests.
	///</summary>
	[TestClass()]
	public class NameValueCollectionExtensionsTests {
		#region Test Class Implementation
		/// <summary>
		/// Describes the context under which the current test is running.
		/// </summary>
		private TestContext _testContextInstance;

		/// <summary>
		/// Initializes a new instance of the <see cref="T:NameValueCollectionExtensionsTests"/> class.
		/// </summary>
		public NameValueCollectionExtensionsTests() : base() { }

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
		[Description("GetAndRemove(NameValueCollection, String) method when 'name' exists.")]
		public void NameValueCollectionExtensions_Unit_GetAndRemove_NameExists() {
			String name = "Name";
			String value = "Value";
			NameValueCollection instance = new NameValueCollection() {
				{ name, value }
			};
			CollectionAssert.Contains(instance.AllKeys, name);

			String actualValue = NameValueCollectionExtensions.GetAndRemove(instance, name);
			Assert.AreEqual(value, actualValue);
			CollectionAssert.DoesNotContain(instance.AllKeys, name);
		}
		[TestMethod()]
		[Description("GetAndRemove(NameValueCollection, String) method when 'name' does not exist.")]
		public void NameValueCollectionExtensions_Unit_GetAndRemove_NameDoesNotExist() {
			String name = "Name";
			String name1 = "Name1";
			String value = "Value";
			NameValueCollection instance = new NameValueCollection() {
				{ name1, value }
			};
			CollectionAssert.DoesNotContain(instance.AllKeys, name);

			String actualValue = NameValueCollectionExtensions.GetAndRemove(instance, name);
			Assert.IsNull(actualValue);
			CollectionAssert.DoesNotContain(instance.AllKeys, name);
			CollectionAssert.Contains(instance.AllKeys, name1);
		}
		[TestMethod()]
		[Description("GetAndRemove(NameValueCollection, String) method when 'instance' is a null reference.")]
		[ExpectedException(typeof(ArgumentNullException))]
		public void NameValueCollectionExtensions_Unit_GetAndRemove_InstanceNull() {
			String name = "Name";

			NameValueCollectionExtensions.GetAndRemove(null, name);
		}
		[TestMethod()]
		[Description("GetAndRemove(NameValueCollection, String) method when 'name' is a null reference.")]
		[ExpectedException(typeof(ArgumentNullException))]
		public void NameValueCollectionExtensions_Unit_GetAndRemove_NameNull() {
			String name = "Name";
			String value = "Value";
			NameValueCollection instance = new NameValueCollection() {
				{ name, value }
			};

			NameValueCollectionExtensions.GetAndRemove(instance, null);
		}

		[TestMethod()]
		[Description("GetBoolean(NameValueCollection, String, Boolean) method for the optimal path.")]
		public void NameValueCollectionExtensions_Unit_GetBoolean_Optimal() {
			String name = "Name";
			Boolean value = false;
			NameValueCollection collection = new NameValueCollection() {
				{ name, value.ToString() } };
			Boolean defaultValue = true;

			Boolean actual = NameValueCollectionExtensions.GetBoolean(collection, name, defaultValue);
			Boolean expected = value;
			Assert.AreEqual(expected, actual);
		}
		[TestMethod()]
		[Description("GetBoolean(NameValueCollection, String, Boolean) method for the optimal path.")]
		public void NameValueCollectionExtensions_Unit_GetBoolean_NameDoesNotExist() {
			String name = "Name";
			NameValueCollection collection = new NameValueCollection();
			Boolean defaultValue = true;

			Boolean actual = NameValueCollectionExtensions.GetBoolean(collection, name, defaultValue);
			Boolean expected = defaultValue;
			Assert.AreEqual(expected, actual);
		}
		[TestMethod()]
		[Description("GetBoolean(NameValueCollection, String, Boolean) method when 'collection' is a null reference.")]
		[ExpectedException(typeof(ArgumentNullException))]
		public void NameValueCollectionExtensions_Unit_GetBoolean_CollectionIsNull() {
			NameValueCollection collection = null;
			String name = "Name";
			Boolean defaultValue = true;
			NameValueCollectionExtensions.GetBoolean(collection, name, defaultValue);
		}
		[TestMethod()]
		[Description("GetBoolean(NameValueCollection, String, Boolean) method when 'name' is a null reference.")]
		[ExpectedException(typeof(ArgumentNullException))]
		public void NameValueCollectionExtensions_Unit_GetBoolean_NameIsNull() {
			NameValueCollection collection = new NameValueCollection();
			String name = null;
			Boolean defaultValue = true;
			NameValueCollectionExtensions.GetBoolean(collection, name, defaultValue);
		}

		[TestMethod()]
		[Description("GetByte(NameValueCollection, String, Byte) method for the optimal path.")]
		public void NameValueCollectionExtensions_Unit_GetByte_Optimal() {
			String name = "Name";
			Byte value = 1;
			NameValueCollection collection = new NameValueCollection() {
				{ name, value.ToString() } };
			Byte defaultValue = 255;

			Byte actual = NameValueCollectionExtensions.GetByte(collection, name, defaultValue);
			Byte expected = value;
			Assert.AreEqual(expected, actual);
		}
		[TestMethod()]
		[Description("GetByte(NameValueCollection, String, Byte) method for the optimal path.")]
		public void NameValueCollectionExtensions_Unit_GetByte_NameDoesNotExist() {
			String name = "Name";
			NameValueCollection collection = new NameValueCollection();
			Byte defaultValue = 255;

			Byte actual = NameValueCollectionExtensions.GetByte(collection, name, defaultValue);
			Byte expected = defaultValue;
			Assert.AreEqual(expected, actual);
		}
		[TestMethod()]
		[Description("GetByte(NameValueCollection, String, Byte) method when 'collection' is a null reference.")]
		[ExpectedException(typeof(ArgumentNullException))]
		public void NameValueCollectionExtensions_Unit_GetByte_CollectionIsNull() {
			NameValueCollection collection = null;
			String name = "Name";
			Byte defaultValue = 255;
			NameValueCollectionExtensions.GetByte(collection, name, defaultValue);
		}
		[TestMethod()]
		[Description("GetByte(NameValueCollection, String, Byte) method when 'name' is a null reference.")]
		[ExpectedException(typeof(ArgumentNullException))]
		public void NameValueCollectionExtensions_Unit_GetByte_NameIsNull() {
			NameValueCollection collection = new NameValueCollection();
			String name = null;
			Byte defaultValue = 255;
			NameValueCollectionExtensions.GetByte(collection, name, defaultValue);
		}

		[TestMethod()]
		[Description("GetChar(NameValueCollection, String, Char) method for the optimal path.")]
		public void NameValueCollectionExtensions_Unit_GetChar_Optimal() {
			String name = "Name";
			Char value = 'A';
			NameValueCollection collection = new NameValueCollection() {
				{ name, value.ToString() } };
			Char defaultValue = 'C';

			Char actual = NameValueCollectionExtensions.GetChar(collection, name, defaultValue);
			Char expected = value;
			Assert.AreEqual(expected, actual);
		}
		[TestMethod()]
		[Description("GetChar(NameValueCollection, String, Char) method for the optimal path.")]
		public void NameValueCollectionExtensions_Unit_GetChar_NameDoesNotExist() {
			String name = "Name";
			NameValueCollection collection = new NameValueCollection();
			Char defaultValue = 'C';

			Char actual = NameValueCollectionExtensions.GetChar(collection, name, defaultValue);
			Char expected = defaultValue;
			Assert.AreEqual(expected, actual);
		}
		[TestMethod()]
		[Description("GetChar(NameValueCollection, String, Char) method when 'collection' is a null reference.")]
		[ExpectedException(typeof(ArgumentNullException))]
		public void NameValueCollectionExtensions_Unit_GetChar_CollectionIsNull() {
			NameValueCollection collection = null;
			String name = "Name";
			Char defaultValue = 'C';
			NameValueCollectionExtensions.GetChar(collection, name, defaultValue);
		}
		[TestMethod()]
		[Description("GetChar(NameValueCollection, String, Char) method when 'name' is a null reference.")]
		[ExpectedException(typeof(ArgumentNullException))]
		public void NameValueCollectionExtensions_Unit_GetChar_NameIsNull() {
			NameValueCollection collection = new NameValueCollection();
			String name = null;
			Char defaultValue = 'C';
			NameValueCollectionExtensions.GetChar(collection, name, defaultValue);
		}

		[TestMethod()]
		[Description("GetDateTime(NameValueCollection, String, DateTime) method for the optimal path.")]
		public void NameValueCollectionExtensions_Unit_GetDateTime_Optimal() {
			String name = "Name";
			DateTime value = DateTime.Today.AddDays(-1);
			NameValueCollection collection = new NameValueCollection() {
				{ name, value.ToString() } };
			DateTime defaultValue = DateTime.Today.AddDays(1);

			DateTime actual = NameValueCollectionExtensions.GetDateTime(collection, name, defaultValue);
			DateTime expected = value;
			Assert.AreEqual(expected, actual);
		}
		[TestMethod()]
		[Description("GetDateTime(NameValueCollection, String, DateTime) method for the optimal path.")]
		public void NameValueCollectionExtensions_Unit_GetDateTime_NameDoesNotExist() {
			String name = "Name";
			NameValueCollection collection = new NameValueCollection();
			DateTime defaultValue = DateTime.Today.AddDays(1);

			DateTime actual = NameValueCollectionExtensions.GetDateTime(collection, name, defaultValue);
			DateTime expected = defaultValue;
			Assert.AreEqual(expected, actual);
		}
		[TestMethod()]
		[Description("GetDateTime(NameValueCollection, String, DateTime) method when 'collection' is a null reference.")]
		[ExpectedException(typeof(ArgumentNullException))]
		public void NameValueCollectionExtensions_Unit_GetDateTime_CollectionIsNull() {
			NameValueCollection collection = null;
			String name = "Name";
			DateTime defaultValue = DateTime.Today.AddDays(1);
			NameValueCollectionExtensions.GetDateTime(collection, name, defaultValue);
		}
		[TestMethod()]
		[Description("GetDateTime(NameValueCollection, String, DateTime) method when 'name' is a null reference.")]
		[ExpectedException(typeof(ArgumentNullException))]
		public void NameValueCollectionExtensions_Unit_GetDateTime_NameIsNull() {
			NameValueCollection collection = new NameValueCollection();
			String name = null;
			DateTime defaultValue = DateTime.Today.AddDays(1);
			NameValueCollectionExtensions.GetDateTime(collection, name, defaultValue);
		}

		[TestMethod()]
		[Description("GetDouble(NameValueCollection, String, Double) method for the optimal path.")]
		public void NameValueCollectionExtensions_Unit_GetDouble_Optimal() {
			String name = "Name";
			Double value = -1;
			NameValueCollection collection = new NameValueCollection() {
				{ name, value.ToString() } };
			Double defaultValue = 567;

			Double actual = NameValueCollectionExtensions.GetDouble(collection, name, defaultValue);
			Double expected = value;
			Assert.AreEqual(expected, actual);
		}
		[TestMethod()]
		[Description("GetDouble(NameValueCollection, String, Double) method for the optimal path.")]
		public void NameValueCollectionExtensions_Unit_GetDouble_NameDoesNotExist() {
			String name = "Name";
			NameValueCollection collection = new NameValueCollection();
			Double defaultValue = 567;

			Double actual = NameValueCollectionExtensions.GetDouble(collection, name, defaultValue);
			Double expected = defaultValue;
			Assert.AreEqual(expected, actual);
		}
		[TestMethod()]
		[Description("GetDouble(NameValueCollection, String, Double) method when 'collection' is a null reference.")]
		[ExpectedException(typeof(ArgumentNullException))]
		public void NameValueCollectionExtensions_Unit_GetDouble_CollectionIsNull() {
			NameValueCollection collection = null;
			String name = "Name";
			Double defaultValue = 567;
			NameValueCollectionExtensions.GetDouble(collection, name, defaultValue);
		}
		[TestMethod()]
		[Description("GetDouble(NameValueCollection, String, Double) method when 'name' is a null reference.")]
		[ExpectedException(typeof(ArgumentNullException))]
		public void NameValueCollectionExtensions_Unit_GetDouble_NameIsNull() {
			NameValueCollection collection = new NameValueCollection();
			String name = null;
			Double defaultValue = 567;
			NameValueCollectionExtensions.GetDouble(collection, name, defaultValue);
		}

		[TestMethod()]
		[Description("GetGuid(NameValueCollection, String, Guid) method for the optimal path.")]
		public void NameValueCollectionExtensions_Unit_GetGuid_Optimal() {
			String name = "Name";
			Guid value = Guid.NewGuid();
			NameValueCollection collection = new NameValueCollection() {
				{ name, value.ToString() } };
			Guid defaultValue = Guid.NewGuid();

			Guid actual = NameValueCollectionExtensions.GetGuid(collection, name, defaultValue);
			Guid expected = value;
			Assert.AreEqual(expected, actual);
		}
		[TestMethod()]
		[Description("GetGuid(NameValueCollection, String, Guid) method for the optimal path.")]
		public void NameValueCollectionExtensions_Unit_GetGuid_NameDoesNotExist() {
			String name = "Name";
			NameValueCollection collection = new NameValueCollection();
			Guid defaultValue = Guid.NewGuid();

			Guid actual = NameValueCollectionExtensions.GetGuid(collection, name, defaultValue);
			Guid expected = defaultValue;
			Assert.AreEqual(expected, actual);
		}
		[TestMethod()]
		[Description("GetGuid(NameValueCollection, String, Guid) method when 'collection' is a null reference.")]
		[ExpectedException(typeof(ArgumentNullException))]
		public void NameValueCollectionExtensions_Unit_GetGuid_CollectionIsNull() {
			NameValueCollection collection = null;
			String name = "Name";
			Guid defaultValue = Guid.NewGuid();
			NameValueCollectionExtensions.GetGuid(collection, name, defaultValue);
		}
		[TestMethod()]
		[Description("GetGuid(NameValueCollection, String, Guid) method when 'name' is a null reference.")]
		[ExpectedException(typeof(ArgumentNullException))]
		public void NameValueCollectionExtensions_Unit_GetGuid_NameIsNull() {
			NameValueCollection collection = new NameValueCollection();
			String name = null;
			Guid defaultValue = Guid.NewGuid();
			NameValueCollectionExtensions.GetGuid(collection, name, defaultValue);
		}

		[TestMethod()]
		[Description("GetInt16(NameValueCollection, String, Int16) method for the optimal path.")]
		public void NameValueCollectionExtensions_Unit_GetInt16_Optimal() {
			String name = "Name";
			Int16 value = -1;
			NameValueCollection collection = new NameValueCollection() {
				{ name, value.ToString() } };
			Int16 defaultValue = 567;

			Int16 actual = NameValueCollectionExtensions.GetInt16(collection, name, defaultValue);
			Int16 expected = value;
			Assert.AreEqual(expected, actual);
		}
		[TestMethod()]
		[Description("GetInt16(NameValueCollection, String, Int16) method for the optimal path.")]
		public void NameValueCollectionExtensions_Unit_GetInt16_NameDoesNotExist() {
			String name = "Name";
			NameValueCollection collection = new NameValueCollection();
			Int16 defaultValue = 567;

			Int16 actual = NameValueCollectionExtensions.GetInt16(collection, name, defaultValue);
			Int16 expected = defaultValue;
			Assert.AreEqual(expected, actual);
		}
		[TestMethod()]
		[Description("GetInt16(NameValueCollection, String, Int16) method when 'collection' is a null reference.")]
		[ExpectedException(typeof(ArgumentNullException))]
		public void NameValueCollectionExtensions_Unit_GetInt16_CollectionIsNull() {
			NameValueCollection collection = null;
			String name = "Name";
			Int16 defaultValue = 567;
			NameValueCollectionExtensions.GetInt16(collection, name, defaultValue);
		}
		[TestMethod()]
		[Description("GetInt16(NameValueCollection, String, Int16) method when 'name' is a null reference.")]
		[ExpectedException(typeof(ArgumentNullException))]
		public void NameValueCollectionExtensions_Unit_GetInt16_NameIsNull() {
			NameValueCollection collection = new NameValueCollection();
			String name = null;
			Int16 defaultValue = 567;
			NameValueCollectionExtensions.GetInt16(collection, name, defaultValue);
		}

		[TestMethod()]
		[Description("GetInt32(NameValueCollection, String, Int32) method for the optimal path.")]
		public void NameValueCollectionExtensions_Unit_GetInt32_Optimal() {
			String name = "Name";
			Int32 value = -1;
			NameValueCollection collection = new NameValueCollection() {
				{ name, value.ToString() } };
			Int32 defaultValue = 567;

			Int32 actual = NameValueCollectionExtensions.GetInt32(collection, name, defaultValue);
			Int32 expected = value;
			Assert.AreEqual(expected, actual);
		}
		[TestMethod()]
		[Description("GetInt32(NameValueCollection, String, Int32) method for the optimal path.")]
		public void NameValueCollectionExtensions_Unit_GetInt32_NameDoesNotExist() {
			String name = "Name";
			NameValueCollection collection = new NameValueCollection();
			Int32 defaultValue = 567;

			Int32 actual = NameValueCollectionExtensions.GetInt32(collection, name, defaultValue);
			Int32 expected = defaultValue;
			Assert.AreEqual(expected, actual);
		}
		[TestMethod()]
		[Description("GetInt32(NameValueCollection, String, Int32) method when 'collection' is a null reference.")]
		[ExpectedException(typeof(ArgumentNullException))]
		public void NameValueCollectionExtensions_Unit_GetInt32_CollectionIsNull() {
			NameValueCollection collection = null;
			String name = "Name";
			Int32 defaultValue = 567;
			NameValueCollectionExtensions.GetInt32(collection, name, defaultValue);
		}
		[TestMethod()]
		[Description("GetInt32(NameValueCollection, String, Int32) method when 'name' is a null reference.")]
		[ExpectedException(typeof(ArgumentNullException))]
		public void NameValueCollectionExtensions_Unit_GetInt32_NameIsNull() {
			NameValueCollection collection = new NameValueCollection();
			String name = null;
			Int32 defaultValue = 567;
			NameValueCollectionExtensions.GetInt32(collection, name, defaultValue);
		}

		[TestMethod()]
		[Description("GetInt64(NameValueCollection, String, Int64) method for the optimal path.")]
		public void NameValueCollectionExtensions_Unit_GetInt64_Optimal() {
			String name = "Name";
			Int64 value = -1;
			NameValueCollection collection = new NameValueCollection() {
				{ name, value.ToString() } };
			Int64 defaultValue = 567;

			Int64 actual = NameValueCollectionExtensions.GetInt64(collection, name, defaultValue);
			Int64 expected = value;
			Assert.AreEqual(expected, actual);
		}
		[TestMethod()]
		[Description("GetInt64(NameValueCollection, String, Int64) method for the optimal path.")]
		public void NameValueCollectionExtensions_Unit_GetInt64_NameDoesNotExist() {
			String name = "Name";
			NameValueCollection collection = new NameValueCollection();
			Int64 defaultValue = 567;

			Int64 actual = NameValueCollectionExtensions.GetInt64(collection, name, defaultValue);
			Int64 expected = defaultValue;
			Assert.AreEqual(expected, actual);
		}
		[TestMethod()]
		[Description("GetInt64(NameValueCollection, String, Int64) method when 'collection' is a null reference.")]
		[ExpectedException(typeof(ArgumentNullException))]
		public void NameValueCollectionExtensions_Unit_GetInt64_CollectionIsNull() {
			NameValueCollection collection = null;
			String name = "Name";
			Int64 defaultValue = 567;
			NameValueCollectionExtensions.GetInt64(collection, name, defaultValue);
		}
		[TestMethod()]
		[Description("GetInt64(NameValueCollection, String, Int64) method when 'name' is a null reference.")]
		[ExpectedException(typeof(ArgumentNullException))]
		public void NameValueCollectionExtensions_Unit_GetInt64_NameIsNull() {
			NameValueCollection collection = new NameValueCollection();
			String name = null;
			Int64 defaultValue = 567;
			NameValueCollectionExtensions.GetInt64(collection, name, defaultValue);
		}

		[TestMethod()]
		[Description("GetSByte(NameValueCollection, String, SByte) method for the optimal path.")]
		public void NameValueCollectionExtensions_Unit_GetSByte_Optimal() {
			String name = "Name";
			SByte value = -1;
			NameValueCollection collection = new NameValueCollection() {
				{ name, value.ToString() } };
			SByte defaultValue = 127;

			SByte actual = NameValueCollectionExtensions.GetSByte(collection, name, defaultValue);
			SByte expected = value;
			Assert.AreEqual(expected, actual);
		}
		[TestMethod()]
		[Description("GetSByte(NameValueCollection, String, SByte) method for the optimal path.")]
		public void NameValueCollectionExtensions_Unit_GetSByte_NameDoesNotExist() {
			String name = "Name";
			NameValueCollection collection = new NameValueCollection();
			SByte defaultValue = 127;

			SByte actual = NameValueCollectionExtensions.GetSByte(collection, name, defaultValue);
			SByte expected = defaultValue;
			Assert.AreEqual(expected, actual);
		}
		[TestMethod()]
		[Description("GetSByte(NameValueCollection, String, SByte) method when 'collection' is a null reference.")]
		[ExpectedException(typeof(ArgumentNullException))]
		public void NameValueCollectionExtensions_Unit_GetSByte_CollectionIsNull() {
			NameValueCollection collection = null;
			String name = "Name";
			SByte defaultValue = 127;
			NameValueCollectionExtensions.GetSByte(collection, name, defaultValue);
		}
		[TestMethod()]
		[Description("GetSByte(NameValueCollection, String, SByte) method when 'name' is a null reference.")]
		[ExpectedException(typeof(ArgumentNullException))]
		public void NameValueCollectionExtensions_Unit_GetSByte_NameIsNull() {
			NameValueCollection collection = new NameValueCollection();
			String name = null;
			SByte defaultValue = 127;
			NameValueCollectionExtensions.GetSByte(collection, name, defaultValue);
		}

		[TestMethod()]
		[Description("GetSingle(NameValueCollection, String, Single) method for the optimal path.")]
		public void NameValueCollectionExtensions_Unit_GetSingle_Optimal() {
			String name = "Name";
			Single value = -1;
			NameValueCollection collection = new NameValueCollection() {
				{ name, value.ToString() } };
			Single defaultValue = 567;

			Single actual = NameValueCollectionExtensions.GetSingle(collection, name, defaultValue);
			Single expected = value;
			Assert.AreEqual(expected, actual);
		}
		[TestMethod()]
		[Description("GetSingle(NameValueCollection, String, Single) method for the optimal path.")]
		public void NameValueCollectionExtensions_Unit_GetSingle_NameDoesNotExist() {
			String name = "Name";
			NameValueCollection collection = new NameValueCollection();
			Single defaultValue = 567;

			Single actual = NameValueCollectionExtensions.GetSingle(collection, name, defaultValue);
			Single expected = defaultValue;
			Assert.AreEqual(expected, actual);
		}
		[TestMethod()]
		[Description("GetSingle(NameValueCollection, String, Single) method when 'collection' is a null reference.")]
		[ExpectedException(typeof(ArgumentNullException))]
		public void NameValueCollectionExtensions_Unit_GetSingle_CollectionIsNull() {
			NameValueCollection collection = null;
			String name = "Name";
			Single defaultValue = 567;
			NameValueCollectionExtensions.GetSingle(collection, name, defaultValue);
		}
		[TestMethod()]
		[Description("GetSingle(NameValueCollection, String, Single) method when 'name' is a null reference.")]
		[ExpectedException(typeof(ArgumentNullException))]
		public void NameValueCollectionExtensions_Unit_GetSingle_NameIsNull() {
			NameValueCollection collection = new NameValueCollection();
			String name = null;
			Single defaultValue = 567;
			NameValueCollectionExtensions.GetSingle(collection, name, defaultValue);
		}

		[TestMethod()]
		[Description("GetUInt16(NameValueCollection, String, UInt16) method for the optimal path.")]
		public void NameValueCollectionExtensions_Unit_GetUInt16_Optimal() {
			String name = "Name";
			UInt16 value = 1;
			NameValueCollection collection = new NameValueCollection() {
				{ name, value.ToString() } };
			UInt16 defaultValue = 567;

			UInt16 actual = NameValueCollectionExtensions.GetUInt16(collection, name, defaultValue);
			UInt16 expected = value;
			Assert.AreEqual(expected, actual);
		}
		[TestMethod()]
		[Description("GetUInt16(NameValueCollection, String, UInt16) method for the optimal path.")]
		public void NameValueCollectionExtensions_Unit_GetUInt16_NameDoesNotExist() {
			String name = "Name";
			NameValueCollection collection = new NameValueCollection();
			UInt16 defaultValue = 567;

			UInt16 actual = NameValueCollectionExtensions.GetUInt16(collection, name, defaultValue);
			UInt16 expected = defaultValue;
			Assert.AreEqual(expected, actual);
		}
		[TestMethod()]
		[Description("GetUInt16(NameValueCollection, String, UInt16) method when 'collection' is a null reference.")]
		[ExpectedException(typeof(ArgumentNullException))]
		public void NameValueCollectionExtensions_Unit_GetUInt16_CollectionIsNull() {
			NameValueCollection collection = null;
			String name = "Name";
			UInt16 defaultValue = 567;
			NameValueCollectionExtensions.GetUInt16(collection, name, defaultValue);
		}
		[TestMethod()]
		[Description("GetUInt16(NameValueCollection, String, UInt16) method when 'name' is a null reference.")]
		[ExpectedException(typeof(ArgumentNullException))]
		public void NameValueCollectionExtensions_Unit_GetUInt16_NameIsNull() {
			NameValueCollection collection = new NameValueCollection();
			String name = null;
			UInt16 defaultValue = 567;
			NameValueCollectionExtensions.GetUInt16(collection, name, defaultValue);
		}

		[TestMethod()]
		[Description("GetUInt32(NameValueCollection, String, UInt32) method for the optimal path.")]
		public void NameValueCollectionExtensions_Unit_GetUInt32_Optimal() {
			String name = "Name";
			UInt32 value = 1;
			NameValueCollection collection = new NameValueCollection() {
				{ name, value.ToString() } };
			UInt32 defaultValue = 567;

			UInt32 actual = NameValueCollectionExtensions.GetUInt32(collection, name, defaultValue);
			UInt32 expected = value;
			Assert.AreEqual(expected, actual);
		}
		[TestMethod()]
		[Description("GetUInt32(NameValueCollection, String, UInt32) method for the optimal path.")]
		public void NameValueCollectionExtensions_Unit_GetUInt32_NameDoesNotExist() {
			String name = "Name";
			NameValueCollection collection = new NameValueCollection();
			UInt32 defaultValue = 567;

			UInt32 actual = NameValueCollectionExtensions.GetUInt32(collection, name, defaultValue);
			UInt32 expected = defaultValue;
			Assert.AreEqual(expected, actual);
		}
		[TestMethod()]
		[Description("GetUInt32(NameValueCollection, String, UInt32) method when 'collection' is a null reference.")]
		[ExpectedException(typeof(ArgumentNullException))]
		public void NameValueCollectionExtensions_Unit_GetUInt32_CollectionIsNull() {
			NameValueCollection collection = null;
			String name = "Name";
			UInt32 defaultValue = 567;
			NameValueCollectionExtensions.GetUInt32(collection, name, defaultValue);
		}
		[TestMethod()]
		[Description("GetUInt32(NameValueCollection, String, UInt32) method when 'name' is a null reference.")]
		[ExpectedException(typeof(ArgumentNullException))]
		public void NameValueCollectionExtensions_Unit_GetUInt32_NameIsNull() {
			NameValueCollection collection = new NameValueCollection();
			String name = null;
			UInt32 defaultValue = 567;
			NameValueCollectionExtensions.GetUInt32(collection, name, defaultValue);
		}

		[TestMethod()]
		[Description("GetUInt64(NameValueCollection, String, UInt64) method for the optimal path.")]
		public void NameValueCollectionExtensions_Unit_GetUInt64_Optimal() {
			String name = "Name";
			UInt64 value = 1;
			NameValueCollection collection = new NameValueCollection() {
				{ name, value.ToString() } };
			UInt64 defaultValue = 567;

			UInt64 actual = NameValueCollectionExtensions.GetUInt64(collection, name, defaultValue);
			UInt64 expected = value;
			Assert.AreEqual(expected, actual);
		}
		[TestMethod()]
		[Description("GetUInt64(NameValueCollection, String, UInt64) method for the optimal path.")]
		public void NameValueCollectionExtensions_Unit_GetUInt64_NameDoesNotExist() {
			String name = "Name";
			NameValueCollection collection = new NameValueCollection();
			UInt64 defaultValue = 567;

			UInt64 actual = NameValueCollectionExtensions.GetUInt64(collection, name, defaultValue);
			UInt64 expected = defaultValue;
			Assert.AreEqual(expected, actual);
		}
		[TestMethod()]
		[Description("GetUInt64(NameValueCollection, String, UInt64) method when 'collection' is a null reference.")]
		[ExpectedException(typeof(ArgumentNullException))]
		public void NameValueCollectionExtensions_Unit_GetUInt64_CollectionIsNull() {
			NameValueCollection collection = null;
			String name = "Name";
			UInt64 defaultValue = 567;
			NameValueCollectionExtensions.GetUInt64(collection, name, defaultValue);
		}
		[TestMethod()]
		[Description("GetUInt64(NameValueCollection, String, UInt64) method when 'name' is a null reference.")]
		[ExpectedException(typeof(ArgumentNullException))]
		public void NameValueCollectionExtensions_Unit_GetUInt64_NameIsNull() {
			NameValueCollection collection = new NameValueCollection();
			String name = null;
			UInt64 defaultValue = 567;
			NameValueCollectionExtensions.GetUInt64(collection, name, defaultValue);
		}
	}
}
