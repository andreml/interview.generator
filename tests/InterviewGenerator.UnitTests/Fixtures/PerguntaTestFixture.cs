using Bogus;
using InterviewGenerator.Application.Dto;

namespace InterviewGenerator.UnitTests.Fixtures
{
    public class PerguntaTestFixture
    {
        private readonly Faker _faker;

        public PerguntaTestFixture()
        {
            _faker = new Faker();
        }

        public AlterarPerguntaDto GerarAlterarPerguntaDto() =>
            new()
            {
                UsuarioId = _faker.Random.Guid(),
                Id = _faker.Random.Guid(),
                Descricao = _faker.Random.String2(1000),
                AreaConhecimento = _faker.Random.String2(100),
                Alternativas = new List<AlterarAlternativaDto>
                {
                    new AlterarAlternativaDto(_faker.Random.String2(1000), true),
                    new AlterarAlternativaDto(_faker.Random.String2(1000), false),
                    new AlterarAlternativaDto(_faker.Random.String2(1000), false),
                    new AlterarAlternativaDto(_faker.Random.String2(1000), false),
                    new AlterarAlternativaDto(_faker.Random.String2(1000), false)
                }
            };

        public AdicionarPerguntaDto GerarAdicionarPerguntaDto() =>
            new()
            {
                Descricao = _faker.Random.String2(1000),
                AreaConhecimento = _faker.Random.String2(100),
                Alternativas = new List<AlternativaDto>
                {
                    new AlternativaDto(_faker.Random.String2(1000), true),
                    new AlternativaDto(_faker.Random.String2(1000), false),
                    new AlternativaDto(_faker.Random.String2(1000), false),
                    new AlternativaDto(_faker.Random.String2(1000), false),
                    new AlternativaDto(_faker.Random.String2(1000), false)
                }
            };
    }
}
