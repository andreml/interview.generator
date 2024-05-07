using InterviewGenerator.Domain.Entidade.Common;

namespace InterviewGenerator.Domain.Entidade;

public class ControleImportacaoPerguntas : EntidadeBase
{
    public Guid UsuarioId { get; set; }
    public DateTime DataUpload { get; set; }
    public string NomeArquivo { get; set; } = default!;
    public int QuantidadeLinhasImportadas { get; set; }

    public ICollection<LinhaArquivo> LinhasArquivo { get; set; } = default!;
}
