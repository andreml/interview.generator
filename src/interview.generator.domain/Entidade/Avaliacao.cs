using interview.generator.domain.Entidade.Common;

namespace interview.generator.domain.Entidade
{
    public class Avaliacao : EntidadeBase
    {
        public Usuario Candidato { get; set; }
        public Questionario Questionario { get; set; }
        public DateTime DataAplicacao { get; set; }
        public string ObservacaoAplicador { get; set; }
        public decimal Nota { get; set; }
        public ICollection<RespostaAvaliacao> Respostas { get; set; }

        public Avaliacao()
        {
            DataAplicacao = DateTime.Now;
        }

        public void AdicionarObservacao(string observacao) =>
            ObservacaoAplicador = observacao;

        public void CalcularNota()
        {
            var totalPerguntas = Questionario.Perguntas.Count;
            var acertos = Respostas.Where(r => r.AlternativaEscolhida.Correta).Count();

            Nota = decimal.Round(((decimal)acertos / totalPerguntas) * 100, 2);
        }
    }
}
