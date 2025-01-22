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

    public async Task<BaseResponse<List<OutputBrand?>>> ValidateCreateBrand(List<InputCreateBrand> inputCreateBrand)
    {
        var response = new BaseResponse<List<OutputBrand?>>();
        var existingCode = await _brandRepository.GetByCode(inputCreateBrand.Select(i => i.Code).ToString());

        if (existingCode != null)
            response.AddErrorMessage("Este código já está em uso!");

        for (var i = 0; i < inputCreateBrand.Count; i++)
        {
            if (string.IsNullOrEmpty(inputCreateBrand[i].Code))
                response.AddErrorMessage("O código tem que ser preenchido!");

            if (string.IsNullOrEmpty(inputCreateBrand[i].Description))
                response.AddErrorMessage("A descrição tem que ser preenchida!");

            if (inputCreateBrand[i].Name.Length > 40)
                response.AddErrorMessage("O nome não pode ultrapassar 40 caracteres.");

            if (inputCreateBrand[i].Code.Length > 6)
                response.AddErrorMessage("O código não pode ultrapassar 6 caracteres.");

            if (inputCreateBrand[i].Description.Length > 100)
                response.AddErrorMessage("A descrição não pode ultrapassar 100 caracteres.");

            if (response.Message.Count > 0)
            {
                response.Success = false;
                inputCreateBrand.Remove(inputCreateBrand[i]);
            }
        }

        return response;
    }

    #endregion

    #region Validate Update

    public async Task<BaseResponse<List<InputIdentityUpdateBrand?>>> ValidateUpdateBrand(List<InputIdentityUpdateBrand> listInputIdentityUpdateBrand)
    {
        var response = new BaseResponse<List<InputIdentityUpdateBrand?>>();

        var currentBrand = await _brandRepository.GetListByListIdFind(listInputIdentityUpdateBrand.Select(i => i.Id).ToList());
        if (currentBrand is null)
            response.AddErrorMessage("As marcas especificas não foram encontradas.");

        for (var i = 0; i < listInputIdentityUpdateBrand.Count; i++)
        {
            var existingCodeBrand = await _brandRepository.GetByCode(listInputIdentityUpdateBrand[i].InputUpdateBrand.Code);
            if (currentBrand[i] is null)
                response.AddErrorMessage($"A marca com o ID: {listInputIdentityUpdateBrand[i].Id} não foi encontrada.");

            if (existingCodeBrand != null && existingCodeBrand.Id != listInputIdentityUpdateBrand.Select(i => i.Id).FirstOrDefault(i => i == existingCodeBrand.Id))
                response.AddErrorMessage("Já existe uma marca com este código.");

            if (string.IsNullOrEmpty(listInputIdentityUpdateBrand[i].InputUpdateBrand.Description))
                response.AddErrorMessage("A descrição não pode ser vazia.");

            if (listInputIdentityUpdateBrand[i].InputUpdateBrand.Name.Length > 40)
                response.AddErrorMessage("O nome não pode ultrapassar 40 caracteres");

            if (listInputIdentityUpdateBrand[i].InputUpdateBrand.Code.Length > 6)
                response.AddErrorMessage("O código não pode ultrapassar 6 caracteres");

            if (listInputIdentityUpdateBrand[i].InputUpdateBrand.Description.Length > 100)
                response.AddErrorMessage("A descrição não pode ultrapassar 100 caracteres");

            if (response.Message.Count > 0)
            {
                listInputIdentityUpdateBrand[i] = null;
            }
        }
        for (var i = 0; i < listInputIdentityUpdateBrand.Count; i++)
        {
            if (listInputIdentityUpdateBrand[i] is null)
            {
                listInputIdentityUpdateBrand.Remove(listInputIdentityUpdateBrand[i]);
            }
        }

        if (!listInputIdentityUpdateBrand.Any())
        {
            response.Success = false;
        }

        response.Content = listInputIdentityUpdateBrand;
        return response;
    }

    #endregion

    #region Validate Delete

    public async Task<BaseResponse<List<string?>>> ValidateDeleteBrand(List<InputIdentityDeleteBrand> listInputIdentityDeleteBrand)
    {
        var response = new BaseResponse<List<string?>>();

        var existingBrand = (await _brandRepository.GetListByListIdWhere(listInputIdentityDeleteBrand.Select(i => i.Id).ToList()));

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