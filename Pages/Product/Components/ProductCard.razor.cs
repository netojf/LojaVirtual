using LojaVirtual.Models;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;

namespace LojaVirtual.Pages.Product.Components
{
	public class ProductCardBase : ComponentBase
	{
		Models.Product _product;
		string _status = "";

		[Inject]
		public NavigationManager NavigationManager { get; set; }

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
			}
		}
		[Parameter]
		public EventCallback<Models.Product> ProductChanged { get; set; }

		public string Status { get => _status; set => _status = value; }

		

		public void AddProductToCart()
		{

			if (LoginManagement.IsLogged)
			{
				using (LojaVirtualContext ctxt = new LojaVirtualContext())
				{

					try
					{
						LoginManagement.TempUser.ShoppingCart ??= new ShoppingCart();
						LoginManagement.TempUser.ShoppingCart.ProductOrders ??= new List<ProductOrder>();
						var orders = LoginManagement.TempUser.ShoppingCart.ProductOrders;

						//returns a existing order if it has the product in it
						ProductOrder order = orders
														.SingleOrDefault(o => o.Products
														.Any(p => p.ProductId == Product.ProductId));

						if (order == null)
						{
							//creates a new order and add the product to it
							order = new ProductOrder();
							order.Products = new List<Models.Product>();
							order.Products.Add(Product);
							orders.Add(order);
						}
						else
						{
							//verify if quantity is 0 and sums one more product to the order
							if (order.Quantity == 0)
							{
								order.Quantity = 1;
							}
							else
							{
								order.Quantity += 1;
							}
							ctxt.productOrders.Update(order);
						}


						LoginManagement.TempUser.ShoppingCart.ProductOrders = orders;

						//ctxt.Entry(LoginManagement.TempUser);
						ctxt.Update(LoginManagement.TempUser);
						ctxt.SaveChanges();


						Status = "Success";
					}
					catch (System.Exception ex)
					{

						Status = ex.Message; 
					}
				}
			}
			else
			{
				Status = "LoginError";
			}
		}
		public void RedirectToProduct() => 
			NavigationManager.NavigateTo(string.Format("Product/ProductView/{0}", Product.ProductId), true);
		public void DialogClose()
		{
			Status = "";
		}
	}


}
