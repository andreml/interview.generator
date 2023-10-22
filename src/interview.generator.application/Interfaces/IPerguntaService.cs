using interview.generator.application.Dto;
using interview.generator.domain.Entidade.Common;

namespace interview.generator.application.Interfaces
{
    public interface IPerguntaService
    { 
        Task<ResponseBase> CadastrarPergunta(AdicionarPerguntaDto pergunta, Guid usuarioId);
    }
}