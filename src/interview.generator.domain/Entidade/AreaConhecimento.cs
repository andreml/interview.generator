using interview.generator.domain.Entidade.Common;

namespace interview.generator.domain.Entidade
{
    public class AreaConhecimento : EntidadeBase
    {
        public string Descricao { get; set; }
        public string UsuarioId { get; set; }
        public ICollection<Pergunta> Perguntas { get; set; }

        public AreaConhecimento()
        {
        }
    }
}
