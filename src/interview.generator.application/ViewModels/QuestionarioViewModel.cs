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
        public ICollection<PerguntaQuestionarioViewModel> Perguntas { get; set; }

        public QuestionarioViewModel()
        {
            Perguntas = new List<PerguntaQuestionarioViewModel>();
        }
    }

    public class PerguntaQuestionarioViewModel
    {
        public PerguntaQuestionarioViewModel(string id, string perguntaId, string questionarioId, int ordemApresentacao, int peso)
        {
            Id = id;
            PerguntaId = perguntaId;
            QuestionarioId = questionarioId;
            OrdemApresentacao = ordemApresentacao;
            Peso = peso;
        }

        public string Id { get; set; }
        public string PerguntaId { get; set; }
        public string QuestionarioId { get; set;}
        public int OrdemApresentacao { get; set; }
        public int Peso { get; set; }
    }
}
