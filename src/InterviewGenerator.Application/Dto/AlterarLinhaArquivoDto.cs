using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewGenerator.Application.Dto
{
    public class AlterarLinhaArquivoDto
    {
        public Guid IdControleImportacao { get; set; }
        public DateTime? DataProcessamento { get; set; }
        public string? Erro { get; set; }
        public int NumeroLinha { get; set; }
        public Guid Id { get; set; }
    }
}
