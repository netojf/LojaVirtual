using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace LojaVirtual.Models
{
	public class Product
	{
		#region Properties
		public int ProductId { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public double Price { get; set; }
		public int? Barcode { get; set; }
		public string Brand { get; set; }
		public int? ProductImage { get; set; }
		[JsonIgnore]
		public ICollection<Image> Images { get; set; }
		[JsonIgnore]
		public ICollection<Product> Products { get; set; }
		[JsonIgnore]
		public Category Category { get; set; }
		[JsonIgnore]
		public Product ProductPack { get; set; }
		[JsonIgnore]
		public ProductOrder ProductOrder { get; set; }
		#endregion
	}
}
