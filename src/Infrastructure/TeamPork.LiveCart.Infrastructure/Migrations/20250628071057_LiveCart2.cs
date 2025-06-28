using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeamPork.LiveCart.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class LiveCart2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Businesses_BusinessProfiles_BusinessProfileSeqId",
                table: "Businesses");

            migrationBuilder.AlterColumn<long>(
                name: "BusinessProfileSeqId",
                table: "Businesses",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddForeignKey(
                name: "FK_Businesses_BusinessProfiles_BusinessProfileSeqId",
                table: "Businesses",
                column: "BusinessProfileSeqId",
                principalTable: "BusinessProfiles",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Businesses_BusinessProfiles_BusinessProfileSeqId",
                table: "Businesses");

            migrationBuilder.AlterColumn<long>(
                name: "BusinessProfileSeqId",
                table: "Businesses",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Businesses_BusinessProfiles_BusinessProfileSeqId",
                table: "Businesses",
                column: "BusinessProfileSeqId",
                principalTable: "BusinessProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
