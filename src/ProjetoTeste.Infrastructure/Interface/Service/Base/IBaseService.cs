using ProjetoTeste.Arguments.Arguments.Response;

namespace ProjetoTeste.Infrastructure.Interface.Base
{
    public interface IBaseService<TInputIdentityView, TInputCreate, TInputIdentityUpdate, TInputIdentityDelete, TOutput>
    {
        Task<List<TOutput>> GetAll();
        Task<List<TOutput>> Get(List<TInputIdentityView> listInputIdentityView);
        Task<BaseResponse<List<TOutput>>> Create(List<TInputCreate> listInputCreate);
        Task<BaseResponse<List<TOutput>>> Update(List<TInputIdentityUpdate> inputIdentityUpdate);
        Task<BaseResponse<bool>> Delete(List<TInputIdentityDelete> listInputIdentityDelete);
        Task<TOutput> GetSingle(TInputIdentityView inputIdentityView);
        Task<BaseResponse<TOutput>> CreateSingle(TInputCreate inputCreate);
        Task<BaseResponse<TOutput>> UpdateSingle(TInputIdentityUpdate inputIdentityUpdate);
        Task<BaseResponse<bool>> DeleteSingle(TInputIdentityDelete inputIdentityDelete);
    }
}
