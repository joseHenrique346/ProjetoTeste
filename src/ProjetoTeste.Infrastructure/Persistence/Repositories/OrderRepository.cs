using Microsoft.EntityFrameworkCore;
using ProjetoTeste.Arguments.Arguments.Order.Reports.DTO;
using ProjetoTeste.Arguments.Arguments.Order.Reports.Outputs;
using ProjetoTeste.Infrastructure.Interface.Repository;
using ProjetoTeste.Infrastructure.Interface.Service;
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

        #region Produto mais pedido

        public async Task<List<OutputMaxSaleValueProduct>> GetMostOrderedProduct()
        {
            var listMostOrderedProduct = (from i in _dbSet
                                          from j in i.ListProductOrder
                                          group j by j.Product into g
                                          select new
                                          {
                                              Product = g.Key,
                                              TotalSaleQuantity = g.Sum(i => i.Quantity),
                                              TotalSaleValue = g.Sum(i => i.SubTotal)
                                          }).Select(k => new OutputMaxSaleValueProduct(k.Product.Id, k.Product.Name, k.Product.Code, k.Product.Description, k.TotalSaleValue, k.Product.BrandId, k.TotalSaleQuantity));
            return listMostOrderedProduct.OrderByDescending(i => i.Quantity).ToList();
        }

        #endregion

        #region Pedido com maior média de preço

        public async Task<OutputAverageSaleValueOrder> GetOrderAveragePrice()
        {
            var TotalSaleValue = (from i in _dbSet
                                  from j in i.ListProductOrder
                                  group j by j.OrderId into g
                                  select new
                                  {
                                      OrderId = g.Key,
                                      Quantity = g.Sum(i => i.Quantity),
                                      Total = g.Sum(i => i.SubTotal),
                                      AverageTotalSaleValue = g.Average(i => i.SubTotal)
                                  }).Select(i => new OutputAverageSaleValueOrder(i.OrderId, i.Quantity, i.Total, i.AverageTotalSaleValue)).OrderByDescending(i => i.AveragePrice).ToList().FirstOrDefault();
            return TotalSaleValue;
        }


        #endregion

        #region Produto menos pedido

        public async Task<List<OutputMinSaleValueProduct>> GetLeastOrderedProduct()
        {
            var listLeastOrderedProduct = (from i in _dbSet
                                           from j in i.ListProductOrder
                                           group j by j.Product into g
                                           select new
                                           {
                                               Product = g.Key,
                                               TotalSaleQuantity = g.Sum(i => i.Quantity),
                                               TotalSaleValue = g.Sum(i => i.SubTotal)
                                           }).Select(k => new OutputMinSaleValueProduct(k.Product.Id, k.Product.Name, k.Product.Code, k.Product.Description, k.TotalSaleValue, k.Product.BrandId, k.TotalSaleQuantity));
            return listLeastOrderedProduct.OrderBy(i => i.Quantity).ToList();
        }

        #endregion

        #region Marca mais pedida

        public async Task<MostOrderedBrandDTO> GetMostOrderedBrand()
        {
            var mostOrderedBrand = (from i in _dbSet
                                    from j in i.ListProductOrder
                                    group j by j.Product.BrandId into g
                                    select new MostOrderedBrandDTO
                                    {
                                        BrandId = g.Key,
                                        TotalSaleQuantity = g.Sum(i => i.Quantity),
                                        TotalSaleValue = g.Sum(i => i.SubTotal)
                                    })
                                    .OrderByDescending(i => i.TotalSaleQuantity)
                                    .FirstOrDefault();
            return mostOrderedBrand;
        }

        #endregion

        public async Task<MostOrdersClientDTO> GetMostOrdersCustomer()
        {
            var mostOrdersClient = (from i in _dbSet
                                    from j in i.ListProductOrder
                                    group j by i.CustomerId into g
                                    select new MostOrdersClientDTO
                                    {
                                        CustomerId = g.Key,
                                        TotalSaleQuantity = g.Sum(i => i.Quantity),
                                        TotalSaleValue = g.Sum(i => i.SubTotal)
                                    }).OrderByDescending(i => i.TotalSaleQuantity).FirstOrDefault();
            return mostOrdersClient;
        }

        public async Task<MostValueOrderClientDTO> GetMostValueOrderCustomer()
        {
            var mostValueOrderClient = (from i in _dbSet
                                    from j in i.ListProductOrder
                                    group j by i.CustomerId into g
                                    select new MostValueOrderClientDTO
                                    {
                                        CustomerId = g.Key,
                                        TotalSaleQuantity = g.Sum(i => i.Quantity),
                                        TotalSaleValue = g.Sum(i => i.SubTotal)
                                    }).OrderByDescending(i => i.TotalSaleValue).FirstOrDefault();
            return mostValueOrderClient;
        }
    }
}