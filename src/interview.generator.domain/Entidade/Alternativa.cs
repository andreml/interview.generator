using interview.generator.domain.Entidade.Common;

namespace interview.generator.domain.Entidade
{
    public class Alternativa : EntidadeBase
    {
        public Guid PerguntaId { get; set; }
        public bool Correta { get; set; }
        public string Descricao { get; set; }

        public ICollection<RespostaAvaliacao>? RespostasAvaliacao { get; set; }
        
        public Alternativa()
        {    
        }

        public Alternativa(string descricao, bool correta)
        {
            Correta = correta;
            Descricao = descricao;
        }
    }
}
