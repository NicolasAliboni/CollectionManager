using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CollectionManager.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddSpecificItemTypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Jogos",
                columns: table => new
                {
                    ItemId = table.Column<int>(type: "integer", nullable: false),
                    MarcaId = table.Column<int>(type: "integer", nullable: false),
                    PlataformaId = table.Column<int>(type: "integer", nullable: false),
                    StatusId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jogos", x => x.ItemId);
                    table.ForeignKey(
                        name: "FK_Jogos_Itens_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Itens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Jogos_Marcas_MarcaId",
                        column: x => x.MarcaId,
                        principalTable: "Marcas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Jogos_Plataformas_PlataformaId",
                        column: x => x.PlataformaId,
                        principalTable: "Plataformas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Jogos_Status_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Status",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Leituras",
                columns: table => new
                {
                    ItemId = table.Column<int>(type: "integer", nullable: false),
                    Tipo = table.Column<int>(type: "integer", nullable: false),
                    EditoraExteriorId = table.Column<int>(type: "integer", nullable: false),
                    EditoraBrasilId = table.Column<int>(type: "integer", nullable: false),
                    Autor = table.Column<string>(type: "text", nullable: false),
                    StatusId = table.Column<int>(type: "integer", nullable: false),
                    Lingua = table.Column<string>(type: "text", nullable: false),
                    ISBN13 = table.Column<string>(type: "text", nullable: true),
                    Volume = table.Column<int>(type: "integer", nullable: false),
                    VolumeAte = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Leituras", x => x.ItemId);
                    table.ForeignKey(
                        name: "FK_Leituras_Editoras_EditoraBrasilId",
                        column: x => x.EditoraBrasilId,
                        principalTable: "Editoras",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Leituras_Editoras_EditoraExteriorId",
                        column: x => x.EditoraExteriorId,
                        principalTable: "Editoras",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Leituras_Itens_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Itens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Leituras_Status_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Status",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Videogames",
                columns: table => new
                {
                    ItemId = table.Column<int>(type: "integer", nullable: false),
                    MarcaId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Videogames", x => x.ItemId);
                    table.ForeignKey(
                        name: "FK_Videogames_Itens_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Itens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Videogames_Marcas_MarcaId",
                        column: x => x.MarcaId,
                        principalTable: "Marcas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Jogos_MarcaId",
                table: "Jogos",
                column: "MarcaId");

            migrationBuilder.CreateIndex(
                name: "IX_Jogos_PlataformaId",
                table: "Jogos",
                column: "PlataformaId");

            migrationBuilder.CreateIndex(
                name: "IX_Jogos_StatusId",
                table: "Jogos",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Leituras_EditoraBrasilId",
                table: "Leituras",
                column: "EditoraBrasilId");

            migrationBuilder.CreateIndex(
                name: "IX_Leituras_EditoraExteriorId",
                table: "Leituras",
                column: "EditoraExteriorId");

            migrationBuilder.CreateIndex(
                name: "IX_Leituras_StatusId",
                table: "Leituras",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Videogames_MarcaId",
                table: "Videogames",
                column: "MarcaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Jogos");

            migrationBuilder.DropTable(
                name: "Leituras");

            migrationBuilder.DropTable(
                name: "Videogames");
        }
    }
}
