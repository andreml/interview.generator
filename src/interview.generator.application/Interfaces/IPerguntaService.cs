using interview.generator.application.Dto;
using interview.generator.application.ViewModels;
using interview.generator.domain.Entidade.Common;

namespace interview.generator.application.Interfaces
{
    public interface IPerguntaService
    { 
        Task<ResponseBase> CadastrarPergunta(AdicionarPerguntaDto pergunta);
        ResponseBase<IEnumerable<PerguntaViewModel>> ListarPerguntas(Guid usuarioCriacaoId, Guid perguntaId, string? areaConhecimento, string? descricao);
        Task<ResponseBase> AlterarPergunta(AlterarPerguntaDto pergunta);
        Task<ResponseBase> ExcluirPergunta(Guid usuarioCriacaoId, Guid perguntaId);
    }
}