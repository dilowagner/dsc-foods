using System;
using Xamarin.Forms;

namespace DscFoods.Page
{
	public partial class MenuPage : ContentPage
	{
		public MenuPage()
		{
			InitializeComponent();
		}

		private async void WaitersOnClicked(object sender, EventArgs args)
		{
			await Navigation.PushAsync(new WaitersPage());
		}

		private async void DeliverymanOnClicked(object sender, EventArgs args)
		{
			await Navigation.PushAsync(new DeliverymanPage());
		}

		private async void TypeItemMenuOnClicked(object sender, EventArgs args)
		{
			await Navigation.PushAsync(new TypeItemMenuPage());
		}
	}
}
