using System.Text.Json.Serialization;

namespace interview.generator.domain.Enum
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Perfil
    {
        Avaliador = 1,
        Candidato = 2
    }

    public static class Perfis
    {
        public const string Avaliador = "Avaliador";
        public const string Candidato = "Candidato";
    }
}
