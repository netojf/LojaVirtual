using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LojaVirtual.Models
{
	public class ShoppingCart
	{
		public int ShoppingCartId { get; set; }
		public int UserFk { get; set; }
		public ICollection<Product> Products { get; set; }
		public User User { get; set; }
	}
}
