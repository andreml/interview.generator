using InterviewGenerator.Domain.Enum;

namespace InterviewGenerator.Application.ViewModels
{
    public class LoginViewModel
    {
        public LoginViewModel(string nome, Perfil perfil, string token)
        {
            Nome = nome;
            Perfil = perfil;
            Token = token;
        }

        public string Nome { get; set; }
        public Perfil Perfil { get; set; }
        public string Token { get; set; }
    }
}
