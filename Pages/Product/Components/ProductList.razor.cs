using LojaVirtual.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LojaVirtual.Pages.Product.Components
{
	public class ProductListBase : ComponentBase
	{
		int _categorId;
		static int _currentPage = 0;
		static int _numberOfItems = 10;
		static List<Models.Product> _currentItems;

		[Parameter]
		public int CategoryId
		{
			get => _categorId;
			set { _categorId = value; CategoryIdChanged.InvokeAsync(value); }
		}


		protected static List<Models.Product> CurrentItems
		{
			get => _currentItems;
			//verificar atualização 
			set { _currentItems = value; CurrentItemsChanged.InvokeAsync(value); }
		}

		public static int NumberOfItems
		{
			get => _numberOfItems;
			private set => _numberOfItems = value;
		}
		public static int CurrentPage
		{
			get => _currentPage;
			private set => _currentPage = value;
		}
		public static int NumberOfPages { get; set; } = 0;

		[Parameter]
		public EventCallback<int> CategoryIdChanged { get; set; }
		public static EventCallback<List<Models.Product>> CurrentItemsChanged { get; set; }

		protected override void OnInitialized()
		{
			Task.Run(LoadData);
			base.OnInitialized();
		}


		void LoadData()
		{
			if (CurrentItems == null || CurrentItems.Count == 0)
			{
				PageChange(CurrentPage);

				using (LojaVirtualContext ctxt = new LojaVirtualContext())
				{
					int total = (from p in ctxt.Products
											 where p.Category.CategoryId == CategoryId
											 select p).Count();

					NumberOfPages = (total / _numberOfItems) + 1;
				}
			}
		}


		public void PageChange(int pageNumber)
		{
			try
			{
				//verificar tempo de execução
				using (LojaVirtualContext ctxt = new LojaVirtualContext())
				{
					CurrentItems = ctxt.Products
						.Include(p => p.Images)
						.Include(p => p.Category)
						.Where(p => p.Category.CategoryId == CategoryId)
						.Skip(pageNumber * _numberOfItems)
						.Take(_numberOfItems)
						.ToList();
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
			CurrentPage = pageNumber;
			base.InvokeAsync(StateHasChanged);

		}
	}

}
