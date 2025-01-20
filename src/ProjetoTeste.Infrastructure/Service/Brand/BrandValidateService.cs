using ProjetoTeste.Arguments.Arguments.Brand;
using ProjetoTeste.Arguments.Arguments.Response;
using ProjetoTeste.Infrastructure.Conversor;
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

    public async Task<BaseResponse<List<OutputBrand?>>> ValidateCreateBrand(List<InputCreateBrand> inputCreate)
    {
        var response = new BaseResponse<List<OutputBrand?>>(); 
        var existingCode = await _brandRepository.GetByCode(inputCreate.Select(i => i.Code).ToString());

        if (existingCode != null)
            response.AddErrorMessage("Este código já está em uso!");

        if (string.IsNullOrEmpty(inputCreate.Select(i => i.Code).ToString()))
            response.AddErrorMessage("O código tem que ser preenchido!");

        if (string.IsNullOrEmpty(inputCreate.Select(i => i.Description).ToString()))
            response.AddErrorMessage("A descrição tem que ser preenchida!");

        if (inputCreate.Select(i => i.Name).ToString().Length > 40)
            response.AddErrorMessage("O nome não pode ultrapassar 40 caracteres.");

        if (inputCreate.Select(i => i.Code).ToString().Length > 6)
            response.AddErrorMessage("O código não pode ultrapassar 6 caracteres.");

        if (inputCreate.Select(i => i.Description).ToString().Length > 100)
            response.AddErrorMessage("A descrição não pode ultrapassar 100 caracteres.");

        if (response.Message.Count > 0)
            response.Success = false;

        return response;
    }

    #endregion

    #region Validate Update

    public async Task<BaseResponse<List<OutputBrand?>>> ValidateUpdateBrand(List<InputUpdateBrand> listInputUpdateBrand)
    {
        var response = new BaseResponse<List<OutputBrand?>>();

        var currentBrand = await _brandRepository.GetListByListId(listInputUpdateBrand.Select(i => i.Id).ToList());

        if (currentBrand == null)
            response.AddErrorMessage("A marca especificada não foi encontrada.");

        var existingCodeBrand = await _brandRepository.GetByCode(listInputUpdateBrand.Select(i => i.Code).ToString());

        if (existingCodeBrand != null && existingCodeBrand.Id != listInputUpdateBrand.Select(i => i.Id).FirstOrDefault(i => i == existingCodeBrand.Id))
            response.AddErrorMessage("Já existe uma marca com este código.");

        if (string.IsNullOrEmpty(listInputUpdateBrand.Select(i => i.Description).ToString()))
            response.AddErrorMessage("A descrição não pode ser vazia.");

        if (listInputUpdateBrand.Select(i => i.Name).ToString().Length > 40)
            response.AddErrorMessage("O nome não pode ultrapassar 40 caracteres");

        if (listInputUpdateBrand.Select(i => i.Code).ToString().Length > 6)
            response.AddErrorMessage("O código não pode ultrapassar 6 caracteres");

        if (listInputUpdateBrand.Select(i => i.Description).ToString().Length > 100)
            response.AddErrorMessage("A descrição não pode ultrapassar 100 caracteres");

        if (response.Message.Count > 0)
            response.Success = false;

        return response;
    }

    #endregion

    #region Validate Delete

    public async Task<BaseResponse<List<string?>>> ValidateDeleteBrand(List<long> id)
    {
        var response = new BaseResponse<List<string?>>();

        var existingBrand = (await _brandRepository.GetListByListId(id));

        foreach (var item in existingBrand)
        {
            if (item is null)
                response.AddErrorMessage($"Não foi encontrado o ID {item}, foi informado corretamente?");
        }

        foreach (var item in existingBrand)
        {
            var existingProductInBrand = await _productRepository.GetExistingProductInBrand(item.Id);
            if (existingProductInBrand)
                response.AddErrorMessage($"Existe Produtos inseridos na marca {existingBrand.Select(i => i.Name)}, não pode ser deletada");
        }

        if (response.Message.Count > 0)
            response.Success = false;
        else
            response.AddSuccessMessage($"A marca {existingBrand.Select(i => i.Name)} foi deletada com sucesso");

        return response;
    }

    #endregion
}