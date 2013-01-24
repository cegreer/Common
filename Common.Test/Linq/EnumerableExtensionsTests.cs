using System;
using System.Text;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Vizistata.Linq {
    /// <summary>
    /// Summary description for EnumerableExtensionsTests
    /// </summary>
    [TestClass]
    public class EnumerableExtensionsTests {
        public EnumerableExtensionsTests() {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext {
            get { return testContextInstance; }
            set { testContextInstance = value; }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

    /* Test Methods */
        [TestMethod]
        [Description("ToArray() test optimal path.")]
        public void EnumerableExtensions_Unit_ToArray_Optimal() {
            String[] expected = { "Abcdefg", "12345", "Another String", "Mike" };
            IEnumerable<String> enumerable = expected;
            String[] actual = enumerable.ToArray(x => x);
            Assert.AreEqual(expected[0], actual[0]);
            Assert.AreEqual(expected[1], actual[1]);
            Assert.AreEqual(expected[2], actual[2]);
            Assert.AreEqual(expected[3], actual[3]);
        }
        [TestMethod]
        [Description("ToArray() test with null selector.")]
        [ExpectedException(typeof(ArgumentNullException))]
        public void EnumerableExtensions_Unit_ToArray_NullSelector() {
            String[] expected = { "Abcdefg", "12345", "Another String", "Mike" };
            IEnumerable<String> enumerable = expected;
            Object[] actual = enumerable.ToArray<Object, String>(null);
        }
        [TestMethod]
        [Description("ToArray() test with null instance.")]
        [ExpectedException(typeof(ArgumentNullException))]
        public void EnumerableExtensions_Unit_ToArray_NullInstance() {
            IEnumerable<Object> target = null;
            target.ToArray<Object, Object>(x => x);
        }

        [TestMethod]
        [Description("ToCollection() test optimal path.")]
        public void EnumerableExtensions_Unit_ToCollection_Optimal() {
            String[] expected = { "Abcdefg", "12345", "Another String", "Mike" };
            IEnumerable<String> enumerable = expected;
            Collection<String> actual = enumerable.ToCollection();
            Assert.AreEqual(expected[0], actual[0]);
            Assert.AreEqual(expected[1], actual[1]);
            Assert.AreEqual(expected[2], actual[2]);
            Assert.AreEqual(expected[3], actual[3]);
        }
        [TestMethod]
        [Description("ToCollection() test with null instance.")]
        [ExpectedException(typeof(ArgumentNullException))]
        public void EnumerableExtensions_Unit_ToCollection_NullInstance() {
            IEnumerable<String> target = null;
            target.ToCollection();
        }

        [TestMethod]
        [Description("ToReadOnlyCollection() test optimal path.")]
        public void EnumerableExtensions_Unit_ToReadOnlyCollection_Optimal() {
            String[] expected = { "Abcdefg", "12345", "Another String", "Mike" };
            IEnumerable<String> enumerable = expected;
            ReadOnlyCollection<String> actual = enumerable.ToReadOnlyCollection();
            Assert.AreEqual(expected[0], actual[0]);
            Assert.AreEqual(expected[1], actual[1]);
            Assert.AreEqual(expected[2], actual[2]);
            Assert.AreEqual(expected[3], actual[3]);
        }
        [TestMethod]
        [Description("ToReadOnlyCollection() test with null instance.")]
        [ExpectedException(typeof(ArgumentNullException))]
        public void EnumerableExtensions_Unit_ToReadOnlyCollection_NullInstance() {
            IEnumerable<String> target = null;
            target.ToReadOnlyCollection();
        }
    }
}
