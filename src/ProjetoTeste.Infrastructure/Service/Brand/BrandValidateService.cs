using ProjetoTeste.Arguments.Arguments.Brand;
using ProjetoTeste.Arguments.Arguments.Response;
using ProjetoTeste.Infrastructure.Interface.Repository;
using ProjetoTeste.Infrastructure.Interface.Service;

namespace ProjetoTeste.Infrastructure.Service;

public class BrandValidateService : IBrandValidateService
{
    
    #region Dependency Injection

    private readonly IProductRepository _productRepository;
    private readonly IBrandRepository _brandRepository;
    public BrandValidateService(IBrandRepository brandRepository, IProductRepository productRepository)
    {
        _brandRepository = brandRepository;
        _productRepository = productRepository;
    }

    #endregion

    #region Validate Create

    public async Task<BaseResponse<InputCreateBrand?>> ValidateCreateBrand(InputCreateBrand inputCreate)
    {
        var existingCode = await _brandRepository.GetByCode(inputCreate.Code);

        if (existingCode != null)
            return new BaseResponse<InputCreateBrand?>() { Success = false, Message = "Este código já está em uso!" };

        if (string.IsNullOrEmpty(inputCreate.Code))
        {
            return new BaseResponse<InputCreateBrand?>() { Success = false, Message = "O código tem que ser preenchido!" };
        }

        if (string.IsNullOrEmpty(inputCreate.Description))
        {
            return new BaseResponse<InputCreateBrand?>() { Success = false, Message = "A descrição tem que ser preenchida!" };
        }

        if (inputCreate.Name.Length > 40)
        {
            return new BaseResponse<InputCreateBrand?>() { Success = false, Message = "O nome não pode ultrapassar 40 caracteres" };
        }

        if (inputCreate.Code.Length > 6)
        {
            return new BaseResponse<InputCreateBrand?>() { Success = false, Message = "O código não pode ultrapassar 6 caracteres" };
        }

        if (inputCreate.Description.Length > 100)
        {
            return new BaseResponse<InputCreateBrand?>() { Success = false, Message = "A descrição não pode ultrapassar 100 caracteres" };
        }

        return new BaseResponse<InputCreateBrand?> { Success = true, Request = inputCreate };
    }

    #endregion

    #region Validate Update

    public async Task<BaseResponse<InputUpdateBrand?>> ValidateUpdateBrand(InputUpdateBrand inputUpdate)
    {
        var currentBrand = await _brandRepository.GetAsync(inputUpdate.Id);

        if (currentBrand == null)
        {
            return new BaseResponse<InputUpdateBrand?>() { Success = false, Message = "A marca especificada não foi encontrada." };
        }

        var existingCodeBrand = await _brandRepository.GetByCode(inputUpdate.Code);


        if (existingCodeBrand != null && existingCodeBrand.Id != inputUpdate.Id)
        {
            return new BaseResponse<InputUpdateBrand?>() { Success = false, Message = "Já existe uma marca com este código." };
        }

        if (string.IsNullOrEmpty(inputUpdate.Description))
        {
            return new BaseResponse<InputUpdateBrand?>() { Success = false, Message = "A descrição não pode ser vazia." };
        }

        if (inputUpdate.Name.Length > 40)
        {
            return new BaseResponse<InputUpdateBrand?>() { Success = false, Message = "O nome não pode ultrapassar 40 caracteres" };
        }

        if (inputUpdate.Code.Length > 6)
        {
            return new BaseResponse<InputUpdateBrand?>() { Success = false, Message = "O código não pode ultrapassar 6 caracteres" };
        }

        if (inputUpdate.Description.Length > 100)
        {
            return new BaseResponse<InputUpdateBrand?>() { Success = false, Message = "A descrição não pode ultrapassar 100 caracteres" };
        }

        return new BaseResponse<InputUpdateBrand?> { Success = true, Request = inputUpdate};
    }

    #endregion

    #region Validate Delete

    public async Task<BaseResponse<string?>> ValidateDeleteBrand(long id)
    {
        var existingBrand = (await _brandRepository.GetAllAsync())
                            .FirstOrDefault(x => x.Id == id);

        if (existingBrand is null)
        {
            return new BaseResponse<string?> { Success = false, Message = "Não foi encontrado o ID inserido, foi informado corretamente?" };
        }

        var existingProductInBrand = await _productRepository.GetExistingProductInBrand(id);
        if (existingProductInBrand)
        {
            return new BaseResponse<string?> { Success = false, Message = $"Existe Produtos inseridos na marca {existingBrand.Name}, não pode ser deletada" };
        }

        return new BaseResponse<string?> { Success = true, Message = $"A marca {existingBrand.Name} foi deletada com sucesso"};
    }

    #endregion
}