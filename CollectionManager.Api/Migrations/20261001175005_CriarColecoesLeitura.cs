using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CollectionManager.Api.Migrations
{
    /// <inheritdoc />
    public partial class CriarColecoesLeitura : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Leituras_Editoras_EditoraBrasilId",
                table: "Leituras");

            migrationBuilder.DropForeignKey(
                name: "FK_Leituras_Editoras_EditoraExteriorId",
                table: "Leituras");

            migrationBuilder.DropForeignKey(
                name: "FK_Leituras_Itens_ItemId",
                table: "Leituras");

            migrationBuilder.DropForeignKey(
                name: "FK_Leituras_Status_StatusId",
                table: "Leituras");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Leituras",
                table: "Leituras");

            migrationBuilder.DropIndex(
                name: "IX_Leituras_EditoraBrasilId",
                table: "Leituras");

            migrationBuilder.DropIndex(
                name: "IX_Leituras_EditoraExteriorId",
                table: "Leituras");

            migrationBuilder.DropColumn(
                name: "ItemId",
                table: "Leituras");

            migrationBuilder.DropColumn(
                name: "Autor",
                table: "Leituras");

            migrationBuilder.DropColumn(
                name: "EditoraBrasilId",
                table: "Leituras");

            migrationBuilder.DropColumn(
                name: "Lingua",
                table: "Leituras");

            migrationBuilder.RenameColumn(
                name: "VolumeAte",
                table: "Leituras",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "Tipo",
                table: "Leituras",
                newName: "EstadoId");

            migrationBuilder.RenameColumn(
                name: "StatusId",
                table: "Leituras",
                newName: "ColecaoLeituraId");

            migrationBuilder.RenameColumn(
                name: "EditoraExteriorId",
                table: "Leituras",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_Leituras_StatusId",
                table: "Leituras",
                newName: "IX_Leituras_ColecaoLeituraId");

            migrationBuilder.AlterColumn<decimal>(
                name: "Volume",
                table: "Leituras",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Leituras",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<string>(
                name: "CodigoEAN",
                table: "Leituras",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "DataAquisicao",
                table: "Leituras",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<DateTime>(
                name: "DataAtualizacao",
                table: "Leituras",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataCadastro",
                table: "Leituras",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DataExclusao",
                table: "Leituras",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "DataLancamento",
                table: "Leituras",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<string>(
                name: "Observacoes",
                table: "Leituras",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Titulo",
                table: "Leituras",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "ValorAquisicao",
                table: "Leituras",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Leituras",
                table: "Leituras",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "ColecaoLeitura",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome = table.Column<string>(type: "text", nullable: false),
                    Tipo = table.Column<int>(type: "integer", nullable: false),
                    FranquiaId = table.Column<int>(type: "integer", nullable: true),
                    EditoraExteriorId = table.Column<int>(type: "integer", nullable: false),
                    EditoraBrasilId = table.Column<int>(type: "integer", nullable: false),
                    Autor = table.Column<string>(type: "text", nullable: false),
                    Lingua = table.Column<string>(type: "text", nullable: false),
                    VolumeAte = table.Column<int>(type: "integer", nullable: false),
                    Observacoes = table.Column<string>(type: "text", nullable: true),
                    DataCadastro = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DataExclusao = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ColecaoLeitura", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ColecaoLeitura_Editoras_EditoraBrasilId",
                        column: x => x.EditoraBrasilId,
                        principalTable: "Editoras",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ColecaoLeitura_Editoras_EditoraExteriorId",
                        column: x => x.EditoraExteriorId,
                        principalTable: "Editoras",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ColecaoLeitura_Franquias_FranquiaId",
                        column: x => x.FranquiaId,
                        principalTable: "Franquias",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Leituras_EstadoId",
                table: "Leituras",
                column: "EstadoId");

            migrationBuilder.CreateIndex(
                name: "IX_ColecaoLeitura_EditoraBrasilId",
                table: "ColecaoLeitura",
                column: "EditoraBrasilId");

            migrationBuilder.CreateIndex(
                name: "IX_ColecaoLeitura_EditoraExteriorId",
                table: "ColecaoLeitura",
                column: "EditoraExteriorId");

            migrationBuilder.CreateIndex(
                name: "IX_ColecaoLeitura_FranquiaId",
                table: "ColecaoLeitura",
                column: "FranquiaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Leituras_ColecaoLeitura_ColecaoLeituraId",
                table: "Leituras",
                column: "ColecaoLeituraId",
                principalTable: "ColecaoLeitura",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Leituras_Estados_EstadoId",
                table: "Leituras",
                column: "EstadoId",
                principalTable: "Estados",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Leituras_ColecaoLeitura_ColecaoLeituraId",
                table: "Leituras");

            migrationBuilder.DropForeignKey(
                name: "FK_Leituras_Estados_EstadoId",
                table: "Leituras");

            migrationBuilder.DropTable(
                name: "ColecaoLeitura");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Leituras",
                table: "Leituras");

            migrationBuilder.DropIndex(
                name: "IX_Leituras_EstadoId",
                table: "Leituras");

            migrationBuilder.DropColumn(
                name: "CodigoEAN",
                table: "Leituras");

            migrationBuilder.DropColumn(
                name: "DataAquisicao",
                table: "Leituras");

            migrationBuilder.DropColumn(
                name: "DataAtualizacao",
                table: "Leituras");

            migrationBuilder.DropColumn(
                name: "DataCadastro",
                table: "Leituras");

            migrationBuilder.DropColumn(
                name: "DataExclusao",
                table: "Leituras");

            migrationBuilder.DropColumn(
                name: "DataLancamento",
                table: "Leituras");

            migrationBuilder.DropColumn(
                name: "Observacoes",
                table: "Leituras");

            migrationBuilder.DropColumn(
                name: "Titulo",
                table: "Leituras");

            migrationBuilder.DropColumn(
                name: "ValorAquisicao",
                table: "Leituras");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Leituras",
                newName: "VolumeAte");

            migrationBuilder.RenameColumn(
                name: "EstadoId",
                table: "Leituras",
                newName: "Tipo");

            migrationBuilder.RenameColumn(
                name: "ColecaoLeituraId",
                table: "Leituras",
                newName: "StatusId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Leituras",
                newName: "EditoraExteriorId");

            migrationBuilder.RenameIndex(
                name: "IX_Leituras_ColecaoLeituraId",
                table: "Leituras",
                newName: "IX_Leituras_StatusId");

            migrationBuilder.AlterColumn<int>(
                name: "Volume",
                table: "Leituras",
                type: "integer",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<int>(
                name: "EditoraExteriorId",
                table: "Leituras",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<int>(
                name: "ItemId",
                table: "Leituras",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Autor",
                table: "Leituras",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "EditoraBrasilId",
                table: "Leituras",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Lingua",
                table: "Leituras",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Leituras",
                table: "Leituras",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Leituras_EditoraBrasilId",
                table: "Leituras",
                column: "EditoraBrasilId");

            migrationBuilder.CreateIndex(
                name: "IX_Leituras_EditoraExteriorId",
                table: "Leituras",
                column: "EditoraExteriorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Leituras_Editoras_EditoraBrasilId",
                table: "Leituras",
                column: "EditoraBrasilId",
                principalTable: "Editoras",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Leituras_Editoras_EditoraExteriorId",
                table: "Leituras",
                column: "EditoraExteriorId",
                principalTable: "Editoras",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Leituras_Itens_ItemId",
                table: "Leituras",
                column: "ItemId",
                principalTable: "Itens",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Leituras_Status_StatusId",
                table: "Leituras",
                column: "StatusId",
                principalTable: "Status",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
