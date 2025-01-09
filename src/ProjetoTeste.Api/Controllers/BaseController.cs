using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ProjetoTeste.Infrastructure.Interface.UnitOfWork;

namespace ProjetoTeste.Api.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class BaseController(IUnitOfWork unitOfWork) : Controller
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        unitOfWork.BeginTransaction();
        base.OnActionExecuting(context);
    }

    public override void OnActionExecuted(ActionExecutedContext context)
    {
        unitOfWork.Commit();
        base.OnActionExecuted(context);
    }
}