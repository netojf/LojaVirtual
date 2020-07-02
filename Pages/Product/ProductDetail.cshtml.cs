using LojaVirtual.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LojaVirtual
{
	public class ProductDetailModel : PageModel
	{
		#region Fieds
		readonly LojaVirtualContext _lojaVirtualContext;
		#endregion


		#region Properties
		[BindProperty]
		public int Quantity { get; set; } = 1;
		public Product Product { get; set; }
		#endregion


		#region Constructor
		public ProductDetailModel(LojaVirtualContext lojaVirtualContext)
		{
			this._lojaVirtualContext = lojaVirtualContext;
		}
		#endregion


		#region Methods
		public async Task<IActionResult> OnPostAddItem(int product)
		{
			if (!LoginManagement.IsLogged)
			{
				return RedirectToPage("/User/Login");
			}


			if (LoginManagement.TempUser.ShoppingCart == null)
			{
				LoginManagement.TempUser.ShoppingCart = new ShoppingCart();
			}


			if (LoginManagement.TempUser.ShoppingCart.ProductOrders == null)
			{
				LoginManagement.TempUser.ShoppingCart.ProductOrders = new List<ProductOrder>();
			}

			Product prod;

			using (LojaVirtualContext ctxt = new LojaVirtualContext())
			{
				prod = await ctxt.Products
						.Where(x => x.ProductId == product)
						.SingleOrDefaultAsync();

				var orders = LoginManagement.TempUser.ShoppingCart.ProductOrders;

				ProductOrder order = orders
								.SingleOrDefault(o => o.Products
								.Any(p => p.ProductId == prod.ProductId));

				if (order == null)
				{
					order = new ProductOrder();
					order.Products = new List<Product>();
					order.Products.Add(prod);
					orders.Add(order);
				}
				else
				{
					if (order.Quantity == 0 && Quantity <= 0)
					{
						order.Quantity = 1;
					}
					else
					{
						order.Quantity += Quantity;
					}

				}


				LoginManagement.TempUser.ShoppingCart.ProductOrders = orders;

				ctxt.Entry(LoginManagement.TempUser);
				await ctxt.SaveChangesAsync();
			}
			//todo: redirecto to a temporary Message then to lastPage
			return RedirectToPage(new { id = product });
		}

		public async Task<IActionResult> OnPostBuyItem(int product)
		{
			if (!LoginManagement.IsLogged)
			{
				return RedirectToPage("/User/Login");
			}


			if (LoginManagement.TempUser.ShoppingCart == null)
			{
				LoginManagement.TempUser.ShoppingCart = new ShoppingCart();
			}


			if (LoginManagement.TempUser.ShoppingCart.ProductOrders == null)
			{
				LoginManagement.TempUser.ShoppingCart.ProductOrders = new List<ProductOrder>();
			}

			Product prod;

			using (LojaVirtualContext ctxt = new LojaVirtualContext())
			{
				prod = await ctxt.Products
						.Where(x => x.ProductId == product)
						.SingleOrDefaultAsync();

				var orders = LoginManagement.TempUser.ShoppingCart.ProductOrders;

				ProductOrder order = orders
								.SingleOrDefault(o => o.Products
								.Any(p => p.ProductId == prod.ProductId));

				if (order == null)
				{
					order = new ProductOrder();
					order.Products = new List<Product>();
					order.Products.Add(prod);
					orders.Add(order);
				}
				else
				{
					if (order.Quantity == 0 && Quantity <= 0)
					{
						order.Quantity = 1;
					}
					else
					{
						order.Quantity += Quantity;
					}

				}


				LoginManagement.TempUser.ShoppingCart.ProductOrders = orders;

				ctxt.Entry(LoginManagement.TempUser);
				await ctxt.SaveChangesAsync();
			}
			//todo: redirecto to a temporary Message then to lastPage
			return RedirectToPage("/Shopping-Cart/ShoppingCart");
		}

		public async Task<IActionResult> OnGet(int id)
		{
			Product = await _lojaVirtualContext.Products.Include(prod => prod.Images).Include(prod => prod.Category).FirstOrDefaultAsync(p => p.ProductId == id);
			if (Product == null)
			{
				return NotFound();
			}

			return Page();
		}
		#endregion
	}
}