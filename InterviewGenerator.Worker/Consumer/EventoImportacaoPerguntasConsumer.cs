using InterviewGenerator.Application.Dto;
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
            var token = Token();

            Console.WriteLine(context.Message);
            return Task.CompletedTask;
        }

        public string Token()
        {
            //var options = new RestClientOptions("")
            //{
            //    MaxTimeout = -1,
            //};
            var client = new RestClient();
            var request = new RestRequest("https://localhost:44357/Usuario/Autenticar", Method.Post);
            request.AddHeader("Content-Type", "application/json");
            var body = new GerarTokenUsuarioDto("jonas", "jonas123");
            request.AddStringBody(JsonConvert.SerializeObject(body), DataFormat.Json);
            RestResponse response = client.Execute(request);
            return response.Content;
        }

        public
    }
}
    