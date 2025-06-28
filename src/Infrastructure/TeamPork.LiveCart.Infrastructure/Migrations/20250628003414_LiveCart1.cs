using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeamPork.LiveCart.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class LiveCart1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "BusinessSeqId",
                table: "Users",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UserProfileSeqId",
                table: "Users",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "BusinessSeqId",
                table: "Items",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UserSeqId",
                table: "Items",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "BusinessSeqId",
                table: "Invoices",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UserSeqId",
                table: "Invoices",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "BusinessSeqId",
                table: "InvoiceItems",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UserSeqId",
                table: "InvoiceItems",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "BusinessSeqId",
                table: "Customers",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UserSeqId",
                table: "Customers",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "BusinessProfiles",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessProfiles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserProfiles",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProfiles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Businesses",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OwnerSeqId = table.Column<long>(type: "bigint", nullable: false),
                    BusinessProfileSeqId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Businesses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Businesses_BusinessProfiles_BusinessProfileSeqId",
                        column: x => x.BusinessProfileSeqId,
                        principalTable: "BusinessProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Businesses_Users_OwnerSeqId",
                        column: x => x.OwnerSeqId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_BusinessSeqId",
                table: "Users",
                column: "BusinessSeqId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserProfileSeqId",
                table: "Users",
                column: "UserProfileSeqId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_BusinessSeqId",
                table: "Items",
                column: "BusinessSeqId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_UserSeqId",
                table: "Items",
                column: "UserSeqId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_BusinessSeqId",
                table: "Invoices",
                column: "BusinessSeqId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_UserSeqId",
                table: "Invoices",
                column: "UserSeqId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceItems_BusinessSeqId",
                table: "InvoiceItems",
                column: "BusinessSeqId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceItems_UserSeqId",
                table: "InvoiceItems",
                column: "UserSeqId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_BusinessSeqId",
                table: "Customers",
                column: "BusinessSeqId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_UserSeqId",
                table: "Customers",
                column: "UserSeqId");

            migrationBuilder.CreateIndex(
                name: "IX_Businesses_BusinessProfileSeqId",
                table: "Businesses",
                column: "BusinessProfileSeqId");

            migrationBuilder.CreateIndex(
                name: "IX_Businesses_OwnerSeqId",
                table: "Businesses",
                column: "OwnerSeqId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Businesses_BusinessSeqId",
                table: "Customers",
                column: "BusinessSeqId",
                principalTable: "Businesses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Users_UserSeqId",
                table: "Customers",
                column: "UserSeqId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceItems_Businesses_BusinessSeqId",
                table: "InvoiceItems",
                column: "BusinessSeqId",
                principalTable: "Businesses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceItems_Users_UserSeqId",
                table: "InvoiceItems",
                column: "UserSeqId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_Businesses_BusinessSeqId",
                table: "Invoices",
                column: "BusinessSeqId",
                principalTable: "Businesses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_Users_UserSeqId",
                table: "Invoices",
                column: "UserSeqId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Businesses_BusinessSeqId",
                table: "Items",
                column: "BusinessSeqId",
                principalTable: "Businesses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Users_UserSeqId",
                table: "Items",
                column: "UserSeqId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Businesses_BusinessSeqId",
                table: "Users",
                column: "BusinessSeqId",
                principalTable: "Businesses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_UserProfiles_UserProfileSeqId",
                table: "Users",
                column: "UserProfileSeqId",
                principalTable: "UserProfiles",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Businesses_BusinessSeqId",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Users_UserSeqId",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceItems_Businesses_BusinessSeqId",
                table: "InvoiceItems");

            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceItems_Users_UserSeqId",
                table: "InvoiceItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_Businesses_BusinessSeqId",
                table: "Invoices");

            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_Users_UserSeqId",
                table: "Invoices");

            migrationBuilder.DropForeignKey(
                name: "FK_Items_Businesses_BusinessSeqId",
                table: "Items");

            migrationBuilder.DropForeignKey(
                name: "FK_Items_Users_UserSeqId",
                table: "Items");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Businesses_BusinessSeqId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_UserProfiles_UserProfileSeqId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "Businesses");

            migrationBuilder.DropTable(
                name: "UserProfiles");

            migrationBuilder.DropTable(
                name: "BusinessProfiles");

            migrationBuilder.DropIndex(
                name: "IX_Users_BusinessSeqId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_UserProfileSeqId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Items_BusinessSeqId",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_Items_UserSeqId",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_Invoices_BusinessSeqId",
                table: "Invoices");

            migrationBuilder.DropIndex(
                name: "IX_Invoices_UserSeqId",
                table: "Invoices");

            migrationBuilder.DropIndex(
                name: "IX_InvoiceItems_BusinessSeqId",
                table: "InvoiceItems");

            migrationBuilder.DropIndex(
                name: "IX_InvoiceItems_UserSeqId",
                table: "InvoiceItems");

            migrationBuilder.DropIndex(
                name: "IX_Customers_BusinessSeqId",
                table: "Customers");

            migrationBuilder.DropIndex(
                name: "IX_Customers_UserSeqId",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "BusinessSeqId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UserProfileSeqId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "BusinessSeqId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "UserSeqId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "BusinessSeqId",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "UserSeqId",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "BusinessSeqId",
                table: "InvoiceItems");

            migrationBuilder.DropColumn(
                name: "UserSeqId",
                table: "InvoiceItems");

            migrationBuilder.DropColumn(
                name: "BusinessSeqId",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "UserSeqId",
                table: "Customers");
        }
    }
}
