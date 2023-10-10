using System.ComponentModel.DataAnnotations.Schema;

namespace interview.generator.domain.Entidade
{
    public class Usuario : CommonEntity
    {
        public string Cpf { get; set; }
        public string Nome { get; set; }
        public string PerfilId { get; set; }
    }
}