using ProjetoTeste.Arguments.Arguments.Brand;
using ProjetoTeste.Arguments.Arguments.Response;
using ProjetoTeste.Infrastructure.Conversor;
using ProjetoTeste.Infrastructure.Interface.Repository;
using ProjetoTeste.Infrastructure.Interface.Service;
using ProjetoTeste.Infrastructure.Interface.UnitOfWork;
using ProjetoTeste.Infrastructure.Persistence.Entities;

namespace ProjetoTeste.Infrastructure.Service;

public class BrandService
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

    public async Task<BaseResponse<List<Brand>>> Get(List<long> id)
    {
        var brand = await _brandRepository.GetListByListId(id);

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

    public async Task<BaseResponse<List<OutputBrand>>> Create(List<InputCreateBrand> input)
    {
        var result = await _brandValidateService.ValidateCreateBrand(input);
        if (!result.Success)
            return new BaseResponse<List<OutputBrand>>() { Success = false, Message = result.Message };

        var brand = await _brandRepository.CreateAsync(input.ToListBrand());
        return new BaseResponse<List<OutputBrand>>() { Success = true, Content = brand.ToListOutputBrand() };
    }

    #endregion

    #region Update

    public async Task<BaseResponse<List<OutputBrand>>> Update(List<InputUpdateBrand> inputUpdate)
    {
        var result = await _brandValidateService.ValidateUpdateBrand(inputUpdate);
        if (!result.Success)
        {
            return new BaseResponse<List<OutputBrand>>
            {
                Success = false,
                Message = result.Message
            };
        }

        // termina isso aqui

        var currentBrand = await _brandRepository.GetListByListId(inputUpdate.Select(i => i.Id).ToList());

        foreach (var i in currentBrand)
        {
            i.Name = inputUpdate.Select(i => i.Name).ToString();
            i.Code = inputUpdate.Select(i => i.Code).ToString();
            i.Description = inputUpdate.Select(i => i.Description).ToString();
        } 

        await _brandRepository.Update(currentBrand);

        return new BaseResponse<List<OutputBrand>> { Success = true, Content = currentBrand.ToListOutputBrand() };
    }

    #endregion

    #region Delete

    public async Task<BaseResponse<string>> Delete(List<long> id)
    {
        var result = await _brandValidateService.ValidateDeleteBrand(id);
        if (!result.Success)
        {
            return new BaseResponse<string> { Success = false, Message = result.Message };
        }

        var getBrands = await _brandRepository.GetListByListId(id);

        await _brandRepository.DeleteAsync(getBrands);

        return new BaseResponse<string> { Success = true, Message = result.Message };
    }

    #endregion
}