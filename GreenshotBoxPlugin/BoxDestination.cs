
using System.ComponentModel;
using System.Drawing;
using Greenshot.Plugin;
using GreenshotPlugin.Core;
using IniFile;

namespace GreenshotBoxPlugin
{
	class BoxDestination:AbstractDestination
	{
		private static log4net.ILog LOG = log4net.LogManager.GetLogger(typeof(BoxDestination));
		private static BoxConfiguration config = IniConfig.GetIniSection<BoxConfiguration>();
		private ILanguage lang = Language.GetInstance();

		private BoxPlugin plugin = null;
		public BoxDestination(BoxPlugin plugin)
		{
			this.plugin = plugin;
		}
		
		public override string Designation {
			get {
				return "Box";
			}
		}

		public override string Description {
			get {
				return lang.GetString(LangKey.upload_menu_item);
			}
		}

		public override Image DisplayIcon {
			get {
				ComponentResourceManager resources = new ComponentResourceManager(typeof(BoxPlugin));
				return (Image)resources.GetObject("Box");
			}
		}

		public override bool ExportCapture(ISurface surface, ICaptureDetails captureDetails) {
			using (Image image = surface.GetImageForExport()) {
				bool uploaded = plugin.Upload(captureDetails, image);
				if (uploaded) {
					surface.SendMessageEvent(this, SurfaceMessageTyp.Info, "Exported to Box");
					surface.Modified = false;
				}
				return uploaded;
			}
		}
	}
}
