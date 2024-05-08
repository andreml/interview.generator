using System.Text.Json.Serialization;

namespace InterviewGenerator.Domain.Enum;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum StatusImportacao
{
    Pendente = 1,
    Concluida = 2,
    ConcluidaComErro = 3
}