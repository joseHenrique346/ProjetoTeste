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

    public async Task<Response<Brand>> Get(long id)
    {
        var brand = await _brandRepository.GetAsync(id);

        return new Response<Brand>
        {
            Success = true,
            Request = brand
        };
    }

    public async Task<List<Brand>> GetAll()
    {
        return await _brandRepository.GetAllAsync();
    }

    public async Task<Response<Brand>> Create(InputCreateBrand input)
    {
        var result = await _brandValidateService.ValidateCreateBrand(input);
        if (!result.Success)
        {
            return new Response<Brand>()
            {
                Success = false,
                Message = result.Message
            };
        }

        var brand = await _brandRepository.CreateAsync(input.ToBrand());
        return new Response<Brand>()
        {
            Success = true,
            Request = brand
        };
    }

    public async Task<Response<Brand>> Update(int id, InputUpdateBrand input)
    {
        var result = await _brandValidateService.ValidateUpdateBrand(id, input);
        var currentBrand = await _brandRepository.GetAsync(id);

        if (!result.Success)
        {
            return new Response<Brand>
            {
                Success = false,
                Message = result.Message
            };
        }

        currentBrand.Name = input.Name;
        currentBrand.Code = input.Code;
        currentBrand.Description = input.Description;

        await _brandRepository.Update(currentBrand);

        return new Response<Brand>
        {
            Success = true,
            Request = currentBrand
        };
    }

    public async Task<Response<bool>> Delete(long id)
    {
        var result = await _brandValidateService.ValidateDeleteBrand(id);
        if (!result.Success)
        {
            return new Response<bool>
            {
                Success = false,
                Message = result.Message
            };
        }

        var brand = await _brandRepository.GetAsync(id);
        if (brand == null)
        {
            return new Response<bool>
            {
                Success = false,
                Message = "Marca não encontrada."
            };
        }

        await _brandRepository.DeleteAsync(id);

        return new Response<bool>
        {
            Success = true,
            Request = true
        };
    }
}