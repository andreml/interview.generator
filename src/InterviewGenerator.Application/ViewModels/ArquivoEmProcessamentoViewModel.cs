namespace InterviewGenerator.Application.ViewModels;

public class ArquivoEmProcessamentoViewModel
{
    public Guid IdArquivo { get; set; }
    public string NomeArquivo { get; set; } = default!;
    public int Linhas { get; set; }
}
