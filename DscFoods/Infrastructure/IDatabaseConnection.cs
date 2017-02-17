using SQLite.Net;

namespace DscFoods.Infrastructure
{
	public interface IDatabaseConnection
	{
		SQLiteConnection DbConnection();
	}
}
