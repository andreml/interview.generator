using System.Text.Json.Serialization;

namespace InterviewGenerator.Domain.Enum;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum StatusLinhaArquivo
{
    Pendente = 1,
    Concluida = 2,
    Erro = 3
}
