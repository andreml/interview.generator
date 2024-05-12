namespace InterviewGenerator.Application.ViewModels;

public class AvaliacaoCandidatoViewModel
{
    public Guid Id { get; set; }
    public string NomeQuestionario { get; set; } = default!;
    public DateTime DataEnvio { get; set; }
    public DateTime? DataResposta { get; set; }
    public decimal? Nota { get; set; }
    public bool Respondido { get; set; }
}
