using InterviewGenerator.Domain.Entidade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewGenerator.Application.Interfaces
{
    public interface ILinhasArquivoService
    {
        Task<LinhaArquivo> ObterLinhaArquivo(Guid idArquivo, int idLinha);
    }
}
