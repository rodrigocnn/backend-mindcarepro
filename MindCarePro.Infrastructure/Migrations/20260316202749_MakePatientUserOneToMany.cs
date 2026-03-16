using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MindCarePro.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MakePatientUserOneToMany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_patients_users_UserId",
                table: "patients");

            migrationBuilder.DropIndex(
                name: "IX_patients_UserId",
                table: "patients");

            migrationBuilder.CreateIndex(
                name: "IX_patients_UserId",
                table: "patients",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_patients_users_UserId",
                table: "patients",
                column: "UserId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_patients_users_UserId",
                table: "patients");

            migrationBuilder.DropIndex(
                name: "IX_patients_UserId",
                table: "patients");

            migrationBuilder.CreateIndex(
                name: "IX_patients_UserId",
                table: "patients",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_patients_users_UserId",
                table: "patients",
                column: "UserId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
