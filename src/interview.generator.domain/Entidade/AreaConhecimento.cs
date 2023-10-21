using interview.generator.domain.Entidade.Common;

namespace interview.generator.domain.Entidade
{
    public class AreaConhecimento : EntidadeBase
    {
        public string Descricao { get; set; }
        public ICollection<Pergunta>? Perguntas { get; set; }

        public Guid UsuarioId { get; set; }

        public AreaConhecimento()
        {
        }
    }
}
