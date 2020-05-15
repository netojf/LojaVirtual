using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LojaVirtual.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LojaVirtual
{
    public class RegisterModel : PageModel
    {
        #region fields
        private readonly LojaVirtual.Models.LojaVirtualContext _context;

        #endregion

        #region Properties
        [BindProperty]
        public User ModelUser { get; set; }
        #endregion

        #region constructor
        public RegisterModel(LojaVirtualContext lojaVirtualContext)
        {
            _context = lojaVirtualContext;
        }
        #endregion

        public void OnGet()
        {
            
        }
        
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Users.Add(ModelUser);

            try
            {
                await _context.SaveChangesAsync();
                await LoginManagement.LoginAsync(ModelUser.Name, ModelUser.Password);

            }
            catch (Exception)
            {
                ModelState.TryAddModelError("", "Something gone wrong");
                return Page(); 
            }

            //todo: Redirect to previous Page
            return RedirectToPage("/index");
        }
    }
}