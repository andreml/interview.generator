﻿using interview.generator.domain.Entidade;

namespace interview.generator.domain.Repositorio
{
    public interface IUsuarioRepositorio : ICommonRepository<Usuario>
    {
        Task<Usuario?> ObterPorId(Guid id);
        Task<bool> ExisteUsuarioPorCpf(string cpf);
        Task<bool> ExisteUsuarioPorLogin(string login);
        Task<Usuario?> ObterUsuarioPorLoginESenha(string nome, string senha);

    }
}