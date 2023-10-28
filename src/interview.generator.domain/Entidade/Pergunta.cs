using interview.generator.domain.Entidade.Common;

namespace interview.generator.domain.Entidade
{
    public class Pergunta : EntidadeBase
    {
        public string Descricao { get; set; } = default!;
        public Guid UsuarioCriacaoId { get; set; }
        public AreaConhecimento AreaConhecimento { get; set; } = default!;
        public List<Alternativa> Alternativas { get; set; } = new();

        public ICollection<Questionario>? Questionarios { get; set; }
        public ICollection<RespostaAvaliacao>? RespostasAvaliacao { get; set; }

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

        public void RemoverAlternativas()
        {
            Alternativas.Clear();
        }
    }
}
