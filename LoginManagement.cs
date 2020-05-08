using Microsoft.AspNetCore.Http;
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
		public static bool Login(string userName, string password)
		{
			using (LojaVirtual.Models.LojaVirtualContext ctxt = new Models.LojaVirtualContext())
			{
				try
				{
					IsLogged = ctxt.Users.Any(u => u.Name == userName && u.Password == password);
					if (IsLogged)
					{
						TempUser = new Models.User();
						TempUser.Name = userName;
						TempUser.Password = password;
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
