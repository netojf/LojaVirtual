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
    public class _ProductCardModel : PageModel
    {
        [BindProperty]
        public Product Product { get; set; }
        public _ProductCardModel(Product product)
        {
            this.Product = product;
            using (LojaVirtualContext ctxt = new LojaVirtualContext())
            {
                Product.Images = ctxt.Images.Where(img => img.Product == Product).ToList();

            }
        }

        public void OnGet()
        {

        }

        public void OnPost()
        {

        }

        
    }
}