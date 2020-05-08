using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LojaVirtual
{
    public class _LoginPartialModel : PageModel
    {


        public _LoginPartialModel()
        {

        }

        public void OnGet()
        {

        }

        public void OnPost()
        {

        }

        public IActionResult Logoff()
        {
            LoginManagement.Logoff();
            return RedirectToPage("../Index"); 
        }

        public IActionResult OnPostLogoff()
        {
            HttpContext.Session.Remove(LoginManagement.TempUser.Name); 
            LoginManagement.Logoff();
            return RedirectToPage("../Index");
        }
    }
}