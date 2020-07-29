using LojaVirtual.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using MecaDevToolkit;
using DocumentFormat.OpenXml.Office.CustomUI;
using System;

namespace LojaVirtual.Pages.Product
{
	public class ProductViewBase : ComponentBase
	{
		private Models.Product _product;
		private int _id =-1;
		private int _quantity;

		public EventCallback<Models.Product> ProductChanged { get; set; }
		public EventCallback<int?> IdChanged { get; set; }

		[Parameter]
		public int Id
		{
			get => _id; 
			set 
			{
				_id = value;

				using (LojaVirtualContext ctxt = new LojaVirtualContext())
				{
					_product = ctxt.Products
						.Include(p => p.Category)
						.Include(p => p.Images)
						.FirstOrDefault(p => p.ProductId == _id);
				}
			}
		}
		public Models.Product Product
		{
			get 
			{
				if(_product == null && _id != -1)
				{
					using (LojaVirtualContext ctxt = new LojaVirtualContext())
					{
						_product = ctxt.Products
							.Include(p => p.Category)
							.Include(p => p.Images)
							.FirstOrDefault(p => p.ProductId == _id);
					}
				}
				return _product; 
			}
			set
			{
				_product = value;
			}
		}
		public int Quantity { get=>_quantity; set=> _quantity = value; }

		protected void AddToCart()
		{

		}

		protected void Buy()
		{

		}

	}
}
