using interview.generator.domain.Entidade.Common;

namespace interview.generator.domain.Entidade
{
    public class PerguntaQuestionario : EntidadeBase
    {
        public Pergunta Pergunta { get; set; }
        public int OrdemApresentacao { get; set; }
        public Questionario Questionario { get; set; }
        

        public PerguntaQuestionario()
        {
        }
    }
}
