﻿using InterviewGenerator.Domain.Entidade.Common;
using InterviewGenerator.Domain.Enum;

namespace InterviewGenerator.Domain.Entidade
{
    public class ControleImportacaoPerguntas : EntidadeBase
    {
        public Guid UsuarioId { get; set; }
        public DateTime DataUpload { get; set; }
        public DateTime DataFimImportacao { get; set; }
        public StatusImportacao StatusImportacao { get; set; }
        public string? ErrosImportacao { get; set; }
    }
}