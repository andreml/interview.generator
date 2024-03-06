using InterviewGenerator.Domain.Entidade;
using InterviewGenerator.Domain.Enum;
using System.Text.Json.Serialization;

namespace InterviewGenerator.Application.ViewModels
{
    public class ControleImportacaoPerguntasViewModel
    {
        public Guid UsuarioId { get; set; }
        public DateTime DataUpload { get; set; }
        [JsonIgnore]
        public DateTime? DataFimImportacao { get; set; }
        public StatusImportacao StatusImportacao { get; set; }
        public string? ErrosImportacao { get; set; }
        public string NomeArquivo { get; set; } = default!;
        public int QuantidadeLinhasImportadas { get; set; }
        [JsonIgnore]
        public ICollection<LinhasArquivoViewModel>? LinhasArquivos { get; set; }
        public Guid IdArquivo { get; set; }
    }

    public class LinhasArquivoViewModel
    {
        public Guid IdControleImportacao { get; set; }
        public DateTime? DataProcessamento { get; set; }
        public string? Erro { get; set; }
        public int NumeroLinha { get; set; }
    }
}
