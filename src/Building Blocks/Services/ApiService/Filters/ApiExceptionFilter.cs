using Microsoft.AspNetCore.Mvc.Filters;

namespace Api.Service.Filters;

public class ApiExceptionFilter : ExceptionFilterAttribute
{
    public override void OnException(ExceptionContext context)
    {
        context.HttpContext.Response.StatusCode = context.HttpContext.GetType().Name switch
        {

        };

        base.OnException(context);
    }
}
