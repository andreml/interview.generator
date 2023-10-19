using interview.generator.domain.Entidade.Common;

namespace interview.generator.domain.Entidade
{
    public class Alternativa : EntidadeBase
    {
        public Guid PerguntaId { get; set; }
        public bool Correta { get; set; }
        public string Descricao { get; set; }
        
        public Alternativa()
        {    
        }
    }
}
