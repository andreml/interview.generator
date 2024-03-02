using InterviewGenerator.Application.Interfaces;
using InterviewGenerator.Domain.Entidade;
using InterviewGenerator.Domain.Entidade.Common;
using MassTransit;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewGenerator.Application.Services
{
    public class MassTransitService : IMassTransitService
    {
        private readonly IBus _bus;
        public MassTransitService(IBus bus)
        {
            _bus = bus;
        }
        public Task<ResponseBase> InserirEmFilaDeErro()
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseBase> InserirMensagem(object mensagem, string fila)
        {
            var response = new ResponseBase();


            var endpoint = await _bus.GetSendEndpoint(new Uri($"queue:{fila}"));

            await endpoint.Send(mensagem);

            return response;
        }
    }
}
