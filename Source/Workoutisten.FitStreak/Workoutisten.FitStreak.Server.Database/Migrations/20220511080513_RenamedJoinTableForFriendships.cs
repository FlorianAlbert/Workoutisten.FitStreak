using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Workoutisten.FitStreak.Server.Database.Implementation.Migrations
{
    public partial class RenamedJoinTableForFriendships : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserUser_User_MyFriendsId",
                table: "UserUser");

            migrationBuilder.DropForeignKey(
                name: "FK_UserUser_User_UsersIAmFriendOfId",
                table: "UserUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserUser",
                table: "UserUser");

            migrationBuilder.RenameTable(
                name: "UserUser",
                newName: "Friendships");

            migrationBuilder.RenameIndex(
                name: "IX_UserUser_UsersIAmFriendOfId",
                table: "Friendships",
                newName: "IX_Friendships_UsersIAmFriendOfId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Friendships",
                table: "Friendships",
                columns: new[] { "MyFriendsId", "UsersIAmFriendOfId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Friendships_User_MyFriendsId",
                table: "Friendships",
                column: "MyFriendsId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Friendships_User_UsersIAmFriendOfId",
                table: "Friendships",
                column: "UsersIAmFriendOfId",
                principalTable: "User",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Friendships_User_MyFriendsId",
                table: "Friendships");

            migrationBuilder.DropForeignKey(
                name: "FK_Friendships_User_UsersIAmFriendOfId",
                table: "Friendships");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Friendships",
                table: "Friendships");

            migrationBuilder.RenameTable(
                name: "Friendships",
                newName: "UserUser");

            migrationBuilder.RenameIndex(
                name: "IX_Friendships_UsersIAmFriendOfId",
                table: "UserUser",
                newName: "IX_UserUser_UsersIAmFriendOfId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserUser",
                table: "UserUser",
                columns: new[] { "MyFriendsId", "UsersIAmFriendOfId" });

            migrationBuilder.AddForeignKey(
                name: "FK_UserUser_User_MyFriendsId",
                table: "UserUser",
                column: "MyFriendsId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserUser_User_UsersIAmFriendOfId",
                table: "UserUser",
                column: "UsersIAmFriendOfId",
                principalTable: "User",
                principalColumn: "Id");
        }
    }
}
