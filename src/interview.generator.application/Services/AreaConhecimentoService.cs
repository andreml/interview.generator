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

        public async Task<ResponseBase> AlterarAreaConhecimento(AlterarAreaConhecimentoDto areaConhecimento)
        {
            var response = new ResponseBase();

            var areaConhecimentoExistente = await _areaConhecimentoRepositorio.ObterPorDescricao(areaConhecimento.Descricao);
            if (areaConhecimentoExistente != null)
            {
                response.AddErro($"Já existe uma área do conhecimento cadastrada com essa descrição. Id: {areaConhecimentoExistente.Id}");
                return response;
            }

            var areaConhecimentoAlterada = new AreaConhecimento { 
                Descricao = areaConhecimento.Descricao, 
                UsuarioId = areaConhecimento.UsuarioId, 
                Id = areaConhecimento.Id 
            };

            await _areaConhecimentoRepositorio.Alterar(areaConhecimentoAlterada);

            response.AddData("Área de Conhecimento alterada com sucesso!");

            return response;
        }

        public async Task<ResponseBase> CadastrarAreaConhecimento(AdicionarAreaConhecimentoDto areaConhecimento)
        {
            var response = new ResponseBase();

            var areaConhecimentoExistente = await _areaConhecimentoRepositorio.ObterPorDescricao(areaConhecimento.Descricao);
            if (areaConhecimentoExistente != null)
            {
                response.AddErro($"Já existe uma área do conhecimento cadastrada com essa descrição. Id: {areaConhecimentoExistente.Id}");
                return response;
            }

            var novaAreaConhecimento = new AreaConhecimento { Descricao = areaConhecimento.Descricao, UsuarioId = areaConhecimento.UsuarioId };

            await _areaConhecimentoRepositorio.Adicionar(novaAreaConhecimento);

            response.AddData("Área de Conhecimento adicionada com sucesso!");

            return response;
        }

        public async Task<ResponseBase> ExcluirAreaConhecimento(Guid id, Guid usuarioId)
        {
            var response = new ResponseBase();

            var areaConhecimento = await _areaConhecimentoRepositorio.ObterPorIdComPerguntas(id, usuarioId);

            if(areaConhecimento == null)
            {
                response.AddErro("Area de conhecimento não encontrada");
                return response;
            }

            if(areaConhecimento.Perguntas!.Count > 0)
            {
                response.AddErro("Area de conhecimento possui uma ou mais perguntas relacionadas");
                return response;
            }

            await _areaConhecimentoRepositorio.Excluir(id);

            response.AddData("Area do conhecimento excluída com sucesso!");

            return response;
        }

        public async Task<ResponseBase<IEnumerable<AreaConhecimentoViewModel>>> ListarAreasConhecimento(Guid usuarioId, Guid areaConhecimentoId, string? descricao)
        {
            var response = new ResponseBase<IEnumerable<AreaConhecimentoViewModel>>();

            var areasConhecimento = await _areaConhecimentoRepositorio.ObterAreaConhecimentoComPerguntas(usuarioId, areaConhecimentoId, descricao);

            if (areasConhecimento == null)
                return response;

            var areasViewModel = areasConhecimento
                                    .Select(x => new AreaConhecimentoViewModel(x.Id, x.Descricao, x.Perguntas!.Count))
                                    .ToList();

            response.AddData(areasViewModel);

            return response;
        }

        public async Task<AreaConhecimento> ObterOuCriarAreaConhecimento(Guid usuarioId, string descricao)
        {
            var areaConhecimento = await _areaConhecimentoRepositorio.ObterPorDescricaoEUsuarioId(descricao, usuarioId);

            if (areaConhecimento != null)
                return areaConhecimento;

            areaConhecimento = new AreaConhecimento { Descricao = descricao, UsuarioId = usuarioId };

            await _areaConhecimentoRepositorio.Adicionar(areaConhecimento);

            return areaConhecimento;
        }

    }
}
