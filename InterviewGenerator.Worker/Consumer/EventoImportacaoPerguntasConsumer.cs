using InterviewGenerator.Application.Dto;
using InterviewGenerator.Application.ViewModels;
using InterviewGenerator.CrossCutting.Eventos;
using MassTransit;
using Newtonsoft.Json;
using RestSharp;
using System.Text.Json.Serialization;

namespace InterviewGenerator.Worker.Consumer
{
    public class EventoImportacaoPerguntasConsumer : IConsumer<ImportarArquivoDto>
    {
        public Task Consume(ConsumeContext<ImportarArquivoDto> context)
        {
            var responseToken = Token();
            if (responseToken.StatusCode == System.Net.HttpStatusCode.OK) 
            {
                var token = JsonConvert.DeserializeObject<LoginViewModel>(responseToken.Content);
                var responsePergunta = ImportarPergunta(token.Token, context.Message.Pergunta);
            }

            Console.WriteLine(context.Message);
            return Task.CompletedTask;
        }

        public RestResponse Token()
        {
            
            var client = new RestClient();
            var request = new RestRequest("https://localhost:44357/Usuario/Autenticar", Method.Post);
            request.AddHeader("Content-Type", "application/json");
            var body = new GerarTokenUsuarioDto("jonas", "jonas123");
            request.AddStringBody(JsonConvert.SerializeObject(body), DataFormat.Json);
            RestResponse response = client.Execute(request);
            return response;
        }

        public RestResponse ImportarPergunta(string? accessToken, AdicionarPerguntaDto pergunta)
        {
            var client = new RestClient();
            var request = new RestRequest("https://localhost:44357/Pergunta/Adicionar", Method.Post);
            request.AddHeader("Authorization", $"Bearer {accessToken}");
            request.AddHeader("Content-Type", "application/json");
            var body = JsonConvert.SerializeObject(pergunta);
            request.AddStringBody(body, DataFormat.Json);
            RestResponse response = client.Execute(request);
            return response;
            
        }

    }
}
    