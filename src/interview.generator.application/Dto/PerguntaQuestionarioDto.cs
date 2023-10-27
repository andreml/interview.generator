namespace interview.generator.application.Dto
{
    public class PerguntaQuestionarioDto
    {
        public Guid PerguntaId { get; set; }
        public int OrdemApresentacao { get; set; }

        public PerguntaQuestionarioDto(Guid perguntaId, int ordemApresentacao)
        {
            PerguntaId = perguntaId;
            OrdemApresentacao = ordemApresentacao;
        }
    }
}
