namespace interview.generator.application.ViewModels
{
    public class PerguntaViewModel
    {
        public Guid Id { get; set; } = default!;
        public string Areaconhecimento { get; set; } = default!;
        public string Descricao { get; set; } = default!;
        public ICollection<AlternativaViewModel> Alternativas { get; set; } = default!;
    }

    public class AlternativaViewModel
    {
        public AlternativaViewModel(string descricao, bool correta)
        {
            Descricao = descricao;
            Correta = correta;
        }

        public string Descricao { get; set; }
        public bool Correta { get; set; }
    }
}
