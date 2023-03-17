using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDb20 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_friends_users_email",
                table: "friends");

            migrationBuilder.RenameColumn(
                name: "email",
                table: "friends",
                newName: "user_email");

            migrationBuilder.RenameIndex(
                name: "IX_friends_email",
                table: "friends",
                newName: "IX_friends_user_email");

            migrationBuilder.AddForeignKey(
                name: "FK_friends_users_user_email",
                table: "friends",
                column: "user_email",
                principalTable: "users",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_friends_users_user_email",
                table: "friends");

            migrationBuilder.RenameColumn(
                name: "user_email",
                table: "friends",
                newName: "email");

            migrationBuilder.RenameIndex(
                name: "IX_friends_user_email",
                table: "friends",
                newName: "IX_friends_email");

            migrationBuilder.AddForeignKey(
                name: "FK_friends_users_email",
                table: "friends",
                column: "email",
                principalTable: "users",
                principalColumn: "id");
        }
    }
}
