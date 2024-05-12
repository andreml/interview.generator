namespace InterviewGenerator.Application.ViewModels;

public class NotasQuestionariosViewModel
{
    public Guid Id { get; set; }

    public decimal? MediaNota { get; set; }
    public ICollection<AvaliacaoQuestionarioViewModel> Notas { get; set; } = default!;
    public ICollection<AvaliacaoQuestionarioViewModel> QuestionariosPendentes { get; set; } = default!;

}

public class AvaliacaoQuestionarioViewModel
{
    public AvaliacaoQuestionarioViewModel(Guid idAvaliacao, string nomeCandidato, decimal? nota, DateTime dataEnvio, DateTime? dataResposta)
    {
        IdAvaliacao = idAvaliacao;
        NomeCandidato = nomeCandidato;
        Nota = nota;
        DataEnvio = dataEnvio;
        DataResposta = dataResposta;
    }

    public AvaliacaoQuestionarioViewModel(Guid idAvaliacao, string nomeCandidato,DateTime dataEnvio)
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
