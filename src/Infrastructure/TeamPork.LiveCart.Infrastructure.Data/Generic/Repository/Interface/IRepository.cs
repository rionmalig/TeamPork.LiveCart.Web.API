using TeamPork.LiveCart.Infrastructure.Data.Entities.Abstract;

namespace TeamPork.LiveCart.Infrastructure.Data.Generic.Repository.Interface
{
    public interface IRepository<Entity, Tid> where Entity : PrimaryKey<Tid>
    {
        Task<Entity?> GetByIdAsync(Tid id);
        Task<Entity> AddAsync(Entity entity);
        Task<Entity[]> AddRangeAsync(Entity[] entity);
        Task<int> UpdateAsync(Entity entity);
        Task RemoveAsync(Entity entity);
        Task RemoveRangeAsync(Entity[] entities);
        IQueryable<Entity> AsQueryable();
    }
}
