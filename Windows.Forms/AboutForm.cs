using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

using Image = System.Drawing.Image;
using Res = Vizistata.Windows.Forms.Properties.Resources;

namespace Vizistata.Windows.Forms {
	/// <summary>
	/// Displays information about the application to the user.
	/// </summary>
	partial class AboutForm : Form {
	// Fields
		/// <summary>
		/// The executing assembly.  This field is read-only.
		/// </summary>
		private readonly Assembly _assembly = typeof(AboutForm).Assembly;
		/// <summary>
		/// Displays the product name.
		/// </summary>
		private Label _productNameLabel = new Label();
		/// <summary>
		/// Displays the copyright information.
		/// </summary>
		private Label _copyrightLabel = new Label();
		/// <summary>
		/// Displays the company name.
		/// </summary>
		private Label _companyNameLabel = new Label();
		/// <summary>
		/// Displays the description.
		/// </summary>
		private TextBox _descriptionTextBox = new TextBox();
		/// <summary>
		/// Displays the product's logo or image.
		/// </summary>
		private PictureBox _logoPictureBox = new PictureBox();
		/// <summary>
		/// Displays the version.
		/// </summary>
		private Label _versionLabel = new Label();

	// Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:AboutForm"/> class.
		/// </summary>
		public AboutForm()
			: base() {
			this.InitializeComponent();
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="T:AboutForm"/> class.
		/// </summary>
		/// <param name="productImage">The product's logo or image to use.</param>
		public AboutForm(Image productImage)
			: this() {
			this.ProductImage = productImage;
		}

	// Properties
		/// <summary>
		/// Gets the company name of the assembly.
		/// </summary>
		private String Company {
			get {
				Object[] attributes = this._assembly.GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
				if (attributes.Length == 0) {
					return String.Empty;
				}
				return ((AssemblyCompanyAttribute)attributes[0]).Company;
			}
		}
		/// <summary>
		/// Gets the copyright information for the assembly.
		/// </summary>
		private String Copyright {
			get {
				Object[] attributes = this._assembly.GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
				if (attributes.Length == 0) {
					return String.Empty;
				}
				return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
			}
		}
		/// <summary>
		/// Gets the description of the assembly.
		/// </summary>
		private String Description {
			get {
				Object[] attributes = this._assembly.GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
				if (attributes.Length == 0) {
					return String.Empty;
				}
				return ((AssemblyDescriptionAttribute)attributes[0]).Description;
			}
		}
		/// <summary>
		/// Gets the product name for the assembly.
		/// </summary>
		private String Product {
			get {
				Object[] attributes = this._assembly.GetCustomAttributes(typeof(AssemblyProductAttribute), false);
				if (attributes.Length == 0) {
					return String.Empty;
				}
				return ((AssemblyProductAttribute)attributes[0]).Product;
			}
		}
		/// <summary>
		/// Gets or sets the product's logo or image to use.
		/// </summary>
		public Image ProductImage {
			get { return this._logoPictureBox.Image; }
			set { this._logoPictureBox.Image = value; }
		}
		/// <summary>
		/// Gets the version of the assembly.
		/// </summary>
		private String Version {
			get { return this._assembly.GetName().Version.ToString(); }
		}

	// Methods
		/// <summary>
		/// Initializes the components in this instance.
		/// </summary>
		[SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification = "The controls are disposed of in the Controls collection.")]
		private void InitializeComponent() {
			TableLayoutPanel _mainTableLayoutPanel = new TableLayoutPanel();
			ComponentResourceManager resources = new ComponentResourceManager(typeof(AboutForm));
			Button _okButton = new Button();
			_mainTableLayoutPanel.SuspendLayout();
			((ISupportInitialize)this._logoPictureBox).BeginInit();
			this.SuspendLayout();
			// 
			// _mainTableLayoutPanel
			// 
			_mainTableLayoutPanel.ColumnCount = 2;
			_mainTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33F));
			_mainTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 67F));
			_mainTableLayoutPanel.Controls.Add(this._logoPictureBox, 0, 0);
			_mainTableLayoutPanel.Controls.Add(this._productNameLabel, 1, 0);
			_mainTableLayoutPanel.Controls.Add(this._versionLabel, 1, 1);
			_mainTableLayoutPanel.Controls.Add(this._copyrightLabel, 1, 2);
			_mainTableLayoutPanel.Controls.Add(this._companyNameLabel, 1, 3);
			_mainTableLayoutPanel.Controls.Add(this._descriptionTextBox, 1, 4);
			_mainTableLayoutPanel.Controls.Add(_okButton, 1, 5);
			_mainTableLayoutPanel.Dock = DockStyle.Fill;
			_mainTableLayoutPanel.Location = new Point(9, 9);
			_mainTableLayoutPanel.Name = "_mainTableLayoutPanel";
			_mainTableLayoutPanel.RowCount = 6;
			_mainTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
			_mainTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
			_mainTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
			_mainTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
			_mainTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
			_mainTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
			_mainTableLayoutPanel.Size = new Size(417, 265);
			_mainTableLayoutPanel.TabIndex = 0;
			// 
			// _logoPictureBox
			// 
			this._logoPictureBox.Dock = DockStyle.Fill;
			this._logoPictureBox.Image = (Image)resources.GetObject("_logoPictureBox.Image");
			this._logoPictureBox.Location = new Point(3, 3);
			this._logoPictureBox.Name = "_logoPictureBox";
			_mainTableLayoutPanel.SetRowSpan(this._logoPictureBox, 6);
			this._logoPictureBox.Size = new Size(131, 259);
			this._logoPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
			this._logoPictureBox.TabIndex = 12;
			this._logoPictureBox.TabStop = false;
			// 
			// _productNameLabel
			// 
			this._productNameLabel.Dock = DockStyle.Fill;
			this._productNameLabel.Location = new Point(143, 0);
			this._productNameLabel.Margin = new Padding(6, 0, 3, 0);
			this._productNameLabel.MaximumSize = new Size(0, 17);
			this._productNameLabel.Name = "_productNameLabel";
			this._productNameLabel.Size = new Size(271, 17);
			this._productNameLabel.TabIndex = 19;
			this._productNameLabel.TextAlign = ContentAlignment.MiddleLeft;
			// 
			// _versionLabel
			// 
			this._versionLabel.Dock = DockStyle.Fill;
			this._versionLabel.Location = new Point(143, 26);
			this._versionLabel.Margin = new Padding(6, 0, 3, 0);
			this._versionLabel.MaximumSize = new Size(0, 17);
			this._versionLabel.Name = "_versionLabel";
			this._versionLabel.Size = new Size(271, 17);
			this._versionLabel.TabIndex = 0;
			this._versionLabel.TextAlign = ContentAlignment.MiddleLeft;
			// 
			// _copyrightLabel
			// 
			this._copyrightLabel.Dock = DockStyle.Fill;
			this._copyrightLabel.Location = new Point(143, 52);
			this._copyrightLabel.Margin = new Padding(6, 0, 3, 0);
			this._copyrightLabel.MaximumSize = new Size(0, 17);
			this._copyrightLabel.Name = "_copyrightLabel";
			this._copyrightLabel.Size = new Size(271, 17);
			this._copyrightLabel.TabIndex = 21;
			this._copyrightLabel.TextAlign = ContentAlignment.MiddleLeft;
			// 
			// _companyNameLabel
			// 
			this._companyNameLabel.Dock = DockStyle.Fill;
			this._companyNameLabel.Location = new Point(143, 78);
			this._companyNameLabel.Margin = new Padding(6, 0, 3, 0);
			this._companyNameLabel.MaximumSize = new Size(0, 17);
			this._companyNameLabel.Name = "_companyNameLabel";
			this._companyNameLabel.Size = new Size(271, 17);
			this._companyNameLabel.TabIndex = 22;
			this._companyNameLabel.TextAlign = ContentAlignment.MiddleLeft;
			// 
			// _descriptionTextBox
			// 
			this._descriptionTextBox.Dock = DockStyle.Fill;
			this._descriptionTextBox.Location = new Point(143, 107);
			this._descriptionTextBox.Margin = new Padding(6, 3, 3, 3);
			this._descriptionTextBox.Multiline = true;
			this._descriptionTextBox.Name = "_descriptionTextBox";
			this._descriptionTextBox.ReadOnly = true;
			this._descriptionTextBox.ScrollBars = ScrollBars.Both;
			this._descriptionTextBox.Size = new Size(271, 126);
			this._descriptionTextBox.TabIndex = 23;
			this._descriptionTextBox.TabStop = false;
			// 
			// _okButton
			// 
			_okButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			_okButton.DialogResult = DialogResult.Cancel;
			_okButton.Location = new Point(339, 239);
			_okButton.Name = "_okButton";
			_okButton.Size = new Size(75, 23);
			_okButton.TabIndex = 24;
			_okButton.Text = Res.OKText;
			// 
			// AboutForm
			// 
			this.AcceptButton = _okButton;
			this.AutoScaleDimensions = new SizeF(6F, 13F);
			this.AutoScaleMode = AutoScaleMode.Font;
			this.CancelButton = _okButton;
			this.ClientSize = new Size(435, 283);
			this.Controls.Add(_mainTableLayoutPanel);
			this.FormBorderStyle = FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "AboutForm";
			this.Padding = new Padding(9);
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = FormStartPosition.CenterParent;
			this.Text = Res.AboutText;
			_mainTableLayoutPanel.ResumeLayout(false);
			_mainTableLayoutPanel.PerformLayout();
			((ISupportInitialize)this._logoPictureBox).EndInit();
			this.ResumeLayout(false);
		}
		/// <summary>
		/// Raises the <see cref="E:Form.Load"/> event.
		/// </summary>
		/// <param name="e">Provides information about the event.</param>
		protected override void OnLoad(EventArgs e) {
			base.OnLoad(e);
			this._productNameLabel.Text = this.Product;
			this._versionLabel.Text = Res.VersionFormat.FormatInvariant(this.Version);
			this._copyrightLabel.Text = this.Copyright;
			this._companyNameLabel.Text = this.Company;
			this._descriptionTextBox.Text = this.Description;
		}
	}
}
