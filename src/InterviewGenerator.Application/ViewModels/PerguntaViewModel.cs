namespace InterviewGenerator.Application.ViewModels;

public class PerguntaViewModel
{
    public Guid Id { get; set; }
    public string Areaconhecimento { get; set; } = default!;
    public string Descricao { get; set; } = default!;
    public ICollection<AlternativaViewModel> Alternativas { get; set; } = default!;
}

public class AlternativaViewModel
{
    public AlternativaViewModel(Guid id, string descricao, bool correta)
    {
        Id = id;
        Descricao = descricao;
        Correta = correta;
    }

    public Guid Id { get; set; }
    public string Descricao { get; set; }
    public bool Correta { get; set; }
}
