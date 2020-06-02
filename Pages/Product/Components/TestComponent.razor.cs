using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
