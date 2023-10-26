using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace interview.generator.infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class CorrecaoQuestionario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PerguntaQuestionario_Questionario_PerguntaId",
                table: "PerguntaQuestionario");

            migrationBuilder.CreateIndex(
                name: "IX_PerguntaQuestionario_QuestionarioId",
                table: "PerguntaQuestionario",
                column: "QuestionarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_PerguntaQuestionario_Questionario_QuestionarioId",
                table: "PerguntaQuestionario",
                column: "QuestionarioId",
                principalTable: "Questionario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PerguntaQuestionario_Questionario_QuestionarioId",
                table: "PerguntaQuestionario");

            migrationBuilder.DropIndex(
                name: "IX_PerguntaQuestionario_QuestionarioId",
                table: "PerguntaQuestionario");

            migrationBuilder.AddForeignKey(
                name: "FK_PerguntaQuestionario_Questionario_PerguntaId",
                table: "PerguntaQuestionario",
                column: "PerguntaId",
                principalTable: "Questionario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
