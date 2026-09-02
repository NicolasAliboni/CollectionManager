using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CollectionManager.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddAuditFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DataAtualizacao",
                table: "Status",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataCadastro",
                table: "Status",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DataExclusao",
                table: "Status",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataAtualizacao",
                table: "Plataformas",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataCadastro",
                table: "Plataformas",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DataExclusao",
                table: "Plataformas",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataAtualizacao",
                table: "Marcas",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataCadastro",
                table: "Marcas",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DataExclusao",
                table: "Marcas",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataAtualizacao",
                table: "Itens",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataCadastro",
                table: "Itens",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DataExclusao",
                table: "Itens",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataAtualizacao",
                table: "Franquias",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataCadastro",
                table: "Franquias",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DataExclusao",
                table: "Franquias",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataAtualizacao",
                table: "Estados",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataCadastro",
                table: "Estados",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DataExclusao",
                table: "Estados",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataAtualizacao",
                table: "Editoras",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataCadastro",
                table: "Editoras",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DataExclusao",
                table: "Editoras",
                type: "timestamp with time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataAtualizacao",
                table: "Status");

            migrationBuilder.DropColumn(
                name: "DataCadastro",
                table: "Status");

            migrationBuilder.DropColumn(
                name: "DataExclusao",
                table: "Status");

            migrationBuilder.DropColumn(
                name: "DataAtualizacao",
                table: "Plataformas");

            migrationBuilder.DropColumn(
                name: "DataCadastro",
                table: "Plataformas");

            migrationBuilder.DropColumn(
                name: "DataExclusao",
                table: "Plataformas");

            migrationBuilder.DropColumn(
                name: "DataAtualizacao",
                table: "Marcas");

            migrationBuilder.DropColumn(
                name: "DataCadastro",
                table: "Marcas");

            migrationBuilder.DropColumn(
                name: "DataExclusao",
                table: "Marcas");

            migrationBuilder.DropColumn(
                name: "DataAtualizacao",
                table: "Itens");

            migrationBuilder.DropColumn(
                name: "DataCadastro",
                table: "Itens");

            migrationBuilder.DropColumn(
                name: "DataExclusao",
                table: "Itens");

            migrationBuilder.DropColumn(
                name: "DataAtualizacao",
                table: "Franquias");

            migrationBuilder.DropColumn(
                name: "DataCadastro",
                table: "Franquias");

            migrationBuilder.DropColumn(
                name: "DataExclusao",
                table: "Franquias");

            migrationBuilder.DropColumn(
                name: "DataAtualizacao",
                table: "Estados");

            migrationBuilder.DropColumn(
                name: "DataCadastro",
                table: "Estados");

            migrationBuilder.DropColumn(
                name: "DataExclusao",
                table: "Estados");

            migrationBuilder.DropColumn(
                name: "DataAtualizacao",
                table: "Editoras");

            migrationBuilder.DropColumn(
                name: "DataCadastro",
                table: "Editoras");

            migrationBuilder.DropColumn(
                name: "DataExclusao",
                table: "Editoras");
        }
    }
}
