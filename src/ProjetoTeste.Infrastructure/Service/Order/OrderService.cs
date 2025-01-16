﻿using Microsoft.EntityFrameworkCore;
using ProjetoTeste.Arguments.Arguments.Order;
using ProjetoTeste.Arguments.Arguments.Order.Reports.Outputs;
using ProjetoTeste.Arguments.Arguments.ProductOrder;
using ProjetoTeste.Arguments.Arguments.Response;
using ProjetoTeste.Infrastructure.Conversor;
using ProjetoTeste.Infrastructure.Interface.Repository;
using ProjetoTeste.Infrastructure.Interface.Service;
using ProjetoTeste.Infrastructure.Persistence.Entities;
using ProjetoTeste.Infrastructure.Persistence.Repositories;

namespace ProjetoTeste.Infrastructure.Service
{
    public class OrderService
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
        public async Task<List<OutputOrder>?> Get(long id)
        {
            var orderId = await _orderRepository.GetWithIncludesAsync(id);
            return orderId.Select(i => i.ToOutputOrder()).ToList();
        }
        #endregion

        #region Relatório

        public async Task<List<OutputMaxSaleValueProduct>> GetMostOrderedProduct()
        {
            return await _orderRepository.GetMostOrderedProduct();
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
            return await _orderRepository.GetOrderAveragePrice();
        }

        public async Task<List<OutputMinSaleValueProduct>> GetLeastOrderedProduct()
        {
            return await _orderRepository.GetLeastOrderedProduct();
        }

        public async Task<OutputMostOrderQuantityCustomer> GetMostOrdersCustomer()
        {
            var mostOrdersClient = await _orderRepository.GetMostOrdersCustomer();

            var customerName = await _customerRepository.GetAsync(mostOrdersClient.CustomerId);

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

            var customerName = await _customerRepository.GetAsync(mostValueOrderClient.CustomerId);

            return new OutputMostValueOrderCustomer(
                mostValueOrderClient.CustomerId,
                customerName.Name.ToString(),
                mostValueOrderClient.TotalSaleValue,
                mostValueOrderClient.TotalSaleQuantity
            );
        }

        #endregion

        #region Create

        public async Task<BaseResponse<OutputOrder>> Create(InputCreateOrder input)
        {
            var result = await _orderValidateService.ValidateCreateOrder(input);

            if (!result.Success)
            {
                return new BaseResponse<OutputOrder> { Success = false, Message = result.Message };
            }
            var order = new Order(input.CustomerId, DateTime.Now);

            await _orderRepository.CreateAsync(order);
            return new BaseResponse<OutputOrder> { Success = true, Content = order.ToOutputOrder() };
        }

        public async Task<BaseResponse<OutputProductOrder>> CreateProductOrder(InputCreateProductOrder input)
        {
            var result = await _orderValidateService.ValidateCreateProductOrder(input);
            if (!result.Success)
            {
                return new BaseResponse<OutputProductOrder> { Success = false, Message = result.Message };
            }

            var currentProduct = await _productRepository.GetAsync(input.ProductId);

            var createProductOrder = new ProductOrder()
            {
                OrderId = input.OrderId,
                ProductId = input.ProductId,
                Quantity = input.Quantity,
                UnitPrice = currentProduct.Price,
                SubTotal = input.Quantity * currentProduct.Price
            };

            //var createProductOrder = new ProductOrder() { OrderId = input.OrderId, ProductId = input.ProductId, Quantity = input.Quantity, UnitPrice = currentProduct.Price };
            await _productOrderRepository.CreateAsync(createProductOrder);

            currentProduct.Stock -= input.Quantity;
            await _productRepository.Update(currentProduct);

            return new BaseResponse<OutputProductOrder> { Success = true, Content = createProductOrder.ToOuputProductOrder() };
        }

        #endregion

    }
}