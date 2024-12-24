using Microsoft.AspNetCore.Mvc;
using ProjetoTeste.Arguments.Arguments.Brand;
using ProjetoTeste.Infrastructure.Conversor;
using ProjetoTeste.Infrastructure.Interface.UnitOfWork;
using ProjetoTeste.Infrastructure.Persistence.Entities;

namespace ProjetoTeste.Infrastructure.Service
{
    public class BrandService
    {
        private readonly IUnitOfWork _uof;
        public BrandService(IUnitOfWork uof)
        {
            _uof = uof;
        }

        public async Task<string?> ValidateGetBrandAsync(long id)
        {
            var existingId = await _uof.BrandRepository.GetAsync(id);
            if (existingId is null)
            {
                return "*ERRO* Tem certeza que digitou o ID certo?";
            }

            return null;
        }

        public async Task<object> GetBrandAsync(long id)
        {
            var validationMessage = await ValidateGetBrandAsync(id);
            if (validationMessage != null)
            {
                return validationMessage;
            }

            var brand = await _uof.BrandRepository.GetAsync(id);

            return brand;
        }

        public async Task<string?> ValidateCreateBrandAsync(InputCreateBrand input)
        {
            var existingBrand = (await _uof.BrandRepository.GetAllAsync())
                                .FirstOrDefault(x => x.Name
                                .Equals(input.Name,
                                StringComparison.OrdinalIgnoreCase));

            if (existingBrand != null)
            {
                return "Este nome já está em uso!";
            }

            var existingCode = (await _uof.BrandRepository.GetAllAsync())
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

        public async Task<Brand> CreateBrandAsync(InputCreateBrand input)
        {
            var validationMessage = await ValidateCreateBrandAsync(input);
            if (validationMessage != null)
            {
                throw new ValidationException(validationMessage);
            }

            var brand = await _uof.BrandRepository.CreateAsync(input.ToBrand());
            await _uof.CommitAsync();
            return brand;
        }

        public string? ValidateUpdateBrand(long id, InputUpdateBrand input)
        {
            var currentBrand = _uof.BrandRepository.Get(id);

            if (currentBrand == null)
            {
                return "A marca especificada não foi encontrada.";
            }

            var allBrands = _uof.BrandRepository.GetAll();

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

        public Brand UpdateBrand(int id, InputUpdateBrand input)
        {
            var validationMessage = ValidateUpdateBrand(id, input);
            var currentBrand = _uof.BrandRepository.Get(id);

            if (validationMessage is string)
            {
                throw new ValidationException(validationMessage);
            }

            currentBrand.Name = input.Name;
            currentBrand.Code = input.Code;
            currentBrand.Description = input.Description;

            _uof.BrandRepository.Update(currentBrand);
            _uof.Commit();

            return currentBrand;
        }

        private async Task<Brand> GetOrCreateGenericBrandAsync()
        {
            const string genericBrandCode = "GEN";
            var genericBrand = (await _uof.BrandRepository.GetAllAsync())
                               .FirstOrDefault(b => b.Code == genericBrandCode);

            if (genericBrand == null)
            {
                genericBrand = new Brand
                {
                    Name = "Marca Genérica",
                    Description = "Esta é a marca atribuída automaticamente para produtos sem marca específica.",
                    Code = genericBrandCode
                };

                await _uof.BrandRepository.CreateAsync(genericBrand);
                await _uof.CommitAsync();
            }

            return genericBrand;
        }


        public async Task<string?> ValidateDeleteBrandAsync(long id)
        {
            var existingBrand = (await _uof.BrandRepository.GetAllAsync())
                                .FirstOrDefault(x => x.Id == id);

            if (existingBrand is null)
            {
                return "Não foi encontrado o ID inserido, foi informado corretamente?";
            }

            return null;
        }

        public async Task<bool> DeleteBrandAsync(long id)
        {
            var validationMessage = await ValidateDeleteBrandAsync(id);
            if (validationMessage is string)
            {
                throw new ValidationException(validationMessage);
            }

            var brand = await _uof.BrandRepository.GetAsync(id);
            if (brand == null)
            {
                throw new ValidationException("Marca não encontrada.");
            }

            var genericBrand = await GetOrCreateGenericBrandAsync();
            if (genericBrand == null)
            {
                throw new ValidationException("Marca genérica não encontrada.");
            }

            genericBrand = await GetOrCreateGenericBrandAsync();

            var products = await _uof.ProductRepository.GetAllAsync();
            var productsToUpdate = products.Where(p => p.BrandId == id).ToList();

            foreach (var product in productsToUpdate)
            {
                product.BrandId = genericBrand.Id;
                _uof.ProductRepository.Update(product);
            }

            await _uof.BrandRepository.DeleteAsync(id);
            await _uof.CommitAsync();

            return true;
        }
    }
}