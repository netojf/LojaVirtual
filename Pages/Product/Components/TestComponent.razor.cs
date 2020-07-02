using Microsoft.AspNetCore.Components;
using System.Collections.Generic;

namespace LojaVirtual.Pages.Product.Components
{
	public partial class TestComponentBase : ComponentBase
	{
		[Parameter]
		public ICollection<Models.Image> Images { get; set; }

		[Parameter]
		public EventCallback<ICollection<Models.Image>> ImagesChanged { get; set; }
	}
}
