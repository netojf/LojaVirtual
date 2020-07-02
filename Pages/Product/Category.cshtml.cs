using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace LojaVirtual.Pages.Product
{
	public class CategoryModel : PageModel
	{
		#region Fields
		private readonly LojaVirtual.Models.LojaVirtualContext _context;
		int? _categoryId;
		#endregion


		#region Properties
		public List<Models.Product> Products { get; set; }
		public Models.Category Category { get; set; }
		public int? CategoryId
		{
			get
			{
				if (_categoryId == null)
				{
					NotFound();
					return _categoryId;
				}
				else
				{
					return _categoryId;

				}
			}
			set
			{
				_categoryId = value;
			}
		}
		#endregion


		#region Constructor
		public CategoryModel(LojaVirtual.Models.LojaVirtualContext context)
		{
			_context = context;

			//currentPage = 0; 
		}
		#endregion


		#region Methods
		public IActionResult OnGet(int? catId)
		{
			if (catId == null)
			{
				return NotFound();
			}


			CategoryId = catId;
			//using (_context)
			//{
			//	Products = _context.Products
			//		.Include(p => p.Category)
			//		.Include(p => p.Images)
			//		.Where(p => p.Category.CategoryId == CategoryId).ToList();
			//}
			return Page();
		}

		//public async Task<IActionResult> OnPostAddItem(int prodId, int catId)
		//{

		//	if (!LoginManagement.IsLogged)
		//	{
		//		return RedirectToPage("/User/Login");
		//	}

		//	if (LoginManagement.TempUser.ShoppingCart == null)
		//	{
		//		LoginManagement.TempUser.ShoppingCart = new ShoppingCart();
		//	}

		//	if (LoginManagement.TempUser.ShoppingCart.ProductOrders == null)
		//	{
		//		LoginManagement.TempUser.ShoppingCart.ProductOrders = new List<ProductOrder>();
		//	}

		//	Models.Product prod;

		//	using (LojaVirtualContext ctxt = new LojaVirtualContext())
		//	{
		//		prod = await ctxt.Products.SingleOrDefaultAsync(x => x.ProductId == prodId);

		//		var orders = LoginManagement.TempUser.ShoppingCart.ProductOrders;

		//		//returns a existing order if it has the product in it
		//		ProductOrder order = orders
		//										.SingleOrDefault(o => o.Products
		//										.Any(p => p.ProductId == prod.ProductId));


		//		if (order == null)
		//		{
		//			//creates a new order and add the product to it
		//			order = new ProductOrder();
		//			order.Products = new List<Models.Product>();
		//			order.Products.Add(prod);
		//			orders.Add(order);
		//		}
		//		else
		//		{
		//			//verify if quantity is 0 and sums one more product to the order
		//			if (order.Quantity == 0)
		//			{
		//				order.Quantity = 1;
		//			}
		//			else
		//			{
		//				order.Quantity += 1;
		//			}
		//			ctxt.productOrders.Update(order);
		//		}


		//		LoginManagement.TempUser.ShoppingCart.ProductOrders = orders;

		//		ctxt.Entry(LoginManagement.TempUser);
		//		await ctxt.SaveChangesAsync();
		//	}

		//	return RedirectToPage("Category", "OnGet", new { id = catId });
		//}
		#endregion
	}
}