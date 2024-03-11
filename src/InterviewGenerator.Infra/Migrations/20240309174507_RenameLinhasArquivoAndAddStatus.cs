using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InterviewGenerator.Infra.Migrations
{
    /// <inheritdoc />
    public partial class RenameLinhasArquivoAndAddStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LinhasArquivo");

            migrationBuilder.DropColumn(
                name: "DataFimImportacao",
                table: "ControleImportacaoPerguntas");

            migrationBuilder.DropColumn(
                name: "ErrosImportacao",
                table: "ControleImportacaoPerguntas");

            migrationBuilder.DropColumn(
                name: "StatusImportacao",
                table: "ControleImportacaoPerguntas");

            migrationBuilder.CreateTable(
                name: "LinhaArquivo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdControleImportacao = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DataProcessamento = table.Column<DateTime>(type: "DATETIME", nullable: true),
                    Erro = table.Column<string>(type: "VARCHAR(500)", nullable: true),
                    NumeroLinha = table.Column<int>(type: "INT", nullable: false),
                    StatusImportacao = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LinhaArquivo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LinhaArquivo_ControleImportacaoPerguntas_IdControleImportacao",
                        column: x => x.IdControleImportacao,
                        principalTable: "ControleImportacaoPerguntas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LinhaArquivo_IdControleImportacao",
                table: "LinhaArquivo",
                column: "IdControleImportacao");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LinhaArquivo");

            migrationBuilder.AddColumn<DateTime>(
                name: "DataFimImportacao",
                table: "ControleImportacaoPerguntas",
                type: "DATETIME",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ErrosImportacao",
                table: "ControleImportacaoPerguntas",
                type: "VARCHAR(500)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StatusImportacao",
                table: "ControleImportacaoPerguntas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "LinhasArquivo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DataProcessamento = table.Column<DateTime>(type: "DATETIME", nullable: true),
                    Erro = table.Column<string>(type: "VARCHAR(500)", nullable: true),
                    IdControleImportacao = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NumeroLinha = table.Column<int>(type: "INT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LinhasArquivo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LinhasArquivo_ControleImportacaoPerguntas_IdControleImportacao",
                        column: x => x.IdControleImportacao,
                        principalTable: "ControleImportacaoPerguntas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LinhasArquivo_IdControleImportacao",
                table: "LinhasArquivo",
                column: "IdControleImportacao");
        }
    }
}
