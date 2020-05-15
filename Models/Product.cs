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
		public int? Barcode { get; set; }
		public string Brand { get; set; }
		public int? ProductImage { get; set; }
		public ICollection<Image> Images { get; set; }
		public ICollection<Product> Products { get; set; }
		public Category Category { get; set; }
		public Product ProductPack { get; set; }
		public ProductOrder ProductOrder { get; set; }
	}
}
