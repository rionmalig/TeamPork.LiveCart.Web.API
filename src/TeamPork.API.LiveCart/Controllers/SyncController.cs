using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
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

        [Authorize]
        [HttpGet("Pull")]
        public ActionResult<SyncPullResponse> Pull([FromQuery] long lastPulledAt, [FromQuery] bool pullAll = false, [FromQuery] bool replacementSync = false)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var userIdLong = long.TryParse(userId, out var id) ? id : throw new UnauthorizedAccessException("Invalid user ID");

            long? businessIdLong = long.TryParse(User.FindFirst(ClaimTypes.PrimaryGroupSid)?.Value, out var businessId) ? businessId : null;

            var response = pullAll ? syncService.PullAll(userIdLong, businessIdLong, replacementSync) : syncService.Pull(lastPulledAt, userIdLong, businessIdLong);
            return Ok(response);
        }

        [Authorize]
        [HttpPost("Push")]
        public async Task<ActionResult> Push([FromBody] SyncPushRequest request)
        {

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var userIdLong = long.TryParse(userId, out var id) ? id : throw new UnauthorizedAccessException("Invalid user ID");

            long? businessIdLong = long.TryParse(User.FindFirst(ClaimTypes.PrimaryGroupSid)?.Value, out var businessId) ? businessId : null;

            await syncService.Push(request, userIdLong, businessIdLong);
            return Ok();
        }


        [AllowAnonymous]
        [HttpPost("Debug-Push")]
        public async Task<ActionResult> DebugPush([FromBody] SyncPushRequest request, [FromQuery] long userId, [FromQuery] long businessId)
        {
            await syncService.Push(request, userId, businessId);
            return Ok();
        }
    }
}
