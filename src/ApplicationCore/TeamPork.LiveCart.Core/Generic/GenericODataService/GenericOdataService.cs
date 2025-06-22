using AutoMapper;
using AutoMapper.QueryableExtensions;
using TeamPork.LiveCart.Core.Generic.GenericODataService.Interface;
using TeamPork.LiveCart.Infrastructure.Data.Entities.Abstract;
using TeamPork.LiveCart.Infrastructure.Data.Generic.Repository.Interface;
namespace TeamPork.LiveCart.Core.Generic.GenericODataService
{
    public class GenericOdataService<TModel, TEntity, TKey> : IGenericOdataService<TModel, TEntity, TKey> where TEntity : PrimaryKey<TKey>
    {
        private readonly IMapper _mapper;
        private readonly IRepository<TEntity, TKey> _repository;

        public GenericOdataService(IMapper mapper, IRepository<TEntity, TKey> repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public IQueryable<TModel> GetODataQuery()
        {
            return _repository.AsQueryable().ProjectTo<TModel>(_mapper.ConfigurationProvider);
        }
    }
}




