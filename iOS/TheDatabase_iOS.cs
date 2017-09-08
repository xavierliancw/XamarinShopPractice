using System;
using System.IO;
using ShopPractice.DatabaseStuff;
using ShopPractice.iOS;
using Xamarin.Forms;
[assembly: Dependency(typeof(TheDatabase_iOS))]
namespace ShopPractice.iOS
{
	/// <summary>
	/// https://developer.xamarin.com/guides/xamarin-forms/application-fundamentals/databases/
	/// </summary>
	public class TheDatabase_iOS : INativeBridgeToDB
    {
        public string GetThePath(string filename)
        {
            string docFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string libFolder = Path.Combine(docFolder, "..", "Library", "Databases");

            if (!Directory.Exists(libFolder))
            {
                Directory.CreateDirectory(libFolder);
            }
            return Path.Combine(libFolder, filename);
        }
    }
}
