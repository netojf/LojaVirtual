using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading;

namespace LojaVirtual
{
	public class PaymentSucessModel : PageModel
	{
		public IActionResult OnGet()
		{
			Thread.Sleep(2000);
			//todo: Return to last Page
			return RedirectToPage("/Index");
		}
	}
}