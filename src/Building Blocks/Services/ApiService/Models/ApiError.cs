using System.Net;

namespace Api.Service.Models;

public class ApiError
{

    public HttpStatusCode? StatusCode { get; private set; }
    public string Descricao { get; set; }

    public ApiError(HttpStatusCode? statusCode, string descricao)
    {
        StatusCode = statusCode;
        Descricao = descricao;
    }

}
