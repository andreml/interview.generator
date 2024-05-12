using InterviewGenerator.Domain.Entidade.Common;

namespace InterviewGenerator.Domain.Entidade;

public class Questionario : EntidadeBase
{
    public string Nome { get; set; } = default!;
    public Guid UsuarioCriacaoId { get; set; }
    public DateTime DataCriacao { get; set; }

    public List<Pergunta> Perguntas { get; set; } = new();

    public List<Avaliacao> Avaliacoes { get; set; } = default!;

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

    public decimal? MediaNota()
    {
        if (Avaliacoes is null || !Avaliacoes.Any(a => a.Respondida))
            return null;

        var media = Avaliacoes
                        .Where(a => a.Respondida)
                        .Select(a => a.Nota!.Value)
                        .Average();

        return decimal.Round(media, 2);
    }
}          
