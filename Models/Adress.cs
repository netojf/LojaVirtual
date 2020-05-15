namespace LojaVirtual.Models
{
	public class Adress
	{
		public int AdressId { get; set; }
		public string PublicPlace { get; set; }
		public string street { get; set; }
		public int? HouseNumber { get; set; }
		public string District { get; set; }
		public User User { get; set; }
	}
}