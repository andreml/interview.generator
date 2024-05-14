namespace InterviewGenerator.Application.ViewModels;

public class AvaliacoesQuestionarioViewModel
{
    public Guid IdQuestionario { get; set; }
    public decimal? MediaNota { get; set; }
    public ICollection<AvaliacaoViewModel> AvaliacoesRespondidas { get; set; } = default!;
    public ICollection<AvaliacaoViewModel> AvaliacoesPendentes { get; set; } = default!;

}

public class AvaliacaoViewModel
{
    public AvaliacaoViewModel(Guid idAvaliacao, string nomeCandidato, decimal? nota, DateTime dataEnvio, DateTime? dataResposta)
    {
        IdAvaliacao = idAvaliacao;
        NomeCandidato = nomeCandidato;
        Nota = nota;
        DataEnvio = dataEnvio;
        DataResposta = dataResposta;
    }

    public AvaliacaoViewModel(Guid idAvaliacao, string nomeCandidato,DateTime dataEnvio)
    {
        IdAvaliacao = idAvaliacao;
        NomeCandidato = nomeCandidato;
        DataEnvio = dataEnvio;
        Nota = null;
        DataResposta = null;
    }

    public Guid IdAvaliacao { get; set; }
    public string NomeCandidato { get; set; }
    public decimal? Nota { get; set; }
    public DateTime DataEnvio { get; set; }
    public DateTime? DataResposta { get; set; }
}
