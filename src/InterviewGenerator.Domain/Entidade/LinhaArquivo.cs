using InterviewGenerator.Domain.Entidade.Common;
using InterviewGenerator.Domain.Enum;

namespace InterviewGenerator.Domain.Entidade;

public class LinhaArquivo : EntidadeBase
{
    public Guid IdControleImportacao { get; set; }
    public DateTime? DataProcessamento { get; set; }
    public string? Erro { get; set; }
    public int NumeroLinha { get; set; }
    public StatusLinhaArquivo StatusImportacao { get; set; }
}
