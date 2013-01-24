using System;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Windows.Forms;

using Res = Vizistata.Windows.Forms.Properties.Resources;

namespace Vizistata.Windows.Forms {
	/// <summary>
	/// Allows the user to configure a SQL Server connection string.  This class may not be inherited.
	/// </summary>
	public partial class SqlConnectionStringBuilderForm : Form {
	// Fields
		/// <summary>
		/// Allows the user to configure a SQL Server connection string.  This class may not be inherited.
		/// </summary>
		private SqlConnectionStringBuilder _connectionStringBuilder = new SqlConnectionStringBuilder();

	// Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:SqlConnectionStringBuilderForm"/> class.
		/// </summary>
		public SqlConnectionStringBuilderForm()
			: base() {
			this.InitializeComponent();
		}

	// Properties
		/// <summary>
		/// Gets or sets the connection string displayed to the user.
		/// </summary>
		/// <exception cref="System.ArgumentException"><paramref name="value"/> is not a valid connection string.</exception>
		public String ConnectionString {
			get { return this._connectionStringBuilder.ConnectionString; }
			set { this._connectionStringBuilder.ConnectionString = value; }
		}

	// Methods
		/// <summary>
		/// Initializes the components in this instance.
		/// </summary>
		[SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification = "The controls are disposed via the Controls property.")]
		private void InitializeComponent() {
			PropertyGrid _mainPropertyGrid = new PropertyGrid();
			LinkLabel _testLinkLabel = new LinkLabel();
			Button _okButton = new Button();
			Button _cancelButton = new Button();
			this.SuspendLayout();
			// 
			// _mainPropertyGrid
			// 
			_mainPropertyGrid.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			_mainPropertyGrid.Location = new Point(12, 12);
			_mainPropertyGrid.Name = "_mainPropertyGrid";
			_mainPropertyGrid.Size = new Size(685, 496);
			_mainPropertyGrid.TabIndex = 0;
			// 
			// _testLinkLabel
			// 
			_testLinkLabel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
			_testLinkLabel.AutoSize = true;
			_testLinkLabel.LinkBehavior = LinkBehavior.HoverUnderline;
			_testLinkLabel.LinkColor = Color.Blue;
			_testLinkLabel.Location = new Point(12, 519);
			_testLinkLabel.Name = "_testLinkLabel";
			_testLinkLabel.Size = new Size(93, 13);
			_testLinkLabel.TabIndex = 1;
			_testLinkLabel.TabStop = true;
			_testLinkLabel.Text = Res.TestConnectionText;
			_testLinkLabel.VisitedLinkColor = Color.Blue;
			_testLinkLabel.LinkClicked += new LinkLabelLinkClickedEventHandler(this._testLinkLabel_LinkClicked);
			// 
			// _okButton
			// 
			_okButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			_okButton.DialogResult = DialogResult.OK;
			_okButton.Location = new Point(541, 514);
			_okButton.Name = "_okButton";
			_okButton.Size = new Size(75, 23);
			_okButton.TabIndex = 2;
			_okButton.Text = Res.OKText;
			_okButton.UseVisualStyleBackColor = true;
			// 
			// _cancelButton
			// 
			_cancelButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			_cancelButton.DialogResult = DialogResult.Cancel;
			_cancelButton.Location = new Point(622, 514);
			_cancelButton.Name = "_cancelButton";
			_cancelButton.Size = new Size(75, 23);
			_cancelButton.TabIndex = 3;
			_cancelButton.Text = Res.CancelText;
			_cancelButton.UseVisualStyleBackColor = true;
			// 
			// SqlConnectionStringBuilderForm
			// 
			this.AcceptButton = _okButton;
			this.AutoScaleDimensions = new SizeF(6F, 13F);
			this.AutoScaleMode = AutoScaleMode.Font;
			this.CancelButton = _cancelButton;
			this.ClientSize = new Size(709, 549);
			this.Controls.Add(_cancelButton);
			this.Controls.Add(_okButton);
			this.Controls.Add(_testLinkLabel);
			this.Controls.Add(_mainPropertyGrid);
			this.FormBorderStyle = FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "SqlConnectionStringBuilderForm";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = FormStartPosition.CenterParent;
			this.Text = Res.SqlConnectionStringBuilderFormText;
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		/// <summary>
		/// Tests the current connection and displays the result to the user.
		/// </summary>
		[SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "This is desired behavior.")]
		private void TestConnection() {
			try {
				using (SqlConnection connection = new SqlConnection(this.ConnectionString)) {
					connection.Open();
				}
				this.ShowInformation(Res.ConnectionSuccessText, Res.ConnectionSuccessCaption);
			}
			catch (Exception ex) {
				this.ShowError(Res.ConnectionErrorText, Res.ConnectionErrorCaption, ex);
			}
		}

	// Event Handler Methods
		private void _testLinkLabel_LinkClicked(Object sender, LinkLabelLinkClickedEventArgs e) {
			this.TestConnection();
		}
	}
}
