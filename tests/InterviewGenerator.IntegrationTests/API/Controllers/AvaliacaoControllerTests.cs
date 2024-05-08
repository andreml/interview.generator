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
public class AvaliacaoControllerTests : IClassFixture<ApiApplicationFactory<Program>>
{
    private readonly HttpClient _client;
    private readonly UsuarioHelper _usuarioHelper;

    public AvaliacaoControllerTests(ApiApplicationFactory<Program> factory)
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

        await _client.PostAsync("/Pergunta", JsonContent.Create(ObterPerguntaParaAdicionar("PerguntaTesteAvaliacao: Pergunta Um", "TesteAvaliacao")));
        await _client.PostAsync("/Pergunta", JsonContent.Create(ObterPerguntaParaAdicionar("PerguntaTesteAvaliacao: Pergunta Dois", "TesteAvaliacao")));
        await _client.PostAsync("/Pergunta", JsonContent.Create(ObterPerguntaParaAdicionar("PerguntaTesteAvaliacao: Pergunta Tres", "TesteAvaliacao")));

        var getperguntas = await _client.GetAsync("/Pergunta?descricao=PerguntaTesteAvaliacao");
        var perguntas = await JsonHelper.LerDoJson<IEnumerable<PerguntaViewModel>>(getperguntas.Content);

        var addQuestionarioDto = new AdicionarQuestionarioDto()
        {
            Nome = "Questionario Teste Avaliacao",
            Perguntas = perguntas.Select(x => x.Id).ToList()
        };

        var postQuestionario = await _client.PostAsync("/Questionario", JsonContent.Create(addQuestionarioDto));
        postQuestionario.EnsureSuccessStatusCode();
        var idQuestionario = await JsonHelper.LerDoJson<Guid>(postQuestionario.Content);

        token = await _usuarioHelper.ObterTokenUsuario(_client, Perfil.Candidato);
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var getQuestionario = await _client.GetAsync($"/Questionario/ObterParaPreenchimento/{idQuestionario}");
        var questionario = await JsonHelper.LerDoJson<QuestionarioViewModelCandidato>(getQuestionario.Content);

        var auxSkip = 0;
        var adicionarAvaliacaoDto = new AdicionarAvaliacaoDto()
        {
            QuestionarioId = questionario.Id,
            Respostas = perguntas.Select(p => new RespostaAvaliacaoDto(
                                                    p.Id,
                                                    p.Alternativas.Skip(auxSkip++).FirstOrDefault()!.Id)
                                         ).ToList()
        };

        //Act
        var postAvaliacao = await _client.PostAsync("Avaliacao", JsonContent.Create(adicionarAvaliacaoDto));

        //Assert
        token = await _usuarioHelper.ObterTokenUsuario(_client, Perfil.Avaliador);
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var getAvaliacoes = await _client.GetAsync($"Avaliacao?QuestionarioId={idQuestionario}");
        var getAvaliacoesResponse = await JsonHelper.LerDoJson<ICollection<AvaliacaoViewModel>>(getAvaliacoes.Content);

        Assert.Equal(HttpStatusCode.Created, postAvaliacao.StatusCode);
        Assert.NotEmpty(getAvaliacoesResponse);
        Assert.Equal(questionario.Nome, getAvaliacoesResponse.FirstOrDefault()!.NomeQuestionario);
        Assert.Equal(questionario.Perguntas.Count, getAvaliacoesResponse.FirstOrDefault()!.Respostas.Count());
    }

    [Fact]
    public async Task GivenValidQuiz_WhenAddingNote_ThenShouldAddNote()
    {
        //Arrange
        var token = await _usuarioHelper.ObterTokenUsuario(_client, Perfil.Avaliador);
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        await _client.PostAsync("/Pergunta", JsonContent.Create(ObterPerguntaParaAdicionar("PerguntaTesteObservação: Pergunta Um", "TesteObservação")));
        await _client.PostAsync("/Pergunta", JsonContent.Create(ObterPerguntaParaAdicionar("PerguntaTesteObservação: Pergunta Dois", "TesteObservação")));
        await _client.PostAsync("/Pergunta", JsonContent.Create(ObterPerguntaParaAdicionar("PerguntaTesteObservação: Pergunta Tres", "TesteObservação")));

        var getperguntas = await _client.GetAsync("/Pergunta?descricao=PerguntaTesteObservação");
        var perguntas = await JsonHelper.LerDoJson<IEnumerable<PerguntaViewModel>>(getperguntas.Content);

        var addQuestionarioDto = new AdicionarQuestionarioDto()
        {
            Nome = "Questionario Teste Observação",
            Perguntas = perguntas.Select(x => x.Id).ToList()
        };

        var postQuestionario = await _client.PostAsync("/Questionario", JsonContent.Create(addQuestionarioDto));
        postQuestionario.EnsureSuccessStatusCode();
        var idQuestionario = await JsonHelper.LerDoJson<Guid>(postQuestionario.Content);

        token = await _usuarioHelper.ObterTokenUsuario(_client, Perfil.Candidato);
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var getQuestionario = await _client.GetAsync($"/Questionario/ObterParaPreenchimento/{idQuestionario}");
        var questionario = await JsonHelper.LerDoJson<QuestionarioViewModelCandidato>(getQuestionario.Content);

        var auxSkip = 0;
        var adicionarAvaliacaoDto = new AdicionarAvaliacaoDto()
        {
            QuestionarioId = questionario.Id,
            Respostas = perguntas.Select(p => new RespostaAvaliacaoDto(
                                                    p.Id,
                                                    p.Alternativas.Skip(auxSkip++).FirstOrDefault()!.Id)
                                         ).ToList()
        };

        var postAvaliacao = await _client.PostAsync("Avaliacao", JsonContent.Create(adicionarAvaliacaoDto));

        token = await _usuarioHelper.ObterTokenUsuario(_client, Perfil.Avaliador);
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var getAvaliacoes = await _client.GetAsync($"Avaliacao?QuestionarioId={idQuestionario}");
        var getAvaliacoesResponse = await JsonHelper.LerDoJson<ICollection<AvaliacaoViewModel>>(getAvaliacoes.Content);

        var putObservacaoAvaliacao = new AdicionarObservacaoAvaliadorDto()
        {
            AvaliacaoId = getAvaliacoesResponse.FirstOrDefault()!.Id,
            ObservacaoAvaliador = "Observação teste"
        };

        //Act
        var putObservacao = await _client.PutAsync("/Avaliacao/AdicionarObservacao", JsonContent.Create(putObservacaoAvaliacao));

        //Assert
        getAvaliacoes = await _client.GetAsync($"Avaliacao?QuestionarioId={idQuestionario}");
        getAvaliacoesResponse = await JsonHelper.LerDoJson<ICollection<AvaliacaoViewModel>>(getAvaliacoes.Content);

        Assert.Equal(HttpStatusCode.OK, putObservacao.StatusCode);
        Assert.NotEmpty(getAvaliacoesResponse);
        Assert.Equal(putObservacaoAvaliacao.ObservacaoAvaliador, getAvaliacoesResponse.FirstOrDefault()!.ObservacaoAvaliador);
    }

    private static AdicionarPerguntaDto ObterPerguntaParaAdicionar(string descricao, string areaConhecimento) =>
        new()
        {
            Descricao = descricao,
            AreaConhecimento = areaConhecimento,
            Alternativas = new List<AlternativaDto>()
            {
                new AlternativaDto("Nova Pergunta Alternativa1", false),
                new AlternativaDto("Nova Pergunta Alternativa2", false),
                new AlternativaDto("Nova Pergunta Alternativa3", true),
            }
        };
}
