using Microsoft.AspNetCore.Components;

namespace LojaVirtual.Pages.Product.Components
{
	public class ProductCardBase : ComponentBase
	{
		Models.Product _product;
		string _status = "";

		[Parameter]
		public Models.Product Product
		{
			get
			{
				_product ??= new Models.Product();
				return _product;
			}
			set
			{
				_product = value;
				ProductChanged.InvokeAsync(value);
			}
		}


		public string Status { get => _status; set => _status = value; }

		[Inject]
		public NavigationManager NavigationManager { get; set; }

		[Parameter]
		public EventCallback<Models.Product> ProductChanged { get; set; }

		public void AddProductToCart()
		{
			if (LoginManagement.IsLogged)
			{
				Status = "Success";

			}
			else
			{
				Status = "LoginError";
			}

		}

		public void RedirectToProduct()
		{
			NavigationManager.NavigateTo(string.Format("Product/ProductDetail/Get?id={0}", Product.ProductId), true);
		}

		public void DialogClose()
		{
			Status = "";
		}
	}


}
