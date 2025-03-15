using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessPdf.ApiService.Migrations
{
    /// <inheritdoc />
    public partial class InvoiceModel1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceModels_AspNetUsers_UserId",
                table: "InvoiceModels");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "InvoiceModels",
                newName: "ApplicationUserId");

            migrationBuilder.RenameIndex(
                name: "IX_InvoiceModels_UserId",
                table: "InvoiceModels",
                newName: "IX_InvoiceModels_ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceModels_AspNetUsers_ApplicationUserId",
                table: "InvoiceModels",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceModels_AspNetUsers_ApplicationUserId",
                table: "InvoiceModels");

            migrationBuilder.RenameColumn(
                name: "ApplicationUserId",
                table: "InvoiceModels",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_InvoiceModels_ApplicationUserId",
                table: "InvoiceModels",
                newName: "IX_InvoiceModels_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceModels_AspNetUsers_UserId",
                table: "InvoiceModels",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
