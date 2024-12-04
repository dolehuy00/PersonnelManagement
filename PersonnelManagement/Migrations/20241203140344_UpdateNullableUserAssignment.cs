using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonnelManagement.Migrations
{
    /// <inheritdoc />
    public partial class UpdateNullableUserAssignment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assignments_Employees_ResponsiblePesonId",
                table: "Assignments");

            migrationBuilder.AlterColumn<long>(
                name: "ResponsiblePesonId",
                table: "Assignments",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddForeignKey(
                name: "FK_Assignments_Employees_ResponsiblePesonId",
                table: "Assignments",
                column: "ResponsiblePesonId",
                principalTable: "Employees",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assignments_Employees_ResponsiblePesonId",
                table: "Assignments");

            migrationBuilder.AlterColumn<long>(
                name: "ResponsiblePesonId",
                table: "Assignments",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Assignments_Employees_ResponsiblePesonId",
                table: "Assignments",
                column: "ResponsiblePesonId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
