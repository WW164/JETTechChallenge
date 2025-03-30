using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AccountService.Repositories
{
	[Table("Addresses")]

	public class Addresses
    {
		[Key]
		public int UserId { get; set; }
		public Address ShippingAddress { get; set; }
		public Address BillingAddress { get; set; }
	}

}


