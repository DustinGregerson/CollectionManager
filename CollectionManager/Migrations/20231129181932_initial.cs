using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CollectionManager.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "items",
                columns: table => new
                {
                    itemID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    image = table.Column<byte[]>(type: "varbinary(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_items", x => x.itemID);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    userID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    itemID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.userID);
                    table.ForeignKey(
                        name: "FK_users_items_itemID",
                        column: x => x.itemID,
                        principalTable: "items",
                        principalColumn: "itemID");
                });

            migrationBuilder.InsertData(
                table: "items",
                columns: new[] { "itemID", "Description", "Name", "image" },
                values: new object[,]
                {
                    { 1, "An old type writter", "Type writter", new byte[0] },
                    { 2, "An old type writter", "Amber", new byte[0] },
                    { 3, "An old type writter", "Ancient pot ", new byte[0] }
                });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "userID", "itemID", "password", "userName" },
                values: new object[,]
                {
                    { 1, null, "test1", "test1" },
                    { 2, null, "test2", "test2" },
                    { 3, null, "test3", "test3" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_users_itemID",
                table: "users",
                column: "itemID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "items");
        }
    }
}
