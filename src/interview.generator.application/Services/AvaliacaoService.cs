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
        private readonly IAvaliacaoRepositorio _repositorio;
        public AvaliacaoService(IAvaliacaoRepositorio repositorio) { _repositorio = repositorio; }
        public async Task<ResponseBase> AdicionarAvaliacao(AdicionarAvaliacaoDto entity)
        {
            var response = new ResponseBase();

            //TODO: Get de questionario p/ validar e adicionar ao obj

            await _repositorio.Adicionar(new Avaliacao()
            {
                Id = Guid.NewGuid(),
                CandidatoId = entity.CandidatoId,
                //QuestionarioId = entity.QuestionarioId, <- implementar apos o TODO
                Respostas = entity.Respostas,
                DataAplicacao = entity.DataAplicacao
            });
            response.SetStatusCode(HttpStatusCode.OK);

            return response;
        }

        public async Task<ResponseBase> AdicionarObservacaoAvaliacao(AdicionarObservacaoAvaliadorDto obj)
        {
            var response = new ResponseBase();
   
            var avaliacao = await _repositorio.ObterAvaliacaoPorIdEUsuarioCriacaoQuestionario(obj.QuestionarioId, obj.UsuarioIdCriacaoQuestionario);

            if (avaliacao == null)
            {
                response.AddErro("Avaliação não existe");
                return response;
            }

            avaliacao.AdicionarObservacao(obj.ObservacaoAvaliador);

            await _repositorio.Alterar(avaliacao);

            return response;
        }

        public async Task<ResponseBase<Avaliacao>> ObterAvaliacaoPorFiltro(Guid usuarioIdCriacaoQuestionario, Guid? candidatoId, Guid? questionarioId)
        {
            var response = new ResponseBase<Avaliacao>();

            var resposta = await _repositorio.ObterAvaliacaoPorFiltro(usuarioIdCriacaoQuestionario, candidatoId, questionarioId);

            if (resposta is null)
            {
                response.AddErro("Avaliação não existe");
                return response;
            }

            response.AddData(resposta);

            return response;
        }
    }
}
