using InterviewGenerator.Domain.Enum;

namespace InterviewGenerator.Application.Dto
{
    public class AlterarLinhaArquivoDto
    {
        public Guid IdControleImportacao { get; set; }
        public DateTime? DataProcessamento { get; set; }
        public string? Erro { get; set; }
        public int NumeroLinha { get; set; }
        public Guid Id { get; set; }
        public StatusLinhaArquivo StatusImportacao { get; set; }
    }
}
