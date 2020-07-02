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
