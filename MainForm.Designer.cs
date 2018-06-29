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
		private System.Windows.Forms.Button goButton;
		private System.Windows.Forms.Label ffmpegstatuslabel;
		
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
			this.goButton = new System.Windows.Forms.Button();
			this.ffmpegstatuslabel = new System.Windows.Forms.Label();
			this.platformListBox = new System.Windows.Forms.ListBox();
			this.label1 = new System.Windows.Forms.Label();
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
			// goButton
			// 
			this.goButton.Location = new System.Drawing.Point(13, 42);
			this.goButton.Name = "goButton";
			this.goButton.Size = new System.Drawing.Size(75, 23);
			this.goButton.TabIndex = 2;
			this.goButton.Text = "Go";
			this.goButton.UseVisualStyleBackColor = true;
			this.goButton.Click += new System.EventHandler(this.GoButtonClick);
			// 
			// ffmpegstatuslabel
			// 
			this.ffmpegstatuslabel.Location = new System.Drawing.Point(13, 68);
			this.ffmpegstatuslabel.Name = "ffmpegstatuslabel";
			this.ffmpegstatuslabel.Size = new System.Drawing.Size(139, 38);
			this.ffmpegstatuslabel.TabIndex = 3;
			this.ffmpegstatuslabel.Text = "label1";
			// 
			// platformListBox
			// 
			this.platformListBox.FormattingEnabled = true;
			this.platformListBox.Location = new System.Drawing.Point(110, 22);
			this.platformListBox.Name = "platformListBox";
			this.platformListBox.Size = new System.Drawing.Size(86, 43);
			this.platformListBox.TabIndex = 4;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(110, 5);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(98, 14);
			this.label1.TabIndex = 5;
			this.label1.Text = "Choose a platform";
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(212, 118);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.platformListBox);
			this.Controls.Add(this.ffmpegstatuslabel);
			this.Controls.Add(this.goButton);
			this.Controls.Add(this.selectFolderButton);
			this.Name = "MainForm";
			this.Text = "VNDSConverterGUI";
			this.ResumeLayout(false);
		}
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ListBox platformListBox;
	}
}
