namespace interview.generator.domain.Entidade
{
    public class ResponseSucesso<T>
    {
        public int Codigo { get; set; }
        public string Mensagem { get; set; }
        public T Data { get; set; }
    }
}