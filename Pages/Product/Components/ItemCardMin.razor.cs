using LojaVirtual.Tools;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LojaVirtual.Pages.Product.Components
{
	public class ItemCardMinBase : ComponentBase
	{
		string _mainStyle = "text-center";
		private string _cardBodyStyle = "";
		private string _cardBodyClass = "";
		private string _cardTitleClass = "";
		private string _cardTitleStyle = "";
		private string _title = "";
		private string _cardTextClass = "";
		private string _cardTextStyle = "";
		private double _price = 0.0;
		private string _imgStyle = "";
		private List<Models.Image> _imageList;
		private string _mainClass = "";
		private string _redirectSrc = "";
		private string _imgClass = "";
		private string _carouselId = "";

		[Parameter]
		public List<Models.Image> ImageList { get => _imageList; set => _imageList = value; }
		[Parameter]
		public string CardTextStyle { get => _cardTextStyle; set => _cardTextStyle = value; }
		[Parameter]
		public string CardTextClass { get => _cardTextClass; set => _cardTextClass = value; }
		[Parameter]
		public string ImgStyle { get => _imgStyle; set => _imgStyle = value; }
		[Parameter]
		public string CardTitleStyle { get => _cardTitleStyle; set => _cardTitleStyle = value; }
		[Parameter]
		public string CardTitleClass { get => _cardTitleClass; set => _cardTitleClass = value; }
		[Parameter]
		public string CardBodyClass { get => _cardBodyClass; set => _cardBodyClass = value; }
		[Parameter]
		public string CardBodyStyle { get => _cardBodyStyle; set => _cardBodyStyle = value; }
		[Parameter]
		public string MainClass { get => _mainClass; set => _mainClass = value; }
		[Parameter]
		public string Title { get => _title; set => _title = value; }
		[Parameter]
		public double Price { get => _price; set => _price = value; }
		[Parameter]
		public string RedirectSrc { get => _redirectSrc; set => _redirectSrc = value; }
		[Parameter]
		public string ImgClass { get => _imgClass; set => _imgClass = value; }
		[Parameter]
		public string CarouselId { get => _carouselId; set => _carouselId = value; }
		[Parameter]
		public string MainStyle { get => _mainStyle; set => _mainStyle = value; }


		[Inject]
		public NavigationManager NavigationManager { get; set; }
		

		protected void RedirectToItem()
		{
			if (string.IsNullOrEmpty(RedirectSrc))
			{
				NavigationManager.NavigateTo(RedirectSrc);
			}
			 
		}
	}
}
