using interview.generator.domain.Entidade.Common;

namespace interview.generator.domain.Entidade
{
    public class Pergunta : EntidadeBase
    {
        public string Descricao { get; set; }
        public Guid UsuarioCriacaoId { get; set; }
        public AreaConhecimento AreaConhecimento { get; set; }
        public List<Alternativa> Alternativas { get; set; } = new();

        public Pergunta()
        { 
        }

        public Pergunta(AreaConhecimento areaConhecimento, string descricao, Guid usuarioCriacaoId)
        {
            AreaConhecimento = areaConhecimento;
            Descricao = descricao;
            UsuarioCriacaoId = usuarioCriacaoId;
        }

        public void AdicionarAlternativa(Alternativa alternativa)
        {
            Alternativas.Add(alternativa);
        }
    }
}
