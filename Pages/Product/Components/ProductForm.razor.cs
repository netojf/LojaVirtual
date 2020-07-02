using LojaVirtual.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

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
		private int _productId;
		#endregion


		#region Properties
		[Inject]
		NavigationManager NavigationManager { get; set; }

		[Parameter]
		public int ProductId
		{
			get => _productId;
			set
			{
				using (Models.LojaVirtualContext ctxt = new LojaVirtualContext())
				{
					Product = ctxt.Products.Include(p => p.Images).Include(p => p.Category).FirstOrDefault(p => p.ProductId == value);
				}
				_productId = value;
				ProductIdChange.InvokeAsync(value);
				//StateHasChanged(); 
			}
		}


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
				if (value != _product && value != null)
				{
					_product = value;
					if (Product.Category != null && Product.Category != ProductCategory) ProductCategory = Product.Category;
					if (Product.Images != null && Product.Images != ProductImages) ProductImages = Product.Images;

				}

			}
		}

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

				if (Product.Category != value)
				{
					_category = value != null && value != new Models.Category() ? value : categories[0];
					if (Product.Category != _category) Product.Category = _category;
				}
			}
		}

		public ICollection<Models.Image> ProductImages
		{
			get
			{
				_productImages ??= new List<Models.Image>();
				return _productImages;
			}
			set
			{
				_productImages = value != null && value != new List<Models.Image>() ? value : new List<Models.Image>();
				if (Product.Images != _productImages) Product.Images = _productImages;

				List<ImageData> tempImgData = new List<ImageData>();

				foreach (var image in ProductImages)
				{
					string byte64 = Convert.ToBase64String(image.Data);
					int extensionIndex = image.ImageName.IndexOf('.') + 1;
					string extension = image.ImageName.Remove(0, extensionIndex);
					tempImgData.Add(new ImageData() { name = image.ImageName, string64Data = new string("data:image/" + extension + "; base64," + byte64) });
				}

				if (ImagesData != tempImgData) ImagesData = tempImgData;
			}
		}

		public List<ImageData> ImagesData
		{
			get
			{
				_imagesData ??= new List<ImageData>();
				return _imagesData;
			}
			set
			{
				_imagesData = value != null && value != new List<ImageData>() ? value : new List<ImageData>();

				if (_imagesData != value && value != null)
				{
					_imagesData = value;
				}

			}
		}
		#endregion


		#region Events and Delegates
		[Parameter]
		public EventCallback<int> ProductIdChange { get; set; }
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
					if (ctxt.Images.Any(i => i == image))
					{
						ctxt.Images.Update(image);
					}
					else
					{
						ctxt.Images.Add(image);
					}
				}

				//updates category if the product (checks if existent and add if new)
				if (Product.Category != null)
				{
					ctxt.Categories.Update(Product.Category);
				}
				//todo: return error
				else return;

				//update if product has already a id
				if (ctxt.Products.Any(p => p == Product))
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
