using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LojaVirtual.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LojaVirtual
{
	public class ShoppingCartModel : PageModel
	{
		#region Fields
		readonly LojaVirtualContext _lojaVirtualCtxt;
		#endregion

		#region Properties
		public ShoppingCart ShoppingCart { get; set; }
		#endregion

		#region Constructor
		public ShoppingCartModel(LojaVirtualContext ctxt)
		{
			_lojaVirtualCtxt = ctxt;
			ShoppingCart = LoginManagement.TempUser.ShoppingCart; 
		}
		#endregion

		#region Methods

		public IActionResult OnGet()
		{
			if (!LoginManagement.IsLogged)
			{
				return RedirectToPage("/Index");
			}
			return Page();
		}

		public async Task<IActionResult> OnPostAddOrder(int id)
		{
			var d = LoginManagement.TempUser.ShoppingCart.ProductOrders;
			var order = d.FirstOrDefault(po => po.Products.Any(pr => pr.ProductId == id));
			if (order.Quantity == 0)
			{
				order.Quantity = order.Products.Count;

			}
			order.Quantity++;
			LoginManagement.TempUser.ShoppingCart.ProductOrders = d;

			_lojaVirtualCtxt.Entry(LoginManagement.TempUser);
			await _lojaVirtualCtxt.SaveChangesAsync();
			return Page();
		}

		public async Task<IActionResult> OnPostSubtractOrder(int id)
		{

			var d = LoginManagement.TempUser.ShoppingCart.ProductOrders;
			var order = d.FirstOrDefault(po => po.Products.Any(pr => pr.ProductId == id));

			if (order.Quantity == 0)
			{
				order.Quantity = order.Products.Count;

			}

			if (order.Quantity > 1)
			{
				order.Quantity--;

			}

			LoginManagement.TempUser.ShoppingCart.ProductOrders = d;

			_lojaVirtualCtxt.Entry(LoginManagement.TempUser);
			await _lojaVirtualCtxt.SaveChangesAsync();
			return Page();


		}

		public async Task<IActionResult> OnPostDeleteOrder(int id)
		{

			var order = ((List<ProductOrder>)LoginManagement.TempUser.ShoppingCart.ProductOrders)[id];
			if (order != null)
			{
				LoginManagement.TempUser.ShoppingCart.ProductOrders.Remove(order);
			}


			_lojaVirtualCtxt.Entry(LoginManagement.TempUser);
			await _lojaVirtualCtxt.SaveChangesAsync();
			return Page();


		} 
		#endregion

	}
}