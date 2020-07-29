using LojaVirtual.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MecaDevToolkit;

namespace LojaVirtual.Tools
{
	public static class CollectionTools
	{

		//TODO: Make this generic
		public static List<Models.Product> GetSimilarData(this Models.Product Product, int MaxNumber)
		{
			using (LojaVirtualContext ctx = new LojaVirtualContext())
			{
				List<string> substrings = Product.Name.Split(new char[0]).ToList();

				//getting data from database
				List<Models.Product> productlist = (ctx.Set<Models.Category>()
					.Include(c => c.Products)
					.ThenInclude(p => p.Images)
					.SingleOrDefault(c => c == Product.Category))
					.Products
					.TakeWhile(p => p.Name.Contains(substrings) && p.ProductId != p.ProductId)
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
					.Where(p => p.ProductId != Product.ProductId)
					.Take(dif)
					.ToList();

					productlist.AddRange(complement);
				}
				return productlist; 
			}
		}
	}

}
