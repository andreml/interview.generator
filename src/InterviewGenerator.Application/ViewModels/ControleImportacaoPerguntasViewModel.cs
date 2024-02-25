﻿using InterviewGenerator.Domain.Enum;

namespace InterviewGenerator.Application.ViewModels
{
    public class ControleImportacaoPerguntasViewModel
    {
        public DateTime DataUpload { get; set; }
        public DateTime DataFimImportacao { get; set; }
        public string NomeArquivo { get; set; } = default!;
        public StatusImportacao StatusImportacao { get; set; }
        public ICollection<string>? ErrosImportacao { get; set; }
    }
}
