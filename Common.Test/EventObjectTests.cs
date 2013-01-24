using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Vizistata {
	/// <summary>
	/// This is a test class for <see cref="T:EventObject"/> and is intended to contain all <see cref="T:EventObject"/> Unit Tests.
	///</summary>
	[TestClass()]
	public class EventObjectTests {
		#region Test Class Implementation
		/// <summary>
		/// Describes the context under which the current test is running.
		/// </summary>
		private TestContext _testContextInstance;

		/// <summary>
		/// Initializes a new instance of the <see cref="T:EventObjectTests"/> class.
		/// </summary>
		public EventObjectTests() : base() { }

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
		public void EventObject_Unit_Constructor1_Optimal() {
			new EventObject();
		}

		[TestMethod()]
		[Description(".ctor(Boolean) constructor when 'ignoreSerializable' is true.")]
		public void EventObject_Unit_Constructor2_IgnoreSerializableIsTrue() {
			Boolean ignoreSerializable = true;
			new EventObject(ignoreSerializable);
		}
		[TestMethod()]
		[Description(".ctor(Boolean) constructor when 'ignoreSerializable' is false.")]
		public void EventObject_Unit_Constructor2_IgnoreSerializableIsFalse() {
			Boolean ignoreSerializable = false;
			new EventObject(ignoreSerializable);
		}

		[TestMethod()]
		[Description("EventObject<T>.ctor() constructor for the optimal path.")]
		public void EventObject_Unit_Constructor3_Optimal() {
			new EventObject<ConsoleCancelEventArgs>();
		}

		[TestMethod()]
		[Description("EventObject<T>.ctor(Boolean) constructor when 'ignoreSerializable' is true.")]
		public void EventObject_Unit_Constructor4_IgnoreSerializableIsTrue() {
			Boolean ignoreSerializable = true;
			new EventObject<ConsoleCancelEventArgs>(ignoreSerializable);
		}
		[TestMethod()]
		[Description("EventObject<T>.ctor(Boolean) constructor when 'ignoreSerializable' is false.")]
		public void EventObject_Unit_Constructor4_IgnoreSerializableIsFalse() {
			Boolean ignoreSerializable = false;
			new EventObject<ConsoleCancelEventArgs>(ignoreSerializable);
		}

	// Method Tests
		[TestMethod()]
		[Description("AddHandler(EventHandler) method for the optimal path.")]
		public void EventObject_Unit_AddHandler1_Optimal() {
			EventObject target = new EventObject();
			EventHandler value = (sender, e) => { };
			target.AddHandler(value);
		}
		[TestMethod()]
		[Description("AddHandler(EventHandler) method when 'value' is a null reference.")]
		[ExpectedException(typeof(ArgumentNullException))]
		public void EventObject_Unit_AddHandler1_ValueIsNull() {
			EventObject target = new EventObject();
			EventHandler value = null;
			target.AddHandler(value);
		}

		[TestMethod()]
		[Description("EventObject<T>.AddHandler(EventHandler<T>) method for the optimal path.")]
		public void EventObject_Unit_AddHandler2_Optimal() {
			EventObject<ConsoleCancelEventArgs> target = new EventObject<ConsoleCancelEventArgs>();
			EventHandler<ConsoleCancelEventArgs> value = (sender, e) => { };
			target.AddHandler(value);
		}
		[TestMethod()]
		[Description("EventObject<T>.AddHandler(EventHandler<T>) method when 'value' is a null reference.")]
		[ExpectedException(typeof(ArgumentNullException))]
		public void EventObject_Unit_AddHandler2_ValueIsNull() {
			EventObject<ConsoleCancelEventArgs> target = new EventObject<ConsoleCancelEventArgs>();
			EventHandler<ConsoleCancelEventArgs> value = null;
			target.AddHandler(value);
		}

		[TestMethod()]
		[Description("RaiseEvent(Object, EventArgs) method for the optimal path.")]
		public void EventObject_Unit_RaiseEvent1_Optimal() {
			EventObject target = new EventObject();

			Boolean isCalled = false;
			EventHandler value = (sender, e) => { isCalled = true; };
			target.AddHandler(value);
			target.RaiseEvent(this, EventArgs.Empty);

			Assert.AreEqual(true, isCalled);
		}
		[TestMethod()]
		[Description("RaiseEvent(Object, EventArgs) method when no event handlers exist.")]
		public void EventObject_Unit_RaiseEvent1_NoEventHandler() {
			EventObject target = new EventObject();
			target.RaiseEvent(this, EventArgs.Empty);
		}
		[TestMethod()]
		[Description("RaiseEvent(Object, EventArgs) method when 'sender' and 'e' are null references.")]
		public void EventObject_Unit_RaiseEvent1_SenderIsNullAndEIsNull() {
			EventObject target = new EventObject();

			Boolean isCalled = false;
			EventHandler value = (sender, e) => { isCalled = true; };
			target.AddHandler(value);
			target.RaiseEvent(null, null);

			Assert.AreEqual(true, isCalled);
		}

		[TestMethod()]
		[Description("EventObject<T>.RaiseEvent(Object, T) method for the optimal path.")]
		public void EventObject_Unit_RaiseEvent2_Optimal() {
			EventObject<EventArgs> target = new EventObject<EventArgs>();

			Boolean isCalled = false;
			EventHandler<EventArgs> value = (sender, e) => { isCalled = true; };
			target.AddHandler(value);
			target.RaiseEvent(this, EventArgs.Empty);

			Assert.AreEqual(true, isCalled);
		}
		[TestMethod()]
		[Description("EventObject<T>.RaiseEvent(Object, T) method when no event handlers exist.")]
		public void EventObject_Unit_RaiseEvent2_NoEventHandler() {
			EventObject<EventArgs> target = new EventObject<EventArgs>();
			target.RaiseEvent(this, EventArgs.Empty);
		}
		[TestMethod()]
		[Description("EventObject<T>.RaiseEvent(Object, T) method when 'sender' and 'e' are null references.")]
		public void EventObject_Unit_RaiseEvent2_SenderIsNullAndEIsNull() {
			EventObject<EventArgs> target = new EventObject<EventArgs>();

			Boolean isCalled = false;
			EventHandler<EventArgs> value = (sender, e) => { isCalled = true; };
			target.AddHandler(value);
			target.RaiseEvent(null, null);

			Assert.AreEqual(true, isCalled);
		}

		[TestMethod()]
		[Description("RemoveHandler(EventHandler) method for the optimal path.")]
		public void EventObject_Unit_RemoveHandler1_Optimal() {
			EventObject target = new EventObject();
			EventHandler value = (sender, e) => { };
			target.AddHandler(value);
			target.RemoveHandler(value);
		}
		[TestMethod()]
		[Description("RemoveHandler(EventHandler) method when the event handler was not added.")]
		public void EventObject_Unit_RemoveHandler1_NoEventHandler() {
			EventObject target = new EventObject();
			EventHandler value = (sender, e) => { };
			target.RemoveHandler(value);
		}
		[TestMethod()]
		[Description("RemoveHandler(EventHandler) method when 'value' is a null reference.")]
		[ExpectedException(typeof(ArgumentNullException))]
		public void EventObject_Unit_RemoveHandler1_ValueIsNull() {
			EventObject target = new EventObject();
			EventHandler value = null;
			target.RemoveHandler(value);
		}

		[TestMethod()]
		[Description("EventObject<T>.RemoveHandler(EventHandler) method for the optimal path.")]
		public void EventObject_Unit_RemoveHandler2_Optimal() {
			EventObject<EventArgs> target = new EventObject<EventArgs>();
			EventHandler<EventArgs> value = (sender, e) => { };
			target.AddHandler(value);
			target.RemoveHandler(value);
		}
		[TestMethod()]
		[Description("EventObject<T>.RemoveHandler(EventHandler) method when the event handler was not added.")]
		public void EventObject_Unit_RemoveHandler2_NoEventHandler() {
			EventObject<EventArgs> target = new EventObject<EventArgs>();
			EventHandler<EventArgs> value = (sender, e) => { };
			target.RemoveHandler(value);
		}
		[TestMethod()]
		[Description("EventObject<T>.RemoveHandler(EventHandler) method when 'value' is a null reference.")]
		[ExpectedException(typeof(ArgumentNullException))]
		public void EventObject_Unit_RemoveHandler2_ValueIsNull() {
			EventObject<EventArgs> target = new EventObject<EventArgs>();
			EventHandler<EventArgs> value = null;
			target.RemoveHandler(value);
		}
	}
}