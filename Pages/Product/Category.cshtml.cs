using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LojaVirtual.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace LojaVirtual.Pages.Product
{
	public class CategoryModel : PageModel
	{
		#region Fields
		private readonly LojaVirtual.Models.LojaVirtualContext _context;
		int currentPage; 
		#endregion


		#region Properties
		public List<Models.Product> Products { get; set; }
		public Models.Category Category { get; set; }
		#endregion


		#region Constructor
		public CategoryModel(LojaVirtual.Models.LojaVirtualContext context)
		{
			_context = context;
			currentPage = 0; 
		}
		#endregion


		#region Methods
		public async Task<IActionResult> OnGet(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			Category = await _context
				.Categories
				.FirstOrDefaultAsync(c => c.CategoryId == id);

			Products = await _context.Products.Include(p => p.Images.First())
				.Where(p => p.Category.CategoryId == id).Take(2)
															.ToListAsync();


			if (Products == null)
			{
				return NotFound();
			}

			return Page();
		}

		public async Task<IActionResult> OnPostAddItem(int prodId, int catId)
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

			Models.Product prod;

			using (LojaVirtualContext ctxt = new LojaVirtualContext())
			{
				prod = await ctxt.Products.SingleOrDefaultAsync(x => x.ProductId == prodId);

				var orders = LoginManagement.TempUser.ShoppingCart.ProductOrders;

				//returns a existing order if it has the product in it
				ProductOrder order = orders
												.SingleOrDefault(o => o.Products
												.Any(p => p.ProductId == prod.ProductId));
				

				if (order == null)
				{
					//creates a new order and add the product to it
					order = new ProductOrder();
					order.Products = new List<Models.Product>();
					order.Products.Add(prod);
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

				ctxt.Entry(LoginManagement.TempUser);
				await ctxt.SaveChangesAsync();
			}
			//todo: redirecto to a temporary Message then to lastPage
			return RedirectToPage("Category","OnGet", new { id = catId }); 
		}
		#endregion
	}
}