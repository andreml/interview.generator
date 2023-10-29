using InterviewGenerator.Domain.Entidade.Common;
using InterviewGenerator.Domain.Enum;
using InterviewGenerator.Domain.Utils;

namespace InterviewGenerator.Domain.Entidade
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