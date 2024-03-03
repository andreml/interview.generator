using InterviewGenerator.Application.Dto;
using InterviewGenerator.Domain.Entidade.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewGenerator.Application.Interfaces
{
    public interface IMassTransitService
    {
        Task<ResponseBase> InserirMensagem(object modelMensagem, string fila);
        Task<ResponseBase> InserirEmFilaDeErro();

    }
}
