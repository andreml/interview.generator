using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace interview.generator.infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AreaConhecimento",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Descricao = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    UsuarioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AreaConhecimento", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Avaliacao",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CandidatoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QuestionarioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DataAplicacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ObservacaoAplicador = table.Column<string>(type: "VARCHAR(500)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Avaliacao", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PerguntaQuestionario",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PerguntaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QuestionarioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrdemApresentacao = table.Column<int>(type: "int", nullable: false),
                    Peso = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PerguntaQuestionario", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Questionario",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "VARCHAR(200)", nullable: false),
                    UsuarioCriacaoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TipoQuestionarioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questionario", x => x.Id);
                });

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

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Cpf = table.Column<string>(type: "VARCHAR(11)", nullable: false),
                    Nome = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    Perfil = table.Column<int>(type: "int", nullable: false),
                    Login = table.Column<string>(type: "VARCHAR(30)", nullable: false),
                    Senha = table.Column<string>(type: "VARCHAR(500)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pergunta",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Descricao = table.Column<string>(type: "VARCHAR(1000)", nullable: false),
                    UsuarioCriacaoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AreaConhecimentoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pergunta", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pergunta_AreaConhecimento_AreaConhecimentoId",
                        column: x => x.AreaConhecimentoId,
                        principalTable: "AreaConhecimento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RespostaAvaliacao",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AvaliacaoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PerguntaQuestionarioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AlternativaEscolhidaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RespostaAvaliacao", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RespostaAvaliacao_Avaliacao_AvaliacaoId",
                        column: x => x.AvaliacaoId,
                        principalTable: "Avaliacao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Alternativa",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PerguntaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Correta = table.Column<bool>(type: "bit", nullable: false),
                    Descricao = table.Column<string>(type: "VARCHAR(1000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alternativa", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Alternativa_Pergunta_PerguntaId",
                        column: x => x.PerguntaId,
                        principalTable: "Pergunta",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Alternativa_PerguntaId",
                table: "Alternativa",
                column: "PerguntaId");

            migrationBuilder.CreateIndex(
                name: "IX_Pergunta_AreaConhecimentoId",
                table: "Pergunta",
                column: "AreaConhecimentoId");

            migrationBuilder.CreateIndex(
                name: "IX_RespostaAvaliacao_AvaliacaoId",
                table: "RespostaAvaliacao",
                column: "AvaliacaoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Alternativa");

            migrationBuilder.DropTable(
                name: "PerguntaQuestionario");

            migrationBuilder.DropTable(
                name: "Questionario");

            migrationBuilder.DropTable(
                name: "RespostaAvaliacao");

            migrationBuilder.DropTable(
                name: "TipoQuestionario");

            migrationBuilder.DropTable(
                name: "Usuario");

            migrationBuilder.DropTable(
                name: "Pergunta");

            migrationBuilder.DropTable(
                name: "Avaliacao");

            migrationBuilder.DropTable(
                name: "AreaConhecimento");
        }
    }
}
