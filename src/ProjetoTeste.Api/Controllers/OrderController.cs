using Microsoft.AspNetCore.Mvc;
using ProjetoTeste.Arguments.Arguments.Order;
using ProjetoTeste.Infrastructure.Interface.UnitOfWork;
using ProjetoTeste.Infrastructure.Service;

namespace ProjetoTeste.Api.Controllers
{
    public class OrderController : BaseController
    {
        private readonly OrderService _orderService;

        public OrderController(IUnitOfWork unitOfWork, OrderService orderService) : base(unitOfWork)
        {
            _orderService = orderService;
        }

        //[HttpGet]
        //public async Task<ActionResult> GetAllAsync()
        //{
        //    var AllOrder = await _orderService.GetAllOrderAsync();
        //    return Ok(AllOrder);
        //}

        //[HttpPost]
        //public async Task<ActionResult<OutputPo>>> Create(InputCreateOrder input)
        //{
        //    var createdOrder = await _orderService.Create(input);
        //    if (createdOrder is null)
        //    {
        //        return new Response<>
        //    }
        //}
    }
}