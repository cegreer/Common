namespace Vizistata.Windows.Forms {
	partial class DualListBox {
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DualListBox));
			this._mainTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
			this._notChosenListBox = new System.Windows.Forms.ListBox();
			this._chosenListBox = new System.Windows.Forms.ListBox();
			this._buttonPanel = new System.Windows.Forms.Panel();
			this._chooseAllButton = new System.Windows.Forms.Button();
			this._removeAllChosenButton = new System.Windows.Forms.Button();
			this._chooseSelectedButton = new System.Windows.Forms.Button();
			this._removeSelectedChosenButton = new System.Windows.Forms.Button();
			this._mainToolTip = new System.Windows.Forms.ToolTip(this.components);
			this._mainTableLayoutPanel.SuspendLayout();
			this._buttonPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// _mainTableLayoutPanel
			// 
			resources.ApplyResources(this._mainTableLayoutPanel, "_mainTableLayoutPanel");
			this._mainTableLayoutPanel.Controls.Add(this._notChosenListBox, 0, 0);
			this._mainTableLayoutPanel.Controls.Add(this._chosenListBox, 2, 0);
			this._mainTableLayoutPanel.Controls.Add(this._buttonPanel, 1, 1);
			this._mainTableLayoutPanel.Name = "_mainTableLayoutPanel";
			// 
			// _notChosenListBox
			// 
			resources.ApplyResources(this._notChosenListBox, "_notChosenListBox");
			this._notChosenListBox.FormattingEnabled = true;
			this._notChosenListBox.Name = "_notChosenListBox";
			this._mainTableLayoutPanel.SetRowSpan(this._notChosenListBox, 3);
			this._notChosenListBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
			this._notChosenListBox.SelectedIndexChanged += new System.EventHandler(this._notChosenListBox_SelectedIndexChanged);
			// 
			// _chosenListBox
			// 
			resources.ApplyResources(this._chosenListBox, "_chosenListBox");
			this._chosenListBox.FormattingEnabled = true;
			this._chosenListBox.Name = "_chosenListBox";
			this._mainTableLayoutPanel.SetRowSpan(this._chosenListBox, 3);
			this._chosenListBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
			this._chosenListBox.SelectedIndexChanged += new System.EventHandler(this._chosenListBox_SelectedIndexChanged);
			// 
			// _buttonPanel
			// 
			this._buttonPanel.Controls.Add(this._chooseAllButton);
			this._buttonPanel.Controls.Add(this._removeAllChosenButton);
			this._buttonPanel.Controls.Add(this._chooseSelectedButton);
			this._buttonPanel.Controls.Add(this._removeSelectedChosenButton);
			resources.ApplyResources(this._buttonPanel, "_buttonPanel");
			this._buttonPanel.Name = "_buttonPanel";
			// 
			// _chooseAllButton
			// 
			resources.ApplyResources(this._chooseAllButton, "_chooseAllButton");
			this._chooseAllButton.Name = "_chooseAllButton";
			this._chooseAllButton.UseVisualStyleBackColor = true;
			this._chooseAllButton.Click += new System.EventHandler(this._chooseAllButton_Click);
			// 
			// _removeAllChosenButton
			// 
			resources.ApplyResources(this._removeAllChosenButton, "_removeAllChosenButton");
			this._removeAllChosenButton.Name = "_removeAllChosenButton";
			this._removeAllChosenButton.UseVisualStyleBackColor = true;
			this._removeAllChosenButton.Click += new System.EventHandler(this._removeAllChosenButton_Click);
			// 
			// _chooseSelectedButton
			// 
			resources.ApplyResources(this._chooseSelectedButton, "_chooseSelectedButton");
			this._chooseSelectedButton.Name = "_chooseSelectedButton";
			this._chooseSelectedButton.UseVisualStyleBackColor = true;
			this._chooseSelectedButton.Click += new System.EventHandler(this._chooseSelectedButton_Click);
			// 
			// _removeSelectedChosenButton
			// 
			resources.ApplyResources(this._removeSelectedChosenButton, "_removeSelectedChosenButton");
			this._removeSelectedChosenButton.Name = "_removeSelectedChosenButton";
			this._removeSelectedChosenButton.UseVisualStyleBackColor = true;
			this._removeSelectedChosenButton.Click += new System.EventHandler(this._removeSelectedChosenButton_Click);
			// 
			// _mainToolTip
			// 
			this._mainToolTip.AutomaticDelay = 250;
			this._mainToolTip.AutoPopDelay = 5000;
			this._mainToolTip.BackColor = System.Drawing.Color.LemonChiffon;
			this._mainToolTip.InitialDelay = 250;
			this._mainToolTip.ReshowDelay = 50;
			this._mainToolTip.ToolTipTitle = "CSS Custom Control Help";
			// 
			// DualListBox
			// 
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this._mainTableLayoutPanel);
			this.MinimumSize = new System.Drawing.Size(250, 150);
			this.Name = "DualListBox";
			this._mainTableLayoutPanel.ResumeLayout(false);
			this._buttonPanel.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ListBox _notChosenListBox;
		private System.Windows.Forms.ListBox _chosenListBox;
		private System.Windows.Forms.Button _chooseAllButton;
		private System.Windows.Forms.Button _chooseSelectedButton;
		private System.Windows.Forms.Button _removeSelectedChosenButton;
		private System.Windows.Forms.Button _removeAllChosenButton;
		private System.Windows.Forms.TableLayoutPanel _mainTableLayoutPanel;
		private System.Windows.Forms.Panel _buttonPanel;
		private System.Windows.Forms.ToolTip _mainToolTip;
	}
}
