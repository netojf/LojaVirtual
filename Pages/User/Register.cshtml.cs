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

        #region Methods
        public IActionResult OnGet()
        {
            if (LoginManagement.IsLogged)
            {
                if (LoginManagement.TempUser.IsAdmin!)
                {
                    return RedirectToPage("/Index");
                }
            }
            return Page();
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
                if (!LoginManagement.TempUser.IsAdmin)
                {
                    await LoginManagement.LoginAsync(ModelUser.Name, ModelUser.Password);
                }

            }
            catch (Exception ex)
            {
                ModelState.TryAddModelError("", ex.Message);
                return Page();
            }

            //todo: Redirect to previous Page
            return RedirectToPage("/index");
        } 
        #endregion
    }
}