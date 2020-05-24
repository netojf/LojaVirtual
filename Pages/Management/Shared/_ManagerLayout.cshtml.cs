using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LojaVirtual.Pages.Management.Shared
{
    public class _ManagerLayoutModel : PageModel
    {
    public _ManagerLayoutModel()
    {


      checkuser();
    }

    public IActionResult checkuser()
    {
      if (LoginManagement.IsLogged!)
      {
        RedirectToPage("~/Index");
      }
      else
      {
        if (LoginManagement.TempUser == null)
        {
          RedirectToPage("~/Index");
        }
        if (LoginManagement.TempUser.IsAdmin!)
        {
          RedirectToPage("~/Index");
        }

      }
      return Page(); 
    }


		public void OnGet()
        {

        }
    }
}