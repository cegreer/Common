using Vizistata;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Vizistata {
    /// <summary>
    ///This is a test class for DomainNameTest and is Int32ended
    ///to contain all DomainNameTest Unit Tests
    ///</summary>
    [TestClass()]
    public class DomainNameTests {
        #region Test Class Implementation

        private TestContext _testContextInstance;

        /// <summary>
		/// Initializes a new instance of the <see cref="T:DomainNameTests"/> class.
		/// </summary>
		public DomainNameTests() : base() { }

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
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
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion
        #endregion Test Class Implementation

    // Constructor Tests
        [TestMethod()]
        [Description(".ctor(String, String, String) constructor for the optimal path.")]
        public void DomainName_Unit_Constructor1_Optimal() {
            String firstLevelLabel = "com"; 
            String secondLevelLabel = "vizistata";
            String subDomainLabel = "www"; 
            DomainName target = new DomainName(firstLevelLabel, secondLevelLabel, subDomainLabel);
        }
        [TestMethod()]
        [Description(".ctor(String, String, String) constructor when firstLevelLabel is null.")]
        [ExpectedException(typeof(ArgumentNullException))]
        public void DomainName_Unit_Constructor1_FirstLevelIsNull() {
            String firstLevelLabel = null;
            String secondLevelLabel = "vizistata";
            String subDomainLabel = "www";
            DomainName target = new DomainName(firstLevelLabel, secondLevelLabel, subDomainLabel);
        }
        [TestMethod()]
        [Description(".ctor(String, String, String) constructor when secondLevelLabel is null.")]
        [ExpectedException(typeof(ArgumentNullException))]
        public void DomainName_Unit_Constructor1_SecondLevelIsNull() {
            String firstLevelLabel = "com";
            String secondLevelLabel = null;
            String subDomainLabel = "www";
            DomainName target = new DomainName(firstLevelLabel, secondLevelLabel, subDomainLabel);
        }
        [TestMethod()]
        [Description(".ctor(String, String, String) constructor when subDomainLabel is null.")]
        [ExpectedException(typeof(ArgumentException))]
        public void DomainName_Unit_Constructor1_SubDomainIsNull() {
            String firstLevelLabel = "com";
            String secondLevelLabel = "vizistata";
            String subDomainLabel = null;
            DomainName target = new DomainName(firstLevelLabel, secondLevelLabel, subDomainLabel);
        }
        [TestMethod()]
        [Description(".ctor(String, String, String) constructor when firstLevelLabel has length 0.")]
        [ExpectedException(typeof(ArgumentException))]
        public void DomainName_Unit_Constructor1_FirstLevelIsEmpty() {
            String firstLevelLabel = String.Empty;
            String secondLevelLabel = "vizistata";
            String subDomainLabel = "www";
            DomainName target = new DomainName(firstLevelLabel, secondLevelLabel, subDomainLabel);
        }
        [TestMethod()]
        [Description(".ctor(String, String, String) constructor when secondLevelLabel has length 0.")]
        [ExpectedException(typeof(ArgumentException))]
        public void DomainName_Unit_Constructor1_SecondLevelIsEmpty() {
            String firstLevelLabel = "com";
            String secondLevelLabel = String.Empty;
            String subDomainLabel = "www";
            DomainName target = new DomainName(firstLevelLabel, secondLevelLabel, subDomainLabel);
        }
        [TestMethod()]
        [Description(".ctor(String, String, String) constructor when subDomainLabel has length 0.")]
        [ExpectedException(typeof(ArgumentException))]
        public void DomainName_Unit_Constructor1_SubDomainIsEmpty() {
            String firstLevelLabel = "com";
            String secondLevelLabel = "vizistata";
            String subDomainLabel = String.Empty;
            DomainName target = new DomainName(firstLevelLabel, secondLevelLabel, subDomainLabel);
        }
        [TestMethod()]
        [Description(".ctor(String, String, String) constructor with more than 127 labels.")]
        [ExpectedException(typeof(ArgumentException))]
        public void DomainName_Unit_Constructor1_MoreThan127Labels() {
            String firstLevelLabel = "com";
            String secondLevelLabel = "vizistata";
            String [] subDomainLabels = new String[126];
            for (Int32 i = 0; i < 126; i++) {
                subDomainLabels[i] = "A";
            }

            DomainName target = new DomainName(firstLevelLabel, secondLevelLabel, subDomainLabels);
        }
        [TestMethod()]
        [Description(".ctor(String, String, String) constructor with more than 253 characters.")]
        [ExpectedException(typeof(FormatException))]
        public void DomainName_Unit_Constructor1_MoreThan253Chars() {
            String firstLevelLabel = "a";
            String secondLevelLabel = new String('A', 250);
            String subDomainLabel = "a";

            DomainName target = new DomainName(firstLevelLabel, secondLevelLabel, subDomainLabel);
        }
        [TestMethod()]
        [Description(".ctor(String, String, String) constructor with invalid characters.")]
        [ExpectedException(typeof(ArgumentException))]
        public void DomainName_Unit_Constructor1_InvalidChars() {
            String firstLevelLabel = "com";
            String secondLevelLabel = "invalid_label";
            String subDomainLabel = "www";

            DomainName target = new DomainName(firstLevelLabel, secondLevelLabel, subDomainLabel);
        }

		[TestMethod()]
		[Description(".ctor(String, String, params String) constructor for the optimal path.")]
		public void DomainName_Unit_Constructor2_Optimal() {
			String firstLevelLabel = "com";
			String secondLevelLabel = "vizistata";
			String[] subDomainLabels = { "test", "something" };
			new DomainName(firstLevelLabel, secondLevelLabel, subDomainLabels);
		}
		[TestMethod()]
		[Description(".ctor(String, String, params String) constructor when 'subDomainLabels' is a null reference.")]
		public void DomainName_Unit_Constructor2_SubDomainLabelsIsNull() {
			String firstLevelLabel = "com";
			String secondLevelLabel = "vizistata";
			String[] subDomainLabels = null;
			new DomainName(firstLevelLabel, secondLevelLabel, subDomainLabels);
		}

	// Property Tests
		[TestMethod()]
		[Description("AllLabels optimal case.")]
		public void DomainName_Unit_AllLabels_Optimal() {
			String firstLevelLabel = "com";
			String secondLevelLabel = "google";
			String subDomainLabel = "www";
			DomainName target = new DomainName(firstLevelLabel, secondLevelLabel, subDomainLabel);
			IEnumerable<String> labels = target.AllLabels;
			Assert.IsNotNull(labels);

			List<String> labelList = new List<String>(labels);

			Assert.AreEqual(firstLevelLabel, labelList[0]);
			Assert.AreEqual(secondLevelLabel, labelList[1]);
			Assert.AreEqual(subDomainLabel, labelList[2]);
		}

		[TestMethod()]
		[Description("FirstLevelLabel optimal case.")]
		public void DomainName_Unit_FirstLevelLabel_Optimal() {
			String firstLevelLabel = "com";
			String secondLevelLabel = "google";
			String subDomainLabel = "www";
			DomainName target = new DomainName(firstLevelLabel, secondLevelLabel, subDomainLabel);

			Assert.AreEqual(firstLevelLabel, target.FirstLevelLabel);
		}

		[TestMethod()]
		[Description("SecondLevelLabel optimal case.")]
		public void DomainName_Unit_SecondLevelLabel_Optimal() {
			String firstLevelLabel = "com";
			String secondLevelLabel = "google";
			String subDomainLabel = "www";
			DomainName target = new DomainName(firstLevelLabel, secondLevelLabel, subDomainLabel);

			Assert.AreEqual(secondLevelLabel, target.SecondLevelLabel);
		}

		[TestMethod()]
		[Description("SubDomainLabel optimal case.")]
		public void DomainName_Unit_SubDomainLabel_Optimal() {
			String firstLevelLabel = "com";
			String secondLevelLabel = "google";
			String subDomainLabel = "www";
			DomainName target = new DomainName(firstLevelLabel, secondLevelLabel, subDomainLabel);
			IEnumerable<String> actual = target.SubDomainLabels;
			List<String> list = new List<String>(actual);

			Assert.AreEqual(subDomainLabel, list[0]);
		}
       
    // Method Tests
        [TestMethod()]
        [Description("Equals(Object) method when 'obj' is an equivalent DomainName.")]
        public void DomainName_Unit_Equals1_ObjIsEquivalentDomainName() {
            String firstLevelLabel = "com";
            String secondLevelLabel = "vizistata";
            String subDomainLabel = "www";
            DomainName target = new DomainName(firstLevelLabel, secondLevelLabel, subDomainLabel);
            Object obj = new DomainName(firstLevelLabel.ToUpperInvariant(), secondLevelLabel.ToLowerInvariant(), subDomainLabel.ToUpperInvariant());

            Boolean actual = target.Equals(obj);
            Assert.IsTrue(actual);
        }
		[TestMethod()]
		[Description("Equals(Object) method when 'obj' is an equivalent string.")]
		public void DomainName_Unit_Equals1_ObjIsEquivalentString() {
			String firstLevelLabel = "com";
			String secondLevelLabel = "vizistata";
			String subDomainLabel = "www";
			DomainName target = new DomainName(firstLevelLabel, secondLevelLabel, subDomainLabel);
			Object obj = new String[] { subDomainLabel, secondLevelLabel, firstLevelLabel }.Join('.');

			Boolean actual = target.Equals(obj);
			Assert.IsTrue(actual);
		}
		[TestMethod()]
        [Description("Equals(Object) method when 'obj' is same object.")]
        public void DomainName_Unit_Equals1_ObjIsSameAsObject() {
            String firstLevelLabel = "com";
            String secondLevelLabel = "vizistata";
            String subDomainLabel = "www";
            DomainName target = new DomainName(firstLevelLabel, secondLevelLabel, subDomainLabel);
            Object obj = target;

            Boolean actual = target.Equals(obj);
            Assert.IsTrue(actual);
        }
        [TestMethod()]
        [Description("Equals(Object) method when 'obj' is not equivalent to the target.")]
        public void DomainName_Unit_Equals1_ObjIsUnequivalentDomainName1() {
            String firstLevelLabel = "com";
            String secondLevelLabel = "vizistata";
            String subDomainLabel = "www";
            DomainName target = new DomainName(firstLevelLabel, secondLevelLabel, subDomainLabel);
            firstLevelLabel = "net";
            Object obj = new DomainName(firstLevelLabel, secondLevelLabel, subDomainLabel);

            Boolean actual = target.Equals(obj);
            Assert.IsFalse(actual);
        }
        [TestMethod()]
        [Description("Equals(Object) method when 'obj' is not equivalent to the target.")]
		public void DomainName_Unit_Equals1_ObjIsUnequivalentDomainName2() {
            String firstLevelLabel = "com";
            String secondLevelLabel = "vizistata";
            String subDomainLabel = "www";
            DomainName target = new DomainName(firstLevelLabel, secondLevelLabel, subDomainLabel);
            secondLevelLabel = "google";
            Object obj = new DomainName(firstLevelLabel, secondLevelLabel, subDomainLabel);

            Boolean actual = target.Equals(obj);
            Assert.IsFalse(actual);
        }
        [TestMethod()]
        [Description("Equals(Object) method when 'obj' is not equivalent to the target.")]
		public void DomainName_Unit_Equals1_ObjIsUnequivalentDomainName3() {
            String firstLevelLabel = "com";
            String secondLevelLabel = "vizistata";
            String subDomainLabel = "www";
            DomainName target = new DomainName(firstLevelLabel, secondLevelLabel, subDomainLabel);
            subDomainLabel = "deepblue";
            Object obj = new DomainName(firstLevelLabel, secondLevelLabel, subDomainLabel);

            Boolean actual = target.Equals(obj);
            Assert.IsFalse(actual);
        }
        [TestMethod()]
        [Description("Equals(Object) method when 'obj' is not equivalent to the target.")]
		public void DomainName_Unit_Equals1_ObjIsUnequivalentDomainName4() {
            String firstLevelLabel = "com";
            String secondLevelLabel = "vizistata";
            String subDomainLabel = "www";
            DomainName target = new DomainName(firstLevelLabel, secondLevelLabel, subDomainLabel);
            String [] subDomainLabels = {"deepblue", "something"};
            Object obj = new DomainName(firstLevelLabel, secondLevelLabel, subDomainLabels);

            Boolean actual = target.Equals(obj);
            Assert.IsFalse(actual);
        }
		[TestMethod()]
		[Description("Equals(Object) method when 'obj' is not equivalent to the target.")]
		public void DomainName_Unit_Equals1_ObjIsUnequivalentString() {
			String firstLevelLabel = "com";
			String secondLevelLabel = "vizistata";
			String subDomainLabel = "www";
			DomainName target = new DomainName(firstLevelLabel, secondLevelLabel, subDomainLabel);
			Object obj = new String[] { subDomainLabel, secondLevelLabel, firstLevelLabel }.Join(String.Empty);

			Boolean actual = target.Equals(obj);
			Assert.IsFalse(actual);
		}
        [TestMethod()]
        [Description("Equals(Object) method when 'obj' is null.")]
        public void DomainName_Unit_Equals1_ObjIsNull() {
            String firstLevelLabel = "com";
            String secondLevelLabel = "vizistata";
            String subDomainLabel = "www";
            DomainName target = new DomainName(firstLevelLabel, secondLevelLabel, subDomainLabel);
            Object obj = null;

            Boolean actual = target.Equals(obj);
            Assert.IsFalse(actual);
        }

		[TestMethod()]
		[Description("Equals(DomainName) method when 'other' is an equivalent DomainName.")]
		public void DomainName_Unit_Equals2_OtherIsEquivalentDomainName() {
			String firstLevelLabel = "com";
			String secondLevelLabel = "vizistata";
			String subDomainLabel = "www";
			DomainName target = new DomainName(firstLevelLabel, secondLevelLabel, subDomainLabel);
			DomainName other = new DomainName(firstLevelLabel.ToUpperInvariant(), secondLevelLabel.ToLowerInvariant(), subDomainLabel.ToUpperInvariant());

			Boolean actual = target.Equals(other);
			Assert.IsTrue(actual);
		}
		[TestMethod()]
		[Description("Equals(DomainName) method when 'other' is same object.")]
		public void DomainName_Unit_Equals2_OtherIsSameAsObject() {
			String firstLevelLabel = "com";
			String secondLevelLabel = "vizistata";
			String subDomainLabel = "www";
			DomainName target = new DomainName(firstLevelLabel, secondLevelLabel, subDomainLabel);
			DomainName other = target;

			Boolean actual = target.Equals(other);
			Assert.IsTrue(actual);
		}
		[TestMethod()]
		[Description("Equals(DomainName) method when 'other' is not equivalent to the target.")]
		public void DomainName_Unit_Equals2_OtherIsUnequivalentDomainName1() {
			String secondLevelLabel = "vizistata";
			String subDomainLabel = "www";
			DomainName target = new DomainName("com", secondLevelLabel, subDomainLabel);
			DomainName other = new DomainName("net", secondLevelLabel, subDomainLabel);

			Boolean actual = target.Equals(other);
			Assert.IsFalse(actual);
		}
		[TestMethod()]
		[Description("Equals(DomainName) method when 'other' is not equivalent to the target.")]
		public void DomainName_Unit_Equals2_OtherIsUnequivalentDomainName2() {
			String firstLevelLabel = "com";
			String subDomainLabel = "www";
			DomainName target = new DomainName(firstLevelLabel, "vizistata", subDomainLabel);
			DomainName other = new DomainName(firstLevelLabel, "google", subDomainLabel);

			Boolean actual = target.Equals(other);
			Assert.IsFalse(actual);
		}
		[TestMethod()]
		[Description("Equals(DomainName) method when 'other' is not equivalent to the target.")]
		public void DomainName_Unit_Equals2_OtherIsUnequivalentDomainName3() {
			String firstLevelLabel = "com";
			String secondLevelLabel = "vizistata";
			DomainName target = new DomainName(firstLevelLabel, secondLevelLabel, "www");
			DomainName other = new DomainName(firstLevelLabel, secondLevelLabel, "deepblue");

			Boolean actual = target.Equals(other);
			Assert.IsFalse(actual);
		}
		[TestMethod()]
		[Description("Equals(DomainName) method when 'other' is not equivalent to the target.")]
		public void DomainName_Unit_Equals2_OtherIsUnequivalentDomainName4() {
			String firstLevelLabel = "com";
			String secondLevelLabel = "vizistata";
			DomainName target = new DomainName(firstLevelLabel, secondLevelLabel, "www");
			DomainName other = new DomainName(firstLevelLabel, secondLevelLabel, "deepblue", "something");

			Boolean actual = target.Equals(other);
			Assert.IsFalse(actual);
		}
		[TestMethod()]
		[Description("Equals(DomainName) method when 'other' is null.")]
		public void DomainName_Unit_Equals2_OtherIsNull() {
			String firstLevelLabel = "com";
			String secondLevelLabel = "vizistata";
			String subDomainLabel = "www";
			DomainName target = new DomainName(firstLevelLabel, secondLevelLabel, subDomainLabel);
			DomainName other = null;

			Boolean actual = target.Equals(other);
			Assert.IsFalse(actual);
		}

		[TestMethod()]
		[Description("Equals(String) method when 'other' is an equivalent string.")]
		public void DomainName_Unit_Equals3_OtherIsEquivalentString() {
			String firstLevelLabel = "com";
			String secondLevelLabel = "vizistata";
			String subDomainLabel = "www";
			DomainName target = new DomainName(firstLevelLabel, secondLevelLabel, subDomainLabel);
			String other = new String[] { subDomainLabel, secondLevelLabel, firstLevelLabel }.Join('.');

			Boolean actual = target.Equals(other);
			Assert.IsTrue(actual);
		}
		[TestMethod()]
		[Description("Equals(String) method when 'other' is not equivalent to the target.")]
		public void DomainName_Unit_Equals3_OtherIsUnequivalentString() {
			String firstLevelLabel = "com";
			String secondLevelLabel = "vizistata";
			String subDomainLabel = "www";
			DomainName target = new DomainName(firstLevelLabel, secondLevelLabel, subDomainLabel);
			String other = new String[] { subDomainLabel, secondLevelLabel, firstLevelLabel }.Join(String.Empty);

			Boolean actual = target.Equals(other);
			Assert.IsFalse(actual);
		}
		[TestMethod()]
		[Description("Equals(String) method when 'other' is null.")]
		public void DomainName_Unit_Equals3_OtherIsNull() {
			String firstLevelLabel = "com";
			String secondLevelLabel = "vizistata";
			String subDomainLabel = "www";
			DomainName target = new DomainName(firstLevelLabel, secondLevelLabel, subDomainLabel);
			String other = null;

			Boolean actual = target.Equals(other);
			Assert.IsFalse(actual);
		}

		[TestMethod()]
        [Description("GetHashCode() method when compared to the same instance.")]
		public void DomainName_Unit_GetHashCode_SameInstances() {
            String firstLevelLabel = "com";
            String secondLevelLabel = "vizistata";
            String subDomainLabel = "www";
            DomainName target = new DomainName(firstLevelLabel, secondLevelLabel, subDomainLabel);
            DomainName other = target;

            Assert.AreEqual(target.GetHashCode(), other.GetHashCode());
        }
        [TestMethod()]
        [Description("GetHashCode() method when compared to an equivalent instance.")]
        public void DomainName_Unit_GetHashCode_EquivalentInstances() {
            String firstLevelLabel = "com";
            String secondLevelLabel = "vizistata";
            String subDomainLabel = "www";
            DomainName target = new DomainName(firstLevelLabel, secondLevelLabel, subDomainLabel);
            DomainName other = new DomainName(firstLevelLabel.ToUpperInvariant(), secondLevelLabel.ToLowerInvariant(), subDomainLabel);

            Assert.AreEqual(target.GetHashCode(), other.GetHashCode());
        }
        [TestMethod()]
        [Description("GetHashCode() method when compared to an object with a different first level label.")]
        public void DomainName_Unit_GetHashCode_DifferentFirstLevelLabel() {
            String firstLevelLabel = "com";
            String secondLevelLabel = "vizistata";
            String subDomainLabel = "www";
            DomainName target = new DomainName(firstLevelLabel, secondLevelLabel, subDomainLabel);
            firstLevelLabel = "net";
            DomainName other = new DomainName(firstLevelLabel, secondLevelLabel, subDomainLabel);

            Assert.AreNotEqual(target.GetHashCode(), other.GetHashCode());
        }
        [TestMethod()]
        [Description("GetHashCode() method when compared to an object with a different second level label.")]
        public void DomainName_Unit_GetHashCode_DifferentSecondLevelLabel() {
            String firstLevelLabel = "com";
            String secondLevelLabel = "vizistata";
            String subDomainLabel = "www";
            DomainName target = new DomainName(firstLevelLabel, secondLevelLabel, subDomainLabel);
            secondLevelLabel = "wikipedia";
            DomainName other = new DomainName(firstLevelLabel, secondLevelLabel, subDomainLabel);

            Assert.AreNotEqual(target.GetHashCode(), other.GetHashCode());
        }
        [TestMethod()]
        [Description("GetHashCode() method when compared to an object with a different sub domain.")]
        public void DomainName_Unit_GetHashCode_DifferentSubDomainLabel() {
            String firstLevelLabel = "com";
            String secondLevelLabel = "vizistata";
            String subDomainLabel = "www";
            DomainName target = new DomainName(firstLevelLabel, secondLevelLabel, subDomainLabel);
            subDomainLabel = "download";
            DomainName other = new DomainName(firstLevelLabel, secondLevelLabel, subDomainLabel);

            Assert.AreNotEqual(target.GetHashCode(), other.GetHashCode());
        }

		[TestMethod()]
		[Description("Parse(String) method optimal case.")]
		public void DomainName_Unit_Parse_Optimal() {
			String value = "www.parivedasolutions.com";
			DomainName actual = DomainName.Parse(value);

			String firstLevelLabel = "com";
			String secondLevelLabel = "parivedasolutions";
			String subDomainLabel = "www";
			DomainName expected = new DomainName(firstLevelLabel, secondLevelLabel, subDomainLabel);

			Assert.AreEqual(expected, actual);
		}
		[TestMethod()]
		[Description("Parse(String) method and ToString() optimal case.")]
		public void DomainName_Unit_Parse_ToString_OptimalCase() {
			String value = "www.parivedasolutions.com";
			DomainName actual = DomainName.Parse(value);
			Assert.AreEqual(value, actual.ToString());
		}
		[TestMethod()]
		[Description("Parse(String) method when 'value' is invalid.")]
		[ExpectedException(typeof(FormatException))]
		public void DomainName_Unit_Parse_ValueIsInvalid() {
			String value = "www.invalid_name.com";
			DomainName.Parse(value);
		}
		[TestMethod()]
		[Description("Parse(String) method when 'value' is a null reference.")]
        [ExpectedException(typeof(ArgumentNullException))]
        public void DomainName_Unit_Parse_ValueIsNull() {
            String value = null; 
            DomainName.Parse(value);
        }
		[TestMethod()]
		[Description("Parse(String) method when 'value' is the empty string.")]
        [ExpectedException(typeof(FormatException))]
		public void DomainName_Unit_Parse_ValueIsEmpty() {
            String value = String.Empty;
            DomainName.Parse(value);
        }
		[TestMethod()]
		[Description("Parse(String) method when the length of 'value' is greater than 253 characters.")]
        [ExpectedException(typeof(FormatException))]
		public void DomainName_Unit_Parse_ValueIsTooLong() {
            String value = new String('a', 250) + ".b.c";
            DomainName.Parse(value);
        }
		[TestMethod()]
		[Description("Parse(String) method when 'value' has more than 127 labels.")]
		[ExpectedException(typeof(FormatException))]
		public void DomainName_Unit_Parse_ValueHasTooManyLabels() {
			String value = new String('.', 127);
			DomainName.Parse(value);
		}
		[TestMethod()]
		[Description("Parse(String) method when 'value' has an empty label.")]
		[ExpectedException(typeof(FormatException))]
		public void DomainName_Unit_Parse_ValueHasEmptyLabel() {
			String value = "www..parivedasolutions.com";
			DomainName.Parse(value);
		}

        [TestMethod()]
        [Description("ToString() method optimal case.")]
        public void DomainName_Unit_ToString_Optimal() {
            String firstLevelLabel = "com";
            String secondLevelLabel = "vizistata";
            String subDomainLabel = "www";
            DomainName target = new DomainName(firstLevelLabel, secondLevelLabel, subDomainLabel); 
            String expected = "www.vizistata.com"; 
            String actual = target.ToString();

            Assert.AreEqual(expected, actual);
        }

		[TestMethod()]
		[Description("TryParse(String, &DomainName) method for the optimal path.")]
		public void DomainName_Unit_TryParse_Optimal() {
			String value = "www.abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-.com";
			DomainName result;

			Boolean actual = DomainName.TryParse(value, out result);
			Assert.IsTrue(actual);
			Assert.IsNotNull(result);
			Assert.AreEqual(value, result.ToString());
		}
		[TestMethod()]
		[Description("TryParse(String, &DomainName) method when 'value' is invalid.")]
		public void DomainName_Unit_TryParse_ValueIsInvalid() {
			String value = "www.invalid_name.com";
			DomainName result;

			Boolean actual = DomainName.TryParse(value, out result);
			Assert.IsFalse(actual);
			Assert.IsNull(result);
		}
		[TestMethod()]
		[Description("TryParse(String, &DomainName) method when 'value' is a null reference.")]
		public void DomainName_Unit_TryParse_ValueIsNull() {
			String value = null;
			DomainName result;

			Boolean actual = DomainName.TryParse(value, out result);
			Assert.IsFalse(actual);
			Assert.IsNull(result);
		}
		[TestMethod()]
		[Description("TryParse(String, &DomainName) method when 'value' is empty.")]
		public void DomainName_Unit_TryParse_ValueIsEmpty() {
			String value = String.Empty;
			DomainName result;

			Boolean actual = DomainName.TryParse(value, out result);
			Assert.IsFalse(actual);
			Assert.IsNull(result);
		}
		[TestMethod()]
		[Description("TryParse(String, &DomainName) method when 'value' has a length that is too long.")]
		public void DomainName_Unit_TryParse_ValueIsTooLong() {
			String value = new String('a', 250) + ".b.c";
			DomainName result;

			Boolean actual = DomainName.TryParse(value, out result);
			Assert.IsFalse(actual);
			Assert.IsNull(result);
		}
		[TestMethod()]
		[Description("TryParse(String, &DomainName) method when 'value' has more than 127 labels.")]
		public void DomainName_Unit_TryParse_ValueHasTooManyLabels() {
			String value = new String('.', 127);
			DomainName result;

			Boolean actual = DomainName.TryParse(value, out result);
			Assert.IsFalse(actual);
			Assert.IsNull(result);
		}
		[TestMethod()]
		[Description("TryParse(String, &DomainName) method when 'value' has an empty label.")]
		public void DomainName_Unit_TryParse_ValueHasEmptyLabel() {
			String value = "www..parivedasolutions.com";
			DomainName result;

			Boolean actual = DomainName.TryParse(value, out result);
			Assert.IsFalse(actual);
			Assert.IsNull(result);
		}

	// Operator Tests
		[TestMethod()]
		[Description("DomainName == DomainName operator when the objects point to the same reference.")]
		public void DomainName_Unit_EqualityOperator1_SameReferences() {
			DomainName objA = new DomainName("com", "parivedasolutions", "www");
			DomainName objB = objA;
			Boolean expected = true;
			Boolean actual = objA == objB;
			Assert.AreEqual(expected, actual);
		}
		[TestMethod()]
		[Description("DomainName == DomainName operator when the objects are equivalent.")]
		public void DomainName_Unit_EqualityOperator1_EquivalentObjects() {
			DomainName objA = new DomainName("com", "parivedasolutions", "www");
			DomainName objB = new DomainName("com", "parivedasolutions", "www");
			Boolean expected = true;
			Boolean actual = objA == objB;
			Assert.AreEqual(expected, actual);
		}
		[TestMethod()]
		[Description("DomainName == DomainName operator when the objects are not equivalent.")]
		public void DomainName_Unit_EqualityOperator1_UnequivalentObjects1() {
			DomainName objA = new DomainName("com", "parivedasolutions", "www");
			DomainName objB = new DomainName("net", "parivedasolutions", "www");
			Boolean expected = false;
			Boolean actual = objA == objB;
			Assert.AreEqual(expected, actual);
		}
		[TestMethod()]
		[Description("DomainName == DomainName operator when the objects are not equivalent.")]
		public void DomainName_Unit_EqualityOperator1_UnequivalentObjects2() {
			DomainName objA = new DomainName("com", "parivedasolutions", "www");
			DomainName objB = new DomainName("com", "google", "www");
			Boolean expected = false;
			Boolean actual = objA == objB;
			Assert.AreEqual(expected, actual);
		}
		[TestMethod()]
		[Description("DomainName == DomainName operator when the objects are not equivalent.")]
		public void DomainName_Unit_EqualityOperator1_UnequivalentObjects3() {
			DomainName objA = new DomainName("com", "parivedasolutions", "www");
			DomainName objB = new DomainName("com", "parivedasolutions", "deepblue");
			Boolean expected = false;
			Boolean actual = objA == objB;
			Assert.AreEqual(expected, actual);
		}
		[TestMethod()]
		[Description("DomainName == DomainName operator when 'objA' is a null reference.")]
		public void DomainName_Unit_EqualityOperator1_ObjAIsNull() {
			DomainName objA = null;
			DomainName objB = new DomainName("com", "parivedasolutions", "www");
			Boolean expected = false;
			Boolean actual = objA == objB;
			Assert.AreEqual(expected, actual);
		}
		[TestMethod()]
		[Description("DomainName == DomainName operator when 'objB' is a null reference.")]
		public void DomainName_Unit_EqualityOperator1_ObjBIsNull() {
			DomainName objA = new DomainName("com", "parivedasolutions", "www");
			DomainName objB = null;
			Boolean expected = false;
			Boolean actual = objA == objB;
			Assert.AreEqual(expected, actual);
		}
		[TestMethod()]
		[Description("DomainName == DomainName operator when both objects are null references.")]
		public void DomainName_Unit_EqualityOperator1_BothObjectsAreNull() {
			DomainName objA = null;
			DomainName objB = null;
			Boolean expected = true;
			Boolean actual = objA == objB;
			Assert.AreEqual(expected, actual);
		}

		[TestMethod()]
		[Description("DomainName == String operator when the objects are equivalent.")]
		public void DomainName_Unit_EqualityOperator2_EquivalentObjects() {
			DomainName objA = new DomainName("com", "parivedasolutions", "www");
			String objB = "www.parivedasolutions.com";
			Boolean expected = true;
			Boolean actual = objA == objB;
			Assert.AreEqual(expected, actual);
		}
		[TestMethod()]
		[Description("DomainName == String operator when the objects are not equivalent.")]
		public void DomainName_Unit_EqualityOperator2_UnequivalentObjects() {
			DomainName objA = new DomainName("com", "parivedasolutions", "www");
			String objB = "www.google.com";
			Boolean expected = false;
			Boolean actual = objA == objB;
			Assert.AreEqual(expected, actual);
		}
		[TestMethod()]
		[Description("DomainName == String operator when 'objA' is a null reference.")]
		public void DomainName_Unit_EqualityOperator2_ObjAIsNull() {
			DomainName objA = null;
			String objB = "www.parivedasolutions.com";
			Boolean expected = false;
			Boolean actual = objA == objB;
			Assert.AreEqual(expected, actual);
		}
		[TestMethod()]
		[Description("DomainName == String operator when 'objB' is a null reference.")]
		public void DomainName_Unit_EqualityOperator2_ObjBIsNull() {
			DomainName objA = new DomainName("com", "parivedasolutions", "www");
			String objB = null;
			Boolean expected = false;
			Boolean actual = objA == objB;
			Assert.AreEqual(expected, actual);
		}
		[TestMethod()]
		[Description("DomainName == String operator when both objects are null references.")]
		public void DomainName_Unit_EqualityOperator2_BothObjectsAreNull() {
			DomainName objA = null;
			String objB = null;
			Boolean expected = true;
			Boolean actual = objA == objB;
			Assert.AreEqual(expected, actual);
		}

		[TestMethod()]
        [Description("String == DomainName operator when the objects are equivalent.")]
        public void DomainName_Unit_EqualityOperator3_EquivalentObjects() {
            String objA = "www.parivedasolutions.com"; 
            DomainName objB = new DomainName("com", "parivedasolutions", "www"); 
            Boolean expected = true; 
            Boolean actual = objA == objB;
            Assert.AreEqual(expected, actual);
        }
        [TestMethod()]
		[Description("String == DomainName operator when the objects are not equivalent.")]
		public void DomainName_Unit_EqualityOperator3_UnequivalentObjects() {
            String objA = "www.google.com";
            DomainName objB = new DomainName("com", "parivedasolutions", "www");
            Boolean expected = false;
            Boolean actual = objA == objB;
            Assert.AreEqual(expected, actual);
        }
		[TestMethod()]
		[Description("String == DomainName operator when 'objA' is a null reference.")]
		public void DomainName_Unit_EqualityOperator3_ObjAIsNull() {
			String objA = null;
			DomainName objB = new DomainName("com", "parivedasolutions", "www");
			Boolean expected = false;
			Boolean actual = objA == objB;
			Assert.AreEqual(expected, actual);
		}
		[TestMethod()]
		[Description("String == DomainName operator when 'objB' is a null reference.")]
		public void DomainName_Unit_EqualityOperator3_ObjBIsNull() {
			String objA = "www.parivedasolutions.com";
			DomainName objB = null;
			Boolean expected = false;
			Boolean actual = objA == objB;
			Assert.AreEqual(expected, actual);
		}
		[TestMethod()]
		[Description("String == DomainName operator when both objects are null references.")]
		public void DomainName_Unit_EqualityOperator3_BothObjectsAreNull() {
			String objA = null;
			DomainName objB = null;
			Boolean expected = true;
			Boolean actual = objA == objB;
			Assert.AreEqual(expected, actual);
		}

		[TestMethod()]
		[Description("DomainName != DomainName operator when the objects point to the same reference.")]
		public void DomainName_Unit_InequalityOperator1_SameReferences() {
			DomainName objA = new DomainName("com", "parivedasolutions", "www");
			DomainName objB = objA;
			Boolean expected = false;
			Boolean actual = objA != objB;
			Assert.AreEqual(expected, actual);
		}
		[TestMethod()]
		[Description("DomainName != DomainName operator when the objects are equivalent.")]
		public void DomainName_Unit_InequalityOperator1_EquivalentObjects() {
			DomainName objA = new DomainName("com", "parivedasolutions", "www");
			DomainName objB = new DomainName("com", "parivedasolutions", "www");
			Boolean expected = false;
			Boolean actual = objA != objB;
			Assert.AreEqual(expected, actual);
		}
		[TestMethod()]
		[Description("DomainName != DomainName operator when the objects are not equivalent.")]
		public void DomainName_Unit_InequalityOperator1_UnequivalentObjects1() {
			DomainName objA = new DomainName("com", "parivedasolutions", "www");
			DomainName objB = new DomainName("net", "parivedasolutions", "www");
			Boolean expected = true;
			Boolean actual = objA != objB;
			Assert.AreEqual(expected, actual);
		}
		[TestMethod()]
		[Description("DomainName != DomainName operator when the objects are not equivalent.")]
		public void DomainName_Unit_InequalityOperator1_UnequivalentObjects2() {
			DomainName objA = new DomainName("com", "parivedasolutions", "www");
			DomainName objB = new DomainName("com", "google", "www");
			Boolean expected = true;
			Boolean actual = objA != objB;
			Assert.AreEqual(expected, actual);
		}
		[TestMethod()]
		[Description("DomainName != DomainName operator when the objects are not equivalent.")]
		public void DomainName_Unit_InequalityOperator1_UnequivalentObjects3() {
			DomainName objA = new DomainName("com", "parivedasolutions", "www");
			DomainName objB = new DomainName("com", "parivedasolutions", "deepblue");
			Boolean expected = true;
			Boolean actual = objA != objB;
			Assert.AreEqual(expected, actual);
		}
		[TestMethod()]
		[Description("DomainName != DomainName operator when 'objA' is a null reference.")]
		public void DomainName_Unit_InequalityOperator1_ObjAIsNull() {
			DomainName objA = null;
			DomainName objB = new DomainName("com", "parivedasolutions", "www");
			Boolean expected = true;
			Boolean actual = objA != objB;
			Assert.AreEqual(expected, actual);
		}
		[TestMethod()]
		[Description("DomainName != DomainName operator when 'objB' is a null reference.")]
		public void DomainName_Unit_InequalityOperator1_ObjBIsNull() {
			DomainName objA = new DomainName("com", "parivedasolutions", "www");
			DomainName objB = null;
			Boolean expected = true;
			Boolean actual = objA != objB;
			Assert.AreEqual(expected, actual);
		}
		[TestMethod()]
		[Description("DomainName != DomainName operator when both objects are null references.")]
		public void DomainName_Unit_InequalityOperator1_BothObjectsAreNull() {
			DomainName objA = null;
			DomainName objB = null;
			Boolean expected = false;
			Boolean actual = objA != objB;
			Assert.AreEqual(expected, actual);
		}

		[TestMethod()]
		[Description("DomainName != String operator when the objects are equivalent.")]
		public void DomainName_Unit_InequalityOperator2_EquivalentObjects() {
			DomainName objA = new DomainName("com", "parivedasolutions", "www");
			String objB = "www.parivedasolutions.com";
			Boolean expected = false;
			Boolean actual = objA != objB;
			Assert.AreEqual(expected, actual);
		}
		[TestMethod()]
		[Description("DomainName != String operator when the objects are not equivalent.")]
		public void DomainName_Unit_InequalityOperator2_UnequivalentObjects() {
			DomainName objA = new DomainName("com", "parivedasolutions", "www");
			String objB = "www.google.com";
			Boolean expected = true;
			Boolean actual = objA != objB;
			Assert.AreEqual(expected, actual);
		}
		[TestMethod()]
		[Description("DomainName != String operator when 'objA' is a null reference.")]
		public void DomainName_Unit_InequalityOperator2_ObjAIsNull() {
			DomainName objA = null;
			String objB = "www.parivedasolutions.com";
			Boolean expected = true;
			Boolean actual = objA != objB;
			Assert.AreEqual(expected, actual);
		}
		[TestMethod()]
		[Description("DomainName != String operator when 'objB' is a null reference.")]
		public void DomainName_Unit_InequalityOperator2_ObjBIsNull() {
			DomainName objA = new DomainName("com", "parivedasolutions", "www");
			String objB = null;
			Boolean expected = true;
			Boolean actual = objA != objB;
			Assert.AreEqual(expected, actual);
		}
		[TestMethod()]
		[Description("DomainName != String operator when both objects are null references.")]
		public void DomainName_Unit_InequalityOperator2_BothObjectsAreNull() {
			DomainName objA = null;
			String objB = null;
			Boolean expected = false;
			Boolean actual = objA != objB;
			Assert.AreEqual(expected, actual);
		}

		[TestMethod()]
		[Description("String != DomainName operator when the objects are equivalent.")]
		public void DomainName_Unit_InequalityOperator3_EquivalentObjects() {
			String objA = "www.parivedasolutions.com";
			DomainName objB = new DomainName("com", "parivedasolutions", "www");
			Boolean expected = false;
			Boolean actual = objA != objB;
			Assert.AreEqual(expected, actual);
		}
		[TestMethod()]
		[Description("String != DomainName operator when the objects are not equivalent.")]
		public void DomainName_Unit_InequalityOperator3_UnequivalentObjects() {
			String objA = "www.google.com";
			DomainName objB = new DomainName("com", "parivedasolutions", "www");
			Boolean expected = true;
			Boolean actual = objA != objB;
			Assert.AreEqual(expected, actual);
		}
		[TestMethod()]
		[Description("String != DomainName operator when 'objA' is a null reference.")]
		public void DomainName_Unit_InequalityOperator3_ObjAIsNull() {
			String objA = null;
			DomainName objB = new DomainName("com", "parivedasolutions", "www");
			Boolean expected = true;
			Boolean actual = objA != objB;
			Assert.AreEqual(expected, actual);
		}
		[TestMethod()]
		[Description("String != DomainName operator when 'objB' is a null reference.")]
		public void DomainName_Unit_InequalityOperator3_ObjBIsNull() {
			String objA = "www.parivedasolutions.com";
			DomainName objB = null;
			Boolean expected = true;
			Boolean actual = objA != objB;
			Assert.AreEqual(expected, actual);
		}
		[TestMethod()]
		[Description("String != DomainName operator when both objects are null references.")]
		public void DomainName_Unit_InequalityOperator3_BothObjectsAreNull() {
			String objA = null;
			DomainName objB = null;
			Boolean expected = false;
			Boolean actual = objA != objB;
			Assert.AreEqual(expected, actual);
		}

		[TestMethod()]
		[Description("Implicit (String)DomainName operator for the optimal path.")]
		public void DomainName_Unit_ImplicitStringCastOperator_Optimal() {
			DomainName instance = new DomainName("com", "parivedasolutions", "www");
			String expected = "www.parivedasolutions.com";
			String actual = instance;
			Assert.AreEqual(expected, actual);
		}
		[TestMethod()]
		[Description("Implicit (String)DomainName operator when 'instance' is a null reference.")]
		public void DomainName_Unit_ImplicitStringCastOperator_InstanceIsNull() {
			DomainName instance = null;
			String expected = null;
			String actual = instance;
			Assert.AreEqual(expected, actual);
		}

		[TestMethod()]
        [Description("Explicit (DomainName)String operator for the optimal path.")]
        public void DomainName_Unit_ExplicitDomainNameCastOperator_Optimal() {
			String firstLevelLabel = "com";
			String secondLevelLabel = "parivedasolutions";
			String[] subDomainLabels = new String[] { "www" };
			String instance = new String[] { firstLevelLabel, secondLevelLabel }
				.Concat(subDomainLabels)
				.Reverse()
				.ToArray()
				.Join('.');
			DomainName expected = new DomainName(firstLevelLabel, secondLevelLabel, subDomainLabels);
            DomainName actual = (DomainName)instance;
            Assert.AreEqual(expected, actual);
        }
		[TestMethod()]
		[Description("Explicit (DomainName)String operator when 'instance' is invalid.")]
		[ExpectedException(typeof(InvalidCastException))]
		public void DomainName_Unit_ExplicitDomainNameCastOperator_InstanceIsInvalid() {
			String firstLevelLabel = "com";
			String secondLevelLabel = "pariveda_solutions";
			String[] subDomainLabels = new String[] { "www" };
			String instance = new String[] { firstLevelLabel, secondLevelLabel }
				.Concat(subDomainLabels)
				.Reverse()
				.ToArray()
				.Join('.');
			DomainName actual = (DomainName)instance;
		}
		[TestMethod()]
		[Description("Explicit (DomainName)String operator when 'instance' is a null reference.")]
		public void DomainName_Unit_ExplicitDomainNameCastOperator_InstanceIsNull() {
			String instance = null;
			DomainName actual = (DomainName)instance;
			Assert.IsNull(actual);
		}
		[TestMethod()]
		[Description("Explicit (DomainName)String operator when 'instance' is empty.")]
		[ExpectedException(typeof(InvalidCastException))]
		public void DomainName_Unit_ExplicitDomainNameCastOperator_InstanceIsEmpty() {
			String instance = String.Empty;
			DomainName actual = (DomainName)instance;
		}
		[TestMethod()]
		[Description("Explicit (DomainName)String operator when 'instance' is too long.")]
		[ExpectedException(typeof(InvalidCastException))]
		public void DomainName_Unit_ExplicitDomainNameCastOperator_InstanceIsTooLong() {
			String instance = new String('a', 250) + ".b.c";
			DomainName actual = (DomainName)instance;
		}
		[TestMethod()]
		[Description("Explicit (DomainName)String operator when 'instance' has too many labels.")]
		[ExpectedException(typeof(InvalidCastException))]
		public void DomainName_Unit_ExplicitDomainNameCastOperator_InstanceHasTooManyLabels() {
			String instance = new String('.', 127);
			DomainName actual = (DomainName)instance;
		}
		[TestMethod()]
		[Description("Explicit (DomainName)String operator when 'instance' has an empty label.")]
		[ExpectedException(typeof(InvalidCastException))]
		public void DomainName_Unit_ExplicitDomainNameCastOperator_InstanceHasEmptyLabel() {
			String instance = "www..parivedasolutions.com";
			DomainName actual = (DomainName)instance;
		}
	}
}
