using interview.generator.domain.Entidade.Common;

namespace interview.generator.domain.Entidade
{
    public class RespostaAvaliacao : EntidadeBase
    {
        public Pergunta Pergunta { get; set; } = default!;
        public Alternativa AlternativaEscolhida { get; set; } = default!;

        public RespostaAvaliacao()
        {
        }

        public RespostaAvaliacao(Pergunta pergunta, Alternativa alternativaEscolhida)
        {
            Pergunta = pergunta;
            AlternativaEscolhida = alternativaEscolhida;
        }
    }
}
