
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace LojaVirtual.Pages
{
	public class IndexModel : PageModel
	{
		private readonly ILogger<IndexModel> _logger;

		[BindProperty]
		public List<Models.Image> ImageList { get; set; }

		public IndexModel(ILogger<IndexModel> logger)
		{
			_logger = logger;
			ImageList = new List<Models.Image>();
		}

		public void OnGet()
		{

		}

		public void OnPost()
		{

		}
	}
}
