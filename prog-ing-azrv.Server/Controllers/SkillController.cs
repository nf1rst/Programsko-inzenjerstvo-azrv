using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using prog_ing_azrv.Server.Interfaces.Services;

namespace prog_ing_azrv.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SkillController : ControllerBase
    {
        private readonly ISkillService SkillService;
        public SkillController(ISkillService SkillService)
        {
            this.SkillService = SkillService;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllSkills()
        {
            return Ok(await SkillService.GetAllSkills());
        }
        [HttpGet("health")]
        public IActionResult HealthCheck()
        {
            var cookie = Request.Cookies[".AspNetCore.Identity.Application"];
            if (cookie != null)
            {
                return Ok($"You are now logged into the application. You cookie: {cookie}");
            }
            return Unauthorized($"You do not have access to this you need a cookie. Your cookie: {cookie}");
        }
        
    }
}
