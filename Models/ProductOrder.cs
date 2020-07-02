using System.Collections.Generic;

namespace LojaVirtual.Models
{
	public class ProductOrder
	{
		public int ProductOrderId { get; set; }
		public int Quantity { get; set; }
		public ShoppingCart ShoppingCart { get; set; }
		public double DeliveryPrice { get; set; }
		public double OrderPrice { get; set; }
		public ICollection<Product> Products { get; set; }
	}
}
