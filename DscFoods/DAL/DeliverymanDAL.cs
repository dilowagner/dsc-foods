using System.Collections.ObjectModel;
using DscFoods.Model;

namespace DscFoods.DAL
{
	public class DeliverymanDAL
	{
		private ObservableCollection<Deliveryman> deliveries = new ObservableCollection<Deliveryman>();
		private static DeliverymanDAL DeliverymanInstance = new DeliverymanDAL();

		public DeliverymanDAL()
		{
			deliveries.Add(new Deliveryman()
			{
				Id = 1,
				Name = "Brauzio",
				Phone = "Asdrugio"
			});

			deliveries.Add(new Deliveryman()
			{
			  Id = 2,
			  Name = "Entencius",
			  Phone = "Gesfredio"
			});

			deliveries.Add(new Deliveryman() {
				Id = 3, 
				Name = "Cartucious", 
				Phone = "Gesfrundo"
			});

			deliveries.Add(new Deliveryman() {
				Id = 4, 
				Name = "Adoliterio", 
				Phone = "Kentencio" 
			});

			deliveries.Add(new Deliveryman() {
				Id = 5, 
				Name = "Castrogildo", 
				Phone = "Gesifrelio" 
			});

			deliveries.Add(new Deliveryman() {
				Id = 6, 
				Name = "Asdrugio", 
				Phone = "Brauzio" 
			});
						
			deliveries.Add(new Deliveryman() {
				Id = 7,
				Name = "Gesfredio",
				Phone = "Entencius"
			});
			deliveries.Add(new Deliveryman()
			{
				Id = 8,
				Name = "Gesfrundio",
				Phone = "Cartucious" 
			});

			deliveries.Add(new Deliveryman()
			{
				Id = 9,
				Name = "Kentencio",
				Phone = "Adoliterio" 
			});

			deliveries.Add(new Deliveryman()
			{
				Id	 = 10,
				Name = "Gesifrelio",
				Phone = "Castrogildo"
			});
		}

		public static DeliverymanDAL GetInstance()
		{
			return DeliverymanInstance;
		}

		public ObservableCollection<Deliveryman> GetAll()
		{
			return deliveries;
		}

		public void Add(Deliveryman deliveryman) 
		{
			this.deliveries.Add(deliveryman);
		}
	}
}