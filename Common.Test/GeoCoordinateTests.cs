using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Vizistata.TestTools;

namespace Vizistata {
	/// <summary>
	/// This is a test class for <see cref="T:GeoCoordinate"/> and is intended to contain all <see cref="T:GeoCoordinate"/> Unit Tests.
	///</summary>
	[TestClass()]
	public class GeoCoordinateTests {
		#region Test Class Implementation
		/// <summary>
		/// Describes the context under which the current test is running.
		/// </summary>
		private TestContext _testContextInstance;

		/// <summary>
		/// Initializes a new instance of the <see cref="T:GeoCoordinateTests"/> class.
		/// </summary>
		public GeoCoordinateTests() : base() { }

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

	// Constants
		/// <summary>
		/// The location of Dallas Texas = { 32.7831, -96.8 }.
		/// </summary>
		/// <remarks>Although not a constant, static readonly on an immutable type is effectively constant.</remarks>
		private static readonly GeoCoordinate Dallas = new GeoCoordinate(32.7831, -96.8);
		/// <summary>
		/// The location of Denver Colorado = { 39.7389, -104.9844 }.
		/// </summary>
		/// <remarks>Although not a constant, static readonly on an immutable type is effectively constant.</remarks>
		private static readonly GeoCoordinate Denver = new GeoCoordinate(39.7389, -104.9844);

	// Methods
		/// <summary>
		/// Returns a collection of tuples representing two points and their distance from each other in kilometers.
		/// </summary>
		/// <returns>The collection of tuples.</returns>
		private static IEnumerable<Tuple<GeoCoordinate, GeoCoordinate, Double>> GetValidDistancesInKilometers() {
			yield return new Tuple<GeoCoordinate, GeoCoordinate, Double>(Dallas, Denver, 1065.2);
		}
		/// <summary>
		/// Returns a collection of tuples representing two points and their distance from each other in miles.
		/// </summary>
		/// <returns>The collection of tuples.</returns>
		private static IEnumerable<Tuple<GeoCoordinate, GeoCoordinate, Double>> GetValidDistancesInMiles() {
			yield return new Tuple<GeoCoordinate, GeoCoordinate, Double>(Dallas, Denver, 661.9);
		}
		/// <summary>
		/// Returns the list of valid formats used in string formatting.
		/// </summary>
		/// <returns>The list of valid string formats.</returns>
		private static IEnumerable<String> GetValidStringFormats() {
			yield return GeoCoordinate.GeneralFormat;
			yield return GeoCoordinate.SetFormat;
			yield break;
		}

	// Constructor Tests
		[TestMethod()]
		[Description(".ctor(Double, Double) constructor for the optimal path.")]
		public void GeoCoordinate_Unit_Constructor_Optimal() {
			Double latitude = -32;
			Double longitude = 32;
			new GeoCoordinate(latitude, longitude);
		}
		[TestMethod()]
		[Description(".ctor(Double, Double) constructor when 'latitude' is too small.")]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void GeoCoordinate_Unit_Constructor_LatitudeIsTooSmall() {
			Double latitude = -91;
			Double longitude = 32;
			new GeoCoordinate(latitude, longitude);
		}
		[TestMethod()]
		[Description(".ctor(Double, Double) constructor when 'latitude' is too large.")]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void GeoCoordinate_Unit_Constructor_LatitudeIsTooLarge() {
			Double latitude = 91;
			Double longitude = 32;
			new GeoCoordinate(latitude, longitude);
		}
		[TestMethod()]
		[Description(".ctor(Double, Double) constructor when 'longitude' is too small.")]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void GeoCoordinate_Unit_Constructor_LongitudeIsTooSmall() {
			Double latitude = -32;
			Double longitude = -181;
			new GeoCoordinate(latitude, longitude);
		}
		[TestMethod()]
		[Description(".ctor(Double, Double) constructor when 'longitude' is too large.")]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void GeoCoordinate_Unit_Constructor_LongitudeIsTooLarge() {
			Double latitude = -32;
			Double longitude = 181;
			new GeoCoordinate(latitude, longitude);
		}

	// Property Tests
		[TestMethod()]
		[Description("Latitude property for the optimal path.")]
		public void GeoCoordinate_Unit_Latitude_Optimal() {
			Double latitude = -32;
			Double longitude = 32;
			GeoCoordinate target = new GeoCoordinate(latitude, longitude);

			Assert.AreEqual(latitude, target.Latitude);
		}

		[TestMethod()]
		[Description("Longitude property for the optimal path.")]
		public void GeoCoordinate_Unit_Longitude_Optimal() {
			Double latitude = -32;
			Double longitude = 32;
			GeoCoordinate target = new GeoCoordinate(latitude, longitude);

			Assert.AreEqual(longitude, target.Longitude);
		}

	// Method Tests
		[TestMethod()]
		[Description("Equals(Object) method when 'obj' is an equivalent object.")]
		public void GeoCoordinate_Unit_Equals1_ObjIsEquivalent() {
			Double latitude = -32;
			Double longitude = 32;
			GeoCoordinate target = new GeoCoordinate(latitude, longitude);
			Object obj = new GeoCoordinate(latitude, longitude);

			Boolean actual = target.Equals(obj);
			Assert.AreEqual(true, actual);
		}
		[TestMethod()]
		[Description("Equals(Object) method when 'obj' is an unequivalent object.")]
		public void GeoCoordinate_Unit_Equals1_ObjIsUnequivalent() {
			Double latitude = -32;
			Double longitude = 32;
			GeoCoordinate target = new GeoCoordinate(latitude, longitude);
			Object obj = new GeoCoordinate();

			Boolean actual = target.Equals(obj);
			Assert.AreEqual(false, actual);
		}
		[TestMethod()]
		[Description("Equals(Object) method when 'obj' is a null reference.")]
		public void GeoCoordinate_Unit_Equals1_ObjIsNull() {
			Double latitude = -32;
			Double longitude = 32;
			GeoCoordinate target = new GeoCoordinate(latitude, longitude);
			Object obj = null;

			Boolean actual = target.Equals(obj);
			Assert.AreEqual(false, actual);
		}
		[TestMethod()]
		[Description("Equals(Object) method when 'obj' is a different type.")]
		public void GeoCoordinate_Unit_Equals1_ObjIsDifferentType() {
			Double latitude = -32;
			Double longitude = 32;
			GeoCoordinate target = new GeoCoordinate(latitude, longitude);
			Object obj = new Object();

			Boolean actual = target.Equals(obj);
			Assert.AreEqual(false, actual);
		}

		[TestMethod()]
		[Description("Equals(GeoCoordinate) method when 'other' is an equivalent object.")]
		public void GeoCoordinate_Unit_Equals2_ObjIsEquivalent() {
			Double latitude = -32;
			Double longitude = 32;
			GeoCoordinate target = new GeoCoordinate(latitude, longitude);
			GeoCoordinate other = new GeoCoordinate(latitude, longitude);

			Boolean actual = target.Equals(other);
			Assert.AreEqual(true, actual);
		}
		[TestMethod()]
		[Description("Equals(GeoCoordinate) method when 'other' is an unequivalent object.")]
		public void GeoCoordinate_Unit_Equals2_ObjIsUnequivalent() {
			Double latitude = -32;
			Double longitude = 32;
			GeoCoordinate target = new GeoCoordinate(latitude, longitude);
			GeoCoordinate other = new GeoCoordinate();

			Boolean actual = target.Equals(other);
			Assert.AreEqual(false, actual);
		}

		[TestMethod()]
		[Description("GetDistanceInKilometersFrom(GeoCoorindate) method for the optimal path.")]
		public void GeoCoordinate_Unit_GetDistanceInKilometersFrom_Optimal() {
			foreach (Tuple<GeoCoordinate, GeoCoordinate, Double> tuple in GeoCoordinateTests.GetValidDistancesInKilometers()) {
				GeoCoordinate target = tuple.Item1;
				GeoCoordinate other = tuple.Item2;
				Double expected = tuple.Item3;

				Double actual = target.GetDistanceInKilometersFrom(other);
				Assert.IsTrue(expected - actual < .01);
			}
		}
		[TestMethod()]
		[Description("GetDistanceInKilometersFrom(GeoCoorindate) method when 'other' is an equivalent object.")]
		public void GeoCoordinate_Unit_GetDistanceInKilometersFrom_OtherIsEquivalent() {
			Double latitude = -32;
			Double longitude = 32;
			GeoCoordinate target = new GeoCoordinate(latitude, longitude);
			GeoCoordinate other = new GeoCoordinate(latitude, longitude);

			Double actual = target.GetDistanceInKilometersFrom(other);
			Assert.AreEqual(0d, actual);
		}

		[TestMethod()]
		[Description("GetDistanceInMilesFrom(GeoCoorindate) method for the optimal path.")]
		public void GeoCoordinate_Unit_GetDistanceInMilesFrom_Optimal() {
			foreach (Tuple<GeoCoordinate, GeoCoordinate, Double> tuple in GeoCoordinateTests.GetValidDistancesInMiles()) {
				GeoCoordinate target = tuple.Item1;
				GeoCoordinate other = tuple.Item2;
				Double expected = tuple.Item3;

				Double actual = target.GetDistanceInMilesFrom(other);
				Assert.IsTrue(expected - actual < .01);
			}
		}
		[TestMethod()]
		[Description("GetDistanceInMilesFrom(GeoCoorindate) method when 'other' is an equivalent object.")]
		public void GeoCoordinate_Unit_GetDistanceInMilesFrom_OtherIsEquivalent() {
			Double latitude = -32;
			Double longitude = 32;
			GeoCoordinate target = new GeoCoordinate(latitude, longitude);
			GeoCoordinate other = new GeoCoordinate(latitude, longitude);

			Double actual = target.GetDistanceInMilesFrom(other);
			Assert.AreEqual(0d, actual);
		}

		[TestMethod()]
		[Description("GetHashCode() method when equivalent objects are compared.")]
		public void GeoCoordinate_Unit_GetHashCode_EquivalentObjects() {
			Double latitude = -32;
			Double longitude = 32;
			GeoCoordinate first = new GeoCoordinate(latitude, longitude);
			GeoCoordinate second = new GeoCoordinate(latitude, longitude);

			Assert.AreEqual(first.GetHashCode(), second.GetHashCode());
		}
		[TestMethod()]
		[Description("GetHashCode() method when unequivalent objects are compared.")]
		public void GeoCoordinate_Unit_GetHashCode_UnequivalentObjects() {
			Double latitude = -32;
			Double longitude = 32;
			GeoCoordinate first = new GeoCoordinate(latitude, longitude);
			GeoCoordinate second = new GeoCoordinate();

			Assert.AreNotEqual(first.GetHashCode(), second.GetHashCode());
		}

		[TestMethod()]
		[Description("IsEastOf(GeoCoordinate) method for the optimal path.")]
		public void GeoCoordinate_Unit_IsEastOf_Optimal() {
			Double latitude = -32;
			Double longitude = 32;
			GeoCoordinate target = new GeoCoordinate(latitude, longitude);
			GeoCoordinate other = new GeoCoordinate(0, 31);

			Boolean actual = target.IsEastOf(other);
			Assert.AreEqual(true, actual);
		}
		[TestMethod()]
		[Description("IsEastOf(GeoCoordinate) method when 'other' is east.")]
		public void GeoCoordinate_Unit_IsEastOf_OtherIsEast() {
			Double latitude = -32;
			Double longitude = 32;
			GeoCoordinate target = new GeoCoordinate(latitude, longitude);
			GeoCoordinate other = new GeoCoordinate(0, 33);

			Boolean actual = target.IsEastOf(other);
			Assert.AreEqual(false, actual);
		}
		[TestMethod()]
		[Description("IsEastOf(GeoCoordinate) method when 'other' has the same longitude.")]
		public void GeoCoordinate_Unit_IsEastOf_OtherHasSameLongitude() {
			Double latitude = -32;
			Double longitude = 32;
			GeoCoordinate target = new GeoCoordinate(latitude, longitude);
			GeoCoordinate other = new GeoCoordinate(0, longitude);

			Boolean actual = target.IsEastOf(other);
			Assert.AreEqual(false, actual);
		}

		[TestMethod()]
		[Description("IsNorthOf(GeoCoordinate) method for the optimal path.")]
		public void GeoCoordinate_Unit_IsNorthOf_Optimal() {
			Double latitude = -32;
			Double longitude = 32;
			GeoCoordinate target = new GeoCoordinate(latitude, longitude);
			GeoCoordinate other = new GeoCoordinate(-33, 0);

			Boolean actual = target.IsNorthOf(other);
			Assert.AreEqual(true, actual);
		}
		[TestMethod()]
		[Description("IsNorthOf(GeoCoordinate) method when 'other' is north.")]
		public void GeoCoordinate_Unit_IsNorthOf_OtherIsNorth() {
			Double latitude = -32;
			Double longitude = 32;
			GeoCoordinate target = new GeoCoordinate(latitude, longitude);
			GeoCoordinate other = new GeoCoordinate(-31, 0);

			Boolean actual = target.IsNorthOf(other);
			Assert.AreEqual(false, actual);
		}
		[TestMethod()]
		[Description("IsNorthOf(GeoCoordinate) method when 'other' has the same latitude.")]
		public void GeoCoordinate_Unit_IsNorthOf_OtherHasSameLatitude() {
			Double latitude = -32;
			Double longitude = 32;
			GeoCoordinate target = new GeoCoordinate(latitude, longitude);
			GeoCoordinate other = new GeoCoordinate(latitude, 0);

			Boolean actual = target.IsNorthOf(other);
			Assert.AreEqual(false, actual);
		}

		[TestMethod()]
		[Description("IsSouthOf(GeoCoordinate) method for the optimal path.")]
		public void GeoCoordinate_Unit_IsSouthOf_Optimal() {
			Double latitude = -32;
			Double longitude = 32;
			GeoCoordinate target = new GeoCoordinate(latitude, longitude);
			GeoCoordinate other = new GeoCoordinate(-31, 0);

			Boolean actual = target.IsSouthOf(other);
			Assert.AreEqual(true, actual);
		}
		[TestMethod()]
		[Description("IsSouthOf(GeoCoordinate) method when 'other' is south.")]
		public void GeoCoordinate_Unit_IsSouthOf_OtherIsSouth() {
			Double latitude = -32;
			Double longitude = 32;
			GeoCoordinate target = new GeoCoordinate(latitude, longitude);
			GeoCoordinate other = new GeoCoordinate(-33, 0);

			Boolean actual = target.IsSouthOf(other);
			Assert.AreEqual(false, actual);
		}
		[TestMethod()]
		[Description("IsSouthOf(GeoCoordinate) method when 'other' has the same latitude.")]
		public void GeoCoordinate_Unit_IsSouthOf_OtherHasSameLatitude() {
			Double latitude = -32;
			Double longitude = 32;
			GeoCoordinate target = new GeoCoordinate(latitude, longitude);
			GeoCoordinate other = new GeoCoordinate(latitude, 0);

			Boolean actual = target.IsSouthOf(other);
			Assert.AreEqual(false, actual);
		}

		[TestMethod()]
		[Description("IsWestOf(GeoCoordinate) method for the optimal path.")]
		public void GeoCoordinate_Unit_IsWestOf_Optimal() {
			Double latitude = -32;
			Double longitude = 32;
			GeoCoordinate target = new GeoCoordinate(latitude, longitude);
			GeoCoordinate other = new GeoCoordinate(0, 33);

			Boolean actual = target.IsWestOf(other);
			Assert.AreEqual(true, actual);
		}
		[TestMethod()]
		[Description("IsWestOf(GeoCoordinate) method when 'other' is west.")]
		public void GeoCoordinate_Unit_IsWestOf_OtherIsWest() {
			Double latitude = -32;
			Double longitude = 32;
			GeoCoordinate target = new GeoCoordinate(latitude, longitude);
			GeoCoordinate other = new GeoCoordinate(0, 31);

			Boolean actual = target.IsWestOf(other);
			Assert.AreEqual(false, actual);
		}
		[TestMethod()]
		[Description("IsWestOf(GeoCoordinate) method when 'other' has the same longitude.")]
		public void GeoCoordinate_Unit_IsWestOf_OtherHasSameLongitude() {
			Double latitude = -32;
			Double longitude = 32;
			GeoCoordinate target = new GeoCoordinate(latitude, longitude);
			GeoCoordinate other = new GeoCoordinate(0, longitude);

			Boolean actual = target.IsWestOf(other);
			Assert.AreEqual(false, actual);
		}

		[TestMethod()]
		[Description("ToString() method for the optimal path.")]
		public void GeoCoordinate_Unit_ToString1_Optimal() {
			Double latitude = -32;
			Double longitude = 32;
			GeoCoordinate target = new GeoCoordinate(latitude, longitude);

			String actual = target.ToString();
			Assert.IsNotNull(actual);
			Assert.AreNotEqual(typeof(GeoCoordinate).ToString(), actual);
		}

		[TestMethod()]
		[Description("ToString(String) method for the optimal path.")]
		public void GeoCoordinate_Unit_ToString2_Optimal() {
			Double latitude = -32;
			Double longitude = 32;
			GeoCoordinate target = new GeoCoordinate(latitude, longitude);

			foreach (String format in GeoCoordinateTests.GetValidStringFormats()) {
				String actual = target.ToString(format);
				Assert.IsNotNull(actual);
				Assert.AreNotEqual(typeof(GeoCoordinate).ToString(), actual);
			}
		}
		[TestMethod()]
		[Description("ToString(String) method when 'format' is a null reference.")]
		public void GeoCoordinate_Unit_ToString2_FormatIsNull() {
			Double latitude = -32;
			Double longitude = 32;
			GeoCoordinate target = new GeoCoordinate(latitude, longitude);
			String format = null;

			String actual = target.ToString(format);
			Assert.IsNotNull(actual);
			Assert.AreNotEqual(typeof(GeoCoordinate).ToString(), actual);
		}
		[TestMethod()]
		[Description("ToString(String) method when 'format' is invalid.")]
		[ExpectedException(typeof(FormatException))]
		public void GeoCoordinate_Unit_ToString2_FormatIsInvalid() {
			Double latitude = -32;
			Double longitude = 32;
			GeoCoordinate target = new GeoCoordinate(latitude, longitude);
			String format = " \r \n \t ";

			target.ToString(format);
		}

		[TestMethod()]
		[Description("ToString(IFormatProvider) method for the optimal path.")]
		public void GeoCoordinate_Unit_ToString3_Optimal() {
			Double latitude = -32;
			Double longitude = 32;
			GeoCoordinate target = new GeoCoordinate(latitude, longitude);
			IFormatProvider provider = CultureInfo.GetCultureInfo("en-US");

			String actual = target.ToString(provider);
			Assert.IsNotNull(actual);
			Assert.AreNotEqual(typeof(GeoCoordinate).ToString(), actual);
		}
		[TestMethod()]
		[Description("ToString(IFormatProvider) method when 'provider' is a null reference.")]
		public void GeoCoordinate_Unit_ToString3_ProviderIsNull() {
			Double latitude = -32;
			Double longitude = 32;
			GeoCoordinate target = new GeoCoordinate(latitude, longitude);
			IFormatProvider provider = null;

			String actual = target.ToString(provider);
			Assert.IsNotNull(actual);
			Assert.AreNotEqual(typeof(GeoCoordinate).ToString(), actual);
		}

		[TestMethod()]
		[Description("ToString(String, IFormatProvider) method for the optimal path.")]
		public void GeoCoordinate_Unit_ToString4_Optimal() {
			Double latitude = -32;
			Double longitude = 32;
			GeoCoordinate target = new GeoCoordinate(latitude, longitude);
			IFormatProvider provider = CultureInfo.GetCultureInfo("en-US");

			String[] validForms = new String[] {
				
			};
			foreach (String format in GeoCoordinateTests.GetValidStringFormats()) {
				String actual = target.ToString(format, provider);
				Assert.IsNotNull(actual);
				Assert.AreNotEqual(typeof(GeoCoordinate).ToString(), actual);
			}
		}
		[TestMethod()]
		[Description("ToString(String, IFormatProvider) method when 'format' is a null reference.")]
		public void GeoCoordinate_Unit_ToString4_FormatIsNull() {
			Double latitude = -32;
			Double longitude = 32;
			GeoCoordinate target = new GeoCoordinate(latitude, longitude);
			String format = null;
			IFormatProvider provider = CultureInfo.GetCultureInfo("en-US");

			String actual = target.ToString(format, provider);
			Assert.IsNotNull(actual);
			Assert.AreNotEqual(typeof(GeoCoordinate).ToString(), actual);
		}
		[TestMethod()]
		[Description("ToString(String, IFormatProvider) method when 'format' is invalid.")]
		[ExpectedException(typeof(FormatException))]
		public void GeoCoordinate_Unit_ToString4_FormatIsInvalid() {
			Double latitude = -32;
			Double longitude = 32;
			GeoCoordinate target = new GeoCoordinate(latitude, longitude);
			String format = " \r \n \t ";
			IFormatProvider provider = CultureInfo.GetCultureInfo("en-US");

			target.ToString(format, provider);
		}
		[TestMethod()]
		[Description("ToString(String, IFormatProvider) method when 'provider' is a null reference.")]
		public void GeoCoordinate_Unit_ToString4_ProviderIsNull() {
			Double latitude = -32;
			Double longitude = 32;
			GeoCoordinate target = new GeoCoordinate(latitude, longitude);
			String format = GeoCoordinate.SetFormat;
			IFormatProvider provider = null;

			String actual = target.ToString(format, provider);
			Assert.IsNotNull(actual);
			Assert.AreNotEqual(typeof(GeoCoordinate).ToString(), actual);
		}

	// Operator Tests
		[TestMethod()]
		[Description("GeoCoordinate==GeoCoordinate operator when equivalent objects are compared.")]
		public void GeoCoordinate_Unit_EqualityOperator_EquivalentObjects() {
			Double latitude = -32;
			Double longitude = 32;
			GeoCoordinate objA = new GeoCoordinate(latitude, longitude);
			GeoCoordinate objB = new GeoCoordinate(latitude, longitude);

			Boolean actual = objA == objB;
			Assert.AreEqual(true, actual);
		}
		[TestMethod()]
		[Description("GeoCoordinate==GeoCoordinate operator when unequivalent objects are compared.")]
		public void GeoCoordinate_Unit_EqualityOperator_UnequivalentObjects() {
			Double latitude = -32;
			Double longitude = 32;
			GeoCoordinate objA = new GeoCoordinate(latitude, longitude);
			GeoCoordinate objB = new GeoCoordinate(-latitude, -longitude);

			Boolean actual = objA == objB;
			Assert.AreEqual(false, actual);
		}

		[TestMethod()]
		[Description("GeoCoordinate!=GeoCoordinate operator when equivalent objects are compared.")]
		public void GeoCoordinate_Unit_InequalityOperator_EquivalentObjects() {
			Double latitude = -32;
			Double longitude = 32;
			GeoCoordinate objA = new GeoCoordinate(latitude, longitude);
			GeoCoordinate objB = new GeoCoordinate(latitude, longitude);

			Boolean actual = objA != objB;
			Assert.AreEqual(false, actual);
		}
		[TestMethod()]
		[Description("GeoCoordinate!=GeoCoordinate operator when unequivalent objects are compared.")]
		public void GeoCoordinate_Unit_InequalityOperator_UnequivalentObjects() {
			Double latitude = -32;
			Double longitude = 32;
			GeoCoordinate objA = new GeoCoordinate(latitude, longitude);
			GeoCoordinate objB = new GeoCoordinate(-latitude, -longitude);

			Boolean actual = objA != objB;
			Assert.AreEqual(true, actual);
		}
		
	// Serialization Tests
		[TestMethod()]
		[Description("Serializability of the class for the optimal path.")]
		public void GeoCoordinate_Integration_Serialization_Optimal() {
			Double latitude = -32;
			Double longitude = 32;
			GeoCoordinate original = new GeoCoordinate(latitude, longitude);

			GeoCoordinate clone = original.SerializeBinary();
			Assert.AreNotSame(original, clone);
			Assert.AreEqual(original, clone);
			Assert.AreEqual(original.GetHashCode(), clone.GetHashCode());
			Assert.AreEqual(original.Latitude, clone.Latitude);
			Assert.AreEqual(original.Longitude, clone.Longitude);
		}
	}
}