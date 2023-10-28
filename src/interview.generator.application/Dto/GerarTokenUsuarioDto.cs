using FluentValidation;

namespace interview.generator.application.Dto
{
    public class GerarTokenUsuarioDto
    {
        public GerarTokenUsuarioDto(string login, string senha)
        {
            Login = login;
            Senha = senha;
        }

        public string Login { get; set; }
        public string Senha { get; set; }
    }

    public class GerarTokenUsuarioDtoValidator : AbstractValidator<GerarTokenUsuarioDto>
    {
        public GerarTokenUsuarioDtoValidator()
        {
            RuleFor(x => x.Login)
                .NotNull().NotEmpty().WithMessage("Login é obrigatório");

            RuleFor(x => x.Senha)
                .NotNull().NotEmpty().WithMessage("Senha é obrigatória");
        }
    }
}
