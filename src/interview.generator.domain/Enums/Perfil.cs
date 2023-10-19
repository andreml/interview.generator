using System.Text.Json.Serialization;

namespace interview.generator.domain.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Perfil
    {
        Avaliador = 1,
        Candidato = 2
    }
}
