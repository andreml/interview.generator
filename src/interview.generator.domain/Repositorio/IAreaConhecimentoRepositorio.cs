﻿using interview.generator.domain.Entidade;

namespace interview.generator.domain.Repositorio
{
    public interface IAreaConhecimentoRepositorio : ICommonRepository<AreaConhecimento>
    {
        Task<AreaConhecimento?> ObterPorIdComPerguntas(Guid id);
        Task<AreaConhecimento?> ObterPorDescricaoEUsuarioId(string descricao, Guid usuarioId);
        Task<AreaConhecimento?> ObterPorDescricao(string descricao);
        Task<IEnumerable<AreaConhecimento>> ObterAreaConhecimentoComPerguntas(Guid usuarioId, Guid areaConhecimentoId, string? descricao);
    }
}