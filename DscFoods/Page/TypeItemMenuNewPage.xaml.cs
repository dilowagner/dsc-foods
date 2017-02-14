using System;
using Plugin.Media;
using System.Linq;
using DscFoods.DAL;
using DscFoods.Model;
using Xamarin.Forms;
using PCLStorage;

namespace DscFoods.Page
{
	public partial class TypeItemMenuNewPage : ContentPage
	{
		private TypeItemMenuDAL types = TypeItemMenuDAL.GetInstance();
		private string filePath;

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

				/* Nas instruções abaixo é feito uso dos components PCLStorage
                   e PCLSpecialFolder */

				// Recupera o arquivo com base no caminho da foto selecionada
				var getArquivoPCL = FileSystem.Current.GetFileFromPathAsync(file.Path);

				// Recupera o caminho raiz da aplicação no dispositivo
				var rootFolder = FileSystem.Current.LocalStorage;

				/* Caso a pasta Fotos não exista, ela é criada para 
                   armazenamento da foto selecionada */
				var folderFoto = await rootFolder.CreateFolderAsync("Fotos", CreationCollisionOption.OpenIfExists);

				// Cria o arquivo referente a foto selecionada
				var setArquivoPCL = folderFoto.CreateFileAsync(getArquivoPCL.Result.Name, CreationCollisionOption.ReplaceExisting);

				// Faz a leitura do arquivo em bytes, para que ele possa ser escrito
				var arquivoLido = getArquivoPCL.Result.ReadAllTextAsync();

				// Escreve o arquivo fisicamente
				var arquivoEscrito = setArquivoPCL.Result.WriteAllTextAsync(arquivoLido.Result);

				// Guarda o caminho do arquivo para ser utilizado na gravação do item criado
				filePath = setArquivoPCL.Result.Path;

				// Recupera o arquivo selecionado e o atribui ao controle no formulário
				photopath.Source = ImageSource.FromStream(() =>
				{
					var stream = file.GetStream();
					file.Dispose();
					return stream;
				});
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

				// Armazena o caminho da foto para ser utilizado na gravação do item
				filePath = file.Path;

				// Recupera a foto e a atribui para o controle visual
				photopath.Source = ImageSource.FromStream(() =>
				{
					var stream = file.GetStream();
					file.Dispose();
					return stream;
				});
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
				types.Add(new TypeItemMenu()
				{
					Id = Convert.ToUInt32(idtypeitemmenu.Text),
					Name = name.Text,
					PhotoPath = filePath
				});
				PrepareNewTypeItemMenu();
			}
		}

		private void PrepareNewTypeItemMenu()
		{
			var newId = types.GetAll().Max(x => x.Id) + 1;
			idtypeitemmenu.Text = newId.ToString().Trim();
			name.Text = string.Empty;
			photopath.Source = null;
		}
	}
}
