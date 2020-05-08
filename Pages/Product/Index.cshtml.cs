using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using LojaVirtual.Models;

namespace LojaVirtual
{
    public class IndexModel : PageModel
    {
        private readonly LojaVirtual.Models.LojaVirtualContext _context;

        public IndexModel(LojaVirtual.Models.LojaVirtualContext context)
        {
            _context = context;
        }

        public IList<Product> Product { get;set; }

        public async Task OnGetAsync()
        {
            Product = await _context.Products.ToListAsync();
        }
    }
}
