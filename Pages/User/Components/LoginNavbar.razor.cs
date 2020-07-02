using Microsoft.AspNetCore.Components;

namespace LojaVirtual.Pages.User.Components
{
	public partial class LoginNavbarBase : ComponentBase
	{
		[Inject]
		NavigationManager _navigationManager { get; set; }
		public static LoginModal LoginModalInstance { get; set; }


		protected void Logoff()
		{
			LoginManagement.Logoff();
			base.InvokeAsync(StateHasChanged);
		}

		protected void Account()
		{
			_navigationManager.NavigateTo(_navigationManager.BaseUri, true);
		}

		protected void ShoppingCart()
		{
			_navigationManager.NavigateTo("/Shopping-Cart/ShoppingCart", true);

		}
	}
}
