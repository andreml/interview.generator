using InterviewGenerator.Domain.Entidade.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace InterviewGenerator.CrossCutting.InjecaoDependencia;

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

            var responseErro = new ResponseErro()
            {
                Codigo = 400,
                Mensagens = errors
            };

            context.Result = new JsonResult(responseErro) { StatusCode = (int)HttpStatusCode.BadRequest };
        }
    }
}
