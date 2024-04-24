using Bogus;
using InterviewGenerator.Application.Dto;
using InterviewGenerator.Domain.Enum;
using Xunit;

namespace InterviewGenerator.UnitTests.Fixtures
{
    public class UsuarioTestFixture
    {
        private readonly Faker _faker;

        public UsuarioTestFixture()
        {
            _faker = new Faker();
        }

        public AlterarUsuarioDto GerarAlterarUsuarioDto() =>
            new()
            {
                Cpf = _faker.Random.Long(11111111111, 99999999999).ToString(),
                Id = _faker.Random.Guid(),
                Login = _faker.Random.String2(30),
                Nome = _faker.Random.String2(30),
                Senha = _faker.Random.String2(10)
            };

        public AdicionarUsuarioDto GerarAdicionarUsuarioDto() =>
            new (
                _faker.Random.Long(11111111111, 99999999999).ToString(),
                _faker.Random.String2(30),
                Perfil.Avaliador,
                _faker.Random.String2(30),
                _faker.Random.String2(10));

        public GerarTokenUsuarioDto GerarGerarTokenUsuarioDto() =>
            new(_faker.Random.String2(30), _faker.Random.String2(10));
    }

    [CollectionDefinition("UsuarioTestFixtureCollection")]
    public class UsuarioTestFixtureCollection : ICollectionFixture<UsuarioTestFixture>
    { }
}
