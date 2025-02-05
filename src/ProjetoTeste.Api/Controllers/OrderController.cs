using Microsoft.AspNetCore.Mvc;
using ProjetoTeste.Arguments.Arguments.Base.Inputs;
using ProjetoTeste.Arguments.Arguments.Order;
using ProjetoTeste.Arguments.Arguments.ProductOrder;
using ProjetoTeste.Arguments.Arguments.Response;
using ProjetoTeste.Infrastructure.Interface.Service;
using ProjetoTeste.Infrastructure.Interface.UnitOfWork;
using ProjetoTeste.Infrastructure.Persistence.Entities;

namespace ProjetoTeste.Api.Controllers
{
    public class OrderController : BaseController<IOrderService, Order, InputIdentityViewOrder, InputCreateOrder, BaseInputIdentityUpdate_0, BaseInputIdentityDelete_0, OutputOrder>
    {
        #region Dependency Injection

        private readonly IOrderService _orderService;

        public OrderController(IUnitOfWork unitOfWork, IOrderService orderService) : base(unitOfWork, orderService)
        {
            _orderService = orderService;
        }

        #endregion

        //#region Get

        //[HttpPost("GetBySingleId")]
        //public override async Task<ActionResult<OutputOrder>> GetId(InputIdentityViewOrder inputIdentityViewOrder)
        //{
        //    return Ok(await _orderService.GetSingle(inputIdentityViewOrder));
        //}

        //[HttpPost("GetByListId")]
        //public override async Task<ActionResult<OutputOrder>> GetId(List<InputIdentityViewOrder> listInputIdentityViewOrder)
        //{
        //    return Ok(await _orderService.Get(listInputIdentityViewOrder));
        //}

        //[HttpGet]
        //public override async Task<ActionResult<List<OutputOrder>>> GetAll()
        //{
        //    return Ok(await _orderService.GetAll());
        //}

        //#endregion

        //#region Post

        //[HttpPost("Create/Single")]
        //public override async Task<ActionResult<BaseResponse<OutputOrder>>> CreateSingle(InputCreateOrder inputCreateOrder)
        //{
        //    var result = await _orderService.CreateSingle(inputCreateOrder);
        //    if (!result.Success)
        //        return BadRequest(result);

        //    return Ok(result);
        //}

        //[HttpPost("Create/Multiple")]
        //public override async Task<ActionResult<BaseResponse<OutputOrder>>> Create(List<InputCreateOrder> listInputCreateOrder)
        //{
        //    var result = await _orderService.Create(listInputCreateOrder);
        //    if (!result.Success)
        //        return BadRequest(result);

        //    return Ok(result);
        //}

        //#endregion

        #region Post Product

        [HttpPost("Product/Create/Single")]
        public async Task<ActionResult<BaseResponse<ProductOrder>>> CreateProductSingle(InputCreateProductOrder inputCreateProductOrder)
        {
            var result = await _orderService.CreateProductOrderSingle(inputCreateProductOrder);
            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPost("Product/Create/Multiple")]
        public async Task<ActionResult<BaseResponse<ProductOrder>>> CreateProduct(List<InputCreateProductOrder> ListInputCreateProductOrder)
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

        #endregion

        #region Put

        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpPut("Single")]
        public override async Task<ActionResult<BaseResponse<OutputOrder>>> UpdateSingle(BaseInputIdentityUpdate_0 inputIdentityUpdate)
        {
            throw new NotImplementedException();
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpPut("Multiple")]
        public override async Task<ActionResult<BaseResponse<List<OutputOrder>>>> Update(List<BaseInputIdentityUpdate_0> listInputIdentityUpdate)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Delete

        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpDelete("Single")]
        public override async Task<ActionResult<BaseResponse<string>>> DeleteSingle(BaseInputIdentityDelete_0 inputIdentityDelete)
        {
            throw new NotImplementedException();
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpDelete("Multiple")]
        public override async Task<ActionResult<BaseResponse<string>>> Delete(List<BaseInputIdentityDelete_0> listInputIdentityDelete)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}