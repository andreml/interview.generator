using Azure.Core;
using interview.generator.domain.Entidade.Common;
using interview.generator.domain.Enum;
using System.Security.Claims;

namespace interview.generator.domain.Entidade
{
    public class Usuario : EntidadeBase
    {
        public string Cpf { get; set; }
        public string Nome { get; set; }
        public Perfil Perfil { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }

        public Usuario()
        {
        }

        public DateTime VerificaValidadeTokenCandidato()
        {
            switch (this.Perfil)
            {
                case Perfil.Avaliador:
                    return DateTime.Now.AddYears(1);
                case Perfil.Candidato:
                    return DateTime.Now.AddDays(1);
                default:
                    return DateTime.Now;
            }
        }

    }
}