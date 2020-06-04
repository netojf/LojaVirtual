using LojaVirtual.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LojaVirtual.Pages.Product.Components
{
  public partial class ProductFormBase : ComponentBase
  {
		#region Fields
		Models.Product _product;
		Models.Category _category;
		protected readonly List<Models.Category> categories;
		protected int catId;
		private ICollection<Image> _productImages;
		List<ImageData> _imagesData; 
		#endregion


		#region Properties
		[Inject]
		NavigationManager NavigationManager { get; set; }

		[Parameter]
		public Models.Product Product
		{
			get
			{
				if (_product == null)
				{
					_product = new Models.Product();
				}
				return _product;
			}
			set
			{
				_product = value;
				ProductChange.InvokeAsync(value);
			}
		}

		[Parameter]
		public Models.Category ProductCategory
		{
			get
			{
				_category ??= new Models.Category();
				return _category;
			}
			set
			{

				_category = value;

				if (Product.Category != value) Product.Category = value;
				Product.Category ??= categories[0]; 

				CategoryChange.InvokeAsync(_category);
			}
		} 

		[Parameter]
		public ICollection<Models.Image> ProductImages
		{
			get
			{
				_productImages ??= new List<Models.Image>();
				return _productImages; 
			}
			set
			{
				if (_productImages != value && value != null)
				{
					_productImages = value;
					foreach (var image in ProductImages)
					{
						string byte64 = Convert.ToBase64String(image.Data);
						int extensionIndex = image.ImageName.IndexOf('.') + 1;
						string extension = image.ImageName.Remove(0, extensionIndex);
						ImagesData.Add(new ImageData() { name = image.ImageName, string64Data = new string("data:image/" + extension + "; base64," + byte64) });
					}
				}
				ProductImagesChange.InvokeAsync(_productImages);
			}
		}

		[Parameter]
		public List<ImageData> ImagesData
		{
			get
			{
				_imagesData ??= new List<ImageData>();
				return _imagesData; 
			}
			set
			{

				if (_imagesData != value && value != null)
				{
					_imagesData = value;

					ImagesDataChange.InvokeAsync(_imagesData);
				}
 
			}
		}
		#endregion


		#region Events and Delegates
		[Parameter]
		public EventCallback<List<ImageData>> ImagesDataChange { get; set; }
		[Parameter]
		public EventCallback<Models.Product> ProductChange { get; set; }
		[Parameter]
		public EventCallback<Models.Category> CategoryChange { get; set; }
		[Parameter]
		public EventCallback<ICollection<Models.Image>> ProductImagesChange { get; set; }
		#endregion


		#region ctor
		public ProductFormBase()
		{
			using (Models.LojaVirtualContext ctxt = new Models.LojaVirtualContext())
			{
				categories = ctxt.Categories.ToList();
			}
		} 
		#endregion


		#region Methods

		protected void ProductImagePop()
		{

		}

		protected void SubmitForm()
		{

			foreach (var image in Product.Images)
			{
				image.Product = Product;
			}

			using (Models.LojaVirtualContext ctxt = new Models.LojaVirtualContext())
			{

				foreach (var image in Product.Images)
				{
					ctxt.Images.Add(image);
				}

				Product.Category = ctxt.Categories.FirstOrDefault(c => c.CategoryId == catId);

				//updates category if the product (checks if existent and add if new)
				if (Product.Category != null)
				{
					ctxt.Categories.Update(Product.Category);
				}
				else return;

				//update if product has already a id
				if (Product.ProductId != 0)
				{
					ctxt.Products.Update(Product);

				}
				//add a new product if the id is new
				else
				{
					ctxt.Products.Add(Product);
				}

				ctxt.SaveChanges();
			}
			NavigationManager.NavigateTo("/Product/Index", true);
		} 

		#endregion

	}
}
