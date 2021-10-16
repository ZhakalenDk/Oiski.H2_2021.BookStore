using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Oiski.H2_2021.BookStore.DataLayer.Migrations
{
    public partial class ForeignKeyFix_2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    AuthorID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.AuthorID);
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    BookID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PublishedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Publisher = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SoftDeleted = table.Column<bool>(type: "bit", nullable: false),
                    PriceOfferID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.BookID);
                });

            migrationBuilder.CreateTable(
                name: "BookAuthors",
                columns: table => new
                {
                    BookID = table.Column<int>(type: "int", nullable: false),
                    AuthorID = table.Column<int>(type: "int", nullable: false),
                    Order = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookAuthors", x => new { x.AuthorID, x.BookID });
                    table.ForeignKey(
                        name: "FK_BookAuthors_Authors_AuthorID",
                        column: x => x.AuthorID,
                        principalTable: "Authors",
                        principalColumn: "AuthorID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookAuthors_Books_BookID",
                        column: x => x.BookID,
                        principalTable: "Books",
                        principalColumn: "BookID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PriceOffers",
                columns: table => new
                {
                    PriceOfferID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NewPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PromotionalText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BookID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriceOffers", x => x.PriceOfferID);
                    table.ForeignKey(
                        name: "FK_PriceOffers_Books_BookID",
                        column: x => x.BookID,
                        principalTable: "Books",
                        principalColumn: "BookID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Review",
                columns: table => new
                {
                    ReviewID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VoterName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumStars = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BookID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Review", x => x.ReviewID);
                    table.ForeignKey(
                        name: "FK_Review_Books_BookID",
                        column: x => x.BookID,
                        principalTable: "Books",
                        principalColumn: "BookID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "AuthorID", "Name" },
                values: new object[,]
                {
                    { 1, "Marting Fowler" },
                    { 2, "Eric Evans" },
                    { 3, "Future Person" }
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "BookID", "Description", "ImageUrl", "Price", "PriceOfferID", "PublishedOn", "Publisher", "SoftDeleted", "Title" },
                values: new object[,]
                {
                    { 1, "Improving the design of the exsisting code", null, 40m, 0, new DateTime(1999, 7, 8, 0, 0, 0, 0, DateTimeKind.Local), null, false, "Refactoring" },
                    { 2, "Written in direct response to the stuff challenges", null, 53m, 0, new DateTime(2002, 11, 15, 0, 0, 0, 0, DateTimeKind.Local), null, false, "Patterns of Enterprise Application Architecture" },
                    { 3, "Linking business needs to software design", null, 56m, 0, new DateTime(2003, 8, 30, 0, 0, 0, 0, DateTimeKind.Local), null, false, "Domain-Driven Design" },
                    { 4, "Entangled quantum netorking provides faster-than-light data communications", null, 220m, 0, new DateTime(2057, 1, 1, 0, 0, 0, 0, DateTimeKind.Local), null, false, "Quantum Networking" }
                });

            migrationBuilder.InsertData(
                table: "BookAuthors",
                columns: new[] { "AuthorID", "BookID", "Order" },
                values: new object[,]
                {
                    { 1, 1, (byte)0 },
                    { 2, 1, (byte)0 },
                    { 1, 2, (byte)0 },
                    { 2, 3, (byte)0 },
                    { 3, 4, (byte)0 }
                });

            migrationBuilder.InsertData(
                table: "Review",
                columns: new[] { "ReviewID", "BookID", "Comment", "NumStars", "VoterName" },
                values: new object[,]
                {
                    { 1, 1, "Great Book", 3, null },
                    { 2, 1, "Boring Book", 1, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookAuthors_BookID",
                table: "BookAuthors",
                column: "BookID");

            migrationBuilder.CreateIndex(
                name: "IX_PriceOffers_BookID",
                table: "PriceOffers",
                column: "BookID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Review_BookID",
                table: "Review",
                column: "BookID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookAuthors");

            migrationBuilder.DropTable(
                name: "PriceOffers");

            migrationBuilder.DropTable(
                name: "Review");

            migrationBuilder.DropTable(
                name: "Authors");

            migrationBuilder.DropTable(
                name: "Books");
        }
    }
}
