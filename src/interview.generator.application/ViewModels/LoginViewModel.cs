using interview.generator.domain.Enum;

namespace interview.generator.application.ViewModels
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
