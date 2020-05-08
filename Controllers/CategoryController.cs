using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LojaVirtual.Controllers
{
	public class CategoryController : Controller
	{
		#region Campos

		#endregion

		#region Properties 

		#endregion

		#region Constructors
		public CategoryController()
		{
		}
		#endregion

		#region Methods
		public IActionResult Save()
		{
			return View(); 
		}
		#endregion
	}
}
