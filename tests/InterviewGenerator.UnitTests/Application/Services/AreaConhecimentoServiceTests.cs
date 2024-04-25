using InterviewGenerator.Application.Services;
using InterviewGenerator.Domain.Entidade;
using InterviewGenerator.Domain.Repositorio;
using InterviewGenerator.UnitTests.Fixtures;
using Moq;
using Xunit;

namespace InterviewGenerator.UnitTests.Application.Services
{
    [Collection(nameof(AreaConhecimentoTestFixtureCollection))]
    public class AreaConhecimentoServiceTests
    {
        private readonly AreaConhecimentoTestFixture _areaConhecimentoTestFixture;

        private readonly Mock<IAreaConhecimentoRepositorio> _repositorioMock;

        private readonly AreaConhecimentoService _service;

        public AreaConhecimentoServiceTests(AreaConhecimentoTestFixture areaConhecimentoTestFixture)
        {
            _areaConhecimentoTestFixture = areaConhecimentoTestFixture;

            _repositorioMock = new Mock<IAreaConhecimentoRepositorio>();

            _service = new AreaConhecimentoService(_repositorioMock.Object);
        }

        [Fact]
        [Trait("Categoria", "AlterarAreaConhecimento")]
        public async Task AlterarAreaConhecimento_ShoudReturnErrorWhenKnowledgeAreaNotFound()
        {
            // Arrange
            _repositorioMock.Setup(x => x.ObterPorIdEUsuarioId(It.IsAny<Guid>(), It.IsAny<Guid>()))
                .ReturnsAsync(value: null);

            // Act
            var result = await _service.AlterarAreaConhecimento(_areaConhecimentoTestFixture.GerarAlterarAreaConhecimentoDto());

            // Assert
            Assert.True(result.HasError);
            Assert.Contains("Area de conhecimento não encontrada", result.Erros);
        }

        [Fact]
        [Trait("Categoria", "AlterarAreaConhecimento")]
        public async Task AlterarAreaConhecimento_ShoudReturnErrorWhenKnowledgeAreaNameInUse()
        {
            // Arrange
            var existingKnowledgeArea = Guid.NewGuid();

            _repositorioMock.Setup(x => x.ObterPorIdEUsuarioId(It.IsAny<Guid>(), It.IsAny<Guid>()))
                .ReturnsAsync(new AreaConhecimento());

            _repositorioMock.Setup(x => x.ObterPorDescricaoEUsuarioId(It.IsAny<Guid>(), It.IsAny<string>()))
                .ReturnsAsync(new AreaConhecimento
                {
                    Id = existingKnowledgeArea
                });

            // Act
            var result = await _service.AlterarAreaConhecimento(_areaConhecimentoTestFixture.GerarAlterarAreaConhecimentoDto());

            // Assert
            Assert.True(result.HasError);
            Assert.Contains($"Já existe uma área do conhecimento cadastrada com essa descrição. Id: {existingKnowledgeArea}", result.Erros);
        }

        [Fact]
        [Trait("Categoria", "AlterarAreaConhecimento")]
        public async Task AlterarAreaConhecimento_ShoudUpdateKnowledgeArea()
        {
            // Arrange
            _repositorioMock.Setup(x => x.ObterPorIdEUsuarioId(It.IsAny<Guid>(), It.IsAny<Guid>()))
                .ReturnsAsync(new AreaConhecimento());

            _repositorioMock.Setup(x => x.ObterPorDescricaoEUsuarioId(It.IsAny<Guid>(), It.IsAny<string>()))
                .ReturnsAsync(value: null);

            // Act
            var result = await _service.AlterarAreaConhecimento(_areaConhecimentoTestFixture.GerarAlterarAreaConhecimentoDto());

            // Assert
            Assert.False(result.HasError);
            Assert.Empty(result.Erros);
        }

        [Fact]
        [Trait("Categoria", "CadastrarAreaConhecimento")]
        public async Task CadastrarAreaConhecimento_ShouldReturErrorWhenExistingKnowledgeArea()
        {
            // Arrange
            var existingKnowledgeAreaId = Guid.NewGuid();

            _repositorioMock.Setup(x => x.ObterPorDescricaoEUsuarioId(It.IsAny<Guid>(), It.IsAny<string>()))
                .ReturnsAsync(new AreaConhecimento { Id = existingKnowledgeAreaId });

            // Act
            var result = await _service.CadastrarAreaConhecimento(_areaConhecimentoTestFixture.GerarAdicionarAreaConhecimentoDto());

            // Assert
            Assert.True(result.HasError);
            Assert.Contains($"Já existe uma área do conhecimento cadastrada com essa descrição. Id: {existingKnowledgeAreaId}", result.Erros);
        }

        [Fact]
        [Trait("Categoria", "CadastrarAreaConhecimento")]
        public async Task CadastrarAreaConhecimento_ShouldAddKnowledgeArea()
        {
            // Arrange
            _repositorioMock.Setup(x => x.ObterPorDescricaoEUsuarioId(It.IsAny<Guid>(), It.IsAny<string>()))
                .ReturnsAsync(value: null);

            // Act
            var result = await _service.CadastrarAreaConhecimento(_areaConhecimentoTestFixture.GerarAdicionarAreaConhecimentoDto());

            // Assert
            Assert.False(result.HasError);
            Assert.Empty(result.Erros);
        }

        [Fact]
        [Trait("Categoria", "ExcluirAreaConhecimento")]
        public async Task ExcluirAreaConhecimento_ShouldReturnErrorWhenKnowledgeAreaNotFound()
        {
            // Arrange
            _repositorioMock.Setup(x => x.ObterPorIdComPerguntas(It.IsAny<Guid>(), It.IsAny<Guid>()))
                .ReturnsAsync(value: null);

            // Act
            var result = await _service.ExcluirAreaConhecimento(Guid.NewGuid(), Guid.NewGuid());

            // Assert
            Assert.True(result.HasError);
            Assert.Contains("Area de conhecimento não encontrada", result.Erros);
        }

        [Fact]
        [Trait("Categoria", "ExcluirAreaConhecimento")]
        public async Task ExcluirAreaConhecimento_ShouldReturnErrorWhenHavingQuestions()
        {
            // Arrange
            _repositorioMock.Setup(x => x.ObterPorIdComPerguntas(It.IsAny<Guid>(), It.IsAny<Guid>()))
                .ReturnsAsync(new AreaConhecimento
                {
                    Perguntas = new List<Pergunta> { new Pergunta() }
                });

            // Act
            var result = await _service.ExcluirAreaConhecimento(Guid.NewGuid(), Guid.NewGuid());

            // Assert
            Assert.True(result.HasError);
            Assert.Contains("Area de conhecimento possui uma ou mais perguntas relacionadas", result.Erros);
        }

        [Fact]
        [Trait("Categoria", "ExcluirAreaConhecimento")]
        public async Task ExcluirAreaConhecimento_ShouldDeleteKnowledgeArea()
        {
            // Arrange
            _repositorioMock.Setup(x => x.ObterPorIdComPerguntas(It.IsAny<Guid>(), It.IsAny<Guid>()))
                .ReturnsAsync(new AreaConhecimento
                {
                    Perguntas = new List<Pergunta>()
                });

            // Act
            var result = await _service.ExcluirAreaConhecimento(Guid.NewGuid(), Guid.NewGuid());

            // Assert
            Assert.False(result.HasError);
            Assert.Empty(result.Erros);
        }

        [Fact]
        [Trait("Categoria", "ObterOuCriarAreaConhecimento")]
        public async Task ObterOuCriarAreaConhecimento_ShouldGetKnowledgeArea()
        {
            // Arrange
            _repositorioMock.Setup(x => x.ObterPorDescricaoEUsuarioId(It.IsAny<Guid>(), It.IsAny<string>()))
                .ReturnsAsync(new AreaConhecimento());

            // Act
            var result = await _service.ObterOuCriarAreaConhecimento(Guid.NewGuid(), "test");

            // Assert
            Assert.NotNull(result);
            _repositorioMock.Verify(x => x.Adicionar(It.IsAny<AreaConhecimento>()), Times.Never);
        }

        [Fact]
        [Trait("Categoria", "ObterOuCriarAreaConhecimento")]
        public async Task ObterOuCriarAreaConhecimento_ShouldCreateKnowledgeArea()
        {
            // Arrange
            _repositorioMock.Setup(x => x.ObterPorDescricaoEUsuarioId(It.IsAny<Guid>(), It.IsAny<string>()))
                .ReturnsAsync(value: null);

            // Act
            var result = await _service.ObterOuCriarAreaConhecimento(Guid.NewGuid(), "test");

            // Assert
            Assert.NotNull(result);
            _repositorioMock.Verify(x => x.Adicionar(It.IsAny<AreaConhecimento>()), Times.Once);
        }
    }
}
