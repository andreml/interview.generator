using InterviewGenerator.Domain.Entidade.Common;

namespace InterviewGenerator.Domain.Entidade
{
    public class LinhasArquivo : EntidadeBase
    {
        public Guid IdControleImportacao { get; set; }
        public DateTime? DataProcessamento { get; set; }
        public string? Erro { get; set; }
        public int NumeroLinha { get; set; }

    }
}
