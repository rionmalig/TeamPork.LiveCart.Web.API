using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeamPork.LiveCart.Core.Services.LiveCart.App.Interface;
using TeamPork.LiveCart.Model.LiveCart.Sync.Request;
using TeamPork.LiveCart.Model.LiveCart.Sync.Response;

namespace TeamPork.API.LiveCart.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SyncController : ControllerBase
    {
        private readonly ISyncService syncService;

        public SyncController(ISyncService syncService)
        {
            this.syncService = syncService;
        }

        [AllowAnonymous]
        [HttpGet("Pull")]
        public ActionResult<SyncPullResponse> Pull([FromQuery] long lastPulledAt, [FromQuery] bool pullAll)
        {
            var response = pullAll ? syncService.PullAll() : syncService.Pull(lastPulledAt);
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpPost("Push")]
        public async Task<ActionResult> Push([FromBody] SyncPushRequest request)
        {
            await syncService.Push(request);
            return Ok();
        }
    }
}
