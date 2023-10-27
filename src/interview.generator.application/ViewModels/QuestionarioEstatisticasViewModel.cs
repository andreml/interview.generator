namespace interview.generator.application.ViewModels
{
    public class QuestionarioEstatisticasViewModel
    {
        public Guid Id { get; set; }
        public string Nome { get; set; } = default!;
        public int AvaliacoesRespondidas { get; set; }
        public decimal MediaNota { get; set; }
        public MaiorNotaViewModel MaiorNota { get; set; }
    }

    public class MaiorNotaViewModel
    {
        public ICollection<string> Candidatos { get; set; }
        public decimal Nota { get; set; }
    }
}
