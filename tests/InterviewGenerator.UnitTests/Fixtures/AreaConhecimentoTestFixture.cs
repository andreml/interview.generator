using Bogus;
using InterviewGenerator.Application.Dto;
using Xunit;

namespace InterviewGenerator.UnitTests.Fixtures
{
    public class AreaConhecimentoTestFixture
    {
        private readonly Faker _faker;
        public AreaConhecimentoTestFixture()
        {
            _faker = new Faker();
        }

        public AlterarAreaConhecimentoDto GerarAlterarAreaConhecimentoDto() =>
            new()
            {
                UsuarioId = _faker.Random.Guid(),
                Id = _faker.Random.Guid(),
                Descricao = _faker.Random.String2(100)
            };

        public AdicionarAreaConhecimentoDto GerarAdicionarAreaConhecimentoDto() =>
            new()
            {
                UsuarioId = _faker.Random.Guid(),
                Descricao = _faker.Random.String2(100)
            };
    }

    [CollectionDefinition("AreaConhecimentoTestFixtureCollection")]
    public class AreaConhecimentoTestFixtureCollection : ICollectionFixture<AreaConhecimentoTestFixture>
    { }
}
