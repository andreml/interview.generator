using interview.generator.domain.Entidade.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace interview.generator.crosscutting.InjecaoDependencia
{
    public class ValidateModelStateAttributeCollectionExtension : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var errors = context.ModelState.Values.Where(v => v.Errors.Count > 0)
                        .SelectMany(v => v.Errors)
                        .Select(v => v.ErrorMessage)
                        .ToList();

                string message = string.Empty;

                foreach (var item in errors) message += " " + item;

                var responseObj = new ResponseErro()
                {
                    Codigo = (int)HttpStatusCode.BadRequest,
                    Mensagem = message,
                    Excecao = "Ocorreu um erro"
                };

                context.Result = new JsonResult(responseObj) { StatusCode = (int)HttpStatusCode.BadRequest };
            }
        }
    }
}
