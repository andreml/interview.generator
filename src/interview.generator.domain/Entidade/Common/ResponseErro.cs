using System.Text.Json.Serialization;

namespace interview.generator.domain.Entidade.Common
{
    public class ResponseErro
    {
        public int Codigo { get; set; }
        public List<string> Mensagens { get; set; } = new();

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Excecao { get; set; }
    }
}
