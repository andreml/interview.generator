using interview.generator.application.Dto;
using interview.generator.application.Interfaces;
using interview.generator.domain.Entidade;
using interview.generator.domain.Entidade.Common;
using interview.generator.domain.Repositorio;
using System.Net;

namespace interview.generator.application.Services
{
    public class RespostaAvaliacaoService : IRespostaAvaliacaoService
    {
        private readonly IRespostaAvaliacaoRepositorio _;
        public RespostaAvaliacaoService(IRespostaAvaliacaoRepositorio repositorio) { _ = repositorio; }
        public async Task<ResponseBase> AdicionarRespostaAvaliacao(AdicionarRespostaAvaliacaoDto entity)
        {
            var response = new ResponseBase();

            try
            {
                await _.Adicionar(new RespostaAvaliacao()
                {
                    AlternativaEscolhidaId = entity.AlternativaEscolhidaId,
                    AvaliacaoId = entity.AvaliacaoId,
                    PerguntaQuestionarioId = entity.PerguntaQuestionarioId
                });
                response.SetStatusCode(HttpStatusCode.OK);
                return response;
            }
            catch (Exception e)
            {
                response.SetStatusCode(HttpStatusCode.BadRequest);
                response.AddErro(e.Message);
                return response;
            }
        }
        public async Task<ResponseBase> AlterarRespostaAvaliacao(AlterarRespostaAvaliacaoDto entity)
        {
            var response = new ResponseBase();

            try
            {
                await _.Alterar(new RespostaAvaliacao()
                {
                    AlternativaEscolhidaId = entity.AlternativaEscolhidaId,
                    AvaliacaoId = entity.AvaliacaoId,
                    PerguntaQuestionarioId = entity.PerguntaQuestionarioId
                });

                response.SetStatusCode(HttpStatusCode.OK);
                return response;
            }
            catch (Exception e)
            {
                response.SetStatusCode(HttpStatusCode.BadRequest);
                response.AddErro(e.Message);
                return response;
            }
        }
        public async Task<ResponseBase<RespostaAvaliacao>> ObterRespostaPorPergunta(Guid PerguntaId)
        {
            var response = new ResponseBase<RespostaAvaliacao>();
            var resposta = await _.ObterRespostaPorPergunta(PerguntaId);

            if (resposta is null)
            {
                response.SetStatusCode(HttpStatusCode.BadRequest);
                response.AddErro("Pergunta não existe");
                return response;
            }

            response.AddData(resposta, HttpStatusCode.OK);
            return response;
        }
        public async Task<ResponseBase<RespostaAvaliacao>> ObterRespostasPorAvaliacao(Guid AvaliacaoId)
        {
            var response = new ResponseBase<RespostaAvaliacao>();
            try
            {
                var resposta = await _.ObterPorId(AvaliacaoId);
                if (resposta is null)
                {
                    response.AddErro("Avaliação não existe");
                    return response;
                }

                response.AddData(resposta, HttpStatusCode.OK);
                return response;
            }
            catch (Exception e)
            {
                response.SetStatusCode(HttpStatusCode.BadRequest);
                response.AddErro(e.Message);
                return response;
            }
        }
        public async Task<ResponseBase<IEnumerable<RespostaAvaliacao>>> ObterTodos()
        {
            var response = new ResponseBase<IEnumerable<RespostaAvaliacao>>();
            try
            {
                var resposta = await _.ObterTodos();
                if (resposta is null)
                {
                    response.SetStatusCode(HttpStatusCode.BadRequest);
                    response.AddErro("Nenhum candidato foi avaliado");
                    return response;
                }

                response.AddData(resposta, HttpStatusCode.OK);
                return response;
            }
            catch (Exception e)
            {
                response.SetStatusCode(HttpStatusCode.BadRequest);
                response.AddErro(e.Message);
                return response;
            }
        }
    }
}
