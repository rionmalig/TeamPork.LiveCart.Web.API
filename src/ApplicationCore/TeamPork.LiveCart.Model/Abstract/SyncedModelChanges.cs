using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamPork.LiveCart.Model.LiveCart.App;

namespace TeamPork.LiveCart.Model.Abstract
{
    public class SyncedModelChanges<TSyncedModel>
    {
        public required ICollection<TSyncedModel> Created { get; set; }
        public required ICollection<TSyncedModel> Updated { get; set; }
        public required ICollection<string> Deleted { get; set; }
    }
}
