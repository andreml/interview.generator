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
                    //TODO ANDRE
                    CandidatoId = entity.CandidatoId,
                    QuestionarioId = entity.QuestionarioId,
                    Respostas = entity.Respostas,
                    DataAplicacao = entity.DataAplicacao,
                    ObservacaoAplicador = entity.ObservacaoAplicador
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
        public async Task<ResponseBase> AlterarAvaliacao(AlterarAvaliacaoDto entity)
        {
            var response = new ResponseBase();

            try
            {
                await _.Alterar(new Avaliacao()
                {
                    //TODO ANDRE
                    CandidatoId = entity.CandidatoId,
                    QuestionarioId = entity.QuestionarioId,
                    Respostas = entity.Respostas,
                    DataAplicacao = entity.DataAplicacao,
                    ObservacaoAplicador = entity.ObservacaoAplicador
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
        public async Task<ResponseBase<Avaliacao>> ObterAvaliacaoPorFiltro(Guid? CandidatoId, Guid? QuestionarioId, DateTime? DataAplicacao)
        {
            var response = new ResponseBase<Avaliacao>();
            var resposta = await _.ObterAvaliacaoPorFiltro(CandidatoId, QuestionarioId, DataAplicacao);

            if (resposta is null)
            {
                response.SetStatusCode(HttpStatusCode.BadRequest);
                response.AddErro("Avaliação não existe");
                return response;
            }

            response.AddData(resposta, HttpStatusCode.OK);
            return response;
        }
        public async Task<ResponseBase<IEnumerable<Avaliacao>>> ObterTodos()
        {
            var response = new ResponseBase<IEnumerable<Avaliacao>>();
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
