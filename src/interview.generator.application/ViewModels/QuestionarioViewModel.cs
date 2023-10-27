namespace interview.generator.application.ViewModels
{
    public class QuestionarioViewModel
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public DateTime DataCriacao { get; set; }
        public ICollection<PerguntaQuestionarioViewModel> Perguntas { get; set; }

        public QuestionarioViewModel()
        {
            Perguntas = new List<PerguntaQuestionarioViewModel>();
        }
    }

    public class PerguntaQuestionarioViewModel
    {
        public PerguntaQuestionarioViewModel(Guid id, int ordemApresentacao, int peso, string descricao)
        {
            Id = id;
            OrdemApresentacao = ordemApresentacao;
            Peso = peso;
            Descricao = descricao;
        }

        public Guid Id { get; set; }
        public int OrdemApresentacao { get; set; }
        public int Peso { get; set; }
        public string Descricao { get; set;}
    }
}
