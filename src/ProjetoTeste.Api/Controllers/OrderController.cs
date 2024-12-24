using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ProjetoTeste.Infrastructure.Interface.UnitOfWork;
using ProjetoTeste.Infrastructure.Service;


namespace ProjetoTeste.Api.Controllers
{
    public class OrderController : Controller
    {
        private readonly IUnitOfWork _uof;
        private readonly OrderService _orderService;

        public OrderController(IUnitOfWork uof, OrderService orderService)
        {
            _uof = uof;
            _orderService = orderService;
        }

        [HttpGet("Busca de todos os Pedidos")]
        public async Task<ActionResult> GetAllAsync()
        {
            var AllOrder = await _orderService.GetAllOrderAsync();
            return Ok(AllOrder);
        }

        public override async void OnActionExecuted(ActionExecutedContext context)
        {
            await _uof.CommitAsync();
            base.OnActionExecuted(context);
        }
    }
}
