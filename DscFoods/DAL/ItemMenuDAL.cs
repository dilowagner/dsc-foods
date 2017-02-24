using System.Collections.Generic;
using DscFoods.Model;
using SQLite;
using System.Linq;

namespace DscFoods.DAL
{
	public class ItemMenuDAL
	{
		private readonly SQLiteConnection database;

		public ItemMenuDAL(string dbPath)
		{
			database = new SQLiteConnection(dbPath);
			database.CreateTable<ItemMenu>();
		}

		public IEnumerable<ItemMenu> GetAll()
		{
			return (from t in database.Table<ItemMenu>() select t).OrderBy(i => i.Name).ToList();
		}

		public ItemMenu GetItemById(long id)
		{
			return database.Table<ItemMenu>().FirstOrDefault(t => t.Id == id);
		}

		public void DeleteById(long id)
		{
			database.Delete<ItemMenu>(id);
		}

		public void Add(ItemMenu type)
		{
			database.Insert(type);
		}

		public void Update(ItemMenu type)
		{
			database.Update(type);
		}
	}
}
