using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewGenerator.Application.Dto
{
    public class ImportarArquivoDto
    {
        public AdicionarPerguntaDto? Pergunta { get; set; }
        public int NumeroLinha { get; set; }
        public Guid IdArquivo { get; set; }
    }
}
