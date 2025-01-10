using Microsoft.EntityFrameworkCore;
using ProjetoTeste.Arguments.Arguments.Order;
using ProjetoTeste.Infrastructure.Conversor;
using ProjetoTeste.Infrastructure.Interface.Repository;
using ProjetoTeste.Infrastructure.Persistence.Context;
using ProjetoTeste.Infrastructure.Persistence.Entities;

namespace ProjetoTeste.Infrastructure.Persistence.Repositories
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(AppDbContext context) : base(context) { }

        public async Task<List<Order?>> GetWithIncludesAsync(long id)
        {
            return await _context.Set<Order>().Include(x => x.ListProductOrder).Where(x => x.Id == id).ToListAsync();
        }

        public async Task<List<Order?>> GetWithIncludesAsync()
        {
            return await _context.Set<Order>().Include(x => x.ListProductOrder).ToListAsync();
        }

        public async Task<List<OutputMaxSaleValueProduct>> GetMostOrderedProduct()
        {
            var order = _dbSet.AsEnumerable();
            return (from i in order
                    from j in i.ListProductOrder
                    group j by j.Product into g
                    let totalSaleQuantity = g.Sum(i => i.Quantity)
                    let totalSaleValue = g.Sum(i => i.TotalPrice)
                    select new OutputMaxSaleValueProduct(g.Key.Id, g.Key.Name, g.Key.Code, g.Key.Description, totalSaleValue, g.Key.BrandId, totalSaleQuantity)).OrderByDescending(i => i.TotalValue).ToList();
        }

        //public async Task<Order?> GetOrderAveragePrice()
        //{

        //}

        //public async Task<List<Order?>> LowerOrderProduct()
        //{

        //}

        //public async Task<List<Order?>> GetHigherOrderBrand()
        //{

        //}

        //public async Task<List<Order?>> GetHigherOrderClient()
        //{

        //}

        public async Task<List<OutputMinSaleValueProduct>> GetLowerOrderClient()
        {
            return (from i in _dbSet
                    from j in i.ListProductOrder
                    .AsEnumerable()
                    group j by j.Product into g
                    let totalSaleQuantity = g.Sum(i => i.Quantity)
                    let totalSaleValue = g.Sum(i => i.TotalPrice)
                    select new OutputMinSaleValueProduct(g.Key.Id, g.Key.Name, g.Key.Code, g.Key.Description, totalSaleValue, g.Key.BrandId, totalSaleQuantity)).OrderBy(i => i.TotalValue).ToList();
        }

        //public async Task<BaseResponse<List<ProductSell>>> ProductSell()
        //{
        //    var order = await _orderRepository.GetProductOrders();
        //    if (order.Count() == 0) return new BaseResponse<List<ProductSell>>() { Success = false, Message = new List<Notification> { new Notification { Message = " >>> Lista De Pedidos Vazia <<<", Type = EnumNotificationType.Error } } };
        //    var totalSeller = (from i in order
        //                       from j in i.ListProductOrder
        //                       group j by j.ProductId into g
        //                       select new
        //                       {
        //                           productId = g.Key,
        //                           totalSeller = g.Sum(p => p.Quantity),
        //                           totalPrice = g.Sum(p => p.SubTotal)
        //                       }).ToList();
        //    return new BaseResponse<List<ProductSell>>() { Success = true, Content = totalSeller.Select(p => new ProductSell(p.productId, p.totalSeller, p.totalPrice)).ToList() };
        //}

        //public async Task<BaseResponse<OutputSellProduct>> BestSellerProduct()
        //{
        //    var totalSeller = await ProductSell();
        //    if (!totalSeller.Success)
        //        return new BaseResponse<OutputSellProduct>() { Success = false, Message = totalSeller.Message };
        //    var BestSeller = totalSeller.Content.MaxBy(x => x.totalSeller);
        //    var bestSellerProduct = await _productRepository.Get(BestSeller.productId);
        //    var output = new BaseResponse<OutputSellProduct> { Success = true, Content = new OutputSellProduct(bestSellerProduct.Id, bestSellerProduct.Name, bestSellerProduct.Code, bestSellerProduct.Description, bestSellerProduct.Price, bestSellerProduct.BrandId, bestSellerProduct.Stock, BestSeller.totalSeller) };
        //    return output;
        //}
    }
}