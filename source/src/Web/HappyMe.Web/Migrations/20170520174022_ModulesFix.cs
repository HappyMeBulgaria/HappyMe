using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HappyMe.Web.Migrations
{
    public partial class ModulesFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Modules_Questions_QuestionId",
                table: "Modules");

            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Modules_ModuleId",
                table: "Questions");

            migrationBuilder.DropIndex(
                name: "IX_Questions_ModuleId",
                table: "Questions");

            migrationBuilder.DropIndex(
                name: "IX_Modules_QuestionId",
                table: "Modules");

            migrationBuilder.DropColumn(
                name: "ModuleId",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "QuestionId",
                table: "Modules");

            migrationBuilder.AlterColumn<string>(
                name: "Text",
                table: "Answers",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.CreateTable(
                name: "QuestionsInModules",
                columns: table => new
                {
                    QuestionId = table.Column<int>(nullable: false),
                    ModuleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionsInModules", x => new { x.QuestionId, x.ModuleId });
                    table.ForeignKey(
                        name: "FK_QuestionsInModules_Modules_ModuleId",
                        column: x => x.ModuleId,
                        principalTable: "Modules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuestionsInModules_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_QuestionsInModules_ModuleId",
                table: "QuestionsInModules",
                column: "ModuleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QuestionsInModules");

            migrationBuilder.AddColumn<int>(
                name: "ModuleId",
                table: "Questions",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "QuestionId",
                table: "Modules",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Text",
                table: "Answers",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 100);

            migrationBuilder.CreateIndex(
                name: "IX_Questions_ModuleId",
                table: "Questions",
                column: "ModuleId");

            migrationBuilder.CreateIndex(
                name: "IX_Modules_QuestionId",
                table: "Modules",
                column: "QuestionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Modules_Questions_QuestionId",
                table: "Modules",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_Modules_ModuleId",
                table: "Questions",
                column: "ModuleId",
                principalTable: "Modules",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
