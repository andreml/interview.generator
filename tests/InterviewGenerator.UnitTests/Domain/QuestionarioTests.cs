using InterviewGenerator.Domain.Entidade;
using Xunit;

namespace InterviewGenerator.UnitTests.Domain;

public class QuestionarioTests
{
    [Theory]
    [MemberData(nameof(ObterQuestionarios))]
    public void Questionario_NotaMaximaMedia(Questionario questionario, decimal? notaMediaEsperada)
    {
        // Act
        var mediaNota = questionario.MediaNota();

        //Assert
        Assert.Equal(notaMediaEsperada, mediaNota);
    }

    public static IEnumerable<object[]> ObterQuestionarios()
    {
        yield return new object[]
        {
            new Questionario
            {
                Avaliacoes = new List<Avaliacao>
                {
                    new Avaliacao { Respondida = true, Nota = 100},
                    new Avaliacao { Respondida = true, Nota = 50},
                    new Avaliacao { Respondida = true, Nota = 20},
                    new Avaliacao { Respondida = false, Nota = null},
                }
            }, (decimal)56.67
        };

        yield return new object[]
        {
            new Questionario
            {
                Avaliacoes = new List<Avaliacao>
                {
                    new Avaliacao { Respondida = true, Nota = 74.03M},
                }
            }, (decimal)74.03
        };

        yield return new object[]
        {
            new Questionario
            {
                Avaliacoes = new List<Avaliacao>
                {
                    new Avaliacao { Respondida = false, Nota = null},
                    new Avaliacao { Respondida = false, Nota = null},
                }
            }, null!
        };

    }
}
