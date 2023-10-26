using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace interview.generator.infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class CorrecaoPerguntaQuestionario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_PerguntaQuestionario_PerguntaId",
                table: "PerguntaQuestionario",
                column: "PerguntaId");

            migrationBuilder.AddForeignKey(
                name: "FK_PerguntaQuestionario_Pergunta_PerguntaId",
                table: "PerguntaQuestionario",
                column: "PerguntaId",
                principalTable: "Pergunta",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PerguntaQuestionario_Questionario_PerguntaId",
                table: "PerguntaQuestionario",
                column: "PerguntaId",
                principalTable: "Questionario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PerguntaQuestionario_Pergunta_PerguntaId",
                table: "PerguntaQuestionario");

            migrationBuilder.DropForeignKey(
                name: "FK_PerguntaQuestionario_Questionario_PerguntaId",
                table: "PerguntaQuestionario");

            migrationBuilder.DropIndex(
                name: "IX_PerguntaQuestionario_PerguntaId",
                table: "PerguntaQuestionario");
        }
    }
}
