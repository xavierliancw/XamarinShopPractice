using System;
using System.IO;
using ShopPractice.DatabaseStuff;
using ShopPractice.Droid;
using Xamarin.Forms;
[assembly: Dependency(typeof(TheDatabase_Droid))]
namespace ShopPractice.Droid
{
	/// <summary>
	/// https://developer.xamarin.com/guides/xamarin-forms/application-fundamentals/databases/
	/// </summary>
	public class TheDatabase_Droid : INativeBridgeToDB
    {
        public string GetThePath(string filename)
        {
			string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
			return Path.Combine(path, filename);
        }
    }
}
