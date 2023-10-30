using System.Text.Json.Serialization;

namespace InterviewGenerator.Domain.Entidade.Common
{
    public class EntidadeBase
    {
        [JsonPropertyOrder(1)]
        [JsonPropertyName("Id")] public Guid Id { get; set; }
    }
}