using Microsoft.AspNetCore.Mvc;
using ProjetoTeste.Arguments.Arguments.Order;
using ProjetoTeste.Arguments.Arguments.ProductOrder;
using ProjetoTeste.Arguments.Arguments.Response;
using ProjetoTeste.Infrastructure.Conversor;
using ProjetoTeste.Infrastructure.Interface.UnitOfWork;
using ProjetoTeste.Infrastructure.Persistence.Entities;
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

        [HttpGet]
        public async Task<ActionResult> GetAllAsync()
        {
            return Ok(await _orderService.GetAll());
        }

        [HttpPost]
        public async Task<ActionResult<Response<Order>>> Create(InputCreateOrder input)
        {
            var result = await _orderService.Create(input);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            return Ok(result.Request.ToOutputOrder());
        }

        [HttpPost("Product")]
        public async Task<ActionResult<Response<ProductOrder>>> Create(InputCreateProductOrder input)
        {
            var result = await _orderService.CreateProductOrder(input);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            return Ok(result.Request);
        }
    }
}