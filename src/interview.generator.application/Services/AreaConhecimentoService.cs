using interview.generator.application.Dto;
using interview.generator.application.Interfaces;
using interview.generator.application.ViewModels;
using interview.generator.domain.Entidade;
using interview.generator.domain.Entidade.Common;
using interview.generator.domain.Repositorio;

namespace interview.generator.application.Services
{
    public class AreaConhecimentoService : IAreaConhecimentoService
    {
        private readonly IAreaConhecimentoRepositorio _areaConhecimentoRepositorio;

        public AreaConhecimentoService(IAreaConhecimentoRepositorio areaConhecimentoRepositorio)
        {
            _areaConhecimentoRepositorio = areaConhecimentoRepositorio;
        }

        public async Task<ResponseBase> AlterarAreaConhecimento(AlterarAreaConhecimentoDto areaConhecimentoDto)
        {
            var response = new ResponseBase();

            var areaConhecimento = await _areaConhecimentoRepositorio.ObterPorIdEUsuarioId(areaConhecimentoDto.UsuarioId, areaConhecimentoDto.Id);
            if (areaConhecimento == null)
            {
                response.AddErro($"Area de conhecimento não encontrada");
                return response;
            }

            var areaConhecimentoDescricao = await _areaConhecimentoRepositorio.ObterPorDescricaoEUsuarioId(areaConhecimentoDto.UsuarioId, areaConhecimentoDto.Descricao);
            if (areaConhecimentoDescricao != null && areaConhecimentoDescricao.Id != areaConhecimento.Id)
            {
                response.AddErro($"Já existe uma área do conhecimento cadastrada com essa descrição. Id: {areaConhecimentoDescricao.Id}");
                return response;
            }

            areaConhecimento.AlterarDescricao(areaConhecimentoDto.Descricao);

            await _areaConhecimentoRepositorio.Alterar(areaConhecimento);

            response.AddData("Área de Conhecimento alterada com sucesso!");

            return response;
        }

        public async Task<ResponseBase> CadastrarAreaConhecimento(AdicionarAreaConhecimentoDto areaConhecimento)
        {
            var response = new ResponseBase();

            var areaConhecimentoExistente = await _areaConhecimentoRepositorio.ObterPorDescricaoEUsuarioId(areaConhecimento.UsuarioId, areaConhecimento.Descricao);
            if (areaConhecimentoExistente != null)
            {
                response.AddErro($"Já existe uma área do conhecimento cadastrada com essa descrição. Id: {areaConhecimentoExistente.Id}");
                return response;
            }

            var novaAreaConhecimento = new AreaConhecimento { Descricao = areaConhecimento.Descricao, UsuarioCriacaoId = areaConhecimento.UsuarioId };

            await _areaConhecimentoRepositorio.Adicionar(novaAreaConhecimento);

            response.AddData("Área de Conhecimento adicionada com sucesso!");

            return response;
        }

        public async Task<ResponseBase> ExcluirAreaConhecimento(Guid usuarioCriacaoId, Guid id)
        {
            var response = new ResponseBase();

            var areaConhecimento = await _areaConhecimentoRepositorio.ObterPorIdComPerguntas(usuarioCriacaoId, id);

            if (areaConhecimento == null)
            {
                response.AddErro("Area de conhecimento não encontrada");
                return response;
            }

            if (areaConhecimento.Perguntas!.Count > 0)
            {
                response.AddErro("Area de conhecimento possui uma ou mais perguntas relacionadas");
                return response;
            }

            await _areaConhecimentoRepositorio.Excluir(areaConhecimento);

            response.AddData("Area do conhecimento excluída com sucesso!");

            return response;
        }

        public async Task<ResponseBase<IEnumerable<AreaConhecimentoViewModel>>> ListarAreasConhecimento(Guid usuarioCriacaoId, Guid areaConhecimentoId, string? descricao)
        {
            var response = new ResponseBase<IEnumerable<AreaConhecimentoViewModel>>();

            var areasConhecimento = await _areaConhecimentoRepositorio.ObterAreaConhecimentoComPerguntas(usuarioCriacaoId, areaConhecimentoId, descricao);

            if (areasConhecimento == null)
                return response;

            var areasViewModel = areasConhecimento
                                    .Select(x => new AreaConhecimentoViewModel(x.Id, x.Descricao, x.Perguntas!.Count))
                                    .ToList();

            response.AddData(areasViewModel);

            return response;
        }

        public async Task<AreaConhecimento> ObterOuCriarAreaConhecimento(Guid usuarioCriacaoId, string descricao)
        {
            var areaConhecimento = await _areaConhecimentoRepositorio.ObterPorDescricaoEUsuarioId(usuarioCriacaoId, descricao);

            if (areaConhecimento != null)
                return areaConhecimento;

            areaConhecimento = new AreaConhecimento { Descricao = descricao, UsuarioCriacaoId = usuarioCriacaoId };

            await _areaConhecimentoRepositorio.Adicionar(areaConhecimento);

            return areaConhecimento;
        }

    }
}
