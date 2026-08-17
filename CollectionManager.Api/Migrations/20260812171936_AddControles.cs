using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CollectionManager.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddControles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Controles",
                columns: table => new
                {
                    ItemId = table.Column<int>(type: "integer", nullable: false),
                    Modelo = table.Column<string>(type: "text", nullable: false),
                    MarcaId = table.Column<int>(type: "integer", nullable: false),
                    PlataformaId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Controles", x => x.ItemId);
                    table.ForeignKey(
                        name: "FK_Controles_Itens_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Itens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Controles_Marcas_MarcaId",
                        column: x => x.MarcaId,
                        principalTable: "Marcas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Controles_Plataformas_PlataformaId",
                        column: x => x.PlataformaId,
                        principalTable: "Plataformas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Controles_MarcaId",
                table: "Controles",
                column: "MarcaId");

            migrationBuilder.CreateIndex(
                name: "IX_Controles_PlataformaId",
                table: "Controles",
                column: "PlataformaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Controles");
        }
    }
}
