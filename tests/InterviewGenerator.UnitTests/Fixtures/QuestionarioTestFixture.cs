using Bogus;
using InterviewGenerator.Application.Dto;
using Xunit;

namespace InterviewGenerator.UnitTests.Fixtures
{
    public class QuestionarioTestFixture
    {
        private readonly Faker _faker;

        public QuestionarioTestFixture()
        {
            _faker = new Faker();
        }

        public AlterarQuestionarioDto GerarAlterarQuestionarioDto() =>
            new()
            {
                UsuarioId = _faker.Random.Guid(),
                Id = _faker.Random.Guid(),
                Nome = _faker.Random.String2(200),
                Perguntas = new List<Guid>
                {
                    _faker.Random.Guid(),
                    _faker.Random.Guid(),
                    _faker.Random.Guid(),
                    _faker.Random.Guid(),
                    _faker.Random.Guid(),
                    _faker.Random.Guid()
                }
            };

        public AdicionarQuestionarioDto GerarAdicionarQuestionarioDto() =>
            new()
            {
                UsuarioId = _faker.Random.Guid(),
                Nome = _faker.Random.String2(200),
                Perguntas = new List<Guid>
                {
                    _faker.Random.Guid(),
                    _faker.Random.Guid(),
                    _faker.Random.Guid(),
                    _faker.Random.Guid(),
                    _faker.Random.Guid(),
                    _faker.Random.Guid()
                }
            };
    }


    [CollectionDefinition("QuestionarioTestFixtureCollection")]
    public class QuestionarioTestFixtureCollection : ICollectionFixture<QuestionarioTestFixture>
    { }
}
