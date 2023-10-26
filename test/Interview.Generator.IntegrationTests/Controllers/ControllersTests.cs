using interview.generator.application.Dto;
using interview.generator.application.ViewModels;
using interview.generator.domain.Enum;
using Newtonsoft.Json;
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
        private readonly string _senha = "Teste@integrado123";

        public ControllersTests(InterviewGeneratorWebAppFactory<Program> factory)
        {
            Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Test");

            _client = factory.CreateClient();
        }

        [Fact, TestPriority(1)]
        public async Task AdicionarUsuario()
        {
            //Arrange
            var novoUsuario = new AdicionarUsuarioDto("89660569467", "João da Silva", Perfil.Avaliador, _loginAvaliador, _senha);

            //Act
            var responseNovoUsuario = await _client.PostAsync("/Usuario/AdicionarUsuario", JsonContent.Create(novoUsuario));

            //Assert
            Assert.Equal(HttpStatusCode.Created, responseNovoUsuario.StatusCode);
        }

        [Fact, TestPriority(2)]
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

        [Fact, TestPriority(3)]
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

        [Fact, TestPriority(4)]
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

        [Fact, TestPriority(5)]
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

        [Fact, TestPriority(6)]
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

        [Fact, TestPriority(7)]
        public async Task AlterarPergunta()
        {
            //Arrange
            await Autenticar(_loginAvaliador, _senha);

            var pergunta = (await ObterPergunta()).FirstOrDefault();

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

            pergunta = (await ObterPergunta(alterarPerguntaDto.Descricao)).FirstOrDefault();

            //Assert
            Assert.Equal(HttpStatusCode.OK, putPergunta.StatusCode);
            Assert.NotNull(pergunta);
            Assert.Equal(alterarPerguntaDto.Descricao, pergunta.Descricao);
            Assert.Equal(alterarPerguntaDto.AreaConhecimento, pergunta.Areaconhecimento);
            Assert.Equal(alterarPerguntaDto.Alternativas.Count, pergunta.Alternativas.Count);
        }

        [Fact, TestPriority(8)]
        public async Task ExcluirPergunta()
        {
            //Arrange
            await Autenticar(_loginAvaliador, _senha);
            var perguntaDescricao = "descricaoPergunta2432";

            var pergunta = (await ObterPergunta(perguntaDescricao)).FirstOrDefault();

            //Act
            var deletePergunta = await _client.DeleteAsync($"/Pergunta/ExcluirPergunta/{pergunta!.Id}");
            deletePergunta.EnsureSuccessStatusCode();

            //Assert
            var getPergunta = await ObterPergunta(perguntaDescricao);

            Assert.Equal(HttpStatusCode.OK, deletePergunta.StatusCode);
            Assert.Null(getPergunta);
        }

        [Fact, TestPriority(9)]
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

        private async Task<IEnumerable<PerguntaViewModel>> ObterPergunta(string? descricao = null)
        {
            var url = "/Pergunta/ObterPerguntas";

            if (descricao != null)
                url = $"{url}?descricao={descricao}";

            var getAreaConhecimento = await _client.GetAsync(url);
            getAreaConhecimento.EnsureSuccessStatusCode();

            return await LerDoJson<IEnumerable<PerguntaViewModel>>(getAreaConhecimento.Content);
        }
    }
}
