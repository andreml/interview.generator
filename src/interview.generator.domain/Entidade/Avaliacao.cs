using interview.generator.domain.Entidade.Common;

namespace interview.generator.domain.Entidade
{
    public class Avaliacao : EntidadeBase
    {
        public Guid CandidatoId { get; set; }
        public Guid QuestionarioId { get; set; }
        public DateTime DataAplicacao { get; set; }
        public string ObservacaoAplicador { get; set; }
        public ICollection<RespostaAvaliacao> Respostas { get; set; }

        public Avaliacao()
        {
        }
    }
}
