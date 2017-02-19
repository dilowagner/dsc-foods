using System.Collections.Generic;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace DscFoods.Model
{
	public class TypeItemMenu
	{
		[PrimaryKey, AutoIncrement]
		public long Id { get; set; }
		public string Name { get; set; }
		public byte[] Photo { get; set; }

		[Ignore, OneToMany]
		public List<ItemMenu> Itens { get; set; }

		public override bool Equals(object obj)
		{
			TypeItemMenu type = obj as TypeItemMenu;
			if (type == null)
			{
				return false;
			}

			return (Id.Equals(type.Id));
		}

		public override int GetHashCode()
		{
			return Id.GetHashCode();
		}
	}
}