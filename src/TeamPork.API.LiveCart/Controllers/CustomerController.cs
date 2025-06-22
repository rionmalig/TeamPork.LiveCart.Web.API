using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using TeamPork.LiveCart.Core.Generic.GenericODataService.Interface;
using TeamPork.LiveCart.Core.Services.LiveCart.App.Interface;
using TeamPork.LiveCart.Infrastructure.Data.Context.SQLContext;
using TeamPork.LiveCart.Infrastructure.Data.Entities.LiveCart.App;
using TeamPork.LiveCart.Infrastructure.Data.Generic.GenericSyncedService.Interface;
using TeamPork.LiveCart.Model.LiveCart.App;

namespace TeamPork.API.LiveCart.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IGenericSyncedService<CustomerEntity, Customer> syncedEntityService;
        private readonly AppDbContext dbContext;

        public CustomerController(IGenericSyncedService<CustomerEntity, Customer> syncedEntityService,
            AppDbContext dbContext)
        {
            this.syncedEntityService = syncedEntityService;
            this.dbContext = dbContext;
        }

        [HttpPost]
        public async Task<ActionResult<Customer?>> Create([FromBody] Customer customer)
        {
            var result = await syncedEntityService.Create(customer);
            return Ok(result);
        }
        [HttpPut]
        public async Task<ActionResult<Customer?>> Update([FromBody] Customer customer)
        {
            var entity = await dbContext.Customers.FindAsync(customer.Id);
            if (entity == null)
                return NotFound("Customer not found");

            customer.ClientId = entity.ClientId;

            var result = await syncedEntityService.Update(customer, entity);
            return Ok(result);
        }
        [HttpDelete("{customerId}")]
        public async Task<ActionResult<Customer?>> Delete([FromRoute] long customerId)
        {
            var entity = await dbContext.Customers.FindAsync(customerId);
            if (entity == null)
                return NotFound("Customer not found");

            var result = await syncedEntityService.Delete(entity);
            return Ok(result);
        }

        [HttpGet]
        [EnableQuery]
        public IQueryable<Customer> Get([FromServices] IGenericOdataService<Customer, CustomerEntity, long> service)
        {
            return service.GetODataQuery();
        }
    }
}
