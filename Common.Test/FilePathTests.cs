using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Vizistata.TestTools;

namespace Vizistata {
	/// <summary>
	/// This is a test class for <see cref="T:FilePath"/> and is intended to contain all <see cref="T:FilePath"/> Unit Tests.
	///</summary>
	[TestClass()]
	public class FilePathTests {
		#region Test Class Implementation
		/// <summary>
		/// Describes the context under which the current test is running.
		/// </summary>
		private TestContext _testContextInstance;

		/// <summary>
		/// Initializes a new instance of the <see cref="T:FilePathTests"/> class.
		/// </summary>
		public FilePathTests() : base() { }

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
		public void FilePath_Unit_Constructor_Optimal() {
			String value = @"C:\Temp\MyFile.txt";
			new FilePath(value);
		}
		[TestMethod()]
		[Description(".ctor(String) constructor when 'value' is a null reference.")]
		[ExpectedException(typeof(ArgumentNullException))]
		public void FilePath_Unit_Constructor_ValueIsNull() {
			String value = null;
			new FilePath(value);
		}
		[TestMethod()]
		[Description(".ctor(String) constructor when 'value' has a length of 0.")]
		[ExpectedException(typeof(ArgumentException))]
		public void FilePath_Unit_Constructor_ValueHasLength0() {
			String value = String.Empty;
			new FilePath(value);
		}
		[TestMethod()]
		[Description(".ctor(String) constructor when 'value' contains only white-space characters.")]
		[ExpectedException(typeof(ArgumentException))]
		public void FilePath_Unit_Constructor_ValueContainsOnlyWhiteSpace() {
			String value = "  \t  \r  \n  ";
			new FilePath(value);
		}
		[TestMethod()]
		[Description(".ctor(String) constructor when 'value' has a length greater than 260 characters.")]
		[ExpectedException(typeof(ArgumentException))]
		public void FilePath_Unit_Constructor_ValueLengthExceeds260() {
			String value = @"C:\Temp\" + new String('a', 249) + ".txt";
			new FilePath(value);
		}
		[TestMethod()]
		[Description(".ctor(String) constructor when 'value' has invalid path characters.")]
		public void FilePath_Unit_Constructor_ValueHasInvalidPathCharacters() {
			foreach (Char invalidCharacter in Path.GetInvalidPathChars()) {
				String value = @"C:\Temp\A" + invalidCharacter + "A.txt";
				try {
					new FilePath(value);
				}
				catch (ArgumentNullException) {
					throw;
				}
				catch (ArgumentOutOfRangeException) {
					throw;
				}
				catch (ArgumentException) {
					/* Success */
				}
			}
		}
		[TestMethod()]
		[Description(".ctor(String) constructor when 'value' has invalid file name characters.")]
		public void FilePath_Unit_Constructor_ValueHasInvalidFileNameCharacters() {
			foreach (Char invalidCharacter in Path.GetInvalidFileNameChars()) {
				String value = @"C:\Temp\A" + invalidCharacter + "A.txt";
				try {
					new FilePath(value);
				}
				catch (ArgumentNullException) {
					throw;
				}
				catch (ArgumentOutOfRangeException) {
					throw;
				}
				catch (ArgumentException) {
					/* Success */
				}
			}
		}

	// Methods
		[TestMethod()]
		[Description("Equals(Object) method when 'obj' is the same reference.")]
		public void FilePath_Unit_Equals1_ObjIsSame() {
			String value = @"C:\Temp\Log.txt";
			FilePath target = new FilePath(value);
			Object obj = target;

			Boolean actual = target.Equals(obj);
			Assert.AreEqual(true, actual);
		}
		[TestMethod()]
		[Description("Equals(Object) method when 'obj' is a null reference.")]
		public void FilePath_Unit_Equals1_ObjIsNull() {
			String value = @"C:\Temp\Log.txt";
			FilePath target = new FilePath(value);
			Object obj = null;

			Boolean actual = target.Equals(obj);
			Assert.AreEqual(false, actual);
		}
		[TestMethod()]
		[Description("Equals(Object) method when 'obj' is an equivalent FilePath object.")]
		public void FilePath_Unit_Equals1_ObjIsEquivalentFilePath() {
			String value = @"C:\Temp\Log.txt";
			FilePath target = new FilePath(value);
			Object obj = new FilePath(value.ToUpperInvariant());

			Boolean actual = target.Equals(obj);
			Assert.AreEqual(true, actual);
		}
		[TestMethod()]
		[Description("Equals(Object) method when 'obj' is an unequivalent FilePath object.")]
		public void FilePath_Unit_Equals1_ObjIsUnequivalentFilePath() {
			String value = @"C:\Temp\Log.txt";
			FilePath target = new FilePath(value);
			Object obj = new FilePath(value + "2");

			Boolean actual = target.Equals(obj);
			Assert.AreEqual(false, actual);
		}
		[TestMethod()]
		[Description("Equals(Object) method when 'obj' is an equivalent String object.")]
		public void FilePath_Unit_Equals1_ObjIsEquivalentString() {
			String value = @"C:\Temp\Log.txt";
			FilePath target = new FilePath(value);
			Object obj = value.ToUpperInvariant();

			Boolean actual = target.Equals(obj);
			Assert.AreEqual(true, actual);
		}
		[TestMethod()]
		[Description("Equals(Object) method when 'obj' is an unequivalent String object.")]
		public void FilePath_Unit_Equals1_ObjIsUnequivalentString() {
			String value = @"C:\Temp\Log.txt";
			FilePath target = new FilePath(value);
			Object obj = value + "2";

			Boolean actual = target.Equals(obj);
			Assert.AreEqual(false, actual);
		}
		[TestMethod()]
		[Description("Equals(Object) method when 'obj' is a different type.")]
		public void FilePath_Unit_Equals1_ObjIsDifferentType() {
			String value = @"C:\Temp\Log.txt";
			FilePath target = new FilePath(value);
			Object obj = new Object();

			Boolean actual = target.Equals(obj);
			Assert.AreEqual(false, actual);
		}

		[TestMethod()]
		[Description("Equals(FilePath) method when 'other' is the same reference.")]
		public void FilePath_Unit_Equals2_OtherIsSame() {
			String value = @"C:\Temp\Log.txt";
			FilePath target = new FilePath(value);
			FilePath other = target;

			Boolean actual = target.Equals(other);
			Assert.AreEqual(true, actual);
		}
		[TestMethod()]
		[Description("Equals(FilePath) method when 'other' is a null reference.")]
		public void FilePath_Unit_Equals2_OtherIsNull() {
			String value = @"C:\Temp\Log.txt";
			FilePath target = new FilePath(value);
			FilePath other = null;

			Boolean actual = target.Equals(other);
			Assert.AreEqual(false, actual);
		}
		[TestMethod()]
		[Description("Equals(FilePath) method when 'other' is an equivalent FilePath object.")]
		public void FilePath_Unit_Equals2_OtherIsEquivalentFilePath() {
			String value = @"C:\Temp\Log.txt";
			FilePath target = new FilePath(value);
			FilePath other = new FilePath(value.ToUpperInvariant());

			Boolean actual = target.Equals(other);
			Assert.AreEqual(true, actual);
		}
		[TestMethod()]
		[Description("Equals(FilePath) method when 'other' is an unequivalent FilePath object.")]
		public void FilePath_Unit_Equals2_OtherIsUnequivalentFilePath() {
			String value = @"C:\Temp\Log.txt";
			FilePath target = new FilePath(value);
			FilePath other = new FilePath(value + "2");

			Boolean actual = target.Equals(other);
			Assert.AreEqual(false, actual);
		}

		[TestMethod()]
		[Description("Equals(String) method when 'other' is a null reference.")]
		public void FilePath_Unit_Equals3_ObjIsNull() {
			String value = @"C:\Temp\Log.txt";
			FilePath target = new FilePath(value);
			String other = null;

			Boolean actual = target.Equals(other);
			Assert.AreEqual(false, actual);
		}
		[TestMethod()]
		[Description("Equals(String) method when 'other' is an equivalent String object.")]
		public void FilePath_Unit_Equals3_ObjIsEquivalentString() {
			String value = @"C:\Temp\Log.txt";
			FilePath target = new FilePath(value);
			String other = value.ToUpperInvariant();

			Boolean actual = target.Equals(other);
			Assert.AreEqual(true, actual);
		}
		[TestMethod()]
		[Description("Equals(String) method when 'other' is an unequivalent String object.")]
		public void FilePath_Unit_Equals3_ObjIsUnequivalentString() {
			String value = @"C:\Temp\Log.txt";
			FilePath target = new FilePath(value);
			String other = value + "2";

			Boolean actual = target.Equals(other);
			Assert.AreEqual(false, actual);
		}

		[TestMethod()]
		[Description("Exists() method when the file exists.")]
		public void FilePath_System_Exists_True() {
			String value = @"C:\Temp\FilePathTest.txt";
			Directory.CreateDirectory(Path.GetDirectoryName(value));
			File.WriteAllText(value, "This file was generated for a system test.");
			FilePath target = new FilePath(value);

			Boolean actual = target.Exists();
			Assert.AreEqual(true, actual);
		}
		[TestMethod()]
		[Description("Exists() method when the file does not exist.")]
		public void FilePath_System_Exists_False() {
			String value = @"C:\Temp\FilePathTest.txt";
			Directory.CreateDirectory(Path.GetDirectoryName(value));
			File.Delete(value);
			FilePath target = new FilePath(value);

			Boolean actual = target.Exists();
			Assert.AreEqual(false, actual);
		}
		[TestMethod()]
		[Description("Exists() method when the file exists initially but is deleted while the FilePath object is still alive.")]
		public void FilePath_System_Exists_FileIsDeletedDuringObjectLifeTime() {
			String value = @"C:\Temp\FilePathTest.txt";
			Directory.CreateDirectory(Path.GetDirectoryName(value));
			File.WriteAllText(value, "This file was generated for a system test.");
			FilePath target = new FilePath(value);

			Boolean actual = target.Exists();
			Assert.AreEqual(true, actual);

			File.Delete(value);
			actual = target.Exists();
			Assert.AreEqual(false, actual);
		}

		[TestMethod()]
		[Description("Equals(Object) method when the same references are compared.")]
		public void FilePath_Unit_GetHashCode_SameReferences() {
			String value = @"C:\Temp\Log.txt";
			FilePath first = new FilePath(value);
			FilePath second = first;

			Assert.AreEqual(first.GetHashCode(), second.GetHashCode());
		}
		[TestMethod()]
		[Description("Equals(Object) method when the references are equivalent.")]
		public void FilePath_Unit_GetHashCode_EquivalentReferences() {
			String value = @"C:\Temp\Log.txt";
			FilePath first = new FilePath(value);
			FilePath second = new FilePath(value.ToUpperInvariant());

			Assert.AreEqual(first.GetHashCode(), second.GetHashCode());
		}
		[TestMethod()]
		[Description("Equals(Object) method when the references are unequivalent.")]
		public void FilePath_Unit_GetHashCode_UnequivalentReferences() {
			String value = @"C:\Temp\Log.txt";
			FilePath first = new FilePath(value);
			FilePath second = new FilePath(value + "2");

			Assert.AreNotEqual(first.GetHashCode(), second.GetHashCode());
		}

		[TestMethod()]
		[Description("Open(FileMode) method for the optimal path.")]
		public void FilePath_System_Open1_Optimal() {
			String value = @"C:\Temp\FilePathTest.txt";
			Directory.CreateDirectory(Path.GetDirectoryName(value));
			FilePath target = new FilePath(value);
			FileMode mode = FileMode.OpenOrCreate;

			using (target.Open(mode)) { }
		}

		[TestMethod()]
		[Description("Open(FileMode, FileAccess) method for the optimal path.")]
		public void FilePath_System_Open2_Optimal() {
			String value = @"C:\Temp\FilePathTest.txt";
			Directory.CreateDirectory(Path.GetDirectoryName(value));
			FilePath target = new FilePath(value);
			FileMode mode = FileMode.OpenOrCreate;
			FileAccess access = FileAccess.Write;

			using (target.Open(mode, access)) { }
		}

		[TestMethod()]
		[Description("Open(FileMode, FileAccess, FileShare) method for the optimal path.")]
		public void FilePath_System_Open3_Optimal() {
			String value = @"C:\Temp\FilePathTest.txt";
			Directory.CreateDirectory(Path.GetDirectoryName(value));
			FilePath target = new FilePath(value);
			FileMode mode = FileMode.OpenOrCreate;
			FileAccess access = FileAccess.Write;
			FileShare share = FileShare.ReadWrite;

			using (target.Open(mode, access, share)) { }
		}

		[TestMethod()]
		[Description("OpenRead() method for the optimal path.")]
		public void FilePath_System_OpenRead_Optimal() {
			String value = @"C:\Temp\FilePathTest.txt";
			Directory.CreateDirectory(Path.GetDirectoryName(value));
			File.WriteAllText(value, "This file was generated for a system test.");
			FilePath target = new FilePath(value);

			using (target.OpenRead()) { }
		}

		[TestMethod()]
		[Description("OpenWrite() method for the optimal path.")]
		public void FilePath_System_OpenWrite_Optimal() {
			String value = @"C:\Temp\FilePathTest.txt";
			Directory.CreateDirectory(Path.GetDirectoryName(value));
			File.Delete(value);
			FilePath target = new FilePath(value);

			using (target.OpenWrite()) { }
		}

		[TestMethod()]
		[Description("ToString() method for the optimal path.")]
		public void FilePath_Unit_ToString_Optimal() {
			String value = @"C:\Temp\MyFile.txt";
			FilePath target = new FilePath(value);

			Assert.AreEqual(value, target.ToString());
		}

	// Operator Tests
		[TestMethod()]
		[Description("FilePath==FilePath operator when the same reference is compared.")]
		public void FilePath_Unit_EqualityOperator1_SameReferences() {
			String value = @"C:\Temp\MyFile.txt";
			FilePath objA = new FilePath(value);
			FilePath objB = objA;

			Boolean actual = objA == objB;
			Assert.AreEqual(true, actual);
		}
		[TestMethod()]
		[Description("FilePath==FilePath operator when equivalent references are compared.")]
		public void FilePath_Unit_EqualityOperator1_EquivalentReferences() {
			String value = @"C:\Temp\MyFile.txt";
			FilePath objA = new FilePath(value);
			FilePath objB = new FilePath(value.ToUpperInvariant());

			Boolean actual = objA == objB;
			Assert.AreEqual(true, actual);
		}
		[TestMethod()]
		[Description("FilePath==FilePath operator when unequivalent references are compared.")]
		public void FilePath_Unit_EqualityOperator1_UnequivalentReferences() {
			String value = @"C:\Temp\MyFile.txt";
			FilePath objA = new FilePath(value);
			FilePath objB = new FilePath(value + "2");

			Boolean actual = objA == objB;
			Assert.AreEqual(false, actual);
		}
		[TestMethod()]
		[Description("FilePath==FilePath operator when 'objA' is a null reference.")]
		public void FilePath_Unit_EqualityOperator1_ObjAIsNull() {
			String value = @"C:\Temp\MyFile.txt";
			FilePath objA = null;
			FilePath objB = new FilePath(value);

			Boolean actual = objA == objB;
			Assert.AreEqual(false, actual);
		}
		[TestMethod()]
		[Description("FilePath==FilePath operator when 'objB' is a null reference.")]
		public void FilePath_Unit_EqualityOperator1_ObjBIsNull() {
			String value = @"C:\Temp\MyFile.txt";
			FilePath objA = new FilePath(value);
			FilePath objB = null;

			Boolean actual = objA == objB;
			Assert.AreEqual(false, actual);
		}
		[TestMethod()]
		[Description("FilePath==FilePath operator when 'objA' and 'objB' are null references.")]
		public void FilePath_Unit_EqualityOperator1_ObjAIsNullAndObjBIsNull() {
			FilePath objA = null;
			FilePath objB = null;

			Boolean actual = objA == objB;
			Assert.AreEqual(true, actual);
		}

		[TestMethod()]
		[Description("FilePath==String operator when equivalent objects are compared.")]
		public void FilePath_Unit_EqualityOperator2_EquivalentObjects() {
			String value = @"C:\Temp\MyFile.txt";
			FilePath filePath = new FilePath(value);
			String str = value.ToUpperInvariant();

			Boolean actual = filePath == str;
			Assert.AreEqual(true, actual);
		}
		[TestMethod()]
		[Description("FilePath==String operator when unequivalent objects are compared.")]
		public void FilePath_Unit_EqualityOperator2_UnequivalentObjects() {
			String value = @"C:\Temp\MyFile.txt";
			FilePath filePath = new FilePath(value);
			String str = value + "2";

			Boolean actual = filePath == str;
			Assert.AreEqual(false, actual);
		}
		[TestMethod()]
		[Description("FilePath==String operator when 'filePath' is a null reference.")]
		public void FilePath_Unit_EqualityOperator2_FilePathIsNull() {
			String value = @"C:\Temp\MyFile.txt";
			FilePath filePath = null;
			String str = value;

			Boolean actual = filePath == str;
			Assert.AreEqual(false, actual);
		}
		[TestMethod()]
		[Description("FilePath==String operator when 'value' is a null reference.")]
		public void FilePath_Unit_EqualityOperator2_ValueIsNull() {
			String value = @"C:\Temp\MyFile.txt";
			FilePath filePath = new FilePath(value);
			String str = null;

			Boolean actual = filePath == str;
			Assert.AreEqual(false, actual);
		}
		[TestMethod()]
		[Description("FilePath==String operator when 'filePath' and 'value' are null references.")]
		public void FilePath_Unit_EqualityOperator2_FilePathIsNullAndValueIsNull() {
			FilePath filePath = null;
			String str = null;

			Boolean actual = filePath == str;
			Assert.AreEqual(true, actual);
		}

		[TestMethod()]
		[Description("String==FilePath operator when equivalent objects are compared.")]
		public void FilePath_Unit_EqualityOperator3_EquivalentObjects() {
			String value = @"C:\Temp\MyFile.txt";
			FilePath filePath = new FilePath(value);
			String str = value.ToUpperInvariant();

			Boolean actual = str == filePath;
			Assert.AreEqual(true, actual);
		}
		[TestMethod()]
		[Description("String==FilePath operator when unequivalent objects are compared.")]
		public void FilePath_Unit_EqualityOperator3_UnequivalentObjects() {
			String value = @"C:\Temp\MyFile.txt";
			FilePath filePath = new FilePath(value);
			String str = value + "2";

			Boolean actual = str == filePath;
			Assert.AreEqual(false, actual);
		}
		[TestMethod()]
		[Description("String==FilePath operator when 'filePath' is a null reference.")]
		public void FilePath_Unit_EqualityOperator3_FilePathIsNull() {
			String value = @"C:\Temp\MyFile.txt";
			FilePath filePath = null;
			String str = value;

			Boolean actual = str == filePath;
			Assert.AreEqual(false, actual);
		}
		[TestMethod()]
		[Description("String==FilePath operator when 'value' is a null reference.")]
		public void FilePath_Unit_EqualityOperator3_ValueIsNull() {
			String value = @"C:\Temp\MyFile.txt";
			FilePath filePath = new FilePath(value);
			String str = null;

			Boolean actual = str == filePath;
			Assert.AreEqual(false, actual);
		}
		[TestMethod()]
		[Description("String==FilePath operator when 'filePath' and 'value' are null references.")]
		public void FilePath_Unit_EqualityOperator3_FilePathIsNullAndValueIsNull() {
			FilePath filePath = null;
			String str = null;

			Boolean actual = str == filePath;
			Assert.AreEqual(true, actual);
		}

		[TestMethod()]
		[Description("FilePath!=FilePath operator when the same reference is compared.")]
		public void FilePath_Unit_InequalityOperator1_SameReferences() {
			String value = @"C:\Temp\MyFile.txt";
			FilePath objA = new FilePath(value);
			FilePath objB = objA;

			Boolean actual = objA != objB;
			Assert.AreEqual(false, actual);
		}
		[TestMethod()]
		[Description("FilePath!=FilePath operator when equivalent references are compared.")]
		public void FilePath_Unit_InequalityOperator1_EquivalentReferences() {
			String value = @"C:\Temp\MyFile.txt";
			FilePath objA = new FilePath(value);
			FilePath objB = new FilePath(value.ToUpperInvariant());

			Boolean actual = objA != objB;
			Assert.AreEqual(false, actual);
		}
		[TestMethod()]
		[Description("FilePath!=FilePath operator when unequivalent references are compared.")]
		public void FilePath_Unit_InequalityOperator1_UnequivalentReferences() {
			String value = @"C:\Temp\MyFile.txt";
			FilePath objA = new FilePath(value);
			FilePath objB = new FilePath(value + "2");

			Boolean actual = objA != objB;
			Assert.AreEqual(true, actual);
		}
		[TestMethod()]
		[Description("FilePath!=FilePath operator when 'objA' is a null reference.")]
		public void FilePath_Unit_InequalityOperator1_ObjAIsNull() {
			String value = @"C:\Temp\MyFile.txt";
			FilePath objA = null;
			FilePath objB = new FilePath(value);

			Boolean actual = objA != objB;
			Assert.AreEqual(true, actual);
		}
		[TestMethod()]
		[Description("FilePath!=FilePath operator when 'objB' is a null reference.")]
		public void FilePath_Unit_InequalityOperator1_ObjBIsNull() {
			String value = @"C:\Temp\MyFile.txt";
			FilePath objA = new FilePath(value);
			FilePath objB = null;

			Boolean actual = objA != objB;
			Assert.AreEqual(true, actual);
		}
		[TestMethod()]
		[Description("FilePath!=FilePath operator when 'objA' and 'objB' are null references.")]
		public void FilePath_Unit_InequalityOperator1_ObjAIsNullAndObjBIsNull() {
			FilePath objA = null;
			FilePath objB = null;

			Boolean actual = objA != objB;
			Assert.AreEqual(false, actual);
		}

		[TestMethod()]
		[Description("FilePath!=String operator when equivalent objects are compared.")]
		public void FilePath_Unit_InequalityOperator2_EquivalentObjects() {
			String value = @"C:\Temp\MyFile.txt";
			FilePath filePath = new FilePath(value);
			String str = value.ToUpperInvariant();

			Boolean actual = filePath != str;
			Assert.AreEqual(false, actual);
		}
		[TestMethod()]
		[Description("FilePath!=String operator when unequivalent objects are compared.")]
		public void FilePath_Unit_InequalityOperator2_UnequivalentObjects() {
			String value = @"C:\Temp\MyFile.txt";
			FilePath filePath = new FilePath(value);
			String str = value + "2";

			Boolean actual = filePath != str;
			Assert.AreEqual(true, actual);
		}
		[TestMethod()]
		[Description("FilePath!=String operator when 'filePath' is a null reference.")]
		public void FilePath_Unit_InequalityOperator2_FilePathIsNull() {
			String value = @"C:\Temp\MyFile.txt";
			FilePath filePath = null;
			String str = value;

			Boolean actual = filePath != str;
			Assert.AreEqual(true, actual);
		}
		[TestMethod()]
		[Description("FilePath!=String operator when 'value' is a null reference.")]
		public void FilePath_Unit_InequalityOperator2_ValueIsNull() {
			String value = @"C:\Temp\MyFile.txt";
			FilePath filePath = new FilePath(value);
			String str = null;

			Boolean actual = filePath != str;
			Assert.AreEqual(true, actual);
		}
		[TestMethod()]
		[Description("FilePath!=String operator when 'filePath' and 'value' are null references.")]
		public void FilePath_Unit_InequalityOperator2_FilePathIsNullAndValueIsNull() {
			FilePath filePath = null;
			String str = null;

			Boolean actual = filePath != str;
			Assert.AreEqual(false, actual);
		}

		[TestMethod()]
		[Description("String!=FilePath operator when equivalent objects are compared.")]
		public void FilePath_Unit_InequalityOperator3_EquivalentObjects() {
			String value = @"C:\Temp\MyFile.txt";
			FilePath filePath = new FilePath(value);
			String str = value.ToUpperInvariant();

			Boolean actual = str != filePath;
			Assert.AreEqual(false, actual);
		}
		[TestMethod()]
		[Description("String!=FilePath operator when unequivalent objects are compared.")]
		public void FilePath_Unit_InequalityOperator3_UnequivalentObjects() {
			String value = @"C:\Temp\MyFile.txt";
			FilePath filePath = new FilePath(value);
			String str = value + "2";

			Boolean actual = str != filePath;
			Assert.AreEqual(true, actual);
		}
		[TestMethod()]
		[Description("String!=FilePath operator when 'filePath' is a null reference.")]
		public void FilePath_Unit_InequalityOperator3_FilePathIsNull() {
			String value = @"C:\Temp\MyFile.txt";
			FilePath filePath = null;
			String str = value;

			Boolean actual = str != filePath;
			Assert.AreEqual(true, actual);
		}
		[TestMethod()]
		[Description("String!=FilePath operator when 'value' is a null reference.")]
		public void FilePath_Unit_InequalityOperator3_ValueIsNull() {
			String value = @"C:\Temp\MyFile.txt";
			FilePath filePath = new FilePath(value);
			String str = null;

			Boolean actual = str != filePath;
			Assert.AreEqual(true, actual);
		}
		[TestMethod()]
		[Description("String!=FilePath operator when 'filePath' and 'value' are null references.")]
		public void FilePath_Unit_InequalityOperator3_FilePathIsNullAndValueIsNull() {
			FilePath filePath = null;
			String str = null;

			Boolean actual = str != filePath;
			Assert.AreEqual(false, actual);
		}

		[TestMethod()]
		[Description("Implicit (String)FilePath operator for the optimal path.")]
		public void FilePath_Unit_ImplicitStringCastOperator_Optimal() {
			String value = @"C:\Temp\MyFile.txt";
			FilePath instance = new FilePath(value);

			String actual = instance;
			Assert.AreEqual(value, actual);
		}
		[TestMethod()]
		[Description("Implicit (String)FilePath operator when 'instance' is a null reference.")]
		public void FilePath_Unit_ImplicitStringCastOperator_InstanceIsNull() {
			FilePath instance = null;

			String actual = instance;
			Assert.IsNull(actual);
		}

		[TestMethod()]
		[Description("Explicit (FilePath)String operator for the optimal path.")]
		public void FilePath_Unit_ExplicitStringCastOperator_Optimal() {
			String instance = @"C:\Temp\MyFile.txt";

			FilePath actual = (FilePath)instance;
			Assert.AreEqual(instance, actual);
		}
		[TestMethod()]
		[Description("Explicit (FilePath)String operator when 'instance' is a null reference.")]
		public void FilePath_Unit_ExplicitStringCastOperator_InstanceIsNull() {
			String instance = null;

			FilePath actual = (FilePath)instance;
			Assert.IsNull(actual);
		}
		[TestMethod()]
		[Description("Explicit (FilePath)String operator when 'instance' is invalid.")]
		[ExpectedException(typeof(InvalidCastException))]
		public void FilePath_Unit_ExplicitStringCastOperator_InstanceIsInvalid() {
			String instance = new String('A', 261);

			FilePath actual = (FilePath)instance;
		}

	// Serialization Tests
		[TestMethod()]
		[Description("Serializability of the class for the optimal path.")]
		public void FilePath_Integration_Serialization_Optimal() {
			String value = @"C:\Temp\MyFile.txt";
			FilePath original = new FilePath(value);

			FilePath clone = original.SerializeBinary();
			Assert.AreNotSame(original, clone);
			Assert.AreEqual(original, clone);
			Assert.AreEqual(original.GetHashCode(), clone.GetHashCode());
		}
	}
}