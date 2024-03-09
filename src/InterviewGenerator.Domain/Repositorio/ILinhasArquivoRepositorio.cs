using InterviewGenerator.Domain.Entidade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewGenerator.Domain.Repositorio
{
    public interface ILinhasArquivoRepositorio : ICommonRepository<LinhaArquivo>
    {
        Task<LinhaArquivo> ObterLinhaArquivo(Guid idArquivo, int idLinha);
    }
}
