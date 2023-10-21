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

                context.Result = new JsonResult(errors) { StatusCode = (int)HttpStatusCode.BadRequest };
            }
        }
    }
}
