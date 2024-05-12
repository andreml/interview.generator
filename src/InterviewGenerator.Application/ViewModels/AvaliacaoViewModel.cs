namespace InterviewGenerator.Application.ViewModels;

public class AvaliacaoViewModel
{
    public Guid Id { get; set; }
    public string Candidato { get; set; } = default!;
    public string NomeQuestionario { get; set; } = default!;
    public Guid QuestionarioId { get; set; }
    public DateTime DataEnvio { get; set; }
    public DateTime? DataResposta { get; set; }
    public string ObservacaoAvaliador { get; set; } = default!;
    public decimal? Nota { get; set; }

    public bool Respondido { get; set; }

    public ICollection<RespostaAvaliacaoViewModel>? Respostas { get; set; } = default!;
}

public class RespostaAvaliacaoViewModel
{
    public string Pergunta { get; set; } = default!;
    public string RespostaEscolhida { get; set; } = default!;
    public bool Correta { get; set; }
}
