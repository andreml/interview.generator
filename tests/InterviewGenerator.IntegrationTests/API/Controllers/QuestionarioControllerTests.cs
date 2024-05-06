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
public class QuestionarioControllerTests : IClassFixture<ApiApplicationFactory<Program>>
{
    private readonly HttpClient _client;
    private readonly UsuarioHelper _usuarioHelper;

    public QuestionarioControllerTests(ApiApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
        _usuarioHelper = new UsuarioHelper();
    }

    [Fact]
    public async Task GivenValidQuiz_WhenAddingQuiz_ThenShouldAddQuiz()
    {
        //Arrange
        var token = await _usuarioHelper.ObterTokenUsuario(_client, Perfil.Avaliador);
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var perguntaDto1 = ObterPerguntaParaAdicionar("TesteAddQuestionario: Pergunta Um", "TesteAddQuestionario");
        var perguntaDto2 = ObterPerguntaParaAdicionar("TesteAddQuestionario: Pergunta Dois", "TesteAddQuestionario");
        var perguntaDto3 = ObterPerguntaParaAdicionar("TesteAddQuestionario: Pergunta Tres", "TesteAddQuestionario");

        var postPergunta = await _client.PostAsync("/Pergunta", JsonContent.Create(perguntaDto1));
        var idPergunta1 = await JsonHelper.LerDoJson<Guid>(postPergunta.Content);
        postPergunta = await _client.PostAsync("/Pergunta", JsonContent.Create(perguntaDto2));
        var idPergunta2 = await JsonHelper.LerDoJson<Guid>(postPergunta.Content);
        postPergunta = await _client.PostAsync("/Pergunta", JsonContent.Create(perguntaDto3));
        var idPergunta3 = await JsonHelper.LerDoJson<Guid>(postPergunta.Content);

        var addQuestionarioDto = new AdicionarQuestionarioDto()
        {
            Nome = "Questionario Teste Add",
            Perguntas = new List<Guid> { idPergunta1, idPergunta2, idPergunta3 }
        };

        //Act
        var postQuestionario = await _client.PostAsync("/Questionario", JsonContent.Create(addQuestionarioDto));
        postQuestionario.EnsureSuccessStatusCode();

        //Assert
        var getQuestionario = await _client.GetAsync($"/Questionario?nome={addQuestionarioDto.Nome}");
        var getQuestionarioResponse = await JsonHelper.LerDoJson<IEnumerable<QuestionarioViewModelAvaliador>>(getQuestionario.Content);

        Assert.Equal(HttpStatusCode.Created, postQuestionario.StatusCode);
        Assert.NotEmpty(getQuestionarioResponse);
        Assert.Equal(getQuestionarioResponse.FirstOrDefault()!.Nome, addQuestionarioDto.Nome);

        Assert.Contains(perguntaDto1.Descricao, getQuestionarioResponse.FirstOrDefault()!.Perguntas.Select(x => x.Descricao));
        Assert.Contains(perguntaDto2.Descricao, getQuestionarioResponse.FirstOrDefault()!.Perguntas.Select(x => x.Descricao));
        Assert.Contains(perguntaDto3.Descricao, getQuestionarioResponse.FirstOrDefault()!.Perguntas.Select(x => x.Descricao));
    }

    [Fact]
    public async Task GivenValidQuiz_WhenUpdatingQuiz_ThenShouldUpdateQuiz()
    {
        //Arrange
        var token = await _usuarioHelper.ObterTokenUsuario(_client, Perfil.Avaliador);
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var perguntaDto1 = ObterPerguntaParaAdicionar("TesteUpdateQuestionario: Pergunta Um", "TesteUpdateQuestionario");
        var perguntaDto2 = ObterPerguntaParaAdicionar("TesteUpdateQuestionario: Pergunta Dois", "TesteUpdateQuestionario");
        var perguntaDto3 = ObterPerguntaParaAdicionar("TesteUpdateQuestionario: Pergunta Tres", "TesteUpdateQuestionario");

        var postPergunta = await _client.PostAsync("/Pergunta", JsonContent.Create(perguntaDto1));
        var idPergunta1 = await JsonHelper.LerDoJson<Guid>(postPergunta.Content);
        postPergunta = await _client.PostAsync("/Pergunta", JsonContent.Create(perguntaDto2));
        var idPergunta2 = await JsonHelper.LerDoJson<Guid>(postPergunta.Content);
        postPergunta = await _client.PostAsync("/Pergunta", JsonContent.Create(perguntaDto3));
        var idPergunta3 = await JsonHelper.LerDoJson<Guid>(postPergunta.Content);

        var addQuestionarioDto = new AdicionarQuestionarioDto()
        {
            Nome = "Questionario Teste Update",
            Perguntas = new List<Guid> { idPergunta1, idPergunta2, idPergunta3 }
        };
        var postQuestionario = await _client.PostAsync("/Questionario", JsonContent.Create(addQuestionarioDto));
        postQuestionario.EnsureSuccessStatusCode();
        var idQuestionario = await JsonHelper.LerDoJson<Guid>(postQuestionario.Content);

        var alterarQuestionarioDto = new AlterarQuestionarioDto()
        {
            Id = idQuestionario,
            Nome = "Questionario Teste Update Atualizado",
            Perguntas = addQuestionarioDto.Perguntas
        };

        //Act
        var putQuestionario = await _client.PutAsync("/Questionario", JsonContent.Create(alterarQuestionarioDto));
        putQuestionario.EnsureSuccessStatusCode();

        //Assert
        var getQuestionario = await _client.GetAsync($"/Questionario?nome={alterarQuestionarioDto.Nome}");
        var getQuestionarioResponse = await JsonHelper.LerDoJson<IEnumerable<QuestionarioViewModelAvaliador>>(getQuestionario.Content);

        Assert.Equal(HttpStatusCode.OK, putQuestionario.StatusCode);
        Assert.NotEmpty(getQuestionarioResponse);
        Assert.Equal(3, getQuestionarioResponse.FirstOrDefault()!.Perguntas.Count);
    }

    [Fact]
    public async Task GivenValidQuizWithQuestions_WhenDeletingQuiz_ThenShouldDeleteQuizWithoutDeleteQuestions()
    {
        //Arrange
        var token = await _usuarioHelper.ObterTokenUsuario(_client, Perfil.Avaliador);
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var perguntaDto1 = ObterPerguntaParaAdicionar("PerguntaTesteDeleteQuestionario: Pergunta Um", "TesteDeleteQuestionario");
        var perguntaDto2 = ObterPerguntaParaAdicionar("PerguntaTesteDeleteQuestionario: Pergunta Dois", "TesteDeleteQuestionario");
        var perguntaDto3 = ObterPerguntaParaAdicionar("PerguntaTesteDeleteQuestionario: Pergunta Tres", "TesteDeleteQuestionario");

        var postPergunta = await _client.PostAsync("/Pergunta", JsonContent.Create(perguntaDto1));
        var idPergunta1 = await JsonHelper.LerDoJson<Guid>(postPergunta.Content);
        postPergunta = await _client.PostAsync("/Pergunta", JsonContent.Create(perguntaDto2));
        var idPergunta2 = await JsonHelper.LerDoJson<Guid>(postPergunta.Content);
        postPergunta = await _client.PostAsync("/Pergunta", JsonContent.Create(perguntaDto3));
        var idPergunta3 = await JsonHelper.LerDoJson<Guid>(postPergunta.Content);

        var addQuestionarioDto = new AdicionarQuestionarioDto()
        {
            Nome = "Questionario Teste Delete",
            Perguntas = new List<Guid> { idPergunta1, idPergunta2, idPergunta3 }
        };
        var postQuestionario = await _client.PostAsync("/Questionario", JsonContent.Create(addQuestionarioDto));
        postQuestionario.EnsureSuccessStatusCode();
        var idQuestionario = await JsonHelper.LerDoJson<Guid>(postQuestionario.Content);

        //Act
        var deleteQuestionario = await _client.DeleteAsync($"/Questionario/{idQuestionario}");

        //Assert
        var getQuestionario = await _client.GetAsync($"/Questionario?nome={addQuestionarioDto.Nome}");

        var getperguntas = await _client.GetAsync("/Pergunta?descricao=PerguntaTesteDeleteQuestionario");
        var getperguntasResponse = await JsonHelper.LerDoJson<IEnumerable<PerguntaViewModel>>(getperguntas.Content);

        Assert.Equal(HttpStatusCode.OK, deleteQuestionario.StatusCode);
        Assert.Equal(HttpStatusCode.NoContent, getQuestionario.StatusCode);
        Assert.NotNull(getperguntasResponse);
        Assert.Equal(3, getperguntasResponse.Count());
    }

    private static AdicionarPerguntaDto ObterPerguntaParaAdicionar(string descricao, string areaConhecimento) =>
        new ()
        {
            Descricao = descricao,
            AreaConhecimento = areaConhecimento,
            Alternativas = new List<AlternativaDto>()
            {
                new AlternativaDto("Nova Pergunta Alternativa1", false),
                new AlternativaDto("Nova Pergunta Alternativa2", false),
                new AlternativaDto("Nova Pergunta Alternativa3", true),
                new AlternativaDto("Nova Pergunta Alternativa4", false),
                new AlternativaDto("Nova Pergunta Alternativa5", false)
            }
        };
}
