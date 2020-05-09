using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        }

        public void OnGet()
        {

        }

        public void OnPost()
        {

        }

        public async Task<IActionResult> AddShoppingCartItem()
        {
            if (!LoginManagement.IsLogged)
            {
                return RedirectToPage("/User/Login"); 
            }


            if (LoginManagement.TempUser.ShoppingCart == null)
            {
                LoginManagement.TempUser.ShoppingCart = new ShoppingCart();
            }


            if (LoginManagement.TempUser.ShoppingCart.Products == null)
            {
                LoginManagement.TempUser.ShoppingCart.Products = new List<Product>(); 
            }

            LoginManagement.TempUser.ShoppingCart.Products.Add(Product);

            using (LojaVirtualContext ctxt = new LojaVirtualContext())
            {
                ctxt.Users.Update(LoginManagement.TempUser);
                await ctxt.SaveChangesAsync(); 
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAddShoppingCartItem()
        {
            if (!LoginManagement.IsLogged)
            {
                return RedirectToPage("/User/Login");
            }


            if (LoginManagement.TempUser.ShoppingCart == null)
            {
                LoginManagement.TempUser.ShoppingCart = new ShoppingCart();
            }


            if (LoginManagement.TempUser.ShoppingCart.Products == null)
            {
                LoginManagement.TempUser.ShoppingCart.Products = new List<Product>();
            }

            LoginManagement.TempUser.ShoppingCart.Products.Add(Product);

            using (LojaVirtualContext ctxt = new LojaVirtualContext())
            {
                ctxt.Users.Update(LoginManagement.TempUser);
                await ctxt.SaveChangesAsync();
            }
            return Page();
        }
    }
}