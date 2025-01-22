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
        List<BrandValidate> listBrandValidate = listInputCreateBrand.Select(i => new BrandValidate().CreateValidate(i)).ToList();

        var result = await _brandValidateService.ValidateCreateBrand(listBrandValidate);
        response.Success = result.Success;
        response.Message = result.Message;

        if (!response.Success)
            return response;

        var listNewBrand = (from i in result.Content
                            select new Brand(i.InputCreateBrand.Name, i.InputCreateBrand.Code, i.InputCreateBrand.Description)).ToList();

        var brand = await _brandRepository.CreateAsync(listNewBrand);
        return new BaseResponse<List<OutputBrand>>() { Success = true, Content = brand.ToListOutputBrand() };
    }

    #endregion

    #region Update

    //public async Task<BaseResponse<List<OutputBrand>>> Update(List<InputIdentityUpdateBrand> listInputIdentityUpdateBrand)
    //{
    //    var response = new BaseResponse<List<OutputBrand>>();
    //    var result = await _brandValidateService.ValidateUpdateBrand(listInputIdentityUpdateBrand);
    //    response.Success = result.Success;
    //    response.Message = result.Message;
    //    if (!response.Success)
    //    {
    //        return response;
    //    }

    //    // termina isso aqui

    //    var currentBrand = await _brandRepository.GetListByListIdWhere(listInputIdentityUpdateBrand.Select(i => i.Id).ToList());

    //    foreach (var i in currentBrand)
    //    {
    //        i.Name = result.Content.Select(i => i.InputUpdateBrand.Name).ToString();
    //        i.Code = result.Content.Select(i => i.InputUpdateBrand.Code).ToString();
    //        i.Description = result.Content.Select(i => i.InputUpdateBrand.Description).ToString();
    //        response.AddSuccessMessage($"A marca com o nome: {i.Name} foi atualizada com sucesso!");
    //    } 

    //    await _brandRepository.Update(currentBrand);
    //    response.Content = currentBrand.ToListOutputBrand();
    //    return response;
    //}

    #endregion

    #region Delete

    public async Task<BaseResponse<string>> Delete(List<InputIdentityDeleteBrand> listInputIdentityDeleteBrand)
    {
        var result = await _brandValidateService.ValidateDeleteBrand(listInputIdentityDeleteBrand);
        if (!result.Success)
        {
            return new BaseResponse<string> { Success = false, Message = result.Message };
        }

        var getBrands = await _brandRepository.GetListByListIdWhere(listInputIdentityDeleteBrand.Select(i => i.Id).ToList());

        await _brandRepository.DeleteAsync(getBrands);

        return new BaseResponse<string> { Success = true, Message = result.Message };
    }

    public Task<BaseResponse<List<OutputBrand>>> Update(List<InputIdentityUpdateBrand> input)
    {
        throw new NotImplementedException();
    }

    #endregion
}