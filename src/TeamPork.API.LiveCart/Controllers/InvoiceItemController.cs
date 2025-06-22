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
    public class InvoiceItemController : ControllerBase
    {
        private readonly IGenericSyncedService<InvoiceItemEntity, InvoiceItem> syncedEntityService;
        private readonly AppDbContext dbContext;

        public InvoiceItemController(IGenericSyncedService<InvoiceItemEntity, InvoiceItem> syncedEntityService,
            AppDbContext dbContext)
        {
            this.syncedEntityService = syncedEntityService;
            this.dbContext = dbContext;
        }

        [HttpPost]
        public async Task<ActionResult<InvoiceItem?>> Create([FromBody] InvoiceItem invoiceItem)
        {
            var result = await syncedEntityService.Create(invoiceItem);
            return Ok(result);
        }
        [HttpGet]
        [EnableQuery]
        public IQueryable<InvoiceItem> Get([FromServices] IGenericOdataService<InvoiceItem, InvoiceItemEntity, long> service)
        {
            return service.GetODataQuery();
        }
        [HttpPut]
        public async Task<ActionResult<InvoiceItem?>> Update([FromBody] InvoiceItem invoiceItem)
        {
            var entity = await dbContext.InvoiceItems.FindAsync(invoiceItem.Id);
            if (entity == null)
                return NotFound("Invoice Item not found");

            invoiceItem.ClientId = entity.ClientId;
            invoiceItem.InvoiceClientId = entity.ClientId;
            invoiceItem.ItemClientId = entity.ClientId;

            var result = await syncedEntityService.Update(invoiceItem, entity);
            return Ok(result);
        }
        [HttpDelete("{invoiceItemId}")]
        public async Task<ActionResult<InvoiceItem?>> Delete([FromRoute] long invoiceItemId)
        {
            var entity = await dbContext.InvoiceItems.FindAsync(invoiceItemId);
            if (entity == null)
                return NotFound("Invoice Item not found");

            var result = await syncedEntityService.Delete(entity);
            return Ok(result);
        }
    }
}
