using System;
using Plugin.Media;
using DscFoods.DAL;
using DscFoods.Model;
using Xamarin.Forms;
using PCLStorage;
using System.IO;

namespace DscFoods.Page
{
	public partial class TypeItemMenuNewPage : ContentPage
	{
		private TypeItemMenuDAL dalTypeItensMenu = new TypeItemMenuDAL(DependencyService.Get<IFileHelper>().GetLocalFilePath());
		private byte[] photos;

		public TypeItemMenuNewPage()
		{
			InitializeComponent();
			PrepareNewTypeItemMenu();
			RegisterClickButtonCamera(idtypeitemmenu.Text.Trim());
			RegisterClickButtonAlbum();
		}

		private void RegisterClickButtonAlbum()
		{
			// Criação do método anônimo para captura do evento click do botão
			BtnAlbum.Clicked += async (sender, args) =>
			{
				// Inicialização do plugin de interação com a câmera e álbum
				await CrossMedia.Current.Initialize();

				// Verificação de disponibilização do álbum
				if (!CrossMedia.Current.IsPickPhotoSupported)
				{
					DisplayAlert("Álbum não suportado", "Não existe permissão para acessar o álbum de fotos", "OK");
					return;
				}

				// Método que habilita o álbum e permite a seleção de uma foto
				var file = await CrossMedia.Current.PickPhotoAsync();

				// Caso o usuário não tenha selecionado uma foto, clicando no botão cancelar
				if (file == null)
					return;

				var stream = file.GetStream();
				var memoryStream = new MemoryStream();
				stream.CopyTo(memoryStream);

				// Recupera o arquivo selecionado e o atribui ao controle no formulário
				photopath.Source = ImageSource.FromStream(() =>
				{
					var s = file.GetStream();
					file.Dispose();
					return s;
				});
				photos = memoryStream.ToArray();
			};
		}

		private void RegisterClickButtonCamera(string idparafoto)
		{
			// Criação do método anônimo para captura do evento click do botão
			BtnCamera.Clicked += async (sender, args) =>
			{
				// Inicialização do plugin de interação com a câmera
				await CrossMedia.Current.Initialize();

				// Verificação de disponibilização da câmera e se é possível tirar foto
				if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
				{
					DisplayAlert("Não existe câmera", "A câmera não está disponível.", "OK");
					return;
				}

				/* Método que habilita a câmera, informando a pasta onde a foto deverá
                   ser armazenada, o nome a ser dado ao arquivo e se é ou não para 
                   armazenar a foto também no álbum */
				var file = await CrossMedia.Current.TakePhotoAsync(
					new Plugin.Media.Abstractions.StoreCameraMediaOptions
					{
						Directory = FileSystem.Current.LocalStorage.Name,
						Name = "tipoitem_" + idparafoto + ".jpg",
						SaveToAlbum = true
					});

				// Caso o usuário não tenha tirado a foto, clicando no botão cancelar 
				if (file == null)
					return;

				var stream = file.GetStream();
				var memoryStream = new MemoryStream();
				stream.CopyTo(memoryStream);

				// Recupera o arquivo selecionado e o atribui ao controle no formulário
				photopath.Source = ImageSource.FromStream(() =>
				{
					var s = file.GetStream();
					file.Dispose();
					return s;
				});
				photos = memoryStream.ToArray();
			};
		}

		public void BtnSaveClick(object sender, EventArgs e)
		{
			if (name.Text.Trim() == string.Empty)
			{
				this.DisplayAlert("Erro",
					"Você precisa informar o nome para o novo tipo de item do cardápio.",
					"Ok");
			}
			else
			{
				dalTypeItensMenu.Add(new TypeItemMenu()
				{
					Name = name.Text,
					Photo = photos
				});
				PrepareNewTypeItemMenu();
			}
		}

		private void PrepareNewTypeItemMenu()
		{
			name.Text = string.Empty;
			photopath.Source = null;
		}
	}
}
