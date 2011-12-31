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
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using GreenshotPlugin.Controls;
using GreenshotPlugin.Core;

namespace GreenshotBoxPlugin.Forms {
	/// <summary>
	/// Description of ImgurHistory.
	/// </summary>
	public partial class BoxHistory : Form {
		private static readonly log4net.ILog LOG = log4net.LogManager.GetLogger(typeof(BoxHistory));
		private ListViewColumnSorter columnSorter;
		private static BoxConfiguration config = IniConfig.GetIniSection<BoxConfiguration>();
		private ILanguage lang = Language.GetInstance();
		private static BoxHistory instance;
		
		public static void ShowHistory() {
			if (instance == null) {
				instance = new BoxHistory();
			}
			instance.Show();
			instance.redraw();
		}
		
		private BoxHistory() {
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();

			// Init sorting
			columnSorter = new ListViewColumnSorter();
			this.listview_Box_uploads.ListViewItemSorter = columnSorter;
			columnSorter.SortColumn = 2; //sort by date
			columnSorter.Order = SortOrder.Descending;
			redraw();
			if (listview_Box_uploads.Items.Count > 0) {
				listview_Box_uploads.Items[0].Selected = true;
			}
		}

		private void redraw() {
			listview_Box_uploads.BeginUpdate();
			listview_Box_uploads.Items.Clear();
			listview_Box_uploads.Columns.Clear();
			string[] columns = { "Id", "Title", "Date"};
			foreach (string column in columns) {
				listview_Box_uploads.Columns.Add(column);
			}
			foreach (BoxInfo imgurInfo in config.runtimeBoxHistory.Values) {
				ListViewItem item = new ListViewItem(imgurInfo.ID.ToString());
				item.Tag = imgurInfo;
				item.SubItems.Add(imgurInfo.Title);
				item.SubItems.Add(imgurInfo.Timestamp.ToString());
				listview_Box_uploads.Items.Add(item);
			}
			for (int i = 0; i < columns.Length; i++) {
				listview_Box_uploads.AutoResizeColumn(i, ColumnHeaderAutoResizeStyle.ColumnContent);
			}
	
			listview_Box_uploads.EndUpdate();
			listview_Box_uploads.Refresh();
			deleteButton.Enabled = false;
			openButton.Enabled = false;
			clipboardButton.Enabled = false;
		}

		private void Listview_Box_uploadsSelectedIndexChanged(object sender, EventArgs e) {
			pictureBox_Box.Image = null;
			if (listview_Box_uploads.SelectedItems != null && listview_Box_uploads.SelectedItems.Count > 0) {
				deleteButton.Enabled = true;
				openButton.Enabled = true;
				clipboardButton.Enabled = true;
				if (listview_Box_uploads.SelectedItems.Count == 1) {
					BoxInfo imgurInfo = (BoxInfo)listview_Box_uploads.SelectedItems[0].Tag;
					pictureBox_Box.Image = imgurInfo.Image;
				}
			} else {
				pictureBox_Box.Image = null;
				deleteButton.Enabled = false;
				openButton.Enabled = false;
				clipboardButton.Enabled = false;
			}
		}

		private void DeleteButtonClick(object sender, EventArgs e) {
			this.deletePicture();
		}

		private void ClipboardButtonClick(object sender, EventArgs e) {
			this.clipboardUrl(config.PictureDisplaySize);
		}

		private void FinishedButtonClick(object sender, EventArgs e) {
			this.Close();
		}

		private void OpenButtonClick(object sender, EventArgs e) {
			this.openPicture(config.PictureDisplaySize);
		}
		
		private void listview_imgur_uploads_ColumnClick(object sender, ColumnClickEventArgs e) {
			// Determine if clicked column is already the column that is being sorted.
			if (e.Column == columnSorter.SortColumn) {
				// Reverse the current sort direction for this column.
				if (columnSorter.Order == SortOrder.Ascending) {
					columnSorter.Order = SortOrder.Descending;
				} else {
					columnSorter.Order = SortOrder.Ascending;
				}
			} else {
				// Set the column number that is to be sorted; default to ascending.
				columnSorter.SortColumn = e.Column;
				columnSorter.Order = SortOrder.Ascending;
			}

			// Perform the sort with these new sort options.
			this.listview_Box_uploads.Sort();
		}

		
		void ImgurHistoryFormClosing(object sender, FormClosingEventArgs e)
		{
			instance = null;
		}

		private void ToolStripMenuItem_Delete_Click(object sender, EventArgs e)
		{
			this.deletePicture();
		}

		private void ToolStripMenuItem_Open_webUrl_Click(object sender, EventArgs e)
		{
			this.openPicture(PictureDisplaySize.WebUrl);
		}

		private void ToolStripMenuItem_Open_SquareThumbnailUrl_Click(object sender, EventArgs e)
		{
			this.openPicture(PictureDisplaySize.SquareThumbnailUrl);
		}

		private void ToolStripMenuItem_Open_OriginalUrl_Click(object sender, EventArgs e)
		{
			this.openPicture(PictureDisplaySize.OriginalUrl);
		}

		private void ToolStripMenuItem_Copy_WebUrl_Click(object sender, EventArgs e)
		{
			this.clipboardUrl(PictureDisplaySize.WebUrl);
		}

		private void ToolStripMenuItem_Copy_SquareThumbnailUrl_Click(object sender, EventArgs e)
		{
			this.clipboardUrl(PictureDisplaySize.SquareThumbnailUrl);
		}

		private void ToolStripMenuItem_Copy_OriginalUrl_Click(object sender, EventArgs e)
		{
			this.clipboardUrl(PictureDisplaySize.OriginalUrl);
		}

			

		private void deletePicture()
		{
			if (listview_Box_uploads.SelectedItems != null && listview_Box_uploads.SelectedItems.Count > 0)
			{
				for (int i = 0; i < listview_Box_uploads.SelectedItems.Count; i++)
				{
					BoxInfo imgurInfo = (BoxInfo)listview_Box_uploads.SelectedItems[i].Tag;
					DialogResult result = MessageBox.Show(lang.GetFormattedString(LangKey.delete_question, imgurInfo.Title), lang.GetFormattedString(LangKey.delete_title, imgurInfo.ID), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
					if (result == DialogResult.Yes)
					{
						BackgroundForm backgroundForm = BackgroundForm.ShowAndWait(BoxPlugin.Attributes.Name, lang.GetString(LangKey.communication_wait));
						try
						{
							BoxUtils.DeleteBoxImage(imgurInfo);
						}
						catch (Exception ex)
						{
							LOG.Warn("Problem communicating with Box: ", ex);
						}
						finally
						{
							backgroundForm.CloseDialog();
						}

						imgurInfo.Dispose();
					}
				}
			}
			redraw();
		}

		private void openPicture(PictureDisplaySize pictureDisplaySize)
		{
			if (listview_Box_uploads.SelectedItems != null && listview_Box_uploads.SelectedItems.Count > 0)
			{
				for (int i = 0; i < listview_Box_uploads.SelectedItems.Count; i++)
				{
					BoxInfo imgurInfo = (BoxInfo)listview_Box_uploads.SelectedItems[i].Tag;

					System.Diagnostics.Process.Start(imgurInfo.LinkUrl(pictureDisplaySize));
				}
			}
		}

		private void clipboardUrl(PictureDisplaySize pictureDisplaySize)
		{
			StringBuilder links = new StringBuilder();
			if (listview_Box_uploads.SelectedItems != null && listview_Box_uploads.SelectedItems.Count > 0)
			{
				for (int i = 0; i < listview_Box_uploads.SelectedItems.Count; i++)
				{
					BoxInfo BoxInfo = (BoxInfo)listview_Box_uploads.SelectedItems[i].Tag;

					links.AppendLine(BoxInfo.LinkUrl(pictureDisplaySize));
				}
			}
			try
			{
				Clipboard.SetText(links.ToString());
			}
			catch (Exception ex)
			{
				LOG.Error("Can't write to clipboard: ", ex);
			}
		}

		private void pictureBox_Box_Click(object sender, EventArgs e)
		{
			this.openPicture(config.PictureDisplaySize);
		}

	}
}
