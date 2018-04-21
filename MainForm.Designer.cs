/*
 * User: good
 * Date: 2/21/2018
 * Time: 7:57 PM
 */
namespace VNDSConverter
{
	partial class MainForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.Button selectFolderButton;
		private System.Windows.Forms.Label currentFolderLabel;
		private System.Windows.Forms.Button goButton;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		protected void InitializeComponent()
		{
			this.selectFolderButton = new System.Windows.Forms.Button();
			this.currentFolderLabel = new System.Windows.Forms.Label();
			this.goButton = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// selectFolderButton
			// 
			this.selectFolderButton.Location = new System.Drawing.Point(13, 13);
			this.selectFolderButton.Name = "selectFolderButton";
			this.selectFolderButton.Size = new System.Drawing.Size(91, 23);
			this.selectFolderButton.TabIndex = 0;
			this.selectFolderButton.Text = "Select Folder";
			this.selectFolderButton.UseVisualStyleBackColor = true;
			this.selectFolderButton.Click += new System.EventHandler(this.selectButtonClick);
			// 
			// currentFolderLabel
			// 
			this.currentFolderLabel.Location = new System.Drawing.Point(13, 43);
			this.currentFolderLabel.Name = "currentFolderLabel";
			this.currentFolderLabel.Size = new System.Drawing.Size(148, 17);
			this.currentFolderLabel.TabIndex = 1;
			this.currentFolderLabel.Text = "Please select a folder.";
			// 
			// goButton
			// 
			this.goButton.Location = new System.Drawing.Point(13, 63);
			this.goButton.Name = "goButton";
			this.goButton.Size = new System.Drawing.Size(75, 23);
			this.goButton.TabIndex = 2;
			this.goButton.Text = "Go";
			this.goButton.UseVisualStyleBackColor = true;
			this.goButton.Click += new System.EventHandler(this.GoButtonClick);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(164, 106);
			this.Controls.Add(this.goButton);
			this.Controls.Add(this.currentFolderLabel);
			this.Controls.Add(this.selectFolderButton);
			this.Name = "MainForm";
			this.Text = "VNDSConverterGUI";
			this.ResumeLayout(false);

		}
	}
}
