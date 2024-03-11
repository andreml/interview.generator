using InterviewGenerator.Application.Dto;
using InterviewGenerator.Application.ViewModels;
using InterviewGenerator.Domain.Entidade.Common;

namespace InterviewGenerator.Application.Interfaces
{
    public interface IPerguntaService
    {
        Task<ResponseBase> CadastrarPergunta(Guid usuarioId, AdicionarPerguntaDto pergunta);
        ResponseBase<IEnumerable<PerguntaViewModel>> ListarPerguntas(Guid usuarioCriacaoId, Guid perguntaId, string? areaConhecimento, string? descricao);
        Task<ResponseBase> AlterarPergunta(AlterarPerguntaDto pergunta);
        Task<ResponseBase> ExcluirPergunta(Guid usuarioCriacaoId, Guid perguntaId);
    }
}