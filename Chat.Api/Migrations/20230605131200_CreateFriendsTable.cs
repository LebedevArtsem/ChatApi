using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Chat.Api.Migrations
{
    /// <inheritdoc />
    public partial class CreateFriendsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FriendId",
                table: "users",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "friends",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_friends", x => x.id);
                    table.ForeignKey(
                        name: "FK_friends_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_users_FriendId",
                table: "users",
                column: "FriendId");

            migrationBuilder.CreateIndex(
                name: "IX_friends_UserId",
                table: "friends",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_users_friends_FriendId",
                table: "users",
                column: "FriendId",
                principalTable: "friends",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_users_friends_FriendId",
                table: "users");

            migrationBuilder.DropTable(
                name: "friends");

            migrationBuilder.DropIndex(
                name: "IX_users_FriendId",
                table: "users");

            migrationBuilder.DropColumn(
                name: "FriendId",
                table: "users");
        }
    }
}
