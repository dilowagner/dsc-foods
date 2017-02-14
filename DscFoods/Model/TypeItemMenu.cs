namespace DscFoods.Model
{
	public class TypeItemMenu
	{
		public long Id { get; set; }
		public string Name { get; set; }
		public string PhotoPath { get; set; }

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
