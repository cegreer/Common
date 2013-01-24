using System;

namespace Vizistata.Mocks {
	/// <summary>
	/// A mock implementation of the <see cref="T:AsyncJob"/> class.  This class may not be inherited.
	/// </summary>
	internal sealed class MockAsyncJob : AsyncJob {
	// Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:MockJob"/> class.
		/// </summary>
		public MockAsyncJob() : base() { }
		/// <summary>
		/// Initializes a new instance of the <see cref="T:MockJob"/> class.
		/// </summary>
		/// <param name="executeOneAction">The action to execute within the <see cref="M:Job.ExecuteOne"/> method.</param>
		public MockAsyncJob(Action executeOneAction) : this(executeOneAction, null) { }
		/// <summary>
		/// Initializes a new instance of the <see cref="T:MockJob"/> class.
		/// </summary>
		/// <param name="isCompleteFunc">The function that returns the value for the <see cref="P:Job.IsComplete"/> method.</param>
		public MockAsyncJob(Func<Boolean> isCompleteFunc) : this(null, isCompleteFunc) { }
		/// <summary>
		/// Initializes a new instance of the <see cref="T:MockJob"/> class.
		/// </summary>
		/// <param name="executeOneAction">The action to execute within the <see cref="M:Job.ExecuteOne"/> method.</param>
		/// <param name="isCompleteFunc">The function that returns the value for the <see cref="P:Job.IsComplete"/> method.</param>
		public MockAsyncJob(Action executeOneAction, Func<Boolean> isCompleteFunc)
			: base() {
			this.ExecuteOneAction = executeOneAction;
			this.IsCompleteFunc = isCompleteFunc;
		}

	// Properties
		/// <summary>
		/// Gets or sets the action to use within the <see cref="M:Job.ExecuteOne"/> method.
		/// </summary>
		public Action ExecuteOneAction {
			get;
			set;
		}
		/// <summary>
		/// Gets a value indicating if this instance is complete.
		/// </summary>
		protected override Boolean IsComplete {
			get {
				Func<Boolean> isCompleteFunc = this.IsCompleteFunc;
				return isCompleteFunc == null || isCompleteFunc();
			}
		}
		/// <summary>
		/// Gets or sets a function that returns the value for the <see cref="P:Job.IsComplete"/> property.
		/// </summary>
		public Func<Boolean> IsCompleteFunc {
			get;
			set;
		}

	// Methods
		/// <summary>
		/// Executes a single instance.
		/// </summary>
		protected override void ExecuteOne() {
			Action executeOneAction = this.ExecuteOneAction;
			if (this.ExecuteOneAction != null) {
				executeOneAction();
			}
		}
	}
}
