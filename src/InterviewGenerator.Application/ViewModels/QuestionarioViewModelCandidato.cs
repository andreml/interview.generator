namespace InterviewGenerator.Application.ViewModels;

public class QuestionarioViewModelCandidato
{
    public Guid Id { get; set; }
    public string Nome { get; set; } = default!;
    public ICollection<PerguntaQuestionarioViewModelCandidato> Perguntas { get; set; } = default!;
}

public class PerguntaQuestionarioViewModelCandidato
{
    public Guid Id { get; set; }
    public string Descricao { get; set;} = default!;
    public ICollection<AlternativaPerguntaQuestionarioViewModelCandidato> Alternativas {  get; set; } = default!;
}

public class AlternativaPerguntaQuestionarioViewModelCandidato
{
    public Guid Id { get; set; }
    public string Descricao { get; set; } = default!;
}
