namespace interview.generator.domain.Entidade.Common
{
    public class ResponseErro
    {
        public int Codigo { get; set; }
        public List<string> Mensagem { get; set; } = new();
        public string Excecao { get; set; } = "Ocorreu um erro";
    }
}
