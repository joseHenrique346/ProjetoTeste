using ProjetoTeste.Arguments.Arguments.Brand;
using ProjetoTeste.Arguments.Arguments.Response;
using ProjetoTeste.Infrastructure.Conversor;
using ProjetoTeste.Infrastructure.Interface.Repository;
using ProjetoTeste.Infrastructure.Interface.UnitOfWork;
using ProjetoTeste.Infrastructure.Persistence.Entities;

namespace ProjetoTeste.Infrastructure.Service
{
    public class BrandService
    {
        private readonly IProductRepository _productRepository;
        private readonly IBrandRepository _brandRepository;
        public BrandService(IBrandRepository brandRepository, IProductRepository productRepository)
        {
            _brandRepository = brandRepository;
            _productRepository = productRepository;
        }

        public async Task<Response<Brand?>> Get(long id)
        {
            var brand = await _brandRepository.GetAsync(id); 

            return new Response<Brand?>
            {
                Success = true,
                Request = brand 
            };
        }

        public async Task<List<Brand?>> GetAll()
        {
            return await _brandRepository.GetAllAsync();
        }

        public async Task<string?> ValidateCreateBrandAsync(InputCreateBrand input)
        {
            var existingCode = await _brandRepository.GetByCode(input.Code);

            if (existingCode != null)
            {
                return "Este código já está em uso!";
            }

            if (string.IsNullOrEmpty(input.Code))
            {
                return "O código tem que ser preenchido!";
            }

            if (string.IsNullOrEmpty(input.Description))
            {
                return "A descrição tem que ser preenchida!";
            }

            return null;
        }

        public async Task<Response<Brand>> Create(InputCreateBrand input)
        {
            var validationMessage = await ValidateCreateBrandAsync(input);
            if (validationMessage != null)
            {
                return new Response<Brand>()
                {
                    Success = false,
                    Message = validationMessage
                };
            }

            var brand = await _brandRepository.CreateAsync(input.ToBrand());
            return new Response<Brand>()
            {
                Success = true,
                Request = brand
            };
        }

        public async Task<string?> ValidateUpdateBrand(long id, InputUpdateBrand input)
        {
            var currentBrand = await _brandRepository.GetAsync(id);

            if (currentBrand == null)
            {
                return "A marca especificada não foi encontrada.";
            }

            var existingCodeBrand = await _brandRepository.GetByCode(input.Code);

            if (existingCodeBrand != null)
            {
                return "Já existe uma marca com este código.";
            }

            if (string.IsNullOrEmpty(input.Description))
            {
                return "A descrição não pode ser vazia.";
            }

            return null;
        }

        public async Task<Response<Brand>> Update(int id, InputUpdateBrand input)
        {
            var validationMessage = await ValidateUpdateBrand(id, input);
            var currentBrand = await _brandRepository.GetAsync(id);

            if (validationMessage is string)
            {
                return new Response<Brand>
                {
                    Success = false,
                    Message = validationMessage
                };
            }

            currentBrand.Name = input.Name;
            currentBrand.Code = input.Code;
            currentBrand.Description = input.Description;

            await _brandRepository.Update(currentBrand);

            return new Response<Brand>
            {
                Success = true,
                Request = currentBrand
            };
        }

        public async Task<Response<string?>> ValidateDeleteBrandAsync(long id)
        {
            var existingBrand = (await _brandRepository.GetAllAsync())
                                .FirstOrDefault(x => x.Id == id);

            if (existingBrand is null)
            {
                return new Response<string?>
                {
                    Success = false,
                    Message = "Não foi encontrado o ID inserido, foi informado corretamente?"
                };
            }

            return new Response<string?>
            {
                Success = true
            };
        }

        public async Task<Response<bool>> Delete(long id)
        {
            var result = await ValidateDeleteBrandAsync(id);
            if (!result.Success)
            {
                return new Response<bool>
                {
                    Success = false,
                    Message = result.Message
                };
            }

            var brand = await _brandRepository.GetAsync(id);
            if (brand == null)
            {
                return new Response<bool>
                {
                    Success = false,
                    Message = "Marca não encontrada."
                };
            }

            await _brandRepository.DeleteAsync(id);

            return new Response<bool>
            {
                Success = true,
                Request = true
            };
        }
    }
}