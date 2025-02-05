using AutoMapper;
using ProjetoTeste.Arguments.Arguments;
using ProjetoTeste.Arguments.Arguments.Base.Inputs;
using ProjetoTeste.Arguments.Arguments.Order;
using ProjetoTeste.Arguments.Arguments.Order.Reports.Outputs;
using ProjetoTeste.Arguments.Arguments.ProductOrder;
using ProjetoTeste.Arguments.Arguments.Response;
using ProjetoTeste.Infrastructure.Interface.Repository;
using ProjetoTeste.Infrastructure.Interface.Service;
using ProjetoTeste.Infrastructure.Persistence.Entities;
using ProjetoTeste.Infrastructure.Service.Base;

namespace ProjetoTeste.Infrastructure.Service
{
    public class OrderService : BaseService<IOrderRepository, Order, InputIdentityViewOrder, InputCreateOrder, BaseInputIdentityUpdate_0, BaseInputIdentityDelete_0, OutputOrder, OrderDTO>, IOrderService
    {
        #region Dependency Injection

        private readonly IOrderRepository _orderRepository;
        private readonly IProductOrderRepository _productOrderRepository;
        private readonly IProductRepository _productRepository;
        private readonly IOrderValidateService _orderValidateService;
        private readonly IBrandRepository _brandRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;
        public OrderService(IOrderRepository orderRepository, IProductRepository productRepository, IProductOrderRepository productOrderRepository, IOrderValidateService orderValidateService, IBrandRepository brandRepository, ICustomerRepository customerRepository, IMapper mapper) : base(orderRepository, mapper)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _productOrderRepository = productOrderRepository;
            _orderValidateService = orderValidateService;
            _brandRepository = brandRepository;
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        #endregion

        #region Get
        public override async Task<OutputOrder?> GetSingle(InputIdentityViewOrder inputIdentityViewOrder)
        {
            return _mapper.Map<OutputOrder>(await _orderRepository.GetWithIncludesId(inputIdentityViewOrder.Id));
        }

        public override async Task<List<OutputOrder>> GetAll()
        {
            var getAll = await _orderRepository.GetWithIncludesAll();
            return _mapper.Map<List<OutputOrder>>(getAll);
        }

        public async Task<BaseResponse<List<OutputOrder>?>> Get(List<InputIdentityViewOrder> listInputIdentityViewOrder)
        {
            var response = new BaseResponse<List<OutputOrder>>();

            var orderId = await _orderRepository.GetWithIncludesList(listInputIdentityViewOrder.Select(i => i.Id).ToList());

            response.Content = _mapper.Map<List<OutputOrder>>(orderId);

            return response;
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

        public override async Task<BaseResponse<List<OutputOrder>>> Create(List<InputCreateOrder> listInputCreateOrder)
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
            response.Content = _mapper.Map<List<OutputOrder>>(order);
            return response;
        }

        //Cria um por vez

        public async Task<BaseResponse<OutputProductOrder>> CreateProductOrderSingle(InputCreateProductOrder inputCreateProductOrder)
        {
            var response = new BaseResponse<OutputProductOrder>();

            var result = await CreateProductOrder([inputCreateProductOrder]);

            response.Success = result.Success;
            response.Message = result.Message;
            if (!response.Success)
                return response;

            response.Content = result.Content.FirstOrDefault();

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
                                       ExistingProduct = existingProduct.FirstOrDefault(j => i.ProductId == j.Id)
                                   }).ToList();

            var listValidateCreateProductOrder = listInputCreate.Select(i => new ProductOrderValidate().CreateProductOrder(i.InputCreate, i.ExistingOrder, _mapper.Map<ProductDTO>(i.ExistingProduct))).ToList();

            var result = await _orderValidateService.ValidateCreateProductOrder(listValidateCreateProductOrder);

            response.Success = result.Success;
            response.Message = result.Message;

            if (!response.Success)
                return response;

            var listProductDTOValidate = result.Content.Select(i => i.ExistingProduct).ToList();

            existingProduct = (from i in existingProduct
                               where i.Id == listProductDTOValidate.FirstOrDefault(j => j.Id == i.Id)?.Id
                               let updateStock = i.Stock = listProductDTOValidate.FirstOrDefault(j => j.Id == i.Id).Stock
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

            var totalOrder = (from i in existingOrder
                              from j in createProductOrder
                              where i.Id == j.OrderId
                              let total = i.Total += j.SubTotal
                              select i).ToList();

            await _productRepository.Update(existingProduct);
            await _orderRepository.Update(totalOrder);

            await _productOrderRepository.CreateAsync(createProductOrder);

            response.Content = _mapper.Map<List<OutputProductOrder>>(createProductOrder);
            return response;
        }

        #endregion
    }
}