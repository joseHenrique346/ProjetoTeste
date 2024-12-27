using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ProjetoTeste.Infrastructure.Interface.UnitOfWork;

namespace ProjetoTeste.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BaseController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public BaseController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public override async void OnActionExecuted(ActionExecutedContext context)
        {
            await _unitOfWork.CommitAsync();
            base.OnActionExecuted(context);
        }
    }
}
