using interview.generator.domain.Entidade.Common;

namespace interview.generator.domain.Entidade
{
    public class RespostaAvaliacao : EntidadeBase
    {
        public Guid AvaliacaoId { get; set; }
        public Guid PerguntaQuestionarioId { get; set; }
        public Guid AlternativaEscolhidaId { get; set; }

        public RespostaAvaliacao()
        {
        }
    }
}
