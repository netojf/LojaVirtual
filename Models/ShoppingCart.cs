using System.Collections.Generic;

namespace LojaVirtual.Models
{
	public class ShoppingCart
	{
		public int ShoppingCartId { get; set; }
		public int UserFk { get; set; }
		public ICollection<ProductOrder> ProductOrders { get; set; }
		public User User { get; set; }
	}
}
