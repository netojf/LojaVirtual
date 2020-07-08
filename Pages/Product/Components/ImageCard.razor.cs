using Microsoft.AspNetCore.Components;
using System.Collections.Generic;

namespace LojaVirtual.Pages.Product.Components
{
	public partial class ImageCardBase : ComponentBase
	{
		List<ImageData> _imagesData;

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
					ImagesDataChanged.InvokeAsync(value);
				}
			}
		}

		[Parameter]
		public EventCallback<List<ImageData>> ImagesDataChanged { get; set; }

	}
}
