using Microsoft.EntityFrameworkCore;

namespace AccountService.Repositories
{
	[Owned] 
    public class Address
    {
		public string Street { get; set; }
		public string Town { get; set; }
		public string Country { get; set; }
	}

}
