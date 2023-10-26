using interview.generator.domain.Entidade.Common;

namespace interview.generator.domain.Entidade
{
    public class PerguntaQuestionario : EntidadeBase
    {
        public Guid PerguntaId { get; set; }
        public Guid QuestionarioId { get; set; }
        public int OrdemApresentacao { get; set; }
        public int Peso { get; set; }

        public Questionario Questionario { get; set; }
        

        public PerguntaQuestionario()
        {
        }
    }
}
