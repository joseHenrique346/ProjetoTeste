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

        [HttpPost("GetBySingleId")]
        public async Task<ActionResult> GetWithIncludesAsync(InputIdentityViewOrder inputIdentityViewOrder)
        {
            return Ok(await _orderService.GetSingle(inputIdentityViewOrder));
        }

        [HttpPost("GetByListId")]
        public async Task<ActionResult> GetWithIncludesAsync(List<InputIdentityViewOrder> listInputIdentityViewOrder)
        {
            return Ok(await _orderService.Get(listInputIdentityViewOrder));
        }

        [HttpGet]
        public async Task<ActionResult> GetWithIncludesAsync()
        {
            return Ok(await _orderService.GetAll());
        }

        #endregion

        #region Post

        [HttpPost("Single")]
        public async Task<ActionResult<BaseResponse<OutputOrder>>> CreateSingle(InputCreateOrder inputCreateOrder)
        {
            var result = await _orderService.CreateSingle(inputCreateOrder);
            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPost("Multiple")]
        public async Task<ActionResult<BaseResponse<OutputOrder>>> Create(List<InputCreateOrder> listInputCreateOrder)
        {
            var result = await _orderService.Create(listInputCreateOrder);
            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        #endregion

        #region Post Product

        [HttpPost("ProductSingle")]
        public async Task<ActionResult<BaseResponse<ProductOrder>>> CreateSingle(InputCreateProductOrder inputCreateProductOrder)
        {
            var result = await _orderService.CreateProductOrderSingle(inputCreateProductOrder);
            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPost("ProductMultiple")]
        public async Task<ActionResult<BaseResponse<ProductOrder>>> Create(List<InputCreateProductOrder> ListInputCreateProductOrder)
        {
            var result = await _orderService.CreateProductOrder(ListInputCreateProductOrder);
            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
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
    }
}
#endregion