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
    public class ProductDetailModel : PageModel
    {
        #region Fieds
        readonly LojaVirtualContext _lojaVirtualContext; 
        #endregion


        #region Properties
        public Product Product { get; set; }
        #endregion


        #region Constructor
        public ProductDetailModel(LojaVirtualContext lojaVirtualContext)
        {
            this._lojaVirtualContext = lojaVirtualContext;
        }
        #endregion


        #region Methods
        public async Task<IActionResult> OnGet(int id)
        {
            Product = await _lojaVirtualContext.Products.FirstOrDefaultAsync(p => p.ProductId == id);
            if (Product == null)
            {
                return NotFound();
            }

            return Page(); 
        } 
        #endregion
    }
}