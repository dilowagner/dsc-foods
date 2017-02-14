using System.Collections.ObjectModel;
using DscFoods.Model;

namespace DscFoods.DAL
{
	public class TypeItemMenuDAL
	{
		private ObservableCollection<TypeItemMenu> Types = new ObservableCollection<TypeItemMenu>();
		private static TypeItemMenuDAL TypeItemMenuInstance = new TypeItemMenuDAL();

		public TypeItemMenuDAL()
		{
			Types.Add(new TypeItemMenu()
			{
				Id = 1,
				Name = "Pizza",
				PhotoPath = "pizzas.png"
			});
			Types.Add(new TypeItemMenu()
			{
				Id = 2,
				Name = "Bebidas",
				PhotoPath = "bebidas.png"
			});
			Types.Add(new TypeItemMenu()
			{
				Id = 3,
				Name = "Saladas",
				PhotoPath = "saladas.png"
			});
			Types.Add(new TypeItemMenu()
			{
				Id = 4,
				Name = "Sanduíches",
				PhotoPath = "sanduiches.png"
			});
			Types.Add(new TypeItemMenu()
			{
				Id = 5,
				Name = "Sobremesas",
				PhotoPath = "sobremesas.png"
			});
			Types.Add(new TypeItemMenu()
			{
				Id = 6,
				Name = "Carnes",
				PhotoPath = "carnes.png"
			});
		}

		public static TypeItemMenuDAL GetInstance()
		{
			return TypeItemMenuInstance;

		}
		public ObservableCollection<TypeItemMenu> GetAll()
		{
			return Types;
		}

		public void Add(TypeItemMenu type)
		{
			this.Types.Add(type);
		}

		public void Remove(TypeItemMenu type)
		{
			this.Types.Remove(type);
		}

		public void Update(TypeItemMenu type)
		{
			this.Types[this.Types.IndexOf(type)] = type;
		}
	}
}
