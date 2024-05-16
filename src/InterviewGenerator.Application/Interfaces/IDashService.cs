using InterviewGenerator.Application.ViewModels;
using InterviewGenerator.Domain.Entidade.Common;

namespace InterviewGenerator.Application.Interfaces;

public interface IDashService
{
    Task<ResponseBase<DashViewModel>> ObterDadosDashAsync(Guid usuarioAvaliadorId);
}
