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

        public async Task<OutputMaxSaleValueBrand> GetMostOrderedBrand()
        {
            var mostOrderedBrand = (from i in _dbSet
                                    from j in i.ListProductOrder
                                    group j by j.Product into g
                                    select new
                                    {
                                        Brand = g.Key,
                                        TotalSaleQuantity = g.Sum(i => i.Quantity),
                                        BrandName = " ",
                                        TotalSaleValue = g.Sum(i => i.SubTotal)
                                    }).Select(k => new OutputMaxSaleValueBrand(k.BrandName, k.TotalSaleValue, k.Brand.BrandId, k.TotalSaleQuantity)).ToList().OrderByDescending(i => i.Quantity).ToList();
            return mostOrderedBrand[0];
        }

        //public async Task<List<Order?>> GetMostOrderedClient()
        //{

        //}

        //public async Task<List<OutputMinSaleValueProduct>> GetMostOrderedClient()
        //{
        //    var totalSeller2 = (from i in _dbSet
        //                        from j in i.ListProductOrder
        //                        join product in _context.Product on j.ProductId equals product.Id
        //                        join brand in _context.Brand on product.BrandId equals brand.Id
        //                        group j by brand into g
        //                        let totalSaleQuantity = g.Sum(i => i.Quantity)
        //                        let totalSaleValue = g.Sum(i => i.SubTotal)
        //                        select new
        //                        {
        //                            BrandCode = g.Key.Code,
        //                            BrandDescription = g.Key.Description,
        //                            TotalValue = totalSaleValue
        //                        }).OrderByDescending(i => i.TotalValue).FirstOrDefault();
        //}

        //public async Task<BaseResponse<OutputBrandBestSeller>> BrandBestSeller()
        //{
        //    var order = await _dbSet.Include(po)
        //    if (order.Count() == 0)
        //        return new BaseResponse<OutputBrandBestSeller>() { Success = false, Message = { " >>> Lista De Pedidos Vazia <<<" } };
        //    var brandShere = (from i in order
        //                      from j in i.ListProductOrder
        //                      group j by j.ProductId into g
        //                      select new
        //                      {
        //                          productId = g.Key,
        //                          totalSeller = g.Sum(p => p.Quantity),
        //                          totalPrice = g.Sum(p => p.SubTotal),
        //                          brandId = _productRepository.BrandId(g.Key)
        //                      }).ToList();
        //    var brandBestSeller = (from i in brandShere
        //                           group i by i.brandId into g
        //                           select new
        //                           {
        //                               brandId = g.Key,
        //                               TotalSell = g.Sum(b => b.totalSeller),
        //                               TotalPrice = g.Sum(b => b.totalPrice),
        //                           }).MaxBy(b => b.TotalSell);
        //    var brand = await _brandRepository.Get(brandBestSeller.brandId);
        //    return new BaseResponse<OutputBrandBestSeller>() { Success = true, Content = new OutputBrandBestSeller(brand.Id, brand.Name, brand.Code, brand.Description, brandBestSeller.TotalSell, brandBestSeller.TotalPrice) };
        //}
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