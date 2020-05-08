
using LojaVirtual.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LojaVirtual
{
    public class TestPageModel : PageModel
    {

        #region Fields
        private readonly LojaVirtual.Models.LojaVirtualContext _context;
        #endregion

        #region Properties
        
        public List<Product> Products { get; set; }
        public Category Category { get; set; }
        #endregion


        public TestPageModel(LojaVirtual.Models.LojaVirtualContext context)
        {
            this._context = context; 
        }

        public async Task<IActionResult> OnGet(int? id)
        {
            Category = await _context.Categories.FirstOrDefaultAsync(c => c.CategoryId == id);

            if (id == null || Category == null)
            {
                return NotFound();
            }

            Products = await _context.Products
                                    .Where(p => p.Category.CategoryId == id)
                                    .ToListAsync();

            if (Products == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}