using interview.generator.application.Dto;
using interview.generator.application.ViewModels;
using interview.generator.domain.Entidade.Common;

namespace interview.generator.application.Interfaces
{
    public interface IPerguntaService
    { 
        Task<ResponseBase> CadastrarPergunta(AdicionarPerguntaDto pergunta, Guid usuarioId);
        ResponseBase<IEnumerable<PerguntaViewModel>> ListarPerguntas(Guid userId, Guid perguntaId, string? areaConhecimento, string? descricao);
    }
}