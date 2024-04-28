using InterviewGenerator.Application.Services;
using InterviewGenerator.Domain.Entidade;
using InterviewGenerator.Domain.Repositorio;
using InterviewGenerator.UnitTests.Fixtures;
using Moq;
using Xunit;

namespace InterviewGenerator.UnitTests.Application.Services
{
    [Collection(nameof(AvaliacaoTestFixtureCollection))]
    public class AvaliacaoServiceTests
    {
        private readonly AvaliacaoTestFixture _avaliacaoTestFixture;

        private readonly Mock<IAvaliacaoRepositorio> _avaliacaoRepositorioMock;
        private readonly Mock<IQuestionarioRepositorio> _questionarioRepositorioMock;
        private readonly Mock<IUsuarioRepositorio> _usuarioRepositorioMock;

        private readonly AvaliacaoService _service;

        public AvaliacaoServiceTests(AvaliacaoTestFixture avaliacaoTestFixture)
        {
            _avaliacaoTestFixture = avaliacaoTestFixture;

            _avaliacaoRepositorioMock = new Mock<IAvaliacaoRepositorio>();
            _questionarioRepositorioMock = new Mock<IQuestionarioRepositorio>();
            _usuarioRepositorioMock = new Mock<IUsuarioRepositorio>();

            _service = new AvaliacaoService(_avaliacaoRepositorioMock.Object, _questionarioRepositorioMock.Object, _usuarioRepositorioMock.Object);
        }

        [Fact]
        [Trait("Categoria", "AdicionarAvaliacao")]
        public async Task AdicionarAvaliacao_ShouldReturnErrorWhenQuizNotFound()
        {
            // Arrange
            _questionarioRepositorioMock.Setup(x => x.ObterPorId(It.IsAny<Guid>()))
                .ReturnsAsync(value: null);

            // Act
            var result = await _service.AdicionarAvaliacao(_avaliacaoTestFixture.GerarAdicionarAvaliacaoDto());

            // Assert
            Assert.True(result.HasError);
            Assert.Contains("Questionário não encontrado", result.Erros);
        }

        [Fact]
        [Trait("Categoria", "AdicionarAvaliacao")]
        public async Task AdicionarAvaliacao_ShouldReturnErrorWhenQuizAlreadySent()
        {
            // Arrange
            var adicionarAvaliacaoDto = _avaliacaoTestFixture.GerarAdicionarAvaliacaoDto();

            _questionarioRepositorioMock.Setup(x => x.ObterPorId(It.IsAny<Guid>()))
                .ReturnsAsync(new Questionario
                {
                    Avaliacoes = new List<Avaliacao>
                    {
                        new Avaliacao
                        {
                            Candidato = new Usuario
                            {
                                Id = adicionarAvaliacaoDto.CandidatoId
                            }
                        }
                    }
                });

            // Act
            var result = await _service.AdicionarAvaliacao(adicionarAvaliacaoDto);

            // Assert
            Assert.True(result.HasError);
            Assert.Contains("Candidato já respondeu este questionário", result.Erros);
        }

        [Fact]
        [Trait("Categoria", "AdicionarAvaliacao")]
        public async Task AdicionarAvaliacao_ShouldReturnErrorWhenQuestionNotAnswered()
        {
            // Arrange
            _questionarioRepositorioMock.Setup(x => x.ObterPorId(It.IsAny<Guid>()))
                .ReturnsAsync(new Questionario
                {
                    Avaliacoes = new List<Avaliacao>(),
                    Perguntas = new List<Pergunta>
                    {
                        new Pergunta
                        {
                            Id = Guid.NewGuid()
                        }
                    }
                });

            // Act
            var result = await _service.AdicionarAvaliacao(_avaliacaoTestFixture.GerarAdicionarAvaliacaoDto());

            // Assert
            Assert.True(result.HasError);
            Assert.Contains("Uma ou mais perguntas não foram respondidas", result.Erros);
        }

        [Fact]
        [Trait("Categoria", "AdicionarAvaliacao")]
        public async Task AdicionarAvaliacao_ShouldReturnErrorWhenQuestionWithInvalidAnswer()
        {
            // Arrange
            var adicionarAvaliacaoDto = _avaliacaoTestFixture.GerarAdicionarAvaliacaoDto();

            var questionario = new Questionario
            {
                Avaliacoes = new List<Avaliacao>()
            };

            questionario.Perguntas = adicionarAvaliacaoDto.Respostas.Select(x => new Pergunta
            {
                Id = x.PerguntaId,
                Alternativas = new List<Alternativa> { new Alternativa { Id = x.AlternativaId, Correta = true } }
            }).ToList();

            questionario.Perguntas[0].Alternativas[0].Id = Guid.NewGuid();

            _questionarioRepositorioMock.Setup(x => x.ObterPorId(It.IsAny<Guid>()))
                .ReturnsAsync(questionario);

            // Act
            var result = await _service.AdicionarAvaliacao(adicionarAvaliacaoDto);

            // Assert
            Assert.True(result.HasError);
            Assert.Contains("Uma ou mais perguntas estão com respostas inválidas", result.Erros);
        }

        [Fact]
        [Trait("Categoria", "AdicionarAvaliacao")]
        public async Task AdicionarAvaliacao_ShouldCreateAssessment()
        {
            // Arrange
            var adicionarAvaliacaoDto = _avaliacaoTestFixture.GerarAdicionarAvaliacaoDto();

            var questionario = new Questionario
            {
                Avaliacoes = new List<Avaliacao>()
            };

            questionario.Perguntas = adicionarAvaliacaoDto.Respostas.Select(x => new Pergunta
            {
                Id = x.PerguntaId,
                Alternativas = new List<Alternativa> { new Alternativa { Id = x.AlternativaId, Correta = true } }
            }).ToList();

            _questionarioRepositorioMock.Setup(x => x.ObterPorId(It.IsAny<Guid>()))
                .ReturnsAsync(questionario);

            // Act
            var result = await _service.AdicionarAvaliacao(adicionarAvaliacaoDto);

            // Assert
            Assert.False(result.HasError);
            Assert.Empty(result.Erros);
        }

        [Fact]
        [Trait("Categoria", "AdicionarObservacaoAvaliacao")]
        public async Task AdicionarObservacaoAvaliacao_ShouldReturnErrorWhenQuizNotFound()
        {
            // Arrange
            _avaliacaoRepositorioMock.Setup(x => x.ObterAvaliacaoPorIdEUsuarioCriacaoQuestionario(It.IsAny<Guid>(), It.IsAny<Guid>()))
                .ReturnsAsync(value: null);

            // Act
            var result = await _service.AdicionarObservacaoAvaliacao(_avaliacaoTestFixture.GerarAdicionarObservacaoAvaliadorDto());

            // Assert
            Assert.True(result.HasError);
            Assert.Contains("Avaliação não existe", result.Erros);
        }

        [Fact]
        [Trait("Categoria", "AdicionarObservacaoAvaliacao")]
        public async Task AdicionarObservacaoAvaliacao_ShouldAddNote()
        {
            // Arrange
            _avaliacaoRepositorioMock.Setup(x => x.ObterAvaliacaoPorIdEUsuarioCriacaoQuestionario(It.IsAny<Guid>(), It.IsAny<Guid>()))
                .ReturnsAsync(new Avaliacao());

            // Act
            var result = await _service.AdicionarObservacaoAvaliacao(_avaliacaoTestFixture.GerarAdicionarObservacaoAvaliadorDto());

            // Assert
            Assert.False(result.HasError);
            Assert.Empty(result.Erros);
        }
    }
}
