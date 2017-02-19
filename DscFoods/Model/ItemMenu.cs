using SQLite;
using SQLiteNetExtensions.Attributes;

namespace DscFoods.Model
{
	public class ItemMenu
	{
		[PrimaryKey, AutoIncrement]
		public long? Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public double Price { get; set; }
		public byte[] Photo { get; set; }

		[ForeignKey(typeof(TypeItemMenu))]
		public long? TypeItemMenuId { get; set; }

		[ManyToOne(CascadeOperations = CascadeOperation.CascadeRead)]
		public TypeItemMenu TypeItemMenu { get; set; }

		public override bool Equals(object obj)
		{
			ItemMenu item = obj as ItemMenu;
			if (item == null)
			{
				return false;
			}

			return (Id.Equals(item.Id));
		}

		public override int GetHashCode()
		{
			return Id.GetHashCode();
		}
	}
}
