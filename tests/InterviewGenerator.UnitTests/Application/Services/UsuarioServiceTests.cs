using InterviewGenerator.Application.Services;
using InterviewGenerator.Domain.Entidade;
using InterviewGenerator.Domain.Enum;
using InterviewGenerator.Domain.Repositorio;
using InterviewGenerator.UnitTests.Fixtures;
using Microsoft.Extensions.Configuration;
using Moq;
using Xunit;

namespace InterviewGenerator.UnitTests.Application.Services;

[Collection(nameof(UsuarioTestFixtureCollection))]
public class UsuarioServiceTests
{
    private readonly UsuarioTestFixture _usuarioTestFixture;

    private readonly Mock<IUsuarioRepositorio> _repositorioMock;
    private readonly Mock<IConfiguration> _configurationMock;

    private readonly UsuarioService _service;

    public UsuarioServiceTests(UsuarioTestFixture usuarioTestFixture)
    {
        _usuarioTestFixture = usuarioTestFixture;

        _repositorioMock = new Mock<IUsuarioRepositorio>();
        _configurationMock = new Mock<IConfiguration>();

        _service = new UsuarioService(_repositorioMock.Object, _configurationMock.Object);
    }

    [Fact]
    [Trait("Categoria", "AlterarUsuario")]
    public async Task AlterarUsuario_ShouldReturnErrorWhenUserNotFound()
    {
        // Arrange
        _repositorioMock.Setup(x => x.ObterPorId(It.IsAny<Guid>()))
            .ReturnsAsync(value: null);

        // Act
        var result = await _service.AlterarUsuario(_usuarioTestFixture.GerarAlterarUsuarioDto());

        // Assert
        Assert.True(result.HasError);
        Assert.Contains("Usuário não encontrado", result.Erros);
    }

    [Fact]
    [Trait("Categoria", "AlterarUsuario")]
    public async Task AlterarUsuario_ShouldReturnErrorWhenCpfInUse()
    {
        // Arrange
        _repositorioMock.Setup(x => x.ObterPorId(It.IsAny<Guid>()))
            .ReturnsAsync(new Usuario());

        _repositorioMock.Setup(x => x.ExisteUsuarioPorCpf(It.IsAny<string>()))
            .ReturnsAsync(true);

        // Act
        var result = await _service.AlterarUsuario(_usuarioTestFixture.GerarAlterarUsuarioDto());

        // Assert
        Assert.True(result.HasError);
        Assert.Contains("Já existe um usuário com este CPF", result.Erros);
    }

    [Fact]
    [Trait("Categoria", "AlterarUsuario")]
    public async Task AlterarUsuario_ShouldReturnErrorWhenLoginInUse()
    {
        // Arrange
        _repositorioMock.Setup(x => x.ObterPorId(It.IsAny<Guid>()))
            .ReturnsAsync(new Usuario());

        _repositorioMock.Setup(x => x.ExisteUsuarioPorCpf(It.IsAny<string>()))
            .ReturnsAsync(false);

        _repositorioMock.Setup(x => x.ExisteUsuarioPorLogin(It.IsAny<string>()))
            .ReturnsAsync(true);

        // Act
        var result = await _service.AlterarUsuario(_usuarioTestFixture.GerarAlterarUsuarioDto());

        // Assert
        Assert.True(result.HasError);
        Assert.Contains("Já existe um usuário com este Login", result.Erros);
    }

    [Fact]
    [Trait("Categoria", "AlterarUsuario")]
    public async Task AlterarUsuario_ShouldUpdateUser()
    {
        // Arrange
        _repositorioMock.Setup(x => x.ObterPorId(It.IsAny<Guid>()))
            .ReturnsAsync(new Usuario());

        _repositorioMock.Setup(x => x.ExisteUsuarioPorCpf(It.IsAny<string>()))
            .ReturnsAsync(false);

        _repositorioMock.Setup(x => x.ExisteUsuarioPorLogin(It.IsAny<string>()))
            .ReturnsAsync(false);

        // Act
        var result = await _service.AlterarUsuario(_usuarioTestFixture.GerarAlterarUsuarioDto());

        // Assert
        Assert.False(result.HasError);
        Assert.Empty(result.Erros);
    }

    [Fact]
    [Trait("Categoria", "CadastrarUsuario")]
    public async Task CadastrarUsuario_ShouldReturnErrorWhenCpfInUse()
    {
        // Arrange
        _repositorioMock.Setup(x => x.ExisteUsuarioPorCpf(It.IsAny<string>()))
            .ReturnsAsync(true);

        // Act
        var result = await _service.CadastrarUsuario(_usuarioTestFixture.GerarAdicionarUsuarioDto());

        // Assert
        Assert.True(result.HasError);
        Assert.Contains("Já existe um usuário com este CPF", result.Erros);
    }

    [Fact]
    [Trait("Categoria", "CadastrarUsuario")]
    public async Task CadastrarUsuario_ShouldReturnErrorWhenLoginInUse()
    {
        // Arrange
        _repositorioMock.Setup(x => x.ExisteUsuarioPorCpf(It.IsAny<string>()))
            .ReturnsAsync(false);

        _repositorioMock.Setup(x => x.ExisteUsuarioPorLogin(It.IsAny<string>()))
            .ReturnsAsync(true);

        // Act
        var result = await _service.CadastrarUsuario(_usuarioTestFixture.GerarAdicionarUsuarioDto());

        // Assert
        Assert.True(result.HasError);
        Assert.Contains("Já existe um usuário com este Login", result.Erros);
    }

    [Fact]
    [Trait("Categoria", "CadastrarUsuario")]
    public async Task CadastrarUsuario_ShouldCreateUser()
    {
        // Arrange
        _repositorioMock.Setup(x => x.ExisteUsuarioPorCpf(It.IsAny<string>()))
            .ReturnsAsync(false);

        _repositorioMock.Setup(x => x.ExisteUsuarioPorLogin(It.IsAny<string>()))
            .ReturnsAsync(false);

        // Act
        var result = await _service.CadastrarUsuario(_usuarioTestFixture.GerarAdicionarUsuarioDto());

        // Assert
        Assert.False(result.HasError);
        Assert.Empty(result.Erros);
    }

    [Fact]
    [Trait("Categoria", "BuscarTokenUsuario")]
    public async Task BuscarTokenUsuario_ShouldReturnErrorWhenInvalidUser()
    {
        // Arrange
        _repositorioMock.Setup(x => x.ObterUsuarioPorLoginESenha(It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(value: null);

        // Act
        var result = await _service.BuscarTokenUsuario(_usuarioTestFixture.GerarGerarTokenUsuarioDto());

        // Assert
        Assert.True(result.HasError);
        Assert.Contains("Não foi possível gerar token para acesso do usuário", result.Erros);
    }

    [Fact]
    [Trait("Categoria", "BuscarTokenUsuario")]
    public async Task BuscarTokenUsuario_ShouldReturnToken()
    {
        // Arrange
        _repositorioMock.Setup(x => x.ObterUsuarioPorLoginESenha(It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(new Usuario
            {
                Nome = "teste",
                Perfil = Perfil.Avaliador,
                Login = "teste.login",
                Senha = "Senha@1234"
            });

        _configurationMock.Setup(x => x["Secret:Key"])
            .Returns("TesteChaveTesteChave");

        // Act
        var result = await _service.BuscarTokenUsuario(_usuarioTestFixture.GerarGerarTokenUsuarioDto());

        // Assert
        Assert.False(result.HasError);
        Assert.NotNull(result.Data);
    }
}
