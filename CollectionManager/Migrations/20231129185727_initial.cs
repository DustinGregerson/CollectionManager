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
                name: "users",
                columns: table => new
                {
                    userID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.userID);
                });

            migrationBuilder.CreateTable(
                name: "items",
                columns: table => new
                {
                    itemID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    image = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    userID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_items", x => x.itemID);
                    table.ForeignKey(
                        name: "FK_items_users_userID",
                        column: x => x.userID,
                        principalTable: "users",
                        principalColumn: "userID");
                });

            migrationBuilder.InsertData(
                table: "items",
                columns: new[] { "itemID", "Description", "Name", "image", "userID" },
                values: new object[,]
                {
                    { 1, "An old type writter", "Type writter", new byte[0], null },
                    { 2, "An old type writter", "Amber", new byte[0], null },
                    { 3, "An old type writter", "Ancient pot ", new byte[0], null }
                });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "userID", "password", "userName" },
                values: new object[,]
                {
                    { 1, "test1", "test1" },
                    { 2, "test2", "test2" },
                    { 3, "test3", "test3" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_items_userID",
                table: "items",
                column: "userID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "items");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
