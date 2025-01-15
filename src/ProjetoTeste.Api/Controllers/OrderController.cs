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
        public async Task<ActionResult> GetWithIncludesAsync()
        {
            return Ok(await _orderService.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetWithIncludesAsync(long id)
        {
            return Ok(await _orderService.Get(id));
        }

        [HttpPost]
        public async Task<ActionResult<Response<OutputOrder>>> Create(InputCreateOrder input)
        {
            var result = await _orderService.Create(input);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            return Ok(result);
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

        [HttpGet("AveragePriceOrder")]
        public async Task<ActionResult> GetOrderAveragePrice()
        {
            return Ok(await _orderService.GetOrderAveragePrice());
        }

        [HttpGet("MostOrderedProduct")]
        public async Task<ActionResult> GetProductMostOrdered()
        {
            return Ok(await _orderService.GetMostOrderedProduct());
        }

        [HttpGet("LeastOrderedProduct")]
        public async Task<ActionResult> GetProductLeastOrdered()
        {
            return Ok(await _orderService.LeastOrderedProduct());
        }

        [HttpGet("MostOrderedBrand")]
        public async Task<ActionResult> GetBrandMostOrdered()
        {
            return Ok(await _orderService.GetMostOrderedBrand());
        }
    }
}