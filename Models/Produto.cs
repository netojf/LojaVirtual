using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LojaVirtual.Models
{
	public class Product
	{
		public int ProductId { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public double Price { get; set; }
		public Category Category { get; set; }
		public ShoppingCart ShoppingCart { get; set; }
	}
}
