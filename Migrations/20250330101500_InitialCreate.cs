using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccountService.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Age = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ShippingStreet = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShippingTown = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShippingCountry = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BillingStreet = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BillingTown = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BillingCountry = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Addresses_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "EmailAddress", "UserId" },
                values: new object[,]
                {
                    { 1, "bart.simpson@doh.net", 4 },
                    { 2, "homer.simpson@doh.net", 5 },
                    { 3, "charles_m_burns@fission.com", 6 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Age", "FirstName", "LastName" },
                values: new object[,]
                {
                    { 4, 10, "Bart", "Simpson" },
                    { 5, 34, "Homer", "Simpson" },
                    { 6, 81, "Charles", "Burns" }
                });

            migrationBuilder.InsertData(
                table: "Addresses",
                columns: new[] { "UserId", "BillingCountry", "BillingStreet", "BillingTown", "ShippingCountry", "ShippingStreet", "ShippingTown" },
                values: new object[] { 5, "USA", "1000 Mammon Lane", "Springfield", "USA", "742 Evergreen Terrace", "Springfield" });

            migrationBuilder.InsertData(
                table: "Addresses",
                columns: new[] { "UserId", "BillingCountry", "BillingStreet", "BillingTown", "ShippingCountry", "ShippingStreet", "ShippingTown" },
                values: new object[] { 6, "USA", "1000 Mammon Lane", "Springfield", "USA", "Springfield Power Plant", "Springfield" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
