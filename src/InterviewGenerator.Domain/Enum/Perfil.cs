using System.Text.Json.Serialization;

namespace InterviewGenerator.Domain.Enum
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
