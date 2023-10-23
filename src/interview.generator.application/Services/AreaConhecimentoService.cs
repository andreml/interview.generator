using interview.generator.application.Dto;
using interview.generator.application.Interfaces;
using interview.generator.domain.Entidade;
using interview.generator.domain.Entidade.Common;
using interview.generator.domain.Repositorio;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace interview.generator.application.Services
{
    public class AreaConhecimentoService : IAreaConhecimentoService
    {
        private readonly IAreaConhecimentoRepositorio _areaConhecimentoRepositorio;

        public AreaConhecimentoService(IAreaConhecimentoRepositorio areaConhecimentoRepositorio)
        {
            _areaConhecimentoRepositorio = areaConhecimentoRepositorio;
        }

        public async Task<ResponseBase> AlterarAreaConhecimento(AlterarAreaConhecimentoDto areaConhecimento, Guid usuarioId)
        {
            var response = new ResponseBase();

            var areaConhecimentoExistente = await _areaConhecimentoRepositorio.ObterPorDescricao(areaConhecimento.Descricao);
            if (areaConhecimentoExistente != null)
            {
                response.AddErro($"Já existe uma área do conhecimento cadastrada com essa descrição. Id: {areaConhecimentoExistente.Id}");
                return response;
            }

            var areaConhecimentoAlterada = new AreaConhecimento { Descricao = areaConhecimento.Descricao, UsuarioId = usuarioId, Id = areaConhecimento.Id };

            await _areaConhecimentoRepositorio.Alterar(areaConhecimentoAlterada);

            response.AddData("Área de Conhecimento alterada com sucesso!");

            return response;
        }

        public async Task<ResponseBase> CadastrarAreaConhecimento(AdicionarAreaConhecimentoDto areaConhecimento, Guid usuarioId)
        {
            var response = new ResponseBase();

            var areaConhecimentoExistente = await _areaConhecimentoRepositorio.ObterPorDescricao(areaConhecimento.Descricao);
            if (areaConhecimentoExistente != null)
            {
                response.AddErro($"Já existe uma área do conhecimento cadastrada com essa descrição. Id: {areaConhecimentoExistente.Id}");
                return response;
            }

            var novaAreaConhecimento = new AreaConhecimento { Descricao = areaConhecimento.Descricao, UsuarioId = usuarioId };

            await _areaConhecimentoRepositorio.Adicionar(novaAreaConhecimento);

            response.AddData("Área de Conhecimento adicionada com sucesso!");

            return response;
        }

        public async Task<ResponseBase> ExcluirAreaConhecimento(Guid id)
        {
            var response = new ResponseBase();

            //Adicionar validações

            await _areaConhecimentoRepositorio.Excluir(id);

            response.AddData("Area do conhecimento excluída com sucesso!");

            return response;
        }

        public async Task<ResponseBase<IEnumerable<AreaConhecimento>>> ListarAreasConhecimento()
        {
            var response = new ResponseBase<IEnumerable<AreaConhecimento>>();

            var usuarios = await _areaConhecimentoRepositorio.ObterTodos();

            response.AddData(usuarios);

            return response;
        }

        public async Task<ResponseBase<AreaConhecimento>> ObterAreaConhecimento(Guid id)
        {
            var response = new ResponseBase<AreaConhecimento>();

            var areaConhecimento = await _areaConhecimentoRepositorio.ObterPorId(id);

            response.AddData(areaConhecimento!);

            return response;
        }

        public async Task<ResponseBase<AreaConhecimento>> ObterAreaConhecimentoPorDescricao(string descricao)
        {
            var response = new ResponseBase<AreaConhecimento>();

            var areaConhecimento = await _areaConhecimentoRepositorio.ObterPorDescricao(descricao);

            response.AddData(areaConhecimento!);

            return response;
        }
    }
}
