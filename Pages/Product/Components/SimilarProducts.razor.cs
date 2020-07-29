using LojaVirtual.Models;
using MecaDevToolkit;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LojaVirtual.Pages.Product.Components
{
	public class SimilarProductsBase : ComponentBase
	{
		private List<Models.Product> _similarProducts ;
		private int _maxNumber = 9; 

		/*
		* 1 - passar o produto para o component
		* 2 - no método OnInitialize selecionar produtos similares 
		* 3 - permitir que seja feito o template
		*/

		/// <summary>
		/// List with Product Data Content of the similar data to target
		/// </summary>
		public List<Models.Product> SimilarProducts { get => _similarProducts; set => _similarProducts = value; }
		/// <summary>
		/// Target Product to get similar data
		/// </summary>
		[Parameter]
		public Models.Product Product { get; set; }
		/// <summary>
		/// Sets the max number of products to show
		/// </summary>
		[Parameter]
		public int MaxNumber { get=>_maxNumber; set=> _maxNumber = value; }


		[Parameter]
		public RenderFragment<List<Models.Product>> ContentTemplate { get; set; }
		[Parameter]
		public RenderFragment EmptyListTemplate { get; set; }
		[Parameter]
		public int LoadingTemplate { get; set; }

		protected override void OnInitialized()
		{
			if (Product == null)
			{
				throw new ArgumentNullException(nameof(Product), "The Product Argument can not be null");
			}

			if (Product?.Category == null)
			{
				throw new ArgumentNullException(nameof(Product.Category), "The Product Category can not be null");
			}

			using (LojaVirtualContext ctx = new LojaVirtualContext())
			{
				string[] substrings = Product.Name.Split(new char[0]);

				//getting data from database
				List<Models.Product> productlist = (ctx.Set<Models.Category>()
					.Include(c => c.Products)
					.ThenInclude(p => p.Images)
					.SingleOrDefault(c => c == Product.Category))
					.Products
					.TakeWhile(p => p.Name.Contains(substrings.ToList()) && p.ProductId != p.ProductId)
					.Take(MaxNumber)
					.ToList();

				if (productlist.Count < MaxNumber)
				{
					int dif = MaxNumber - productlist.Count;

					List<Models.Product> complement = (ctx.Set<Models.Category>()
					.Include(c => c.Products)
					.ThenInclude(p => p.Images)
					.SingleOrDefault(c => c == Product.Category))
					.Products
					.Take(dif)
					.ToList();

					productlist.Concat(complement); 
				}

				List<string> names = productlist.Select(p => p.Name).ToList();

				Dictionary<int, string> similars = names.GetSimilar(Product.Name);


				//completes the number of similars to the value of the Max Number with another products from category
				if (similars.Count < MaxNumber)
				{
					foreach (var item in productlist)
					{
						try
						{
							similars.Add(productlist.IndexOf(item), item.Name);
						}
						catch (ArgumentException)
						{

							continue;
						}
						if (similars.Count == MaxNumber)
						{
							break;
						}
					}
				}

				//Withdraw the current product from similars list
				if (similars.ContainsKey(productlist.IndexOf(Product)))
				{
					similars.Remove(productlist.IndexOf(Product));
				}

				//Removes exceding values from similars until reaches the MaxNumber Property
				if (similars.Count > MaxNumber)
				{
					for (int i = similars.Count; i == MaxNumber; i--)
					{
						similars.Remove(similars.Keys.Last());
					}
				}
				SimilarProducts = new List<Models.Product>();
				//Adds all similar data to the SimilarProducts List
				foreach (var item in similars)
				{
					SimilarProducts.Add(ctx.Products.ToList()[item.Key]); 
				}
			}

			base.OnInitialized();
		}
	}
}
