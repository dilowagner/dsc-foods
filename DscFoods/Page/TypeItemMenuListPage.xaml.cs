using System;
using DscFoods.DAL;
using DscFoods.Model;
using Xamarin.Forms;

namespace DscFoods.Page
{
	public partial class TypeItemMenuListPage : ContentPage
	{
		private TypeItemMenuDAL dalTypeItensMenu = new TypeItemMenuDAL(DependencyService.Get<IFileHelper>().GetLocalFilePath());

		public TypeItemMenuListPage()
		{
			InitializeComponent();
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
				dalTypeItensMenu.DeleteById(item.Id);
				lvTypesItensMenu.ItemsSource = dalTypeItensMenu.GetAll();
			}
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();
			lvTypesItensMenu.ItemsSource = dalTypeItensMenu.GetAll();
		}
	}
}
