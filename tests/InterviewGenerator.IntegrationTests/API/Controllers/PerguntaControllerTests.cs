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
public class PerguntaControllerTests : IClassFixture<ApiApplicationFactory<Program>>
{
    private readonly HttpClient _client;
    private readonly UsuarioHelper _usuarioHelper;

    public PerguntaControllerTests(ApiApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
        _usuarioHelper = new UsuarioHelper();
    }

    [Fact]
    public async Task GivenValidQuestion_WhenAddingQuestion_ThenShouldAddQuestion()
    {
        //Arrange
        var token = await _usuarioHelper.ObterTokenUsuario(_client, Perfil.Avaliador);
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var addPerguntaDto = new AdicionarPerguntaDto()
        {
            Descricao = "Teste Add Pergunta",
            AreaConhecimento = "Add",
            Alternativas = new List<AlternativaDto>()
            {
                new AlternativaDto("Alternativa 1", false),
                new AlternativaDto("Alternativa 2", true),
                new AlternativaDto("Alternativa 3", false)
            }
        };

        //Act
        var postPergunta = await _client.PostAsync("/Pergunta", JsonContent.Create(addPerguntaDto));

        //Assert
        Assert.Equal(HttpStatusCode.Created, postPergunta.StatusCode);
    }

    [Fact]
    public async Task AlterarPergunta()
    {
        //Arrange
        var token = await _usuarioHelper.ObterTokenUsuario(_client, Perfil.Avaliador);
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var addPerguntaDto = new AdicionarPerguntaDto()
        {
            Descricao = "Teste Alterar Pergunta",
            AreaConhecimento = "Update",
            Alternativas = new List<AlternativaDto>()
            {
                new AlternativaDto("Alternativa 1", false),
                new AlternativaDto("Alternativa 2", true),
                new AlternativaDto("Alternativa 3", false)
            }
        };

        var postPergunta = await _client.PostAsync("/Pergunta", JsonContent.Create(addPerguntaDto));
        postPergunta.EnsureSuccessStatusCode();
        var perguntaId = await JsonHelper.LerDoJson<Guid>(postPergunta.Content);

        var alterarPerguntaDto = new AlterarPerguntaDto()
        {
            Id = perguntaId,
            Descricao = "Teste Alterar Pergunta Alterado",
            AreaConhecimento = "Update Alterado",
            Alternativas = new List<AlterarAlternativaDto>()
            {
                new AlterarAlternativaDto("Alternativa 1", true),
                new AlterarAlternativaDto("Alternativa 2", false),
                new AlterarAlternativaDto("Alternativa 3", false),
                new AlterarAlternativaDto("Alternativa 4", false)
            }
        };

        //Act
        var putPergunta = await _client.PutAsync("/Pergunta", JsonContent.Create(alterarPerguntaDto));
        putPergunta.EnsureSuccessStatusCode();



        //Assert
        var getPergunta = await _client.GetAsync($"/Pergunta?descricao={alterarPerguntaDto.Descricao}");
        var getPerguntaResponse = (await JsonHelper.LerDoJson<IEnumerable<PerguntaViewModel>>(getPergunta.Content)).FirstOrDefault();

        Assert.Equal(HttpStatusCode.OK, putPergunta.StatusCode);
        Assert.NotNull(getPerguntaResponse);
        Assert.Equal(alterarPerguntaDto.Descricao, getPerguntaResponse.Descricao);
        Assert.Equal(alterarPerguntaDto.AreaConhecimento, getPerguntaResponse.Areaconhecimento);
        Assert.Equal(alterarPerguntaDto.Alternativas.Count, getPerguntaResponse.Alternativas.Count);
    }

    [Fact]
    public async Task ExcluirPergunta()
    {
        //Arrange
        var token = await _usuarioHelper.ObterTokenUsuario(_client, Perfil.Avaliador);
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var addPerguntaDto = new AdicionarPerguntaDto()
        {
            Descricao = "Teste Delete Pergunta",
            AreaConhecimento = "Delete",
            Alternativas = new List<AlternativaDto>()
            {
                new AlternativaDto("Alternativa 1", false),
                new AlternativaDto("Alternativa 2", true),
                new AlternativaDto("Alternativa 3", false)
            }
        };

        var postPergunta = await _client.PostAsync("/Pergunta", JsonContent.Create(addPerguntaDto));
        postPergunta.EnsureSuccessStatusCode();
        var perguntaId = await JsonHelper.LerDoJson<Guid>(postPergunta.Content);

        //Act
        var deletePergunta = await _client.DeleteAsync($"/Pergunta/{perguntaId}");
        deletePergunta.EnsureSuccessStatusCode();

        //Assert
        var getPergunta = await _client.GetAsync($"/Pergunta?descricao={addPerguntaDto.Descricao}");

        Assert.Equal(HttpStatusCode.OK, deletePergunta.StatusCode);
        Assert.Equal(HttpStatusCode.NoContent, getPergunta.StatusCode);
    }
}
