using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessPdf.ApiService.Migrations
{
    /// <inheritdoc />
    public partial class InvoiceModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "JobTitle",
                table: "AspNetUsers",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CompanyInfos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Address = table.Column<string>(type: "TEXT", nullable: true),
                    City = table.Column<string>(type: "TEXT", nullable: true),
                    Country = table.Column<string>(type: "TEXT", nullable: true),
                    VatNumber = table.Column<string>(type: "TEXT", nullable: true),
                    PhoneNumber = table.Column<string>(type: "TEXT", nullable: true),
                    WebSite = table.Column<string>(type: "TEXT", nullable: true),
                    SwiftInfo = table.Column<string>(type: "TEXT", nullable: true),
                    IbanInfo = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyInfos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InvoiceModels",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    InvoiceNumber = table.Column<string>(type: "TEXT", nullable: true),
                    IssueDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DueDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    SellerId = table.Column<Guid>(type: "TEXT", nullable: true),
                    CustomerId = table.Column<Guid>(type: "TEXT", nullable: true),
                    Remarks = table.Column<string>(type: "TEXT", nullable: true),
                    Comments = table.Column<string>(type: "TEXT", nullable: true),
                    GrandTotalPrice = table.Column<double>(type: "REAL", nullable: false),
                    GrandTotalPriceWithVat = table.Column<double>(type: "REAL", nullable: false),
                    UserId = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceModels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InvoiceModels_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_InvoiceModels_CompanyInfos_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "CompanyInfos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_InvoiceModels_CompanyInfos_SellerId",
                        column: x => x.SellerId,
                        principalTable: "CompanyInfos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "OrderForm",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Header = table.Column<string>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    TotalPrice = table.Column<double>(type: "REAL", nullable: false),
                    TotalPriceWithVat = table.Column<double>(type: "REAL", nullable: false),
                    InvoiceModelId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderForm", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderForm_InvoiceModels_InvoiceModelId",
                        column: x => x.InvoiceModelId,
                        principalTable: "InvoiceModels",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ServiceForm",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Header = table.Column<string>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    Duration = table.Column<string>(type: "TEXT", nullable: true),
                    TotalPrice = table.Column<double>(type: "REAL", nullable: false),
                    TotalPriceWithVat = table.Column<double>(type: "REAL", nullable: false),
                    InvoiceModelId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceForm", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiceForm_InvoiceModels_InvoiceModelId",
                        column: x => x.InvoiceModelId,
                        principalTable: "InvoiceModels",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "OrderItem",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Quantity = table.Column<string>(type: "TEXT", nullable: true),
                    Price = table.Column<string>(type: "TEXT", nullable: true),
                    OrderFormId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItem_OrderForm_OrderFormId",
                        column: x => x.OrderFormId,
                        principalTable: "OrderForm",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ServiceItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Duration = table.Column<string>(type: "TEXT", nullable: true),
                    ServiceFormId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiceItems_ServiceForm_ServiceFormId",
                        column: x => x.ServiceFormId,
                        principalTable: "ServiceForm",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceModels_CustomerId",
                table: "InvoiceModels",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceModels_SellerId",
                table: "InvoiceModels",
                column: "SellerId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceModels_UserId",
                table: "InvoiceModels",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderForm_InvoiceModelId",
                table: "OrderForm",
                column: "InvoiceModelId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_OrderFormId",
                table: "OrderItem",
                column: "OrderFormId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceForm_InvoiceModelId",
                table: "ServiceForm",
                column: "InvoiceModelId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceItems_ServiceFormId",
                table: "ServiceItems",
                column: "ServiceFormId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderItem");

            migrationBuilder.DropTable(
                name: "ServiceItems");

            migrationBuilder.DropTable(
                name: "OrderForm");

            migrationBuilder.DropTable(
                name: "ServiceForm");

            migrationBuilder.DropTable(
                name: "InvoiceModels");

            migrationBuilder.DropTable(
                name: "CompanyInfos");

            migrationBuilder.DropColumn(
                name: "JobTitle",
                table: "AspNetUsers");
        }
    }
}
