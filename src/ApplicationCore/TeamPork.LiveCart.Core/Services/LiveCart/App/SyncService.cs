using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamPork.LiveCart.Core.Services.LiveCart.App.Interface;
using TeamPork.LiveCart.Infrastructure.Data.Context.SQLContext;
using TeamPork.LiveCart.Model.LiveCart.App;
using TeamPork.LiveCart.Model.LiveCart.Sync.Changes;
using TeamPork.LiveCart.Model.LiveCart.Sync.Request;
using TeamPork.LiveCart.Model.LiveCart.Sync.Response;
using TeamPork.LiveCart.Infrastructure.Data.Entities.LiveCart.App;
using TeamPork.LiveCart.Model.Abstract;
using TeamPork.LiveCart.Infrastructure.Data.Generic.GenericSyncedService.Interface;
using TeamPork.LiveCart.Model.LiveCart;

namespace TeamPork.LiveCart.Core.Services.LiveCart.App
{
    public class SyncService : ISyncService
    {
        private readonly AppDbContext dbContext;
        private readonly IMapper mapper;
        private readonly IGenericSyncedService<CustomerEntity, Customer> customerSyncedService;
        private readonly IGenericSyncedService<ItemEntity, Item> itemSyncedService;
        private readonly IGenericSyncedService<InvoiceEntity, Invoice> invoiceSyncedService;
        private readonly IGenericSyncedService<InvoiceItemEntity, InvoiceItem> invoiceItemSyncedService;

        public SyncService(AppDbContext dbContext, IMapper mapper,
            IGenericSyncedService<CustomerEntity, Customer> customerSyncedService,
            IGenericSyncedService<ItemEntity, Item> itemSyncedService,
            IGenericSyncedService<InvoiceEntity, Invoice> invoiceSyncedService,
            IGenericSyncedService<InvoiceItemEntity, InvoiceItem> invoiceItemSyncedService


            )
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
            this.customerSyncedService = customerSyncedService;
            this.itemSyncedService = itemSyncedService;
            this.invoiceSyncedService = invoiceSyncedService;
            this.invoiceItemSyncedService = invoiceItemSyncedService;
        }
        public SyncPullResponse Pull(long lastPulledAt, long userId)
        {

            var lastPulled = DateTimeOffset.FromUnixTimeMilliseconds(lastPulledAt).UtcDateTime;
            var changes = new ChangeSet
            {
                Customers = customerSyncedService.Pull(lastPulled, userId),
                Items = itemSyncedService.Pull(lastPulled, userId),
                Invoices = invoiceSyncedService.Pull(lastPulled, userId),
                InvoiceItems = invoiceItemSyncedService.Pull(lastPulled, userId),

            };
            return new SyncPullResponse
            {
                Changes = changes,
                TimeStamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()
            };
        }

        public SyncPullResponse PullAll(long userId)
        {
            var changes = new ChangeSet
            {
                Customers = customerSyncedService.PullAll(userId),
                Items = itemSyncedService.PullAll(userId),
                Invoices = invoiceSyncedService.PullAll(userId),
                InvoiceItems = invoiceItemSyncedService.PullAll(userId),

            };
            return new SyncPullResponse
            {
                Changes = changes,
                TimeStamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()
            };
        }

        public async Task Push(SyncPushRequest request, long userId)
        {
            var changes = request.Changes;
            var now = DateTime.UtcNow;

            await customerSyncedService.Push(changes.Customers, now, userId);
            await itemSyncedService.Push(changes.Items, now, userId);
            // For invoice's relationships HAVE TO BE HANDLED MANUALLY!!
            foreach(var invoice in changes.Invoices.Created)
            {
                var customer = dbContext.Customers
                    .FirstOrDefault(customer => customer.ClientId == invoice.CustomerClientId);
                if (customer == null) continue;
                invoice.CustomerId = customer.Id;
            }
            await invoiceSyncedService.Push(changes.Invoices, now, userId);

            // For invoice item's relationships HAVE TO BE HANDLED MANUALLY!!
            foreach (var invoiceItem in changes.InvoiceItems.Created)
            {
                var invoice = dbContext.Invoices
                    .FirstOrDefault(invoice => invoice.ClientId == invoiceItem.InvoiceClientId);
                var item = dbContext.Items
                    .FirstOrDefault(item => item.ClientId == invoiceItem.ItemClientId);
                if (invoice == null || item == null) continue;

                invoiceItem.InvoiceId = invoice.Id;
                invoiceItem.ItemId = item.Id;
            }
            await invoiceItemSyncedService.Push(changes.InvoiceItems, now, userId);
        }
    }
}
