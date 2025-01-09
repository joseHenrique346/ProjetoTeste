using ProjetoTeste.Arguments.Arguments.Brand;
using ProjetoTeste.Arguments.Arguments.Response;
using ProjetoTeste.Infrastructure.Interface.Repository;
using ProjetoTeste.Infrastructure.Interface.Service;

namespace ProjetoTeste.Infrastructure.Service;

public class BrandValidateService : IBrandValidateService
{
    private readonly IProductRepository _productRepository;
    private readonly IBrandRepository _brandRepository;
    public BrandValidateService(IBrandRepository brandRepository, IProductRepository productRepository)
    {
        _brandRepository = brandRepository;
        _productRepository = productRepository;
    }

    public async Task<Response<InputCreateBrand?>> ValidateCreateBrand(InputCreateBrand input)
    {
        var existingCode = await _brandRepository.GetByCode(input.Code);

        if (existingCode != null)
        {
            return new Response<InputCreateBrand?>() { Success = false, Message = "Este código já está em uso!" };
        }

        if (string.IsNullOrEmpty(input.Code))
        {
            return new Response<InputCreateBrand?>() { Success = false, Message = "O código tem que ser preenchido!" };
        }

        if (string.IsNullOrEmpty(input.Description))
        {
            return new Response<InputCreateBrand?>() { Success = false, Message = "A descrição tem que ser preenchida!" };
        }

        return new Response<InputCreateBrand?> { Success = true, Request = input };
    }

    public async Task<Response<InputUpdateBrand?>> ValidateUpdateBrand(long id, InputUpdateBrand input)
    {
        var currentBrand = await _brandRepository.GetAsync(id);

        if (currentBrand == null)
        {
            return new Response<InputUpdateBrand?>() { Success = false, Message = "A marca especificada não foi encontrada." };
        }

        var existingCodeBrand = await _brandRepository.GetByCode(input.Code);

        if (existingCodeBrand != null)
        {
            return new Response<InputUpdateBrand?>() { Success = false, Message = "Já existe uma marca com este código." };
        }

        if (string.IsNullOrEmpty(input.Description))
        {
            return new Response<InputUpdateBrand?>() { Success = false, Message = "A descrição não pode ser vazia." };
        }

        return null;
    }

    public async Task<Response<string?>> ValidateDeleteBrand(long id)
    {
        var existingBrand = (await _brandRepository.GetAllAsync())
                            .FirstOrDefault(x => x.Id == id);

        if (existingBrand is null)
        {
            return new Response<string?> { Success = false, Message = "Não foi encontrado o ID inserido, foi informado corretamente?" };
        }

        return new Response<string?>
        {
            Success = true
        };
    }
}