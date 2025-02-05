using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ProjetoTeste.Arguments.Arguments.Base.Inputs;
using ProjetoTeste.Arguments.Arguments.Base.Inputs.Interfaces;
using ProjetoTeste.Arguments.Arguments.Base.Outputs;
using ProjetoTeste.Arguments.Arguments.Response;
using ProjetoTeste.Infrastructure.Interface.Base;
using ProjetoTeste.Infrastructure.Interface.UnitOfWork;
using ProjetoTeste.Infrastructure.Persistence.Entities;

namespace ProjetoTeste.Api.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class BaseController<TService, TEntity, TInputIdentityView, TInputCreate, TInputIdentityUpdate, TInputIdentityDelete, TOutput> : Controller
    where TService : IBaseService<TInputIdentityView, TInputCreate, TInputIdentityUpdate, TInputIdentityDelete, TOutput>
    where TEntity : BaseEntity, new()
    where TInputIdentityView : BaseInputIdentityView<TInputIdentityView>, IBaseIdentityView
    where TInputCreate : BaseInputCreate<TInputCreate>
    where TInputIdentityUpdate : BaseInputIdentityUpdate<TInputIdentityUpdate>
    where TInputIdentityDelete : BaseInputIdentityDelete<TInputIdentityDelete>
    where TOutput : BaseOutput<TOutput>
{
    private readonly TService _service;
    private readonly IUnitOfWork unitOfWork;

    public BaseController(IUnitOfWork unitOfWork, TService service)
    {
        this.unitOfWork = unitOfWork;
        _service = service;
    }

    #region Commits

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        unitOfWork.BeginTransaction();
        base.OnActionExecuting(context);
    }

    public override void OnActionExecuted(ActionExecutedContext context)
    {
        unitOfWork.Commit();
        base.OnActionExecuted(context);
    }

    #endregion

    #region Get

    [HttpPost("GetBySingleId")]
    public virtual async Task<ActionResult<TOutput>> GetId(TInputIdentityView inputIdentityView)
    {
        return Ok(await _service.GetSingle(inputIdentityView));
    }

    [HttpGet]
    public virtual async Task<ActionResult<List<TOutput>>> GetAll()
    {
        return Ok(await _service.GetAll());
    }

    [HttpPost("GetByListId")]
    public virtual async Task<ActionResult<TOutput>> GetId(List<TInputIdentityView> listInputIdentityView)
    {
        return Ok(await _service.Get(listInputIdentityView));
    }

    #endregion

    #region Post

    [HttpPost("Create/Single")]
    public virtual async Task<ActionResult<BaseResponse<TOutput>>> CreateSingle(TInputCreate inputCreate)
    {
        var result = await _service.CreateSingle(inputCreate);

        if (!result.Success)
            return BadRequest(result);

        return Ok(result);
    }

    [HttpPost("Create/Multiple")]
    public virtual async Task<ActionResult<BaseResponse<TOutput>>> Create(List<TInputCreate> listInputCreate)
    {
        var result = await _service.Create(listInputCreate);

        if (!result.Success)
            return BadRequest(result);

        return Ok(result);
    }

    #endregion

    #region Put

    [HttpPut("Single")]
    public virtual async Task<ActionResult<BaseResponse<TOutput>>> UpdateSingle(TInputIdentityUpdate inputIdentityUpdate)
    {
        var result = await _service.UpdateSingle(inputIdentityUpdate);

        if (!result.Success)
            return BadRequest(result);

        return Ok(result);
    }

    [HttpPut("Multiple")]
    public virtual async Task<ActionResult<BaseResponse<List<TOutput>>>> Update(List<TInputIdentityUpdate> listInputIdentityUpdate)
    {
        var result = await _service.Update(listInputIdentityUpdate);

        if (!result.Success)
            return BadRequest(result);

        return Ok(result);
    }

    #endregion

    #region Delete

    [HttpDelete("Single")]
    public virtual async Task<ActionResult<BaseResponse<string>>> DeleteSingle(TInputIdentityDelete inputIdentityDelete)
    {
        var result = await _service.DeleteSingle(inputIdentityDelete);

        if (!result.Success)
            return BadRequest(result);

        return Ok(result);
    }

    [HttpDelete("Multiple")]
    public virtual async Task<ActionResult<BaseResponse<string>>> Delete(List<TInputIdentityDelete> listInputIdentityDelete)
    {
        var result = await _service.Delete(listInputIdentityDelete);

        if (!result.Success)
            return BadRequest(result);

        return Ok(result);
    }

    #endregion
}