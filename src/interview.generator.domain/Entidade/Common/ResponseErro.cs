namespace interview.generator.domain.Entidade.Common
{
    public class ResponseErro
    {
        public int Codigo { get; set; }
        public string Mensagem { get; set; }
        public string Excecao { get; set; } = "Ocorreu um erro";
    }
}
