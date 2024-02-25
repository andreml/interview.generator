using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InterviewGenerator.Infra.Migrations
{
    /// <inheritdoc />
    public partial class AddNomeArquivoControleImportacaoPerguntas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NomeArquivo",
                table: "ControleImportacaoPerguntas",
                type: "VARCHAR(250)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NomeArquivo",
                table: "ControleImportacaoPerguntas");
        }
    }
}
