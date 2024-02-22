using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InterviewGenerator.Infra.Migrations
{
    /// <inheritdoc />
    public partial class AddControleImportacaoPerguntas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ControleImportacaoPerguntas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UsuarioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DataUpload = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataFimImportacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StatusImportacao = table.Column<int>(type: "int", nullable: false),
                    ErrosImportacao = table.Column<string>(type: "VARCHAR(500)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ControleImportacaoPerguntas", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ControleImportacaoPerguntas");
        }
    }
}
