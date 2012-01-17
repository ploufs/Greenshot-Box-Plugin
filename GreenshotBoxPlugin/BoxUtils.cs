/*
 * Greenshot - a free and open source screenshot tool
 * Copyright (C) 2011-2012  Francis Noel
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
using System.Collections.Specialized;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Xml;
using System.Xml.Linq;
using GreenshotPlugin.Core;
using IniFile;

namespace GreenshotBoxPlugin {
	/// <summary>
	/// Description of ImgurUtils.
	/// </summary>
	public class BoxUtils {
		private static readonly log4net.ILog LOG = log4net.LogManager.GetLogger(typeof(BoxUtils));
		public static string Box_API_KEY = "fhzk02sc02jef67sple9u9j5gxa9all4";
		private static BoxConfiguration config = IniConfig.GetIniSection<BoxConfiguration>();

		private BoxUtils() {
		}

		/// <summary>
		/// Instance of soap with binding and end point set.
		/// </summary>
		/// <returns></returns>
		public static BoxNet.boxnetPortClient SoapClient()
		{
		   Binding binding=new BasicHttpBinding(BasicHttpSecurityMode.None);

		   EndpointAddress remoteAddress=new EndpointAddress("http://box.net/api/1.0/soap");

			return new BoxNet.boxnetPortClient(binding, remoteAddress);
		}

		public static void LoadHistory() {
			if (config.runtimeBoxHistory == null) {
				return;
			}
			if (config.BoxUploadHistory == null)
			{
				return;
			}

			if (config.runtimeBoxHistory.Count == config.BoxUploadHistory.Count) {
				return;
			}
			// Load the Box history
			List<string> hashes = new List<string>();
			foreach (string hash in config.BoxUploadHistory.Keys)
			{
				hashes.Add(hash);
			}
			
			bool saveNeeded = false;

			foreach(string hash in hashes) {
				if (config.runtimeBoxHistory.ContainsKey(hash)) {
					// Already loaded
					continue;
				}
				try {
					long id = 0;
					id = long.Parse(hash);
					BoxInfo imgurInfo = BoxUtils.RetrieveBoxInfo(id);
					if (imgurInfo != null) {
						BoxUtils.RetrieveBoxThumbnail(imgurInfo);
						config.runtimeBoxHistory.Add(hash, imgurInfo);
					} else {
						LOG.DebugFormat("Deleting not found Box {0} from config.", hash);
						config.BoxUploadHistory.Remove(hash);
						saveNeeded = true;
					}
				} catch (Exception e) {
					LOG.Error("Problem loading Box history for hash " + hash, e);
				}
			}
			if (saveNeeded) {
				// Save needed changes
				IniConfig.Save();
			}
		}

		/// <summary>
		/// Upload file by post
		/// </summary>
		/// <param name="url">Post action</param>
		/// <param name="fileName"></param>
		/// <param name="paramName"></param>
		/// <param name="contentType"></param>
		/// <param name="nvc"></param>
		/// <returns>Web response</returns>
		public static Stream HttpUploadFile(string url, string fileName, byte[] fileContent, string paramName, string contentType, NameValueCollection nvc)
		{
			LOG.InfoFormat(string.Format("Uploading {0} to {1}", fileName, url));
			string boundary = "---------------------------" + DateTime.Now.Ticks.ToString("x");
			byte[] boundarybytes = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");

			HttpWebRequest wr = (HttpWebRequest)WebRequest.Create(url);
			wr.ContentType = "multipart/form-data; boundary=" + boundary;
			wr.Method = "POST";
			wr.KeepAlive = true;
			wr.Credentials = System.Net.CredentialCache.DefaultCredentials;

			Stream rs = wr.GetRequestStream();

			string formdataTemplate = "Content-Disposition: form-data; name=\"{0}\"\r\n\r\n{1}";
			foreach (string key in nvc.Keys)
			{
				rs.Write(boundarybytes, 0, boundarybytes.Length);
				string formitem = string.Format(formdataTemplate, key, nvc[key]);
				byte[] formitembytes = System.Text.Encoding.UTF8.GetBytes(formitem);
				rs.Write(formitembytes, 0, formitembytes.Length);
			}
			rs.Write(boundarybytes, 0, boundarybytes.Length);

			string headerTemplate = "Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\nContent-Type: {2}\r\n\r\n";
			string header = string.Format(headerTemplate, paramName, fileName, contentType);
			byte[] headerbytes = System.Text.Encoding.UTF8.GetBytes(header);
			rs.Write(headerbytes, 0, headerbytes.Length);

			rs.Write(fileContent, 0, fileContent.Length);


			byte[] trailer = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "--\r\n");
			rs.Write(trailer, 0, trailer.Length);
			rs.Close();

			WebResponse wresp = null;
			try
			{
				wresp = wr.GetResponse();
				return wresp.GetResponseStream();
			}
			catch (Exception ex)
			{
				LOG.Error("Error uploading file", ex);
				if (wresp != null)
				{
					wresp.Close();
					wresp = null;
				}
				return null;
			}
			finally
			{
				wr = null;
			}
		}

		/// <summary>
		/// Do the actual upload to Box
		/// For more details on the available parameters, see: http://developers.box.net/w/page/12923951/ApiFunction_Upload%20and%20Download
		/// </summary>
		/// <param name="imageData">byte[] with image data</param>
		/// <returns>BoxResponse</returns>
		public static BoxInfo UploadToBox(byte[] imageData, string title, string filename, string contentType)
		{
			string folderId = "0";
			string strUrl = string.Format("https://upload.box.net/api/1.0/upload/{0}/{1}?file_name={2}&new_copy=1", config.boxToken, folderId, filename);

			NameValueCollection nvc = new NameValueCollection();
			nvc.Add("share", "1");
			XmlReader streamIn = XmlReader.Create(HttpUploadFile(strUrl, filename, imageData, "new_file", contentType, nvc));
			
			XDocument xdoc = XDocument.Load(streamIn);
			XElement xResponse = xdoc.Element("response");
			XElement xStatus = xResponse.Element("status");
			XElement xFiles = xResponse.Element("files");
			XElement xFile = xFiles.Element("file");

			streamIn.Close();

			if (xStatus.Value.Equals("upload_ok", StringComparison.OrdinalIgnoreCase))
			{
				string fileID = xFile.Attribute("id").Value;
				long id = long.Parse(fileID);
				return RetrieveBoxInfo(id);
			}
			else
			{
				return null;
			}
		}

		public static Image CreateThumbnail(Image image, int thumbWidth, int thumbHeight) {
			int srcWidth=image.Width;
			int srcHeight=image.Height; 
			Bitmap bmp = new Bitmap(thumbWidth, thumbHeight);  
			using (Graphics gr = System.Drawing.Graphics.FromImage(bmp)) {
				gr.SmoothingMode = SmoothingMode.HighQuality  ; 
				gr.CompositingQuality = CompositingQuality.HighQuality; 
				gr.InterpolationMode = InterpolationMode.High; 
				System.Drawing.Rectangle rectDestination = new System.Drawing.Rectangle(0, 0, thumbWidth, thumbHeight);
				gr.DrawImage(image, rectDestination, 0, 0, srcWidth, srcHeight, GraphicsUnit.Pixel);  
			}
			return bmp;
		}

		public static void RetrieveBoxThumbnail(BoxInfo imgurInfo) {
			LOG.InfoFormat("Retrieving Box image for {0} with url {1}", imgurInfo.ID, imgurInfo);
			HttpWebRequest webRequest = (HttpWebRequest)NetworkHelper.CreatedWebRequest(imgurInfo.SquareThumbnailUrl);
			webRequest.Method = "GET";
			webRequest.ServicePoint.Expect100Continue = false;

			using (WebResponse response = webRequest.GetResponse()) {
				Stream responseStream = response.GetResponseStream();
				imgurInfo.Image = Image.FromStream(responseStream);
			}
			return;
		}

		public static BoxInfo RetrieveBoxInfo(long id)
		{
			BoxInfo boxInfo = new BoxInfo();
			boxInfo.ID = id;
			boxInfo.WebUrl = string.Format("http://www.box.com/files#/files/0/f/0/1/f_{0}",  id);
		
			boxInfo.SquareThumbnailUrl =  string.Format("https://www.box.net/api/1.0/download/{0}/{1}", config.boxToken, id);
			boxInfo.OriginalUrl = boxInfo.SquareThumbnailUrl;
			boxInfo.Timestamp = DateTime.Now;

			using (BoxNet.boxnetPortClient client = BoxUtils.SoapClient())
			{
				BoxNet.SOAPFileInfo info;
				client.get_file_info(out info, BoxUtils.Box_API_KEY, config.boxToken, id);
				if (info != null)
				{
					boxInfo.Description = info.description;
					boxInfo.Title = info.file_name;
					if (info.shared == 1)
					{
						boxInfo.WebUrl = string.Format("http://www.box.net/shared/{0}", info.public_name);
					}
				}
			}
			return boxInfo;
		}


		public static void DeleteBoxImage(BoxInfo BoxInfo)
		{
			config.runtimeBoxHistory.Remove(BoxInfo.ID.ToString());
			config.BoxUploadHistory.Remove(BoxInfo.ID.ToString());

			using (BoxNet.boxnetPortClient client = BoxUtils.SoapClient())
			{
				client.delete(BoxUtils.Box_API_KEY, config.boxToken, "file", BoxInfo.ID);
			}
			BoxInfo.Image = null;
		}
	}
}
