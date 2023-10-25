using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace interview.generator.application.Dto
{
    public class AdicionarQuestionarioDto
    {
        public string Nome { get; set; }
        public Guid TipoQuestionarioId { get; set; }
        public DateTime DataCriacao { get; set; }
        public ICollection<PerguntaQuestionario> Perguntas { get; set; }

        public class PerguntaQuestionario
        {
            public Guid PerguntaId { get; set; }
            public int OrdemApresentacao { get; set; }
            public int Peso { get; set; }
        }
    }

    
}
