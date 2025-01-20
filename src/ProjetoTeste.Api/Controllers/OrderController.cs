using Microsoft.AspNetCore.Mvc;
using ProjetoTeste.Arguments.Arguments.Order;
using ProjetoTeste.Arguments.Arguments.ProductOrder;
using ProjetoTeste.Arguments.Arguments.Response;
using ProjetoTeste.Infrastructure.Interface.Service;
using ProjetoTeste.Infrastructure.Interface.UnitOfWork;
using ProjetoTeste.Infrastructure.Persistence.Entities;

namespace ProjetoTeste.Api.Controllers
{
    public class OrderController : BaseController
    {
        #region Dependency Injection

        private readonly IOrderService _orderService;

        public OrderController(IUnitOfWork unitOfWork, IOrderService orderService) : base(unitOfWork)
        {
            _orderService = orderService;
        }

        #endregion

        #region Get

        [HttpGet("{id}")]
        public async Task<ActionResult> GetWithIncludesAsync(long id)
        {
            return Ok(await _orderService.Get(id));
        }

        [HttpGet]
        public async Task<ActionResult> GetWithIncludesAsync()
        {
            return Ok(await _orderService.GetAll());
        }

        #endregion

        #region Post

        [HttpPost]
        public async Task<ActionResult<BaseResponse<OutputOrder>>> Create(InputCreateOrder input)
        {
            var result = await _orderService.Create(input);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            return Ok(result.Content);
        }

        #endregion

        #region Post Product

        [HttpPost("Product")]
        public async Task<ActionResult<BaseResponse<ProductOrder>>> Create(InputCreateProductOrder input)
        {
            var result = await _orderService.CreateProductOrder(input);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            return Ok(result.Content);
        }

        #endregion

        #region Relatórios

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
            return Ok(await _orderService.GetLeastOrderedProduct());
        }

        [HttpGet("MostOrderedBrand")]
        public async Task<ActionResult> GetBrandMostOrdered()
        {
            return Ok(await _orderService.GetMostOrderedBrand());
        }

        [HttpGet("MostOrdersFromACustomer")]
        public async Task<ActionResult> GetMostOrdersCustomer()
        {
            return Ok(await _orderService.GetMostOrdersCustomer());
        }

        [HttpGet("MostValueOrderFromACustomer")]
        public async Task<ActionResult> GetMostValueOrderCustomer()
        {
            return Ok(await _orderService.GetMostValueOrderClient());
        }
        #endregion
    }
}