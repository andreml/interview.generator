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
    public async Task GivenValidQuiz_WhenSendingQuiz_ThenShouldSendQuiz()
    {
        //Arrange
        //criar usuario Candidato
        await _usuarioHelper.ObterTokenUsuario(_client, Perfil.Candidato);

        var token = await _usuarioHelper.ObterTokenUsuario(_client, Perfil.Avaliador);
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        await _client.PostAsync("/Pergunta", JsonContent.Create(ObterPerguntaParaAdicionar("PerguntaTesteEnvioAvaliacao: Pergunta Um", "TesteAvaliacao")));
        await _client.PostAsync("/Pergunta", JsonContent.Create(ObterPerguntaParaAdicionar("PerguntaTesteEnvioAvaliacao: Pergunta Dois", "TesteAvaliacao")));
        await _client.PostAsync("/Pergunta", JsonContent.Create(ObterPerguntaParaAdicionar("PerguntaTesteEnvioAvaliacao: Pergunta Tres", "TesteAvaliacao")));

        var getperguntas = await _client.GetAsync("/pergunta?descricao=PerguntaTesteEnvioAvaliacao");
        var perguntas = await JsonHelper.LerDoJson<IEnumerable<PerguntaViewModel>>(getperguntas.Content);

        var addQuestionarioDto = new AdicionarQuestionarioDto()
        {
            Nome = "Questionario Teste Avaliacao",
            Perguntas = perguntas.Select(x => x.Id).ToList()
        };

        var postQuestionario = await _client.PostAsync("/Questionario", JsonContent.Create(addQuestionarioDto));
        postQuestionario.EnsureSuccessStatusCode();
        var idQuestionario = await JsonHelper.LerDoJson<Guid>(postQuestionario.Content);

        var enviarAvaliacaoDto = new EnviarAvaliacaoParaCandidatoDto()
        {
            QuestionarioId = idQuestionario,
            LoginCandidato = _usuarioHelper.LoginCandidato
        };

        //Act
        var postAvaliacao = await _client.PostAsync("avaliacao/enviarParaCandidato", JsonContent.Create(enviarAvaliacaoDto));

        //Assert
        Assert.Equal(HttpStatusCode.Created, postAvaliacao.StatusCode);
    }

    [Fact]
    public async Task GivenValidQuiz_WhenAddingNote_ThenShouldAddNote()
    {
        //Arrange
        //criar usuario Candidato
        await _usuarioHelper.ObterTokenUsuario(_client, Perfil.Candidato);

        var token = await _usuarioHelper.ObterTokenUsuario(_client, Perfil.Avaliador);
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        await _client.PostAsync("/Pergunta", JsonContent.Create(ObterPerguntaParaAdicionar("PerguntaTesteObservação: Pergunta Um", "TesteAvaliacao")));
        await _client.PostAsync("/Pergunta", JsonContent.Create(ObterPerguntaParaAdicionar("PerguntaTesteObservação: Pergunta Dois", "TesteAvaliacao")));
        await _client.PostAsync("/Pergunta", JsonContent.Create(ObterPerguntaParaAdicionar("PerguntaTesteObservação: Pergunta Tres", "TesteAvaliacao")));

        var getperguntas = await _client.GetAsync("/pergunta?descricao=PerguntaTesteObservação");
        var perguntas = await JsonHelper.LerDoJson<IEnumerable<PerguntaViewModel>>(getperguntas.Content);

        var addQuestionarioDto = new AdicionarQuestionarioDto()
        {
            Nome = "Questionario Teste Observacao",
            Perguntas = perguntas.Select(x => x.Id).ToList()
        };

        var postQuestionario = await _client.PostAsync("/Questionario", JsonContent.Create(addQuestionarioDto));
        postQuestionario.EnsureSuccessStatusCode();
        var idQuestionario = await JsonHelper.LerDoJson<Guid>(postQuestionario.Content);

        var enviarAvaliacaoDto = new EnviarAvaliacaoParaCandidatoDto()
        {
            QuestionarioId = idQuestionario,
            LoginCandidato = _usuarioHelper.LoginCandidato
        };
        var postAvaliacao = await _client.PostAsync("avaliacao/enviarParaCandidato", JsonContent.Create(enviarAvaliacaoDto));
        var idAvaliacao = await JsonHelper.LerDoJson<Guid>(postAvaliacao.Content);

        var putObservacaoAvaliacao = new AdicionarObservacaoAvaliadorDto()
        {
            AvaliacaoId = idAvaliacao,
            ObservacaoAvaliador = "Observação teste"
        };

        //Act
        var putObservacao = await _client.PutAsync("/avaliacao/adicionarObservacao", JsonContent.Create(putObservacaoAvaliacao));

        //Assert
        Assert.Equal(HttpStatusCode.Created, postAvaliacao.StatusCode);
        var getAvaliacao = await _client.GetAsync($"Avaliacao/detalhes/{idAvaliacao}");
        var getAvaliacoaosResponse = await JsonHelper.LerDoJson<AvaliacaoDetalheViewModel>(getAvaliacao.Content);

        Assert.Equal(HttpStatusCode.OK, putObservacao.StatusCode);
        Assert.NotNull(getAvaliacoaosResponse);
        Assert.Equal(putObservacaoAvaliacao.ObservacaoAvaliador, getAvaliacoaosResponse.ObservacaoAvaliador);
    }

    [Fact]
    public async Task GivenValidAnswer_WhenAnsweringQuiz_ThenShouldReturnSuccess()
    {
        //Arrange
        //criar usuario Candidato
        await _usuarioHelper.ObterTokenUsuario(_client, Perfil.Candidato);

        var token = await _usuarioHelper.ObterTokenUsuario(_client, Perfil.Avaliador);
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        await _client.PostAsync("/Pergunta", JsonContent.Create(ObterPerguntaParaAdicionar("PerguntaTesteRespostaAvaliacao: Pergunta Um", "TesteAvaliacao")));
        await _client.PostAsync("/Pergunta", JsonContent.Create(ObterPerguntaParaAdicionar("PerguntaTesteRespostaAvaliacao: Pergunta Dois", "TesteAvaliacao")));
        await _client.PostAsync("/Pergunta", JsonContent.Create(ObterPerguntaParaAdicionar("PerguntaTesteRespostaAvaliacao: Pergunta Tres", "TesteAvaliacao")));

        var getperguntas = await _client.GetAsync("/pergunta?descricao=PerguntaTesteRespostaAvaliacao");
        var perguntas = await JsonHelper.LerDoJson<IEnumerable<PerguntaViewModel>>(getperguntas.Content);

        var addQuestionarioDto = new AdicionarQuestionarioDto()
        {
            Nome = "Questionario Teste Resposta Avaliacao",
            Perguntas = perguntas.Select(x => x.Id).ToList()
        };

        var postQuestionario = await _client.PostAsync("/Questionario", JsonContent.Create(addQuestionarioDto));
        postQuestionario.EnsureSuccessStatusCode();
        var idQuestionario = await JsonHelper.LerDoJson<Guid>(postQuestionario.Content);

        var enviarAvaliacaoDto = new EnviarAvaliacaoParaCandidatoDto()
        {
            QuestionarioId = idQuestionario,
            LoginCandidato = _usuarioHelper.LoginCandidato
        };
        var postAvaliacao = await _client.PostAsync("avaliacao/enviarParaCandidato", JsonContent.Create(enviarAvaliacaoDto));
        var idAvaliacao = await JsonHelper.LerDoJson<Guid>(postAvaliacao.Content);

        token = await _usuarioHelper.ObterTokenUsuario(_client, Perfil.Candidato);
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var getUusuario = await _client.GetAsync("usuario");
        var usuario = await JsonHelper.LerDoJson<UsuarioViewModel>(getUusuario.Content);

        var respostaAvaliacaoDto = new ResponderAvaliacaoDto
        {
            AvaliacaoId = idAvaliacao,
            CandidatoId = usuario.Id,
            Respostas = perguntas.Select(p => new RespostaAvaliacaoDto(p.Id, p.Alternativas.FirstOrDefault()!.Id)).ToList()
        };

        //Act
        var putAvaliacao = await _client.PutAsync("avaliacao/responder", JsonContent.Create(respostaAvaliacaoDto));

        //Assert
        Assert.Equal(HttpStatusCode.Created, postAvaliacao.StatusCode);
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
