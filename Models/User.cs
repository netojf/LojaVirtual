using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LojaVirtual.Models
{
	public class User
	{
		public int UserId { get; set; }
		public string Name { get; set; }
		public string Password { get; set; }
		[DataType(DataType.EmailAddress)]
		[EmailAddress]
		public string Email { get; set; }
		public bool IsAdmin { get; set; }
		public ICollection<Adress> Adresses { get; set; }
		public ShoppingCart ShoppingCart { get; set; }
	}
}
