using System;
using System.Diagnostics.CodeAnalysis;

using ImmutableObject = System.ComponentModel.ImmutableObjectAttribute;

namespace Vizistata {
	/// <summary>
	/// Represents a geological set of coordinates.  Instances of this struct are immutable.
	/// </summary>
	[Serializable()]
	[ImmutableObject(true)]
	public struct GeoCoordinate : IEquatable<GeoCoordinate>, IFormattable {
	// Constants
        /// <summary>
        /// The value of the radius of Earth in kilometers = 6371.1.
        /// </summary>
        private const Double EarthRadiusInKilometers = 6371.1;
        /// <summary>
        /// The value of the radius of Earth in miles = 3963.1676.
        /// </summary>
        private const Double EarthRadiusInMiles = 3963.1676;
        /// <summary>
        /// The general or default format string = "g".
        /// </summary>
        public const String GeneralFormat = "g";
        /// <summary>
        /// The format string that denotes a set (i.e.  "{ lat, long }") = "s".
        /// </summary>
        public const String SetFormat = "s";

    // Fields
        /// <summary>
        /// The latitude of this GeoCoordinate.  This field is read-only.
        /// </summary>
        private readonly Double _latitude;
        /// <summary>
		/// The longitude of this GeoCoordinate.  This field is read-only.
        /// </summary>
		private readonly Double _longitude;

	// Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:GeoCoordinate"/> class.
		/// </summary>
		/// <param name="latitude">The latitude in degrees.</param>
		/// <param name="longitude">The longitude in degrees.</param>
		/// <exception cref="System.ArgumentOutOfRangeException"><paramref name="latitude"/> is less than -90.
		/// -or <paramref name="latitude"/> is greater than 90.
		/// -or <paramref name="longitude"/> is less than -180.
		/// -or <paramref name="longitude"/> is greater than 180.</exception>
		public GeoCoordinate(Double latitude, Double longitude) {
			if (latitude < -90 || latitude > 90) {
				throw new ArgumentOutOfRangeException("latitude");
			}
			if (longitude < -180 || longitude > 180) {
				throw new ArgumentOutOfRangeException("longitude");
			}

            this._latitude = latitude;
            this._longitude = longitude;
		}

	// Properties
		/// <summary>
		/// Gets the latitude in degrees for the coordinate.  Positive values are east, and negative values are west.
		/// </summary>
		public Double Latitude {
            get { return this._latitude; }
		}
		/// <summary>
		/// Gets the longitude in degrees for the coordinate.  Positive values are north, and negative values are south.
		/// </summary>
		public Double Longitude {
            get { return this._longitude; }
		}

	// Methods
        /// <summary>
        /// Returns the radians from the specified degrees.
        /// </summary>
        /// <param name="degrees">The degree value to convert into radians.</param>
        /// <returns>The radians value from the specified degrees.</returns>
        private static Double ConvertDegreesToRadians(Double degrees) {
            return Math.PI / 180D * degrees;
        }
		/// <summary>
		/// Determines whether the specified <see cref="T:Object"/> is equal to the current <see cref="T:GeoCoordinate"/>.
		/// </summary>
		/// <param name="obj">The <see cref="T:Object"/> to compare with the current <see cref="T:GeoCoordinate"/>.</param>
		/// <returns><c>true</c> if the specified <see cref="T:Object"/> is equal to the current <see cref="T:GeoCoordinate"/>; otherwise, <c>false</c>.</returns>
		public override Boolean Equals(Object obj) {
			if (obj is GeoCoordinate) {
				return this.Equals((GeoCoordinate)obj);
			}
			return false;
		}
		/// <summary>
		/// Indicates whether the current object is equal to another object of the same type.
		/// </summary>
		/// <param name="other">An object to compare with this object.</param>
		/// <returns><c>true</c> if the current object is equal to the other parameter; otherwise, <c>false</c>.</returns>
		public Boolean Equals(GeoCoordinate other) {
            return this._latitude == other._latitude 
				&& this._longitude == other._longitude;
		}
        /// <summary>
        /// Returns the distance from the <see cref="T:GeoCoordinate"/> specified.
        /// </summary>
        /// <param name="other">The other <see cref="T:GeoCoordinate"/> to use in calculating the distance.</param>
        /// <param name="earthRadius">The <see cref="T:Double"/> that represents the radius of earth in a measure such as kilometers or miles.</param>
        /// <returns>The distance in the measure implied by <paramref name="earthRadius"/> between the two <see cref="T:GeoCoordinate"/> instances.</returns>
        private Double GetDistanceFrom(GeoCoordinate other, Double earthRadius) {
            if (this.Equals(other)) {
                return 0D;
            }
            /* This method is implemented by solving for distance from the Haversine formula.
             * Haversine formula: haversin(d/R) = haversin(lat1 - lat2) + cos(lat1) * cos(lat2) * haversin(long1-long2)
             * Where:
             *  haversin is the function, haversin(x) = sin^2(x/2)
             *  d is the distance between the GeoCoordinate objects along a great circle of Earth.
             *  R is the radius of Earth.
             */
            Double deltaLatitude = GeoCoordinate.ConvertDegreesToRadians(other._latitude - this._latitude);
			Double deltaLongitude = GeoCoordinate.ConvertDegreesToRadians(other._longitude - this._longitude);
            Double sinHalfDeltaLatitude = Math.Sin(deltaLatitude / 2);
            Double sinHalfDeltaLongitude = Math.Sin(deltaLongitude / 2);
            Double haversineResult = sinHalfDeltaLatitude * sinHalfDeltaLatitude +
				Math.Cos(GeoCoordinate.ConvertDegreesToRadians(this._latitude)) * Math.Cos(GeoCoordinate.ConvertDegreesToRadians(other._latitude)) *
                sinHalfDeltaLongitude * sinHalfDeltaLongitude;
            return Math.Asin(Math.Min(1D, Math.Sqrt(haversineResult))) * earthRadius * 2D;
        }
		/// <summary>
		/// Returns the distance in kilometers from the <see cref="T:GeoCoordinate"/> specified.
		/// </summary>
		/// <param name="other">The other <see cref="T:GeoCoordinate"/> to use in calculating the distance.</param>
		/// <returns>The distance in kilometers between the two <see cref="T:GeoCoordinate"/> instances.</returns>
		public Double GetDistanceInKilometersFrom(GeoCoordinate other) {
            return this.GetDistanceFrom(other, EarthRadiusInKilometers);
		}
		/// <summary>   
		/// Returns the distance in miles from the <see cref="T:GeoCoordinate"/> specified.
		/// </summary>
		/// <param name="other">The other <see cref="T:GeoCoordinate"/> to use in calculating the distance.</param>
		/// <returns>The distance in miles between the two <see cref="T:GeoCoordinate"/> instances.</returns>
		public Double GetDistanceInMilesFrom(GeoCoordinate other) {
            return this.GetDistanceFrom(other, EarthRadiusInMiles);
		}
		/// <summary>
		/// Serves as a hash function for a particular type. <see cref="M:Object.GetHashCode"/> is suitable for use in hashing algorithms and data structures like a hash table.
		/// </summary>
		/// <returns>A hash code for the current <see cref="T:GeoCoordinate"/>.</returns>
		public override Int32 GetHashCode() {
            return this._latitude.GetHashCode() ^ this._longitude.GetHashCode();
		}
		/// <summary>
		/// Returns a value indicating if this instance is east of the <see cref="T:GeoCoordinate"/> specified.
		/// </summary>
		/// <param name="other">The <see cref="T:GeoCoordinate"/> against which to compare this instance.</param>
		/// <returns><c>true</c> if this instance is east of the <paramref name="other"/>; otherwise, <c>false</c>.</returns>
		public Boolean IsEastOf(GeoCoordinate other) {
            return this._longitude > other._longitude;
		}
		/// <summary>
		/// Returns a value indicating if this instance is north of the <see cref="T:GeoCoordinate"/> specified.
		/// </summary>
		/// <param name="other">The <see cref="T:GeoCoordinate"/> against which to compare this instance.</param>
		/// <returns><c>true</c> if this instance is north of the <paramref name="other"/>; otherwise, <c>false</c>.</returns>
		public Boolean IsNorthOf(GeoCoordinate other) {
            return this._latitude > other._latitude;
		}
		/// <summary>
		/// Returns a value indicating if this instance is south of the <see cref="T:GeoCoordinate"/> specified.
		/// </summary>
		/// <param name="other">The <see cref="T:GeoCoordinate"/> against which to compare this instance.</param>
		/// <returns><c>true</c> if this instance is south of the <paramref name="other"/>; otherwise, <c>false</c>.</returns>
		public Boolean IsSouthOf(GeoCoordinate other) {
            return this._latitude < other._latitude;
		}
		/// <summary>
		/// Returns a value indicating if this instance is west of the <see cref="T:GeoCoordinate"/> specified.
		/// </summary>
		/// <param name="other">The <see cref="T:GeoCoordinate"/> against which to compare this instance.</param>
		/// <returns><c>true</c> if this instance is west of the <paramref name="other"/>; otherwise, <c>false</c>.</returns>
		public Boolean IsWestOf(GeoCoordinate other) {
            return this._longitude < other._longitude;
		}
		/// <summary>
		/// Returns a <see cref="T:String"/> that represents the current <see cref="T:GeoCoordinate"/>.
		/// </summary>
		/// <returns>A <see cref="T:String"/> that represents the current <see cref="T:GeoCoordinate"/>.</returns>
		public override String ToString() {
			return this.ToString(GeneralFormat, null);
		}
		/// <summary>
		/// Returns a <see cref="T:String"/> that represents the current <see cref="T:GeoCoordinate"/>.
		/// </summary>
		/// <param name="format">The format string to use.</param>
		/// <returns>A <see cref="T:String"/> that represents the current <see cref="T:GeoCoordinate"/>.</returns>
		/// <exception cref="System.FormatException"><paramref name="format"/> is not a valid format.</exception>
		public String ToString(String format) {
			return this.ToString(format, null);
		}
		/// <summary>
		/// Returns a <see cref="T:String"/> that represents the current <see cref="T:GeoCoordinate"/>.
		/// </summary>
		/// <param name="provider">Provides formatting information.</param>
		/// <returns>A <see cref="T:String"/> that represents the current <see cref="T:GeoCoordinate"/>.</returns>
		public String ToString(IFormatProvider provider) {
			return this.ToString(GeneralFormat, provider);
		}
		/// <summary>
		/// Returns a <see cref="T:String"/> that represents the current <see cref="T:GeoCoordinate"/>.
		/// </summary>
		/// <param name="format">The format string to use.</param>
		/// <param name="formatProvider">Provides formatting information.</param>
		/// <returns>A <see cref="T:String"/> that represents the current <see cref="T:GeoCoordinate"/>.</returns>
		/// <exception cref="System.FormatException"><paramref name="format"/> is not a valid format.</exception>
		[SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase", Justification = "Using lowercase characters stays with the formatting conventions established by the .NET Framework.")]
		public String ToString(String format, IFormatProvider formatProvider) {
            String formatString = null;
            if (format != null) {
                format = format.ToLowerInvariant();
            }
            switch (format) {
                case SetFormat:
                    formatString = "{{ {0:g}, {1:g} }}";
                    break;
                case GeneralFormat:
                case null:
                    formatString = "{0:g}, {1:g}";
                    break;
                default:
                    throw new FormatException();
            }
            return String.Format(formatProvider, formatString, this._latitude, this._longitude);
    	}

	// Operators
		/// <summary>
		/// Returns a value indicating if the two instances are equal.
		/// </summary>
		/// <param name="objA">The first object to compare.</param>
		/// <param name="objB">The second object to compare.</param>
		/// <returns><c>true</c> if the two objects are equal; otherwise, <c>false</c>.</returns>
		public static Boolean operator ==(GeoCoordinate objA, GeoCoordinate objB) {
            return objA.Equals(objB);
		}
		/// <summary>
		/// Returns a value indicating if the two instances are not equal.
		/// </summary>
		/// <param name="objA">The first object to compare.</param>
		/// <param name="objB">The second object to compare.</param>
		/// <returns><c>true</c> if the two objects are not equal; otherwise, <c>false</c>.</returns>
		public static Boolean operator !=(GeoCoordinate objA, GeoCoordinate objB) {
            return !objA.Equals(objB);
		}
	}
}
