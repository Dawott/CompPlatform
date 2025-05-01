using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Compliance_Platform.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Imie = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nazwisko = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Departament = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rola = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Questionnaires",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nazwa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Opis = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Aktywny = table.Column<bool>(type: "bit", nullable: false),
                    DataUtworzenia = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questionnaires", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Questionnaires_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tools",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nazwa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Opis = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataPowstania = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OwnerId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WlascicielId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tools", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tools_Users_WlascicielId",
                        column: x => x.WlascicielId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuestionnaireId = table.Column<int>(type: "int", nullable: false),
                    Tresc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TypPytania = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrderIndex = table.Column<int>(type: "int", nullable: false),
                    Kategoria = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Wymagane = table.Column<bool>(type: "bit", nullable: false),
                    WagaRyzyka = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    QuestionnairesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Questions_Questionnaires_QuestionnairesId",
                        column: x => x.QuestionnairesId,
                        principalTable: "Questionnaires",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InstancesTool",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ToolId = table.Column<int>(type: "int", nullable: false),
                    QuestionnaireId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataUtworzenia = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataZlozenia = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DataSprawdzenia = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AudytorId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    KalkulacjaRyzyka = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PoziomRyzyka = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompPlatformToolId = table.Column<int>(type: "int", nullable: false),
                    CompPlatformQuestionnairesId = table.Column<int>(type: "int", nullable: false),
                    CompPlatformUserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstancesTool", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InstancesTool_Questionnaires_CompPlatformQuestionnairesId",
                        column: x => x.CompPlatformQuestionnairesId,
                        principalTable: "Questionnaires",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InstancesTool_Tools_CompPlatformToolId",
                        column: x => x.CompPlatformToolId,
                        principalTable: "Tools",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InstancesTool_Users_AudytorId",
                        column: x => x.AudytorId,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_InstancesTool_Users_CompPlatformUserId",
                        column: x => x.CompPlatformUserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Answers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuestionTemplateId = table.Column<int>(type: "int", nullable: false),
                    Treść = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WagaRyzyka = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OrderIndex = table.Column<int>(type: "int", nullable: false),
                    QuestionsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Answers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Answers_Questions_QuestionsId",
                        column: x => x.QuestionsId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InstanceAnswers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InstanceId = table.Column<int>(type: "int", nullable: false),
                    QuestionTemplateId = table.Column<int>(type: "int", nullable: false),
                    OdpowiedzTekst = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OpcjaOdpowiedziId = table.Column<int>(type: "int", nullable: true),
                    WagaRyzykaPojedyncza = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DataOdpowiedzi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    WnioskujacyId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CompPlatformInstanceId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstanceAnswers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InstanceAnswers_Answers_OpcjaOdpowiedziId",
                        column: x => x.OpcjaOdpowiedziId,
                        principalTable: "Answers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_InstanceAnswers_InstancesTool_CompPlatformInstanceId",
                        column: x => x.CompPlatformInstanceId,
                        principalTable: "InstancesTool",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InstanceAnswers_Questions_QuestionTemplateId",
                        column: x => x.QuestionTemplateId,
                        principalTable: "Questions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_InstanceAnswers_Users_WnioskujacyId",
                        column: x => x.WnioskujacyId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Answers_QuestionsId",
                table: "Answers",
                column: "QuestionsId");

            migrationBuilder.CreateIndex(
                name: "IX_InstanceAnswers_CompPlatformInstanceId",
                table: "InstanceAnswers",
                column: "CompPlatformInstanceId");

            migrationBuilder.CreateIndex(
                name: "IX_InstanceAnswers_OpcjaOdpowiedziId",
                table: "InstanceAnswers",
                column: "OpcjaOdpowiedziId");

            migrationBuilder.CreateIndex(
                name: "IX_InstanceAnswers_QuestionTemplateId",
                table: "InstanceAnswers",
                column: "QuestionTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_InstanceAnswers_WnioskujacyId",
                table: "InstanceAnswers",
                column: "WnioskujacyId");

            migrationBuilder.CreateIndex(
                name: "IX_InstancesTool_AudytorId",
                table: "InstancesTool",
                column: "AudytorId");

            migrationBuilder.CreateIndex(
                name: "IX_InstancesTool_CompPlatformQuestionnairesId",
                table: "InstancesTool",
                column: "CompPlatformQuestionnairesId");

            migrationBuilder.CreateIndex(
                name: "IX_InstancesTool_CompPlatformToolId",
                table: "InstancesTool",
                column: "CompPlatformToolId");

            migrationBuilder.CreateIndex(
                name: "IX_InstancesTool_CompPlatformUserId",
                table: "InstancesTool",
                column: "CompPlatformUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Questionnaires_CreatedById",
                table: "Questionnaires",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_QuestionnairesId",
                table: "Questions",
                column: "QuestionnairesId");

            migrationBuilder.CreateIndex(
                name: "IX_Tools_WlascicielId",
                table: "Tools",
                column: "WlascicielId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InstanceAnswers");

            migrationBuilder.DropTable(
                name: "Answers");

            migrationBuilder.DropTable(
                name: "InstancesTool");

            migrationBuilder.DropTable(
                name: "Questions");

            migrationBuilder.DropTable(
                name: "Tools");

            migrationBuilder.DropTable(
                name: "Questionnaires");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
