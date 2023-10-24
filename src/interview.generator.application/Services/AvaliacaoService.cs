using interview.generator.application.Dto;
using interview.generator.application.Interfaces;
using interview.generator.domain.Entidade;
using interview.generator.domain.Entidade.Common;
using interview.generator.domain.Repositorio;
using System.Net;

namespace interview.generator.application.Services
{
    public class AvaliacaoService : IAvaliacaoService
    {
        private readonly IAvaliacaoRepositorio _;
        public AvaliacaoService(IAvaliacaoRepositorio repositorio) { _ = repositorio; }
        public async Task<ResponseBase> AdicionarAvaliacao(AdicionarAvaliacaoDto entity)
        {
            var response = new ResponseBase();

            try
            {
                await _.Adicionar(new Avaliacao()
                {
                    Id = Guid.NewGuid(),
                    CandidatoId = entity.CandidatoId,
                    QuestionarioId = entity.QuestionarioId,
                    Respostas = entity.Respostas,
                    DataAplicacao = entity.DataAplicacao
                });
                response.SetStatusCode(HttpStatusCode.OK);
            }
            catch (Exception e)
            {
                response.SetStatusCode(HttpStatusCode.BadRequest);
                response.AddErro(e.Message);
            }

            return response;
        }

        public async Task<ResponseBase> AdicionarObservacaoAvaliacao(AdicionarObservacaoAvaliadorDto obj)
        {
            var response = new ResponseBase();
            try
            {
                await _.AdicionarObservacaoAvaliacao(obj.CandidatoId, obj.QuestionarioId, obj.ObservacaoAvaliador);
                response.SetStatusCode(HttpStatusCode.Created);
            }
            catch (Exception e)
            {
                response.SetStatusCode(HttpStatusCode.BadRequest);
                response.AddErro(e.Message);
            }

            return response;
        }

        public async Task<ResponseBase<Avaliacao>> ObterAvaliacaoPorFiltro(Guid? CandidatoId, Guid? QuestionarioId)
        {
            var response = new ResponseBase<Avaliacao>();
            try
            {
                var resposta = await _.ObterAvaliacaoPorFiltro(CandidatoId, QuestionarioId);

                if (resposta is null)
                {
                    response.SetStatusCode(HttpStatusCode.BadRequest);
                    response.AddErro("Avaliação não existe");
                    return response;
                }

                response.AddData(resposta, HttpStatusCode.OK);
            }
            catch (Exception e)
            {
                response.SetStatusCode(HttpStatusCode.BadRequest);
                response.AddErro(e.Message);
            }

            return response;
        }
    }
}
