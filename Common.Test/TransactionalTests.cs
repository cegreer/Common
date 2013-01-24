using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Vizistata {
	/// <summary>
	/// This is a test class for <see cref="T:Transactional&lt;T&gt;"/> and is intended to contain all <see cref="T:Transactional&lt;T&gt;"/> Unit Tests.
	///</summary>
	[TestClass()]
	public class TransactionalTests {
		#region Test Class Implementation
		/// <summary>
		/// Describes the context under which the current test is running.
		/// </summary>
		private TestContext _testContextInstance;

		/// <summary>
		/// Initializes a new instance of the <see cref="T:TransactionalTests"/> class.
		/// </summary>
		public TransactionalTests() : base() { }

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
		public void Transactional_Unit_Constructor1_Optimal() {
			new Transactional<String>();
		}

		[TestMethod()]
		[Description(".ctor(T) constructor for the optimal path.")]
		public void Transactional_Unit_Constructor2_Optimal() {
			String value = "Value";
			new Transactional<String>(value);
		}

		[TestMethod()]
		[Description(".ctor(T, T) constructor for the optimal path.")]
		public void Transactional_Unit_Constructor3_Optimal() {
			String currentValue = "Current";
			String originalValue = "Original";
			new Transactional<String>(currentValue, originalValue);
		}

	// Property Tests
		[TestMethod()]
		[Description("CurrentValue property for the optimal path.")]
		public void Transactional_Unit_CurrentValue_Optimal() {
			String currentValue = "Current";
			String originalValue = "Original";
			Transactional<String> target = new Transactional<String>(currentValue, originalValue);

			String actual = target.CurrentValue;
			Assert.AreEqual(currentValue, actual);
		}

		[TestMethod()]
		[Description("IsDirty property when the object has been changed.")]
		public void Transactional_Unit_IsDirty_Changed() {
			String currentValue = "Current";
			String originalValue = "Original";
			Transactional<String> target = new Transactional<String>(currentValue, originalValue);

			Boolean expected = true;
			Boolean actual = target.IsDirty;
			Assert.AreEqual(expected, actual);
		}
		[TestMethod()]
		[Description("IsDirty property when the object has not been changed.")]
		public void Transactional_Unit_IsDirty_NotChanged() {
			String currentValue = "Current";
			String originalValue = currentValue;
			Transactional<String> target = new Transactional<String>(currentValue, originalValue);

			Boolean expected = false;
			Boolean actual = target.IsDirty;
			Assert.AreEqual(expected, actual);
		}

		[TestMethod()]
		[Description("OriginalValue property for the optimal path.")]
		public void Transactional_Unit_OriginalValue_Optimal() {
			String currentValue = "Current";
			String originalValue = "Original";
			Transactional<String> target = new Transactional<String>(currentValue, originalValue);

			String actual = target.OriginalValue;
			Assert.AreEqual(originalValue, actual);
		}

	// Method Tests
		[TestMethod()]
		[Description("Commit() method when the object has been changed.")]
		public void Transactional_Unit_Commit_Changed() {
			String currentValue = "Current";
			String originalValue = "Original";
			Transactional<String> target = new Transactional<String>(currentValue, originalValue);

			target.Commit();

			Assert.AreEqual(currentValue, target.CurrentValue);
			Assert.AreEqual(currentValue, target.OriginalValue);
		}
		[TestMethod()]
		[Description("Commit() method when the object has not been changed.")]
		public void Transactional_Unit_Commit_NotChanged() {
			String currentValue = "Current";
			String originalValue = currentValue;
			Transactional<String> target = new Transactional<String>(currentValue, originalValue);

			target.Commit();

			Assert.AreEqual(currentValue, target.CurrentValue);
			Assert.AreEqual(currentValue, target.OriginalValue);
		}

		[TestMethod()]
		[Description("Equals(Object) method when 'obj' is the same reference.")]
		public void Transactional_Unit_Equals1_ObjIsSameReference() {
			String currentValue = "Current";
			String originalValue = "Original";
			Transactional<String> target = new Transactional<String>(currentValue, originalValue);
			Object obj = target;

			Boolean expected = true;
			Boolean actual = target.Equals(obj);
			Assert.AreEqual(expected, actual);
		}
		[TestMethod()]
		[Description("Equals(Object) method when 'obj' is an equivalent Transactional<T>.")]
		public void Transactional_Unit_Equals1_ObjIsEquivalentTransactional() {
			String currentValue = "Current";
			String originalValue = "Original";
			Transactional<String> target = new Transactional<String>(currentValue, originalValue);
			Object obj = new Transactional<String>(currentValue, originalValue);

			Boolean expected = true;
			Boolean actual = target.Equals(obj);
			Assert.AreEqual(expected, actual);
		}
		[TestMethod()]
		[Description("Equals(Object) method when 'obj' is an unequivalent Transactional<T>.")]
		public void Transactional_Unit_Equals1_ObjIsUnequivalentTransactional() {
			String currentValue = "Current";
			String originalValue = "Original";
			Transactional<String> target = new Transactional<String>(currentValue, originalValue);
			Object obj = new Transactional<String>(currentValue);

			Boolean expected = false;
			Boolean actual = target.Equals(obj);
			Assert.AreEqual(expected, actual);
		}
		[TestMethod()]
		[Description("Equals(Object) method when 'obj' is an equivalent T.")]
		public void Transactional_Unit_Equals1_ObjIsEquivalentT() {
			String currentValue = "Current";
			String originalValue = "Original";
			Transactional<String> target = new Transactional<String>(currentValue, originalValue);
			Object obj = currentValue;

			Boolean expected = true;
			Boolean actual = target.Equals(obj);
			Assert.AreEqual(expected, actual);
		}
		[TestMethod()]
		[Description("Equals(Object) method when 'obj' is an unequivalent T.")]
		public void Transactional_Unit_Equals1_ObjIsUnequivalentT() {
			String currentValue = "Current";
			String originalValue = "Original";
			Transactional<String> target = new Transactional<String>(currentValue, originalValue);
			Object obj = originalValue;

			Boolean expected = false;
			Boolean actual = target.Equals(obj);
			Assert.AreEqual(expected, actual);
		}
		[TestMethod()]
		[Description("Equals(Object) method when 'obj' is a null reference.")]
		public void Transactional_Unit_Equals1_ObjIsNull() {
			String currentValue = "Current";
			String originalValue = "Original";
			Transactional<String> target = new Transactional<String>(currentValue, originalValue);
			Object obj = null;

			Boolean expected = false;
			Boolean actual = target.Equals(obj);
			Assert.AreEqual(expected, actual);
		}
		[TestMethod()]
		[Description("Equals(Object) method when 'obj' is of a different type.")]
		public void Transactional_Unit_Equals1_ObjIsDifferentType() {
			String currentValue = "Current";
			String originalValue = "Original";
			Transactional<String> target = new Transactional<String>(currentValue, originalValue);
			Object obj = new Object();

			Boolean expected = false;
			Boolean actual = target.Equals(obj);
			Assert.AreEqual(expected, actual);
		}

		[TestMethod()]
		[Description("Equals(Transactional<T>) method when 'other' is the same reference.")]
		public void Transactional_Unit_Equals2_OtherIsSameReference() {
			String currentValue = "Current";
			String originalValue = "Original";
			Transactional<String> target = new Transactional<String>(currentValue, originalValue);
			Transactional<String> other = target;

			Boolean expected = true;
			Boolean actual = target.Equals(other);
			Assert.AreEqual(expected, actual);
		}
		[TestMethod()]
		[Description("Equals(Transactional<T>) method when 'other' is equivalent.")]
		public void Transactional_Unit_Equals2_OtherIsEquivalent() {
			String currentValue = "Current";
			String originalValue = "Original";
			Transactional<String> target = new Transactional<String>(currentValue, originalValue);
			Transactional<String> other = new Transactional<String>(currentValue, originalValue);

			Boolean expected = true;
			Boolean actual = target.Equals(other);
			Assert.AreEqual(expected, actual);
		}
		[TestMethod()]
		[Description("Equals(Transactional<T>) method when 'other' is unequivalent.")]
		public void Transactional_Unit_Equals2_OtherIsUnequivalent() {
			String currentValue = "Current";
			String originalValue = "Original";
			Transactional<String> target = new Transactional<String>(currentValue, originalValue);
			Transactional<String> other = new Transactional<String>(currentValue);

			Boolean expected = false;
			Boolean actual = target.Equals(other);
			Assert.AreEqual(expected, actual);
		}
		[TestMethod()]
		[Description("Equals(Transactional<T>) method when 'other' is a null reference.")]
		public void Transactional_Unit_Equals2_OtherIsNull() {
			String currentValue = "Current";
			String originalValue = "Original";
			Transactional<String> target = new Transactional<String>(currentValue, originalValue);
			Transactional<String> other = null;

			Boolean expected = false;
			Boolean actual = target.Equals(other);
			Assert.AreEqual(expected, actual);
		}
		[TestMethod()]
		[Description("Equals(Transactional<T>) method when the current and original values are null references.")]
		public void Transactional_Unit_Equals2_ValuesAreNull() {
			String currentValue = null;
			String originalValue = null;
			Transactional<String> target = new Transactional<String>(currentValue, originalValue);
			Transactional<String> other = new Transactional<String>(currentValue, originalValue);

			Boolean expected = true;
			Boolean actual = target.Equals(other);
			Assert.AreEqual(expected, actual);
		}

		[TestMethod()]
		[Description("Equals(T) method when 'other' is equivalent.")]
		public void Transactional_Unit_Equals3_OtherIsEquivalent() {
			String currentValue = "Current";
			String originalValue = "Original";
			Transactional<String> target = new Transactional<String>(currentValue, originalValue);
			String other = currentValue;

			Boolean expected = true;
			Boolean actual = target.Equals(other);
			Assert.AreEqual(expected, actual);
		}
		[TestMethod()]
		[Description("Equals(T) method when 'other' is unequivalent.")]
		public void Transactional_Unit_Equals3_OtherIsUnequivalent() {
			String currentValue = "Current";
			String originalValue = "Original";
			Transactional<String> target = new Transactional<String>(currentValue, originalValue);
			String other = originalValue;

			Boolean expected = false;
			Boolean actual = target.Equals(other);
			Assert.AreEqual(expected, actual);
		}
		[TestMethod()]
		[Description("Equals(T) method when 'other' is a null reference.")]
		public void Transactional_Unit_Equals3_OtherIsNull() {
			String currentValue = "Current";
			String originalValue = "Original";
			Transactional<String> target = new Transactional<String>(currentValue, originalValue);
			String other = null;

			Boolean expected = false;
			Boolean actual = target.Equals(other);
			Assert.AreEqual(expected, actual);
		}

		[TestMethod()]
		[Description("GetHashCode() method when the same reference is compared.")]
		public void Transactional_Unit_GetHashCode_SameReferences() {
			String currentValue = "Current";
			String originalValue = "Original";
			Transactional<String> first = new Transactional<String>(currentValue, originalValue);
			Transactional<String> second = first;

			Assert.AreEqual(first.GetHashCode(), second.GetHashCode());
		}
		[TestMethod()]
		[Description("GetHashCode() method when equivalent objects are compared.")]
		public void Transactional_Unit_GetHashCode_EquivalentObjects() {
			String currentValue = "Current";
			String originalValue = "Original";
			Transactional<String> first = new Transactional<String>(currentValue, originalValue);
			Transactional<String> second = new Transactional<String>(currentValue, originalValue);

			Assert.AreEqual(first.GetHashCode(), second.GetHashCode());
		}
		[TestMethod()]
		[Description("GetHashCode() method when unequivalent objects are compared.")]
		public void Transactional_Unit_GetHashCode_UnequivalentObjects() {
			String currentValue = "Current";
			String originalValue = "Original";
			Transactional<String> first = new Transactional<String>(currentValue, originalValue);
			Transactional<String> second = new Transactional<String>(currentValue);

			Int32 firstHash = first.GetHashCode();
			Int32 secondHash = second.GetHashCode();
			Assert.AreNotEqual(firstHash, secondHash);
		}
		[TestMethod()]
		[Description("GetHashCode() method when the current and original values are null references.")]
		public void Transactional_Unit_GetHashCode_NullValues() {
			String currentValue = null;
			String originalValue = null;
			Transactional<String> first = new Transactional<String>(currentValue, originalValue);
			Transactional<String> second = new Transactional<String>(currentValue, originalValue);

			Assert.AreEqual(first.GetHashCode(), second.GetHashCode());
		}

		[TestMethod()]
		[Description("Rollback() method when the object has been changed.")]
		public void Transactional_Unit_Rollback_Changed() {
			String currentValue = "Current";
			String originalValue = "Original";
			Transactional<String> target = new Transactional<String>(currentValue, originalValue);

			target.Rollback();

			Assert.AreEqual(originalValue, target.CurrentValue);
			Assert.AreEqual(originalValue, target.OriginalValue);
		}
		[TestMethod()]
		[Description("Rollback() method when the object has not been changed.")]
		public void Transactional_Unit_Rollback_NotChanged() {
			String currentValue = "Current";
			String originalValue = currentValue;
			Transactional<String> target = new Transactional<String>(currentValue, originalValue);

			target.Rollback();

			Assert.AreEqual(originalValue, target.CurrentValue);
			Assert.AreEqual(originalValue, target.OriginalValue);
		}

		[TestMethod()]
		[Description("Set(T) method for the optimal path.")]
		public void Transactional_Unit_Set1_Optimal() {
			String currentValue = "Current";
			String originalValue = "Original";
			Transactional<String> target = new Transactional<String>(currentValue, originalValue);

			String value = "Test";
			target.Set(value);

			Assert.AreEqual(value, target.CurrentValue);
			Assert.AreEqual(originalValue, target.OriginalValue);
		}

		[TestMethod()]
		[Description("Set(T, Boolean) method when the value is set and committed.")]
		public void Transactional_Unit_Set2_ChangedAndCommitted() {
			String currentValue = "Current";
			String originalValue = "Original";
			Transactional<String> target = new Transactional<String>(currentValue, originalValue);

			String value = "Test";
			Boolean commit = true;
			target.Set(value, commit);

			Assert.AreEqual(value, target.CurrentValue);
			Assert.AreEqual(value, target.OriginalValue);
		}
		[TestMethod()]
		[Description("Set(T, Boolean) method when the value is set and not committed.")]
		public void Transactional_Unit_Set2_ChangedAndNotCommitted() {
			String currentValue = "Current";
			String originalValue = "Original";
			Transactional<String> target = new Transactional<String>(currentValue, originalValue);

			String value = "Test";
			Boolean commit = false;
			target.Set(value, commit);

			Assert.AreEqual(value, target.CurrentValue);
			Assert.AreEqual(originalValue, target.OriginalValue);
		}
		[TestMethod()]
		[Description("Set(T, Boolean) method when the value is not set and committed.")]
		public void Transactional_Unit_Set2_NotChangedAndCommitted() {
			String currentValue = "Current";
			String originalValue = "Original";
			Transactional<String> target = new Transactional<String>(currentValue, originalValue);

			String value = currentValue;
			Boolean commit = true;
			target.Set(value, commit);

			Assert.AreEqual(currentValue, target.CurrentValue);
			Assert.AreEqual(originalValue, target.OriginalValue);
		}

		[TestMethod()]
		[Description("ToString() method for the optimal path.")]
		public void Transactional_Unit_ToString_Optimal() {
			String currentValue = "Current";
			String originalValue = "Original";
			Transactional<String> target = new Transactional<String>(currentValue, originalValue);

			String actual = target.ToString();
			Assert.AreEqual(currentValue, actual);
		}
		[TestMethod()]
		[Description("ToString() method when the current value is a null reference.")]
		public void Transactional_Unit_ToString_CurrentValueIsNull() {
			String currentValue = null;
			String originalValue = "Original";
			Transactional<String> target = new Transactional<String>(currentValue, originalValue);

			String actual = target.ToString();
			Assert.AreNotEqual(currentValue, actual);
		}

	// Operator Tests
		[TestMethod()]
		[Description("==(Transactional<T>, Transactional<T>) operator when the objects are the same reference.")]
		public void Transactional_Unit_EqualityOperator1_SameReferences() {
			String currentValue = "Current";
			String originalValue = "Original";
			Transactional<String> objA = new Transactional<String>(currentValue, originalValue);
			Transactional<String> objB = objA;

			Boolean expected = true;
			Boolean actual = objA == objB;
			Assert.AreEqual(expected, actual);
		}
		[TestMethod()]
		[Description("==(Transactional<T>, Transactional<T>) operator when the objects are equivalent objects.")]
		public void Transactional_Unit_EqualityOperator1_EquivalentObjects() {
			String currentValue = "Current";
			String originalValue = "Original";
			Transactional<String> objA = new Transactional<String>(currentValue, originalValue);
			Transactional<String> objB = new Transactional<String>(currentValue, originalValue);

			Boolean expected = true;
			Boolean actual = objA == objB;
			Assert.AreEqual(expected, actual);
		}
		[TestMethod()]
		[Description("==(Transactional<T>, Transactional<T>) operator when the objects are not equivalent objects.")]
		public void Transactional_Unit_EqualityOperator1_UnequivalentObjects() {
			String currentValue = "Current";
			String originalValue = "Original";
			Transactional<String> objA = new Transactional<String>(currentValue, originalValue);
			Transactional<String> objB = new Transactional<String>(currentValue);

			Boolean expected = false;
			Boolean actual = objA == objB;
			Assert.AreEqual(expected, actual);
		}
		[TestMethod()]
		[Description("==(Transactional<T>, Transactional<T>) operator when 'objA' is a null reference.")]
		public void Transactional_Unit_EqualityOperator1_ObjAIsNull() {
			String currentValue = "Current";
			String originalValue = "Original";
			Transactional<String> objA = null;
			Transactional<String> objB = new Transactional<String>(currentValue, originalValue);

			Boolean expected = false;
			Boolean actual = objA == objB;
			Assert.AreEqual(expected, actual);
		}
		[TestMethod()]
		[Description("==(Transactional<T>, Transactional<T>) operator when 'objB' is a null reference.")]
		public void Transactional_Unit_EqualityOperator1_ObjBIsNull() {
			String currentValue = "Current";
			String originalValue = "Original";
			Transactional<String> objA = new Transactional<String>(currentValue, originalValue);
			Transactional<String> objB = null;

			Boolean expected = false;
			Boolean actual = objA == objB;
			Assert.AreEqual(expected, actual);
		}
		[TestMethod()]
		[Description("==(Transactional<T>, Transactional<T>) operator when 'objA' and 'objB' are null references.")]
		public void Transactional_Unit_EqualityOperator1_ObjAIsNullAndObjBIsNull() {
			Transactional<String> objA = null;
			Transactional<String> objB = null;

			Boolean expected = true;
			Boolean actual = objA == objB;
			Assert.AreEqual(expected, actual);
		}

		[TestMethod()]
		[Description("==(Transactional<T>, T) operator when the objects are equivalent objects.")]
		public void Transactional_Unit_EqualityOperator2_EquivalentObjects() {
			String currentValue = "Current";
			String originalValue = "Original";
			Transactional<String> objA = new Transactional<String>(currentValue, originalValue);
			String objB = currentValue;

			Boolean expected = true;
			Boolean actual = objA == objB;
			Assert.AreEqual(expected, actual);
		}
		[TestMethod()]
		[Description("==(Transactional<T>, T) operator when the objects are not equivalent objects.")]
		public void Transactional_Unit_EqualityOperator2_UnequivalentObjects() {
			String currentValue = "Current";
			String originalValue = "Original";
			Transactional<String> objA = new Transactional<String>(currentValue, originalValue);
			String objB = originalValue;

			Boolean expected = false;
			Boolean actual = objA == objB;
			Assert.AreEqual(expected, actual);
		}
		[TestMethod()]
		[Description("==(Transactional<T>, T) operator when 'objA' is a null reference.")]
		public void Transactional_Unit_EqualityOperator2_ObjAIsNull() {
			String currentValue = "Current";
			Transactional<String> objA = null;
			String objB = currentValue;

			Boolean expected = false;
			Boolean actual = objA == objB;
			Assert.AreEqual(expected, actual);
		}
		[TestMethod()]
		[Description("==(Transactional<T>, T) operator when 'objB' is a null reference.")]
		public void Transactional_Unit_EqualityOperator2_ObjBIsNull() {
			String currentValue = "Current";
			String originalValue = "Original";
			Transactional<String> objA = new Transactional<String>(currentValue, originalValue);
			String objB = null;

			Boolean expected = false;
			Boolean actual = objA == objB;
			Assert.AreEqual(expected, actual);
		}
		[TestMethod()]
		[Description("==(Transactional<T>, T) operator when 'objA' and 'objB' are null references.")]
		public void Transactional_Unit_EqualityOperator2_ObjAIsNullAndObjBIsNull() {
			Transactional<String> objA = null;
			String objB = null;

			Boolean expected = true;
			Boolean actual = objA == objB;
			Assert.AreEqual(expected, actual);
		}

		[TestMethod()]
		[Description("==(T, Transactional<T>) operator when the objects are equivalent objects.")]
		public void Transactional_Unit_EqualityOperator3_EquivalentObjects() {
			String currentValue = "Current";
			String originalValue = "Original";
			String objA = currentValue;
			Transactional<String> objB = new Transactional<String>(currentValue, originalValue);

			Boolean expected = true;
			Boolean actual = objA == objB;
			Assert.AreEqual(expected, actual);
		}
		[TestMethod()]
		[Description("==(T, Transactional<T>) operator when the objects are not equivalent objects.")]
		public void Transactional_Unit_EqualityOperator3_UnequivalentObjects() {
			String currentValue = "Current";
			String originalValue = "Original";
			String objA = originalValue;
			Transactional<String> objB = new Transactional<String>(currentValue, originalValue);

			Boolean expected = false;
			Boolean actual = objA == objB;
			Assert.AreEqual(expected, actual);
		}
		[TestMethod()]
		[Description("==(T, Transactional<T>) operator when 'objA' is a null reference.")]
		public void Transactional_Unit_EqualityOperator3_ObjAIsNull() {
			String currentValue = "Current";
			String originalValue = "Original";
			String objA = null;
			Transactional<String> objB = new Transactional<String>(currentValue, originalValue);

			Boolean expected = false;
			Boolean actual = objA == objB;
			Assert.AreEqual(expected, actual);
		}
		[TestMethod()]
		[Description("==(T, Transactional<T>) operator when 'objB' is a null reference.")]
		public void Transactional_Unit_EqualityOperator3_ObjBIsNull() {
			String currentValue = "Current";
			String objA = currentValue;
			Transactional<String> objB = null;

			Boolean expected = false;
			Boolean actual = objA == objB;
			Assert.AreEqual(expected, actual);
		}
		[TestMethod()]
		[Description("==(T, Transactional<T>) operator when 'objA' and 'objB' are null references.")]
		public void Transactional_Unit_EqualityOperator3_ObjAIsNullAndObjBIsNull() {
			String objA = null;
			Transactional<String> objB = null;

			Boolean expected = true;
			Boolean actual = objA == objB;
			Assert.AreEqual(expected, actual);
		}

		[TestMethod()]
		[Description("!=(Transactional<T>, Transactional<T>) operator when the objects are the same reference.")]
		public void Transactional_Unit_InequalityOperator1_SameReferences() {
			String currentValue = "Current";
			String originalValue = "Original";
			Transactional<String> objA = new Transactional<String>(currentValue, originalValue);
			Transactional<String> objB = objA;

			Boolean expected = false;
			Boolean actual = objA != objB;
			Assert.AreEqual(expected, actual);
		}
		[TestMethod()]
		[Description("!=(Transactional<T>, Transactional<T>) operator when the objects are equivalent objects.")]
		public void Transactional_Unit_InequalityOperator1_EquivalentObjects() {
			String currentValue = "Current";
			String originalValue = "Original";
			Transactional<String> objA = new Transactional<String>(currentValue, originalValue);
			Transactional<String> objB = new Transactional<String>(currentValue, originalValue);

			Boolean expected = false;
			Boolean actual = objA != objB;
			Assert.AreEqual(expected, actual);
		}
		[TestMethod()]
		[Description("!=(Transactional<T>, Transactional<T>) operator when the objects are not equivalent objects.")]
		public void Transactional_Unit_InequalityOperator1_UnequivalentObjects() {
			String currentValue = "Current";
			String originalValue = "Original";
			Transactional<String> objA = new Transactional<String>(currentValue, originalValue);
			Transactional<String> objB = new Transactional<String>(currentValue);

			Boolean expected = true;
			Boolean actual = objA != objB;
			Assert.AreEqual(expected, actual);
		}
		[TestMethod()]
		[Description("!=(Transactional<T>, Transactional<T>) operator when 'objA' is a null reference.")]
		public void Transactional_Unit_InequalityOperator1_ObjAIsNull() {
			String currentValue = "Current";
			String originalValue = "Original";
			Transactional<String> objA = null;
			Transactional<String> objB = new Transactional<String>(currentValue, originalValue);

			Boolean expected = true;
			Boolean actual = objA != objB;
			Assert.AreEqual(expected, actual);
		}
		[TestMethod()]
		[Description("!=(Transactional<T>, Transactional<T>) operator when 'objB' is a null reference.")]
		public void Transactional_Unit_InequalityOperator1_ObjBIsNull() {
			String currentValue = "Current";
			String originalValue = "Original";
			Transactional<String> objA = new Transactional<String>(currentValue, originalValue);
			Transactional<String> objB = null;

			Boolean expected = true;
			Boolean actual = objA != objB;
			Assert.AreEqual(expected, actual);
		}
		[TestMethod()]
		[Description("!=(Transactional<T>, Transactional<T>) operator when 'objA' and 'objB' are null references.")]
		public void Transactional_Unit_InequalityOperator1_ObjAIsNullAndObjBIsNull() {
			Transactional<String> objA = null;
			Transactional<String> objB = null;

			Boolean expected = false;
			Boolean actual = objA != objB;
			Assert.AreEqual(expected, actual);
		}

		[TestMethod()]
		[Description("!=(Transactional<T>, T) operator when the objects are equivalent objects.")]
		public void Transactional_Unit_InequalityOperator2_EquivalentObjects() {
			String currentValue = "Current";
			String originalValue = "Original";
			Transactional<String> objA = new Transactional<String>(currentValue, originalValue);
			String objB = currentValue;

			Boolean expected = false;
			Boolean actual = objA != objB;
			Assert.AreEqual(expected, actual);
		}
		[TestMethod()]
		[Description("!=(Transactional<T>, T) operator when the objects are not equivalent objects.")]
		public void Transactional_Unit_InequalityOperator2_UnequivalentObjects() {
			String currentValue = "Current";
			String originalValue = "Original";
			Transactional<String> objA = new Transactional<String>(currentValue, originalValue);
			String objB = originalValue;

			Boolean expected = true;
			Boolean actual = objA != objB;
			Assert.AreEqual(expected, actual);
		}
		[TestMethod()]
		[Description("!=(Transactional<T>, T) operator when 'objA' is a null reference.")]
		public void Transactional_Unit_InequalityOperator2_ObjAIsNull() {
			String currentValue = "Current";
			Transactional<String> objA = null;
			String objB = currentValue;

			Boolean expected = true;
			Boolean actual = objA != objB;
			Assert.AreEqual(expected, actual);
		}
		[TestMethod()]
		[Description("!=(Transactional<T>, T) operator when 'objB' is a null reference.")]
		public void Transactional_Unit_InequalityOperator2_ObjBIsNull() {
			String currentValue = "Current";
			String originalValue = "Original";
			Transactional<String> objA = new Transactional<String>(currentValue, originalValue);
			String objB = null;

			Boolean expected = true;
			Boolean actual = objA != objB;
			Assert.AreEqual(expected, actual);
		}
		[TestMethod()]
		[Description("!=(Transactional<T>, T) operator when 'objA' and 'objB' are null references.")]
		public void Transactional_Unit_InequalityOperator2_ObjAIsNullAndObjBIsNull() {
			Transactional<String> objA = null;
			String objB = null;

			Boolean expected = false;
			Boolean actual = objA != objB;
			Assert.AreEqual(expected, actual);
		}

		[TestMethod()]
		[Description("!=(T, Transactional<T>) operator when the objects are equivalent objects.")]
		public void Transactional_Unit_InequalityOperator3_EquivalentObjects() {
			String currentValue = "Current";
			String originalValue = "Original";
			String objA = currentValue;
			Transactional<String> objB = new Transactional<String>(currentValue, originalValue);

			Boolean expected = false;
			Boolean actual = objA != objB;
			Assert.AreEqual(expected, actual);
		}
		[TestMethod()]
		[Description("!=(T, Transactional<T>) operator when the objects are not equivalent objects.")]
		public void Transactional_Unit_InequalityOperator3_UnequivalentObjects() {
			String currentValue = "Current";
			String originalValue = "Original";
			String objA = originalValue;
			Transactional<String> objB = new Transactional<String>(currentValue, originalValue);

			Boolean expected = true;
			Boolean actual = objA != objB;
			Assert.AreEqual(expected, actual);
		}
		[TestMethod()]
		[Description("!=(T, Transactional<T>) operator when 'objA' is a null reference.")]
		public void Transactional_Unit_InequalityOperator3_ObjAIsNull() {
			String currentValue = "Current";
			String originalValue = "Original";
			String objA = null;
			Transactional<String> objB = new Transactional<String>(currentValue, originalValue);

			Boolean expected = true;
			Boolean actual = objA != objB;
			Assert.AreEqual(expected, actual);
		}
		[TestMethod()]
		[Description("!=(T, Transactional<T>) operator when 'objB' is a null reference.")]
		public void Transactional_Unit_InequalityOperator3_ObjBIsNull() {
			String currentValue = "Current";
			String objA = currentValue;
			Transactional<String> objB = null;

			Boolean expected = true;
			Boolean actual = objA != objB;
			Assert.AreEqual(expected, actual);
		}
		[TestMethod()]
		[Description("!=(T, Transactional<T>) operator when 'objA' and 'objB' are null references.")]
		public void Transactional_Unit_InequalityOperator3_ObjAIsNullAndObjBIsNull() {
			String objA = null;
			Transactional<String> objB = null;

			Boolean expected = false;
			Boolean actual = objA != objB;
			Assert.AreEqual(expected, actual);
		}

		[TestMethod()]
		[Description("T(Transactional<T>) operator for the optimal path.")]
		public void Transactional_Unit_ImplicitTCastOperator_Optimal() {
			String currentValue = "Current";
			String originalValue = "Original";
			Transactional<String> instance = new Transactional<String>(currentValue, originalValue);

			String actual = instance;
			Assert.AreEqual(currentValue, actual);
		}
		[TestMethod()]
		[Description("T(Transactional<T>) operator when 'instance' is a null reference.")]
		public void Transactional_Unit_ImplicitTCastOperator_InstanceIsNull() {
			Transactional<String> instance = null;

			String actual = instance;
			Assert.IsNull(actual);
		}

		[TestMethod()]
		[Description("Transactional<T>(T) operator for the optimal path.")]
		public void Transactional_Unit_ExplicitTransactionalCastOperator_Optimal() {
			String instance = "Test";

			Transactional<String> actual = (Transactional<String>)instance;
			Assert.AreEqual(instance, actual.CurrentValue);
			Assert.AreEqual(instance, actual.OriginalValue);
		}
		[TestMethod()]
		[Description("Transactional<T>(T) operator when 'instance' is a null reference.")]
		public void Transactional_Unit_ExplicitTransactionalCastOperator_InstanceIsNull() {
			String instance = null;

			Transactional<String> actual = (Transactional<String>)instance;
			Assert.AreEqual(instance, actual.CurrentValue);
			Assert.AreEqual(instance, actual.OriginalValue);
		}
	}
}