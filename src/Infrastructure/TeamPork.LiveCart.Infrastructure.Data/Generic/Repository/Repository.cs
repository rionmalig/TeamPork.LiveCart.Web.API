using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamPork.LiveCart.Infrastructure.Data.Context.SQLContext;
using TeamPork.LiveCart.Infrastructure.Data.Entities.Abstract;
using TeamPork.LiveCart.Infrastructure.Data.Generic.Repository.Interface;

namespace TeamPork.LiveCart.Infrastructure.Data.Generic.Repository
{
    public class Repository<Entity, Tid> : IRepository<Entity, Tid> where Entity : PrimaryKey<Tid>
    {
        public AppDbContext _dbContext { get; }

        public Repository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public virtual async Task<Entity?> GetByIdAsync(Tid id)
        {
            return await _dbContext.Set<Entity>().FindAsync(id);
        }

        public async Task<Entity> AddAsync(Entity entity)
        {

            _dbContext.Set<Entity>().Add(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<Entity[]> AddRangeAsync(Entity[] entity)
        {
            _dbContext.Set<Entity>().AddRange(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task RemoveAsync(Entity entity)
        {
            _dbContext.Set<Entity>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task RemoveRangeAsync(Entity[] entities)
        {
            _dbContext.Set<Entity>().RemoveRange(entities);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<int> UpdateAsync(Entity entity)
        {
            _dbContext.Set<Entity>().Update(entity);
            return await _dbContext.SaveChangesAsync();
        }

        public virtual IQueryable<Entity> AsQueryable()
        {
            return _dbContext.Set<Entity>().AsNoTracking().AsSplitQuery();
        }
    }
}
