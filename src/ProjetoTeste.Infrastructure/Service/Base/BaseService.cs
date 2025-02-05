using AutoMapper;
using ProjetoTeste.Arguments.Arguments.Base.Inputs;
using ProjetoTeste.Arguments.Arguments.Base.Inputs.Interfaces;
using ProjetoTeste.Arguments.Arguments.Base.Outputs;
using ProjetoTeste.Arguments.Arguments.Response;
using ProjetoTeste.Infrastructure.Interface.Base;
using ProjetoTeste.Infrastructure.Interface.Repository;
using ProjetoTeste.Infrastructure.Persistence.Entities;

namespace ProjetoTeste.Infrastructure.Service.Base
{
    public abstract class BaseService<TRepository, TEntity, TInputIdentityView, TInputCreate, TInputIdentityUpdate, TInputIdentityDelete, TOutput, TDTO> : IBaseService<TInputIdentityView, TInputCreate, TInputIdentityUpdate, TInputIdentityDelete, TOutput>
        where TEntity : BaseEntity, new()
        where TInputIdentityView : BaseInputIdentityView<TInputIdentityView>, IBaseIdentityView
        where TInputCreate : BaseInputCreate<TInputCreate>
        where TInputIdentityUpdate : BaseInputIdentityUpdate<TInputIdentityUpdate>
        where TInputIdentityDelete : BaseInputIdentityDelete<TInputIdentityDelete>
        where TOutput : BaseOutput<TOutput>
        where TRepository : IRepository<TEntity>
    {
        #region Dependency Injection

        private readonly TRepository _repository;
        private readonly IMapper _mapper;

        protected BaseService(TRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        #endregion

        #region Get

        public virtual async Task<List<TOutput>> GetAll()
        {
            var getAll = await _repository.GetAllAsync();
            return _mapper.Map<List<TOutput>>(getAll);
        }

        public virtual async Task<List<TOutput>> Get(List<TInputIdentityView> listInputIdentityView)
        {
            var getListByListId = await _repository.GetListByListIdWhere(listInputIdentityView.Select(i => i.Id).ToList());
            return _mapper.Map<List<TOutput>>(getListByListId);
        }

        public virtual async Task<TOutput> GetSingle(TInputIdentityView inputIdentityView)
        {
            var getId = await _repository.GetById(inputIdentityView.Id);
            return _mapper.Map<TOutput>(getId);
        }

        #endregion

        #region Create

        public virtual Task<BaseResponse<List<TOutput>>> Create(List<TInputCreate> listInputCreate)
        {
            throw new NotImplementedException();
        }

        public virtual async Task<BaseResponse<TOutput>> CreateSingle(TInputCreate inputCreate)
        {
            var response = new BaseResponse<TOutput>();

            var result = await Create([inputCreate]);
            response.Success = result.Success;
            response.Message = result.Message;

            if (!response.Success)
                return response;

            response.Content = result.Content.FirstOrDefault();

            return response;
        }

        #endregion

        #region Update
        public virtual Task<BaseResponse<List<TOutput>>> Update(List<TInputIdentityUpdate> inputIdentityUpdate)
        {
            throw new NotImplementedException();
        }

        public virtual async Task<BaseResponse<TOutput>> UpdateSingle(TInputIdentityUpdate inputIdentityUpdate)
        {
            var response = new BaseResponse<TOutput>();

            var result = await Update([inputIdentityUpdate]);
            response.Success = result.Success;
            response.Message = result.Message;

            if (!response.Success)
                return response;

            response.Content = result.Content.FirstOrDefault();

            return response;
        }

        #endregion

        #region Delete

        public virtual async Task<BaseResponse<bool>> Delete(List<TInputIdentityDelete> listInputIdentityDelete)
        {
            throw new NotImplementedException();
        }

        public virtual async Task<BaseResponse<bool>> DeleteSingle(TInputIdentityDelete inputIdentityDelete)
        {
            return await Delete([inputIdentityDelete]);
        }

        #endregion
    }
}
