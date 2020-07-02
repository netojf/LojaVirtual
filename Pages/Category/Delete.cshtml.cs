using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace LojaVirtual.Pages.Category
{
	public class DeleteModel : PageModel
	{
		private readonly LojaVirtual.Models.LojaVirtualContext _context;

		public DeleteModel(LojaVirtual.Models.LojaVirtualContext context)
		{
			_context = context;
		}

		[BindProperty]
		public Models.Category Category { get; set; }

		public async Task<IActionResult> OnGetAsync(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			Category = await _context.Categories.FirstOrDefaultAsync(m => m.CategoryId == id);

			if (Category == null)
			{
				return NotFound();
			}
			return Page();
		}

		public async Task<IActionResult> OnPostAsync(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			Category = await _context.Categories.FindAsync(id);

			if (Category != null)
			{
				_context.Categories.Remove(Category);
				await _context.SaveChangesAsync();
			}

			return RedirectToPage("./Index");
		}
	}
}
