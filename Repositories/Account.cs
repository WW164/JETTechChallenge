using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AccountService.Repositories
{
	[Table("Accounts")]

	public class Account
    {
        [Key]
        public int Id { get; set; }
        public string EmailAddress { get; set; }
        public int UserId { get; set; }
    }
}
