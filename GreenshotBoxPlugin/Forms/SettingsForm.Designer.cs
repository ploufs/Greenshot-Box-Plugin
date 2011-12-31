﻿/*
 * Greenshot - a free and open source screenshot tool
 * Copyright (C) 2007-2011  Thomas Braun, Jens Klingen, Robin Krom
 * 
 * For more information see: http://getgreenshot.org/
 * The Greenshot project is hosted on Sourceforge: http://sourceforge.net/projects/greenshot/
 * 
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 1 of the License, or
 * (at your option) any later version.
 * 
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */
namespace GreenshotBoxPlugin {
	partial class SettingsForm {
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
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
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.combobox_uploadimageformat = new System.Windows.Forms.ComboBox();
            this.label_upload_format = new System.Windows.Forms.Label();
            this.historyButton = new System.Windows.Forms.Button();
            this.label_AfterUpload = new System.Windows.Forms.Label();
            this.checkboxAfterUploadOpenHistory = new System.Windows.Forms.CheckBox();
            this.checkboxAfterUploadLinkToClipBoard = new System.Windows.Forms.CheckBox();
            this.label_DefaultSize = new System.Windows.Forms.Label();
            this.comboBox_DefaultSize = new System.Windows.Forms.ComboBox();
            this.buttonAuthenticate = new System.Windows.Forms.Button();
            this.label_AuthToken = new System.Windows.Forms.Label();
            this.textBoxAuthToken = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.Location = new System.Drawing.Point(267, 148);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 12;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.ButtonOKClick);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.Location = new System.Drawing.Point(348, 148);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 13;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.ButtonCancelClick);
            // 
            // combobox_uploadimageformat
            // 
            this.combobox_uploadimageformat.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.combobox_uploadimageformat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combobox_uploadimageformat.FormattingEnabled = true;
            this.combobox_uploadimageformat.Location = new System.Drawing.Point(113, 45);
            this.combobox_uploadimageformat.Name = "combobox_uploadimageformat";
            this.combobox_uploadimageformat.Size = new System.Drawing.Size(309, 21);
            this.combobox_uploadimageformat.TabIndex = 5;
            // 
            // label_upload_format
            // 
            this.label_upload_format.Location = new System.Drawing.Point(10, 51);
            this.label_upload_format.Name = "label_upload_format";
            this.label_upload_format.Size = new System.Drawing.Size(84, 20);
            this.label_upload_format.TabIndex = 4;
            this.label_upload_format.Text = "Upload format";
            // 
            // historyButton
            // 
            this.historyButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.historyButton.Location = new System.Drawing.Point(13, 148);
            this.historyButton.Name = "historyButton";
            this.historyButton.Size = new System.Drawing.Size(75, 23);
            this.historyButton.TabIndex = 11;
            this.historyButton.Text = "History";
            this.historyButton.UseVisualStyleBackColor = true;
            this.historyButton.Click += new System.EventHandler(this.ButtonHistoryClick);
            // 
            // label_AfterUpload
            // 
            this.label_AfterUpload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label_AfterUpload.Location = new System.Drawing.Point(10, 122);
            this.label_AfterUpload.Name = "label_AfterUpload";
            this.label_AfterUpload.Size = new System.Drawing.Size(84, 21);
            this.label_AfterUpload.TabIndex = 8;
            this.label_AfterUpload.Text = "After upload";
            // 
            // checkboxAfterUploadOpenHistory
            // 
            this.checkboxAfterUploadOpenHistory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkboxAfterUploadOpenHistory.AutoSize = true;
            this.checkboxAfterUploadOpenHistory.Location = new System.Drawing.Point(117, 121);
            this.checkboxAfterUploadOpenHistory.Name = "checkboxAfterUploadOpenHistory";
            this.checkboxAfterUploadOpenHistory.Size = new System.Drawing.Size(85, 17);
            this.checkboxAfterUploadOpenHistory.TabIndex = 9;
            this.checkboxAfterUploadOpenHistory.Text = "Open history";
            this.checkboxAfterUploadOpenHistory.UseVisualStyleBackColor = true;
            // 
            // checkboxAfterUploadLinkToClipBoard
            // 
            this.checkboxAfterUploadLinkToClipBoard.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkboxAfterUploadLinkToClipBoard.AutoSize = true;
            this.checkboxAfterUploadLinkToClipBoard.Location = new System.Drawing.Point(208, 121);
            this.checkboxAfterUploadLinkToClipBoard.Name = "checkboxAfterUploadLinkToClipBoard";
            this.checkboxAfterUploadLinkToClipBoard.Size = new System.Drawing.Size(104, 17);
            this.checkboxAfterUploadLinkToClipBoard.TabIndex = 10;
            this.checkboxAfterUploadLinkToClipBoard.Text = "Link to clipboard";
            this.checkboxAfterUploadLinkToClipBoard.UseVisualStyleBackColor = true;
            // 
            // label_DefaultSize
            // 
            this.label_DefaultSize.Location = new System.Drawing.Point(7, 85);
            this.label_DefaultSize.Name = "label_DefaultSize";
            this.label_DefaultSize.Size = new System.Drawing.Size(84, 21);
            this.label_DefaultSize.TabIndex = 6;
            this.label_DefaultSize.Text = "Default size";
            // 
            // comboBox_DefaultSize
            // 
            this.comboBox_DefaultSize.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBox_DefaultSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_DefaultSize.FormattingEnabled = true;
            this.comboBox_DefaultSize.Location = new System.Drawing.Point(113, 80);
            this.comboBox_DefaultSize.Name = "comboBox_DefaultSize";
            this.comboBox_DefaultSize.Size = new System.Drawing.Size(309, 21);
            this.comboBox_DefaultSize.TabIndex = 7;
            // 
            // buttonAuthenticate
            // 
            this.buttonAuthenticate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAuthenticate.Location = new System.Drawing.Point(347, 12);
            this.buttonAuthenticate.Name = "buttonAuthenticate";
            this.buttonAuthenticate.Size = new System.Drawing.Size(75, 23);
            this.buttonAuthenticate.TabIndex = 16;
            this.buttonAuthenticate.Text = "Authenticate";
            this.buttonAuthenticate.UseVisualStyleBackColor = true;
            this.buttonAuthenticate.Click += new System.EventHandler(this.buttonAuthenticate_Click);
            // 
            // label_AuthToken
            // 
            this.label_AuthToken.Location = new System.Drawing.Point(7, 15);
            this.label_AuthToken.Name = "label_AuthToken";
            this.label_AuthToken.Size = new System.Drawing.Size(84, 20);
            this.label_AuthToken.TabIndex = 14;
            this.label_AuthToken.Text = "Auth Token";
            // 
            // textBoxAuthToken
            // 
            this.textBoxAuthToken.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxAuthToken.Location = new System.Drawing.Point(112, 13);
            this.textBoxAuthToken.Name = "textBoxAuthToken";
            this.textBoxAuthToken.ReadOnly = true;
            this.textBoxAuthToken.Size = new System.Drawing.Size(229, 20);
            this.textBoxAuthToken.TabIndex = 15;
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(432, 180);
            this.Controls.Add(this.buttonAuthenticate);
            this.Controls.Add(this.label_AuthToken);
            this.Controls.Add(this.textBoxAuthToken);
            this.Controls.Add(this.label_DefaultSize);
            this.Controls.Add(this.comboBox_DefaultSize);
            this.Controls.Add(this.checkboxAfterUploadLinkToClipBoard);
            this.Controls.Add(this.checkboxAfterUploadOpenHistory);
            this.Controls.Add(this.label_AfterUpload);
            this.Controls.Add(this.historyButton);
            this.Controls.Add(this.label_upload_format);
            this.Controls.Add(this.combobox_uploadimageformat);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsForm";
            this.Text = "Box settings";
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		private System.Windows.Forms.Button historyButton;
		private System.Windows.Forms.ComboBox combobox_uploadimageformat;
        private System.Windows.Forms.Label label_upload_format;
		private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Label label_AfterUpload;
        private System.Windows.Forms.CheckBox checkboxAfterUploadOpenHistory;
        private System.Windows.Forms.CheckBox checkboxAfterUploadLinkToClipBoard;
        private System.Windows.Forms.Label label_DefaultSize;
        private System.Windows.Forms.ComboBox comboBox_DefaultSize;
        private System.Windows.Forms.Button buttonAuthenticate;
        private System.Windows.Forms.Label label_AuthToken;
        private System.Windows.Forms.TextBox textBoxAuthToken;
	}
}
