using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LojaVirtual
{
    public class PaymentSucessModel : PageModel
    {
        public IActionResult OnGet()
        {
            Thread.Sleep(2000);
            //todo: Return to last Page
            return RedirectToPage("/Index");
        }
    }
}