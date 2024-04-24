using InterviewGenerator.Application.Interfaces;
using InterviewGenerator.Application.Services;
using InterviewGenerator.Domain.Entidade;
using InterviewGenerator.Domain.Repositorio;
using InterviewGenerator.UnitTests.Fixtures;
using Moq;
using Xunit;

namespace InterviewGenerator.UnitTests.Application.Services
{
    [Collection(nameof(PerguntaTestFixtureCollection))]
    public class PerguntaServiceTests
    {
        private readonly PerguntaTestFixture _perguntaTestFixture;

        private readonly Mock<IPerguntaRepositorio> _repositorioMock;
        private readonly Mock<IAreaConhecimentoService> _areaConhecimentoServiceMock;

        private readonly PerguntaService _service;

        public PerguntaServiceTests(PerguntaTestFixture perguntaTestFixture)
        {
            _perguntaTestFixture = perguntaTestFixture;

            _repositorioMock = new Mock<IPerguntaRepositorio>();
            _areaConhecimentoServiceMock = new Mock<IAreaConhecimentoService>();

            _service = new PerguntaService(_repositorioMock.Object, _areaConhecimentoServiceMock.Object);
        }

        [Fact]
        [Trait("Categoria", "AlterarPergunta")]
        public async Task AlterarPergunta_ShouldReturnErrorWhenQuestionNotFound()
        {
            // Arrange
            _repositorioMock.Setup(x => x.ObterPerguntaPorId(It.IsAny<Guid>(), It.IsAny<Guid>()))
                .ReturnsAsync(value: null);

            // Act
            var result = await _service.AlterarPergunta(_perguntaTestFixture.GerarAlterarPerguntaDto());

            // Assert
            Assert.True(result.HasError);
            Assert.Contains("Pergunta não encontrada", result.Erros);
        }

        [Fact]
        [Trait("Categoria", "AlterarPergunta")]
        public async Task AlterarPergunta_ShouldReturnErrorWhenQuestionBeingUsedInQuiz()
        {
            // Arrange
            _repositorioMock.Setup(x => x.ObterPerguntaPorId(It.IsAny<Guid>(), It.IsAny<Guid>()))
                .ReturnsAsync(new Pergunta
                {
                    Questionarios = new List<Questionario> { new Questionario() }
                });

            // Act
            var result = await _service.AlterarPergunta(_perguntaTestFixture.GerarAlterarPerguntaDto());

            // Assert
            Assert.True(result.HasError);
            Assert.Contains("Já existem questionários cadastrados com esta pergunta", result.Erros);
        }

        [Fact]
        [Trait("Categoria", "AlterarPergunta")]
        public async Task AlterarPergunta_ShouldUpdateQuestion()
        {
            // Arrange
            _repositorioMock.Setup(x => x.ObterPerguntaPorId(It.IsAny<Guid>(), It.IsAny<Guid>()))
                .ReturnsAsync(new Pergunta
                {
                    Questionarios = new List<Questionario>()
                });

            _areaConhecimentoServiceMock.Setup(x => x.ObterOuCriarAreaConhecimento(It.IsAny<Guid>(), It.IsAny<string>()))
                .ReturnsAsync(new AreaConhecimento());

            // Act
            var result = await _service.AlterarPergunta(_perguntaTestFixture.GerarAlterarPerguntaDto());

            // Assert
            Assert.False(result.HasError);
            Assert.Empty(result.Erros);
        }

        [Fact]
        [Trait("Categoria", "CadastrarPergunta")]
        public async Task CadastrarPergunta_ShouldReturnErrorWhenInvalidUserId()
        {
            // Arrange
            var invalidId = Guid.Empty;

            // Act
            var result = await _service.CadastrarPergunta(invalidId, _perguntaTestFixture.GerarAdicionarPerguntaDto());

            // Assert
            Assert.True(result.HasError);
            Assert.Contains("UsuarioId inválido", result.Erros);
        }

        [Fact]
        [Trait("Categoria", "CadastrarPergunta")]
        public async Task CadastrarPergunta_ShouldReturnErrorWhenExistingQuestion()
        {
            // Arrange
            _repositorioMock.Setup(x => x.ExistePorDescricao(It.IsAny<Guid>(), It.IsAny<string>()))
                .ReturnsAsync(true);

            // Act
            var result = await _service.CadastrarPergunta(Guid.NewGuid(), _perguntaTestFixture.GerarAdicionarPerguntaDto());

            // Assert
            Assert.True(result.HasError);
            Assert.Contains("Pergunta já cadastrada", result.Erros);
        }

        [Fact]
        [Trait("Categoria", "CadastrarPergunta")]
        public async Task CadastrarPergunta_ShouldCreateQuestion()
        {
            // Arrange
            _repositorioMock.Setup(x => x.ExistePorDescricao(It.IsAny<Guid>(), It.IsAny<string>()))
                .ReturnsAsync(false);

            _areaConhecimentoServiceMock.Setup(x => x.ObterOuCriarAreaConhecimento(It.IsAny<Guid>(), It.IsAny<string>()))
                .ReturnsAsync(new AreaConhecimento());

            // Act
            var result = await _service.CadastrarPergunta(Guid.NewGuid(), _perguntaTestFixture.GerarAdicionarPerguntaDto());

            // Assert
            Assert.False(result.HasError);
            Assert.Empty(result.Erros);
        }

        [Fact]
        [Trait("Categoria", "ExcluirPergunta")]
        public async Task ExcluirPergunta_ShouldReturnErrorWhenQuestionNotFound()
        {
            // Arrange
            _repositorioMock.Setup(x => x.ObterPerguntaPorId(It.IsAny<Guid>(), It.IsAny<Guid>()))
                .ReturnsAsync(value: null);

            // Act
            var result = await _service.ExcluirPergunta(Guid.NewGuid(), Guid.NewGuid());

            // Assert
            Assert.True(result.HasError);
            Assert.Contains("Pergunta não encontrada", result.Erros);
        }

        [Fact]
        [Trait("Categoria", "ExcluirPergunta")]
        public async Task ExcluirPergunta_ShouldReturnErrorWhenQuestionUsedInQuiz()
        {
            // Arrange
            _repositorioMock.Setup(x => x.ObterPerguntaPorId(It.IsAny<Guid>(), It.IsAny<Guid>()))
                .ReturnsAsync(new Pergunta
                {
                    Questionarios = new List<Questionario> { new Questionario() }
                });

            // Act
            var result = await _service.ExcluirPergunta(Guid.NewGuid(), Guid.NewGuid());

            // Assert
            Assert.True(result.HasError);
            Assert.Contains("Já existem questionários cadastrados com esta pergunta", result.Erros);
        }

        [Fact]
        [Trait("Categoria", "ExcluirPergunta")]
        public async Task ExcluirPergunta_ShouldDeleteQuestion()
        {
            // Arrange
            _repositorioMock.Setup(x => x.ObterPerguntaPorId(It.IsAny<Guid>(), It.IsAny<Guid>()))
                .ReturnsAsync(new Pergunta
                {
                    Questionarios = new List<Questionario>()
                });

            // Act
            var result = await _service.ExcluirPergunta(Guid.NewGuid(), Guid.NewGuid());

            // Assert
            Assert.False(result.HasError);
            Assert.Empty(result.Erros);
        }
    }
}
