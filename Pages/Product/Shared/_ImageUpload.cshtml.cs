using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LojaVirtual.Pages.Product.Shared
{
    public class _ImageUploadModel : PageModel
    {
        public void OnGet()
        {

        }

        public PartialViewResult OnPostMyPartial()
        {
            var part = new PartialViewResult()
            {
                ViewName = "Shared/_PartialTest"
            };


            return part;
        }
    }
}