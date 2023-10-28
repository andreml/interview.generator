using interview.generator.domain.Entidade.Common;

namespace interview.generator.domain.Entidade
{
    public class Questionario : EntidadeBase
    {
        public string Nome { get; set; }
        public Guid UsuarioCriacaoId { get; set; }
        public DateTime DataCriacao { get; set; }

        public List<Pergunta> Perguntas { get; set; } = new();

        public List<Avaliacao> Avaliacoes { get; set; }


        public Questionario()
        {
            DataCriacao = DateTime.Now;
        }

        public void AdicionarPergunta(Pergunta pergunta)
        {
            Perguntas.Add(pergunta);
        }

        public void RemoverPerguntas()
        {
            Perguntas.Clear();
        }
    }
}
