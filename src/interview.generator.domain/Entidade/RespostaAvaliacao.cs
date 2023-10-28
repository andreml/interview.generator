using interview.generator.domain.Entidade.Common;

namespace interview.generator.domain.Entidade
{
    public class RespostaAvaliacao : EntidadeBase
    {
        public Pergunta Pergunta { get; set; }
        public Alternativa AlternativaEscolhida { get; set; }

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
