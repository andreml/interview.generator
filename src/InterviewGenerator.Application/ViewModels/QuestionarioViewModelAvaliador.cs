namespace InterviewGenerator.Application.ViewModels;

public class QuestionarioViewModelAvaliador
{
    public Guid Id { get; set; }
    public string Nome { get; set; } = default!;
    public DateTime DataCriacao { get; set; }
    public int AvaliacoesEnviadas { get; set; }
    public ICollection<PerguntaQuestionarioViewModelAvaliador> Perguntas { get; set; } = default!;
}

public class PerguntaQuestionarioViewModelAvaliador
{
    public Guid Id { get; set; }
    public string Descricao { get; set; } = default!;
    public ICollection<AlternativaPerguntaQuestionarioViewModelAvaliador> Alternativas { get; set; } = default!;
}

public class AlternativaPerguntaQuestionarioViewModelAvaliador
{
    public Guid Id { get; set; }
    public string Descricao { get; set; } = default!;
    public bool Correta { get; set; }
}
