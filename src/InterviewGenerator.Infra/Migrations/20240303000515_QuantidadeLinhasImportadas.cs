using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InterviewGenerator.Infra.Migrations
{
    /// <inheritdoc />
    public partial class QuantidadeLinhasImportadas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "QuantidadeLinhasImportadas",
                table: "ControleImportacaoPerguntas",
                type: "INT",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QuantidadeLinhasImportadas",
                table: "ControleImportacaoPerguntas");
        }
    }
}
