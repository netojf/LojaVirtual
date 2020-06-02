using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LojaVirtual
{
	public class _LoginPartialModel : PageModel
	{
		[BindProperty]
		public Models.User UserData { get; set; }
		public _LoginPartialModel()
		{
			UserData = new Models.User();
		}

		public void OnGet()
		{

		}

		public void OnPost()
		{

		}

		public IActionResult OnPostLogoff()
		{
			HttpContext.Session.Remove(LoginManagement.TempUser.Name);
			LoginManagement.Logoff();
			return RedirectToPage("../Index");
		}

		public async Task<IActionResult> OnPostLoginAsync()
		{

			try
			{
				// Verification.  
				if (ModelState.IsValid)
				{
					// Initialization.  
					bool isLogged = await LoginManagement.LoginAsync(UserData.Name, UserData.Password);

					if (isLogged)
					{
						HttpContext.Session.SetString("username", LoginManagement.TempUser.Name);
						return RedirectToPage("../Index");
					}
					else
					{
						ModelState.AddModelError(string.Empty, "Invalid username or password.");
					}
				}
			}
			catch (Exception ex)
			{
				ModelState.AddModelError(string.Empty, ex.Message);
			}

			// Info.  
			return this.Page();

		}
	}
}