namespace InterviewGenerator.Application.ViewModels;

public class ResponderAvaliacaoViewModel
{
    public Guid QuestionarioId { get; set; }
    public Guid AvaliacaoId { get; set; }
    public string NomeQuestionario { get; set; } = default!;
    public ICollection<PerguntaQuestionarioAvaliacao> Perguntas { get; set; } = default!;
}

public class PerguntaQuestionarioAvaliacao
{
    public Guid Id { get; set; }
    public string Descricao { get; set;} = default!;
    public ICollection<AlternativaPerguntaQuestionarioAvaliacao> Alternativas {  get; set; } = default!;
}

public class AlternativaPerguntaQuestionarioAvaliacao
{
    public Guid Id { get; set; }
    public string Descricao { get; set; } = default!;
}
