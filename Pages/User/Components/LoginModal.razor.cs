using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;

namespace LojaVirtual.Pages.User.Components
{

	public class LoginModalBase : ComponentBase
	{
		#region Fields

		public Guid Guid = Guid.NewGuid();
		public string ModalDisplay = "none;";
		public string ModalClass = "";
		public bool ShowBackdrop = false;
		Models.User _userData;

		#endregion

		#region Properties

		[Inject]
		public NavigationManager NavigationManager { get; set; }
		public string StatusClass { get; set; }
		public string StatusMessage { get; set; }


		protected Models.User UserData
		{
			get
			{
				_userData ??= new Models.User();
				return _userData;
			}
			set
			{
				_userData = value != null ? value : new Models.User();
			}
		}

		#endregion

		#region Methods

		public void Open()
		{
			ModalDisplay = "block;";
			ModalClass = "Show";
			ShowBackdrop = true;
			StateHasChanged();
		}

		public void Close()
		{
			ModalDisplay = "none";
			ModalClass = "";
			ShowBackdrop = false;
			NavigationManager.NavigateTo(NavigationManager.Uri, true);
			StateHasChanged();
		}

		protected async Task ValidSubmitAsync()
		{
			try
			{
				// Initialization.  
				bool isLogged = await LoginManagement.LoginAsync(UserData.Name, UserData.Password);
				if (isLogged)
				{
					StatusClass = "alert-info";
					//todo: this message
					StatusMessage = "Conexão Efetuada com Sucesso!";
					Close();

				}
				else
				{
					InvalidSubmit();
				}

			}
			catch (Exception ex)
			{
				StatusClass = "alert-danger";
				StatusMessage = "Erro: " + ex.Message;
			}

		}

		protected void InvalidSubmit()
		{
			StatusClass = "alert-danger";
			StatusMessage = "Nome ou usuário inválidos";
		}


		#endregion
	}
}
