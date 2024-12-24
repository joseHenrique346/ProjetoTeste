using Microsoft.AspNetCore.Mvc;
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
        private readonly IUnitOfWork _uof;
        private readonly IProductRepository _productRepository;
        private readonly IBrandRepository _brandRepository;
        public BrandService(IUnitOfWork uof, IBrandRepository brandRepository, IProductRepository productRepository)
        {
            _uof = uof;
            _brandRepository = brandRepository;
            _productRepository = productRepository;
        }

        public async Task<Response<string?>> ValidateGetBrandAsync(long id)
        {
            var existingId = await _brandRepository.GetAsync(id);
            if (existingId is null)
            {
                return new Response<string?>
                {
                    Success = false,
                    Message = "*ERRO* Tem certeza que digitou o ID certo?"
                };
            }

            return new Response<string?>
            {
                Success = true
            };
        }

        public async Task<Response<object>> GetBrandAsync(long id)
        {
            var validationResponse = await ValidateGetBrandAsync(id); 

            if (!validationResponse.Success) 
            {
                return new Response<object>
                {
                    Success = false,
                    Message = validationResponse.Message 
                };
            }

            var brand = await _brandRepository.GetAsync(id); 

            if (brand == null)
            {
                return new Response<object>
                {
                    Success = false,
                    Message = "Marca não encontrada."
                };
            }

            return new Response<object>
            {
                Success = true,
                Request = brand 
            };
        }


        public async Task<string?> ValidateCreateBrandAsync(InputCreateBrand input)
        {
            var existingBrand = (await _brandRepository.GetAllAsync())
                                .FirstOrDefault(x => x.Name
                                .Equals(input.Name,
                                StringComparison.OrdinalIgnoreCase));

            if (existingBrand != null)
            {
                return "Este nome já está em uso!";
            }

            var existingCode = (await _brandRepository.GetAllAsync())
                                .FirstOrDefault(x =>
                                x.Code.Equals(input.Code));

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

        public async Task<Response<Brand>> CreateBrandAsync(InputCreateBrand input)
        {
            var validationMessage = await ValidateCreateBrandAsync(input);
            if (validationMessage != null)
            {
                new Response<Brand>()
                {
                    Success = false,
                    Message = validationMessage
                };
            }

            var brand = await _brandRepository.CreateAsync(input.ToBrand());
            await _uof.CommitAsync();
            return new Response<Brand>()
            {
                Success = true,
                Request = brand
            };
        }

        public string? ValidateUpdateBrand(long id, InputUpdateBrand input)
        {
            var currentBrand = _brandRepository.Get(id);

            if (currentBrand == null)
            {
                return "A marca especificada não foi encontrada.";
            }

            var allBrands = _brandRepository.GetAll();

            var existingNameBrand = allBrands.FirstOrDefault(x =>
                x.Name.Equals(input.Name, StringComparison.OrdinalIgnoreCase) &&
                x.Id != currentBrand.Id);

            if (existingNameBrand != null)
            {
                return "Já existe uma marca com este nome.";
            }

            var existingCodeBrand = allBrands.FirstOrDefault(x =>
                x.Code.Equals(input.Code) &&
                x.Id != currentBrand.Id);

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

        public Response<Brand> UpdateBrand(int id, InputUpdateBrand input)
        {
            var validationMessage = ValidateUpdateBrand(id, input);
            var currentBrand = _brandRepository.Get(id);

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

            _brandRepository.Update(currentBrand);
            _uof.Commit();

            return new Response<Brand>
            {
                Success = true,
                Request = currentBrand
            };
        }

        private async Task<Brand> GetOrCreateGenericBrandAsync()
        {
            const string genericBrandCode = "GEN";
            var genericBrand = (await _brandRepository.GetAllAsync())
                               .FirstOrDefault(b => b.Code == genericBrandCode);

            if (genericBrand == null)
            {
                genericBrand = new Brand
                {
                    Name = "Marca Genérica",
                    Description = "Esta é a marca atribuída automaticamente para produtos sem marca específica.",
                    Code = genericBrandCode
                };

                await _brandRepository.CreateAsync(genericBrand);
                await _uof.CommitAsync();
            }

            return genericBrand;
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
                    Message = "Não foi encontrado o ID inserido, foi informado corretamente?";
                };
            }

            return new Response<string?>
            {
                Success = true
            };
        }

        public async Task<Response<bool>> DeleteBrandAsync(long id)
        {
            var validationResponse = await ValidateDeleteBrandAsync(id);
            if (!validationResponse.Success)
            {
                return new Response<bool>
                {
                    Success = false,
                    Message = validationResponse.Message
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

            var genericBrand = await GetOrCreateGenericBrandAsync();
            if (genericBrand == null)
            {
                return new Response<bool>
                {
                    Success = false,
                    Message = "Marca genérica não encontrada."
                };
            }

            await _brandRepository.DeleteAsync(id);
            await _uof.CommitAsync();

            return new Response<bool>
            {
                Success = true,
                Request = true
            };
        }

    }
}