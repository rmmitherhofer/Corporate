using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Api.Service.Controllers;

[ApiController]
[Produces("application/json")]
public abstract class MainController : ControllerBase
{
    protected IActionResult CustonResponse(object result = null)
    {
        if (result is not null)
        {
            switch (result.GetType().Name)
            {
                case nameof(ModelStateDictionary):
                    return CustonResponse((ModelStateDictionary)result);
                case nameof(ValidationResult):
                    return CustonResponse((ValidationResult)result);

            }

        }
        return Ok(result);
        //return BadRequest();
    }
}