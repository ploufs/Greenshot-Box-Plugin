/*
 * Greenshot - a free and open source screenshot tool
 * Copyright (C) 2011  Francis Noel
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
using System;
using System.Drawing;
using System.Windows.Forms;

using GreenshotBoxPlugin.Forms;
using GreenshotPlugin.Core;

namespace GreenshotBoxPlugin {
	/// <summary>
	/// Description of PasswordRequestForm.
	/// </summary>
	public partial class SettingsForm : Form {
		private ILanguage lang = Language.GetInstance();
		string boxTicket = string.Empty;

		public SettingsForm(BoxConfiguration config) {
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			InitializeTexts();
			
			combobox_uploadimageformat.Items.Clear();
			foreach(OutputFormat format in Enum.GetValues(typeof(OutputFormat))) {
				combobox_uploadimageformat.Items.Add(format.ToString());
			}

			comboBox_DefaultSize.Items.Clear();
			foreach (PictureDisplaySize displaySize in Enum.GetValues(typeof(PictureDisplaySize)))
			{
				comboBox_DefaultSize.Items.Add(displaySize.ToString());
			}

			BoxUtils.LoadHistory();

			if (config.runtimeBoxHistory.Count > 0) {
				historyButton.Enabled = true;
			} else {
				historyButton.Enabled = false;
			}
		}
				
		private void InitializeTexts() {
			this.buttonOK.Text = lang.GetString(LangKey.OK);
			
			this.buttonCancel.Text = lang.GetString(LangKey.CANCEL);
			this.Text = lang.GetString(LangKey.settings_title);
			this.label_upload_format.Text = lang.GetString(LangKey.label_upload_format);
			this.label_AfterUpload.Text = lang.GetString(LangKey.label_AfterUpload);
			this.label_DefaultSize.Text = lang.GetString(LangKey.label_DefaultSize);
			this.checkboxAfterUploadOpenHistory.Text = lang.GetString(LangKey.label_AfterUploadOpenHistory);
			this.checkboxAfterUploadLinkToClipBoard.Text = lang.GetString(LangKey.label_AfterUploadLinkToClipBoard);
		}

		public bool AfterUploadOpenHistory {
			get { return checkboxAfterUploadOpenHistory.Checked; }
			set { checkboxAfterUploadOpenHistory.Checked = value; }
		}
		public bool AfterUploadLinkToClipBoard
		{
			get { return checkboxAfterUploadLinkToClipBoard.Checked; }
			set { checkboxAfterUploadLinkToClipBoard.Checked = value; }
		}

		public string AuthToken
		{
			get { return textBoxAuthToken.Text; }
			set { textBoxAuthToken.Text = value; }
		}

		public string DefaultSize
		{
			get { return comboBox_DefaultSize.Text; }
			set { comboBox_DefaultSize.Text = value; }
		}

		public string UploadFormat
		{
			get { return combobox_uploadimageformat.Text; }
			set { combobox_uploadimageformat.Text = value; }
		}

		void ButtonOKClick(object sender, EventArgs e)
		{
			if (!string.IsNullOrEmpty(boxTicket))
			{
				using (BoxNet.boxnetPortClient client = BoxUtils.SoapClient())
				{
					string auth_token = string.Empty;

					BoxNet.SOAPUser user = new BoxNet.SOAPUser();
					client.get_auth_token(out auth_token, out user, BoxUtils.Box_API_KEY, boxTicket);

					textBoxAuthToken.Text = auth_token;
					this.DialogResult = DialogResult.OK;
				}
			   
			}
			else
			{
				this.DialogResult = DialogResult.OK;
			}
		}
		
		void ButtonCancelClick(object sender, System.EventArgs e) {
			this.DialogResult = DialogResult.Cancel;
		}
		
		void ButtonHistoryClick(object sender, EventArgs e) {
			BoxHistory.ShowHistory();
		}

		private void buttonAuthenticate_Click(object sender, EventArgs e)
		{

			using (BoxNet.boxnetPortClient client = BoxUtils.SoapClient())
			{
				if (string.IsNullOrEmpty(boxTicket))
				{
					string result = client.get_ticket(out boxTicket, BoxUtils.Box_API_KEY);
					System.Diagnostics.Process.Start(string.Format("https://www.box.net/api/1.0/auth/{0}", boxTicket));
				}
				else
				{
					string auth_token = string.Empty;

					BoxNet.SOAPUser user = new BoxNet.SOAPUser();
					string result = client.get_auth_token(out auth_token, out user, BoxUtils.Box_API_KEY, boxTicket);

					boxTicket = string.Empty;
					textBoxAuthToken.Text = auth_token;
				}
			}
		}

	}
}
