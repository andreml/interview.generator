using interview.generator.domain.Entidade.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System.Net;

namespace interview.generator.api.Controllers
{
    [ApiController]
    public abstract class BaseController : ControllerBase
    {
        protected new IActionResult Response<T>(ResponseBase<T> result)
        {
            if (result.HasError)
            {
                var status = result.StatusCode == 0 ? (int)HttpStatusCode.BadRequest : result.StatusCode;

                return StatusCode((int)status, result.erros);
            }

            switch (HttpContext.Request.Method)
            {
                case "GET":
                    return ResponseGet(result);
                case "POST":
                    return ResponsePost(result);
                case "PUT":
                case "DELETE":
                    return ResponsePutAndDelete(result);
                default:
                    return StatusCode(result.StatusCode, result.Data);
            }
        }

        protected IActionResult ResponseErro(string exception, string mensagem)
        {
            var mensagens = new List<string>();
            mensagens.Add(mensagem);

            return StatusCode((int)HttpStatusCode.BadRequest, new ResponseErro()
            {
                Codigo = (int)HttpStatusCode.BadRequest,
                Mensagem = mensagens,
                Excecao = exception
            });
        }
        protected IActionResult ResponseErro(int statusCode, List<string> mensagem)
        {
            if (statusCode == 0) statusCode = (int)HttpStatusCode.BadRequest;
            
            return StatusCode(statusCode, new ResponseErro()
            {
                Codigo = statusCode,
                Mensagem = mensagem
            });
        }

        protected new virtual IActionResult Response(ResponseBase result)
        {
            return Response((ResponseBase<object>)result);
        }

        private IActionResult ResponseGet<T>(ResponseBase<T> result)
        {
            if (result.Data == null)
                return NoContent();

            return Ok(result.Data);
        }

        private IActionResult ResponsePost<T>(ResponseBase<T> result)
        {
            var status = result.StatusCode == 0 ? HttpStatusCode.Created : ((HttpStatusCode)result.StatusCode);

            return StatusCode((int)status, result.Data);
        }

        private IActionResult ResponsePutAndDelete<T>(ResponseBase<T> result)
        {
            var status = result.StatusCode == 0 ? HttpStatusCode.OK : ((HttpStatusCode)result.StatusCode);

            return StatusCode((int)status, result.Data);
        }

        protected Guid ObterUsuarioIdLogado() =>
            Guid.Parse(User.Claims.FirstOrDefault(x => x.Type == "Id")!.Value);
    }
}
