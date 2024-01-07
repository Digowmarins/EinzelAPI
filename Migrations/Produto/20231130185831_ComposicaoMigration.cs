using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EinzelAPI.Migrations.Produto
{
    /// <inheritdoc />
    public partial class ComposicaoMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Composicao",
                table: "Produtos",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Composicao",
                table: "Produtos");
        }
    }
}
