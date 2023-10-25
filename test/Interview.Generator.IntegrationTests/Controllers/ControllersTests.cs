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

        private string bearerToken = string.Empty;

        public ControllersTests(InterviewGeneratorWebAppFactory<Program> factory)
        {
            Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Test");

            _client = factory.CreateClient();
        }

        [Fact, TestPriority(1)]
        public async Task AdicaoDeUsuario()
        {
            //Arrange
            var novoUsuario = new AdicionarUsuarioDto("89660569467", "João da Silva", Perfil.Avaliador, _loginAvaliador, _senha);

            //Act
            var responseNovoUsuario = await _client.PostAsync("/Usuario/AdicionarUsuario", JsonContent.Create(novoUsuario));

            //Assert
            Assert.Equal(HttpStatusCode.Created, responseNovoUsuario.StatusCode);
        }

        [Fact, TestPriority(2)]
        public async Task LoginDeUsuario()
        {
            //Arrange
            var loginDto = new GeraTokenUsuario(_loginAvaliador, _senha);

            //Act
            var postLogin = await _client.PostAsync("/Login/GerarToken", JsonContent.Create(loginDto));
            var postLoginResponse = await LerDoJson<LoginViewModel>(postLogin.Content);

            //Assert
            Assert.Equal(HttpStatusCode.OK, postLogin.StatusCode);
            Assert.NotNull(postLoginResponse);
            Assert.False(string.IsNullOrEmpty(postLoginResponse.Token));
        }

        [Fact, TestPriority(3)]
        public async Task AdicionarAreaConhecimento()
        {
            //Arrange
            await Autenticar(_loginAvaliador, _senha);

            var areaConhecimento = new AdicionarAreaConhecimentoDto() { Descricao = ".Net" };

            //Act
            var postAreaConhecimento = await _client.PostAsync("/AreaConhecimento/AdicionarAreaConhecimento", JsonContent.Create(areaConhecimento));

            //Assert
            Assert.Equal(HttpStatusCode.Created, postAreaConhecimento.StatusCode);
        }

        [Fact, TestPriority(4)]
        public async Task EditarAreaConhecimento()
        {
            //Arrange
            await Autenticar(_loginAvaliador, _senha);

            var getAreaConhecimento = await ObterAreaConhecimento();
            
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

            var areaConhecimentoDescricao = "Portugues";

            var postAreaConhecimento = await _client.PostAsync("/AreaConhecimento/AdicionarAreaConhecimento",
                                        JsonContent.Create(new AdicionarAreaConhecimentoDto() { Descricao = areaConhecimentoDescricao }));
            postAreaConhecimento.EnsureSuccessStatusCode();

            var getAreaConhecimento = await ObterAreaConhecimento(areaConhecimentoDescricao);

            //Act
            var deleteAreaConhecimento = await _client.DeleteAsync($"/AreaConhecimento/ExcluirAreaConhecimento/{getAreaConhecimento.FirstOrDefault()!.Id}");

            //Assert
            var getAreaConhecimentoDepoisDelete = await ObterAreaConhecimento(areaConhecimentoDescricao);

            Assert.Equal(HttpStatusCode.OK, deleteAreaConhecimento.StatusCode);
            Assert.Empty(getAreaConhecimentoDepoisDelete);
        }

        //TODO: Alterar area de conhecimento com perguntas

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
    }
}
