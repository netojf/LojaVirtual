using System.Linq;
using LojaVirtual.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LojaVirtual
{
	public class _ProductCardModel : PageModel
    {
        [BindProperty]
        public Product Product { get; set; }
        public _ProductCardModel(Product product)
        {
            this.Product = product;
            //using (LojaVirtualContext ctxt = new LojaVirtualContext())
            //{
            //    Product.Images = ctxt.Images.Where(img => img.Product == Product).ToList();
            //}
        }

        public void OnGet()
        {

        }

        public void OnPost()
        {

        }

        
    }
}