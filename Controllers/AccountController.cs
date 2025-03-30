using System;
using System.Linq;
using System.Threading.Tasks;
using AccountService.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace AccountService.Controllers
{
    /// <summary>
    /// This controller implements a basic REST HTTP endpoint for querying information about accounts in a repository.
    /// How would you go about changing this code to improve it?
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
		//private IAccountRepository Repo { get; } = new AccountRepository();

		private readonly IAccountRepository _repository;

		public AccountController(IAccountRepository repository)
		{
			_repository = repository;
		}

		// GET /api/account?id={id}
		// Authorization: Basic htrqSjG1ua4r28iqgfgWNA==

		[HttpGet]
		public async Task<ActionResult<Models.Account>> GetAsync(int id)
		{
			//var account = Repo.GetAccountAsync(Request.Headers["Authorization"].Single(), id).Result;

			var authHeader = Request.Headers["Authorization"].FirstOrDefault();
			if (string.IsNullOrEmpty(authHeader)) return Unauthorized("Authorization header is missing.");

			var account = await _repository.GetAccountAsync(authHeader, id);
			if (account == null) return NotFound($"Account with ID {id} not found.");

			//var user = Repo.GetUserAsync(Request.Headers["Authorization"].Single(), account.UserId).Result;
			var user = await _repository.GetUserAsync(authHeader, account.UserId);
			if (user == null) return NotFound($"User with user ID {account.UserId} not found.");

			//var addresses = Repo.GetAddressesAsync(Request.Headers["Authorization"].Single(), account.UserId).Result;
			var addresses = await _repository.GetAddressesAsync(authHeader, account.UserId);
			if(addresses == null) return NotFound($"Address for user ID {account.UserId} not found.");

			if (user.FirstName == null || user.LastName == null || account.EmailAddress == null)
				return BadRequest("Required user information is missing.");

			if (addresses.ShippingAddress == null || addresses.BillingAddress == null)
				return BadRequest("Required address information is missing.");

			var content = new Models.Account
			{
				Id = account.Id,
				AddressLines = new[] { addresses.ShippingAddress.Street, addresses.ShippingAddress.Town, addresses.BillingAddress.Country },
				Email = account.EmailAddress,
				Forename = user.FirstName,
				Surname = user.LastName
			};

			return Ok(content);
		}
    }
}
