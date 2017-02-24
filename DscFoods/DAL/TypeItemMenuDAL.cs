using System.Collections.Generic;
using DscFoods.Model;
using SQLite;
using System.Linq;

namespace DscFoods.DAL
{
	public class TypeItemMenuDAL
	{
		private readonly SQLiteConnection database;

		public TypeItemMenuDAL(string dbPath)
		{
			database = new SQLiteConnection(dbPath);
			database.CreateTable<TypeItemMenu>();
		}

		public IEnumerable<TypeItemMenu> GetAll()
		{
			return (from t in database.Table<TypeItemMenu>() select t).OrderBy(i => i.Name).ToList();
		}

		public IEnumerable<TypeItemMenu> GetAllWithChildren()
		{
			return (from t in database.Table<TypeItemMenu>() select t).OrderBy(i => i.Name).ToList();
		}

		public TypeItemMenu GetItemById(long id)
		{
			return database.Table<TypeItemMenu>().FirstOrDefault(t => t.Id == id);
		}

		public void DeleteById(long id)
		{
			database.Delete<TypeItemMenu>(id);
		}

		public void Add(TypeItemMenu type)
		{
			database.Insert(type);
		}

		public void Update(TypeItemMenu type)
		{
			database.Update(type);
		}
	}
}
