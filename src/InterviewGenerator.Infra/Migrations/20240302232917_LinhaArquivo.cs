using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InterviewGenerator.Infra.Migrations
{
    /// <inheritdoc />
    public partial class LinhaArquivo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LinhasArquivo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdControleImportacao = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DataProcessamento = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    Erro = table.Column<string>(type: "VARCHAR(500)", nullable: true),
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LinhasArquivo");
        }
    }
}
