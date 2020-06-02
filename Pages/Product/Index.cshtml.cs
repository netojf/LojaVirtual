using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using LojaVirtual.Models;

namespace LojaVirtual.Pages.Product
{
	public class IndexModel : PageModel
	{
		private readonly LojaVirtual.Models.LojaVirtualContext _context;

		public IndexModel(LojaVirtual.Models.LojaVirtualContext context)
		{
			_context = context;
		}

		public IList<Models.Product> Products { get; set; }

		public async Task OnGetAsync()
		{
			Products = await _context.Products.ToListAsync();
		}

		public async Task<IActionResult> OnPostDeleteAsync(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}
			Models.Product _product;

			_product = await _context.Products.Include(p => p.Images).FirstAsync(p=> p.ProductId == id);

			if (_product != null)
			{
				if (_product.Images!= null)
				{
					if (_product.Images.Count > 0)
					{
						foreach (var image in _product.Images)
						{
							_context.Images.Remove(image);
						}
					}
				}
				_context.Products.Remove(_product);
				await _context.SaveChangesAsync();
			}

			return RedirectToPage("Index");
		}
	}
}
