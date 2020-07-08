using LojaVirtual.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LojaVirtual.Pages.Product
{
	public class ProductListBase : ComponentBase
	{
		int _categoryId;
		static int _currentPage = 0;
		static int _numberOfItems = 10;
		static List<Models.Product> _currentItems;
		private static int _numberOfPages = 0;
		private string _message = "Carregando..."; 

		[Parameter]
		public int CategoryId
		{
			get => _categoryId;
			set 
			{
				if (value != _categoryId)
				{
					_categoryId = value;
					CurrentItems = null;
					_message = "Carregando..."; 
				}
				try
				{
					if (CurrentItems.Count == 0)
					{
						Task.Run(LoadData);
					}
				}
				catch(NullReferenceException)
				{
					CurrentItems = new List<Models.Product>();
					Task.Run(LoadData);
				}
			}
		}
		protected string Message 
		{ 
			get => _message; 
			set => _message = value;  
		}
		protected static List<Models.Product> CurrentItems{ get => _currentItems; set => _currentItems = value; } 
		protected static int NumberOfItems{get => _numberOfItems;private set => _numberOfItems = value;}
		protected static int CurrentPage{get => _currentPage;private set => _currentPage = value;}
		protected static int NumberOfPages { get => _numberOfPages; set => _numberOfPages = value; }

		void LoadData()
		{
			if (CurrentItems == null || CurrentItems.Count == 0)
			{
				try
				{
					using (LojaVirtualContext ctxt = new LojaVirtualContext())
					{
						CurrentItems = ctxt.Products
							.Include(p => p.Images)
							.Include(p => p.Category)
							.Where(p => p.Category.CategoryId == CategoryId)
							.Skip(_currentPage * _numberOfItems)
							.Take(_numberOfItems)
							.ToList();

						int total = (from p in ctxt.Products
												 where p.Category.CategoryId == CategoryId
												 select p).Count();

						NumberOfPages = (total / _numberOfItems) + 1;

						if(CurrentItems.Count == 0)	Message = "Vazio"; 
					}
				}
				catch (Exception ex)
				{
					Message = ex.Message;
				}
				finally
				{
					InvokeAsync(StateHasChanged);
				}
			}
		}


		public void PageChangeAsync(int pageNumber)
		{
			CurrentPage = pageNumber;
			LoadData(); 
		}
	}

}
