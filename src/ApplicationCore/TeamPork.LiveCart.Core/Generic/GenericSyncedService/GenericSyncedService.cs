using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using TeamPork.LiveCart.Infrastructure.Data.Context.SQLContext;
using TeamPork.LiveCart.Infrastructure.Data.Entities.Abstract;
using TeamPork.LiveCart.Infrastructure.Data.Generic.GenericSyncedService.Interface;
using TeamPork.LiveCart.Model.Abstract;

namespace TeamPork.LiveCart.Infrastructure.Data.Generic.GenericSyncedService
{
    public class GenericSyncedService<TSyncedEntity, TSyncedModel> : IGenericSyncedService<TSyncedEntity, TSyncedModel> where TSyncedEntity : SyncedEntity<long> where TSyncedModel : SyncedModel<long>
    {
        private readonly IMapper mapper;
        private readonly AppDbContext dbContext;

        public GenericSyncedService(IMapper mapper, AppDbContext dbContext)
        {
            this.mapper = mapper;
            this.dbContext = dbContext;
        }

        public async Task<TSyncedModel> Create(TSyncedModel model)
        {
            var now = DateTime.UtcNow;

            model.CreatedAt = now;
            var entity = mapper.Map<TSyncedEntity>(model);
            dbContext.Set<TSyncedEntity>().Add(entity);
            await dbContext.SaveChangesAsync();

            return model;
        }
        public async Task<TSyncedModel> Update(TSyncedModel model, TSyncedEntity entity)
        {
            var now = DateTime.UtcNow;

            mapper.Map(model, entity);
            entity.UpdatedAt = now;

            await dbContext.SaveChangesAsync();
            return model;
        }
        public async Task<TSyncedModel> Delete(TSyncedEntity entity)
        {
            var now = DateTime.UtcNow;

            entity.DeletedAt = now;
            await dbContext.SaveChangesAsync();

            return mapper.Map<TSyncedModel>(entity);
        }

        public SyncedModelChanges<TSyncedModel> Pull(DateTime lastPulled)
        {

            var modelChanges = new SyncedModelChanges<TSyncedModel>
            {
                Created =  mapper.Map<ICollection<TSyncedModel>>(dbContext.Set<TSyncedEntity>()
                .Where(x => x.CreatedAt > lastPulled && x.ClientId == null && x.DeletedAt == null)
                    .ToList()),
                Updated = mapper.Map<ICollection<TSyncedModel>>(dbContext.Set<TSyncedEntity>()
                .Where(x => x.UpdatedAt > lastPulled && x.ClientId != null && x.DeletedAt == null)
                    .ToList()),
                Deleted = dbContext.Set<TSyncedEntity>()
                    .Where(x => x.DeletedAt > lastPulled && x.ClientId != null && x.ClientId != "")
                    .Select(x => x.ClientId!)
                    .ToList()
            };
            return modelChanges;
        }

        public SyncedModelChanges<TSyncedModel> PullAll()
        {

            var modelChanges = new SyncedModelChanges<TSyncedModel>
            {
                Created = mapper.Map<ICollection<TSyncedModel>>(dbContext
                .Set<TSyncedEntity>()
                .Where(x => x.DeletedAt == null)
                .ToList()),
                Updated = [],
                Deleted = []
            };
            return modelChanges;
        }

        public async Task Push(SyncedModelChanges<TSyncedModel> changes, DateTime now)
        {
            foreach (var model in changes.Created)
            {
                var existing = await dbContext.Set<TSyncedEntity>().FindAsync(model.Id);
                if (existing == null)
                {
                    var entity = mapper.Map<TSyncedEntity>(model);
                    entity.CreatedAt = now;
                    dbContext.Set<TSyncedEntity>().Add(entity);
                }
                else
                {
                    mapper.Map(model, existing);
                    existing.UpdatedAt = now;
                }
            }

            foreach (var model in changes.Updated)
            {
                var existing = await dbContext.Set<TSyncedEntity>().FindAsync(model.Id);
                if (existing != null)
                {
                    mapper.Map(model, existing);
                    existing.UpdatedAt = now;
                }
            }

            foreach (var id in changes.Deleted)
            {
                var entity = await dbContext.Set<TSyncedEntity>().FindAsync(id);
                if (entity != null)
                    entity.DeletedAt = now;
            }
            await dbContext.SaveChangesAsync();
        }

        
    }
}
