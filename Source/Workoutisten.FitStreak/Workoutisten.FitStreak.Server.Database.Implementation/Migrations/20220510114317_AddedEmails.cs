using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Workoutisten.FitStreak.Server.Database.Implementation.Migrations
{
    public partial class AddedEmails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Email",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Email", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmailUser",
                columns: table => new
                {
                    EmailsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReceiversId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailUser", x => new { x.EmailsId, x.ReceiversId });
                    table.ForeignKey(
                        name: "FK_EmailUser_Email_EmailsId",
                        column: x => x.EmailsId,
                        principalTable: "Email",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmailUser_User_ReceiversId",
                        column: x => x.ReceiversId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmailUser_ReceiversId",
                table: "EmailUser",
                column: "ReceiversId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmailUser");

            migrationBuilder.DropTable(
                name: "Email");
        }
    }
}
