using InterviewGenerator.Application.Services;
using InterviewGenerator.Domain.Entidade;
using InterviewGenerator.Domain.Enum;
using InterviewGenerator.Domain.Repositorio;
using InterviewGenerator.UnitTests.Fixtures;
using Moq;
using Xunit;

namespace InterviewGenerator.UnitTests.Application.Services;

[Collection(nameof(AvaliacaoTestFixtureCollection))]
public class AvaliacaoServiceTests
{
    private readonly AvaliacaoTestFixture _avaliacaoTestFixture;

    private readonly Mock<IAvaliacaoRepositorio> _avaliacaoRepositorioMock;
    private readonly Mock<IUsuarioRepositorio> _usuarioRepositorioMock;
    private readonly Mock<IQuestionarioRepositorio> _questionarioRepositorioMock;

    private readonly AvaliacaoService _service;

    public AvaliacaoServiceTests(AvaliacaoTestFixture avaliacaoTestFixture)
    {
        _avaliacaoTestFixture = avaliacaoTestFixture;

        _avaliacaoRepositorioMock = new Mock<IAvaliacaoRepositorio>();
        _usuarioRepositorioMock = new Mock<IUsuarioRepositorio>();
        _questionarioRepositorioMock = new Mock<IQuestionarioRepositorio>();

        _service = new AvaliacaoService(
                            _avaliacaoRepositorioMock.Object,
                            _usuarioRepositorioMock.Object,
                            _questionarioRepositorioMock.Object);
    }

    [Fact]
    [Trait("Categoria", "ResponderAvaliacao")]
    public async Task ResponderAvaliacao_ShouldReturnErrorWhenQuizNotFound()
    {
        // Arrange
        _avaliacaoRepositorioMock.Setup(x => x.ObterPorIdECandidatoId(It.IsAny<Guid>(), It.IsAny<Guid>()))
            .ReturnsAsync(value: null);

        // Act
        var result = await _service.ResponderAvaliacao(_avaliacaoTestFixture.GerarResponderAvaliacaoDto());

        // Assert
        Assert.True(result.HasError);
        Assert.Contains("Avaliação não encontrada", result.Erros);
    }

    [Fact]
    [Trait("Categoria", "ResponderAvaliacao")]
    public async Task ResponderAvaliacao_ShouldReturnErrorWhenQuizAlreadyAnswered()
    {
        // Arrange
        _avaliacaoRepositorioMock.Setup(x => x.ObterPorIdECandidatoId(It.IsAny<Guid>(), It.IsAny<Guid>()))
            .ReturnsAsync(new Avaliacao
            {
                Respondida = true
            });

        // Act
        var result = await _service.ResponderAvaliacao(_avaliacaoTestFixture.GerarResponderAvaliacaoDto());

        // Assert
        Assert.True(result.HasError);
        Assert.Contains("Candidato já respondeu essa avaliação", result.Erros);
    }

    [Fact]
    [Trait("Categoria", "ResponderAvaliacao")]
    public async Task ResponderAvaliacao_ShouldReturnErrorWhenQuestionNotAnswered()
    {
        // Arrange
        _avaliacaoRepositorioMock.Setup(x => x.ObterPorIdECandidatoId(It.IsAny<Guid>(), It.IsAny<Guid>()))
            .ReturnsAsync(new Avaliacao
            {
                Respondida = false,
                Questionario = new Questionario
                {
                    Avaliacoes = new List<Avaliacao>(),
                    Perguntas = new List<Pergunta>
                    {
                        new Pergunta
                        {
                            Id = Guid.NewGuid()
                        }
                    }
                }
            });

        // Act
        var result = await _service.ResponderAvaliacao(_avaliacaoTestFixture.GerarResponderAvaliacaoDto());

        // Assert
        Assert.True(result.HasError);
        Assert.Contains("Uma ou mais perguntas não foram respondidas", result.Erros);
    }

    [Fact]
    [Trait("Categoria", "ResponderAvaliacao")]
    public async Task ResponderAvaliacao_ShouldReturnErrorWhenQuestionWithInvalidAnswer()
    {
        // Arrange
        var responderAvaliacaoDto = _avaliacaoTestFixture.GerarResponderAvaliacaoDto();

        var questionario = new Questionario
        {
            Avaliacoes = new List<Avaliacao>(),
            Perguntas = responderAvaliacaoDto.Respostas.Select(x => new Pergunta
            {
                Id = x.PerguntaId,
                Alternativas = new List<Alternativa> { new Alternativa { Id = x.AlternativaId, Correta = true } }
            }).ToList()
        };

        questionario.Perguntas[0].Alternativas[0].Id = Guid.NewGuid();

        _avaliacaoRepositorioMock.Setup(x => x.ObterPorIdECandidatoId(It.IsAny<Guid>(), It.IsAny<Guid>()))
            .ReturnsAsync(new Avaliacao
            {
                Respondida = false,
                Questionario = questionario
            });

        // Act
        var result = await _service.ResponderAvaliacao(responderAvaliacaoDto);

        // Assert
        Assert.True(result.HasError);
        Assert.Contains("Uma ou mais perguntas estão com respostas inválidas", result.Erros);
    }

    [Fact]
    [Trait("Categoria", "ResponderAvaliacao")]
    public async Task ResponderAvaliacao_ShouldReturnSuccess()
    {
        // Arrange
        var adicionarAvaliacaoDto = _avaliacaoTestFixture.GerarResponderAvaliacaoDto();

        var questionario = new Questionario
        {
            Avaliacoes = new List<Avaliacao>(),
            Perguntas = adicionarAvaliacaoDto.Respostas.Select(x => new Pergunta
            {
                Id = x.PerguntaId,
                Alternativas = new List<Alternativa> { new Alternativa { Id = x.AlternativaId, Correta = true } }
            }).ToList()
        };

        _avaliacaoRepositorioMock.Setup(x => x.ObterPorIdECandidatoId(It.IsAny<Guid>(), It.IsAny<Guid>()))
            .ReturnsAsync(new Avaliacao
            {
                Respondida = false,
                Questionario = questionario
            });

        // Act
        var result = await _service.ResponderAvaliacao(adicionarAvaliacaoDto);

        // Assert
        Assert.False(result.HasError);
        Assert.Empty(result.Erros);
    }

    [Fact]
    [Trait("Categoria", "AdicionarObservacaoAvaliacao")]
    public async Task AdicionarObservacaoAvaliacao_ShouldReturnErrorWhenQuizNotFound()
    {
        // Arrange
        _avaliacaoRepositorioMock.Setup(x => x.ObterPorIdEUsuarioCriacaoQuestionario(It.IsAny<Guid>(), It.IsAny<Guid>()))
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
        _avaliacaoRepositorioMock.Setup(x => x.ObterPorIdEUsuarioCriacaoQuestionario(It.IsAny<Guid>(), It.IsAny<Guid>()))
            .ReturnsAsync(new Avaliacao());

        // Act
        var result = await _service.AdicionarObservacaoAvaliacao(_avaliacaoTestFixture.GerarAdicionarObservacaoAvaliadorDto());

        // Assert
        Assert.False(result.HasError);
        Assert.Empty(result.Erros);
    }

    [Fact]
    [Trait("Categoria", "EnviarAvaliacaoParaCandidatoAsync")]
    public async Task EnviarAvaliacaoParaCandidatoAsync_ShouldReturnErrorWhenQuizNotFound()
    {
        // Arrange
        _questionarioRepositorioMock.Setup(x => x.ObterPorIdEUsuarioCriacao(It.IsAny<Guid>(), It.IsAny<Guid>()))
            .ReturnsAsync(value: null);

        // Act
        var result = await _service.EnviarAvaliacaoParaCandidatoAsync(_avaliacaoTestFixture.GerarEnviarAvaliacaoParaCandidatoDto());

        // Assert
        Assert.True(result.HasError);
        Assert.Contains("Questionário não encontrado", result.Erros);
    }

    [Fact]
    [Trait("Categoria", "EnviarAvaliacaoParaCandidatoAsync")]
    public async Task EnviarAvaliacaoParaCandidatoAsync_ShouldReturnErrorWhenCandidateNotFound()
    {
        // Arrange
        _questionarioRepositorioMock.Setup(x => x.ObterPorIdEUsuarioCriacao(It.IsAny<Guid>(), It.IsAny<Guid>()))
            .ReturnsAsync(new Questionario());

        _usuarioRepositorioMock.Setup(x => x.ObterPorLogin(It.IsAny<string>()))
            .ReturnsAsync(value: null);

        // Act
        var result = await _service.EnviarAvaliacaoParaCandidatoAsync(_avaliacaoTestFixture.GerarEnviarAvaliacaoParaCandidatoDto());

        // Assert
        Assert.True(result.HasError);
        Assert.Contains("Candidato não encontrado", result.Erros);
    }

    [Fact]
    [Trait("Categoria", "EnviarAvaliacaoParaCandidatoAsync")]
    public async Task EnviarAvaliacaoParaCandidatoAsync_ShouldReturnErrorWhenLoginIsNotCandidate()
    {
        // Arrange
        _questionarioRepositorioMock.Setup(x => x.ObterPorIdEUsuarioCriacao(It.IsAny<Guid>(), It.IsAny<Guid>()))
            .ReturnsAsync(new Questionario());

        _usuarioRepositorioMock.Setup(x => x.ObterPorLogin(It.IsAny<string>()))
            .ReturnsAsync(new Usuario
            {
                Perfil = Perfil.Avaliador
            });

        // Act
        var result = await _service.EnviarAvaliacaoParaCandidatoAsync(_avaliacaoTestFixture.GerarEnviarAvaliacaoParaCandidatoDto());

        // Assert
        Assert.True(result.HasError);
        Assert.Contains("Login não pertence a um usuário Candidato", result.Erros);
    }

    [Fact]
    [Trait("Categoria", "EnviarAvaliacaoParaCandidatoAsync")]
    public async Task EnviarAvaliacaoParaCandidatoAsync_ShouldReturnErrorWhenQuizAlreadySent()
    {
        // Arrange
        var dto = _avaliacaoTestFixture.GerarEnviarAvaliacaoParaCandidatoDto();

        _questionarioRepositorioMock.Setup(x => x.ObterPorIdEUsuarioCriacao(It.IsAny<Guid>(), It.IsAny<Guid>()))
            .ReturnsAsync(new Questionario() { Id = dto.QuestionarioId });

        _usuarioRepositorioMock.Setup(x => x.ObterPorLogin(It.IsAny<string>()))
            .ReturnsAsync(new Usuario { Perfil = Perfil.Candidato });

        _avaliacaoRepositorioMock.Setup(x => x.ObterPorCandidatoId(It.IsAny<Guid>()))
            .ReturnsAsync(new List<Avaliacao>
            {
                new Avaliacao
                {
                    Questionario = new Questionario { Id = dto.QuestionarioId }
                }
            });

        // Act
        var result = await _service.EnviarAvaliacaoParaCandidatoAsync(dto);

        // Assert
        Assert.True(result.HasError);
        Assert.Contains("Avaliação já enviada a esse candidato", result.Erros);
    }

    [Fact]
    [Trait("Categoria", "EnviarAvaliacaoParaCandidatoAsync")]
    public async Task EnviarAvaliacaoParaCandidatoAsync_ShouldSendQuiz()
    {
        // Arrange
        var dto = _avaliacaoTestFixture.GerarEnviarAvaliacaoParaCandidatoDto();

        _questionarioRepositorioMock.Setup(x => x.ObterPorIdEUsuarioCriacao(It.IsAny<Guid>(), It.IsAny<Guid>()))
            .ReturnsAsync(new Questionario() { Id = dto.QuestionarioId });

        _usuarioRepositorioMock.Setup(x => x.ObterPorLogin(It.IsAny<string>()))
            .ReturnsAsync(new Usuario { Perfil = Perfil.Candidato });

        _avaliacaoRepositorioMock.Setup(x => x.ObterPorCandidatoId(It.IsAny<Guid>()))
            .ReturnsAsync(new List<Avaliacao>());

        // Act
        var result = await _service.EnviarAvaliacaoParaCandidatoAsync(dto);

        // Assert
        Assert.False(result.HasError);
        Assert.Empty(result.Erros);
    }

    [Fact]
    [Trait("Categoria", "ObterAvaliacaoParaResponderAsync")]
    public async Task ObterAvaliacaoParaResponderAsync_ShouldReturnErrorWhenCandidateHasAlreadyResponded()
    {
        // Arrange
        _avaliacaoRepositorioMock.Setup(x => x.ObterPorIdECandidatoId(It.IsAny<Guid>(), It.IsAny<Guid>()))
            .ReturnsAsync(new Avaliacao
            {
                Respondida = true
            });

        // Act
        var result = await _service.ObterAvaliacaoParaResponderAsync(Guid.NewGuid(), Guid.NewGuid());

        // Assert
        Assert.True(result.HasError);
        Assert.Contains("Candidato já respondeu esta avaliação", result.Erros);
    }
}
