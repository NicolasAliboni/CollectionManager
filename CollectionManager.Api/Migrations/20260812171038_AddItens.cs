using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CollectionManager.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddItens : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Itens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome = table.Column<string>(type: "text", nullable: false),
                    DataLancamento = table.Column<DateOnly>(type: "date", nullable: false),
                    EstadoId = table.Column<int>(type: "integer", nullable: false),
                    CodigoEAN = table.Column<string>(type: "text", nullable: true),
                    DataAquisicao = table.Column<DateOnly>(type: "date", nullable: false),
                    ValorAquisicao = table.Column<decimal>(type: "numeric", nullable: true),
                    FranquiaId = table.Column<int>(type: "integer", nullable: true),
                    Observacoes = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Itens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Itens_Estados_EstadoId",
                        column: x => x.EstadoId,
                        principalTable: "Estados",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Itens_Franquias_FranquiaId",
                        column: x => x.FranquiaId,
                        principalTable: "Franquias",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Itens_EstadoId",
                table: "Itens",
                column: "EstadoId");

            migrationBuilder.CreateIndex(
                name: "IX_Itens_FranquiaId",
                table: "Itens",
                column: "FranquiaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Itens");
        }
    }
}
