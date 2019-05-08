using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace kubaapi.Migrations
{
    public partial class shortnameChapterAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ShortName",
                table: "TestungChapters",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ShortName",
                table: "ReviewChapters",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Anamnesen",
                keyColumn: "Id",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2019, 5, 8, 20, 12, 5, 889, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "TestungChapters",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Name", "ShortName" },
                values: new object[] { "I. TESTS ZUR ÜBERPRÜFUNG DER GROBMOTORISCHEN KOORDINATION UND GLEICHGEWICHT", "GROBMOTORISCHE KOORDINATION UND GLEICHGEWICHT" });

            migrationBuilder.UpdateData(
                table: "TestungChapters",
                keyColumn: "Id",
                keyValue: 3,
                column: "ShortName",
                value: "KLEINHIRNFUNKTIONEN");

            migrationBuilder.UpdateData(
                table: "TestungChapters",
                keyColumn: "Id",
                keyValue: 4,
                column: "ShortName",
                value: "DYSDIADOCHOKINESE");

            migrationBuilder.UpdateData(
                table: "TestungChapters",
                keyColumn: "Id",
                keyValue: 8,
                column: "ShortName",
                value: "ABERRANTEN REFLEXEN");

            migrationBuilder.UpdateData(
                table: "TestungChapters",
                keyColumn: "Id",
                keyValue: 10,
                column: "ShortName",
                value: "AUGENMUSKELMOTORIK");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShortName",
                table: "TestungChapters");

            migrationBuilder.DropColumn(
                name: "ShortName",
                table: "ReviewChapters");

            migrationBuilder.UpdateData(
                table: "Anamnesen",
                keyColumn: "Id",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2019, 4, 21, 13, 8, 46, 584, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "TestungChapters",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "I. TESTS ZUR ÜBERPRÜFUNG DER GROBMOTORISCHEN KOORDINAION UND DES GLEICHGEWICHTS");

            migrationBuilder.UpdateData(
                table: "Testungen",
                keyColumn: "Id",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2019, 4, 21, 13, 8, 46, 584, DateTimeKind.Local));
        }
    }
}
