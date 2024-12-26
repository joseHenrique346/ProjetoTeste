using ProjetoTeste.Arguments.Arguments.Product;
using ProjetoTeste.Arguments.Arguments.Response;
using ProjetoTeste.Infrastructure.Conversor;
using ProjetoTeste.Infrastructure.Interface.Repository;
using ProjetoTeste.Infrastructure.Interface.UnitOfWork;
using ProjetoTeste.Infrastructure.Persistence.Entities;
using System.Linq.Expressions;

namespace ProjetoTeste.Infrastructure.Service
{
    public class ProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IBrandRepository _brandRepository;
        private readonly IUnitOfWork _uof;
        public ProductService(IUnitOfWork uof, IProductRepository productRepository, IBrandRepository brandRepository)
        {
            _uof = uof;
            _productRepository = productRepository;
            _brandRepository = brandRepository;
        }

        public async Task<string?> ValidateGetProductAsync(long id)
        {
            var existingProduct = await _productRepository.GetWithIncludesAsync(id, p => p.Brand);
            if (existingProduct is null)
            {
                return "*ERRO* Tem certeza que digitou o ID certo?";
            }

            return null;
        }

        public async Task<Response<Product?>> GetProductAsync(long id)
        {
            var validationMessage = await ValidateGetProductAsync(id);
            if (validationMessage != null)
            {
                return new Response<Product?>
                {
                    Success = false,
                    Message = validationMessage
                };
            }

            var product = await _productRepository.GetWithIncludesAsync(id, p => p.Brand);
            return new Response<Product?>
            {
                Success = true,
                Request = product
            };
        }

        public async Task<List<Product?>> GetAllProductAsync()
        {
            var allProducts = await _productRepository.GetAllAsync();
            return allProducts;
        }

        public async Task<Product?> GetWithIncludesAsync(long id, params Expression<Func<Product, object>>[] includes)
        {
            var productsWithIncludes = await _productRepository.GetWithIncludesAsync(id, includes);
            return productsWithIncludes;
        }

        public async Task<string?> ValidateCreateProductAsync(InputCreateProduct input)
        {
            var existingProduct = (await _productRepository.GetAllAsync())
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
                var brandExists = await _brandRepository.GetAsync(input.BrandId.Value);

                if (brandExists == null)
                {
                    var newBrand = new Brand
                    {
                        Id = input.BrandId.Value,
                        Name = "Marca Padrão"
                    };

                    await _brandRepository.CreateAsync(newBrand);
                    await _uof.CommitAsync();
                }
            }
            else
            {
                return "O ID da marca não pode ser nulo.";
            }

            return null;
        }



        public async Task<Response<Product>> CreateProductAsync(InputCreateProduct input)
        {
            var validationMessage = await ValidateCreateProductAsync(input);
            if (validationMessage != null)
            {
                return new Response<Product>
                {
                    Success = false,
                    Message = validationMessage
                };
            }

            var product = await _productRepository.CreateAsync(input.ToProduct());
            await _uof.CommitAsync();
            return new Response<Product>
            {
                Success = true,
                Request = product
            };
        }

        public string? ValidateUpdateProduct(long id, InputUpdateProduct input)
        {
            var allProducts = _productRepository.GetAll();

            var currentProduct = _productRepository.Get(id);

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

        public Response<Product> UpdateProduct(long id, InputUpdateProduct input)
        {
            var validationMessage = ValidateUpdateProduct(id, input);
            if (validationMessage != null)
            {
                return new Response<Product>
                {
                    Success = false,
                    Message = validationMessage
                };
            }

            var existingProduct = _productRepository.Get(id);

            if (existingProduct == null)
            {
                return new Response<Product>
                {
                    Success = false,
                    Message = "Produto não encontrado."
                };
            }

            existingProduct.Name = input.Name;
            existingProduct.Code = input.Code;
            existingProduct.Description = input.Description;
            existingProduct.Price = input.Price;
            existingProduct.Stock = input.Stock;
            existingProduct.BrandId = input.BrandId;

            _productRepository.Update(existingProduct);
            _uof.Commit();

            return new Response<Product>
            {
                Success = true,
                Request = existingProduct
            };
        }

        public async Task<Response<string?>> ValidateDeleteProductAsync(long id)
        {
            var products = await _productRepository.GetAllAsync();
            var existingProduct = products.FirstOrDefault(x => x.Id == id);

            if (existingProduct is null)
            {
                return new Response<string?>
                {
                    Success = false,
                    Message = "Não foi encontrado o ID inserido, foi informado corretamente?"
                };
            }

            return new Response<string?>
            {
                Success = true,
            };
        }

        public async Task<Response<bool>> DeleteProductAsync(long id)
        {
            var validationMessage = await ValidateDeleteProductAsync(id);
            if (!validationMessage.Success)
            {
                return new Response<bool>
                {
                    Success = false,
                    Message = validationMessage.Message
                };
            }

            await _productRepository.DeleteAsync(id);
            await _uof.CommitAsync();
            return new Response<bool>
            {
                Success = true
            };
        }
    }
}