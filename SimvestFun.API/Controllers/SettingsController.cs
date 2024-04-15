using Microsoft.AspNetCore.Mvc;
using SimvestFun.ApplicationCore.Entities;
using SimvestFun.ApplicationCore.Interfaces;

namespace SimvestFun.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SettingsController : BaseController
    {
        private readonly ISettingService _settingService;
        public SettingsController(ISettingService settingService, IJwtUtils jwtUtils) : base(jwtUtils)
        {
            _settingService = settingService;
        }

        [HttpGet("{key}")]
        public async Task<ActionResult<Setting>> GetSettingByKey(string key)
        {
            var setting = await _settingService.GetSettingByKey(key);

            return setting;
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAnnouncement([FromBody] Setting announcement)
        {
            var loggedUserId = GetConnectedUserId();

            if (announcement.Key.ToLower() != "announcement")
                return BadRequest();

            try
            {
                await _settingService.UpdateAnnouncement(loggedUserId, announcement);
                return Ok();
            }
            catch (UnauthorizedAccessException)
            {
                return Forbid();
            }
        }
    }
}
