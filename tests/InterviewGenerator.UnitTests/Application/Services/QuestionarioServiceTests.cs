using InterviewGenerator.Application.Services;
using InterviewGenerator.Domain.Entidade;
using InterviewGenerator.Domain.Repositorio;
using InterviewGenerator.UnitTests.Fixtures;
using Moq;
using Xunit;

namespace InterviewGenerator.UnitTests.Application.Services
{
    [Collection(nameof(QuestionarioTestFixtureCollection))]
    public class QuestionarioServiceTests
    {
        private readonly QuestionarioTestFixture _questionarioTestFixture;

        private readonly Mock<IQuestionarioRepositorio> _questionarioRepositorioMock;
        private readonly Mock<IPerguntaRepositorio> _perguntaRepositorioMock;

        private readonly QuestionarioService _service;

        public QuestionarioServiceTests(QuestionarioTestFixture questionarioTestFixture)
        {
            _questionarioTestFixture = questionarioTestFixture;

            _questionarioRepositorioMock = new Mock<IQuestionarioRepositorio>();
            _perguntaRepositorioMock = new Mock<IPerguntaRepositorio>();

            _service = new QuestionarioService(_questionarioRepositorioMock.Object, _perguntaRepositorioMock.Object);
        }

        [Fact]
        [Trait("Categoria", "AlterarQuestionario")]
        public async Task AlterarQuestionario_ShouldReturnErrorWhenQuizNotFound()
        {
            // Arrange
            _questionarioRepositorioMock.Setup(x => x.ObterPorIdComAvaliacoesEPerguntas(It.IsAny<Guid>(), It.IsAny<Guid>()))
                .ReturnsAsync(value: null);

            // Act
            var result = await _service.AlterarQuestionario(_questionarioTestFixture.GerarAlterarQuestionarioDto());

            // Assert
            Assert.True(result.HasError);
            Assert.Contains("Questionario não encontrado", result.Erros);
        }

        [Fact]
        [Trait("Categoria", "AlterarQuestionario")]
        public async Task AlterarQuestionario_ShouldReturnErrorWhenHaveAnsweredQuiz()
        {
            // Arrange
            _questionarioRepositorioMock.Setup(x => x.ObterPorIdComAvaliacoesEPerguntas(It.IsAny<Guid>(), It.IsAny<Guid>()))
                .ReturnsAsync(new Questionario
                {
                    Avaliacoes = new List<Avaliacao> { new Avaliacao() }
                });

            // Act
            var result = await _service.AlterarQuestionario(_questionarioTestFixture.GerarAlterarQuestionarioDto());

            // Assert
            Assert.True(result.HasError);
            Assert.Contains("Não é possível alterar o questionário, existem avaliações feitas", result.Erros);
        }

        [Fact]
        [Trait("Categoria", "AlterarQuestionario")]
        public async Task AlterarQuestionario_ShouldReturnErrorWhenQuizNameInUse()
        {
            // Arrange
            _questionarioRepositorioMock.Setup(x => x.ObterPorIdComAvaliacoesEPerguntas(It.IsAny<Guid>(), It.IsAny<Guid>()))
                .ReturnsAsync(new Questionario
                {
                    Avaliacoes = new List<Avaliacao>(),
                    Id = Guid.NewGuid()
                });

            _questionarioRepositorioMock.Setup(x => x.ObterPorNome(It.IsAny<Guid>(), It.IsAny<string>()))
                .ReturnsAsync(new Questionario
                {
                    Avaliacoes = new List<Avaliacao>(),
                    Id = Guid.NewGuid()
                });

            // Act
            var result = await _service.AlterarQuestionario(_questionarioTestFixture.GerarAlterarQuestionarioDto());

            // Assert
            Assert.True(result.HasError);
            Assert.Contains("Já existe um questionário com este nome", result.Erros);
        }

        [Fact]
        [Trait("Categoria", "AlterarQuestionario")]
        public async Task AlterarQuestionario_ShouldReturnErrorWhenQuestionNotFound()
        {
            // Arrange
            var questionGuid = Guid.NewGuid();

            _questionarioRepositorioMock.Setup(x => x.ObterPorIdComAvaliacoesEPerguntas(It.IsAny<Guid>(), It.IsAny<Guid>()))
                .ReturnsAsync(new Questionario
                {
                    Avaliacoes = new List<Avaliacao>(),
                    Id = questionGuid
                });

            _questionarioRepositorioMock.Setup(x => x.ObterPorNome(It.IsAny<Guid>(), It.IsAny<string>()))
                .ReturnsAsync(new Questionario
                {
                    Avaliacoes = new List<Avaliacao>(),
                    Id = questionGuid
                });

            _perguntaRepositorioMock.Setup(x => x.ObterPerguntaPorId(It.IsAny<Guid>(), It.IsAny<Guid>()))
                .ReturnsAsync(value: null);

            var alterarQuestionarioDto = _questionarioTestFixture.GerarAlterarQuestionarioDto();
            alterarQuestionarioDto.Id = questionGuid;

            // Act
            var result = await _service.AlterarQuestionario(alterarQuestionarioDto);

            // Assert
            Assert.True(result.HasError);
            Assert.Contains($"Pergunta {alterarQuestionarioDto.Perguntas.First()} não encontrada", result.Erros);
        }

        [Fact]
        [Trait("Categoria", "AlterarQuestionario")]
        public async Task AlterarQuestionario_ShouldUpdateQuiz()
        {
            // Arrange
            var questionGuid = Guid.NewGuid();

            _questionarioRepositorioMock.Setup(x => x.ObterPorIdComAvaliacoesEPerguntas(It.IsAny<Guid>(), It.IsAny<Guid>()))
                .ReturnsAsync(new Questionario
                {
                    Avaliacoes = new List<Avaliacao>(),
                    Id = questionGuid
                });

            _questionarioRepositorioMock.Setup(x => x.ObterPorNome(It.IsAny<Guid>(), It.IsAny<string>()))
                .ReturnsAsync(new Questionario
                {
                    Avaliacoes = new List<Avaliacao>(),
                    Id = questionGuid
                });

            _perguntaRepositorioMock.Setup(x => x.ObterPerguntaPorId(It.IsAny<Guid>(), It.IsAny<Guid>()))
                .ReturnsAsync(new Pergunta());

            var alterarQuestionarioDto = _questionarioTestFixture.GerarAlterarQuestionarioDto();
            alterarQuestionarioDto.Id = questionGuid;

            // Act
            var result = await _service.AlterarQuestionario(alterarQuestionarioDto);

            // Assert
            Assert.False(result.HasError);
            Assert.Empty(result.Erros);
        }

        [Fact]
        [Trait("Categoria", "CadastrarQuestionario")]
        public async Task CadastrarQuestionario_ShouldReturnErrorWhenQuizNameInUse()
        {
            // Arrange
            _questionarioRepositorioMock.Setup(x => x.ObterPorNome(It.IsAny<Guid>(), It.IsAny<string>()))
                .ReturnsAsync(new Questionario { Id = Guid.NewGuid() });

            // Act
            var result = await _service.CadastrarQuestionario(_questionarioTestFixture.GerarAdicionarQuestionarioDto());

            // Assert
            Assert.True(result.HasError);
            Assert.Contains("Questionário com esse nome já cadastrado", result.Erros);
        }

        [Fact]
        [Trait("Categoria", "CadastrarQuestionario")]
        public async Task CadastrarQuestionario_ShouldReturnErrorWhenQuestionNotFound()
        {
            // Arrange
            _questionarioRepositorioMock.Setup(x => x.ObterPorNome(It.IsAny<Guid>(), It.IsAny<string>()))
                .ReturnsAsync(value: null);

            _perguntaRepositorioMock.Setup(x => x.ObterPerguntaPorId(It.IsAny<Guid>(), It.IsAny<Guid>()))
                .ReturnsAsync(value: null);

            var adicionarQuestionarioDto = _questionarioTestFixture.GerarAdicionarQuestionarioDto();

            // Act
            var result = await _service.CadastrarQuestionario(adicionarQuestionarioDto);

            // Assert
            Assert.True(result.HasError);
            Assert.Contains($"Pergunta {adicionarQuestionarioDto.Perguntas.First()} não encontrada", result.Erros);
        }

        [Fact]
        [Trait("Categoria", "CadastrarQuestionario")]
        public async Task CadastrarQuestionario_ShouldAddQuiz()
        {
            // Arrange
            _questionarioRepositorioMock.Setup(x => x.ObterPorNome(It.IsAny<Guid>(), It.IsAny<string>()))
                .ReturnsAsync(value: null);

            _perguntaRepositorioMock.Setup(x => x.ObterPerguntaPorId(It.IsAny<Guid>(), It.IsAny<Guid>()))
                .ReturnsAsync(new Pergunta());

            // Act
            var result = await _service.CadastrarQuestionario(_questionarioTestFixture.GerarAdicionarQuestionarioDto());

            // Assert
            Assert.False(result.HasError);
            Assert.Empty(result.Erros);
        }

        [Fact]
        [Trait("Categoria", "ExcluirQuestionario")]
        public async Task ExcluirQuestionario_ShouldReturnErrorWhenQuizNotFound()
        {
            // Arrange
            _questionarioRepositorioMock.Setup(x => x.ObterPorIdComAvaliacoesEPerguntas(It.IsAny<Guid>(), It.IsAny<Guid>()))
                .ReturnsAsync(value: null);

            // Act
            var result = await _service.ExcluirQuestionario(Guid.NewGuid(), Guid.NewGuid());

            // Assert
            Assert.True(result.HasError);
            Assert.Contains("Questionario não encontrado", result.Erros);
        }

        [Fact]
        [Trait("Categoria", "ExcluirQuestionario")]
        public async Task ExcluirQuestionario_ShouldReturnErrorWhenHaveAnsweredQuiz()
        {
            // Arrange
            _questionarioRepositorioMock.Setup(x => x.ObterPorIdComAvaliacoesEPerguntas(It.IsAny<Guid>(), It.IsAny<Guid>()))
                .ReturnsAsync(new Questionario
                {
                    Avaliacoes = new List<Avaliacao> { new Avaliacao() }
                });

            // Act
            var result = await _service.ExcluirQuestionario(Guid.NewGuid(), Guid.NewGuid());

            // Assert
            Assert.True(result.HasError);
            Assert.Contains("Não é possível excluir o questionário, existem avaliações feitas", result.Erros);
        }

        [Fact]
        [Trait("Categoria", "ExcluirQuestionario")]
        public async Task ExcluirQuestionario_ShouldDeleteQuiz()
        {
            // Arrange
            _questionarioRepositorioMock.Setup(x => x.ObterPorIdComAvaliacoesEPerguntas(It.IsAny<Guid>(), It.IsAny<Guid>()))
                .ReturnsAsync(new Questionario
                {
                    Avaliacoes = new List<Avaliacao>()
                });

            // Act
            var result = await _service.ExcluirQuestionario(Guid.NewGuid(), Guid.NewGuid());

            // Assert
            Assert.False(result.HasError);
            Assert.Empty(result.Erros);
        }
    }
}
