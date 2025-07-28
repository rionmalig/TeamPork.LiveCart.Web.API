﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeamPork.LiveCart.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class LiveCart_v1_0_3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PaymentMethod",
                table: "Invoices",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymentMethod",
                table: "Invoices");
        }
    }
}
