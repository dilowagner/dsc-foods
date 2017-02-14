using System;
using System.Linq;
using DscFoods.DAL;
using DscFoods.Model;
using Xamarin.Forms;

namespace DscFoods.Page
{
	public partial class DeliverymanNewPage : ContentPage
	{
		private DeliverymanDAL delivery = DeliverymanDAL.GetInstance();

		public DeliverymanNewPage()
		{
			InitializeComponent();
			PrepareNewDeliveryman();
		}

		public void BtnGravarClick(object sender, EventArgs e)
		{
			if (name.Text.Trim() == string.Empty || phone.Text == string.Empty)
			{
				this.DisplayAlert("Erro",
					"Você precisa informar o nome e telefone para o novo entregador.",
					"Ok");
			}
			else
			{
				delivery.Add(new Deliveryman()
				{
					Id = Convert.ToUInt32(iddelivery.Text),
					Name = name.Text,
					Phone = phone.Text
				});
				PrepareNewDeliveryman();
			}
		}

		private void PrepareNewDeliveryman()
		{
			var newId = delivery.GetAll().Max(x => x.Id) + 1;
			iddelivery.Text = newId.ToString().Trim();
			name.Text = string.Empty;
			phone.Text = string.Empty;
		}
	}
}
