using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Chat.Api.Migrations
{
    /// <inheritdoc />
    public partial class UpdateFriendsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "user_id",
                table: "friends");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "friends",
                newName: "user_id");

            migrationBuilder.RenameIndex(
                name: "IX_friends_UserId",
                table: "friends",
                newName: "IX_friends_user_id");

            migrationBuilder.AddForeignKey(
                name: "FK_friends_users_user_id",
                table: "friends",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_friends_users_user_id",
                table: "friends");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "friends",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_friends_user_id",
                table: "friends",
                newName: "IX_friends_UserId");

            migrationBuilder.AddForeignKey(
                name: "user_id",
                table: "friends",
                column: "UserId",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
