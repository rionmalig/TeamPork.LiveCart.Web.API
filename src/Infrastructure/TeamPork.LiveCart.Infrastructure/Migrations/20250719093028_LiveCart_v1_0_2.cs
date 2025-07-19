using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace TeamPork.LiveCart.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class LiveCart_v1_0_2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InvoicesAdjustments",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ClientId = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Amount = table.Column<float>(type: "real", nullable: false),
                    IsPercentage = table.Column<bool>(type: "boolean", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: false),
                    InvoiceId = table.Column<long>(type: "bigint", nullable: false),
                    InvoiceClientId = table.Column<string>(type: "text", nullable: true),
                    UserSeqId = table.Column<long>(type: "bigint", nullable: true),
                    BusinessSeqId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoicesAdjustments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InvoicesAdjustments_Businesses_BusinessSeqId",
                        column: x => x.BusinessSeqId,
                        principalTable: "Businesses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_InvoicesAdjustments_Invoices_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "Invoices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InvoicesAdjustments_Users_UserSeqId",
                        column: x => x.UserSeqId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_InvoicesAdjustments_BusinessSeqId",
                table: "InvoicesAdjustments",
                column: "BusinessSeqId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoicesAdjustments_InvoiceId",
                table: "InvoicesAdjustments",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoicesAdjustments_UserSeqId",
                table: "InvoicesAdjustments",
                column: "UserSeqId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InvoicesAdjustments");
        }
    }
}
