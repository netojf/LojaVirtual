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

            Category = await _context
                .Categories
                .FirstOrDefaultAsync(c => c.CategoryId == id);
            Products = await _context.Products
                                    .Where(p => p.Category.CategoryId == id)
                                    .ToListAsync();

            if (Products == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAddItem(int product, int category)
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

            Product prod;

            using (LojaVirtualContext ctxt = new LojaVirtualContext())
            {
                prod = await ctxt.Products
                    .Where(x => x.ProductId == product)
                    .SingleOrDefaultAsync();

                var orders = LoginManagement.TempUser.ShoppingCart.ProductOrders;

                ProductOrder order = orders
                        .SingleOrDefault(o => o.Products
                        .Any(p => p.ProductId == prod.ProductId));

                if (order == null)
                {
                    order = new ProductOrder();
                    order.Products = new List<Product>();
                    order.Products.Add(prod);
                    orders.Add(order);
                }
                else
                {
                    if (order.Quantity == 0)
                    {
                        order.Quantity = 1; 
                    }
                    else
          {
            order.Quantity += 1;
          }

                }


                LoginManagement.TempUser.ShoppingCart.ProductOrders = orders; 

                ctxt.Entry(LoginManagement.TempUser);
                await ctxt.SaveChangesAsync();
            }
            //todo: redirecto to a temporary Message then to lastPage
            return RedirectToPage(new {id = category } ); 
        }
        #endregion
    }
}