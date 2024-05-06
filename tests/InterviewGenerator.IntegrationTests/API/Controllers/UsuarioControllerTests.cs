using InterviewGenerator.Application.Dto;
using InterviewGenerator.Application.ViewModels;
using InterviewGenerator.Domain.Enum;
using InterviewGenerator.IntegrationTests.API.Configuration;
using InterviewGenerator.IntegrationTests.API.Utils;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Xunit;

namespace InterviewGenerator.IntegrationTests.API.Controllers;

[Collection("Database")]
public class UsuarioControllerTests : IClassFixture<ApiApplicationFactory<Program>>
{
    private readonly HttpClient _client;
    private readonly UsuarioHelper _usuarioHelper;

    public UsuarioControllerTests(ApiApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
        _usuarioHelper = new UsuarioHelper();
    }

    [Fact]
    public async Task GivenValidAvaliadorUser_WhenCreatingUser_ThenShouldCreateUser()
    {
        // Arrange
        var usuarioAvaliador = new AdicionarUsuarioDto("89660569467", "João da Silva", Perfil.Avaliador, "joao.silva", "Teste@321");

        // Act
        var responseNovoUsuario = await _client.PostAsync("/Usuario", JsonContent.Create(usuarioAvaliador));

        // Assert
        Assert.Equal(HttpStatusCode.Created, responseNovoUsuario.StatusCode);
    }

    [Fact]
    public async Task GivenValidCandidatoUser_WhenCreatingUser_ThenShouldCreateUser()
    {
        // Arrange
        var usuarioCandidato = new AdicionarUsuarioDto("13971371426", "Maria Lima", Perfil.Candidato, "maria.lima", "testE@123");

        // Act
        var responseNovoUsuario = await _client.PostAsync("/Usuario", JsonContent.Create(usuarioCandidato));

        // Assert
        Assert.Equal(HttpStatusCode.Created, responseNovoUsuario.StatusCode);
    }

    [Fact]
    public async Task GivenValidUser_WhenUpdatingUser_ThenShouldCreateUser()
    {
        //Arrange
        var token = await _usuarioHelper.ObterTokenUsuario(_client, Perfil.Avaliador);
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var alterarUsuarioDto = new AlterarUsuarioDto()
        {
            Cpf = _usuarioHelper.CpfAvaliador,
            Nome = "Guilherme Dos Santos",
            Login = _usuarioHelper.LoginAvaliador,
            Senha = _usuarioHelper.SenhaUsuario
        };

        //Act
        var putUsuario = await _client.PutAsync("/Usuario", JsonContent.Create(alterarUsuarioDto));
        putUsuario.EnsureSuccessStatusCode();

        //Assert
        var getUsuario = await _client.GetAsync("/Usuario");
        getUsuario.EnsureSuccessStatusCode();
        var getUsuarioResponse = await JsonHelper.LerDoJson<UsuarioViewModel>(getUsuario.Content);

        Assert.Equal(HttpStatusCode.OK, putUsuario.StatusCode);
        Assert.Equal(alterarUsuarioDto.Cpf, getUsuarioResponse.Cpf);
        Assert.Equal(alterarUsuarioDto.Nome, getUsuarioResponse.Nome);
        Assert.Equal(alterarUsuarioDto.Login, getUsuarioResponse.Login);
    }
}
