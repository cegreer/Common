using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Vizistata {
	/// <summary>
	/// This is a test class for <see cref="T:RelativePath"/> and is intended to contain all <see cref="T:RelativePath"/> Unit Tests.
	///</summary>
	[TestClass()]
	public class RelativePathTests {
		#region Test Class Implementation
		/// <summary>
		/// Describes the context under which the current test is running.
		/// </summary>
		private TestContext _testContextInstance;

		/// <summary>
		/// Initializes a new instance of the <see cref="T:RelativePathTests"/> class.
		/// </summary>
		public RelativePathTests() : base() { }

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
		[Description(".ctor(String) constructor for the optimal path.")]
		public void RelativePath_Unit_Constructor1_Optimal() {
			String path = "sites/Chad/Greer";
			new RelativePath(path);
		}
		[TestMethod()]
		[Description(".ctor(String) constructor when 'path' is a null reference.")]
		[ExpectedException(typeof(ArgumentNullException))]
		public void RelativePath_Unit_Constructor1_PathIsNull() {
			String path = null;
			new RelativePath(path);
		}
		[TestMethod()]
		[Description(".ctor(String) constructor when 'path' contains multiple valid separators.")]
		[ExpectedException(typeof(ArgumentException))]
		public void RelativePath_Unit_Constructor1_PathContainsMultipleSeparators() {
			String path = "sites/Chad\\Greer";
			new RelativePath(path);
		}
		[TestMethod()]
		[Description(".ctor(String) constructor when 'path' contains no separators.")]
		[ExpectedException(typeof(ArgumentException))]
		public void RelativePath_Unit_Constructor1_PathContainsNoSeparators() {
			String path = "sites Chad Greer";
			new RelativePath(path);
		}
		[TestMethod()]
		[Description(".ctor(String) constructor when 'path' contains consecutive separators.")]
		[ExpectedException(typeof(ArgumentException))]
		public void RelativePath_Unit_Constructor1_PathContainsConsecutiveSeparators() {
			String path = "sites//Chad/Greer";
			new RelativePath(path);
		}
		[TestMethod()]
		[Description(".ctor(String) constructor when 'path' contains a leading space in a node.")]
		[ExpectedException(typeof(ArgumentException))]
		public void RelativePath_Unit_Constructor1_PathContainsNodeWithLeadingSpace() {
			String path = "sites/ Chad/Greer";
			new RelativePath(path);
		}
		[TestMethod()]
		[Description(".ctor(String) constructor when 'path' contains a trailing space in a node.")]
		[ExpectedException(typeof(ArgumentException))]
		public void RelativePath_Unit_Constructor1_PathContainsNodeWithTrailingSpace() {
			String path = "sites/Chad /Greer";
			new RelativePath(path);
		}
		[TestMethod()]
		[Description(".ctor(String) constructor for an empty path.")]
		[ExpectedException(typeof(ArgumentException))]
		public void RelativePath_Unit_Constructor1_PathIsEmpty() {
			String path = String.Empty;
			new RelativePath(path);
		}
		[TestMethod()]
		[Description(".ctor(String) constructor when 'path' contains a leading slash.")]
		public void RelativePath_Unit_Constructor1_PathContainsLeadingSlash() {
			String path = "/sites/Chad/Greer";
			new RelativePath(path);
		}

		[TestMethod()]
		[Description(".ctor(String, Char) constructor for the optimal path.")]
		public void RelativePath_Unit_Constructor2_Optimal() {
			String path = "sites/Chad/Greer/";
			Char separator = RelativePath.ForwardSlash;
			
			RelativePath target = new RelativePath(path, separator);
			Assert.AreEqual(4, target.Nodes.Count());
		}
		[TestMethod()]
		[Description(".ctor(String, Char) constructor when 'path' is a null reference.")]
		[ExpectedException(typeof(ArgumentNullException))]
		public void RelativePath_Unit_Constructor2_PathIsNull() {
			String path = null;
			Char separator = RelativePath.ForwardSlash;
			new RelativePath(path, separator);
		}
		[TestMethod()]
		[Description(".ctor(String, Char) constructor when 'separator' is not valid.")]
		[ExpectedException(typeof(ArgumentException))]
		public void RelativePath_Unit_Constructor2_SeparatorIsInvalid() {
			String path = "sites/Chad/Greer";
			Char separator = ' ';
			new RelativePath(path, separator);
		}
		[TestMethod()]
		[Description(".ctor(String, Char) constructor when 'path' contains a separator other than 'separator'.")]
		[ExpectedException(typeof(ArgumentException))]
		public void RelativePath_Unit_Constructor2_PathContainsAnotherSeparator() {
			String path = "sites/Chad/Greer";
			Char separator = RelativePath.Backslash;
			new RelativePath(path, separator);
		}
		[TestMethod()]
		[Description(".ctor(String, Char) constructor when 'path' contains no separator.")]
		public void RelativePath_Unit_Constructor2_PathContainsNoSeparators() {
			String path = "sites Chad Greer";
			Char separator = RelativePath.ForwardSlash;
			new RelativePath(path, separator);
		}
		[TestMethod()]
		[Description(".ctor(String, Char) constructor when 'path' contains consecutive separators.")]
		[ExpectedException(typeof(ArgumentException))]
		public void RelativePath_Unit_Constructor2_PathContainsConsecutiveSeparators() {
			String path = "sites//Chad/Greer";
			Char separator = RelativePath.ForwardSlash;
			new RelativePath(path, separator);
		}
		[TestMethod()]
		[Description(".ctor(String, Char) constructor when 'path' contains a node with a leading space.")]
		[ExpectedException(typeof(ArgumentException))]
		public void RelativePath_Unit_Constructor2_PathContainsNodeWithLeadingSpace() {
			String path = "sites/ Chad/Greer";
			Char separator = RelativePath.ForwardSlash;
			new RelativePath(path, separator);
		}
		[TestMethod()]
		[Description(".ctor(String, Char) constructor when 'path' contains a node with a trailing space.")]
		[ExpectedException(typeof(ArgumentException))]
		public void RelativePath_Unit_Constructor2_PathContainsNodeWithTrailingSpace() {
			String path = "sites/Chad /Greer";
			Char separator = RelativePath.ForwardSlash;
			new RelativePath(path, separator);
		}
		[TestMethod()]
		[Description(".ctor(String, Char) constructor for an empty path.")]
		public void RelativePath_Unit_Constructor2_PathIsEmpty() {
			String path = String.Empty;
			Char separator = RelativePath.ForwardSlash;
			new RelativePath(path, separator);
		}
		[TestMethod()]
		[Description(".ctor(String, Char) constructor when 'path' contains a leading slash.")]
		public void RelativePath_Unit_Constructor2_PathContainsLeadingSlash() {
			String path = "/sites/Chad/Greer/";
			Char separator = RelativePath.ForwardSlash;

			RelativePath target = new RelativePath(path, separator);
			Assert.AreEqual(4, target.Nodes.Count());
		}

		[TestMethod()]
		[Description(".ctor(String, Char, Boolean) constructor for the optimal path.")]
		public void RelativePath_Unit_Constructor3_Optimal() {
			String path = "sites/Chad/Greer/";
			Char separator = RelativePath.ForwardSlash;
			Boolean removeEmptyLastNode = true;
			
			RelativePath target = new RelativePath(path, separator, removeEmptyLastNode);
			Assert.AreEqual(3, target.Nodes.Count());
		}
		[TestMethod()]
		[Description(".ctor(String, Char, Boolean) constructor when 'path' is a null reference.")]
		[ExpectedException(typeof(ArgumentNullException))]
		public void RelativePath_Unit_Constructor3_PathIsNull() {
			String path = null;
			Char separator = RelativePath.ForwardSlash;
			Boolean removeEmptyLastNode = true;
			new RelativePath(path, separator, removeEmptyLastNode);
		}
		[TestMethod()]
		[Description(".ctor(String, Char, Boolean) constructor when 'separator' is not valid.")]
		[ExpectedException(typeof(ArgumentException))]
		public void RelativePath_Unit_Constructor3_SeparatorIsInvalid() {
			String path = "sites/Chad/Greer";
			Char separator = ' ';
			Boolean removeEmptyLastNode = true;
			new RelativePath(path, separator, removeEmptyLastNode);
		}
		[TestMethod()]
		[Description(".ctor(String, Char, Boolean) constructor when 'path' contains a separator other than 'separator'.")]
		[ExpectedException(typeof(ArgumentException))]
		public void RelativePath_Unit_Constructor3_PathContainsAnotherSeparator() {
			String path = "sites/Chad/Greer";
			Char separator = RelativePath.Backslash;
			Boolean removeEmptyLastNode = true;
			new RelativePath(path, separator, removeEmptyLastNode);
		}
		[TestMethod()]
		[Description(".ctor(String, Char, Boolean) constructor when 'path' contains no separator.")]
		public void RelativePath_Unit_Constructor3_PathContainsNoSeparators() {
			String path = "sites Chad Greer";
			Char separator = RelativePath.ForwardSlash;
			Boolean removeEmptyLastNode = true;
			new RelativePath(path, separator, removeEmptyLastNode);
		}
		[TestMethod()]
		[Description(".ctor(String, Char, Boolean) constructor when 'path' contains consecutive separators.")]
		[ExpectedException(typeof(ArgumentException))]
		public void RelativePath_Unit_Constructor3_PathContainsConsecutiveSeparators() {
			String path = "sites//Chad/Greer";
			Char separator = RelativePath.ForwardSlash;
			Boolean removeEmptyLastNode = true;
			new RelativePath(path, separator, removeEmptyLastNode);
		}
		[TestMethod()]
		[Description(".ctor(String, Char, Boolean) constructor when 'path' contains a node with a leading space.")]
		[ExpectedException(typeof(ArgumentException))]
		public void RelativePath_Unit_Constructor3_PathContainsNodeWithLeadingSpace() {
			String path = "sites/ Chad/Greer";
			Char separator = RelativePath.ForwardSlash;
			Boolean removeEmptyLastNode = true;
			new RelativePath(path, separator, removeEmptyLastNode);
		}
		[TestMethod()]
		[Description(".ctor(String, Char, Boolean) constructor when 'path' contains a node with a trailing space.")]
		[ExpectedException(typeof(ArgumentException))]
		public void RelativePath_Unit_Constructor3_PathContainsNodeWithTrailingSpace() {
			String path = "sites/Chad /Greer";
			Char separator = RelativePath.ForwardSlash;
			Boolean removeEmptyLastNode = true;
			new RelativePath(path, separator, removeEmptyLastNode);
		}
		[TestMethod()]
		[Description(".ctor(String, Char, Boolean) constructor for an empty path.")]
		public void RelativePath_Unit_Constructor3_PathIsEmpty() {
			String path = String.Empty;
			Char separator = RelativePath.ForwardSlash;
			Boolean removeEmptyLastNode = true;
			new RelativePath(path, separator, removeEmptyLastNode);
		}
		[TestMethod()]
		[Description(".ctor(String, Char, Boolean) constructor when 'path' contains a leading slash.")]
		public void RelativePath_Unit_Constructor3_PathContainsLeadingSlash() {
			String path = "/sites/Chad/Greer/";
			Char separator = RelativePath.ForwardSlash;
			Boolean removeEmptyLastNode = true;

			RelativePath target = new RelativePath(path, separator, removeEmptyLastNode);
			Assert.AreEqual(3, target.Nodes.Count());
		}

		[TestMethod()]
		[Description(".ctor(String[], Char) constructor for the optimal path.")]
		public void RelativePath_Unit_Constructor4_Optimal() {
			String[] nodes = new String[] { "sites", "Chad", "Greer" };
			Char separator = RelativePath.ForwardSlash;
			new RelativePath(nodes, separator);
		}
		[TestMethod()]
		[Description(".ctor(String[], Char) constructor when 'nodes' is a null reference.")]
		[ExpectedException(typeof(ArgumentNullException))]
		public void RelativePath_Unit_Constructor4_NodesIsNull() {
			String[] nodes = null;
			Char separator = RelativePath.ForwardSlash;
			new RelativePath(nodes, separator);
		}
		[TestMethod()]
		[Description(".ctor(String[], Char) constructor when 'separator' is not valid.")]
		[ExpectedException(typeof(ArgumentException))]
		public void RelativePath_Unit_Constructor4_SeparatorIsInvalid() {
			String[] nodes = new String[] { "sites", "Chad", "Greer" };
			Char separator = ' ';
			new RelativePath(nodes, separator);
		}
		[TestMethod()]
		[Description(".ctor(String[], Char) constructor when 'nodes' contains an element with a separator.")]
		[ExpectedException(typeof(ArgumentException))]
		public void RelativePath_Unit_Constructor4_NodesContainsElementWithSeparator() {
			String[] nodes = new String[] { "sites", "Ch/ad", "Greer" };
			Char separator = RelativePath.ForwardSlash;
			new RelativePath(nodes, separator);
		}
		[TestMethod()]
		[Description(".ctor(String[], Char) constructor when 'nodes' contains an element that is a null reference.")]
		[ExpectedException(typeof(ArgumentException))]
		public void RelativePath_Unit_Constructor4_NodesContainsElementThatIsNull() {
			String[] nodes = new String[] { "sites", "Chad", null };
			Char separator = RelativePath.ForwardSlash;
			new RelativePath(nodes, separator);
		}
		[TestMethod()]
		[Description(".ctor(String[], Char) constructor when 'nodes' contains an element that has a leading space.")]
		[ExpectedException(typeof(ArgumentException))]
		public void RelativePath_Unit_Constructor4_NodesContainsElementWithLeadingSpace() {
			String[] nodes = new String[] { "sites", " Chad", "Greer" };
			Char separator = RelativePath.ForwardSlash;
			new RelativePath(nodes, separator);
		}
		[TestMethod()]
		[Description(".ctor(String[], Char) constructor when 'nodes' contains an element that has a trailing space.")]
		[ExpectedException(typeof(ArgumentException))]
		public void RelativePath_Unit_Constructor4_NodesContainsElementWithTrailingSpace() {
			String[] nodes = new String[] { "sites", "Chad ", "Greer" };
			Char separator = RelativePath.ForwardSlash;
			new RelativePath(nodes, separator);
		}
		[TestMethod()]
		[Description(".ctor(String[], Char) constructor when 'nodes' contains an element that is empty and is not the last one.")]
		[ExpectedException(typeof(ArgumentException))]
		public void RelativePath_Unit_Constructor4_NodesContainsNonLastElementThatIsEmpty() {
			String[] nodes = new String[] { "sites", "Chad", String.Empty, "Greer" };
			Char separator = RelativePath.ForwardSlash;
			new RelativePath(nodes, separator);
		}
		[TestMethod()]
		[Description(".ctor(String[], Char) constructor when 'nodes' contains empty last element.")]
		public void RelativePath_Unit_Constructor4_NodesContainsEmptyLastElement() {
			String[] nodes = new String[] { "sites", "Chad", "Greer", String.Empty };
			Char separator = RelativePath.ForwardSlash;
			
			RelativePath target = new RelativePath(nodes, separator);
			Assert.AreEqual(4, target.Nodes.Count());
		}
		[TestMethod()]
		[Description(".ctor(String[], Char) constructor for an empty path.")]
		public void RelativePath_Unit_Constructor4_PathIsEmpty() {
			String[] nodes = new String[0];
			Char separator = RelativePath.ForwardSlash;
			new RelativePath(nodes, separator);
		}

		[TestMethod()]
		[Description(".ctor(String[], Char, Boolean) constructor for the optimal path.")]
		public void RelativePath_Unit_Constructor5_Optimal() {
			String[] nodes = new String[] { "sites", "Chad", "Greer" };
			Char separator = RelativePath.ForwardSlash;
			Boolean removeEmptyLastNode = true;
			new RelativePath(nodes, separator, removeEmptyLastNode);
		}
		[TestMethod()]
		[Description(".ctor(String[], Char, Boolean) constructor when 'nodes' is a null reference.")]
		[ExpectedException(typeof(ArgumentNullException))]
		public void RelativePath_Unit_Constructor5_NodesIsNull() {
			String[] nodes = null;
			Char separator = RelativePath.ForwardSlash;
			Boolean removeEmptyLastNode = true;
			new RelativePath(nodes, separator, removeEmptyLastNode);
		}
		[TestMethod()]
		[Description(".ctor(String[], Char, Boolean) constructor when 'separator' is not valid.")]
		[ExpectedException(typeof(ArgumentException))]
		public void RelativePath_Unit_Constructor5_SeparatorIsInvalid() {
			String[] nodes = new String[] { "sites", "Chad", "Greer" };
			Char separator = ' ';
			Boolean removeEmptyLastNode = true;
			new RelativePath(nodes, separator, removeEmptyLastNode);
		}
		[TestMethod()]
		[Description(".ctor(String[], Char, Boolean) constructor when 'nodes' contains an element with a separator.")]
		[ExpectedException(typeof(ArgumentException))]
		public void RelativePath_Unit_Constructor5_NodesContainsElementWithSeparator() {
			String[] nodes = new String[] { "sites", "Ch/ad", "Greer" };
			Char separator = RelativePath.ForwardSlash;
			Boolean removeEmptyLastNode = true;
			new RelativePath(nodes, separator, removeEmptyLastNode);
		}
		[TestMethod()]
		[Description(".ctor(String[], Char, Boolean) constructor when 'nodes' contains an element that is a null reference.")]
		[ExpectedException(typeof(ArgumentException))]
		public void RelativePath_Unit_Constructor5_NodesContainsElementThatIsNull() {
			String[] nodes = new String[] { "sites", "Chad", null };
			Char separator = RelativePath.ForwardSlash;
			Boolean removeEmptyLastNode = true;
			new RelativePath(nodes, separator, removeEmptyLastNode);
		}
		[TestMethod()]
		[Description(".ctor(String[], Char, Boolean) constructor when 'nodes' contains an element that is empty and is not the last one.")]
		[ExpectedException(typeof(ArgumentException))]
		public void RelativePath_Unit_Constructor5_NodesContainsNonLastElementThatIsEmpty() {
			String[] nodes = new String[] { "sites", "Chad", String.Empty, "Greer" };
			Char separator = RelativePath.ForwardSlash;
			Boolean removeEmptyLastNode = true;
			new RelativePath(nodes, separator, removeEmptyLastNode);
		}
		[TestMethod()]
		[Description(".ctor(String[], Char, Boolean) constructor when 'nodes' contains empty last element.")]
		public void RelativePath_Unit_Constructor5_NodesContainsEmptyLastElement() {
			String[] nodes = new String[] { "sites", "Chad", "Greer", String.Empty };
			Char separator = RelativePath.ForwardSlash;
			Boolean removeEmptyLastNode = true;

			RelativePath target = new RelativePath(nodes, separator, removeEmptyLastNode);
			Assert.AreEqual(3, target.Nodes.Count());
		}
		[TestMethod()]
		[Description(".ctor(String[], Char, Boolean) constructor when 'nodes' contains an element that has a leading space.")]
		[ExpectedException(typeof(ArgumentException))]
		public void RelativePath_Unit_Constructor5_NodesContainsElementWithLeadingSpace() {
			String[] nodes = new String[] { "sites", " Chad", "Greer" };
			Char separator = RelativePath.ForwardSlash;
			Boolean removeEmptyLastNode = true;
			new RelativePath(nodes, separator, removeEmptyLastNode);
		}
		[TestMethod()]
		[Description(".ctor(String[], Char, Boolean) constructor when 'nodes' contains an element that has a trailing space.")]
		[ExpectedException(typeof(ArgumentException))]
		public void RelativePath_Unit_Constructor5_NodesContainsElementWithTrailingSpace() {
			String[] nodes = new String[] { "sites", "Chad ", "Greer" };
			Char separator = RelativePath.ForwardSlash;
			Boolean removeEmptyLastNode = true;
			new RelativePath(nodes, separator, removeEmptyLastNode);
		}
		[TestMethod()]
		[Description(".ctor(String[], Char, Boolean) constructor for an empty path.")]
		public void RelativePath_Unit_Constructor5_PathIsEmpty() {
			String[] nodes = new String[0];
			Char separator = RelativePath.ForwardSlash;
			Boolean removeEmptyLastNode = true;
			new RelativePath(nodes, separator, removeEmptyLastNode);
		}

	// Property Tests
		[TestMethod()]
		[Description("Nodes property for the optimal path.")]
		public void RelativePath_Unit_Nodes_Optimal() {
			String[] nodes = new String[] { "sites", "Chad", "Greer" };
			Char separator = RelativePath.ForwardSlash;
			RelativePath target = new RelativePath(nodes, separator);

			String[] targetNodes = target.Nodes.ToArray();
			Assert.AreEqual(nodes[0], targetNodes[0]);
			Assert.AreEqual(nodes[1], targetNodes[1]);
			Assert.AreEqual(nodes[2], targetNodes[2]);
		}

		[TestMethod()]
		[Description("Separator property for the optimal path.")]
		public void RelativePath_Unit_Separator_Optimal() {
			String[] nodes = new String[] { "sites", "Chad", "Greer" };
			Char separator = RelativePath.ForwardSlash;
			RelativePath target = new RelativePath(nodes, separator);

			Assert.AreEqual(separator, target.Separator);
		}

	// Method Tests
		[TestMethod()]
		[Description("Append(String) method for the optimal path.")]
		public void RelativePath_Unit_Append1_Optimal() {
			String[] nodes = new String[] { "sites" };
			Char separator = RelativePath.ForwardSlash;
			RelativePath target = new RelativePath(nodes, separator);
			String path = "Chad/Greer";

			RelativePath result = target.Append(path);
			Assert.AreEqual("sites", result.Nodes.ElementAt(0));
			Assert.AreEqual("Chad", result.Nodes.ElementAt(1));
			Assert.AreEqual("Greer", result.Nodes.ElementAt(2));
			Assert.AreEqual(separator, result.Separator);
		}
		[TestMethod()]
		[Description("Append(String) method when 'path' is empty.")]
		public void RelativePath_Unit_Append1_PathIsEmpty() {
			String[] nodes = new String[] { "sites" };
			Char separator = RelativePath.ForwardSlash;
			RelativePath target = new RelativePath(nodes, separator);
			String path = String.Empty;

			RelativePath result = target.Append(path);
			Assert.AreEqual(target.Nodes.Count(), result.Nodes.Count());
			Assert.AreEqual("sites", result.Nodes.ElementAt(0));
			Assert.AreEqual(separator, result.Separator);
		}
		[TestMethod()]
		[Description("Append(String) method when 'path' is white-space.")]
		public void RelativePath_Unit_Append1_PathIsWhitespace() {
			String[] nodes = new String[] { "sites" };
			Char separator = RelativePath.ForwardSlash;
			RelativePath target = new RelativePath(nodes, separator);
			String path = "  \t  \r  \n  ";

			RelativePath result = target.Append(path);
			Assert.AreEqual(target.Nodes.Count(), result.Nodes.Count());
			Assert.AreEqual("sites", result.Nodes.ElementAt(0));
			Assert.AreEqual(separator, result.Separator);
		}
		[TestMethod()]
		[Description("Append(String) method when 'path' is the separator.")]
		public void RelativePath_Unit_Append1_PathIsSeparator() {
			String[] nodes = new String[] { "sites" };
			Char separator = RelativePath.ForwardSlash;
			RelativePath target = new RelativePath(nodes, separator);
			String path = separator.ToString();

			RelativePath result = target.Append(path);
			Assert.AreEqual(target.Nodes.Count(), result.Nodes.Count());
			Assert.AreEqual("sites", result.Nodes.ElementAt(0));
			Assert.AreEqual(separator, result.Separator);
		}
		[TestMethod()]
		[Description("Append(String) method when 'path' starts with a valid separator.")]
		public void RelativePath_Unit_Append1_PathStartsWithSeparator() {
			String[] nodes = new String[] { "sites" };
			Char separator = RelativePath.ForwardSlash;
			RelativePath target = new RelativePath(nodes, separator);
			String path = "/Chad/Greer";

			RelativePath result = target.Append(path);
			Assert.AreEqual("sites", result.Nodes.ElementAt(0));
			Assert.AreEqual("Chad", result.Nodes.ElementAt(1));
			Assert.AreEqual("Greer", result.Nodes.ElementAt(2));
			Assert.AreEqual(separator, result.Separator);
		}
		[TestMethod()]
		[Description("Append(String) method when the target has an empty last node.")]
		public void RelativePath_Unit_Append1_TargetHasEmptyLastNode() {
			String[] nodes = new String[] { "sites", String.Empty };
			Char separator = RelativePath.ForwardSlash;
			RelativePath target = new RelativePath(nodes, separator, false);
			String path = "Chad/Greer";

			RelativePath result = target.Append(path);
			Assert.AreEqual("sites", result.Nodes.ElementAt(0));
			Assert.AreEqual("Chad", result.Nodes.ElementAt(1));
			Assert.AreEqual("Greer", result.Nodes.ElementAt(2));
			Assert.AreEqual(separator, result.Separator);
		}
		[TestMethod()]
		[Description("Append(String) method when 'path' is a null reference.")]
		[ExpectedException(typeof(ArgumentNullException))]
		public void RelativePath_Unit_Append1_PathIsNull() {
			String[] nodes = new String[] { "sites" };
			Char separator = RelativePath.ForwardSlash;
			RelativePath target = new RelativePath(nodes, separator);
			String path = null;

			target.Append(path);
		}
		[TestMethod()]
		[Description("Append(String) method when 'path' contains another separator.")]
		[ExpectedException(typeof(ArgumentException))]
		public void RelativePath_Unit_Append1_PathContainsAnotherSeparator() {
			String[] nodes = new String[] { "sites" };
			Char separator = RelativePath.ForwardSlash;
			RelativePath target = new RelativePath(nodes, separator);
			String path = "Chad\\Greer";

			target.Append(path);
		}
		[TestMethod()]
		[Description("Append(String) method when 'path' contains consecutive separators.")]
		[ExpectedException(typeof(ArgumentException))]
		public void RelativePath_Unit_Append1_PathContainsConsecutiveSeparators() {
			String[] nodes = new String[] { "sites" };
			Char separator = RelativePath.ForwardSlash;
			RelativePath target = new RelativePath(nodes, separator);
			String path = "Chad//Greer";

			target.Append(path);
		}

		[TestMethod()]
		[Description("Append(String, Boolean) method for the optimal path.")]
		public void RelativePath_Unit_Append2_Optimal() {
			String[] nodes = new String[] { "sites" };
			Char separator = RelativePath.ForwardSlash;
			RelativePath target = new RelativePath(nodes, separator);
			String path = "Chad/Greer";
			Boolean normalizeSeparators = true;

			RelativePath result = target.Append(path, normalizeSeparators);
			Assert.AreEqual("sites", result.Nodes.ElementAt(0));
			Assert.AreEqual("Chad", result.Nodes.ElementAt(1));
			Assert.AreEqual("Greer", result.Nodes.ElementAt(2));
			Assert.AreEqual(separator, result.Separator);
		}
		[TestMethod()]
		[Description("Append(String, Boolean) method when 'path' is empty.")]
		public void RelativePath_Unit_Append2_PathIsEmpty() {
			String[] nodes = new String[] { "sites" };
			Char separator = RelativePath.ForwardSlash;
			RelativePath target = new RelativePath(nodes, separator);
			String path = String.Empty;
			Boolean normalizeSeparators = true;

			RelativePath result = target.Append(path, normalizeSeparators);
			Assert.AreEqual(target.Nodes.Count(), result.Nodes.Count());
			Assert.AreEqual("sites", result.Nodes.ElementAt(0));
			Assert.AreEqual(separator, result.Separator);
		}
		[TestMethod()]
		[Description("Append(String, Boolean) method when 'path' is white-space.")]
		public void RelativePath_Unit_Append2_PathIsWhiteSpace() {
			String[] nodes = new String[] { "sites" };
			Char separator = RelativePath.ForwardSlash;
			RelativePath target = new RelativePath(nodes, separator);
			String path = " \t \r \n ";
			Boolean normalizeSeparators = true;

			RelativePath result = target.Append(path, normalizeSeparators);
			Assert.AreEqual(target.Nodes.Count(), result.Nodes.Count());
			Assert.AreEqual("sites", result.Nodes.ElementAt(0));
			Assert.AreEqual(separator, result.Separator);
		}
		[TestMethod()]
		[Description("Append(String, Boolean) method when 'path' is the separator.")]
		public void RelativePath_Unit_Append2_PathIsSeparataor() {
			String[] nodes = new String[] { "sites" };
			Char separator = RelativePath.ForwardSlash;
			RelativePath target = new RelativePath(nodes, separator);
			String path = separator.ToString();
			Boolean normalizeSeparators = true;

			RelativePath result = target.Append(path, normalizeSeparators);
			Assert.AreEqual(target.Nodes.Count(), result.Nodes.Count());
			Assert.AreEqual("sites", result.Nodes.ElementAt(0));
			Assert.AreEqual(separator, result.Separator);
		}
		[TestMethod()]
		[Description("Append(String, Boolean) method when 'path' starts with a valid separator.")]
		public void RelativePath_Unit_Append2_PathStartsWithSeparator() {
			String[] nodes = new String[] { "sites" };
			Char separator = RelativePath.ForwardSlash;
			RelativePath target = new RelativePath(nodes, separator);
			String path = "/Chad/Greer";
			Boolean normalizeSeparators = true;

			RelativePath result = target.Append(path, normalizeSeparators);
			Assert.AreEqual("sites", result.Nodes.ElementAt(0));
			Assert.AreEqual("Chad", result.Nodes.ElementAt(1));
			Assert.AreEqual("Greer", result.Nodes.ElementAt(2));
			Assert.AreEqual(separator, result.Separator);
		}
		[TestMethod()]
		[Description("Append(String, Boolean) method when the target has an empty last node.")]
		public void RelativePath_Unit_Append2_TargetHasEmptyLastNode() {
			String[] nodes = new String[] { "sites", String.Empty };
			Char separator = RelativePath.ForwardSlash;
			RelativePath target = new RelativePath(nodes, separator, false);
			String path = "Chad/Greer";
			Boolean normalizeSeparators = true;

			RelativePath result = target.Append(path, normalizeSeparators);
			Assert.AreEqual("sites", result.Nodes.ElementAt(0));
			Assert.AreEqual("Chad", result.Nodes.ElementAt(1));
			Assert.AreEqual("Greer", result.Nodes.ElementAt(2));
			Assert.AreEqual(separator, result.Separator);
		}
		[TestMethod()]
		[Description("Append(String, Boolean) method when 'path' is a null reference.")]
		[ExpectedException(typeof(ArgumentNullException))]
		public void RelativePath_Unit_Append2_PathIsNull() {
			String[] nodes = new String[] { "sites" };
			Char separator = RelativePath.ForwardSlash;
			RelativePath target = new RelativePath(nodes, separator);
			String path = null;
			Boolean normalizeSeparators = true;

			target.Append(path, normalizeSeparators);
		}
		[TestMethod()]
		[Description("Append(String, Boolean) method when 'path' contains another separator.")]
		public void RelativePath_Unit_Append2_PathContainsAnotherSeparator() {
			String[] nodes = new String[] { "sites" };
			Char separator = RelativePath.ForwardSlash;
			RelativePath target = new RelativePath(nodes, separator);
			String path = "Chad\\Greer";
			Boolean normalizeSeparators = true;

			RelativePath result = target.Append(path, normalizeSeparators);
			Assert.AreEqual("sites", result.Nodes.ElementAt(0));
			Assert.AreEqual("Chad", result.Nodes.ElementAt(1));
			Assert.AreEqual("Greer", result.Nodes.ElementAt(2));
			Assert.AreEqual(separator, result.Separator);
		}
		[TestMethod()]
		[Description("Append(String, Boolean) method when 'path' contains consecutive separators.")]
		[ExpectedException(typeof(ArgumentException))]
		public void RelativePath_Unit_Append2_PathContainsConsecutiveSeparators() {
			String[] nodes = new String[] { "sites" };
			Char separator = RelativePath.ForwardSlash;
			RelativePath target = new RelativePath(nodes, separator);
			String path = "Chad//Greer";
			Boolean normalizeSeparators = true;

			target.Append(path, normalizeSeparators);
		}

		[TestMethod()]
		[Description("CompareTo(RelativePath) method when 'other' is equivalent.")]
		public void RelativePath_Unit_CompareTo1_OtherIsEquivalent() {
			String[] nodes = new String[] { "sites" };
			Char separator = RelativePath.ForwardSlash;
			RelativePath target = new RelativePath(nodes, separator);
			RelativePath other = new RelativePath(nodes, separator);

			Int32 actual = target.CompareTo(other);
			Assert.AreEqual(0, actual);
		}
		[TestMethod()]
		[Description("CompareTo(RelativePath) method when 'other' is smaller.")]
		public void RelativePath_Unit_CompareTo1_OtherIsSmaller() {
			String[] nodes = new String[] { "sites" };
			Char separator = RelativePath.ForwardSlash;
			RelativePath target = new RelativePath(nodes, separator);
			RelativePath other = new RelativePath(new String[] { "site" }, separator);

			Int32 actual = target.CompareTo(other);
			Assert.AreEqual(1, actual);
		}
		[TestMethod()]
		[Description("CompareTo(RelativePath) method when 'other' is larger.")]
		public void RelativePath_Unit_CompareTo1_OtherIsGreater() {
			String[] nodes = new String[] { "sites" };
			Char separator = RelativePath.ForwardSlash;
			RelativePath target = new RelativePath(nodes, separator);
			RelativePath other = new RelativePath(new String[] { "sites1" }, separator);

			Int32 actual = target.CompareTo(other);
			Assert.IsTrue(actual < 0);
		}
		[TestMethod()]
		[Description("CompareTo(RelativePath) method when 'other' is a null reference.")]
		public void RelativePath_Unit_CompareTo1_OtherIsNull() {
			String[] nodes = new String[] { "sites" };
			Char separator = RelativePath.ForwardSlash;
			RelativePath target = new RelativePath(nodes, separator);
			RelativePath other = null;

			Int32 actual = target.CompareTo(other);
			Assert.AreEqual(1, actual);
		}

		[TestMethod()]
		[Description("IComparable.CompareTo(Object) method when 'obj' is equivalent.")]
		public void RelativePath_Unit_CompareTo2_ObjIsEquivalent() {
			String[] nodes = new String[] { "sites" };
			Char separator = RelativePath.ForwardSlash;
			IComparable target = new RelativePath(nodes, separator);
			Object obj = new RelativePath(nodes, separator);

			Int32 actual = target.CompareTo(obj);
			Assert.AreEqual(0, actual);
		}
		[TestMethod()]
		[Description("IComparable.CompareTo(Object) method when 'obj' is a different reference type.")]
		public void RelativePath_Unit_CompareTo2_ObjIsDifferentReferenceType() {
			String[] nodes = new String[] { "sites" };
			Char separator = RelativePath.ForwardSlash;
			IComparable target = new RelativePath(nodes, separator);
			Object obj = "sites";

			Int32 actual = target.CompareTo(obj);
			Assert.AreEqual(1, actual);
		}
		[TestMethod()]
		[Description("IComparable.CompareTo(Object) method when 'obj' is a different value type.")]
		public void RelativePath_Unit_CompareTo2_ObjIsDifferentValueType() {
			String[] nodes = new String[] { "sites" };
			Char separator = RelativePath.ForwardSlash;
			IComparable target = new RelativePath(nodes, separator);
			Object obj = 1;

			Int32 actual = target.CompareTo(obj);
			Assert.AreEqual(1, actual);
		}
		[TestMethod()]
		[Description("IComparable.CompareTo(Object) method when 'obj' is smaller.")]
		public void RelativePath_Unit_CompareTo2_ObjIsSmaller() {
			String[] nodes = new String[] { "sites" };
			Char separator = RelativePath.ForwardSlash;
			IComparable target = new RelativePath(nodes, separator);
			Object obj = new RelativePath(new String[] { "site" }, separator);

			Int32 actual = target.CompareTo(obj);
			Assert.AreEqual(1, actual);
		}
		[TestMethod()]
		[Description("IComparable.CompareTo(Object) method when 'obj' is larger.")]
		public void RelativePath_Unit_CompareTo2_ObjIsGreater() {
			String[] nodes = new String[] { "sites" };
			Char separator = RelativePath.ForwardSlash;
			IComparable target = new RelativePath(nodes, separator);
			Object obj = new RelativePath(new String[] { "sites1" }, separator);

			Int32 actual = target.CompareTo(obj);
			Assert.IsTrue(actual < 0);
		}
		[TestMethod()]
		[Description("IComparable.CompareTo(Object) method when 'obj' is a null reference.")]
		public void RelativePath_Unit_CompareTo2_ObjIsNull() {
			String[] nodes = new String[] { "sites" };
			Char separator = RelativePath.ForwardSlash;
			IComparable target = new RelativePath(nodes, separator);
			Object obj = null;

			Int32 actual = target.CompareTo(obj);
			Assert.AreEqual(1, actual);
		}

		[TestMethod()]
		[Description("Equals(Object) method when 'obj' is the same reference.")]
		public void RelativePath_Unit_Equals1_ObjIsSameReference() {
			String[] nodes = new String[] { "sites", "Chad", "Greer" };
			Char separator = RelativePath.ForwardSlash;
			RelativePath target = new RelativePath(nodes, separator);
			Object obj = target;

			Boolean actual = target.Equals(obj);
			Assert.AreEqual(true, actual);
		}
		[TestMethod()]
		[Description("Equals(Object) method when 'obj' is an equivalent RelativePath.")]
		public void RelativePath_Unit_Equals1_ObjIsEqualRelativePath() {
			String[] nodes = new String[] { "sites", "Chad", "Greer" };
			Char separator = RelativePath.ForwardSlash;
			RelativePath target = new RelativePath(nodes, separator);
			Object obj = new RelativePath(nodes, separator);

			Boolean actual = target.Equals(obj);
			Assert.AreEqual(true, actual);
		}
		[TestMethod()]
		[Description("Equals(Object) method when 'obj' is an equivalent String.")]
		public void RelativePath_Unit_Equals1_ObjIsEqualString() {
			String[] nodes = new String[] { "sites", "Chad", "Greer" };
			Char separator = RelativePath.ForwardSlash;
			RelativePath target = new RelativePath(nodes, separator);
			Object obj = nodes.Join(separator);

			Boolean actual = target.Equals(obj);
			Assert.AreEqual(true, actual);
		}
		[TestMethod()]
		[Description("Equals(Object) method when 'obj' is an non-equivalent RelativePath.")]
		public void RelativePath_Unit_Equals1_UnequivalentRelativePath1() {
			String[] nodes = new String[] { "sites", "Chad", "Greer" };
			Char separator = RelativePath.ForwardSlash;
			RelativePath target = new RelativePath(nodes, separator);
			Object obj = new RelativePath(nodes.Select(node => node.ToUpperInvariant()).ToArray(), separator);

			Boolean actual = target.Equals(obj);
			Assert.AreEqual(false, actual);
		}
		[TestMethod()]
		[Description("Equals(Object) method when 'obj' is an non-equivalent RelativePath.")]
		public void RelativePath_Unit_Equals1_UnequivalentRelativePath2() {
			String[] nodes = new String[] { "sites", "Chad", "Greer" };
			RelativePath target = new RelativePath(nodes, RelativePath.ForwardSlash);
			Object obj = new RelativePath(nodes, RelativePath.Backslash);

			Boolean actual = target.Equals(obj);
			Assert.AreEqual(false, actual);
		}
		[TestMethod()]
		[Description("Equals(Object) method when 'obj' is an non-equivalent RelativePath.")]
		public void RelativePath_Unit_Equals1_UnequivalentRelativePath3() {
			String[] nodes = new String[] { "sites", "Chad", "Greer" };
			RelativePath target = new RelativePath(nodes, RelativePath.ForwardSlash);
			Object obj = new RelativePath(nodes.Concat(new String[] { "test" }).ToArray(), RelativePath.Backslash);

			Boolean actual = target.Equals(obj);
			Assert.AreEqual(false, actual);
		}
		[TestMethod()]
		[Description("Equals(Object) method when 'obj' is an non-equivalent String.")]
		public void RelativePath_Unit_Equals1_ObjIsNonEqualString() {
			String[] nodes = new String[] { "sites", "Chad", "Greer" };
			Char separator = RelativePath.ForwardSlash;
			RelativePath target = new RelativePath(nodes, separator);
			Object obj = nodes.Select(node => node.ToUpperInvariant()).ToArray().Join(separator);

			Boolean actual = target.Equals(obj);
			Assert.AreEqual(false, actual);
		}
		[TestMethod()]
		[Description("Equals(Object) method when 'obj' is a null reference.")]
		public void RelativePath_Unit_Equals1_ObjIsNull() {
			String[] nodes = new String[] { "sites", "Chad", "Greer" };
			Char separator = RelativePath.ForwardSlash;
			RelativePath target = new RelativePath(nodes, separator);
			Object obj = null;

			Boolean actual = target.Equals(obj);
			Assert.AreEqual(false, actual);
		}
		[TestMethod()]
		[Description("Equals(Object) method when 'obj' is a different type.")]
		public void RelativePath_Unit_Equals1_ObjIsDifferentType() {
			String[] nodes = new String[] { "sites", "Chad", "Greer" };
			Char separator = RelativePath.ForwardSlash;
			RelativePath target = new RelativePath(nodes, separator);
			Object obj = new Object();

			Boolean actual = target.Equals(obj);
			Assert.AreEqual(false, actual);
		}

		[TestMethod()]
		[Description("Equals(RelativePath) method when 'obj' is the same reference.")]
		public void RelativePath_Unit_Equals2_ObjIsSameReference() {
			String[] nodes = new String[] { "sites", "Chad", "Greer" };
			Char separator = RelativePath.ForwardSlash;
			RelativePath target = new RelativePath(nodes, separator);
			RelativePath other = target;

			Boolean actual = target.Equals(other);
			Assert.AreEqual(true, actual);
		}
		[TestMethod()]
		[Description("Equals(RelativePath) method when 'obj' is an equivalent RelativePath.")]
		public void RelativePath_Unit_Equals2_ObjIsEqualRelativePath() {
			String[] nodes = new String[] { "sites", "Chad", "Greer" };
			Char separator = RelativePath.ForwardSlash;
			RelativePath target = new RelativePath(nodes, separator);
			RelativePath other = new RelativePath(nodes, separator);

			Boolean actual = target.Equals(other);
			Assert.AreEqual(true, actual);
		}
		[TestMethod()]
		[Description("Equals(RelativePath) method when 'obj' is an non-equivalent RelativePath.")]
		public void RelativePath_Unit_Equals2_UnequivalentRelativePath1() {
			String[] nodes = new String[] { "sites", "Chad", "Greer" };
			Char separator = RelativePath.ForwardSlash;
			RelativePath target = new RelativePath(nodes, separator);
			RelativePath other = new RelativePath(nodes.Select(node => node.ToUpperInvariant()).ToArray(), separator);

			Boolean actual = target.Equals(other);
			Assert.AreEqual(false, actual);
		}
		[TestMethod()]
		[Description("Equals(RelativePath) method when 'obj' is an non-equivalent RelativePath.")]
		public void RelativePath_Unit_Equals2_UnequivalentRelativePath2() {
			String[] nodes = new String[] { "sites", "Chad", "Greer" };
			RelativePath target = new RelativePath(nodes, RelativePath.ForwardSlash);
			RelativePath other = new RelativePath(nodes, RelativePath.Backslash);

			Boolean actual = target.Equals(other);
			Assert.AreEqual(false, actual);
		}
		[TestMethod()]
		[Description("Equals(RelativePath) method when 'obj' is a null reference.")]
		public void RelativePath_Unit_Equals2_ObjIsNull() {
			String[] nodes = new String[] { "sites", "Chad", "Greer" };
			Char separator = RelativePath.ForwardSlash;
			RelativePath target = new RelativePath(nodes, separator);
			RelativePath other = null;

			Boolean actual = target.Equals(other);
			Assert.AreEqual(false, actual);
		}

		[TestMethod()]
		[Description("Equals(String) method when 'obj' is an equivalent String.")]
		public void RelativePath_Unit_Equals3_ObjIsEqualString() {
			String[] nodes = new String[] { "sites", "Chad", "Greer" };
			Char separator = RelativePath.ForwardSlash;
			RelativePath target = new RelativePath(nodes, separator);
			String other = nodes.Join(separator);

			Boolean actual = target.Equals(other);
			Assert.AreEqual(true, actual);
		}
		[TestMethod()]
		[Description("Equals(String) method when 'obj' is an non-equivalent String.")]
		public void RelativePath_Unit_Equals3_ObjIsNonEqualString() {
			String[] nodes = new String[] { "sites", "Chad", "Greer" };
			Char separator = RelativePath.ForwardSlash;
			RelativePath target = new RelativePath(nodes, separator);
			String other = nodes.Select(node => node.ToUpperInvariant()).ToArray().Join(separator);

			Boolean actual = target.Equals(other);
			Assert.AreEqual(false, actual);
		}
		[TestMethod()]
		[Description("Equals(String) method when 'obj' is a null reference.")]
		public void RelativePath_Unit_Equals3_ObjIsNull() {
			String[] nodes = new String[] { "sites", "Chad", "Greer" };
			Char separator = RelativePath.ForwardSlash;
			RelativePath target = new RelativePath(nodes, separator);
			String other = null;

			Boolean actual = target.Equals(other);
			Assert.AreEqual(false, actual);
		}

		[TestMethod()]
		[Description("GetHashCode() method when equivalent objects are compared.")]
		public void RelativePath_Unit_GetHashCode_EqualObjects() {
			String[] nodes = new String[] { "sites", "Chad", "Greer" };
			Char separator = RelativePath.ForwardSlash;
			RelativePath first = new RelativePath(nodes, separator);
			RelativePath second = new RelativePath(nodes, separator);

			Assert.AreEqual(first.GetHashCode(), second.GetHashCode());
		}
		[TestMethod()]
		[Description("GetHashCode() method when non-equivalent objects are compared.")]
		public void RelativePath_Unit_GetHashCode_NonEqualObjects() {
			String[] nodes = new String[] { "sites", "Chad", "Greer" };
			Char separator = RelativePath.ForwardSlash;
			RelativePath first = new RelativePath(nodes, separator);
			RelativePath second = new RelativePath(nodes.Select(node => node.ToUpperInvariant()).ToArray(), separator);

			Assert.AreNotEqual(first.GetHashCode(), second.GetHashCode());
		}

		[TestMethod()]
		[Description("NavigateUp() method for the optimal path.")]
		public void RelativePath_Unit_NavigateUp1_Optimal() {
			String[] nodes = new String[] { "sites", "Chad", "Greer" };
			Char separator = RelativePath.ForwardSlash;
			RelativePath target = new RelativePath(nodes, separator);

			RelativePath parent = target.NavigateUp();
			Assert.IsNotNull(parent);
			Assert.AreEqual(2, parent.Nodes.Count());
			Assert.AreEqual(nodes[0], parent.Nodes.ElementAt(0));
			Assert.AreEqual(nodes[1], parent.Nodes.ElementAt(1));
		}
		[TestMethod()]
		[Description("NavigateUp() method when the path has an empty last node.")]
		public void RelativePath_Unit_NavigateUp1_RelativePathHasEmptyLastNode() {
			String[] nodes = new String[] { "sites", "Chad", "Greer", String.Empty };
			Char separator = RelativePath.ForwardSlash;
			RelativePath target = new RelativePath(nodes, separator, false);

			RelativePath parent = target.NavigateUp();
			Assert.IsNotNull(parent);
			Assert.AreEqual(3, parent.Nodes.Count());
			Assert.AreEqual(nodes[0], parent.Nodes.ElementAt(0));
			Assert.AreEqual(nodes[1], parent.Nodes.ElementAt(1));
			Assert.AreEqual(nodes[2], parent.Nodes.ElementAt(2));
		}
		[TestMethod()]
		[Description("NavigateUp() method when the path is empty.")]
		public void RelativePath_Unit_NavigateUp1_RelativePathIsEmpty() {
			String[] nodes = new String[0];
			Char separator = RelativePath.ForwardSlash;
			RelativePath target = new RelativePath(nodes, separator);

			RelativePath parent = target.NavigateUp();
			Assert.IsNotNull(parent);
			Assert.AreEqual(0, parent.Nodes.Count());
			Assert.AreEqual(separator, parent.Separator);
		}

		[TestMethod()]
		[Description("NavigateUp(Int32) method for the optimal path.")]
		public void RelativePath_Unit_NavigateUp2_Optimal() {
			String[] nodes = new String[] { "sites", "Chad", "Greer" };
			Char separator = RelativePath.ForwardSlash;
			RelativePath target = new RelativePath(nodes, separator);
			Int32 nodeCount = 2;

			RelativePath parent = target.NavigateUp(nodeCount);
			Assert.IsNotNull(parent);
			Assert.AreEqual(1, parent.Nodes.Count());
			Assert.AreEqual(nodes[0], parent.Nodes.ElementAt(0));
		}
		[TestMethod()]
		[Description("NavigateUp(Int32) method when 'nodeCount' is too small.")]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void RelativePath_Unit_NavigateUp2_NodeCountIsTooSmall() {
			String[] nodes = new String[] { "sites", "Chad", "Greer" };
			Char separator = RelativePath.ForwardSlash;
			RelativePath target = new RelativePath(nodes, separator);
			Int32 nodeCount = 0;

			target.NavigateUp(nodeCount);
		}
		[TestMethod()]
		[Description("NavigateUp(Int32) method when the path has an empty last node.")]
		public void RelativePath_Unit_NavigateUp2_RelativePathHasEmptyLastNode() {
			String[] nodes = new String[] { "sites", "Chad", "Greer", String.Empty };
			Char separator = RelativePath.ForwardSlash;
			RelativePath target = new RelativePath(nodes, separator);
			Int32 nodeCount = 2;

			RelativePath parent = target.NavigateUp(nodeCount);
			Assert.IsNotNull(parent);
			Assert.AreEqual(2, parent.Nodes.Count());
			Assert.AreEqual(nodes[0], parent.Nodes.ElementAt(0));
			Assert.AreEqual(nodes[1], parent.Nodes.ElementAt(1));
		}
		[TestMethod()]
		[Description("NavigateUp(Int32) method when the path does not contain enough nodes.")]
		public void RelativePath_Unit_NavigateUp2_RelativePathHasTooFewNodes() {
			String[] nodes = new String[] { "sites", "Chad", "Greer" };
			Char separator = RelativePath.ForwardSlash;
			RelativePath target = new RelativePath(nodes, separator);
			Int32 nodeCount = nodes.Length + 1;

			RelativePath parent = target.NavigateUp(nodeCount);
			Assert.IsNotNull(parent);
			Assert.AreEqual(0, parent.Nodes.Count());
			Assert.AreEqual(separator, parent.Separator);
		}

		[TestMethod()]
		[Description("ToString() method for the optimal path.")]
		public void RelativePath_Unit_ToString_Optimal() {
			String[] nodes = new String[] { "sites", "Chad", "Greer" };
			Char separator = RelativePath.ForwardSlash;
			RelativePath target = new RelativePath(nodes, separator);

			String actual = target.ToString();
			Assert.AreEqual(nodes.Join(separator), actual);
		}
		[TestMethod()]
		[Description("ToString() method when the path has an empty last node.")]
		public void RelativePath_Unit_ToString_RelativePathhasEmptyLastNode() {
			String[] nodes = new String[] { "sites", "Chad", "Greer", String.Empty };
			Char separator = RelativePath.ForwardSlash;
			RelativePath target = new RelativePath(nodes, separator);

			String actual = target.ToString();
			Assert.AreEqual(nodes.Join(separator), actual);
		}

	// Operator Tests
		[TestMethod()]
		[Description("RelativePath==RelativePath operator when the same reference is compared.")]
		public void RelativePath_Unit_EqualityOperator1_SameReferences() {
			String[] nodes = new String[] { "sites", "Chad", "Greer" };
			Char separator = RelativePath.ForwardSlash;
			RelativePath objA = new RelativePath(nodes, separator);
			RelativePath objB = objA;

			Boolean actual = objA == objB;
			Assert.AreEqual(true, actual);
		}
		[TestMethod()]
		[Description("RelativePath==RelativePath operator when equivalent objects are compared.")]
		public void RelativePath_Unit_EqualityOperator1_EqualObjects() {
			String[] nodes = new String[] { "sites", "Chad", "Greer" };
			Char separator = RelativePath.ForwardSlash;
			RelativePath objA = new RelativePath(nodes, separator);
			RelativePath objB = new RelativePath(nodes, separator);

			Boolean actual = objA == objB;
			Assert.AreEqual(true, actual);
		}
		[TestMethod()]
		[Description("RelativePath==RelativePath operator when non-equivalent objects are compared.")]
		public void RelativePath_Unit_EqualityOperator1_NonEqualObjects1() {
			String[] nodes = new String[] { "sites", "Chad", "Greer" };
			Char separator = RelativePath.ForwardSlash;
			RelativePath objA = new RelativePath(nodes, separator);
			RelativePath objB = new RelativePath(nodes.Select(node => node.ToUpperInvariant()).ToArray(), separator);

			Boolean actual = objA == objB;
			Assert.AreEqual(false, actual);
		}
		[TestMethod()]
		[Description("RelativePath==RelativePath operator when non-equivalent objects are compared.")]
		public void RelativePath_Unit_EqualityOperator1_NonEqualObjects2() {
			String[] nodes = new String[] { "sites", "Chad", "Greer" };
			RelativePath objA = new RelativePath(nodes, RelativePath.ForwardSlash);
			RelativePath objB = new RelativePath(nodes, RelativePath.Backslash);

			Boolean actual = objA == objB;
			Assert.AreEqual(false, actual);
		}
		[TestMethod()]
		[Description("RelativePath==RelativePath operator when 'objA' is a null reference.")]
		public void RelativePath_Unit_EqualityOperator1_ObjAIsNull() {
			String[] nodes = new String[] { "sites", "Chad", "Greer" };
			Char separator = RelativePath.ForwardSlash;
			RelativePath objA = null;
			RelativePath objB = new RelativePath(nodes, separator);

			Boolean actual = objA == objB;
			Assert.AreEqual(false, actual);
		}
		[TestMethod()]
		[Description("RelativePath==RelativePath operator when 'objB' is a null reference.")]
		public void RelativePath_Unit_EqualityOperator1_ObjBIsNull() {
			String[] nodes = new String[] { "sites", "Chad", "Greer" };
			Char separator = RelativePath.ForwardSlash;
			RelativePath objA = new RelativePath(nodes, separator);
			RelativePath objB = null;

			Boolean actual = objA == objB;
			Assert.AreEqual(false, actual);
		}
		[TestMethod()]
		[Description("RelativePath==RelativePath operator when 'objA' is a null reference and 'objB' is a null reference.")]
		public void RelativePath_Unit_EqualityOperator1_ObjAIsNullAndObjBIsNull() {
			String[] nodes = new String[] { "sites", "Chad", "Greer" };
			RelativePath objA = null;
			RelativePath objB = null;

			Boolean actual = objA == objB;
			Assert.AreEqual(true, actual);
		}

		[TestMethod()]
		[Description("RelativePath==String operator when equivalent objects are compared.")]
		public void RelativePath_Unit_EqualityOperator2_EqualObjects() {
			String[] nodes = new String[] { "sites", "Chad", "Greer" };
			Char separator = RelativePath.ForwardSlash;
			RelativePath objA = new RelativePath(nodes, separator);
			String objB = nodes.Join(separator);

			Boolean actual = objA == objB;
			Assert.AreEqual(true, actual);
		}
		[TestMethod()]
		[Description("RelativePath==String operator when non-equivalent objects are compared.")]
		public void RelativePath_Unit_EqualityOperator2_NonEqualObjects() {
			String[] nodes = new String[] { "sites", "Chad", "Greer" };
			Char separator = RelativePath.ForwardSlash;
			RelativePath objA = new RelativePath(nodes, separator);
			String objB = nodes.Select(node => node.ToUpperInvariant()).ToArray().Join(separator);

			Boolean actual = objA == objB;
			Assert.AreEqual(false, actual);
		}
		[TestMethod()]
		[Description("RelativePath==String operator when 'objA' is a null reference.")]
		public void RelativePath_Unit_EqualityOperator2_ObjAIsNull() {
			String[] nodes = new String[] { "sites", "Chad", "Greer" };
			Char separator = RelativePath.ForwardSlash;
			RelativePath objA = null;
			String objB = nodes.Join(separator);

			Boolean actual = objA == objB;
			Assert.AreEqual(false, actual);
		}
		[TestMethod()]
		[Description("RelativePath==String operator when 'objB' is a null reference.")]
		public void RelativePath_Unit_EqualityOperator2_ObjBIsNull() {
			String[] nodes = new String[] { "sites", "Chad", "Greer" };
			Char separator = RelativePath.ForwardSlash;
			RelativePath objA = new RelativePath(nodes, separator);
			String objB = null;

			Boolean actual = objA == objB;
			Assert.AreEqual(false, actual);
		}
		[TestMethod()]
		[Description("RelativePath==String operator when 'objA' is a null reference and 'objB' is a null reference.")]
		public void RelativePath_Unit_EqualityOperator2_ObjAIsNullAndObjBIsNull() {
			RelativePath objA = null;
			String objB = null;

			Boolean actual = objA == objB;
			Assert.AreEqual(true, actual);
		}

		[TestMethod()]
		[Description("String==RelativePath operator when equivalent objects are compared.")]
		public void RelativePath_Unit_EqualityOperator3_EqualObjects() {
			String[] nodes = new String[] { "sites", "Chad", "Greer" };
			Char separator = RelativePath.ForwardSlash;
			String objA = nodes.Join(separator);
			RelativePath objB = new RelativePath(nodes, separator);

			Boolean actual = objA == objB;
			Assert.AreEqual(true, actual);
		}
		[TestMethod()]
		[Description("String==RelativePath operator when non-equivalent objects are compared.")]
		public void RelativePath_Unit_EqualityOperator3_NonEqualObjects() {
			String[] nodes = new String[] { "sites", "Chad", "Greer" };
			Char separator = RelativePath.ForwardSlash;
			String objA = nodes.Select(node => node.ToUpperInvariant()).ToArray().Join(separator);
			RelativePath objB = new RelativePath(nodes, separator);

			Boolean actual = objA == objB;
			Assert.AreEqual(false, actual);
		}
		[TestMethod()]
		[Description("String==RelativePath operator when 'objA' is a null reference.")]
		public void RelativePath_Unit_EqualityOperator3_ObjAIsNull() {
			String[] nodes = new String[] { "sites", "Chad", "Greer" };
			Char separator = RelativePath.ForwardSlash;
			String objA = nodes.Join(separator);
			RelativePath objB = null;

			Boolean actual = objA == objB;
			Assert.AreEqual(false, actual);
		}
		[TestMethod()]
		[Description("String==RelativePath operator when 'objB' is a null reference.")]
		public void RelativePath_Unit_EqualityOperator3_ObjBIsNull() {
			String[] nodes = new String[] { "sites", "Chad", "Greer" };
			Char separator = RelativePath.ForwardSlash;
			String objA = null;
			RelativePath objB = new RelativePath(nodes, separator);

			Boolean actual = objA == objB;
			Assert.AreEqual(false, actual);
		}
		[TestMethod()]
		[Description("String==RelativePath operator when 'objA' is a null reference and 'objB' is a null reference.")]
		public void RelativePath_Unit_EqualityOperator3_ObjAIsNullAndObjBIsNull() {
			String objA = null;
			RelativePath objB = null;

			Boolean actual = objA == objB;
			Assert.AreEqual(true, actual);
		}

		[TestMethod()]
		[Description("RelativePath!=RelativePath operator when the same reference is compared.")]
		public void RelativePath_Unit_InequalityOperator1_SameReferences() {
			String[] nodes = new String[] { "sites", "Chad", "Greer" };
			Char separator = RelativePath.ForwardSlash;
			RelativePath objA = new RelativePath(nodes, separator);
			RelativePath objB = objA;

			Boolean actual = objA != objB;
			Assert.AreEqual(false, actual);
		}
		[TestMethod()]
		[Description("RelativePath!=RelativePath operator when equivalent objects are compared.")]
		public void RelativePath_Unit_InequalityOperator1_EqualObjects() {
			String[] nodes = new String[] { "sites", "Chad", "Greer" };
			Char separator = RelativePath.ForwardSlash;
			RelativePath objA = new RelativePath(nodes, separator);
			RelativePath objB = new RelativePath(nodes, separator);

			Boolean actual = objA != objB;
			Assert.AreEqual(false, actual);
		}
		[TestMethod()]
		[Description("RelativePath!=RelativePath operator when non-equivalent objects are compared.")]
		public void RelativePath_Unit_InequalityOperator1_NonEqualObjects1() {
			String[] nodes = new String[] { "sites", "Chad", "Greer" };
			Char separator = RelativePath.ForwardSlash;
			RelativePath objA = new RelativePath(nodes, separator);
			RelativePath objB = new RelativePath(nodes.Select(node => node.ToUpperInvariant()).ToArray(), separator);

			Boolean actual = objA != objB;
			Assert.AreEqual(true, actual);
		}
		[TestMethod()]
		[Description("RelativePath!=RelativePath operator when non-equivalent objects are compared.")]
		public void RelativePath_Unit_InequalityOperator1_NonEqualObjects2() {
			String[] nodes = new String[] { "sites", "Chad", "Greer" };
			RelativePath objA = new RelativePath(nodes, RelativePath.ForwardSlash);
			RelativePath objB = new RelativePath(nodes, RelativePath.Backslash);

			Boolean actual = objA != objB;
			Assert.AreEqual(true, actual);
		}
		[TestMethod()]
		[Description("RelativePath!=RelativePath operator when 'objA' is a null reference.")]
		public void RelativePath_Unit_InequalityOperator1_ObjAIsNull() {
			String[] nodes = new String[] { "sites", "Chad", "Greer" };
			Char separator = RelativePath.ForwardSlash;
			RelativePath objA = null;
			RelativePath objB = new RelativePath(nodes, separator);

			Boolean actual = objA != objB;
			Assert.AreEqual(true, actual);
		}
		[TestMethod()]
		[Description("RelativePath!=RelativePath operator when 'objB' is a null reference.")]
		public void RelativePath_Unit_InequalityOperator1_ObjBIsNull() {
			String[] nodes = new String[] { "sites", "Chad", "Greer" };
			Char separator = RelativePath.ForwardSlash;
			RelativePath objA = new RelativePath(nodes, separator);
			RelativePath objB = null;

			Boolean actual = objA != objB;
			Assert.AreEqual(true, actual);
		}
		[TestMethod()]
		[Description("RelativePath!=RelativePath operator when 'objA' is a null reference and 'objB' is a null reference.")]
		public void RelativePath_Unit_InequalityOperator1_ObjAIsNullAndObjBIsNull() {
			String[] nodes = new String[] { "sites", "Chad", "Greer" };
			RelativePath objA = null;
			RelativePath objB = null;

			Boolean actual = objA != objB;
			Assert.AreEqual(false, actual);
		}

		[TestMethod()]
		[Description("RelativePath!=String operator when equivalent objects are compared.")]
		public void RelativePath_Unit_InequalityOperator2_EqualObjects() {
			String[] nodes = new String[] { "sites", "Chad", "Greer" };
			Char separator = RelativePath.ForwardSlash;
			RelativePath objA = new RelativePath(nodes, separator);
			String objB = nodes.Join(separator);

			Boolean actual = objA != objB;
			Assert.AreEqual(false, actual);
		}
		[TestMethod()]
		[Description("RelativePath!=String operator when non-equivalent objects are compared.")]
		public void RelativePath_Unit_InequalityOperator2_NonEqualObjects() {
			String[] nodes = new String[] { "sites", "Chad", "Greer" };
			Char separator = RelativePath.ForwardSlash;
			RelativePath objA = new RelativePath(nodes, separator);
			String objB = nodes.Select(node => node.ToUpperInvariant()).ToArray().Join(separator);

			Boolean actual = objA != objB;
			Assert.AreEqual(true, actual);
		}
		[TestMethod()]
		[Description("RelativePath!=String operator when 'objA' is a null reference.")]
		public void RelativePath_Unit_InequalityOperator2_ObjAIsNull() {
			String[] nodes = new String[] { "sites", "Chad", "Greer" };
			Char separator = RelativePath.ForwardSlash;
			RelativePath objA = null;
			String objB = nodes.Join(separator);

			Boolean actual = objA != objB;
			Assert.AreEqual(true, actual);
		}
		[TestMethod()]
		[Description("RelativePath!=String operator when 'objB' is a null reference.")]
		public void RelativePath_Unit_InequalityOperator2_ObjBIsNull() {
			String[] nodes = new String[] { "sites", "Chad", "Greer" };
			Char separator = RelativePath.ForwardSlash;
			RelativePath objA = new RelativePath(nodes, separator);
			String objB = null;

			Boolean actual = objA != objB;
			Assert.AreEqual(true, actual);
		}
		[TestMethod()]
		[Description("RelativePath!=String operator when 'objA' is a null reference and 'objB' is a null reference.")]
		public void RelativePath_Unit_InequalityOperator2_ObjAIsNullAndObjBIsNull() {
			RelativePath objA = null;
			String objB = null;

			Boolean actual = objA != objB;
			Assert.AreEqual(false, actual);
		}

		[TestMethod()]
		[Description("String!=RelativePath operator when equivalent objects are compared.")]
		public void RelativePath_Unit_InequalityOperator3_EqualObjects() {
			String[] nodes = new String[] { "sites", "Chad", "Greer" };
			Char separator = RelativePath.ForwardSlash;
			String objA = nodes.Join(separator);
			RelativePath objB = new RelativePath(nodes, separator);

			Boolean actual = objA != objB;
			Assert.AreEqual(false, actual);
		}
		[TestMethod()]
		[Description("String!=RelativePath operator when non-equivalent objects are compared.")]
		public void RelativePath_Unit_InequalityOperator3_NonEqualObjects() {
			String[] nodes = new String[] { "sites", "Chad", "Greer" };
			Char separator = RelativePath.ForwardSlash;
			String objA = nodes.Select(node => node.ToUpperInvariant()).ToArray().Join(separator);
			RelativePath objB = new RelativePath(nodes, separator);

			Boolean actual = objA != objB;
			Assert.AreEqual(true, actual);
		}
		[TestMethod()]
		[Description("String!=RelativePath operator when 'objA' is a null reference.")]
		public void RelativePath_Unit_InequalityOperator3_ObjAIsNull() {
			String[] nodes = new String[] { "sites", "Chad", "Greer" };
			Char separator = RelativePath.ForwardSlash;
			String objA = nodes.Join(separator);
			RelativePath objB = null;

			Boolean actual = objA != objB;
			Assert.AreEqual(true, actual);
		}
		[TestMethod()]
		[Description("String!=RelativePath operator when 'objB' is a null reference.")]
		public void RelativePath_Unit_InequalityOperator3_ObjBIsNull() {
			String[] nodes = new String[] { "sites", "Chad", "Greer" };
			Char separator = RelativePath.ForwardSlash;
			String objA = null;
			RelativePath objB = new RelativePath(nodes, separator);

			Boolean actual = objA != objB;
			Assert.AreEqual(true, actual);
		}
		[TestMethod()]
		[Description("String!=RelativePath operator when 'objA' is a null reference and 'objB' is a null reference.")]
		public void RelativePath_Unit_InequalityOperator3_ObjAIsNullAndObjBIsNull() {
			String objA = null;
			RelativePath objB = null;

			Boolean actual = objA != objB;
			Assert.AreEqual(false, actual);
		}

		[TestMethod()]
		[Description("RelativePath<RelativePath operator when the same reference is compared.")]
		public void RelativePath_Unit_LessThanOperator_SameReferences() {
			String[] nodes = new String[] { "sites", "Chad", "Greer" };
			Char separator = RelativePath.ForwardSlash;
			RelativePath objA = new RelativePath(nodes, separator);
			RelativePath objB = objA;

			Boolean actual = objA < objB;
			Assert.AreEqual(false, actual);
		}
		[TestMethod()]
		[Description("RelativePath<RelativePath operator when equivalent objects are compared.")]
		public void RelativePath_Unit_LessThanOperator_EqualObjects() {
			String[] nodes = new String[] { "sites", "Chad", "Greer" };
			Char separator = RelativePath.ForwardSlash;
			RelativePath objA = new RelativePath(nodes, separator);
			RelativePath objB = new RelativePath(nodes, separator);

			Boolean actual = objA < objB;
			Assert.AreEqual(false, actual);
		}
		[TestMethod()]
		[Description("RelativePath<RelativePath operator when 'objA' is smaller.")]
		public void RelativePath_Unit_LessThanOperator_ObjAIsSmaller() {
			String[] nodes = new String[] { "sites", "Chad", "Greer" };
			Char separator = RelativePath.ForwardSlash;
			RelativePath objA = new RelativePath("sites", separator);
			RelativePath objB = new RelativePath(nodes, separator);

			Boolean actual = objA < objB;
			Assert.AreEqual(true, actual);
		}
		[TestMethod()]
		[Description("RelativePath<RelativePath operator when 'objB' is smaller.")]
		public void RelativePath_Unit_LessThanOperator_ObjBIsSmaller() {
			String[] nodes = new String[] { "sites", "Chad", "Greer" };
			RelativePath objA = new RelativePath(nodes, RelativePath.ForwardSlash);
			RelativePath objB = new RelativePath("sites", RelativePath.Backslash);

			Boolean actual = objA < objB;
			Assert.AreEqual(false, actual);
		}
		[TestMethod()]
		[Description("RelativePath<RelativePath operator when 'objA' is a null reference.")]
		public void RelativePath_Unit_LessThanOperator_ObjAIsNull() {
			String[] nodes = new String[] { "sites", "Chad", "Greer" };
			Char separator = RelativePath.ForwardSlash;
			RelativePath objA = null;
			RelativePath objB = new RelativePath(nodes, separator);

			Boolean actual = objA < objB;
			Assert.AreEqual(true, actual);
		}
		[TestMethod()]
		[Description("RelativePath<RelativePath operator when 'objB' is a null reference.")]
		public void RelativePath_Unit_LessThanOperator_ObjBIsNull() {
			String[] nodes = new String[] { "sites", "Chad", "Greer" };
			Char separator = RelativePath.ForwardSlash;
			RelativePath objA = new RelativePath(nodes, separator);
			RelativePath objB = null;

			Boolean actual = objA < objB;
			Assert.AreEqual(false, actual);
		}
		[TestMethod()]
		[Description("RelativePath<RelativePath operator when 'objA' is a null reference and 'objB' is a null reference.")]
		public void RelativePath_Unit_LessThanOperator_ObjAIsNullAndObjBIsNull() {
			String[] nodes = new String[] { "sites", "Chad", "Greer" };
			RelativePath objA = null;
			RelativePath objB = null;

			Boolean actual = objA < objB;
			Assert.AreEqual(false, actual);
		}

		[TestMethod()]
		[Description("RelativePath<=RelativePath operator when the same reference is compared.")]
		public void RelativePath_Unit_LessThanOrEqualToOperator_SameReferences() {
			String[] nodes = new String[] { "sites", "Chad", "Greer" };
			Char separator = RelativePath.ForwardSlash;
			RelativePath objA = new RelativePath(nodes, separator);
			RelativePath objB = objA;

			Boolean actual = objA <= objB;
			Assert.AreEqual(true, actual);
		}
		[TestMethod()]
		[Description("RelativePath<=RelativePath operator when equivalent objects are compared.")]
		public void RelativePath_Unit_LessThanOrEqualToOperator_EqualObjects() {
			String[] nodes = new String[] { "sites", "Chad", "Greer" };
			Char separator = RelativePath.ForwardSlash;
			RelativePath objA = new RelativePath(nodes, separator);
			RelativePath objB = new RelativePath(nodes, separator);

			Boolean actual = objA <= objB;
			Assert.AreEqual(true, actual);
		}
		[TestMethod()]
		[Description("RelativePath<=RelativePath operator when 'objA' is smaller.")]
		public void RelativePath_Unit_LessThanOrEqualToOperator_ObjAIsSmaller() {
			String[] nodes = new String[] { "sites", "Chad", "Greer" };
			Char separator = RelativePath.ForwardSlash;
			RelativePath objA = new RelativePath("sites", separator);
			RelativePath objB = new RelativePath(nodes, separator);

			Boolean actual = objA <= objB;
			Assert.AreEqual(true, actual);
		}
		[TestMethod()]
		[Description("RelativePath<=RelativePath operator when 'objB' is smaller.")]
		public void RelativePath_Unit_LessThanOrEqualToOperator_ObjBIsSmaller() {
			String[] nodes = new String[] { "sites", "Chad", "Greer" };
			RelativePath objA = new RelativePath(nodes, RelativePath.ForwardSlash);
			RelativePath objB = new RelativePath("sites", RelativePath.Backslash);

			Boolean actual = objA <= objB;
			Assert.AreEqual(false, actual);
		}
		[TestMethod()]
		[Description("RelativePath<=RelativePath operator when 'objA' is a null reference.")]
		public void RelativePath_Unit_LessThanOrEqualToOperator_ObjAIsNull() {
			String[] nodes = new String[] { "sites", "Chad", "Greer" };
			Char separator = RelativePath.ForwardSlash;
			RelativePath objA = null;
			RelativePath objB = new RelativePath(nodes, separator);

			Boolean actual = objA <= objB;
			Assert.AreEqual(true, actual);
		}
		[TestMethod()]
		[Description("RelativePath<=RelativePath operator when 'objB' is a null reference.")]
		public void RelativePath_Unit_LessThanOrEqualToOperator_ObjBIsNull() {
			String[] nodes = new String[] { "sites", "Chad", "Greer" };
			Char separator = RelativePath.ForwardSlash;
			RelativePath objA = new RelativePath(nodes, separator);
			RelativePath objB = null;

			Boolean actual = objA <= objB;
			Assert.AreEqual(false, actual);
		}
		[TestMethod()]
		[Description("RelativePath<=RelativePath operator when 'objA' is a null reference and 'objB' is a null reference.")]
		public void RelativePath_Unit_LessThanOrEqualToOperator_ObjAIsNullAndObjBIsNull() {
			String[] nodes = new String[] { "sites", "Chad", "Greer" };
			RelativePath objA = null;
			RelativePath objB = null;

			Boolean actual = objA <= objB;
			Assert.AreEqual(true, actual);
		}

		[TestMethod()]
		[Description("RelativePath>RelativePath operator when the same reference is compared.")]
		public void RelativePath_Unit_GreaterThanOperator_SameReferences() {
			String[] nodes = new String[] { "sites", "Chad", "Greer" };
			Char separator = RelativePath.ForwardSlash;
			RelativePath objA = new RelativePath(nodes, separator);
			RelativePath objB = objA;

			Boolean actual = objA > objB;
			Assert.AreEqual(false, actual);
		}
		[TestMethod()]
		[Description("RelativePath>RelativePath operator when equivalent objects are compared.")]
		public void RelativePath_Unit_GreaterThanOperator_EqualObjects() {
			String[] nodes = new String[] { "sites", "Chad", "Greer" };
			Char separator = RelativePath.ForwardSlash;
			RelativePath objA = new RelativePath(nodes, separator);
			RelativePath objB = new RelativePath(nodes, separator);

			Boolean actual = objA > objB;
			Assert.AreEqual(false, actual);
		}
		[TestMethod()]
		[Description("RelativePath>RelativePath operator when 'objA' is smaller.")]
		public void RelativePath_Unit_GreaterThanOperator_ObjAIsSmaller() {
			String[] nodes = new String[] { "sites", "Chad", "Greer" };
			Char separator = RelativePath.ForwardSlash;
			RelativePath objA = new RelativePath("sites", separator);
			RelativePath objB = new RelativePath(nodes, separator);

			Boolean actual = objA > objB;
			Assert.AreEqual(false, actual);
		}
		[TestMethod()]
		[Description("RelativePath>RelativePath operator when 'objB' is smaller.")]
		public void RelativePath_Unit_GreaterThanOperator_ObjBIsSmaller() {
			String[] nodes = new String[] { "sites", "Chad", "Greer" };
			RelativePath objA = new RelativePath(nodes, RelativePath.ForwardSlash);
			RelativePath objB = new RelativePath("sites", RelativePath.Backslash);

			Boolean actual = objA > objB;
			Assert.AreEqual(true, actual);
		}
		[TestMethod()]
		[Description("RelativePath>RelativePath operator when 'objA' is a null reference.")]
		public void RelativePath_Unit_GreaterThanOperator_ObjAIsNull() {
			String[] nodes = new String[] { "sites", "Chad", "Greer" };
			Char separator = RelativePath.ForwardSlash;
			RelativePath objA = null;
			RelativePath objB = new RelativePath(nodes, separator);

			Boolean actual = objA > objB;
			Assert.AreEqual(false, actual);
		}
		[TestMethod()]
		[Description("RelativePath>RelativePath operator when 'objB' is a null reference.")]
		public void RelativePath_Unit_GreaterThanOperator_ObjBIsNull() {
			String[] nodes = new String[] { "sites", "Chad", "Greer" };
			Char separator = RelativePath.ForwardSlash;
			RelativePath objA = new RelativePath(nodes, separator);
			RelativePath objB = null;

			Boolean actual = objA > objB;
			Assert.AreEqual(true, actual);
		}
		[TestMethod()]
		[Description("RelativePath>RelativePath operator when 'objA' is a null reference and 'objB' is a null reference.")]
		public void RelativePath_Unit_GreaterThanOperator_ObjAIsNullAndObjBIsNull() {
			String[] nodes = new String[] { "sites", "Chad", "Greer" };
			RelativePath objA = null;
			RelativePath objB = null;

			Boolean actual = objA > objB;
			Assert.AreEqual(false, actual);
		}

		[TestMethod()]
		[Description("RelativePath>=RelativePath operator when the same reference is compared.")]
		public void RelativePath_Unit_GreaterThanOrEqualToOperator_SameReferences() {
			String[] nodes = new String[] { "sites", "Chad", "Greer" };
			Char separator = RelativePath.ForwardSlash;
			RelativePath objA = new RelativePath(nodes, separator);
			RelativePath objB = objA;

			Boolean actual = objA >= objB;
			Assert.AreEqual(true, actual);
		}
		[TestMethod()]
		[Description("RelativePath>=RelativePath operator when equivalent objects are compared.")]
		public void RelativePath_Unit_GreaterThanOrEqualToOperator_EqualObjects() {
			String[] nodes = new String[] { "sites", "Chad", "Greer" };
			Char separator = RelativePath.ForwardSlash;
			RelativePath objA = new RelativePath(nodes, separator);
			RelativePath objB = new RelativePath(nodes, separator);

			Boolean actual = objA >= objB;
			Assert.AreEqual(true, actual);
		}
		[TestMethod()]
		[Description("RelativePath>=RelativePath operator when 'objA' is smaller.")]
		public void RelativePath_Unit_GreaterThanOrEqualToOperator_ObjAIsSmaller() {
			String[] nodes = new String[] { "sites", "Chad", "Greer" };
			Char separator = RelativePath.ForwardSlash;
			RelativePath objA = new RelativePath("sites", separator);
			RelativePath objB = new RelativePath(nodes, separator);

			Boolean actual = objA >= objB;
			Assert.AreEqual(false, actual);
		}
		[TestMethod()]
		[Description("RelativePath>=RelativePath operator when 'objB' is smaller.")]
		public void RelativePath_Unit_GreaterThanOrEqualToOperator_ObjBIsSmaller() {
			String[] nodes = new String[] { "sites", "Chad", "Greer" };
			RelativePath objA = new RelativePath(nodes, RelativePath.ForwardSlash);
			RelativePath objB = new RelativePath("sites", RelativePath.Backslash);

			Boolean actual = objA >= objB;
			Assert.AreEqual(true, actual);
		}
		[TestMethod()]
		[Description("RelativePath>=RelativePath operator when 'objA' is a null reference.")]
		public void RelativePath_Unit_GreaterThanOrEqualToOperator_ObjAIsNull() {
			String[] nodes = new String[] { "sites", "Chad", "Greer" };
			Char separator = RelativePath.ForwardSlash;
			RelativePath objA = null;
			RelativePath objB = new RelativePath(nodes, separator);

			Boolean actual = objA >= objB;
			Assert.AreEqual(false, actual);
		}
		[TestMethod()]
		[Description("RelativePath>=RelativePath operator when 'objB' is a null reference.")]
		public void RelativePath_Unit_GreaterThanOrEqualToOperator_ObjBIsNull() {
			String[] nodes = new String[] { "sites", "Chad", "Greer" };
			Char separator = RelativePath.ForwardSlash;
			RelativePath objA = new RelativePath(nodes, separator);
			RelativePath objB = null;

			Boolean actual = objA >= objB;
			Assert.AreEqual(true, actual);
		}
		[TestMethod()]
		[Description("RelativePath>=RelativePath operator when 'objA' is a null reference and 'objB' is a null reference.")]
		public void RelativePath_Unit_GreaterThanOrEqualToOperator_ObjAIsNullAndObjBIsNull() {
			String[] nodes = new String[] { "sites", "Chad", "Greer" };
			RelativePath objA = null;
			RelativePath objB = null;

			Boolean actual = objA >= objB;
			Assert.AreEqual(true, actual);
		}

		[TestMethod()]
		[Description("Implicit (String)RelativePath operator for the optimal path.")]
		public void RelativePath_Unit_ImplicitStringCastOperator_Optimal() {
			String[] nodes = new String[] { "sites", "Chad", "Greer" };
			Char separator = RelativePath.ForwardSlash;
			RelativePath instance = new RelativePath(nodes, separator);

			String actual = instance;
			Assert.AreEqual(instance.ToString(), actual);
		}
		[TestMethod()]
		[Description("Implicit (String)RelativePath operator when 'instance' is a null reference.")]
		public void RelativePath_Unit_ImplicitStringCastOperator_InstanceIsNull() {
			RelativePath instance = null;

			String actual = instance;
			Assert.IsNull(actual);
		}

		[TestMethod()]
		[Description("Explicit (RelativePath)String operator for the optimal path.")]
		public void RelativePath_Unit_ExplicitStringCastOperator_Optimal() {
			String[] nodes = new String[] { "sites", "Chad", "Greer" };
			Char separator = RelativePath.ForwardSlash;
			String instance = nodes.Join(separator);

			RelativePath actual = (RelativePath)instance;
			Assert.AreEqual(separator, actual.Separator);
			Assert.AreEqual(nodes.Length, actual.Nodes.Count());
			foreach (Int32 i in Enumerable.Range(0, nodes.Length)) {
				Assert.AreEqual(nodes[i], actual.Nodes.ElementAt(i));
			}
		}
		[TestMethod()]
		[Description("Explicit (RelativePath)String operator when 'instance' has no separator.")]
		[ExpectedException(typeof(InvalidCastException))]
		public void RelativePath_Unit_ExplicitStringCastOperator_InstanceHasNoSeparator() {
			String instance = "sites";

			RelativePath actual = (RelativePath)instance;
		}
		[TestMethod()]
		[Description("Explicit (RelativePath)String operator when 'instance' is a null reference.")]
		public void RelativePath_Unit_ExplicitStringCastOperator_InstanceIsNull() {
			String instance = null;

			RelativePath actual = (RelativePath)instance;
			Assert.IsNull(actual);
		}
		[TestMethod()]
		[Description("Explicit (RelativePath)String operator when 'instance' contains multiple separators.")]
		[ExpectedException(typeof(InvalidCastException))]
		public void RelativePath_Unit_ExplicitStringCastOperator_InstanceContainsMultipleSeparators() {
			String instance = "sites/Chad\\Greer";

			RelativePath actual = (RelativePath)instance;
		}
		[TestMethod()]
		[Description("Explicit (RelativePath)String operator when 'instance' contains consecutive separators.")]
		[ExpectedException(typeof(InvalidCastException))]
		public void RelativePath_Unit_ExplicitStringCastOperator_InstanceContainsConsecutiveSeparators() {
			String instance = "sites//Chad/Greer";

			RelativePath actual = (RelativePath)instance;
		}
		[TestMethod()]
		[Description("Explicit (RelativePath)String operator when 'instance' contains a node with a leading space.")]
		[ExpectedException(typeof(InvalidCastException))]
		public void RelativePath_Unit_ExplicitStringCastOperator_InstanceContainsNodeWithLeadingSpace() {
			String instance = "sites/ Chad/Greer";

			RelativePath actual = (RelativePath)instance;
		}
		[TestMethod()]
		[Description("Explicit (RelativePath)String operator when 'instance' contains a node with a trailing space.")]
		[ExpectedException(typeof(InvalidCastException))]
		public void RelativePath_Unit_ExplicitStringCastOperator_InstanceContainsNodeWithTrailingSpace() {
			String instance = "sites/Chad /Greer";

			RelativePath actual = (RelativePath)instance;
		}
	}
}