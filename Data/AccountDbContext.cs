using AccountService.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

namespace AccountService.Data
{
	public class AccountDbContext : DbContext
	{

		public AccountDbContext(DbContextOptions<AccountDbContext> options) : base(options) { }
		public DbSet<Addresses> Addresses { get; set; }
		public DbSet<Account> Accounts { get; set; }
		public DbSet<User> Users { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			// Define primary key on UserId for Addresses
			modelBuilder.Entity<Addresses>()
				.HasKey(a => a.UserId);  // Set UserId as the primary key for Addresses

			// Configure the relationship between Addresses and User
			modelBuilder.Entity<Addresses>()
				.HasOne<User>()  // Addresses has one User
				.WithOne()  // User has one Addresses
				.HasForeignKey<Addresses>(a => a.UserId)  // Foreign key is UserId in Addresses
				.OnDelete(DeleteBehavior.Cascade);  // Optionally, set delete behavior

			// Configure owned ShippingAddress
			modelBuilder.Entity<Addresses>()
				.OwnsOne(a => a.ShippingAddress, sa =>
				{
					sa.Property(s => s.Street).HasColumnName("ShippingStreet");
					sa.Property(s => s.Town).HasColumnName("ShippingTown");
					sa.Property(s => s.Country).HasColumnName("ShippingCountry");
				});

			// Configure owned BillingAddress
			modelBuilder.Entity<Addresses>()
				.OwnsOne(a => a.BillingAddress, ba =>
				{
					ba.Property(b => b.Street).HasColumnName("BillingStreet");
					ba.Property(b => b.Town).HasColumnName("BillingTown");
					ba.Property(b => b.Country).HasColumnName("BillingCountry");
				});

			// Seed Users
			modelBuilder.Entity<User>().HasData(
				new User() { Id = 4, FirstName = "Bart", LastName = "Simpson", Age = 10 },
				new User() { Id = 5, FirstName = "Homer", LastName = "Simpson", Age = 34 },
				new User() { Id = 6, FirstName = "Charles", LastName = "Burns", Age = 81 }
			);

			// Seed Accounts
			modelBuilder.Entity<Account>().HasData(
				new Account() { Id = 1, UserId = 4, EmailAddress = "bart.simpson@doh.net" },
				new Account() { Id = 2, UserId = 5, EmailAddress = "homer.simpson@doh.net" },
				new Account() { Id = 3, UserId = 6, EmailAddress = "charles_m_burns@fission.com" }
			);

			// Seed Addresses with explicit associations to Users
			modelBuilder.Entity<Addresses>().HasData(
				new Addresses { UserId = 5 }, // Homer
				new Addresses { UserId = 6 }  // Charles
			);

			// Seed ShippingAddress with explicit UserId
			modelBuilder.Entity<Addresses>().OwnsOne(a => a.ShippingAddress).HasData(
				new
				{
					AddressesUserId = 5, // This is the foreign key
					Street = "742 Evergreen Terrace",
					Town = "Springfield",
					Country = "USA"
				},
				new
				{
					AddressesUserId = 6, // This is the foreign key
					Street = "Springfield Power Plant",
					Town = "Springfield",
					Country = "USA"
				}
			);

			// Seed BillingAddress with explicit UserId
			modelBuilder.Entity<Addresses>().OwnsOne(a => a.BillingAddress).HasData(
				new
				{
					AddressesUserId = 6, // This is the foreign key
					Street = "1000 Mammon Lane",
					Town = "Springfield",
					Country = "USA"
				}
			);

			base.OnModelCreating(modelBuilder);
		}



	}

	public static class ServiceExtensions
	{
		public static void ConfigureDatabase(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddDbContext<AccountDbContext>(options =>
				options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
			services.AddScoped<IAccountRepository, AccountRepository>();
		}
	}
}
