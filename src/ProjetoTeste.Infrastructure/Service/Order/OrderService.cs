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
            var result = await _orderValidateService.ValidateCreateOrder(listInputCreateOrder);

            if (!result.Success)
            {
                return new BaseResponse<List<OutputOrder>> { Success = false, Message = result.Message };
            }

            var order = (from i in listInputCreateOrder
                         select new Order
                         (
                             i.CustomerId, DateTime.Now
                         )).ToList();

            await _orderRepository.CreateAsync(order);
            return new BaseResponse<List<OutputOrder>> { Success = true, Content = order.ToListOutputOrder() };
        }

        public async Task<BaseResponse<List<OutputProductOrder>>> CreateProductOrder(List<InputCreateProductOrder> listInputCreateProductOrder)
        {
            var result = await _orderValidateService.ValidateCreateProductOrder(listInputCreateProductOrder);
            if (!result.Success)
            {
                return new BaseResponse<List<OutputProductOrder>> { Success = false, Message = result.Message };
            }

            var currentProduct = await _productRepository.GetListByListIdWhere(listInputCreateProductOrder.Select(i => i.ProductId).ToList());

            var createProductOrder = (from i in listInputCreateProductOrder
                                      from j in currentProduct
                                      select new ProductOrder
                                      {
                                          OrderId = i.OrderId,
                                          ProductId = i.ProductId,
                                          Quantity = i.Quantity,
                                          UnitPrice = j.Price,
                                          SubTotal = i.Quantity * j.Price
                                      }).ToList();

            await _productOrderRepository.CreateAsync(createProductOrder);

            foreach (var i in listInputCreateProductOrder)
            {
                var productToUpdate = currentProduct.FirstOrDefault(j => j.Id == i.ProductId);
                productToUpdate.Stock -= i.Quantity;
            }
            await _productRepository.Update(currentProduct);

            var order = await _orderRepository.GetListByListIdWhere(listInputCreateProductOrder.Select(i => i.OrderId).ToList());
            foreach (var i in createProductOrder)
            {
                var TotalFromOrder = order.FirstOrDefault(j => j.Id == i.OrderId);
                TotalFromOrder.Total += i.SubTotal;
            }
            await _orderRepository.Update(order);

            return new BaseResponse<List<OutputProductOrder>> { Success = true, Content = createProductOrder.ToListOuputProductOrder() };
        }

        #endregion
    }
}