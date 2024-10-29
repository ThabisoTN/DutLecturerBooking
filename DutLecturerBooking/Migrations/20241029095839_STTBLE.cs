using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DutLecturerBooking.Migrations
{
    /// <inheritdoc />
    public partial class STTBLE : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentModules_AspNetUsers_ApplicationUserId",
                table: "StudentModules");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentModules_Modules_ModulesModuleId",
                table: "StudentModules");

            migrationBuilder.DropIndex(
                name: "IX_StudentModules_ApplicationUserId",
                table: "StudentModules");

            migrationBuilder.DropIndex(
                name: "IX_StudentModules_ModulesModuleId",
                table: "StudentModules");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "StudentModules");

            migrationBuilder.DropColumn(
                name: "ModulesModuleId",
                table: "StudentModules");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "StudentModules",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_StudentModules_ModuleId",
                table: "StudentModules",
                column: "ModuleId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentModules_UserId",
                table: "StudentModules",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentModules_AspNetUsers_UserId",
                table: "StudentModules",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentModules_Modules_ModuleId",
                table: "StudentModules",
                column: "ModuleId",
                principalTable: "Modules",
                principalColumn: "ModuleId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentModules_AspNetUsers_UserId",
                table: "StudentModules");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentModules_Modules_ModuleId",
                table: "StudentModules");

            migrationBuilder.DropIndex(
                name: "IX_StudentModules_ModuleId",
                table: "StudentModules");

            migrationBuilder.DropIndex(
                name: "IX_StudentModules_UserId",
                table: "StudentModules");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "StudentModules",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "StudentModules",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ModulesModuleId",
                table: "StudentModules",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_StudentModules_ApplicationUserId",
                table: "StudentModules",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentModules_ModulesModuleId",
                table: "StudentModules",
                column: "ModulesModuleId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentModules_AspNetUsers_ApplicationUserId",
                table: "StudentModules",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentModules_Modules_ModulesModuleId",
                table: "StudentModules",
                column: "ModulesModuleId",
                principalTable: "Modules",
                principalColumn: "ModuleId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
