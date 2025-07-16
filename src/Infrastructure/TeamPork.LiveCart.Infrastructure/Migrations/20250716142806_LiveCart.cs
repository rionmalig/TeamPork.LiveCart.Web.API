using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace TeamPork.LiveCart.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class LiveCart : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BusinessProfiles",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: true),
                    ContactNumber = table.Column<string>(type: "text", nullable: true),
                    Address = table.Column<string>(type: "text", nullable: true)
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
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: true),
                    ContactNumber = table.Column<string>(type: "text", nullable: true),
                    Address = table.Column<string>(type: "text", nullable: true)
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
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OwnerSeqId = table.Column<long>(type: "bigint", nullable: false),
                    BusinessProfileSeqId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Businesses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Businesses_BusinessProfiles_BusinessProfileSeqId",
                        column: x => x.BusinessProfileSeqId,
                        principalTable: "BusinessProfiles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Username = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: false),
                    UserProfileSeqId = table.Column<long>(type: "bigint", nullable: true),
                    BusinessSeqId = table.Column<long>(type: "bigint", nullable: true),
                    Active = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedBy = table.Column<int>(type: "integer", nullable: false),
                    CreatedDate = table.Column<DateOnly>(type: "date", nullable: false),
                    ModifiedBy = table.Column<int>(type: "integer", nullable: true),
                    ModifiedDate = table.Column<DateOnly>(type: "date", nullable: true),
                    DeletedBy = table.Column<int>(type: "integer", nullable: true),
                    DeletedDate = table.Column<DateOnly>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Businesses_BusinessSeqId",
                        column: x => x.BusinessSeqId,
                        principalTable: "Businesses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Users_UserProfiles_UserProfileSeqId",
                        column: x => x.UserProfileSeqId,
                        principalTable: "UserProfiles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BusinessInviteCodes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Code = table.Column<string>(type: "text", nullable: false),
                    BusinessSeqId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsRedeemed = table.Column<bool>(type: "boolean", nullable: false),
                    RedeemedByUserSeqId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessInviteCodes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BusinessInviteCodes_Businesses_BusinessSeqId",
                        column: x => x.BusinessSeqId,
                        principalTable: "Businesses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BusinessInviteCodes_Users_RedeemedByUserSeqId",
                        column: x => x.RedeemedByUserSeqId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ClientId = table.Column<string>(type: "text", nullable: true),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: true),
                    ContactNumber = table.Column<string>(type: "text", nullable: true),
                    TaxRegNo = table.Column<string>(type: "text", nullable: true),
                    Note = table.Column<string>(type: "text", nullable: true),
                    UserSeqId = table.Column<long>(type: "bigint", nullable: true),
                    BusinessSeqId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Customers_Businesses_BusinessSeqId",
                        column: x => x.BusinessSeqId,
                        principalTable: "Businesses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Customers_Users_UserSeqId",
                        column: x => x.UserSeqId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ClientId = table.Column<string>(type: "text", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Code = table.Column<string>(type: "text", nullable: true),
                    Price = table.Column<float>(type: "real", precision: 18, scale: 2, nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    Discount = table.Column<float>(type: "real", nullable: false),
                    IsPercentage = table.Column<bool>(type: "boolean", nullable: false),
                    UserSeqId = table.Column<long>(type: "bigint", nullable: true),
                    BusinessSeqId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Items_Businesses_BusinessSeqId",
                        column: x => x.BusinessSeqId,
                        principalTable: "Businesses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Items_Users_UserSeqId",
                        column: x => x.UserSeqId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RefreshTokens",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserSeqId = table.Column<long>(type: "bigint", nullable: false),
                    Token = table.Column<string>(type: "text", nullable: false),
                    Expirey = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefreshTokens_Users_UserSeqId",
                        column: x => x.UserSeqId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SaleForecastModelMetadatas",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserSeqId = table.Column<long>(type: "bigint", nullable: true),
                    BusinessSeqId = table.Column<long>(type: "bigint", nullable: true),
                    ModelPath = table.Column<string>(type: "text", nullable: false),
                    LastTrainedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    TrainingMinDate = table.Column<DateOnly>(type: "date", nullable: false),
                    TotalMin = table.Column<float>(type: "real", nullable: true),
                    TotalMax = table.Column<float>(type: "real", nullable: true),
                    R2Score = table.Column<double>(type: "double precision", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SaleForecastModelMetadatas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SaleForecastModelMetadatas_Businesses_BusinessSeqId",
                        column: x => x.BusinessSeqId,
                        principalTable: "Businesses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SaleForecastModelMetadatas_Users_UserSeqId",
                        column: x => x.UserSeqId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Invoices",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ClientId = table.Column<string>(type: "text", nullable: true),
                    InvoiceTitle = table.Column<string>(type: "text", nullable: false),
                    OrderedAt = table.Column<DateOnly>(type: "date", nullable: false),
                    DueAt = table.Column<DateOnly>(type: "date", nullable: false),
                    Total = table.Column<float>(type: "real", precision: 18, scale: 2, nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    CustomerId = table.Column<long>(type: "bigint", nullable: false),
                    CustomerClientId = table.Column<string>(type: "text", nullable: true),
                    UserSeqId = table.Column<long>(type: "bigint", nullable: true),
                    BusinessSeqId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Invoices_Businesses_BusinessSeqId",
                        column: x => x.BusinessSeqId,
                        principalTable: "Businesses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Invoices_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Invoices_Users_UserSeqId",
                        column: x => x.UserSeqId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "InvoiceItems",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ClientId = table.Column<string>(type: "text", nullable: true),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    Discount = table.Column<float>(type: "real", nullable: false),
                    InvoiceId = table.Column<long>(type: "bigint", nullable: false),
                    InvoiceClientId = table.Column<string>(type: "text", nullable: true),
                    ItemId = table.Column<long>(type: "bigint", nullable: false),
                    ItemClientId = table.Column<string>(type: "text", nullable: true),
                    UserSeqId = table.Column<long>(type: "bigint", nullable: true),
                    BusinessSeqId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InvoiceItems_Businesses_BusinessSeqId",
                        column: x => x.BusinessSeqId,
                        principalTable: "Businesses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_InvoiceItems_Invoices_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "Invoices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InvoiceItems_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InvoiceItems_Users_UserSeqId",
                        column: x => x.UserSeqId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Businesses_BusinessProfileSeqId",
                table: "Businesses",
                column: "BusinessProfileSeqId");

            migrationBuilder.CreateIndex(
                name: "IX_Businesses_OwnerSeqId",
                table: "Businesses",
                column: "OwnerSeqId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BusinessInviteCodes_BusinessSeqId",
                table: "BusinessInviteCodes",
                column: "BusinessSeqId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessInviteCodes_RedeemedByUserSeqId",
                table: "BusinessInviteCodes",
                column: "RedeemedByUserSeqId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_BusinessSeqId",
                table: "Customers",
                column: "BusinessSeqId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_UserSeqId",
                table: "Customers",
                column: "UserSeqId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceItems_BusinessSeqId",
                table: "InvoiceItems",
                column: "BusinessSeqId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceItems_InvoiceId",
                table: "InvoiceItems",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceItems_ItemId",
                table: "InvoiceItems",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceItems_UserSeqId",
                table: "InvoiceItems",
                column: "UserSeqId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_BusinessSeqId",
                table: "Invoices",
                column: "BusinessSeqId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_CustomerId",
                table: "Invoices",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_UserSeqId",
                table: "Invoices",
                column: "UserSeqId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_BusinessSeqId",
                table: "Items",
                column: "BusinessSeqId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_UserSeqId",
                table: "Items",
                column: "UserSeqId");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_UserSeqId",
                table: "RefreshTokens",
                column: "UserSeqId");

            migrationBuilder.CreateIndex(
                name: "IX_SaleForecastModelMetadatas_BusinessSeqId",
                table: "SaleForecastModelMetadatas",
                column: "BusinessSeqId");

            migrationBuilder.CreateIndex(
                name: "IX_SaleForecastModelMetadatas_UserSeqId",
                table: "SaleForecastModelMetadatas",
                column: "UserSeqId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_BusinessSeqId",
                table: "Users",
                column: "BusinessSeqId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserProfileSeqId",
                table: "Users",
                column: "UserProfileSeqId");

            migrationBuilder.AddForeignKey(
                name: "FK_Businesses_Users_OwnerSeqId",
                table: "Businesses",
                column: "OwnerSeqId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Businesses_BusinessProfiles_BusinessProfileSeqId",
                table: "Businesses");

            migrationBuilder.DropForeignKey(
                name: "FK_Businesses_Users_OwnerSeqId",
                table: "Businesses");

            migrationBuilder.DropTable(
                name: "BusinessInviteCodes");

            migrationBuilder.DropTable(
                name: "InvoiceItems");

            migrationBuilder.DropTable(
                name: "RefreshTokens");

            migrationBuilder.DropTable(
                name: "SaleForecastModelMetadatas");

            migrationBuilder.DropTable(
                name: "Invoices");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "BusinessProfiles");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Businesses");

            migrationBuilder.DropTable(
                name: "UserProfiles");
        }
    }
}
