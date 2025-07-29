using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PanPan.WebApp.Models.ViewModels;
using System.Net;
using Zypher.Extensions.Core;

namespace PanPan.WebApp.Controllers;

public class ErrorControler : Controller
{
    [AllowAnonymous]
    [Route("error/{id:length(3,3)}")]
    public IActionResult Error(int id)
    {
        ErrorViewModel modelError = new()
        {
            ErrorCode = id,
            CorrelationId = HttpContext.Request?.Cookies?[HttpRequestExtensions.CORRELATION_ID]
        };

        switch (id)
        {
            case (int)HttpStatusCode.Unauthorized: // 401
                modelError.Title = "Acesso não autorizado";
                modelError.Message = "Você não tem permissão para acessar este recurso. Faça login e tente novamente.";
                break;

            case (int)HttpStatusCode.BadRequest: // 400
                modelError.Title = "Requisição inválida";
                modelError.Message = "A requisição enviada está incorreta ou incompleta. Verifique os dados informados.";
                break;

            case (int)HttpStatusCode.NotFound: // 404
                modelError.Title = "Página não encontrada";
                modelError.Message = "A página ou recurso que você está tentando acessar não foi encontrado.";
                break;

            case (int)HttpStatusCode.Forbidden: // 403
                modelError.Title = "Acesso proibido";
                modelError.Message = "Você não tem autorização para acessar este recurso.";
                break;

            case (int)HttpStatusCode.InternalServerError: // 500
                modelError.Title = "Erro interno do servidor";
                modelError.Message = "Ocorreu um erro inesperado. Já estamos trabalhando para resolver o problema.";
                break;

            default:
                modelError.Title = "Erro inesperado";
                modelError.Message = "Algo deu errado. Por favor, tente novamente mais tarde.";
                break;

        }
        return View(nameof(Error), modelError);
    }
}