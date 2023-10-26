using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace interview.generator.infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class RenameColumnAndRemoveTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TipoQuestionario");

            migrationBuilder.RenameColumn(
                name: "UsuarioId",
                table: "AreaConhecimento",
                newName: "UsuarioCriacaoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UsuarioCriacaoId",
                table: "AreaConhecimento",
                newName: "UsuarioId");

            migrationBuilder.CreateTable(
                name: "TipoQuestionario",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoQuestionario", x => x.Id);
                });
        }
    }
}
