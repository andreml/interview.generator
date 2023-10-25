using interview.generator.domain.Entidade.Common;
using System.Text.Json.Serialization;

namespace interview.generator.domain.Entidade
{
    public class Avaliacao : EntidadeBase
    {
        [JsonPropertyName("CandidatoId")] public Guid CandidatoId { get; set; }
        public Questionario Questionario { get; set; }
        [JsonPropertyName("DataAplicacao")] public DateTime DataAplicacao { get; set; }
        [JsonPropertyName("ObservacaoAplicador")] public string ObservacaoAplicador { get; set; }
        [JsonPropertyName("Respostas")] public ICollection<RespostaAvaliacao> Respostas { get; set; }

        public Avaliacao()
        {
        }

        public void AdicionarObservacao(string observacao) =>
            ObservacaoAplicador = observacao;
    }
}
