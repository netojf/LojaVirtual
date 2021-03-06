﻿using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace LojaVirtual
{
	public static class LoginManagement
	{

		public static bool IsLogged { get; set; } = false;

		public static Models.User TempUser { get; set; }
		public static bool IsAdmin
		{
			get
			{
				if (TempUser == null)
				{
					return false;
				}
				if (TempUser.IsAdmin)
				{
					return true;
				}
				else return false;
			}
			private set => IsAdmin = value;
		}

		public static async Task<bool> LoginAsync(string userName, string password)
		{
			using Models.LojaVirtualContext ctxt = new Models.LojaVirtualContext();
			TempUser = await ctxt.Users
				.Include(u => u.ShoppingCart)
				.ThenInclude(s => s.ProductOrders)
				.ThenInclude(po => po.Products)
				.SingleOrDefaultAsync(x => x.Name == userName && x.Password == password);

			if (TempUser != null)
			{
				IsLogged = true;
			}
			else
			{
				IsLogged = false;
			}
			return IsLogged;
		}

		public static bool Logoff()
		{
			return IsLogged = false;
		}

	}
}
