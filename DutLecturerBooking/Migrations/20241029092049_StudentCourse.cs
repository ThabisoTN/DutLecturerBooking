using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DutLecturerBooking.Migrations
{
    /// <inheritdoc />
    public partial class StudentCourse : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "StudentCourses",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "StudentCourses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_StudentCourses_ApplicationUserId",
                table: "StudentCourses",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentCourses_AspNetUsers_ApplicationUserId",
                table: "StudentCourses",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentCourses_AspNetUsers_ApplicationUserId",
                table: "StudentCourses");

            migrationBuilder.DropIndex(
                name: "IX_StudentCourses_ApplicationUserId",
                table: "StudentCourses");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "StudentCourses");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "StudentCourses");
        }
    }
}
