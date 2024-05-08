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
public class AreaConhecimentoControllerTests : IClassFixture<ApiApplicationFactory<Program>>
{
    private readonly HttpClient _client;
    private readonly UsuarioHelper _usuarioHelper;

    public AreaConhecimentoControllerTests(ApiApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
        _usuarioHelper = new UsuarioHelper();
    }

    [Fact]
    public async Task GivenValidKnowledgeArea_WhenAddingKnowledgeArea_ThenShouldAddKnowledgeArea()
    {
        //Arrange
        var token = await _usuarioHelper.ObterTokenUsuario(_client, Perfil.Avaliador);
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var areaConhecimento = new AdicionarAreaConhecimentoDto() { Descricao = "Add Area Conhecimento" };

        //Act
        var postAreaConhecimento = await _client.PostAsync("/AreaConhecimento", JsonContent.Create(areaConhecimento));

        //Assert
        Assert.Equal(HttpStatusCode.Created, postAreaConhecimento.StatusCode);
    }

    [Fact]
    public async Task GivenValidKnowledgeArea_WhenUpdatingKnowledgeArea_ThenShouldUpdateKnowledgeArea()
    {
        //Arrange
        var token = await _usuarioHelper.ObterTokenUsuario(_client, Perfil.Avaliador);
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var addAreaConhecimentoDto = new AdicionarAreaConhecimentoDto() { Descricao = "Update Area Conhecimento" };
        var postAreaConhecimento = await _client.PostAsync("/AreaConhecimento", JsonContent.Create(addAreaConhecimentoDto));
        postAreaConhecimento.EnsureSuccessStatusCode();
        var idAreaConhecimento = await JsonHelper.LerDoJson<Guid>(postAreaConhecimento.Content);

        var alterarAreaConhecimento = new AlterarAreaConhecimentoDto()
        {
            Descricao = "Update Area Conhecimento Updated",
            Id = idAreaConhecimento
        };

        //Act
        var putAreaConhecimento = await _client.PutAsync("/AreaConhecimento", JsonContent.Create(alterarAreaConhecimento));

        //Assert
        var getAreaConhecimento = await _client.GetAsync($"/AreaConhecimento?descricao={alterarAreaConhecimento.Descricao}");
        getAreaConhecimento.EnsureSuccessStatusCode();
        var getAreaConhecimentoResponse = await JsonHelper.LerDoJson<IEnumerable<AreaConhecimentoViewModel>>(getAreaConhecimento.Content);

        Assert.Equal(HttpStatusCode.OK, putAreaConhecimento.StatusCode);
        Assert.NotEmpty(getAreaConhecimentoResponse);
        Assert.Equal(alterarAreaConhecimento.Descricao, getAreaConhecimentoResponse.FirstOrDefault()!.Descricao);
    }

    [Fact]
    public async Task GivenValidKnowledgeAreaWithLinkedQuestion_WhenUpdatingKnowledgeArea_ThenShouldUpdateKnowledgeArea()
    {
        //Arrange
        var token = await _usuarioHelper.ObterTokenUsuario(_client, Perfil.Avaliador);
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var addAreaConhecimentoDto = new AdicionarAreaConhecimentoDto() { Descricao = "AreaDeConhecimentoComPerguntas" };
        var postAreaConhecimento = await _client.PostAsync("/AreaConhecimento", JsonContent.Create(addAreaConhecimentoDto));
        postAreaConhecimento.EnsureSuccessStatusCode();
        var idAreaConhecimento = await JsonHelper.LerDoJson<Guid>(postAreaConhecimento.Content);

        var addPerguntaDto = new AdicionarPerguntaDto()
        {
            Descricao = "Teste AreaDeConhecimentoComPerguntas",
            AreaConhecimento = "AreaDeConhecimentoComPerguntas",
            Alternativas = new List<AlternativaDto>()
            {
                new AlternativaDto("Alternativa 1", false),
                new AlternativaDto("Alternativa 2", true),
                new AlternativaDto("Alternativa 3", false)
            }
        };
        var postPergunta = await _client.PostAsync("/Pergunta", JsonContent.Create(addPerguntaDto));
        postPergunta.EnsureSuccessStatusCode();

        var alterarAreaConhecimentoDto = new AlterarAreaConhecimentoDto()
        {
            Descricao = "AreaDeConhecimentoComPerguntas Updated",
            Id = idAreaConhecimento
        };

        //Act
        var putAreaConhecimento = await _client.PutAsync("/AreaConhecimento", JsonContent.Create(alterarAreaConhecimentoDto));

        //Assert
        var getAreaConhecimento = await _client.GetAsync($"/AreaConhecimento?descricao={alterarAreaConhecimentoDto.Descricao}");
        getAreaConhecimento.EnsureSuccessStatusCode();
        var getAreaConhecimentoResponse = await JsonHelper.LerDoJson<IEnumerable<AreaConhecimentoViewModel>>(getAreaConhecimento.Content);

        Assert.Equal(HttpStatusCode.OK, putAreaConhecimento.StatusCode);
        Assert.NotEmpty(getAreaConhecimentoResponse);
        Assert.Equal(1, getAreaConhecimentoResponse.FirstOrDefault()!.PerguntasCadastradas);
        Assert.Equal(alterarAreaConhecimentoDto.Descricao, getAreaConhecimentoResponse.FirstOrDefault()!.Descricao);
    }

    [Fact]
    public async Task GivenValidKnowledgeArea_WhenDeletingKnowledgeArea_ThenShouldDeleteKnowledgeArea()
    {
        //Arrange
        var token = await _usuarioHelper.ObterTokenUsuario(_client, Perfil.Avaliador);
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var addAreaConhecimentoDto = new AdicionarAreaConhecimentoDto() { Descricao = "Delete Area Conhecimento" };
        var postAreaConhecimento = await _client.PostAsync("/AreaConhecimento", JsonContent.Create(addAreaConhecimentoDto));
        postAreaConhecimento.EnsureSuccessStatusCode();
        var idAreaConhecimento = await JsonHelper.LerDoJson<Guid>(postAreaConhecimento.Content);

        //Act
        var deleteAreaConhecimento = await _client.DeleteAsync($"/AreaConhecimento/{idAreaConhecimento}");

        //Assert
        var getAreaConhecimento = await _client.GetAsync($"/AreaConhecimento?descricao={addAreaConhecimentoDto.Descricao}");

        Assert.Equal(HttpStatusCode.OK, deleteAreaConhecimento.StatusCode);
        Assert.Equal(HttpStatusCode.NoContent, getAreaConhecimento.StatusCode);
    }
}
