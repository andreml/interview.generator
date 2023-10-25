using interview.generator.domain.Entidade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace interview.generator.application.ViewModels
{
    public class QuestionarioViewModel
    {
        public string Nome { get; set; }
        public Guid UsuarioCriacaoId { get; set; }
        public Guid TipoQuestionarioId { get; set; }
        public DateTime DataCriacao { get; set; }
        public ICollection<PerguntaQuestionario> Perguntas { get; set; }
    }
}
