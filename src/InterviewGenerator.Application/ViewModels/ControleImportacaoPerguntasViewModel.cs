using InterviewGenerator.Domain.Enum;
using System.Text.Json.Serialization;

namespace InterviewGenerator.Application.ViewModels
{
    public class ControleImportacaoPerguntasViewModel
    {
        public Guid IdArquivo { get; set; }
        public DateTime DataUpload { get; set; }
        public StatusImportacao StatusImportacao { get; set; }
        public string NomeArquivo { get; set; } = default!;
        public ICollection<LinhasArquivoViewModel> LinhasArquivo { get; set; } = default!;
    }

    public class LinhasArquivoViewModel
    {
        public DateTime? DataProcessamento { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Erro { get; set; }
        public int NumeroLinha { get; set; }
        public StatusLinhaArquivo StatusImportacao { get; set; }
    }
}
