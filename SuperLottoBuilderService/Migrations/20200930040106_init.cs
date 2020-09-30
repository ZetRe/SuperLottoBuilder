using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SuperLottoBuilderService.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GenerateNumbers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Period = table.Column<string>(maxLength: 50, nullable: true),
                    Code = table.Column<string>(maxLength: 50, nullable: true),
                    CreateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GenerateNumbers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WinningNumbers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Code = table.Column<string>(maxLength: 50, nullable: true),
                    Period = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WinningNumbers", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GenerateNumbers_Period",
                table: "GenerateNumbers",
                column: "Period");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GenerateNumbers");

            migrationBuilder.DropTable(
                name: "WinningNumbers");
        }
    }
}
