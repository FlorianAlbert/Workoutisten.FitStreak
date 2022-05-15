using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Workoutisten.FitStreak.Server.Database.Implementation.Migrations
{
    public partial class AddedStrengthAndCardioExerciseEntries : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "ExerciseEntry",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "Distance",
                table: "ExerciseEntry",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "Duration",
                table: "ExerciseEntry",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Repetitions",
                table: "ExerciseEntry",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Weight",
                table: "ExerciseEntry",
                type: "float",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "ExerciseEntry");

            migrationBuilder.DropColumn(
                name: "Distance",
                table: "ExerciseEntry");

            migrationBuilder.DropColumn(
                name: "Duration",
                table: "ExerciseEntry");

            migrationBuilder.DropColumn(
                name: "Repetitions",
                table: "ExerciseEntry");

            migrationBuilder.DropColumn(
                name: "Weight",
                table: "ExerciseEntry");
        }
    }
}
