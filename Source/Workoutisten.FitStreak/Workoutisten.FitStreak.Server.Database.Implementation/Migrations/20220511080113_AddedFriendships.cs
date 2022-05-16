using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Workoutisten.FitStreak.Server.Database.Implementation.Migrations
{
    public partial class AddedFriendships : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FriendshipRequest",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RequestingUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RequestedUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FriendshipRequest", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FriendshipRequest_User_RequestedUserId",
                        column: x => x.RequestedUserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FriendshipRequest_User_RequestingUserId",
                        column: x => x.RequestingUserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserUser",
                columns: table => new
                {
                    MyFriendsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UsersIAmFriendOfId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserUser", x => new { x.MyFriendsId, x.UsersIAmFriendOfId });
                    table.ForeignKey(
                        name: "FK_UserUser_User_MyFriendsId",
                        column: x => x.MyFriendsId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserUser_User_UsersIAmFriendOfId",
                        column: x => x.UsersIAmFriendOfId,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_FriendshipRequest_RequestedUserId_RequestingUserId",
                table: "FriendshipRequest",
                columns: new[] { "RequestedUserId", "RequestingUserId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FriendshipRequest_RequestingUserId",
                table: "FriendshipRequest",
                column: "RequestingUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserUser_UsersIAmFriendOfId",
                table: "UserUser",
                column: "UsersIAmFriendOfId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FriendshipRequest");

            migrationBuilder.DropTable(
                name: "UserUser");
        }
    }
}
