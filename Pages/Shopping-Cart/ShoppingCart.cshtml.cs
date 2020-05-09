using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LojaVirtual.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LojaVirtual
{
    public class ShoppingCartModel : PageModel
    {
        #region Fields
        readonly LojaVirtualContext _lojaVirtualCtxt;
        #endregion

        #region Properties
        public ShoppingCart ShoppingCart { get; set; }
        #endregion

        #region MyRegion
        public ShoppingCartModel(LojaVirtualContext ctxt)
        {
            _lojaVirtualCtxt = ctxt; 
        } 
        #endregion

        public IActionResult OnGet()
        {
            if (!LoginManagement.IsLogged)
            {
                return RedirectToPage("/Index");
            }
            else
            {
                ShoppingCart = LoginManagement.TempUser.ShoppingCart; 
            }

            if (ShoppingCart == null )
            {

            }
            return Page();
        }
    }
}