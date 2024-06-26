﻿using InterviewGenerator.Domain.Entidade.Common;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace InterviewGenerator.Api.Controllers;

[ApiController]
public abstract class BaseController : ControllerBase
{
    protected new IActionResult Response<T>(ResponseBase<T> result)
    {
        if (result.HasError)
            return ResponseErro((int)HttpStatusCode.BadRequest, result.GetErrors());

        return HttpContext.Request.Method switch
        {
            "GET" => ResponseGet(result),
            "POST" => ResponsePost(result),
            "PUT" or "DELETE" => ResponsePutAndDelete(result),
            _ => StatusCode(result.StatusCode, result.Data),
        };
    }

    protected IActionResult ResponseErro(string exception, string mensagem)
    {
        return StatusCode((int)HttpStatusCode.BadRequest, new ResponseErro()
        {
            Codigo = (int)HttpStatusCode.BadRequest,
            Mensagens = new List<string> { mensagem },
            Excecao = exception
        });
    }
    protected IActionResult ResponseErro(int statusCode, List<string> mensagens)
    {
        if (statusCode == 0) statusCode = (int)HttpStatusCode.BadRequest;
        
        return StatusCode(statusCode, new ResponseErro()
        {
            Codigo = statusCode,
            Mensagens = mensagens
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
