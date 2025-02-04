using AutoMapper;
using ProjetoTeste.Arguments.Arguments;
using ProjetoTeste.Arguments.Arguments.Product;
using ProjetoTeste.Arguments.Arguments.Response;
using ProjetoTeste.Infrastructure.Interface.Repository;
using ProjetoTeste.Infrastructure.Interface.Service;
using ProjetoTeste.Infrastructure.Persistence.Entities;
using ProjetoTeste.Infrastructure.Service.Base;

namespace ProjetoTeste.Infrastructure.Service
{
    public class ProductService : BaseService<IProductRepository, Product, InputIdentityViewProduct, InputCreateProduct, InputIdentityUpdateProduct, InputIdentityDeleteProduct, OutputProduct, ProductDTO>, IProductService
    {

        #region Dependency Injection

        private readonly IProductRepository _productRepository;
        private readonly IBrandRepository _brandRepository;
        private readonly IProductValidateService _productValidateService;
        private readonly IMapper _mapper;
        public ProductService(IProductRepository productRepository, IBrandRepository brandRepository, IProductValidateService productValidateService, IMapper mapper) : base(productRepository, mapper)
        {
            _productRepository = productRepository;
            _brandRepository = brandRepository;
            _productValidateService = productValidateService;
            _mapper = mapper;
        }

        #endregion

        #region Create

        public override async Task<BaseResponse<List<OutputProduct>>> Create(List<InputCreateProduct> listInputCreateProduct)
        {
            var response = new BaseResponse<List<OutputProduct?>>();

            var listRepeatedCode = (from i in listInputCreateProduct
                                    where listInputCreateProduct.Count(j => j.Code == i.Code) > 1
                                    select i.Code).ToList();

            var existingCodeProduct = await _productRepository.GetListByCode(listInputCreateProduct.Select(i => i.Code).ToList());
            var selectedCodeExistingProduct = existingCodeProduct.Select(i => i.Code).ToList();

            var existingBrand = await _brandRepository.GetListByListIdWhere(listInputCreateProduct.Select(i => i.BrandId).ToList());
            var selectedExistingBrand = existingBrand.Select(i => i.Id);

            var listCreateProduct = (from i in listInputCreateProduct
                                     select new
                                     {
                                         InputCreate = i,
                                         RepeatedCode = listRepeatedCode.FirstOrDefault(j => i.Code == j),
                                         ExistingCodeProduct = selectedCodeExistingProduct.FirstOrDefault(j => j == i.Code),
                                         ExistingBrand = selectedExistingBrand.FirstOrDefault(k => i.BrandId == k)
                                     }).ToList();

            var listCreateProductValidate = listCreateProduct.Select(i => new ProductValidate().CreateValidate(i.InputCreate, i.ExistingCodeProduct, i.ExistingBrand, i.RepeatedCode)).ToList();

            var result = await _productValidateService.ValidateCreateProduct(listCreateProductValidate);
            response.Success = result.Success;
            response.Message = result.Message;

            if (!response.Success)
                return response;

            var listNewProduct = (from i in result.Content
                                  let message = response.AddSuccessMessage($"O produto {i.InputCreateProduct.Name} foi criado com sucesso!")
                                  select new Product(i.InputCreateProduct.Name, i.InputCreateProduct.Code, i.InputCreateProduct.Description, i.InputCreateProduct.BrandId, i.InputCreateProduct.Price, i.InputCreateProduct.Stock)
                                  ).ToList();

            var listProduct = await _productRepository.CreateAsync(listNewProduct);

            response.Content = _mapper.Map<List<Product>, List<OutputProduct>>(listProduct);
            return response;
        }

        #endregion

        #region Update

        public override async Task<BaseResponse<List<OutputProduct>>> Update(List<InputIdentityUpdateProduct> listInputIdentityUpdateProduct)
        {
            var response = new BaseResponse<List<OutputProduct>>();

            var currentProduct = await _productRepository.GetListByListIdWhere(listInputIdentityUpdateProduct.Select(i => i.Id).ToList());
            var selectCurrentProduct = currentProduct.Select(i => i.Id);

            var existingCodeProduct = await _productRepository.GetListByCode(listInputIdentityUpdateProduct.Select(i => i.InputUpdateProduct.Code).ToList());

            var existingBrand = await _brandRepository.GetListByListIdWhere(listInputIdentityUpdateProduct.Select(i => i.InputUpdateProduct.BrandId).ToList());
            var selectedExistingBrand = existingBrand.Select(i => i.Id);

            var listUpdate = (from i in listInputIdentityUpdateProduct
                              select new
                              {
                                  InputUpdate = i,
                                  CurrentProduct = selectCurrentProduct.FirstOrDefault(j => i.Id == j),
                                  ExistingCodeProduct = existingCodeProduct.FirstOrDefault(j => j.Id != i.Id),
                                  ExistingBrand = selectedExistingBrand.FirstOrDefault(j => i.InputUpdateProduct.BrandId == j)
                              }).ToList();

            var listValidateUpdate = listUpdate.Select(i => new ProductValidate().UpdateValidate(i.InputUpdate, _mapper.Map<ProductDTO>(i.ExistingCodeProduct), i.CurrentProduct, i.ExistingBrand)).ToList();

            var result = await _productValidateService.ValidateUpdateProduct(listValidateUpdate);

            response.Success = result.Success;
            response.Message = result.Message;

            if (!response.Success)
                return response;

            var updatedList = (from i in result.Content
                               from j in currentProduct
                               where i.InputIdentityUpdateProduct.Id == j.Id
                               let name = j.Name = i.InputIdentityUpdateProduct.InputUpdateProduct.Name
                               let code = j.Code = i.InputIdentityUpdateProduct.InputUpdateProduct.Code
                               let description = j.Description = i.InputIdentityUpdateProduct.InputUpdateProduct.Description
                               let brandId = j.BrandId = i.InputIdentityUpdateProduct.InputUpdateProduct.BrandId
                               let price = j.Price = i.InputIdentityUpdateProduct.InputUpdateProduct.Price
                               let stock = j.Stock = i.InputIdentityUpdateProduct.InputUpdateProduct.Stock
                               let message = response.AddSuccessMessage($"O produto {i.InputIdentityUpdateProduct.InputUpdateProduct.Name} foi atualizado com sucesso!")
                               select j).ToList();

            await _productRepository.Update(updatedList);

            response.Content = _mapper.Map<List<OutputProduct>>(updatedList);
            return response;
        }

        #endregion

        #region Delete

        public override async Task<BaseResponse<bool>> Delete(List<InputIdentityDeleteProduct> listInputIdentityDeleteProduct)
        {
            var response = new BaseResponse<bool>();

            var listProduct = await _productRepository.GetListByListIdWhere(listInputIdentityDeleteProduct.Select(i => i.Id).ToList());

            var listRepeatedId = (from i in listInputIdentityDeleteProduct
                                  where listInputIdentityDeleteProduct.Count(j => i.Id == j.Id) > 1
                                  select i).ToList();
            var selectedRepeatedId = listRepeatedId.Select(i => i.Id).ToList();

            var listInputDelete = (from i in listInputIdentityDeleteProduct
                                   select new
                                   {
                                       InputDelete = i,
                                       Product = listProduct.FirstOrDefault(j => i.Id == j.Id),
                                       RepeatedId = selectedRepeatedId.FirstOrDefault(j => i.Id == j)
                                   }).ToList();

            var listInputValidateDelete = listInputDelete.Select(i => new ProductValidate().DeleteValidate(i.InputDelete, _mapper.Map<ProductDTO>(i.Product), i.RepeatedId)).ToList();

            var result = await _productValidateService.ValidateDeleteProduct(listInputValidateDelete);
            response.Success = result.Success;
            response.Message = result.Message;

            if (!response.Success)
                return response;

            var deletedProduct = await _productRepository.GetListByListIdWhere(result.Content.Select(i => i.InputIdentityDeleteProduct.Id).ToList());
            foreach (var i in deletedProduct)
                response.AddSuccessMessage($"O produto {i.Name} foi deletado com sucesso!");

            await _productRepository.DeleteAsync(deletedProduct);
            response.Content = true;
            return response;
        }

        #endregion
    }
}