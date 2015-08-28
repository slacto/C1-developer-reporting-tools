using System;
using System.IO;
using System.Web.UI;

using Composite.Core.IO;

namespace Composite.Developers
{
	public partial class DeveloperTools : Page
	{
		protected void Page_Load(object sender, EventArgs e) {}

		public void BtnFixFileNamesClick(object sender, EventArgs e)
		{
            Func<string, string> fixFileNames = file => file.Replace("_Published", "");

		    var files = Directory.GetFiles(PathUtil.Resolve("~/App_Data/Composite/DataStores"), "*_Published*.xml");

		    foreach (var file in files)
		    {
                Response.Write("<div>" + file + "</div>");

		        var newFileName = fixFileNames(file);
		        if (File.Exists(newFileName))
		        {
		            var oldFileName = file.Replace(".xml", ".old");

		            if (!File.Exists(oldFileName))
		            {
                        File.Move(file, oldFileName);
		            }
		        }
		        else
		        {
		            File.Move(file, newFileName);
		        }
		    }

		    var configFile = PathUtil.Resolve("~/App_Data/Composite/Configuration/DynamicXmlDataProvider.config");
		    var content = File.ReadAllText(configFile);

            File.WriteAllText(configFile, fixFileNames(content));

            Response.Write("<pre>" + content + "</pre>");
		}
	}
}