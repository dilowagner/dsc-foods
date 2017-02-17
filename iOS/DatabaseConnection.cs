using System;
using System.IO;
using DscFoods.Infrastructure;
using SQLite.Net;
using DscFoods.iOS;

[assembly: Xamarin.Forms.Dependency(typeof(DatabaseConnection))]
namespace DscFoods.iOS
{
	public class DatabaseConnection : IDatabaseConnection
	{
		public SQLiteConnection DbConnection()
		{
			var dbName = "DscFoodsDb.db3";
			string personalFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
			string libraryFolder = Path.Combine(personalFolder, "..", "Library");
			var path = Path.Combine(libraryFolder, dbName);
			return new SQLiteConnection(path);
		}
	}
}
