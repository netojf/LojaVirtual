using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LojaVirtual
{
	public static class LoginManagement
	{
		public static bool IsLogged { get; private set; }
		public static Models.User TempUser { get; set; }

		//todo: Make this a Async method
		public static async Task<bool> LoginAsync(string userName, string password)
		{
			using (LojaVirtual.Models.LojaVirtualContext ctxt = new Models.LojaVirtualContext())
			{

				try
				{
					TempUser = await ctxt.Users
					.SingleOrDefaultAsync(x => x.Name == userName && x.Password == password);

					if (TempUser != null)
					{
						IsLogged = true; 
					}
					else
					{
						throw new Exception(); 
					}
				}
				catch (Exception ex)
				{
					throw ex;
				}
				return IsLogged; 
			}
		}

		public static bool Logoff()
		{
			return IsLogged = false;
		}
	}
}
