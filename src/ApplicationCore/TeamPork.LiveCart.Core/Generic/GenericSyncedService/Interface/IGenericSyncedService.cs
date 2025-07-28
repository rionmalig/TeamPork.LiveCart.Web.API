using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamPork.LiveCart.Infrastructure.Data.Entities.Abstract;
using TeamPork.LiveCart.Model.Abstract;

namespace TeamPork.LiveCart.Infrastructure.Data.Generic.GenericSyncedService.Interface
{
    public interface IGenericSyncedService<TSyncedEntity, TSyncedModel> where TSyncedEntity : SyncedEntity<long> where TSyncedModel : SyncedModel<long>
    {
        public SyncedModelChanges<TSyncedModel> Pull(DateTime lastPulled, long userId, long? businessId);
        public SyncedModelChanges<TSyncedModel> PullAll(long userId, long? businessId);
        public SyncedModelChanges<TSyncedModel> PullAllResync(long userId, long? businessId);

        public Task Push(SyncedModelChanges<TSyncedModel> changes, DateTime now, long userId, long? businessId);

        public Task<TSyncedModel> Create(TSyncedModel model);
        public Task<TSyncedModel> Update(TSyncedModel model, TSyncedEntity entity);
        public Task<TSyncedModel> Delete(TSyncedEntity entity);

    }
}
