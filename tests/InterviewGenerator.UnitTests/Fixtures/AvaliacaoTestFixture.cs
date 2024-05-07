using Bogus;
using InterviewGenerator.Application.Dto;
using Xunit;

namespace InterviewGenerator.UnitTests.Fixtures;

public class AvaliacaoTestFixture
{
    private readonly Faker _faker;
    public AvaliacaoTestFixture()
    {
        _faker = new Faker();
    }

    public AdicionarAvaliacaoDto GerarAdicionarAvaliacaoDto() =>
        new ()
        {
            CandidatoId = _faker.Random.Guid(),
            QuestionarioId = _faker.Random.Guid(),
            Respostas = new List<RespostaAvaliacaoDto>
            {
                new RespostaAvaliacaoDto(_faker.Random.Guid(), _faker.Random.Guid()),
                new RespostaAvaliacaoDto(_faker.Random.Guid(), _faker.Random.Guid()),
                new RespostaAvaliacaoDto(_faker.Random.Guid(), _faker.Random.Guid()),
                new RespostaAvaliacaoDto(_faker.Random.Guid(), _faker.Random.Guid()),
                new RespostaAvaliacaoDto(_faker.Random.Guid(), _faker.Random.Guid()),
                new RespostaAvaliacaoDto(_faker.Random.Guid(), _faker.Random.Guid())
            }
        };

    public AdicionarObservacaoAvaliadorDto GerarAdicionarObservacaoAvaliadorDto() =>
        new ()
        {
            UsuarioIdCriacaoQuestionario = _faker.Random.Guid(),
            AvaliacaoId = _faker.Random.Guid(),
            ObservacaoAvaliador = _faker.Random.String2(500)
        };
}

[CollectionDefinition("AvaliacaoTestFixtureCollection")]
public class AvaliacaoTestFixtureCollection : ICollectionFixture<AvaliacaoTestFixture>
{ }
