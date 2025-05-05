using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Compliance_Platform.Migrations
{
    /// <inheritdoc />
    public partial class DataUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Kategoria",
                table: "Tools",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "VerificationHistory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InstanceId = table.Column<int>(type: "int", nullable: false),
                    AuditorId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    OldStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NewStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VerificationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VerificationHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VerificationHistory_InstancesTool_InstanceId",
                        column: x => x.InstanceId,
                        principalTable: "InstancesTool",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VerificationHistory_Users_AuditorId",
                        column: x => x.AuditorId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_VerificationHistory_AuditorId",
                table: "VerificationHistory",
                column: "AuditorId");

            migrationBuilder.CreateIndex(
                name: "IX_VerificationHistory_InstanceId",
                table: "VerificationHistory",
                column: "InstanceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VerificationHistory");

            migrationBuilder.DropColumn(
                name: "Kategoria",
                table: "Tools");
        }
    }
}
