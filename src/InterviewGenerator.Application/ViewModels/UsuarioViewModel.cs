using InterviewGenerator.Domain.Enum;

namespace InterviewGenerator.Application.ViewModels;

public class UsuarioViewModel
{
    public UsuarioViewModel(Guid id, string nome, string cpf, string login, Perfil perfil)
    {
        Id = id;
        Nome = nome;
        Cpf = cpf;
        Login = login;
        Perfil = perfil;
    }

    public Guid Id { get; set; }
    public string Nome { get; set; }
    public string Cpf { get; set; }
    public string Login { get; set; }
    public Perfil Perfil { get; set; }
}
