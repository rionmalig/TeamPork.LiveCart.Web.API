using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using TeamPork.LiveCart.Core.Generic.GenericODataService.Interface;
using TeamPork.LiveCart.Infrastructure.Data.Context.SQLContext;
using TeamPork.LiveCart.Infrastructure.Data.Entities.LiveCart.App;
using TeamPork.LiveCart.Infrastructure.Data.Generic.GenericSyncedService.Interface;
using TeamPork.LiveCart.Model.LiveCart.App;

namespace TeamPork.API.LiveCart.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly IGenericSyncedService<InvoiceEntity, Invoice> syncedEntityService;
        private readonly AppDbContext dbContext;

        public InvoiceController(IGenericSyncedService<InvoiceEntity, Invoice> syncedEntityService,
            AppDbContext dbContext)
        {
            this.syncedEntityService = syncedEntityService;
            this.dbContext = dbContext;
        }

        [HttpPost]
        public async Task<ActionResult<Invoice?>> Create([FromBody] Invoice invoice)
        {
            var result = await syncedEntityService.Create(invoice);
            return Ok(result);
        }
        [HttpGet]
        [EnableQuery]
        public IQueryable<Invoice> Get([FromServices] IGenericOdataService<Invoice, InvoiceEntity, long> service)
        {
            return service.GetODataQuery();
        }
        [HttpPut]
        public async Task<ActionResult<Invoice?>> Update([FromBody] Invoice invoice)
        {
            var entity = await dbContext.Invoices.FindAsync(invoice.Id);
            if (entity == null)
                return NotFound("Invoice not found");

            invoice.ClientId = entity.ClientId;
            invoice.CustomerClientId = entity.CustomerClientId;

            var result = await syncedEntityService.Update(invoice, entity);
            return Ok(result);
        }
        [HttpDelete("{invoiceId}")]
        public async Task<ActionResult<Invoice?>> Delete([FromRoute] long invoiceId)
        {
            var entity = await dbContext.Invoices.FindAsync(invoiceId);
            if (entity == null)
                return NotFound("Invoice not found");

            var result = await syncedEntityService.Delete(entity);
            return Ok(result);
        }
    }
}
