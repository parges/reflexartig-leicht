using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace kubaapi.Migrations
{
    public partial class addFiles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FileDatas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FileName = table.Column<string>(maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileDatas", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Anamnesen",
                keyColumn: "Id",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2019, 5, 9, 18, 16, 2, 263, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "TestungChapters",
                keyColumn: "Id",
                keyValue: 11,
                column: "ShortName",
                value: "visuelle Wahrnehmungsprüfung");

            migrationBuilder.UpdateData(
                table: "Testungen",
                keyColumn: "Id",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2019, 5, 9, 18, 16, 2, 263, DateTimeKind.Local));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FileDatas");

            migrationBuilder.UpdateData(
                table: "Anamnesen",
                keyColumn: "Id",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2019, 5, 8, 20, 12, 5, 889, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "TestungChapters",
                keyColumn: "Id",
                keyValue: 11,
                column: "ShortName",
                value: "VISUELLE WAHRNEHMUNGSÜBERPRÜFUNG");

            migrationBuilder.UpdateData(
                table: "Testungen",
                keyColumn: "Id",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2019, 5, 8, 20, 12, 5, 888, DateTimeKind.Local));
        }
    }
}
