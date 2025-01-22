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

    public async Task<BaseResponse<List<BrandValidate?>>> ValidateCreateBrand(List<BrandValidate> inputCreateBrand)
    {
        var response = new BaseResponse<List<BrandValidate?>>();
        var existingCode = await _brandRepository.GetByCode(inputCreateBrand.Select(i => i.InputCreateBrand.Code).ToString());


        for (var i = 0; i < inputCreateBrand.Count; i++)
        {
            if (existingCode != null)
            {
                response.AddErrorMessage("Este código já está em uso!");
                inputCreateBrand[i].SetInvalid();
            }

            if (string.IsNullOrEmpty(inputCreateBrand[i].InputCreateBrand.Code))
            {
                response.AddErrorMessage("O código tem que ser preenchido!");
                inputCreateBrand[i].SetInvalid();
            }

            if (string.IsNullOrEmpty(inputCreateBrand[i].InputCreateBrand.Description))
            {
                response.AddErrorMessage("A descrição tem que ser preenchida!");
                inputCreateBrand[i].SetInvalid();
            }

            if (inputCreateBrand[i].InputCreateBrand.Name.Length > 40)
            {
                response.AddErrorMessage("O nome não pode ultrapassar 40 caracteres.");
                inputCreateBrand[i].SetInvalid();
            }

            if (inputCreateBrand[i].InputCreateBrand.Code.Length > 6)
            {
                response.AddErrorMessage("O código não pode ultrapassar 6 caracteres.");
                inputCreateBrand[i].SetInvalid();
            }

            if (inputCreateBrand[i].InputCreateBrand.Description.Length > 100)
            {
                response.AddErrorMessage("A descrição não pode ultrapassar 100 caracteres.");
                inputCreateBrand[i].SetInvalid();
            }

            if (response.Message.Count > 0)
            {
                response.Success = false;
                inputCreateBrand.Remove(inputCreateBrand[i]);
            }
        }
        response.Content = inputCreateBrand;

        return response;
    }

    #endregion

    #region Validate Update

    public async Task<BaseResponse<List<BrandValidate?>>> ValidateUpdateBrand(List<BrandValidate> listInputIdentityUpdateBrand)
    {
        var response = new BaseResponse<List<BrandValidate?>>();

        var currentBrand = await _brandRepository.GetListByListIdFind(listInputIdentityUpdateBrand.Select(i => i.InputIdentityUpdateBrand.Id).ToList());
        if (currentBrand is null)
            response.AddErrorMessage("As marcas especificas não foram encontradas.");

        for (var i = 0; i < listInputIdentityUpdateBrand.Count; i++)
        {
            var existingCodeBrand = await _brandRepository.GetByCode(listInputIdentityUpdateBrand[i].InputIdentityUpdateBrand.InputUpdateBrand.Code);
            if (currentBrand[i] is null)
            {
                response.AddErrorMessage($"A marca com o ID: {listInputIdentityUpdateBrand[i].InputIdentityUpdateBrand.Id} não foi encontrada.");
                listInputIdentityUpdateBrand[i].SetInvalid();
            }

            if (existingCodeBrand != null && existingCodeBrand.Id != listInputIdentityUpdateBrand.Select(i => i.InputIdentityUpdateBrand.Id).FirstOrDefault(i => i == existingCodeBrand.Id))
            {
                response.AddErrorMessage("Já existe uma marca com este código.");
                listInputIdentityUpdateBrand[i].SetInvalid();
            }

            if (string.IsNullOrEmpty(listInputIdentityUpdateBrand[i].InputIdentityUpdateBrand.InputUpdateBrand.Description))
            {
                response.AddErrorMessage("A descrição não pode ser vazia.");
                listInputIdentityUpdateBrand[i].SetInvalid();
            }

            if (listInputIdentityUpdateBrand[i].InputIdentityUpdateBrand.InputUpdateBrand.Name.Length > 40)
            {
                response.AddErrorMessage("O nome não pode ultrapassar 40 caracteres");
                listInputIdentityUpdateBrand[i].SetInvalid();
            }

            if (listInputIdentityUpdateBrand[i].InputIdentityUpdateBrand.InputUpdateBrand.Code.Length > 6)
            {
                response.AddErrorMessage("O código não pode ultrapassar 6 caracteres");
                listInputIdentityUpdateBrand[i].SetInvalid();
            }

            if (listInputIdentityUpdateBrand[i].InputIdentityUpdateBrand.InputUpdateBrand.Description.Length > 100)
            {
                response.AddErrorMessage("A descrição não pode ultrapassar 100 caracteres");
                listInputIdentityUpdateBrand[i].SetInvalid();
            }

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