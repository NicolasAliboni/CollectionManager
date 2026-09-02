using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CollectionManager.Api.Migrations
{
    /// <inheritdoc />
    public partial class CampoAtivoEditoras : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Ativo",
                table: "Editoras",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ativo",
                table: "Editoras");
        }
    }
}
