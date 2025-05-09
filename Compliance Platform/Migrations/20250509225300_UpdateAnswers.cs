using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Compliance_Platform.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAnswers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answers_Questions_QuestionsId",
                table: "Answers");

            migrationBuilder.DropIndex(
                name: "IX_Answers_QuestionsId",
                table: "Answers");

            migrationBuilder.DropColumn(
                name: "QuestionsId",
                table: "Answers");

            migrationBuilder.CreateIndex(
                name: "IX_Answers_QuestionTemplateId",
                table: "Answers",
                column: "QuestionTemplateId");

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_Questions_QuestionTemplateId",
                table: "Answers",
                column: "QuestionTemplateId",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answers_Questions_QuestionTemplateId",
                table: "Answers");

            migrationBuilder.DropIndex(
                name: "IX_Answers_QuestionTemplateId",
                table: "Answers");

            migrationBuilder.AddColumn<int>(
                name: "QuestionsId",
                table: "Answers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Answers_QuestionsId",
                table: "Answers",
                column: "QuestionsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_Questions_QuestionsId",
                table: "Answers",
                column: "QuestionsId",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
