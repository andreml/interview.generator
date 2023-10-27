namespace interview.generator.application.Dto
{
    public class PerguntaQuestionarioDto
    {
        public Guid PerguntaId { get; set; }
        public int OrdemApresentacao { get; set; }
        public int Peso { get; set; }

        public PerguntaQuestionarioDto(Guid perguntaId, int ordemApresentacao, int peso)
        {
            PerguntaId = perguntaId;
            OrdemApresentacao = ordemApresentacao;
            Peso = peso;
        }
    }
}
