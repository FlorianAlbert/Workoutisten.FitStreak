using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Workoutisten.FitStreak.Server.Database.Implementation.Migrations
{
    public partial class AddedRegistrationConfirmationKeyAndPasswordForgottenKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_User_PasswordForgottenKey",
                table: "User");

            migrationBuilder.AlterColumn<string>(
                name: "PasswordForgottenKey",
                table: "User",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RegistrationConfirmationKey",
                table: "User",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RegistrationConfirmationKey",
                table: "User");

            migrationBuilder.AlterColumn<Guid>(
                name: "PasswordForgottenKey",
                table: "User",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_PasswordForgottenKey",
                table: "User",
                column: "PasswordForgottenKey",
                unique: true,
                filter: "[PasswordForgottenKey] IS NOT NULL");
        }
    }
}
