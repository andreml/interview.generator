using interview.generator.domain.Entidade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace interview.generator.domain.Repositorio
{
    public interface IQuestionarioRepositorio
    {
        Task Adicionar(Questionario entity);
        Task<Questionario?> ObterPorId(Guid Id);
        Task<Questionario?> ObterPorNome(string nome);
        Task Alterar(Questionario entity);
        Task Excluir(Questionario entity);
    }
}
