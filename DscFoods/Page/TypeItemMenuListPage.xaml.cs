using System;
using DscFoods.DAL;
using DscFoods.Model;
using Xamarin.Forms;

namespace DscFoods.Page
{
	public partial class TypeItemMenuListPage : ContentPage
	{
		private TypeItemMenuDAL dalTypeItensMenu = TypeItemMenuDAL.GetInstance();

		public TypeItemMenuListPage()
		{
			InitializeComponent();
			lvTypesItensMenu.ItemsSource = dalTypeItensMenu.GetAll();
		}

		public async void OnUpdateClick(object sender, EventArgs e)
		{
			var mi = ((MenuItem)sender);
			var item = mi.CommandParameter as TypeItemMenu;
			await Navigation.PushModalAsync(new TypeItemMenuEditPage(item));
		}

		public async void OnRemoveClick(object sender, EventArgs e)
		{
			var mi = ((MenuItem)sender);
			var item = mi.CommandParameter as TypeItemMenu;
			var opcao = await DisplayAlert("Confirmação de exclusão",
				"Confirma excluir o item " + item.Name.ToUpper() + "?", "Sim", "Não");
			if (opcao)
			{
				dalTypeItensMenu.Remove(item);
			}
		}
	}
}
