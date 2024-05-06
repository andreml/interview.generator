using InterviewGenerator.Application.Dto;
using InterviewGenerator.Application.ViewModels;
using InterviewGenerator.Domain.Enum;
using System.Net.Http.Json;

namespace InterviewGenerator.IntegrationTests.API.Utils;

public class UsuarioHelper
{
    public readonly string LoginAvaliador = "teste.integrado.avaliador";
    public readonly string LoginCandidato = "teste.integrado.candidato";

    public readonly string CpfAvaliador = "19826624411";
    public readonly string CpfCandidato = "78117459256";

    public readonly string SenhaUsuario = "Teste@integrado123";

    public async Task<string> ObterTokenUsuario(HttpClient client, Perfil perfil)
    {
        var gerarTokenUsuarioDto = new GerarTokenUsuarioDto(ObterLogin(perfil), SenhaUsuario);

        var postLogin = await client.PostAsync("/Usuario/Autenticar", JsonContent.Create(gerarTokenUsuarioDto));

        if (!postLogin.IsSuccessStatusCode)
        {
            await client.PostAsync("/Usuario",
                JsonContent.Create(new AdicionarUsuarioDto(ObterCpf(perfil), "Felipe", perfil, ObterLogin(perfil), SenhaUsuario)));

            postLogin = await client.PostAsync("/Usuario/Autenticar", JsonContent.Create(gerarTokenUsuarioDto));
        }

        var postLoginResponse = await JsonHelper.LerDoJson<LoginViewModel>(postLogin.Content);

        return postLoginResponse.Token;
    }

    private string ObterLogin(Perfil perfil) =>
        perfil == Perfil.Avaliador ? LoginAvaliador : LoginCandidato;

    private string ObterCpf(Perfil perfil) =>
        perfil == Perfil.Avaliador ? CpfAvaliador : CpfCandidato;
}
