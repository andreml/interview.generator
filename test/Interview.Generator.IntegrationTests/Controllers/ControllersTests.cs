using interview.generator.application.Dto;
using interview.generator.application.ViewModels;
using interview.generator.domain.Enum;
using Newtonsoft.Json;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Xunit;

namespace Interview.Generator.IntegrationTests.Controllers
{
    [TestCaseOrderer("Interview.Generator.IntegrationTests.PriorityOrderer", "Interview.Generator.IntegrationTests")]
    public class ControllersTests : IClassFixture<InterviewGeneratorWebAppFactory<Program>>
    {
        private readonly HttpClient _client;

        private readonly string _loginAvaliador = "teste.integrado.avaliador";
        private readonly string _loginCandidato = "teste.integrado.candidato";
        private readonly string _senha = "Teste@integrado123";

        public ControllersTests(InterviewGeneratorWebAppFactory<Program> factory)
        {
            Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Test");

            _client = factory.CreateClient();
        }

        [Fact, TestPriority(1)]
        public async Task AdicionarUsuarioAvaliador()
        {
            //Arrange
            var novoUsuario = new AdicionarUsuarioDto("89660569467", "João da Silva", Perfil.Avaliador, _loginAvaliador, _senha);

            //Act
            var responseNovoUsuario = await _client.PostAsync("/Usuario/AdicionarUsuario", JsonContent.Create(novoUsuario));

            //Assert
            Assert.Equal(HttpStatusCode.Created, responseNovoUsuario.StatusCode);
        }

        [Fact, TestPriority(2)]
        public async Task AdicionarUsuarioCandidato()
        {
            //Arrange
            var novoUsuario = new AdicionarUsuarioDto("13971371426", "Maria Lima", Perfil.Candidato, _loginCandidato, _senha);

            //Act
            var responseNovoUsuario = await _client.PostAsync("/Usuario/AdicionarUsuario", JsonContent.Create(novoUsuario));

            //Assert
            Assert.Equal(HttpStatusCode.Created, responseNovoUsuario.StatusCode);
        }

        [Fact, TestPriority(3)]
        public async Task AlterarUsuario()
        {
            //Arrange
            await Autenticar(_loginAvaliador, _senha);

            var alterarUsuarioDto = new AlterarUsuarioDto()
            {
                Cpf = "37756742257",
                Nome = "Guilherme Dos Santos",
                Login = _loginAvaliador,
                Senha = _senha
            };

            //Act
            var putUsuario = await _client.PutAsync("/Usuario/AlterarUsuario", JsonContent.Create(alterarUsuarioDto));
            putUsuario.EnsureSuccessStatusCode();

            //Assert
            var getUsuario = await _client.GetAsync("/Usuario/ObterUsuario");
            getUsuario.EnsureSuccessStatusCode();
            var getUsuarioResponse = await LerDoJson<UsuarioViewModel>(getUsuario.Content);

            Assert.Equal(HttpStatusCode.OK, putUsuario.StatusCode);
            Assert.Equal(alterarUsuarioDto.Cpf, getUsuarioResponse.Cpf);
            Assert.Equal(alterarUsuarioDto.Nome, getUsuarioResponse.Nome);
            Assert.Equal(alterarUsuarioDto.Login, getUsuarioResponse.Login);
        }

        [Fact, TestPriority(4)]
        public async Task AdicionarAreaConhecimento()
        {
            //Arrange
            await Autenticar(_loginAvaliador, _senha);

            var areaConhecimento = new AdicionarAreaConhecimentoDto() { Descricao = "SqlServer" };

            //Act
            var postAreaConhecimento = await _client.PostAsync("/AreaConhecimento/AdicionarAreaConhecimento", JsonContent.Create(areaConhecimento));

            //Assert
            Assert.Equal(HttpStatusCode.Created, postAreaConhecimento.StatusCode);
        }

        [Fact, TestPriority(5)]
        public async Task AlterarAreaConhecimento()
        {
            //Arrange
            await Autenticar(_loginAvaliador, _senha);

            var getAreaConhecimento = await ObterAreaConhecimento("SqlServer");
            
            var alterarAreaConhecimento = new AlterarAreaConhecimentoDto() { 
                Descricao = "Matematica",
                Id = getAreaConhecimento.FirstOrDefault()!.Id
            };

            //Act
            var putAreaConhecimento = await _client.PutAsync("/AreaConhecimento/AlterarAreaConhecimento", JsonContent.Create(alterarAreaConhecimento));

            //Assert
            getAreaConhecimento = await ObterAreaConhecimento("Matematica");

            Assert.Equal(HttpStatusCode.OK, putAreaConhecimento.StatusCode);
            Assert.NotEmpty(getAreaConhecimento);
            Assert.Equal(alterarAreaConhecimento.Descricao, getAreaConhecimento.FirstOrDefault()!.Descricao);
        }

        [Fact, TestPriority(6)]
        public async Task ExcluirAreaConhecimento()
        {
            //Arrange
            await Autenticar(_loginAvaliador, _senha);

            var areaConhecimentoDescricao = "Matematica";

            var getAreaConhecimento = await ObterAreaConhecimento(areaConhecimentoDescricao);

            //Act
            var deleteAreaConhecimento = await _client.DeleteAsync($"/AreaConhecimento/ExcluirAreaConhecimento/{getAreaConhecimento.FirstOrDefault()!.Id}");

            //Assert
            var getAreaConhecimentoDepoisDelete = await ObterAreaConhecimento(areaConhecimentoDescricao);

            Assert.Equal(HttpStatusCode.OK, deleteAreaConhecimento.StatusCode);
            Assert.Empty(getAreaConhecimentoDepoisDelete);
        }

        [Fact, TestPriority(7)]
        public async Task AdicionarPergunta()
        {
            //Arrange
            await Autenticar(_loginAvaliador, _senha);

            var perguntaDto = new AdicionarPerguntaDto()
            {
                Descricao = "Quanto é 1+1?",
                AreaConhecimento = "Matematica",
                Alternativas = new List<AlternativaDto>()
                {
                    new AlternativaDto("É 3", false),
                    new AlternativaDto("É 2", true),
                    new AlternativaDto("É 5", false)
                }
            };

            //Act
            var postPergunta = await _client.PostAsync("/Pergunta/AdicionarPergunta", JsonContent.Create(perguntaDto));

            //Assert
            Assert.Equal(HttpStatusCode.Created, postPergunta.StatusCode);
        }

        [Fact, TestPriority(8)]
        public async Task AlterarPergunta()
        {
            //Arrange
            await Autenticar(_loginAvaliador, _senha);

            var pergunta = (await ObterPerguntas()).FirstOrDefault();

            var alterarPerguntaDto = new AlterarPerguntaDto()
            {
                Id = pergunta!.Id,
                Descricao = "descricaoPergunta2432",
                AreaConhecimento = "TesteDescricao12543",
                Alternativas = new List<AlterarAlternativaDto>()
                {
                    new AlterarAlternativaDto("Alternativa 1", true),
                    new AlterarAlternativaDto("Alternativa 2", false),
                    new AlterarAlternativaDto("Alternativa 3", false),
                    new AlterarAlternativaDto("Alternativa 4", false)
                }
            };

            //Act
            var putPergunta = await _client.PutAsync("/Pergunta/AlterarPergunta", JsonContent.Create(alterarPerguntaDto));
            putPergunta.EnsureSuccessStatusCode();

            pergunta = (await ObterPerguntas(alterarPerguntaDto.Descricao)).FirstOrDefault();

            //Assert
            Assert.Equal(HttpStatusCode.OK, putPergunta.StatusCode);
            Assert.NotNull(pergunta);
            Assert.Equal(alterarPerguntaDto.Descricao, pergunta.Descricao);
            Assert.Equal(alterarPerguntaDto.AreaConhecimento, pergunta.Areaconhecimento);
            Assert.Equal(alterarPerguntaDto.Alternativas.Count, pergunta.Alternativas.Count);
        }

        [Fact, TestPriority(9)]
        public async Task ExcluirPergunta()
        {
            //Arrange
            await Autenticar(_loginAvaliador, _senha);
            var perguntaDescricao = "descricaoPergunta2432";

            var pergunta = (await ObterPerguntas(perguntaDescricao)).FirstOrDefault();

            //Act
            var deletePergunta = await _client.DeleteAsync($"/Pergunta/ExcluirPergunta/{pergunta!.Id}");
            deletePergunta.EnsureSuccessStatusCode();

            //Assert
            var getPergunta = await ObterPerguntas(perguntaDescricao);

            Assert.Equal(HttpStatusCode.OK, deletePergunta.StatusCode);
            Assert.Null(getPergunta);
        }

        [Fact, TestPriority(10)]
        public async Task AlterarAreaDeConhecimentoComPerguntasVinculadas()
        {
            //Arrange
            await Autenticar(_loginAvaliador, _senha);

            var perguntaDto = new AdicionarPerguntaDto()
            {
                Descricao = "Qual o resultado deste comando no JS? console.log('1'+'1')",
                AreaConhecimento = "BancoDeDados",
                Alternativas = new List<AlternativaDto>()
                {
                    new AlternativaDto("11", true),
                    new AlternativaDto("2", false),
                    new AlternativaDto("Erro", false)
                }
            };

            var postPergunta = await _client.PostAsync("/Pergunta/AdicionarPergunta", JsonContent.Create(perguntaDto));
            postPergunta.EnsureSuccessStatusCode();

            var getAreaConhecimento = await ObterAreaConhecimento("BancoDeDados");

            var alterarAreaConhecimento = new AlterarAreaConhecimentoDto()
            {
                Descricao = "JavaScript",
                Id = getAreaConhecimento.FirstOrDefault()!.Id
            };

            //Act
            var putAreaConhecimento = await _client.PutAsync("/AreaConhecimento/AlterarAreaConhecimento", JsonContent.Create(alterarAreaConhecimento));

            //Assert
            getAreaConhecimento = await ObterAreaConhecimento("JavaScript");

            Assert.Equal(HttpStatusCode.OK, putAreaConhecimento.StatusCode);
            Assert.NotEmpty(getAreaConhecimento);
            Assert.Equal(1, getAreaConhecimento.FirstOrDefault()!.PerguntasCadastradas);
        }

        [Fact, TestPriority(11)]
        public async Task AdicionarQuestionario()
        {
            //Arrange
            await Autenticar(_loginAvaliador, _senha);

            var perguntaDto1 = new AdicionarPerguntaDto()
            {
                Descricao = "TesteQuestionario: Pergunta Um",
                AreaConhecimento = "TesteQuestionario",
                Alternativas = new List<AlternativaDto>()
                {
                    new AlternativaDto("PerguntaUm Alternativa1", true),
                    new AlternativaDto("PerguntaUm Alternativa2", false),
                    new AlternativaDto("PerguntaUm Alternativa3", false)
                }
            };

            var perguntaDto2 = new AdicionarPerguntaDto()
            {
                Descricao = "TesteQuestionario: Pergunta Dois",
                AreaConhecimento = "TesteQuestionario",
                Alternativas = new List<AlternativaDto>()
                {
                    new AlternativaDto("PerguntaDois Alternativa1", false),
                    new AlternativaDto("PerguntaDois Alternativa2", false),
                    new AlternativaDto("PerguntaDois Alternativa3", false),
                    new AlternativaDto("PerguntaDois Alternativa4", true)
                }
            };

            var perguntaDto3 = new AdicionarPerguntaDto()
            {
                Descricao = "TesteQuestionario: Pergunta Tres",
                AreaConhecimento = "TesteQuestionario",
                Alternativas = new List<AlternativaDto>()
                {
                    new AlternativaDto("PerguntaTres Alternativa1", false),
                    new AlternativaDto("PerguntaTres Alternativa2", false),
                    new AlternativaDto("PerguntaTres Alternativa3", true),
                    new AlternativaDto("PerguntaTres Alternativa4", false),
                    new AlternativaDto("PerguntaTres Alternativa5", false)
                }
            };

            await _client.PostAsync("/Pergunta/AdicionarPergunta", JsonContent.Create(perguntaDto1));
            await _client.PostAsync("/Pergunta/AdicionarPergunta", JsonContent.Create(perguntaDto2));
            await _client.PostAsync("/Pergunta/AdicionarPergunta", JsonContent.Create(perguntaDto3));

            var perguntas = await ObterPerguntas("TesteQuestionario");

            var addQuestionarioDto = new AdicionarQuestionarioDto()
            {
                Nome = "Questionario Teste 1",
                Perguntas = perguntas.Select(x => x.Id).ToList()
            };

            //Act
            var postQuestionario = await _client.PostAsync("/Questionario/AdicionarQuestionario", JsonContent.Create(addQuestionarioDto));
            postQuestionario.EnsureSuccessStatusCode();

            //Assert
            var getQuestionario = await ObterQuestionarios(addQuestionarioDto.Nome);

            Assert.Equal(HttpStatusCode.Created, postQuestionario.StatusCode);
            Assert.NotEmpty(getQuestionario);
            Assert.Equal(getQuestionario.FirstOrDefault()!.Nome, addQuestionarioDto.Nome);

            Assert.Contains(perguntaDto1.Descricao, getQuestionario.FirstOrDefault()!.Perguntas.Select(x => x.Descricao));
            Assert.Contains(perguntaDto2.Descricao, getQuestionario.FirstOrDefault()!.Perguntas.Select(x => x.Descricao));
            Assert.Contains(perguntaDto3.Descricao, getQuestionario.FirstOrDefault()!.Perguntas.Select(x => x.Descricao));
        }

        [Fact, TestPriority(12)]
        public async Task AlterarQuestionario()
        {
            //Arrange
            await Autenticar(_loginAvaliador, _senha);

            var perguntaDto1 = new AdicionarPerguntaDto()
            {
                Descricao = "TesteQuestionario: Nova Pergunta Um",
                AreaConhecimento = "TesteQuestionario 2",
                Alternativas = new List<AlternativaDto>()
                {
                    new AlternativaDto("Nova PerguntaUm Alternativa1", true),
                    new AlternativaDto("Nova PerguntaUm Alternativa2", false),
                    new AlternativaDto("Nova PerguntaUm Alternativa3", false)
                }
            };

            var perguntaDto2 = new AdicionarPerguntaDto()
            {
                Descricao = "TesteQuestionario: Nova Pergunta Dois",
                AreaConhecimento = "TesteQuestionario 2",
                Alternativas = new List<AlternativaDto>()
                {
                    new AlternativaDto("Nova PerguntaDois Alternativa1", false),
                    new AlternativaDto("Nova PerguntaDois Alternativa2", false),
                    new AlternativaDto("Nova PerguntaDois Alternativa3", false),
                    new AlternativaDto("Nova PerguntaDois Alternativa4", true)
                }
            };

            var perguntaDto3 = new AdicionarPerguntaDto()
            {
                Descricao = "TesteQuestionario: Nova Pergunta Tres",
                AreaConhecimento = "TesteQuestionario 2",
                Alternativas = new List<AlternativaDto>()
                {
                    new AlternativaDto("Nova PerguntaTres Alternativa1", false),
                    new AlternativaDto("Nova PerguntaTres Alternativa2", false),
                    new AlternativaDto("Nova PerguntaTres Alternativa3", true),
                    new AlternativaDto("Nova PerguntaTres Alternativa4", false),
                    new AlternativaDto("Nova PerguntaTres Alternativa5", false)
                }
            };

            await _client.PostAsync("/Pergunta/AdicionarPergunta", JsonContent.Create(perguntaDto1));
            await _client.PostAsync("/Pergunta/AdicionarPergunta", JsonContent.Create(perguntaDto2));
            await _client.PostAsync("/Pergunta/AdicionarPergunta", JsonContent.Create(perguntaDto3));

            var perguntas = await ObterPerguntas("TesteQuestionario: Nova Pergunta");

            var questionario = await ObterQuestionarios("Questionario Teste 1");

            var alterarQuestionarioDto = new AlterarQuestionarioDto()
            {
                QuestionarioId = questionario.FirstOrDefault()!.Id,
                Nome = "Questionario Teste 1 Alterado",
                Perguntas = perguntas.Select(x => x.Id).ToList()
            };

            //Act
            var putQuestionario = await _client.PutAsync("/Questionario/AlterarQuestionario", JsonContent.Create(alterarQuestionarioDto));
            putQuestionario.EnsureSuccessStatusCode();

            //Assert
            questionario = await ObterQuestionarios("Questionario Teste 1 Alterado");
            perguntas = await ObterPerguntas();

            Assert.Equal(HttpStatusCode.OK, putQuestionario.StatusCode);
            Assert.NotEmpty(questionario);

            Assert.Equal(alterarQuestionarioDto.Nome, questionario.FirstOrDefault()!.Nome);
            Assert.Contains(perguntaDto1.Descricao, questionario.FirstOrDefault()!.Perguntas.Select(x => x.Descricao));
            Assert.Contains(perguntaDto2.Descricao, questionario.FirstOrDefault()!.Perguntas.Select(x => x.Descricao));
            Assert.Contains(perguntaDto3.Descricao, questionario.FirstOrDefault()!.Perguntas.Select(x => x.Descricao));

            Assert.NotEmpty(perguntas);
            Assert.Equal(7, perguntas.Count());
        }

        [Fact, TestPriority(13)]
        public async Task ExcluirQuestionarioSemExcluirPerguntas()
        {
            //Arrange
            await Autenticar(_loginAvaliador, _senha);

            var getQuestionario = await ObterQuestionarios("Questionario Teste 1 Alterado");

            //Act
            var deleteQuestionario = await _client.DeleteAsync($"/Questionario/ExcluirQuestionario/{getQuestionario.FirstOrDefault()!.Id}");
            deleteQuestionario.EnsureSuccessStatusCode();

            //Assert
            var perguntas = await ObterPerguntas();

            getQuestionario = await ObterQuestionarios("Questionario Teste 1");

            Assert.Equal(7, perguntas.Count());
            Assert.Empty(getQuestionario);
        }

        [Fact, TestPriority(14)]
        public async Task AdicionarAvaliacao()
        {
            //Arrange
            await Autenticar(_loginAvaliador, _senha);

            var perguntaDto1 = new AdicionarPerguntaDto()
            {
                Descricao = "Calcule o resultado de 1 + 1?",
                AreaConhecimento = "Matematica Teste",
                Alternativas = new List<AlternativaDto>()
                {
                    new AlternativaDto("2", true),
                    new AlternativaDto("11", false),
                    new AlternativaDto("3", false)
                }
            };

            var perguntaDto2 = new AdicionarPerguntaDto()
            {
                Descricao = "Calcule o resultado de 8 * 7",
                AreaConhecimento = "Matematica Teste",
                Alternativas = new List<AlternativaDto>()
                {
                    new AlternativaDto("52", false),
                    new AlternativaDto("34", false),
                    new AlternativaDto("48", false),
                    new AlternativaDto("56", true)
                }
            };

            var perguntaDto3 = new AdicionarPerguntaDto()
            {
                Descricao = "Calcule o resultado de 50% de 200",
                AreaConhecimento = "Matematica Teste",
                Alternativas = new List<AlternativaDto>()
                {
                    new AlternativaDto("50", false),
                    new AlternativaDto("200", false),
                    new AlternativaDto("100", true),
                    new AlternativaDto("1000", false),
                    new AlternativaDto("0", false)
                }
            };

            var perguntaDto4 = new AdicionarPerguntaDto()
            {
                Descricao = "Calcule o resultado de 1000 / 4",
                AreaConhecimento = "Matematica Teste",
                Alternativas = new List<AlternativaDto>()
                {
                    new AlternativaDto("500", false),
                    new AlternativaDto("250", true),
                    new AlternativaDto("4000", false),
                    new AlternativaDto("1000", false),
                    new AlternativaDto("666", false)
                }
            };

            await _client.PostAsync("/Pergunta/AdicionarPergunta", JsonContent.Create(perguntaDto1));
            await _client.PostAsync("/Pergunta/AdicionarPergunta", JsonContent.Create(perguntaDto2));
            await _client.PostAsync("/Pergunta/AdicionarPergunta", JsonContent.Create(perguntaDto3));
            await _client.PostAsync("/Pergunta/AdicionarPergunta", JsonContent.Create(perguntaDto4));

            var perguntas = await ObterPerguntas("Calcule o resultado de");
;
            var addQuestionarioDto = new AdicionarQuestionarioDto()
            {
                Nome = "Questionario matemática teste",
                Perguntas = perguntas.Select(x => x.Id).ToList()
            };

            var postQuestionario = await _client.PostAsync("/Questionario/AdicionarQuestionario", JsonContent.Create(addQuestionarioDto));
            postQuestionario.EnsureSuccessStatusCode();

            var questionario = await ObterQuestionarios("Questionario matemática teste");

            var auxSkip = 0;
            var adicionarAvaliacaoDto = new AdicionarAvaliacaoDto()
            {
                QuestionarioId = questionario.FirstOrDefault()!.Id,
                Respostas = perguntas.Select(p => new RespostaAvaliacaoDto(
                                                        p.Id,
                                                        p.Alternativas.Skip(auxSkip++).FirstOrDefault()!.Id)
                                             ).ToList()
            };

            //Act
            await Autenticar(_loginCandidato, _senha);

            var postAvaliacao = await _client.PostAsync("Avaliacao/Adicionar", JsonContent.Create(adicionarAvaliacaoDto));
            postAvaliacao.EnsureSuccessStatusCode();

            //Assert
            await Autenticar(_loginAvaliador, _senha);

            var getAvaliacoes = await _client.GetAsync("Avaliacao/ObterAvaliacoesPorFiltro?nomeQuestionario=Questionario matemática teste");
            var getAvaliacoesResponse = await LerDoJson<ICollection<AvaliacaoViewModel>>(getAvaliacoes.Content);

            Assert.NotEmpty(getAvaliacoesResponse);
            Assert.Equal(questionario.FirstOrDefault()!.Nome, getAvaliacoesResponse.FirstOrDefault()!.NomeQuestionario);
            Assert.Equal(questionario.FirstOrDefault()!.Perguntas.Count, getAvaliacoesResponse.FirstOrDefault()!.Respostas.Count());
        }

        [Fact, TestPriority(15)]
        public async Task AdicionarObservacaoAvaliacao()
        {
            //Arrange
            await Autenticar(_loginAvaliador, _senha);

            var getAvaliacoes = await _client.GetAsync("Avaliacao/ObterAvaliacoesPorFiltro?nomeQuestionario=Questionario matemática teste");
            var getAvaliacoesResponse = await LerDoJson<ICollection<AvaliacaoViewModel>>(getAvaliacoes.Content);

            var putObservacaoAvaliacao = new AdicionarObservacaoAvaliadorDto()
            {
                AvaliacaoId = getAvaliacoesResponse.FirstOrDefault()!.Id,
                ObservacaoAvaliador = "Observação teste"
            };

            //Act
            var putObservacao = await _client.PutAsync("/Avaliacao/AdicionarObservacao", JsonContent.Create(putObservacaoAvaliacao));
            putObservacao.EnsureSuccessStatusCode();

            //Assert
            getAvaliacoes = await _client.GetAsync("Avaliacao/ObterAvaliacoesPorFiltro?nomeQuestionario=Questionario matemática teste");
            getAvaliacoesResponse = await LerDoJson<ICollection<AvaliacaoViewModel>>(getAvaliacoes.Content);

            Assert.Equal(HttpStatusCode.OK, putObservacao.StatusCode);
            Assert.NotEmpty(getAvaliacoesResponse);
            Assert.Equal(putObservacaoAvaliacao.ObservacaoAvaliador, getAvaliacoesResponse.FirstOrDefault()!.ObservacaoAvaliador);
        }

        //Metodos auxiliares
        private static async Task<T> LerDoJson<T>(HttpContent content)
        {
            var responseString = await content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(responseString)!;
        }

        private async Task Autenticar(string login, string senha)
        {
            var postLogin = await _client.PostAsync("/Login/GerarToken", JsonContent.Create(new GeraTokenUsuario(login, senha)));

            var postLoginResponse = await LerDoJson<LoginViewModel>(postLogin.Content);

            _client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", postLoginResponse.Token);
        }

        private async Task<IEnumerable<AreaConhecimentoViewModel>> ObterAreaConhecimento(string? descricao = null)
        {
            var url = "/AreaConhecimento/ObterAreasConhecimento";

            if (descricao != null)
                url = $"{url}?descricao={descricao}";

            var getAreaConhecimento = await _client.GetAsync(url);
            getAreaConhecimento.EnsureSuccessStatusCode();

            return  await LerDoJson<IEnumerable<AreaConhecimentoViewModel>>(getAreaConhecimento.Content);
        }

        private async Task<IEnumerable<PerguntaViewModel>> ObterPerguntas(string? descricao = null)
        {
            var url = "/Pergunta/ObterPerguntas";

            if (descricao != null)
                url = $"{url}?descricao={descricao}";

            var getAreaConhecimento = await _client.GetAsync(url);
            getAreaConhecimento.EnsureSuccessStatusCode();

            return await LerDoJson<IEnumerable<PerguntaViewModel>>(getAreaConhecimento.Content);
        }

        private async Task<IEnumerable<QuestionarioViewModel>> ObterQuestionarios(string? nome = null)
        {
            var url = "/Questionario/ObterQuestionarios";

            if (nome != null)
                url = $"{url}?nome={nome}";

            var getQuestionario = await _client.GetAsync(url);
            getQuestionario.EnsureSuccessStatusCode();

            return await LerDoJson<IEnumerable<QuestionarioViewModel>>(getQuestionario.Content);
        }
    }
}
