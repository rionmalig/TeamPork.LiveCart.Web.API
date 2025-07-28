using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamPork.LiveCart.Model.LiveCart.Sync.Request;
using TeamPork.LiveCart.Model.LiveCart.Sync.Response;

namespace TeamPork.LiveCart.Core.Services.LiveCart.App.Interface
{
    public interface ISyncService
    {
        public SyncPullResponse Pull(long lastPulledAt, long userId, long? businessId);
        public SyncPullResponse PullAll(long userId, long? businessId, bool reSync);

        public Task Push(SyncPushRequest request, long userId, long? businessId);
    }
}
