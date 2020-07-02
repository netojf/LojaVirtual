using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LojaVirtual.Pages.Management
{
	public class IndexModel : PageModel
	{
		public void OnGet()
		{
			if (!LoginManagement.IsLogged)
			{
				RedirectToPage("Index");
			}
			else
			{
				if (!LoginManagement.TempUser.IsAdmin)
				{
					RedirectToPage("Index");
				}
			}
		}
	}
}