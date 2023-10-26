using interview.generator.domain.Entidade.Common;

namespace interview.generator.domain.Entidade
{
    public class AreaConhecimento : EntidadeBase
    {
        public string Descricao { get; set; } = default!;
        public ICollection<Pergunta>? Perguntas { get; set; }

        public Guid UsuarioCriacaoId { get; set; }

        public AreaConhecimento()
        {
        }

        public void AlterarDescricao(string descricao) =>
            Descricao = descricao;
    }
}
