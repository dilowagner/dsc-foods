using DscFoods.DAL;
using Xamarin.Forms;

namespace DscFoods.Page
{
	public partial class DeliverymanListPage : ContentPage
	{
		private DeliverymanDAL delivery = DeliverymanDAL.GetInstance();

		public DeliverymanListPage()
		{
			InitializeComponent();
			lvDeliveries.ItemsSource = delivery.GetAll();
		}
	}
}
