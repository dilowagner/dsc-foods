using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using DscFoods.DAL;
using DscFoods.HelperControls;
using DscFoods.Model;
using Xamarin.Forms;

namespace DscFoods.Page.ItensMenu
{
	public partial class ItensMenuPage : ContentPage
	{
		private TypeItemMenuDAL dalTypeItemMenu = new TypeItemMenuDAL(DependencyService.Get<IFileHelper>().GetLocalFilePath());
		private ItemMenuDAL dalItemMenu = new ItemMenuDAL(DependencyService.Get<IFileHelper>().GetLocalFilePath());

		public ItensMenuPage()
		{
			InitializeComponent();
			this.lvItensMenu.ItemsSource = GetDataByGroup();
		}

		private Collection<ListViewGrouping<TypeItemMenu, ItemMenu>> GetDataByGroup()
		{
			var groupData = new Collection<ListViewGrouping<TypeItemMenu, ItemMenu>>();
			var types = dalTypeItemMenu.GetAllWithChildren();
			foreach (var tipo in types)
			{
				groupData.Add(new ListViewGrouping<TypeItemMenu, ItemMenu>(tipo, tipo.Itens));
			}
			return groupData;
		}
	}
}
