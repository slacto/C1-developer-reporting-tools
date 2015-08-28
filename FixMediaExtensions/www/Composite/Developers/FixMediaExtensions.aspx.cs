using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;

using Composite.Data;
using Composite.Data.Types;

namespace Composite.Developers
{
	public partial class DeveloperTools : Page
	{
		protected void Page_Load(object sender, EventArgs e) {}

		public void BtnFixMediaExtensionsClick(object sender, EventArgs e)
		{
			var mimetypeMappings = new Dictionary<string, string>
				{
					{"application/msword", "doc"},
					{"application/pdf", "pdf"},
					{"application/rtf", "rtf"},
					{"application/vnd.ms-excel", "xls"},
					{"application/vnd.ms-powerpoint", "ppt"},
					{"application/vnd.openxmlformats-officedocument.presentationml.presentation", "pptx"},
					{"application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "xlsx"},
					{"application/vnd.openxmlformats-officedocument.wordprocessingml.document", "docx"},
					{"application/x-shockwave-flash", "swf"},
					{"application/x-gzip", "zip"},
					{"audio/mpeg", "mp3"},
					{"image/bmp", "bmp"},
					{"image/gif", "gif"},
					{"image/jpeg", "jpg"},
					{"image/png", "png"},
					{"text/plain", "txt"},
					{"video/quicktime", "mov"},
					{"video/x-flv", "flv"},
					{"video/x-ms-wmv", "wmv"},
					{"video/x-msvideo", "avi"}
				};

			var messages = String.Empty;
			var unHandledMimeType = new Dictionary<string, string>();
			var changes = String.Empty;

			using (var data = new DataConnection())
			{
				var mediaFiles = data.Get<IMediaFileData>();

				foreach (var file in mediaFiles.OrderBy(f=>f.FolderPath).ThenBy(f => f.FileName))
				{
					string extension;

					if (!mimetypeMappings.TryGetValue(file.MimeType, out extension))
					{
						if (!unHandledMimeType.ContainsKey(file.MimeType))
						{
							unHandledMimeType.Add(file.MimeType, file.FolderPath + "/" + file.FileName);
						}
						continue;
					}

					var needsUpdate = false;
					var fileName = file.FileName;

					if (fileName.EndsWith(".jpeg")) fileName = fileName.Replace(".jpeg", ".jpg");
					if (fileName.EndsWith(".jpe")) fileName = fileName.Replace(".jpe", ".jpg");

					if (!fileName.EndsWith("." + extension))
					{
						changes += "<li>" + file.FolderPath + "/" + file.FileName + " (\"." + extension + "\" added)</li>";
						file.FileName = fileName + "." + extension;

						needsUpdate = true;
					}

					if (String.IsNullOrWhiteSpace(file.Title))
					{
						file.Title = file.FileName.Substring(0, file.FileName.LastIndexOf(".", StringComparison.Ordinal));

						needsUpdate = true;
					}

					if (needsUpdate)
					{
						data.Update(file);
					}
				}
			}

			if (unHandledMimeType.Any())
			{
				messages += "<div>Add the following mimetypes to the code and run it again</div><ul>";
				messages = unHandledMimeType.Aggregate(messages, (current, mimeType) => current + ("<li>" + mimeType.Key + " - see " + mimeType.Value + "</li>"));
				messages += "</ul>";
			}

			if (!String.IsNullOrEmpty(changes))
			{
				messages += "<div>The following media were updated</div><ul>" + changes + "</ul>";
			}

			if (String.IsNullOrEmpty(messages))
			{
				messages = "<div>Complete</div>";
			}

			Messages.Controls.Add(new LiteralControl(messages));
		}
	}
}