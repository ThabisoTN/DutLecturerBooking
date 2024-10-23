using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DutLecturerBooking.Migrations
{
    /// <inheritdoc />
    public partial class ModuleidForeignkeyset : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_lecturerConsultationAvailabilities_Modules_ModulesModuleId",
                table: "lecturerConsultationAvailabilities");

            migrationBuilder.DropIndex(
                name: "IX_lecturerConsultationAvailabilities_ModulesModuleId",
                table: "lecturerConsultationAvailabilities");

            migrationBuilder.DropColumn(
                name: "ModulesModuleId",
                table: "lecturerConsultationAvailabilities");

            migrationBuilder.CreateIndex(
                name: "IX_lecturerConsultationAvailabilities_ModuleId",
                table: "lecturerConsultationAvailabilities",
                column: "ModuleId");

            migrationBuilder.AddForeignKey(
                name: "FK_lecturerConsultationAvailabilities_Modules_ModuleId",
                table: "lecturerConsultationAvailabilities",
                column: "ModuleId",
                principalTable: "Modules",
                principalColumn: "ModuleId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_lecturerConsultationAvailabilities_Modules_ModuleId",
                table: "lecturerConsultationAvailabilities");

            migrationBuilder.DropIndex(
                name: "IX_lecturerConsultationAvailabilities_ModuleId",
                table: "lecturerConsultationAvailabilities");

            migrationBuilder.AddColumn<int>(
                name: "ModulesModuleId",
                table: "lecturerConsultationAvailabilities",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_lecturerConsultationAvailabilities_ModulesModuleId",
                table: "lecturerConsultationAvailabilities",
                column: "ModulesModuleId");

            migrationBuilder.AddForeignKey(
                name: "FK_lecturerConsultationAvailabilities_Modules_ModulesModuleId",
                table: "lecturerConsultationAvailabilities",
                column: "ModulesModuleId",
                principalTable: "Modules",
                principalColumn: "ModuleId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
