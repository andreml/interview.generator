using InterviewGenerator.Domain.Entidade;
using Xunit;

namespace InterviewGenerator.UnitTests.Domain;

public class AvaliacaoTests
{
    [Theory]
    [MemberData(nameof(ObterAvaliacoes))]
    public void Avaliacao_NotaCalc(Avaliacao avaliacao, decimal notaEsperada)
    {
        // Act
        avaliacao.CalcularNota();

        // Assert
        Assert.Equal(notaEsperada, avaliacao.Nota);
    }

    public static IEnumerable<object[]> ObterAvaliacoes()
    {
        yield return new object[]
        {
            new Avaliacao
            {
                Respostas = ObterListaRespostaAvaliacao(2, 2),
                Questionario = new Questionario
                {
                    Perguntas = ObterListaPerguntas(4)
                }
            }, 50
        };

        yield return new object[]
        {
            new Avaliacao
            {
                Respostas = ObterListaRespostaAvaliacao(5,0),
                Questionario = new Questionario
                {
                    Perguntas = ObterListaPerguntas(5)
                }
            }, 100
        };

        yield return new object[]
        {
            new Avaliacao
            {
                Respostas = ObterListaRespostaAvaliacao(0, 20),
                Questionario = new Questionario
                {
                    Perguntas = ObterListaPerguntas(20)
                }
            }, 0
        };

        yield return new object[]
        {
            new Avaliacao
            {
                Respostas = ObterListaRespostaAvaliacao(8, 2),
                Questionario = new Questionario
                {
                    Perguntas = ObterListaPerguntas(10)
                }
            }, 80
        };
    }

    private static List<Pergunta> ObterListaPerguntas(int perguntas)
    {
        List<Pergunta> lista = new();

        for (int i = 0; i < perguntas; i++)
            lista.Add(new Pergunta());

        return lista;
    }

    private static List<RespostaAvaliacao> ObterListaRespostaAvaliacao(int respostasCorretas, int respostasIncorretas)
    {
        List<RespostaAvaliacao> lista = new();

        for (int i = 0; i < respostasCorretas; i++)
            lista.Add(new RespostaAvaliacao{ AlternativaEscolhida = new Alternativa() { Correta = true }});

        for (int i = 0; i < respostasIncorretas; i++)
            lista.Add(new RespostaAvaliacao { AlternativaEscolhida = new Alternativa() { Correta = false } });

        return lista;
    }
}
