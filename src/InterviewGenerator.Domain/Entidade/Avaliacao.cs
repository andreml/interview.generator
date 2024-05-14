using InterviewGenerator.Domain.Entidade.Common;

namespace InterviewGenerator.Domain.Entidade;

public class Avaliacao : EntidadeBase
{
    public Usuario Candidato { get; set; } = default!;
    public Questionario Questionario { get; set; } = default!;
    public DateTime DataEnvio { get; set; }
    public DateTime? DataResposta { get; set; }
    public string ObservacaoAplicador { get; set; } = default!;
    public decimal? Nota { get; set; }
    public ICollection<RespostaAvaliacao>? Respostas { get; set; } = default!;
    public bool Respondida { get; set; }

    public Avaliacao()
    {
        DataEnvio = DateTime.Now;
    }

    public void AdicionarObservacao(string observacao) =>
        ObservacaoAplicador = observacao;

    public void CalcularNota()
    {
        var totalPerguntas = Questionario.Perguntas.Count;
        var acertos = Respostas!.Where(r => r.AlternativaEscolhida.Correta).Count();

        Nota = decimal.Round(((decimal)acertos / totalPerguntas) * 100, 2);
    }

    public void AdicionarRespostas(ICollection<RespostaAvaliacao> respostas)
    {
        Respostas = respostas;
        Respondida = true;
        DataResposta = DateTime.Now;
    }
}
