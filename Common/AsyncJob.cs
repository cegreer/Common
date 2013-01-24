using System;

using Thread = System.Threading.Thread;

namespace Vizistata {
	/// <summary>
	/// Acts as the base class for operations that execute asynchronously on a separate thread.
	/// </summary>
	public abstract class AsyncJob : DisposableBase {
	// Fields
		/// <summary>
		/// The error that occurred during execution, or a null reference.
		/// </summary>
		private Exception _error;
		/// <summary>
		/// <c>true</c> if this instance should be running; otherwise, <c>false</c>.
		/// </summary>
		/// <remarks>This is different from the <see cref="P:IsRunning"/> property in that this field indicates whether the main loop should continue.</remarks>
		private Boolean _shouldBeRunning;
		/// <summary>
		/// The thread on which the job runs.
		/// </summary>
		private Thread _thread;
		/// <summary>
		/// Controls access to the <see cref="F:_thread"/> field.  This field is read-only.
		/// </summary>
		private readonly Object _threadDoor = new Object();

	// Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:AsyncJob"/> class.
		/// </summary>
		protected AsyncJob() : base() { }

	// Properties
		/// <summary>
		/// Gets the error that occurred during execution, or a null reference.
		/// </summary>
		public Exception Error {
			get { return this._error; }
		}
		/// <summary>
		/// Gets a value indicating if this instance is complete or if it has additional operations to perform.
		/// </summary>
		protected abstract Boolean IsComplete { get; }
		/// <summary>
		/// Gets a value indicating if this instance is running.
		/// </summary>
		public Boolean IsRunning {
			get { return this._shouldBeRunning && this._thread != null; }
		}

	// Methods
		/// <summary>
		/// Executes a single operation in the job.
		/// </summary>
		protected abstract void ExecuteOne();
		/// <summary>
		/// Acts as the main loop for the job's thread.
		/// </summary>
		private void MainLoop() {
			while (this._shouldBeRunning) {
				try {
					if (this.IsComplete) {
						this._shouldBeRunning = false;
						break;
					}
					this.ExecuteOne();
				}
				catch (Exception ex) {
					if (!ex.CanBeHandledSafely()) {
						throw;
					}
					this._error = ex;
					this._shouldBeRunning = false;
				}
			}
		}
		/// <summary>
		/// Releases any managed resources held by this instance.
		/// </summary>
		protected override void ReleaseManagedResources() {
			this.Stop();
		}
		/// <summary>
		/// Starts this instance.  Subsequent calls will result in no side effects.
		/// </summary>
		public void Start() {
			lock (this._threadDoor) {
				if (!this._shouldBeRunning) {
					this._shouldBeRunning = true;
				}

				if (this._thread == null) {
					Thread thread = new Thread(this.MainLoop);
					thread.Start();
					this._thread = thread;
				}
			}
		}
		/// <summary>
		/// Stops this instance.  Subsequent calls will result in no side effects.
		/// </summary>
		public void Stop() {
			lock (this._threadDoor) {
				if (this._shouldBeRunning) {
					this._shouldBeRunning = false;
				}

				if (this._thread != null) {
					if (!this._thread.Join(TimeSpan.FromSeconds(1))) {
						this._thread.Abort();
					}
					this._thread = null;
				}
			}
		}
		/// <summary>
		/// Blocks the current executing thread to wait for this instance to stop on its own.
		/// </summary>
		/// <param name="timeoutDuration">The amount of time to wait.  The duration of the time span will be used.</param>
		/// <returns><c>true</c> if the instance stopped on its own; otherwise, <c>false</c>.</returns>
		public Boolean WaitToStop(TimeSpan timeoutDuration) {
			DateTime anticipatedFinish = DateTime.Now.Add(timeoutDuration.Duration());
			while (this.IsRunning) {
				Thread.Sleep(10);
				if (DateTime.Now >= anticipatedFinish) {
					return false;
				}
			}
			return true;
		}
	}
}
