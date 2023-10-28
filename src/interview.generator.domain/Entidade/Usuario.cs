using interview.generator.domain.Entidade.Common;
using interview.generator.domain.Enum;
using interview.generator.domain.Utils;

namespace interview.generator.domain.Entidade
{
    public class Usuario : EntidadeBase
    {
        public string Cpf { get; set; } = default!;
        public string Nome { get; set; } = default!;
        public Perfil Perfil { get; set; }
        public string Login { get; set; } = default!;
        public string Senha { get; set; } = default!;

        public Usuario()
        {
        }

        public Usuario(string cpf, string nome, Perfil perfil, string login, string senha)
        {
            Cpf = cpf;
            Nome = nome;
            Perfil = perfil;
            Login = login;
            Senha = Encryptor.Encrypt(senha);
        }

        public DateTime VerificaValidadeTokenUsuario()
        {
            return Perfil switch
            {
                Perfil.Avaliador => DateTime.Now.AddYears(1),
                Perfil.Candidato => DateTime.Now.AddDays(1),
                _ => DateTime.Now,
            };
        }

        public void Atualizar(string cpf, string nome, string login, string senha)
        {
            Cpf = cpf;
            Nome = nome;
            Login = login;
            Senha = Encryptor.Encrypt(senha);
        }
    }
}