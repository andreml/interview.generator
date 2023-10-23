using interview.generator.domain.Entidade.Common;

namespace interview.generator.domain.Entidade
{
    public class Candidato : EntidadeBase
    {
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string DataNascimento { get; set; }
    }
}