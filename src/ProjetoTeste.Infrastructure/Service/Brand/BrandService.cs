using ProjetoTeste.Arguments.Arguments.Brand;
using ProjetoTeste.Arguments.Arguments.Response;
using ProjetoTeste.Infrastructure.Conversor;
using ProjetoTeste.Infrastructure.Interface.Repository;
using ProjetoTeste.Infrastructure.Interface.Service;
using ProjetoTeste.Infrastructure.Interface.UnitOfWork;
using ProjetoTeste.Infrastructure.Persistence.Entities;

namespace ProjetoTeste.Infrastructure.Service;

public class BrandService : IBrandService
{
    #region Dependency Injection

    private readonly IProductRepository _productRepository;
    private readonly IBrandRepository _brandRepository;
    private readonly IBrandValidateService _brandValidateService;
    private readonly IUnitOfWork _unitOfWork;
    public BrandService(IBrandRepository brandRepository, IProductRepository productRepository, IUnitOfWork unitOfWork, IBrandValidateService brandValidateService)
    {
        _brandRepository = brandRepository;
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
        _brandValidateService = brandValidateService;
    }

    #endregion

    #region Get

    public async Task<BaseResponse<List<Brand>>> Get(List<InputIdentityViewBrand> listInputIdentityViewBrand)
    {
        var brand = await _brandRepository.GetListByListIdWhere(listInputIdentityViewBrand.Select(i => i.Id).ToList());

        return new BaseResponse<List<Brand>>
        {
            Success = true,
            Content = brand
        };
    }

    public async Task<List<Brand>> GetAll()
    {
        return await _brandRepository.GetAllAsync();
    }

    #endregion

    #region Create

    public async Task<BaseResponse<List<OutputBrand>>> Create(List<InputCreateBrand> listInputCreateBrand)
    {
        var response = new BaseResponse<List<OutputBrand>>();

        var listRepeatedCode = (from i in listInputCreateBrand
                                where listInputCreateBrand.Count(j => j.Code == i.Code) > 1
                                select i.Code).ToList();

        var listExistingBrand = (await _brandRepository.GetListByCode(listInputCreateBrand.Select(i => i.Code).ToList())).Select(i => i.Code).ToList();

        var listCreate = (from i in listInputCreateBrand
                          select new
                          {
                              InputCreate = i,
                              RepeatedCode = listRepeatedCode.FirstOrDefault(j => i.Code == j),
                              ExistingCode = listExistingBrand.FirstOrDefault(j => i.Code == j)
                          }).ToList();

        List<BrandValidate> listBrandValidate = listCreate.Select(i => new BrandValidate().CreateValidate(i.InputCreate, i.RepeatedCode, i.ExistingCode)).ToList();

        var result = await _brandValidateService.ValidateCreateBrand(listBrandValidate);
        response.Success = result.Success;
        response.Message = result.Message;

        if (!response.Success)
            return response;

        var listNewBrand = (from i in result.Content
                            select new Brand(i.InputCreateBrand.Name, i.InputCreateBrand.Code, i.InputCreateBrand.Description)).ToList();

        var brand = await _brandRepository.CreateAsync(listNewBrand);
        response.Content = brand.ToListOutputBrand();
        return response;
    }

    #endregion

    #region Update

    public async Task<BaseResponse<List<OutputBrand>>> Update(List<InputIdentityUpdateBrand> listInputIdentityUpdateBrand)
    {
        var response = new BaseResponse<List<OutputBrand>>();

        var listRepeatedCode = (from i in listInputIdentityUpdateBrand
                                where listInputIdentityUpdateBrand.Count(j => j.InputUpdateBrand.Code == i.InputUpdateBrand.Code) > 1
                                select i.InputUpdateBrand.Code).ToList();

        var listRepeatedId = (from i in listInputIdentityUpdateBrand
                              where listInputIdentityUpdateBrand.Count(j => j.Id == i.Id) > 1
                              select i.Id).ToList();

        var listExistingCode = (await _brandRepository.GetListByCode(listInputIdentityUpdateBrand.Select(i => i.InputUpdateBrand.Code).ToList())).Select(i => i.Code).ToList();

        var currentBrand = await _brandRepository.GetListByListIdWhere(listInputIdentityUpdateBrand.Select(i => i.Id).ToList());
        var selectedCurrentBrandById = currentBrand.Select(i => i.Id).ToList();

        var listUpdate = from i in listInputIdentityUpdateBrand
                         select new
                         {
                             inputUpdate = i,
                             RepeatedCode = listRepeatedCode.FirstOrDefault(j => i.InputUpdateBrand.Code == j),
                             RepeatedId = listRepeatedId.FirstOrDefault(j => i.Id == j),
                             ExistingCode = listExistingCode.FirstOrDefault(j => i.InputUpdateBrand.Code == j),
                             CurrentBrand = selectedCurrentBrandById.FirstOrDefault(j => i.Id == j)
                         };

        List<BrandValidate> listBrandValidate = listUpdate.Select(i => new BrandValidate().UpdateValidate(i.inputUpdate, i.RepeatedCode, i.ExistingCode, i.CurrentBrand, i.RepeatedId)).ToList();

        var result = await _brandValidateService.ValidateUpdateBrand(listBrandValidate);
        response.Success = result.Success;
        response.Message = result.Message;
        if (!response.Success)
        {
            return response;
        }

        var updatedBrand = (from i in result.Content
                            from j in currentBrand
                            where i.InputIdentityUpdateBrand.Id == j.Id
                            let name = j.Name = i.InputIdentityUpdateBrand.InputUpdateBrand.Name
                            let code = j.Code = i.InputIdentityUpdateBrand.InputUpdateBrand.Code
                            let description = j.Description = i.InputIdentityUpdateBrand.InputUpdateBrand.Description
                            let message = response.AddSuccessMessage($"A marca {i.InputIdentityUpdateBrand.InputUpdateBrand.Name} foi atualizada com sucesso!")
                            select j).ToList();

        await _brandRepository.Update(updatedBrand);
        response.Content = updatedBrand.ToListOutputBrand();
        return response;
    }

    #endregion

    #region Delete

    public async Task<BaseResponse<bool>> Delete(List<InputIdentityDeleteBrand> listInputIdentityDeleteBrand)
    {
        var response = new BaseResponse<bool>();

        var existingBrand = await _brandRepository.GetListByListIdWhere(listInputIdentityDeleteBrand.Select(i => i.Id).ToList());

        var existingProductInBrand = _productRepository.GetExistingProductInBrand(listInputIdentityDeleteBrand.Select(i => i.Id).ToList());

        var listDelete = (from i in listInputIdentityDeleteBrand
                          select new
                          {
                              InputDelete = i,
                              ExistingBrand = existingBrand.FirstOrDefault(j => i.Id == j.Id).Id,
                              ExistingProductInBrand = existingProductInBrand.FirstOrDefault(j => i.Id == j)
                          }).ToList();

        List<BrandValidate> listDeleteValidate = listDelete.Select(i => new BrandValidate().DeleteValidate(i.InputDelete, i.ExistingBrand, i.ExistingProductInBrand)).ToList();

        var result = await _brandValidateService.ValidateDeleteBrand(listDeleteValidate);
        response.Success = result.Success;
        response.Message = result.Message;
        if (!response.Success)
        {
            return response;
        }

        var deletedBrand = (from i in result.Content
                            from j in existingBrand
                            where j.Id == i.ExistingBrand
                            let message = response.AddSuccessMessage($"A marca {j.Name} foi deletada com sucesso!")
                            select j).ToList();

        response.Content = await _brandRepository.DeleteAsync(deletedBrand);

        return response;
    }
}
#endregion