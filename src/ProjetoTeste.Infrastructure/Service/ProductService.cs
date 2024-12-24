using Microsoft.AspNetCore.Mvc;
using ProjetoTeste.Arguments.Arguments.Product;
using ProjetoTeste.Infrastructure.Interface.UnitOfWork;
using ProjetoTeste.Infrastructure.Persistence.Entities;

namespace ProjetoTeste.Infrastructure.Service
{
    public class ProductService
    {
        private readonly IUnitOfWork _uof;
        public ProductService(IUnitOfWork uof)
        {
            _uof = uof;
        }

        public async Task<string?> ValidateGetProductAsync(long id)
        {
            var existingProduct = await _uof.ProductRepository.GetWithIncludesAsync(id, p => p.Brand);
            if (existingProduct is null)
            {
                return "*ERRO* Tem certeza que digitou o ID certo?";
            }

            return null;
        }

        public async Task<Product?> GetProductAsync(long id)
        {
            var validationMessage = await ValidateGetProductAsync(id);
            if (validationMessage != null)
            {
                throw new ValidationException(validationMessage);
            }

            var product = await _uof.ProductRepository.GetWithIncludesAsync(id, p => p.Brand);
            return product;
        }

        public async Task<string?> ValidateCreateProductAsync(InputCreateProduct input)
        {
            var existingProduct = (await _uof.ProductRepository.GetAllAsync())
                                  .FirstOrDefault(x => x.Code.Equals(input.Code));

            if (existingProduct != null)
            {
                return "Já existe produto com este Código.";
            }

            if (string.IsNullOrEmpty(input.Code))
            {
                return "O código tem que ser preenchido!";
            }

            if (string.IsNullOrEmpty(input.Description))
            {
                return "A descrição tem que ser preenchida!";
            }

            if (input.Stock < 0)
            {
                return "O estoque não pode ser menor que zero.";
            }

            if (input.Price < 0)
            {
                return "O preço não pode ser menor que zero.";
            }

            if (input.BrandId.HasValue && input.BrandId.Value <= 0)
            {
                return "Informe um valor válido para o Id da marca.";
            }

            if (input.BrandId.HasValue)
            {
                var brandExists = await _uof.BrandRepository.GetAsync(input.BrandId.Value);

                if (brandExists == null)
                {
                    var newBrand = new Brand
                    {
                        Id = input.BrandId.Value,
                        Name = "Marca Padrão"
                    };

                    await _uof.BrandRepository.CreateAsync(newBrand);
                    await _uof.CommitAsync();
                }
            }
            else
            {
                return "O ID da marca não pode ser nulo.";
            }

            return null;
        }



        public async Task<Product> CreateProductAsync(InputCreateProduct input)
        {
            var validationMessage = await ValidateCreateProductAsync(input);
            if (validationMessage != null)
            {
                throw new ValidationException(validationMessage);
            }

            var product = await _uof.ProductRepository.CreateAsync(input.ToProduct());
            await _uof.CommitAsync();
            return product;
        }

        public string? ValidateUpdateProduct(long id, InputUpdateProduct input)
        {
            var allProducts = _uof.ProductRepository.GetAll();

            var currentProduct = _uof.ProductRepository.Get(id);

            if (currentProduct == null)
            {
                return "Produto não encontrado.";
            }

            var existingCodeProduct = allProducts
                .FirstOrDefault(x =>
                    x.Code.Equals(input.Code) &&
                    x.Id != currentProduct.Id);

            if (existingCodeProduct != null)
            {
                return "Já existe um produto com este código.";
            }

            if (string.IsNullOrEmpty(input.Description))
            {
                return "A descrição não pode ser vazia.";
            }

            return null;
        }

        public Product UpdateProduct(long id, InputUpdateProduct input)
        {
            var validationMessage = ValidateUpdateProduct(id, input);
            if (validationMessage != null)
            {
                throw new ValidationException(validationMessage);
            }

            var existingProduct = _uof.ProductRepository.Get(id);

            if (existingProduct == null)
            {
                throw new ValidationException("Produto não encontrado.");
            }

            existingProduct.Name = input.Name;
            existingProduct.Code = input.Code;
            existingProduct.Description = input.Description;
            existingProduct.Price = input.Price;
            existingProduct.Stock = input.Stock;
            existingProduct.BrandId = input.BrandId;

            _uof.ProductRepository.Update(existingProduct);
            _uof.Commit();

            return existingProduct;
        }

        public async Task<string?> ValidateDeleteProductAsync(long id)
        {
            var products = await _uof.ProductRepository.GetAllAsync();
            var existingProduct = products.FirstOrDefault(x => x.Id == id);

            if (existingProduct is null)
            {
                return "Não foi encontrado o ID inserido, foi informado corretamente?";
            }

            return null;
        }

        public async Task<bool> DeleteProductAsync(long id)
        {
            var validationMessage = await ValidateDeleteProductAsync(id);
            if (validationMessage is string)
            {
                throw new ValidationException(validationMessage);
            }

            await _uof.ProductRepository.DeleteAsync(id);
            await _uof.CommitAsync();
            return true;
        }
    }
}