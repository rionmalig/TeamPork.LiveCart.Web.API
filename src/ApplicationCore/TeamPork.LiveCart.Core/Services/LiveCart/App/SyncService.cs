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
using Microsoft.Extensions.Logging;

namespace TeamPork.LiveCart.Core.Services.LiveCart.App
{
    public class SyncService : ISyncService
    {
        private readonly AppDbContext dbContext;
        private readonly IGenericSyncedService<CustomerEntity, Customer> customerSyncedService;
        private readonly IGenericSyncedService<ItemEntity, Item> itemSyncedService;
        private readonly IGenericSyncedService<InvoiceEntity, Invoice> invoiceSyncedService;
        private readonly IGenericSyncedService<InvoiceItemEntity, InvoiceItem> invoiceItemSyncedService;
        private readonly ILogger<SyncService> logger;

        public SyncService(AppDbContext dbContext, IMapper mapper,
            IGenericSyncedService<CustomerEntity, Customer> customerSyncedService,
            IGenericSyncedService<ItemEntity, Item> itemSyncedService,
            IGenericSyncedService<InvoiceEntity, Invoice> invoiceSyncedService,
            IGenericSyncedService<InvoiceItemEntity, InvoiceItem> invoiceItemSyncedService,
            ILogger<SyncService> logger

            )
        {
            this.dbContext = dbContext;
            this.customerSyncedService = customerSyncedService;
            this.itemSyncedService = itemSyncedService;
            this.invoiceSyncedService = invoiceSyncedService;
            this.invoiceItemSyncedService = invoiceItemSyncedService;
            this.logger = logger;
        }
        public SyncPullResponse Pull(long lastPulledAt, long userId, long? businessId)
        {

            var lastPulled = DateTimeOffset.FromUnixTimeMilliseconds(lastPulledAt).UtcDateTime;
            var changes = new ChangeSet
            {
                Customers = customerSyncedService.Pull(lastPulled, userId, businessId),
                Items = itemSyncedService.Pull(lastPulled, userId, businessId),
                Invoices = invoiceSyncedService.Pull(lastPulled, userId, businessId),
                InvoiceItems = invoiceItemSyncedService.Pull(lastPulled, userId, businessId),

            };
            return new SyncPullResponse
            {
                Changes = changes,
                TimeStamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()
            };
        }

        public SyncPullResponse PullAll(long userId, long? businessId)
        {
            var changes = new ChangeSet
            {
                Customers = customerSyncedService.PullAll(userId, businessId),
                Items = itemSyncedService.PullAll(userId, businessId),
                Invoices = invoiceSyncedService.PullAll(userId, businessId),
                InvoiceItems = invoiceItemSyncedService.PullAll(userId, businessId),

            };
            return new SyncPullResponse
            {
                Changes = changes,
                TimeStamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()
            };
        }

        public async Task Push(SyncPushRequest request, long userId, long? businessId)
        {
            var changes = request.Changes;
            var now = DateTime.UtcNow;

            // Customer preprocess update 
            foreach (var customer in changes.Customers.Updated)
            {
                if (customer.Id != 0)
                    continue;

                var customerEntity = dbContext.Customers
                    .FirstOrDefault(c => c.ClientId == customer.ClientId);
                if (customerEntity == null) continue;

                customer.Id = customerEntity.Id;
            }
            await customerSyncedService.Push(changes.Customers, now, userId, businessId);

            // Item preprocess update
            foreach (var item in changes.Items.Updated)
            {
                if (item.Id != 0)
                    continue;

                var itemEntity = dbContext.Items
                    .FirstOrDefault(i => i.ClientId == item.ClientId);
                if (itemEntity == null) continue;

                item.Id = itemEntity.Id;
            }
            await itemSyncedService.Push(changes.Items, now, userId, businessId);


            // For invoice's relationships HAVE TO BE HANDLED MANUALLY!!
            foreach(var invoice in changes.Invoices.Created)
            {
                var customer = dbContext.Customers
                    .FirstOrDefault(customer => customer.ClientId == invoice.CustomerClientId);
                if (customer == null) continue;
                invoice.CustomerId = customer.Id;
            }

            // Invoice preprocess update
            foreach (var invoice in changes.Invoices.Updated)
            {
                if (invoice.Id != 0)
                    continue;

                var invoiceEntity = dbContext.Invoices
                    .FirstOrDefault(i => i.ClientId == invoice.ClientId);
                if (invoiceEntity == null) continue;

                var customer = dbContext.Customers
                    .FirstOrDefault(c => c.ClientId == invoice.CustomerClientId);
                if (customer == null) continue;

                invoice.CustomerId = customer.Id;
                invoice.Id = invoiceEntity.Id;
            }
            await invoiceSyncedService.Push(changes.Invoices, now, userId, businessId);

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
            await invoiceItemSyncedService.Push(changes.InvoiceItems, now, userId, businessId);
        }
    }
}
