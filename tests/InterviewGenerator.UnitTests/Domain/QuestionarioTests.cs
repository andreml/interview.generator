using InterviewGenerator.Domain.Entidade;
using Xunit;

namespace InterviewGenerator.UnitTests.Domain
{
    public class QuestionarioTests
    {
        [Theory]
        [MemberData(nameof(ObterQuestionarios))]
        public void Questionario_NotaMaximaMedia(Questionario questionario, decimal notaMaximaEsperada, decimal notaMediaEsperada)
        {
            // Act & Assert
            Assert.Equal(notaMaximaEsperada, questionario.MaiorNota);
            Assert.Equal(notaMediaEsperada, questionario.MediaNota);
        }

        public static IEnumerable<object[]> ObterQuestionarios()
        {
            yield return new object[]
            {
                new Questionario
                {
                    Avaliacoes = new List<Avaliacao>
                    {
                        new Avaliacao { Nota = 100},
                        new Avaliacao { Nota = 50},
                        new Avaliacao { Nota = 20},
                    }
                }, 100, 56.67
            };

            yield return new object[]
            {
                new Questionario
                {
                    Avaliacoes = new List<Avaliacao>
                    {
                        new Avaliacao { Nota = 74.03M},
                    }
                }, 74.03, 74.03
            };

            yield return new object[]
            {
                new Questionario
                {
                    Avaliacoes = new List<Avaliacao>
                    {
                        new Avaliacao { Nota = 99.99M},
                        new Avaliacao { Nota = 99.99M},
                        new Avaliacao { Nota = 99.99M},
                        new Avaliacao { Nota = 99.99M},
                    }
                }, 99.99M, 99.99M
            };

        }
    }
}
