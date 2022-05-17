using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Workoutisten.FitStreak.Server.Database.Implementation.Migrations
{
    public partial class AddedRegistrationConfirmationKeyAndPasswordForgottenKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PasswordForgottenKey",
                table: "User",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RegistrationConfirmationKey",
                table: "User",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_RegistrationConfirmationKey",
                table: "User",
                column: "RegistrationConfirmationKey",
                unique: true,
                filter: "[RegistrationConfirmationKey] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_User_RegistrationConfirmationKey",
                table: "User");

            migrationBuilder.DropColumn(
                name: "RegistrationConfirmationKey",
                table: "User");

            migrationBuilder.AlterColumn<Guid>(
                name: "PasswordForgottenKey",
                table: "User",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);
        }
    }
}
