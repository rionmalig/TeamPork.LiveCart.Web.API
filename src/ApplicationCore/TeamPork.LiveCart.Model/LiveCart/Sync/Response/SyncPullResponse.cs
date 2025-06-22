using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamPork.LiveCart.Model.LiveCart.Sync.Changes;

namespace TeamPork.LiveCart.Model.LiveCart.Sync.Response
{
    public class SyncPullResponse
    {
        public ChangeSet? Changes { get; set; }
        public long TimeStamp {  get; set; }
    }
}
    