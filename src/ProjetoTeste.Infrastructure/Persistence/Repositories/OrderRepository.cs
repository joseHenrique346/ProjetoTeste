using Microsoft.EntityFrameworkCore;
using ProjetoTeste.Arguments.Arguments.Order.Reports.DTO;
using ProjetoTeste.Infrastructure.Interface.Repository;
using ProjetoTeste.Infrastructure.Persistence.Context;
using ProjetoTeste.Infrastructure.Persistence.Entities;

namespace ProjetoTeste.Infrastructure.Persistence.Repositories
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(AppDbContext context) : base(context) { }

        #region GetWithInclude

        public async Task<List<Order?>> GetWithIncludesAsync(List<long> id)
        {
            if (id == null || !id.Any())
                return new List<Order?>();

            var getWithIncludeSet = await _context.Set<Order>()
                                          .Include(x => x.ListProductOrder)
                                          .Where(order => id.Contains(order.Id))
                                          .ToListAsync();

            return getWithIncludeSet;
        }

        public async Task<List<Order?>> GetWithIncludesAsync()
        {
            return await _context.Set<Order>().Include(x => x.ListProductOrder).ToListAsync();
        }

        #endregion

        #region Produto mais pedido

        public async Task<List<MaxSaleValueProductDTO>> GetMostOrderedProduct()
        {
            var listMostOrderedProduct = from i in _dbSet
                                         from j in i.ListProductOrder
                                         group j by j.Product into g
                                         select new MaxSaleValueProductDTO
                                         {
                                             ProductId = g.Key.Id,
                                             ProductName = g.Key.Name,
                                             ProductCode = g.Key.Code,
                                             ProductDescription = g.Key.Description,
                                             TotalSaleValue = g.Sum(i => i.SubTotal),
                                             ProductBrandId = g.Key.BrandId,
                                             TotalSaleQuantity = g.Sum(i => i.Quantity)
                                         };

            return listMostOrderedProduct.OrderByDescending(i => i.TotalSaleQuantity).ToList();
        }

        #endregion

        #region Pedido com maior média de preço

        public async Task<AverageSaleValueOrderDTO> GetOrderAveragePrice()
        {
            var TotalSaleValue = (from i in _dbSet
                                  from j in i.ListProductOrder
                                  group j by j.OrderId into g
                                  select new AverageSaleValueOrderDTO
                                  {
                                      OrderId = g.Key,
                                      TotalSaleQuantity = g.Sum(i => i.Quantity),
                                      Total = g.Sum(i => i.SubTotal),
                                      AverageTotalSaleValue = g.Average(i => i.SubTotal)
                                  }).ToList().FirstOrDefault();
            return TotalSaleValue;
        }


        #endregion

        #region Produto menos pedido

        public async Task<List<MinSaleValueProductDTO>> GetLeastOrderedProduct()
        {
            var listLeastOrderedProduct = (from i in _dbSet
                                           from j in i.ListProductOrder
                                           group j by j.Product into g
                                           select new MinSaleValueProductDTO
                                           {
                                               ProductId = g.Key.Id,
                                               ProductName = g.Key.Name,
                                               ProductCode = g.Key.Code,
                                               ProductDescription = g.Key.Description,
                                               TotalSaleValue = g.Sum(i => i.SubTotal),
                                               ProductBrandId = g.Key.BrandId,
                                               TotalSaleQuantity = g.Sum(i => i.Quantity)
                                           })
                                           .OrderBy(i => i.TotalSaleQuantity)
                                           .ToList();
            return listLeastOrderedProduct;
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

        #region Cliente com mais pedidos

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

        #endregion

        #region Cliente que mais gastou

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

        #endregion
    }
}