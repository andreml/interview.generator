namespace interview.generator.domain.Entidade
{
    public class Candidato : interview.generator.domain.Entidade.Common.Entidade
    {
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string DataNascimento { get; set; }
    }
}