using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Windows.Forms;

using Thread = System.Threading.Thread;
using Res = Vizistata.Windows.Forms.Properties.Resources;

namespace Vizistata.Windows.Forms {
	/// <summary>
	/// Displays a modal dialog box that informs the user a long-running process is occurring.  This class may not be inherited.
	/// </summary>
	public sealed class LoadingForm : Form {
	// Fields
		/// <summary>
		/// The action to execute.  This field is read-only.
		/// </summary>
		private readonly Action _action;
		/// <summary>
		/// The collection of components on the form.  This field is read-only.
		/// </summary>
		private readonly IContainer _components = new Container();
		/// <summary>
		/// The error that occurred during the action, or a null reference if the action is not complete or no error occurred.
		/// </summary>
		private Exception _error;
		/// <summary>
		/// <c>true</c> if the action has finished; otherwise, <c>false</c>.
		/// </summary>
		private Boolean _isFinished;
		/// <summary>
		/// The thread on which to invoke the action.
		/// </summary>
		private Thread _thread;
		/// <summary>
		/// A progress bar to display to the user while the action executes.  This field is read-only.
		/// </summary>
		private readonly ProgressBar _mainProgressBar = new ProgressBar();
		/// <summary>
		/// Polls the thread to determine when the action finishes.
		/// </summary>
		private Timer _mainTimer;
		/// <summary>
		/// Displays text to the user.  This field is read-only.
		/// </summary>
		private readonly Label _mesageLabel = new Label();

	// Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:LoadingForm"/> class.
		/// </summary>
		private LoadingForm()
			: base() {
			this.InitializeComponent();
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="T:LoadingForm"/> class.
		/// </summary>
		/// <param name="displayText">The text to display.</param>
		/// <param name="action">The action to execute.</param>
		/// <exception cref="System.ArgumentNullException"><paramref name="displayText"/> is a null reference.
		/// -or- <paramref name="action"/> is a null reference.</exception>
		public LoadingForm(String displayText, Action action)
			: this() {
			if (displayText == null) {
				throw new ArgumentNullException("displayText");
			}
			if (action == null) {
				throw new ArgumentNullException("action");
			}

			this._mesageLabel.Text = displayText;
			this._action = action;
		}

	// Properties
		/// <summary>
		/// Gets or sets the text to display to the user.
		/// </summary>
		/// <exception cref="System.ArgumentNullException"><paramref name="value"/> is a null reference.</exception>
		public String DisplayText {
			get { return this._mesageLabel.Text; }
			set {
				if (value == null) {
					throw new ArgumentNullException("value");
				}
				this._mesageLabel.Text = value;
			}
		}
		/// <summary>
		/// Gets the error that occurred from the operation, or a null reference.
		/// </summary>
		public Exception Error {
			get { return this._error; }
		}

	// Event Handler Methods
		/// <summary>
		/// Monitors the <see cref="E:Timer.Tick"/> event and closes the form when the action completes.
		/// </summary>
		/// <param name="sender"><see cref="F:_mainTimer"/>.</param>
		/// <param name="e">Provides information about the event.</param>
		private void MainTimer_Tick(Object sender, EventArgs e) {
			Debug.Assert(this._mainTimer != null);
			Debug.Assert(this._thread != null);
			if (this._isFinished) {
				this._mainTimer.Stop();
				this._thread.Join();
				this._thread = null;
				this.DialogResult = this._error == null ? DialogResult.OK : DialogResult.Cancel;
				this.Close();
			}
		}

	// Methods
		/// <summary>
		/// Disposes of any resources held by this instance.
		/// </summary>
		/// <param name="disposing"><c>true</c> if this method is called from the <see cref="M:IDisposable.Dispose"/> method; otherwise, <c>false</c>.</param>
		protected override void Dispose(Boolean disposing) {
			Debug.Assert(this._components != null);
			if (disposing) {
				this._components.Dispose();
			}
			base.Dispose(disposing);
		}
		/// <summary>
		/// Initializes the controls and layout for this instance.
		/// </summary>
		private void InitializeComponent() {
			this._mainTimer = new Timer(this._components);
			this.SuspendLayout();

			// _mesageLabel
			this._mesageLabel.Location = new Point(13, 13);
			this._mesageLabel.Name = "_mesageLabel";
			this._mesageLabel.Size = new Size(267, 110);
			this._mesageLabel.TabIndex = 0;

			// _mainProgressBar
			this._mainProgressBar.Location = new Point(12, 126);
			this._mainProgressBar.Name = "_mainProgressBar";
			this._mainProgressBar.Size = new Size(268, 23);
			this._mainProgressBar.Style = ProgressBarStyle.Marquee;
			this._mainProgressBar.TabIndex = 1;

			// _mainTimer
			this._mainTimer.Tick += this.MainTimer_Tick;

			// LoadingForm
			this.AutoScaleDimensions = new SizeF(6F, 13F);
			this.AutoScaleMode = AutoScaleMode.Font;
			this.ClientSize = new Size(292, 154);
			this.ControlBox = false;
			this.Controls.Add(this._mainProgressBar);
			this.Controls.Add(this._mesageLabel);
			this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "LoadingForm";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = FormStartPosition.CenterParent;
			this.Text = Res.LoadingCaption;
			this.ResumeLayout(false);
		}
		/// <summary>
		/// Acts as the main loop for the thread.
		/// </summary>
		[SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "The exception is returned later.")]
		private void MainLoop() {
			Debug.Assert(this._action != null);
			Debug.Assert(!this._isFinished);
			try {
				this._action();
			}
			catch (Exception ex) {
				this._error = ex;
			}
			this._isFinished = true;
		}
		/// <summary>
		/// Raises the <see cref="E:Form.Shown"/> event.
		/// </summary>
		/// <param name="e">Provides information about the event.</param>
		protected override void OnShown(EventArgs e) {
			Debug.Assert(this._thread == null);
			base.OnShown(e);

			this._thread = new Thread(this.MainLoop);
			this._thread.Start();
			this._mainTimer.Start();
		}
	}
}
