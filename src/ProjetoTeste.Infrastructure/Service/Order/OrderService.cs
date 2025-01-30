using ProjetoTeste.Arguments.Arguments.Order;
using ProjetoTeste.Arguments.Arguments.Order.Reports.Outputs;
using ProjetoTeste.Arguments.Arguments.ProductOrder;
using ProjetoTeste.Arguments.Arguments.Response;
using ProjetoTeste.Infrastructure.Conversor;
using ProjetoTeste.Infrastructure.Interface.Repository;
using ProjetoTeste.Infrastructure.Interface.Service;
using ProjetoTeste.Infrastructure.Persistence.Entities;

namespace ProjetoTeste.Infrastructure.Service
{
    public class OrderService : IOrderService
    {
        #region Dependency Injection

        private readonly IOrderRepository _orderRepository;
        private readonly IProductOrderRepository _productOrderRepository;
        private readonly IProductRepository _productRepository;
        private readonly IOrderValidateService _orderValidateService;
        private readonly IBrandRepository _brandRepository;
        private readonly ICustomerRepository _customerRepository;
        public OrderService(IOrderRepository orderRepository, IProductRepository productRepository, IProductOrderRepository productOrderRepository, IOrderValidateService orderValidateService, IBrandRepository brandRepository, ICustomerRepository customerRepository)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _productOrderRepository = productOrderRepository;
            _orderValidateService = orderValidateService;
            _brandRepository = brandRepository;
            _customerRepository = customerRepository;
        }

        #endregion

        #region Get

        public async Task<List<OutputOrder>> GetAll()
        {
            var get = await _orderRepository.GetWithIncludesAsync();
            return get.Select(i => i.ToOutputOrder()).ToList();
        }
        public async Task<List<OutputOrder>?> Get(List<InputIdentityViewOrder> InputIdentityViewOrder)
        {
            var orderId = await _orderRepository.GetWithIncludesAsync(InputIdentityViewOrder.Select(i => i.Id).ToList());
            return orderId.Select(i => i.ToOutputOrder()).ToList();
        }
        #endregion

        #region Relatório

        public async Task<List<OutputMaxSaleValueProduct>> GetMostOrderedProduct()
        {
            var mostOrderedProduct = await _orderRepository.GetMostOrderedProduct();

            return mostOrderedProduct.Select(product => new OutputMaxSaleValueProduct
                (
                    product.ProductId,
                    product.ProductName,
                    product.ProductCode,
                    product.ProductDescription,
                    product.TotalSaleValue,
                    product.ProductBrandId,
                    product.TotalSaleQuantity
                )).ToList();
        }

        public async Task<OutputMaxSaleValueBrand> GetMostOrderedBrand()
        {
            var mostOrderedBrand = await _orderRepository.GetMostOrderedBrand();

            var brandName = _brandRepository.GetBrandNameById(mostOrderedBrand.BrandId);

            return new OutputMaxSaleValueBrand(
                brandName.ToString(),
                mostOrderedBrand.TotalSaleValue,
                mostOrderedBrand.BrandId,
                mostOrderedBrand.TotalSaleQuantity
            );
        }

        public async Task<OutputAverageSaleValueOrder> GetOrderAveragePrice()
        {
            var orderAveragePrice = await _orderRepository.GetOrderAveragePrice();

            return new OutputAverageSaleValueOrder
                (
                    orderAveragePrice.OrderId,
                    orderAveragePrice.TotalSaleQuantity,
                    orderAveragePrice.Total,
                    orderAveragePrice.AverageTotalSaleValue
                );
        }

        public async Task<List<OutputMinSaleValueProduct>> GetLeastOrderedProduct()
        {
            var leastOrderedProducts = await _orderRepository.GetLeastOrderedProduct();

            return leastOrderedProducts.Select(product => new OutputMinSaleValueProduct(
                product.ProductId,
                product.ProductName,
                product.ProductCode,
                product.ProductDescription,
                product.TotalSaleValue,
                product.ProductBrandId,
                product.TotalSaleQuantity
            )).ToList();
        }


        public async Task<OutputMostOrderQuantityCustomer> GetMostOrdersCustomer()
        {
            var mostOrdersClient = await _orderRepository.GetMostOrdersCustomer();

            var customerName = await _customerRepository.GetById(mostOrdersClient.CustomerId);

            return new OutputMostOrderQuantityCustomer(
                mostOrdersClient.CustomerId,
                customerName.Name.ToString(),
                mostOrdersClient.TotalSaleValue,
                mostOrdersClient.TotalSaleQuantity
            );
        }

        public async Task<OutputMostValueOrderCustomer> GetMostValueOrderClient()
        {
            var mostValueOrderClient = await _orderRepository.GetMostValueOrderCustomer();

            var customerName = await _customerRepository.GetById(mostValueOrderClient.CustomerId);

            return new OutputMostValueOrderCustomer(
                mostValueOrderClient.CustomerId,
                customerName.Name.ToString(),
                mostValueOrderClient.TotalSaleValue,
                mostValueOrderClient.TotalSaleQuantity
            );
        }

        #endregion

        #region Create

        public async Task<BaseResponse<List<OutputOrder>>> Create(List<InputCreateOrder> listInputCreateOrder)
        {
            var response = new BaseResponse<List<OutputOrder>>();

            var existingCustomer = await _customerRepository.GetListByListIdWhere(listInputCreateOrder.Select(i => i.CustomerId).ToList());
            var selectedExistingCustomer = existingCustomer.Select(i => i.Id);

            var listInputCreate = (from i in listInputCreateOrder
                                   select new
                                   {
                                       InputCreate = i,
                                       ExistingCustomer = selectedExistingCustomer.FirstOrDefault(j => i.CustomerId == j)
                                   }).ToList();

            var listInputValidateCreateOrder = listInputCreate.Select(i => new OrderValidate().CreateOrder(i.InputCreate, i.ExistingCustomer)).ToList();

            var result = await _orderValidateService.ValidateCreateOrder(listInputValidateCreateOrder);

            response.Success = result.Success;
            response.Message = result.Message;

            if (!response.Success)
                return response;

            var order = (from i in result.Content
                         select new Order(i.InputCreateOrder.CustomerId, DateTime.Now)).ToList();

            await _orderRepository.CreateAsync(order);
            response.Content = order.ToListOutputOrder();
            return response;
        }

        public async Task<BaseResponse<List<OutputProductOrder>>> CreateProductOrder(List<InputCreateProductOrder> listInputCreateProductOrder)
        {
            var response = new BaseResponse<List<OutputProductOrder>>();

            var existingOrder = await _orderRepository.GetListByListIdWhere(listInputCreateProductOrder.Select(i => i.OrderId).ToList());
            var selectedExistingOrder = existingOrder.Select(i => i.Id);

            var existingProduct = await _productRepository.GetListByListIdWhere(listInputCreateProductOrder.Select(i => i.ProductId).ToList());

            var listInputCreate = (from i in listInputCreateProductOrder
                                   select new
                                   {
                                       InputCreate = i,
                                       ExistingOrder = selectedExistingOrder.FirstOrDefault(j => i.OrderId == j),
                                       ExistingProduct = existingProduct.FirstOrDefault(j => i.ProductId == j.Id).ToProductDto()
                                   }).ToList();

            var listValidateCreateProductOrder = listInputCreate.Select(i => new ProductOrderValidate().CreateProductOrder(i.InputCreate, i.ExistingOrder, i.ExistingProduct)).ToList();

            var result = await _orderValidateService.ValidateCreateProductOrder(listValidateCreateProductOrder);

            response.Success = result.Success;
            response.Message = result.Message;

            if (!response.Success)
                return response;

            existingProduct = (from i in existingProduct
                               from j in result.Content
                               where i.Id == j.ExistingProduct.Id
                               let updateStock = i.Stock = j.ExistingProduct.Stock
                               select i).ToList();

            var createProductOrder = (from i in result.Content
                                      select new ProductOrder
                                      {
                                          OrderId = i.InputCreateProductOrder.OrderId,
                                          ProductId = i.InputCreateProductOrder.ProductId,
                                          Quantity = i.InputCreateProductOrder.Quantity,
                                          UnitPrice = i.ExistingProduct.Price,
                                          SubTotal = i.InputCreateProductOrder.Quantity * i.ExistingProduct.Price
                                      }).ToList();

            await _productRepository.Update(existingProduct);

            await _productOrderRepository.CreateAsync(createProductOrder);

            response.Content = createProductOrder.ToListOuputProductOrder();
            return response;
        }
    }
}
#endregion