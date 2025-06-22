using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamPork.LiveCart.Model.Abstract;
using TeamPork.LiveCart.Model.LiveCart.App;

namespace TeamPork.LiveCart.Model.LiveCart.Sync.Changes
{
    public class ChangeSet
    {
        public required SyncedModelChanges<Customer> Customers { get; set; }
        public required SyncedModelChanges<Item> Items { get; set; }
        public required SyncedModelChanges<Invoice> Invoices { get; set; }
        public required SyncedModelChanges<InvoiceItem> InvoiceItems { get; set; }
    }
}
