using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LojaVirtual.Models
{
	public class Image
	{
		public int ImageId { get; set; }
		public string ImageName { get; set; }
		public byte[] Data { get; set; }
		public Product Product { get; set; }

	}
}
