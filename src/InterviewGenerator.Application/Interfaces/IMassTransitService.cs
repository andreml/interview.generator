using InterviewGenerator.Domain.Entidade.Common;

namespace InterviewGenerator.Application.Interfaces
{
    public interface IMassTransitService
    {
        Task<ResponseBase> InserirMensagem(object modelMensagem, string fila);
    }
}
