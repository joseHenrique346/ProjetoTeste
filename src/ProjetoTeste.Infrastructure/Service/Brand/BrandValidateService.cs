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
        var response = new BaseResponse<InputCreateBrand?>(); 
        var existingCode = await _brandRepository.GetByCode(inputCreate.Code);

        if (existingCode != null)
            response.AddErrorMessage("Este código já está em uso!");

        if (string.IsNullOrEmpty(inputCreate.Code))
            response.AddErrorMessage("O código tem que ser preenchido!");

        if (string.IsNullOrEmpty(inputCreate.Description))
            response.AddErrorMessage("A descrição tem que ser preenchida!");

        if (inputCreate.Name.Length > 40)
            response.AddErrorMessage("O nome não pode ultrapassar 40 caracteres.");

        if (inputCreate.Code.Length > 6)
            response.AddErrorMessage("O código não pode ultrapassar 6 caracteres.");

        if (inputCreate.Description.Length > 100)
            response.AddErrorMessage("A descrição não pode ultrapassar 100 caracteres.");

        if (response.Message.Count > 0)
            response.Success = false;

        return response;
    }

    #endregion

    #region Validate Update

    public async Task<BaseResponse<InputUpdateBrand?>> ValidateUpdateBrand(InputUpdateBrand inputUpdate)
    {
        var response = new BaseResponse<InputUpdateBrand?>();

        var currentBrand = await _brandRepository.GetAsync(inputUpdate.Id);

        if (currentBrand == null)
            response.AddErrorMessage("A marca especificada não foi encontrada.");

        var existingCodeBrand = await _brandRepository.GetByCode(inputUpdate.Code);


        if (existingCodeBrand != null && existingCodeBrand.Id != inputUpdate.Id)
            response.AddErrorMessage("Já existe uma marca com este código.");

        if (string.IsNullOrEmpty(inputUpdate.Description))
            response.AddErrorMessage("A descrição não pode ser vazia.");

        if (inputUpdate.Name.Length > 40)
            response.AddErrorMessage("O nome não pode ultrapassar 40 caracteres");

        if (inputUpdate.Code.Length > 6)
            response.AddErrorMessage("O código não pode ultrapassar 6 caracteres");

        if (inputUpdate.Description.Length > 100)
            response.AddErrorMessage("A descrição não pode ultrapassar 100 caracteres");

        if (response.Message.Count > 0)
            response.Success = false;

        return response;
    }

    #endregion

    #region Validate Delete

    public async Task<BaseResponse<string?>> ValidateDeleteBrand(long id)
    {
        var response = new BaseResponse<string?>();

        var existingBrand = (await _brandRepository.GetAllAsync())
                            .FirstOrDefault(x => x.Id == id);

        if (existingBrand is null)
            response.AddErrorMessage("Não foi encontrado o ID inserido, foi informado corretamente?");

        var existingProductInBrand = await _productRepository.GetExistingProductInBrand(id);
        if (existingProductInBrand)
            response.AddErrorMessage($"Existe Produtos inseridos na marca {existingBrand.Name}, não pode ser deletada");

        if (response.Message.Count > 0)
            response.Success = false;
        else
            response.AddSuccessMessage($"A marca {existingBrand.Name} foi deletada com sucesso");

        return response;
    }

    #endregion
}