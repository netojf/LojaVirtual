using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LojaVirtual.Pages.Product.Components
{
  public partial class ProductFormBase : ComponentBase
  {
    [Inject]
    NavigationManager NavigationManager { get; set; }

    [Parameter]
    public Models.Product Product { get; set; } = new Models.Product();
    [Parameter]
		public EventCallback<Models.Product> ProductChange { get; set; }

		protected readonly List<Models.Category> categories;

    protected int catId;

    protected void ProductImagePop()
    {

    }

    public ProductFormBase()
    {
      using (Models.LojaVirtualContext ctxt = new Models.LojaVirtualContext())
      {
        categories = ctxt.Categories.ToList();
      }
    }


    protected void SelectCategory(ChangeEventArgs e)
    {
      catId = int.Parse((string)e.Value);
    }


    protected void SubmitForm()
    {
      if (Product == null)
      {
        return;
      }

      foreach (var image in Product.Images)
      {
        image.Product = Product;
      }

      if (Product.ProductImage == null)
      {
        return;
      }

      using (Models.LojaVirtualContext ctxt = new Models.LojaVirtualContext())
      {

        foreach (var image in Product.Images)
        {
          ctxt.Images.Add(image);
        }
        Product.Category =
       ctxt.Categories.FirstOrDefault(c => c.CategoryId == catId);
        //Product.Category = cat;

        //Product.Category.CategoryId = default;
        //if (cat.Products == null)
        //{
        //  cat.Products = new List<Models.Product>();
        //}
        //cat.Products.Add(Product);

        if (Product.Category != null)
        {
          ctxt.Categories.Update(Product.Category);

        }
        else return;

        if (Product.ProductId != 0)
        {
          ctxt.Products.Update(Product);

        }
        else
        {
          ctxt.Products.Add(Product);
        }


        ctxt.SaveChanges();
      }
      NavigationManager.NavigateTo("/Product/Index", true);
    }
  }
}
