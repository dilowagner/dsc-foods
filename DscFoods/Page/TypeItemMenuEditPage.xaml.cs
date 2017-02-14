using DscFoods.Model;
using DscFoods.DAL;
using Xamarin.Forms;
using System;
using Plugin.Media;
using PCLStorage;

namespace DscFoods.Page
{
	public partial class TypeItemMenuEditPage : ContentPage
	{
		private TypeItemMenu type;
		private string filePath;
		private TypeItemMenuDAL types = TypeItemMenuDAL.GetInstance();

		public TypeItemMenuEditPage(TypeItemMenu type)
		{
			InitializeComponent();
			PopulateForm(type);
			RegisterClickCameraButton(idtypeitemenu.Text.Trim());

			RegisterClickAlbumButton();
		}

		private void PopulateForm(TypeItemMenu typeItem)
		{
			this.type = typeItem;
			idtypeitemenu.Text = typeItem.Id.ToString();
			name.Text = typeItem.Name;
			filePath = typeItem.PhotoPath;
			photopath.Source = ImageSource.FromFile(typeItem.PhotoPath);
		}

		private void RegisterClickAlbumButton()
		{
			BtnAlbum.Clicked += async (sender, args) =>
			{
				await CrossMedia.Current.Initialize();

				if (!CrossMedia.Current.IsPickPhotoSupported)
				{
					DisplayAlert("Álbum não suportado", "Não existe permissão para acessar o álbum de fotos", "OK");
					return;
				}

				var file = await CrossMedia.Current.PickPhotoAsync();
				var getArquivoPCL = FileSystem.Current.GetFileFromPathAsync(file.Path);
				var rootFolder = FileSystem.Current.LocalStorage;
				var folderFoto = await rootFolder.CreateFolderAsync("Fotos", CreationCollisionOption.OpenIfExists);
				var setArquivoPCL = folderFoto.CreateFileAsync(getArquivoPCL.Result.Name, CreationCollisionOption.ReplaceExisting);
				var arquivoLido = getArquivoPCL.Result.ReadAllTextAsync();
				var arquivoEscrito = setArquivoPCL.Result.WriteAllTextAsync(arquivoLido.Result);
				filePath = setArquivoPCL.Result.Path;

				if (file == null)
					return;

				photopath.Source = ImageSource.FromStream(() =>
				{
					var stream = file.GetStream();
					file.Dispose();
					return stream;
				});
			};
		}

		private void RegisterClickCameraButton(string idparafoto)
		{
			BtnCamera.Clicked += async (sender, args) =>
			{
				await CrossMedia.Current.Initialize();

				if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
				{
					await DisplayAlert("Não existe câmera", "A câmera não está disponível.", "OK");
					return;
				}

				var file = await CrossMedia.Current.TakePhotoAsync(
					new Plugin.Media.Abstractions.StoreCameraMediaOptions
					{
						Directory = FileSystem.Current.LocalStorage.Name,
						Name = "tipoitem_" + idparafoto.Trim() + ".jpg"
					});

				if (file == null)
					return;

				photopath.Source = ImageSource.FromFile(file.Path);
				filePath = file.Path;
			};
		}

		public async void BtnSaveClick(object sender, EventArgs e)
		{
			if (name.Text.Trim() == string.Empty)
			{
				this.DisplayAlert("Erro", "Você precisa informar o nome para o novo tipo de item do cardápio.", "Ok");
			}
			else
			{
				this.type.Name = name.Text;
				this.type.PhotoPath = filePath;

				types.Update(this.type);
				await Navigation.PopModalAsync();
			}
		}
	}
}
