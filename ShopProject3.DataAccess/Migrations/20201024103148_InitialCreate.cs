using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ShopProject3.DataAccess.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductMedia_Documents_DocumentId",
                table: "ProductMedia");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Brands_BrandId",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductMedia",
                table: "ProductMedia");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "ProductTags");

            migrationBuilder.DropColumn(
                name: "Published",
                table: "ProductTags");

            migrationBuilder.DropColumn(
                name: "HeroImage",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "DocumentId",
                table: "ProductMedia");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "ProductMedia");

            migrationBuilder.DropColumn(
                name: "Published",
                table: "ProductMedia");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "ProductFeatureAttributes");

            migrationBuilder.DropColumn(
                name: "Published",
                table: "ProductFeatureAttributes");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "ProductCategories");

            migrationBuilder.DropColumn(
                name: "Published",
                table: "ProductCategories");

            migrationBuilder.DropColumn(
                name: "Total",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "CategoryFeatures");

            migrationBuilder.DropColumn(
                name: "Published",
                table: "CategoryFeatures");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Products",
                type: "decimal(19,10)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(19,10)");

            migrationBuilder.AlterColumn<int>(
                name: "BrandId",
                table: "Products",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "CategoriesEntityId",
                table: "Products",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Featured",
                table: "Products",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Hero",
                table: "Products",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MediaId",
                table: "ProductMedia",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DocumentsEntityId",
                table: "ProductMedia",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "OrdersDetail",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "AddressLine1",
                table: "Orders",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AddressLine2",
                table: "Orders",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Orders",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "Orders",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Orders",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Orders",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "OrderPlaced",
                table: "Orders",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "OrderTotal",
                table: "Orders",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Orders",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "Orders",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ZipCode",
                table: "Orders",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Hero",
                table: "Categories",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductMedia",
                table: "ProductMedia",
                columns: new[] { "MediaId", "ProductId" });

            migrationBuilder.CreateTable(
                name: "Banners",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Order = table.Column<int>(nullable: false),
                    Location = table.Column<int>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    Published = table.Column<bool>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false),
                    BannerImagePath = table.Column<string>(nullable: true),
                    BannerCode = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Banners", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Order = table.Column<int>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    Published = table.Column<bool>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MediaEntity",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Order = table.Column<int>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    Published = table.Column<bool>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false),
                    MediaPath = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MediaEntity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Offers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Order = table.Column<int>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    Published = table.Column<bool>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Offers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OffersDetail",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Order = table.Column<int>(nullable: false),
                    ProductsId = table.Column<int>(nullable: false),
                    Price = table.Column<decimal>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    Published = table.Column<bool>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OffersDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OffersDetail_Products_ProductsId",
                        column: x => x.ProductsId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PressReleases",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Order = table.Column<int>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    Published = table.Column<bool>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PressReleases", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ShoppingCartItem",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    SessionCartId = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingCartItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShoppingCartItem_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BannersStatistics",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SessionId = table.Column<string>(nullable: true),
                    BannersId = table.Column<int>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BannersStatistics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BannersStatistics_Banners_BannersId",
                        column: x => x.BannersId,
                        principalTable: "Banners",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EventsMedia",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventsId = table.Column<int>(nullable: false),
                    Order = table.Column<int>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    Published = table.Column<bool>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false),
                    MediaPath = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventsMedia", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EventsMedia_Events_EventsId",
                        column: x => x.EventsId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoriesEntityId",
                table: "Products",
                column: "CategoriesEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductMedia_DocumentsEntityId",
                table: "ProductMedia",
                column: "DocumentsEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_BannersStatistics_BannersId",
                table: "BannersStatistics",
                column: "BannersId");

            migrationBuilder.CreateIndex(
                name: "IX_EventsMedia_EventsId",
                table: "EventsMedia",
                column: "EventsId");

            migrationBuilder.CreateIndex(
                name: "IX_OffersDetail_ProductsId",
                table: "OffersDetail",
                column: "ProductsId");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCartItem_ProductId",
                table: "ShoppingCartItem",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductMedia_Documents_DocumentsEntityId",
                table: "ProductMedia",
                column: "DocumentsEntityId",
                principalTable: "Documents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductMedia_MediaEntity_MediaId",
                table: "ProductMedia",
                column: "MediaId",
                principalTable: "MediaEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Brands_BrandId",
                table: "Products",
                column: "BrandId",
                principalTable: "Brands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_CategoriesEntityId",
                table: "Products",
                column: "CategoriesEntityId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductMedia_Documents_DocumentsEntityId",
                table: "ProductMedia");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductMedia_MediaEntity_MediaId",
                table: "ProductMedia");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Brands_BrandId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_CategoriesEntityId",
                table: "Products");

            migrationBuilder.DropTable(
                name: "BannersStatistics");

            migrationBuilder.DropTable(
                name: "EventsMedia");

            migrationBuilder.DropTable(
                name: "MediaEntity");

            migrationBuilder.DropTable(
                name: "Offers");

            migrationBuilder.DropTable(
                name: "OffersDetail");

            migrationBuilder.DropTable(
                name: "PressReleases");

            migrationBuilder.DropTable(
                name: "ShoppingCartItem");

            migrationBuilder.DropTable(
                name: "Banners");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropIndex(
                name: "IX_Products_CategoriesEntityId",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductMedia",
                table: "ProductMedia");

            migrationBuilder.DropIndex(
                name: "IX_ProductMedia_DocumentsEntityId",
                table: "ProductMedia");

            migrationBuilder.DropColumn(
                name: "CategoriesEntityId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Featured",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Hero",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "MediaId",
                table: "ProductMedia");

            migrationBuilder.DropColumn(
                name: "DocumentsEntityId",
                table: "ProductMedia");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "OrdersDetail");

            migrationBuilder.DropColumn(
                name: "AddressLine1",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "AddressLine2",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "City",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "OrderPlaced",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "OrderTotal",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "State",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ZipCode",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Hero",
                table: "Categories");

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "ProductTags",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Published",
                table: "ProductTags",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Products",
                type: "decimal(19,10)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(19,10)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "BrandId",
                table: "Products",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HeroImage",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DocumentId",
                table: "ProductMedia",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "ProductMedia",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Published",
                table: "ProductMedia",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "ProductFeatureAttributes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Published",
                table: "ProductFeatureAttributes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "ProductCategories",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Published",
                table: "ProductCategories",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "Total",
                table: "Orders",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "CategoryFeatures",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Published",
                table: "CategoryFeatures",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductMedia",
                table: "ProductMedia",
                columns: new[] { "DocumentId", "ProductId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ProductMedia_Documents_DocumentId",
                table: "ProductMedia",
                column: "DocumentId",
                principalTable: "Documents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Brands_BrandId",
                table: "Products",
                column: "BrandId",
                principalTable: "Brands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
