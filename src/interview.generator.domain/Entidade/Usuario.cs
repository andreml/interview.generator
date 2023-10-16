using FluentValidation;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace interview.generator.domain.Entidade
{
    public class Usuario : Entidade
    {
        public string Cpf { get; set; }
        public string Nome { get; set; }
        public string PerfilId { get; set; }
    }

    public class UsuarioValidator : AbstractValidator<Usuario>
    {
        public UsuarioValidator()
        {
            RuleFor(x => x.Cpf).NotNull().Length(11);
            RuleFor(x => x.Nome).NotNull();
            RuleFor(x => x.PerfilId).NotNull();
        }
    }
}