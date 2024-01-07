using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Einzel.Migrations
{
    /// <inheritdoc />
    public partial class userMigration2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Compras_AspNetUsers_UsuarioId1",
                table: "Compras");

            migrationBuilder.DropIndex(
                name: "IX_Compras_UsuarioId1",
                table: "Compras");

            migrationBuilder.DropColumn(
                name: "UsuarioId1",
                table: "Compras");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UsuarioId1",
                table: "Compras",
                type: "varchar(255)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Compras_UsuarioId1",
                table: "Compras",
                column: "UsuarioId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Compras_AspNetUsers_UsuarioId1",
                table: "Compras",
                column: "UsuarioId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
