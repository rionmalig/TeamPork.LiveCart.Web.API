namespace TeamPork.LiveCart.Core.Generic.GenericODataService.Interface
{
    public interface IGenericOdataService<TModel, TEntity, TKey>
    {
        IQueryable<TModel> GetODataQuery();
    }
}
