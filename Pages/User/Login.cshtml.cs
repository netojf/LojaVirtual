using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LojaVirtual
{
	public class LoginModel : PageModel
	{
		[BindProperty]
		public Models.User tempUser { get; set; }

		public LoginModel()
		{
		}

		public IActionResult OnGet()
		{
			try
			{
				// Verification.  
				if (LoginManagement.IsLogged)
				{
					// Home Page.  
					return this.RedirectToPage("../Index");
				}
			}
			catch (Exception ex)
			{
				// Info  
				Console.Write(ex);
			}
			return this.Page();
		}

		public async Task<IActionResult> OnPostLogIn()
		{

			try
			{
				// Verification.  
				if (ModelState.IsValid)
				{
					// Initialization.  
					bool loginInfo = await LoginManagement.LoginAsync(tempUser.Name, tempUser.Password);
					if (loginInfo)
					{
						HttpContext.Session.SetString("username", tempUser.Name);
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

		#region Sign In method.  

		/// <summary>  
		/// Sign In User method.  
		/// </summary>  
		/// <param name="username">Username parameter.</param>  
		/// <param name="isPersistent">Is persistent parameter.</param>  
		/// <returns>Returns - await task</returns>  
		private async Task SignInUser(string username, bool isPersistent)
		{
			// Initialization.  
			var claims = new List<Claim>();

			try
			{
				// Setting  
				claims.Add(new Claim(ClaimTypes.Name, username));
				var claimIdenties = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
				var claimPrincipal = new ClaimsPrincipal(claimIdenties);
				var authenticationManager = Request.HttpContext;

				// Sign In.  
				await authenticationManager.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimPrincipal, new AuthenticationProperties() { IsPersistent = isPersistent });
			}
			catch (Exception ex)
			{
				// Info  
				throw ex;
			}
		}

		#endregion
	}
}