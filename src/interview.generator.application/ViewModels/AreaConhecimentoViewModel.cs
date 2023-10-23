namespace interview.generator.application.ViewModels
{
    public class AreaConhecimentoViewModel
    {
        public AreaConhecimentoViewModel(Guid id, string descricao, int perguntasCadastradas)
        {
            Id = id;
            Descricao = descricao;
            PerguntasCadastradas = perguntasCadastradas;
        }

        public Guid Id { get; set; }
        public string Descricao { get; set; }
        public int PerguntasCadastradas { get; set; }
    }
}
