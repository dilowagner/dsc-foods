using System;
using System.IO;
using Xamarin.Forms;
using DscFoods.iOS;

[assembly: Dependency(typeof(FileHelper))]
namespace DscFoods.iOS
{
	public class FileHelper : IFileHelper
	{
		public string GetLocalFilePath()
		{
			string docFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
			string libFolder = Path.Combine(docFolder, "..", "Library", "Databases");

			if (!Directory.Exists(libFolder))
			{
				Directory.CreateDirectory(libFolder);
			}

			return Path.Combine(libFolder, "DscFoodsDb.db3");
		}
	}
}
