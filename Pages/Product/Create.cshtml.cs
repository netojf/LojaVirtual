using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using LojaVirtual.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Drawing;
using System.Threading;
using DocumentFormat.OpenXml.EMMA;

namespace LojaVirtual.Pages.Product
{
    public class CreateModel : PageModel 
    {
        #region Fields
        private readonly LojaVirtual.Models.LojaVirtualContext _context;
        private IWebHostEnvironment _environment;
        public delegate void TestDelegate(string text);
        #endregion

        #region Properties

        [BindProperty]
        public Models.Product Product { get; set; }

        [BindProperty]
        List<Models.Product> listproducts { get; set; } = new List<Models.Product>();

        [BindProperty]
        public IFormFile Upload { get; set; }

        #endregion

        #region constructor
        public CreateModel(LojaVirtual.Models.LojaVirtualContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
            Product = new Models.Product();

        }
        #endregion

        #region Methods



        public IActionResult OnGet()
        {
            return Page();
        }



        //To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            Models.Image image = new Models.Image();

            ////Unique filename
            var uniqueFileName = Guid.NewGuid().ToString();
            var fileName = Path.GetFileName(uniqueFileName + "." + Upload.FileName.Split(".")[1].ToLower());


            if (image.Data == null)
            {
                image.Data = new byte[Upload.Length+1];
            }

            using (var memoryStream = new MemoryStream((int)Upload.Length))
            {
                await Upload.CopyToAsync(memoryStream);
                image.Data = memoryStream.ToArray();
            }


            if (Product.Images == null)
            {
                Product.Images = new List<Models.Image>();
            }

            Product.Images.Add(image);

            if (Product.ProductImage == null)
            {
                Product.ProductImage = 0; 
            }


            image.ImageName = fileName;

            image.Product = Product;

            if (Product.ProductImage == null)
            {
                Product.ProductImage = 0;
            }

            if (Product.Images == null)
            {
                Product.Images = new List<Models.Image>();
            }

            string byte64 = Convert.ToBase64String(image.Data);

            image.Product = Product;
            

            return RedirectToPage("./Index");
        }

        public PartialViewResult OnPostMyPartial()
        
        {
            var part = new PartialViewResult()
            {
                ViewName = "Shared/_PartialTest"
            };
            return part; 
        }


        public void OnGetUpdateValue()
        {

        }

        public void OnPostUpdateValue()
        {

        }
        #endregion



    }
}
