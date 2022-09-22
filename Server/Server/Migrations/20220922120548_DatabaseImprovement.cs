using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Migrations
{
    public partial class DatabaseImprovement : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exams_Students_StudentId",
                table: "Exams");

            migrationBuilder.DropIndex(
                name: "IX_Exams_StudentId",
                table: "Exams");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "Exams");

            migrationBuilder.CreateTable(
                name: "ExamStudent",
                columns: table => new
                {
                    ExamsId = table.Column<int>(type: "int", nullable: false),
                    StudentsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamStudent", x => new { x.ExamsId, x.StudentsId });
                    table.ForeignKey(
                        name: "FK_ExamStudent_Exams_ExamsId",
                        column: x => x.ExamsId,
                        principalTable: "Exams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExamStudent_Students_StudentsId",
                        column: x => x.StudentsId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExamStudent_StudentsId",
                table: "ExamStudent",
                column: "StudentsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExamStudent");

            migrationBuilder.AddColumn<int>(
                name: "StudentId",
                table: "Exams",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Exams_StudentId",
                table: "Exams",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Exams_Students_StudentId",
                table: "Exams",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id");
        }
    }
}
