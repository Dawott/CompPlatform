using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Compliance_Platform.Migrations
{
    /// <inheritdoc />
    public partial class UpdateForeignKeyForQuestions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Questionnaires_QuestionnairesId",
                table: "Questions");

            migrationBuilder.DropIndex(
                name: "IX_Questions_QuestionnairesId",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "QuestionnairesId",
                table: "Questions");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_QuestionnaireId",
                table: "Questions",
                column: "QuestionnaireId");

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_Questionnaires_QuestionnaireId",
                table: "Questions",
                column: "QuestionnaireId",
                principalTable: "Questionnaires",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Questionnaires_QuestionnaireId",
                table: "Questions");

            migrationBuilder.DropIndex(
                name: "IX_Questions_QuestionnaireId",
                table: "Questions");

            migrationBuilder.AddColumn<int>(
                name: "QuestionnairesId",
                table: "Questions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Questions_QuestionnairesId",
                table: "Questions",
                column: "QuestionnairesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_Questionnaires_QuestionnairesId",
                table: "Questions",
                column: "QuestionnairesId",
                principalTable: "Questionnaires",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
