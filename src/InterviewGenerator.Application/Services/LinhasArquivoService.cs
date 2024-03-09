using InterviewGenerator.Application.Interfaces;
using InterviewGenerator.Domain.Entidade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewGenerator.Application.Services
{
    public class LinhasArquivoService : ILinhasArquivoService
    {
        public Task<LinhasArquivo> ObterLinhaArquivo(Guid idArquivo, int idLinha)
        {
            throw new NotImplementedException();
        }
    }
}
