using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Vizistata.Mocks;

using Thread = System.Threading.Thread;

namespace Vizistata {
	/// <summary>
	/// This is a test class for <see cref="T:AsyncJob"/> and is intended to contain all <see cref="T:AsyncJob"/> Unit Tests.
	///</summary>
	[TestClass()]
	public class AsyncJobTests {
		#region Test Class Implementation
		/// <summary>
		/// Describes the context under which the current test is running.
		/// </summary>
		private TestContext _testContextInstance;

		/// <summary>
		/// Initializes a new instance of the <see cref="T:JobTests"/> class.
		/// </summary>
		public AsyncJobTests() : base() { }

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

	// Methods
		/// <summary>
		/// Waits up to a second for the job to complete.
		/// </summary>
		/// <param name="job">The job for which to wait.</param>
		/// <exception cref="Microsoft.VisualStudio.TestTools.UnitTesting.AssertInconclusiveException">The job takes longer than 1 second to stop.</exception>
		private static void WaitOneSecond(AsyncJob job) {
			Debug.Assert(job != null);

			if (!job.WaitToStop(TimeSpan.FromSeconds(1))) {
				job.Stop();
				job.Dispose();
				Assert.Inconclusive("The job has been running for over a second and did not stopped.  The job was forcibly stopped and disposed.");
			}
		}

	// Constructor Tests
		[TestMethod()]
		[Description(".ctor() constructor for the optimal path.")]
		public void AsyncJob_Unit_Constructor_Optimal() {
			using (new MockAsyncJob()) { }
		}

	// Property Tests
		[TestMethod()]
		[Description("Error property when an error occurs.")]
		public void AsyncJob_Unit_Error_ErrorOccurs() {
			String message = "This is a test.";
			Action executeOneAction = () => { throw new ApplicationException(message); };
			Func<Boolean> isCompleteFunc = () => false;
			using (AsyncJob target = new MockAsyncJob(executeOneAction, isCompleteFunc)) {
				target.Start();
				AsyncJobTests.WaitOneSecond(target);

				Exception actual = target.Error;
				Assert.IsNotNull(actual);
				Assert.AreEqual(message, actual.Message);
			}
		}
		[TestMethod()]
		[Description("Error property when no error occurs.")]
		public void AsyncJob_Unit_Error_NoErrorOccurs() {
			Action executeOneAction = () => { };
			using (AsyncJob target = new MockAsyncJob(executeOneAction)) {
				target.Start();
				AsyncJobTests.WaitOneSecond(target);

				Exception actual = target.Error;
				Assert.IsNull(actual);
			}
		}
		[TestMethod()]
		[Description("Error property when the job hasn't run.")]
		public void AsyncJob_Unit_Error_JobNotRun() {
			Action executeOneAction = () => { throw new ApplicationException("This is a test."); };
			using (AsyncJob target = new MockAsyncJob(executeOneAction)) {
				Exception actual = target.Error;
				Assert.IsNull(actual);
			}
		}

		[TestMethod()]
		[Description("IsRunning property when the job is running.")]
		public void AsyncJob_Unit_IsRunning_JobIsRunning() {
			Action executeOneAction = () => Thread.Sleep(10);
			Func<Boolean> isCompleteFunc = () => false;
			using (AsyncJob target = new MockAsyncJob()) {
				target.Start();

				Boolean actual = target.IsRunning;
				Assert.AreEqual(true, actual);

				target.Stop();
			}
		}
		[TestMethod()]
		[Description("IsRunning property when the job has not started running.")]
		public void AsyncJob_Unit_IsRunning_JobNotStarted() {
			Action executeOneAction = () => Thread.Sleep(10);
			Func<Boolean> isCompleteFunc = () => false;
			using (AsyncJob target = new MockAsyncJob()) {
				Boolean actual = target.IsRunning;
				Assert.AreEqual(false, actual);
			}
		}
		[TestMethod()]
		[Description("IsRunning property when the job has stopped running.")]
		public void AsyncJob_Unit_IsRunning_JobStopped() {
			Action executeOneAction = () => Thread.Sleep(10);
			Func<Boolean> isCompleteFunc = () => false;
			using (AsyncJob target = new MockAsyncJob()) {
				target.Start();
				target.Stop();

				Boolean actual = target.IsRunning;
				Assert.AreEqual(false, actual);
			}
		}

	// Method Tests
		[TestMethod()]
		[Description("Start() method for the optimal path.")]
		public void AsyncJob_Unit_Start_Optimal() {
			Action executeOneAction = () => Thread.Sleep(10);
			Func<Boolean> isCompleteFunc = () => false;
			using (AsyncJob target = new MockAsyncJob(executeOneAction, isCompleteFunc)) {
				target.Start();
				target.Stop();
			}
		}
		[TestMethod()]
		[Description("Start() method when the method is called twice.")]
		public void AsyncJob_Unit_Start_CalledTwice() {
			Action executeOneAction = () => Thread.Sleep(10);
			Func<Boolean> isCompleteFunc = () => false;
			using (AsyncJob target = new MockAsyncJob(executeOneAction, isCompleteFunc)) {
				target.Start();
				target.Start();
				target.Stop();
			}
		}

		[TestMethod()]
		[Description("Stop() method for the optimal path.")]
		public void AsyncJob_Unit_Stop_Optimal() {
			Action executeOneAction = () => Thread.Sleep(10);
			Func<Boolean> isCompleteFunc = () => false;
			using (AsyncJob target = new MockAsyncJob(executeOneAction, isCompleteFunc)) {
				target.Start();
				target.Stop();
			}
		}
		[TestMethod()]
		[Description("Stop() method when the job has not started.")]
		public void AsyncJob_Unit_Stop_JobIsNotStarted() {
			Action executeOneAction = () => Thread.Sleep(10);
			Func<Boolean> isCompleteFunc = () => false;
			using (AsyncJob target = new MockAsyncJob(executeOneAction, isCompleteFunc)) {
				target.Stop();
			}
		}
		[TestMethod()]
		[Description("Stop() method when the job has already started.")]
		public void AsyncJob_Unit_Stop_JobIsStopped() {
			Action executeOneAction = () => { };
			Func<Boolean> isCompleteFunc = () => true;
			using (AsyncJob target = new MockAsyncJob(executeOneAction, isCompleteFunc)) {
				target.Start();
				AsyncJobTests.WaitOneSecond(target);
				target.Stop();
			}
		}

		[TestMethod()]
		[Description("WaitToStop(TimeSpan) method when 'timeoutDuration' is less than the job's duration.")]
		public void AsyncJob_Unit_WaitToStop_TimeoutDurationIsLessThanJobDuration() {
			List<Object> list = Enumerable.Range(0, 100).Select(i => new Object()).ToList();
			Action executeOneAction = () => { list.RemoveAt(0); Thread.Sleep(10); };
			Func<Boolean> isCompleteFunc = () => list.Count > 0;
			using (AsyncJob target = new MockAsyncJob(executeOneAction, isCompleteFunc)) {
				target.Start();
				
				Boolean actual = target.WaitToStop(TimeSpan.FromMilliseconds(10));
				Assert.AreEqual(false, actual);

				target.Stop();
			}
		}
		[TestMethod()]
		[Description("WaitToStop(TimeSpan) method when 'timeoutDuration' is greater than the job's duration.")]
		public void AsyncJob_Unit_WaitToStop_TimeoutDurationIsGreaterThanJobDuration() {
			List<Object> list = Enumerable.Range(0, 10).Select(i => new Object()).ToList();
			Action executeOneAction = () => { list.RemoveAt(0); Thread.Sleep(10); };
			Func<Boolean> isCompleteFunc = () => list.Count > 0;
			using (AsyncJob target = new MockAsyncJob(executeOneAction, isCompleteFunc)) {
				target.Start();

				Boolean actual = target.WaitToStop(TimeSpan.FromSeconds(2));
				Assert.AreEqual(true, actual);

				if (!actual) {
					target.Stop();
				}
			}
		}
		[TestMethod()]
		[Description("WaitToStop(TimeSpan) method when 'timeoutDuration' is 0.")]
		public void AsyncJob_Unit_WaitToStop_TimeoutDurationIsZero() {
			List<Object> list = Enumerable.Range(0, 100).Select(i => new Object()).ToList();
			Action executeOneAction = () => { list.RemoveAt(0); Thread.Sleep(10); };
			Func<Boolean> isCompleteFunc = () => list.Count > 0;
			using (AsyncJob target = new MockAsyncJob(executeOneAction, isCompleteFunc)) {
				target.Start();

				Boolean actual = target.WaitToStop(TimeSpan.FromTicks(0));
				Assert.AreEqual(false, actual);

				target.Stop();
			}
		}
		[TestMethod()]
		[Description("WaitToStop(TimeSpan) method when 'timeoutDuration' is negative.")]
		public void AsyncJob_Unit_WaitToStop_TimeoutDurationIsNegative() {
			List<Object> list = Enumerable.Range(0, 100).Select(i => new Object()).ToList();
			Action executeOneAction = () => { list.RemoveAt(0); Thread.Sleep(10); };
			Func<Boolean> isCompleteFunc = () => list.Count > 0;
			using (AsyncJob target = new MockAsyncJob(executeOneAction, isCompleteFunc)) {
				target.Start();

				Boolean actual = target.WaitToStop(new TimeSpan(-100));
				Assert.AreEqual(false, actual);

				target.Stop();
			}
		}

	// Special Cases
		[TestMethod()]
		[Description("Dispose() method when the job is running.")]
		public void AsyncJob_Unit_Dispose_JobIsRunning() {
			List<Object> list = Enumerable.Range(0, 100).Select(i => new Object()).ToList();
			Action executeOneAction = () => { list.RemoveAt(0); Thread.Sleep(10); };
			Func<Boolean> isCompleteFunc = () => list.Count > 0;
			using (AsyncJob target = new MockAsyncJob(executeOneAction, isCompleteFunc)) {
				target.Start();

				target.Dispose();
				Assert.AreEqual(false, target.IsRunning);
			}
		}
		[TestMethod()]
		[Description("IsComplete property when an exception is thrown.")]
		public void AsyncJob_Unit_IsComplete_Exception() {
			Action executeOneAction = () => { };
			String message = "This is a test.";
			Func<Boolean> isCompleteFunc = () => { throw new ApplicationException(message); };
			using (AsyncJob target = new MockAsyncJob(executeOneAction, isCompleteFunc)) {
				target.Start();
				AsyncJobTests.WaitOneSecond(target);

				Exception error = target.Error;
				Assert.IsNotNull(error);
				Assert.AreEqual(message, error.Message);
			}
		}
	}
}