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
				//Photo = "pizzas.png"
			});
			Types.Add(new TypeItemMenu()
			{
				Id = 2,
				Name = "Bebidas",
				//Photo = "bebidas.png"
			});
			Types.Add(new TypeItemMenu()
			{
				Id = 3,
				Name = "Saladas",
				//Photo = "saladas.png"
			});
			Types.Add(new TypeItemMenu()
			{
				Id = 4,
				Name = "Sanduíches",
				//Photo = "sanduiches.png"
			});
			Types.Add(new TypeItemMenu()
			{
				Id = 5,
				Name = "Sobremesas",
				//Photo = "sobremesas.png"
			});
			Types.Add(new TypeItemMenu()
			{
				Id = 6,
				Name = "Carnes",
				//Photo = "carnes.png"
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
