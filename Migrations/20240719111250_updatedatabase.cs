using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ManagementAssistanceForBusinessWeb_OnlyRole.Migrations
{
    /// <inheritdoc />
    public partial class updatedatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Statuses_StatusID",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_StatusID",
                table: "Tasks");

            migrationBuilder.RenameColumn(
                name: "StatusID",
                table: "Tasks",
                newName: "Status");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Tasks",
                newName: "StatusID");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_StatusID",
                table: "Tasks",
                column: "StatusID");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Statuses_StatusID",
                table: "Tasks",
                column: "StatusID",
                principalTable: "Statuses",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
