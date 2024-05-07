namespace InterviewGenerator.Application.ViewModels;

public class NotasQuestionariosViewModel
{
    public Guid Id { get; set; }
    public ICollection<AvaliacaoQuestionarioViewModel> Notas { get; set; } = default!;
}

public class AvaliacaoQuestionarioViewModel
{
    public AvaliacaoQuestionarioViewModel(Guid idAvaliacao, string nomeCandidato, decimal nota)
    {
        IdAvaliacao = idAvaliacao;
        NomeCandidato = nomeCandidato;
        Nota = nota;
    }

    public Guid IdAvaliacao { get; set; }
    public string NomeCandidato { get; set; }
    public decimal Nota { get; set; }
}
