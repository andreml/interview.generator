using InterviewGenerator.Application.Dto;
using InterviewGenerator.Application.ViewModels;
using InterviewGenerator.CrossCutting.Eventos;
using InterviewGenerator.Domain.Repositorio;
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
                var atualizaStatusDto = new AlterarLinhaArquivoDto { 
                    DataProcessamento = DateTime.Now,
                    Erro = responsePergunta.Content,
                    NumeroLinha = context.Message.Pergunta.NumeroLinha.Value,
                    IdControleImportacao = context.Message.IdArquivo
                };

                var statusProcessamento = AtualizaStatus(token.Token, atualizaStatusDto);
                return Task.CompletedTask;
            }
            else
            {
                return Task.Delay(1000);
            }
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

        public RestResponse AtualizaStatus(string? accessToken, AlterarLinhaArquivoDto linha)
        {
            var client = new RestClient();
            var request = new RestRequest("https://localhost:44357/ImportacaoPerguntas/AtualizarControleLinhaArquivo", Method.Post);
            request.AddHeader("Authorization", $"Bearer {accessToken}");
            request.AddHeader("Content-Type", "application/json");
            var body = JsonConvert.SerializeObject(linha);
            request.AddStringBody(body, DataFormat.Json);
            RestResponse response = client.Execute(request);
            return response;
        }

    }
}
    