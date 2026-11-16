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
        
    }
}
