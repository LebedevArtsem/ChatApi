using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Chat.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_chat_messages_messages_message_id",
                table: "chat_messages");

            migrationBuilder.DropForeignKey(
                name: "FK_chat_messages_users_sender_id",
                table: "chat_messages");

            migrationBuilder.DropForeignKey(
                name: "FK_messages_chat_messages_id",
                table: "messages");

            migrationBuilder.DropForeignKey(
                name: "FK_users_chat_messages_id",
                table: "users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_messages",
                table: "messages");

            migrationBuilder.DropPrimaryKey(
                name: "id",
                table: "chat_messages");

            migrationBuilder.RenameTable(
                name: "messages",
                newName: "Messages");

            migrationBuilder.RenameTable(
                name: "chat_messages",
                newName: "ChatMessages");

            migrationBuilder.RenameColumn(
                name: "time",
                table: "Messages",
                newName: "Time");

            migrationBuilder.RenameColumn(
                name: "text",
                table: "Messages",
                newName: "Text");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Messages",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "is_read",
                table: "Messages",
                newName: "IsRead");

            migrationBuilder.RenameColumn(
                name: "is_changed",
                table: "Messages",
                newName: "IsChanged");

            migrationBuilder.RenameColumn(
                name: "sender_id",
                table: "ChatMessages",
                newName: "SenderId");

            migrationBuilder.RenameColumn(
                name: "reciever_id",
                table: "ChatMessages",
                newName: "RecieverId");

            migrationBuilder.RenameColumn(
                name: "message_id",
                table: "ChatMessages",
                newName: "MessageId");

            migrationBuilder.RenameIndex(
                name: "IX_chat_messages_sender_id",
                table: "ChatMessages",
                newName: "IX_ChatMessages_SenderId");

            migrationBuilder.RenameIndex(
                name: "IX_chat_messages_message_id",
                table: "ChatMessages",
                newName: "IX_ChatMessages_MessageId");

            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "users",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

            migrationBuilder.AlterColumn<string>(
                name: "Text",
                table: "Messages",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Messages",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

            migrationBuilder.AlterColumn<bool>(
                name: "IsRead",
                table: "Messages",
                type: "boolean",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldDefaultValue: false);

            migrationBuilder.AlterColumn<bool>(
                name: "IsChanged",
                table: "Messages",
                type: "boolean",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldDefaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Messages",
                table: "Messages",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ChatMessages",
                table: "ChatMessages",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ChatMessages_RecieverId",
                table: "ChatMessages",
                column: "RecieverId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChatMessages_Messages_MessageId",
                table: "ChatMessages",
                column: "MessageId",
                principalTable: "Messages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ChatMessages_users_RecieverId",
                table: "ChatMessages",
                column: "RecieverId",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ChatMessages_users_SenderId",
                table: "ChatMessages",
                column: "SenderId",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChatMessages_Messages_MessageId",
                table: "ChatMessages");

            migrationBuilder.DropForeignKey(
                name: "FK_ChatMessages_users_RecieverId",
                table: "ChatMessages");

            migrationBuilder.DropForeignKey(
                name: "FK_ChatMessages_users_SenderId",
                table: "ChatMessages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Messages",
                table: "Messages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ChatMessages",
                table: "ChatMessages");

            migrationBuilder.DropIndex(
                name: "IX_ChatMessages_RecieverId",
                table: "ChatMessages");

            migrationBuilder.RenameTable(
                name: "Messages",
                newName: "messages");

            migrationBuilder.RenameTable(
                name: "ChatMessages",
                newName: "chat_messages");

            migrationBuilder.RenameColumn(
                name: "Time",
                table: "messages",
                newName: "time");

            migrationBuilder.RenameColumn(
                name: "Text",
                table: "messages",
                newName: "text");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "messages",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "IsRead",
                table: "messages",
                newName: "is_read");

            migrationBuilder.RenameColumn(
                name: "IsChanged",
                table: "messages",
                newName: "is_changed");

            migrationBuilder.RenameColumn(
                name: "SenderId",
                table: "chat_messages",
                newName: "sender_id");

            migrationBuilder.RenameColumn(
                name: "RecieverId",
                table: "chat_messages",
                newName: "reciever_id");

            migrationBuilder.RenameColumn(
                name: "MessageId",
                table: "chat_messages",
                newName: "message_id");

            migrationBuilder.RenameIndex(
                name: "IX_ChatMessages_SenderId",
                table: "chat_messages",
                newName: "IX_chat_messages_sender_id");

            migrationBuilder.RenameIndex(
                name: "IX_ChatMessages_MessageId",
                table: "chat_messages",
                newName: "IX_chat_messages_message_id");

            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "users",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

            migrationBuilder.AlterColumn<string>(
                name: "text",
                table: "messages",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "messages",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

            migrationBuilder.AlterColumn<bool>(
                name: "is_read",
                table: "messages",
                type: "boolean",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "boolean");

            migrationBuilder.AlterColumn<bool>(
                name: "is_changed",
                table: "messages",
                type: "boolean",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "boolean");

            migrationBuilder.AddPrimaryKey(
                name: "PK_messages",
                table: "messages",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "id",
                table: "chat_messages",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_chat_messages_messages_message_id",
                table: "chat_messages",
                column: "message_id",
                principalTable: "messages",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_chat_messages_users_sender_id",
                table: "chat_messages",
                column: "sender_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_messages_chat_messages_id",
                table: "messages",
                column: "id",
                principalTable: "chat_messages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_users_chat_messages_id",
                table: "users",
                column: "id",
                principalTable: "chat_messages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
