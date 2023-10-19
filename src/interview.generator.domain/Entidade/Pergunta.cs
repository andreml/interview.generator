using interview.generator.domain.Entidade.Common;

namespace interview.generator.domain.Entidade
{
    public class Pergunta : EntidadeBase
    {
        public Guid AreaConhecimentoId { get; set; }
        public string Descricao { get; set; }
        public Guid UsuarioCriacaoId { get; set; }
        public AreaConhecimento AreaConhecimento { get; set; }
        public ICollection<Alternativa> Alternativas { get; set; }

        public Pergunta()
        { 
        }
    }
}
