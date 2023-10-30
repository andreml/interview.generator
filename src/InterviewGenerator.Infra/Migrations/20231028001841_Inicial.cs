using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InterviewGenerator.Infra.Migrations
{
    /// <inheritdoc />
    public partial class Inicial : Migration
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
                    UsuarioCriacaoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AreaConhecimento", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Questionario",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "VARCHAR(200)", nullable: false),
                    UsuarioCriacaoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questionario", x => x.Id);
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
                name: "Avaliacao",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CandidatoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QuestionarioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DataAplicacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ObservacaoAplicador = table.Column<string>(type: "VARCHAR(500)", nullable: false),
                    Nota = table.Column<decimal>(type: "decimal(3,3)", precision: 3, scale: 3, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Avaliacao", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Avaliacao_Questionario_QuestionarioId",
                        column: x => x.QuestionarioId,
                        principalTable: "Questionario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Avaliacao_Usuario_CandidatoId",
                        column: x => x.CandidatoId,
                        principalTable: "Usuario",
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

            migrationBuilder.CreateTable(
                name: "QuestionarioPergunta",
                columns: table => new
                {
                    PerguntaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QuestionarioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionarioPergunta", x => new { x.PerguntaId, x.QuestionarioId });
                    table.ForeignKey(
                        name: "FK_QuestionarioPergunta_Pergunta_PerguntaId",
                        column: x => x.PerguntaId,
                        principalTable: "Pergunta",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuestionarioPergunta_Questionario_QuestionarioId",
                        column: x => x.QuestionarioId,
                        principalTable: "Questionario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RespostaAvaliacao",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PerguntaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AlternativaEscolhidaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AvaliacaoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RespostaAvaliacao", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RespostaAvaliacao_Alternativa_AlternativaEscolhidaId",
                        column: x => x.AlternativaEscolhidaId,
                        principalTable: "Alternativa",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RespostaAvaliacao_Avaliacao_AvaliacaoId",
                        column: x => x.AvaliacaoId,
                        principalTable: "Avaliacao",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RespostaAvaliacao_Pergunta_PerguntaId",
                        column: x => x.PerguntaId,
                        principalTable: "Pergunta",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Alternativa_PerguntaId",
                table: "Alternativa",
                column: "PerguntaId");

            migrationBuilder.CreateIndex(
                name: "IX_Avaliacao_CandidatoId",
                table: "Avaliacao",
                column: "CandidatoId");

            migrationBuilder.CreateIndex(
                name: "IX_Avaliacao_QuestionarioId",
                table: "Avaliacao",
                column: "QuestionarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Pergunta_AreaConhecimentoId",
                table: "Pergunta",
                column: "AreaConhecimentoId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionarioPergunta_QuestionarioId",
                table: "QuestionarioPergunta",
                column: "QuestionarioId");

            migrationBuilder.CreateIndex(
                name: "IX_RespostaAvaliacao_AlternativaEscolhidaId",
                table: "RespostaAvaliacao",
                column: "AlternativaEscolhidaId");

            migrationBuilder.CreateIndex(
                name: "IX_RespostaAvaliacao_AvaliacaoId",
                table: "RespostaAvaliacao",
                column: "AvaliacaoId");

            migrationBuilder.CreateIndex(
                name: "IX_RespostaAvaliacao_PerguntaId",
                table: "RespostaAvaliacao",
                column: "PerguntaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QuestionarioPergunta");

            migrationBuilder.DropTable(
                name: "RespostaAvaliacao");

            migrationBuilder.DropTable(
                name: "Alternativa");

            migrationBuilder.DropTable(
                name: "Avaliacao");

            migrationBuilder.DropTable(
                name: "Pergunta");

            migrationBuilder.DropTable(
                name: "Questionario");

            migrationBuilder.DropTable(
                name: "Usuario");

            migrationBuilder.DropTable(
                name: "AreaConhecimento");
        }
    }
}
