using Microsoft.EntityFrameworkCore;
using ProjetoTeste.Arguments.Arguments.Order.GetLINQ;
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
            //var order = _dbSet.Include(i => i.ListProductOrder).ThenInclude(j => j.Product).ToList();
            //return (from i in _dbSet
            //        from j in i.ListProductOrder
            //        group j by j.Product into g
            //        let totalSaleQuantity = g.Sum(i => i.Quantity)
            //        let totalSaleValue = g.Sum(k => k.SubTotal)
            //        select new OutputMaxSaleValueProduct(g.Key.Id, g.Key.Name, g.Key.Code, g.Key.Description, totalSaleValue, g.Key.BrandId, totalSaleQuantity)).OrderByDescending(l => l.Quantity).ToList();

            var listMostOrderedProduct = (from i in _dbSet
                                          from j in i.ListProductOrder
                                          group j by j.Product into g
                                          select new
                                          {
                                              Product = g.Key,
                                              TotalSaleQuantity = g.Sum(i => i.Quantity),
                                              TotalSaleValue = g.Sum(i => i.SubTotal)
                                          }).Select(k => new OutputMaxSaleValueProduct(k.Product.Id, k.Product.Name, k.Product.Code, k.Product.Description, k.TotalSaleValue, k.Product.BrandId, k.TotalSaleQuantity)).ToList();
            return listMostOrderedProduct.OrderByDescending(i => i.Quantity).ToList();
        }

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
                                  }).Select(i => new OutputAverageSaleValueOrder(i.OrderId, i.Quantity, i.Total, i.AverageTotalSaleValue)).ToList().OrderByDescending(i => i.AveragePrice).ToList();
            return TotalSaleValue[0];
        }

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
                                           }).Select(k => new OutputMinSaleValueProduct(k.Product.Id, k.Product.Name, k.Product.Code, k.Product.Description, k.TotalSaleValue, k.Product.BrandId, k.TotalSaleQuantity)).ToList();
            return listLeastOrderedProduct.OrderBy(i => i.Quantity).ToList();
        }

        //public async Task<OutputMaxSaleValueBrand> GetMostOrderedBrand()
        //{
        //    var mostOrderedBrand = (from i in _dbSet
        //                            from j in i.ListProductOrder
        //                            group j by j.Product into g
        //                            select new
        //                            {
        //                                Brand = g.Key,
        //                                TotalSaleQuantity = g.Sum(i => i.Quantity),
        //                                BrandName = " ",
        //                                TotalSaleValue = g.Sum(i => i.SubTotal)
        //                            }).Select(k => new OutputMaxSaleValueBrand(k.BrandName, k.TotalSaleValue, k.Brand.BrandId, k.TotalSaleQuantity)).ToList().OrderByDescending(i => i.Quantity).ToList();
        //    return mostOrderedBrand[0];
        //}

        //public async Task<List<Order?>> GetMostOrderedClient()
        //{

        //}
    }
}