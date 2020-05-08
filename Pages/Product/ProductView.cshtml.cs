using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LojaVirtual.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace LojaVirtual
{
    public class ProductViewModel : PageModel
    {
        #region Fields
        private readonly LojaVirtual.Models.LojaVirtualContext _context; 
        #endregion


        #region Properties
        public List<Product> Products { get; set; }
        public Category Category { get; set; }
        #endregion


        #region Constructor
        public ProductViewModel(LojaVirtual.Models.LojaVirtualContext context)
        {
            _context = context;
        }
        #endregion


        #region Methods
        public async Task<IActionResult> OnGet(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Category = await _context.Categories.FirstOrDefaultAsync(c => c.CategoryId == id);
            Products = await _context.Products
                                    .Where(p => p.Category.CategoryId == id)
                                    .ToListAsync();

            if (Products == null)
            {
                return NotFound();
            }

            ModelState.Clear();
            return Page();
        }

        public async Task<IActionResult> OnGetPopulate(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Category = await _context.Categories.FirstOrDefaultAsync(c => c.CategoryId == id);
            Products = await _context.Products
                                    .Where(p => p.Category.CategoryId == id)
                                    .ToListAsync();

            if (Products == null)
            {
                return NotFound();
            }

            return Page();
        }
        #endregion
    }
}