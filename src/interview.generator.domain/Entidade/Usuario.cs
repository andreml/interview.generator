using interview.generator.domain.Entidade.Common;
using interview.generator.domain.Enum;

namespace interview.generator.domain.Entidade
{
    public class Usuario : EntidadeBase
    {
        public string Cpf { get; set; }
        public string Nome { get; set; }
        public Perfil Perfil { get; set; }

        public Usuario()
        {
        }

        public void Atualizar(string cpf, string nome, Perfil perfil)
        {
            Cpf = cpf;
            Nome = nome;
            Perfil = perfil;
        }
    }
}