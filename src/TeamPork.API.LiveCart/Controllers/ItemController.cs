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
    public class ItemController : ControllerBase
    {
        private readonly IGenericSyncedService<ItemEntity, Item> syncedEntityService;
        private readonly AppDbContext dbContext;

        public ItemController(IGenericSyncedService<ItemEntity, Item> syncedEntityService,
            AppDbContext dbContext)
        {
            this.syncedEntityService = syncedEntityService;
            this.dbContext = dbContext;
        }

        [HttpPost]
        public async Task<ActionResult<Item?>> Create([FromBody] Item item)
        {
            var result = await syncedEntityService.Create(item);
            return Ok(result);
        }
        [HttpGet]
        [EnableQuery]
        public IQueryable<Item> Get([FromServices] IGenericOdataService<Item, ItemEntity, long> service)
        {
            return service.GetODataQuery();
        }
        [HttpPut]
        public async Task<ActionResult<Item?>> Update([FromBody] Item item)
        {
            var entity = await dbContext.Items.FindAsync(item.Id);
            if (entity == null)
                return NotFound("Item not found");

            item.ClientId = entity.ClientId;

            var result = await syncedEntityService.Update(item, entity);
            return Ok(result);
        }
        [HttpDelete("{itemId}")]
        public async Task<ActionResult<Item?>> Delete([FromRoute] long itemId)
        {
            var entity = await dbContext.Items.FindAsync(itemId);
            if (entity == null)
                return NotFound("Item not found");

            var result = await syncedEntityService.Delete(entity);
            return Ok(result);
        }
    }
}
