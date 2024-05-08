using InterviewGenerator.Application.Interfaces;
using InterviewGenerator.Domain.Entidade.Common;
using MassTransit;

namespace InterviewGenerator.Application.Services;

public class MassTransitService : IMassTransitService
{
    private readonly IBus _bus;
    public MassTransitService(IBus bus)
    {
        _bus = bus;
    }

    public async Task<ResponseBase> InserirMensagem(object mensagem, string fila)
    {
        var response = new ResponseBase();

        var endpoint = await _bus.GetSendEndpoint(new Uri($"queue:{fila}"));

        await endpoint.Send(mensagem);

        return response;
    }
}
