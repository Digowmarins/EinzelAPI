using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Einzel.Migrations
{
    /// <inheritdoc />
    public partial class CompraMigration7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "produtoNome",
                table: "Compras",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Compras",
                keyColumn: "produtoNome",
                keyValue: null,
                column: "produtoNome",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "produtoNome",
                table: "Compras",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }
    }
}
