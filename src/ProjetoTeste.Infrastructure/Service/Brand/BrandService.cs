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

    public async Task<BaseResponse<Brand>> Get(long id)
    {
        var brand = await _brandRepository.GetAsync(id);

        return new BaseResponse<Brand>
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

    public async Task<BaseResponse<OutputBrand>> Create(InputCreateBrand input)
    {
        var result = await _brandValidateService.ValidateCreateBrand(input);
        if (!result.Success)
            return new BaseResponse<OutputBrand>() { Success = false, Message = result.Message };

        var brand = await _brandRepository.CreateAsync(input.ToBrand());
        return new BaseResponse<OutputBrand>() { Success = true, Content = brand.ToOutputBrand() };
    }

    #endregion

    #region Update

    public async Task<BaseResponse<OutputBrand>> Update(InputUpdateBrand inputUpdate)
    {
        var result = await _brandValidateService.ValidateUpdateBrand(inputUpdate);
        var currentBrand = await _brandRepository.GetAsync(inputUpdate.Id);

        if (!result.Success)
        {
            return new BaseResponse<OutputBrand>
            {
                Success = false,
                Message = result.Message
            };
        }

        currentBrand.Name = inputUpdate.Name;
        currentBrand.Code = inputUpdate.Code;
        currentBrand.Description = inputUpdate.Description;

        await _brandRepository.Update(currentBrand);

        return new BaseResponse<OutputBrand> { Success = true, Content = currentBrand.ToOutputBrand() };
    }

    #endregion

    #region Delete

    public async Task<BaseResponse<string>> Delete(long id)
    {
        var result = await _brandValidateService.ValidateDeleteBrand(id);
        if (!result.Success)
        {
            return new BaseResponse<string> { Success = false, Message = result.Message };
        }

        await _brandRepository.DeleteAsync(id);

        return new BaseResponse<string> { Success = true, Message = result.Message };
    }

    #endregion
}