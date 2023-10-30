using InterviewGenerator.Domain.Entidade.Common;

namespace InterviewGenerator.Domain.Entidade
{
    public class Avaliacao : EntidadeBase
    {
        public Usuario Candidato { get; set; } = default!;
        public Questionario Questionario { get; set; } = default!;
        public DateTime DataAplicacao { get; set; }
        public string ObservacaoAplicador { get; set; } = default!;
        public decimal Nota { get; set; }
        public ICollection<RespostaAvaliacao> Respostas { get; set; } = default!;

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
