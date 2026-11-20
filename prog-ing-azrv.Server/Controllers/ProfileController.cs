using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using prog_ing_azrv.Server.Data;
using prog_ing_azrv.Server.Models;
using prog_ing_azrv.Server.Models.M2N;

namespace prog_ing_azrv.Server.Controllers;

[ApiController]
[Route("api/profile")]
[Authorize]
public class ProfileController(AzrvContext db, UserManager<User> users) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetProfile()
    {
        var user = await users.GetUserAsync(User);
        if (user is null) return Unauthorized();

        var skills = await db.Set<UserSkill>().Where(s => s.UserID == user.Id)
            .Select(s => new { s.Id, Name = s.Skill.Name, Status = s.WantedStatusId, Level = s.SkillLevelID })
            .OrderBy(s => s.Name).ToListAsync();
        var friends = await db.Set<UserUser>()
            .Where(f => f.FriendhipStatusID == 2 && (f.FirstUserID == user.Id || f.SecondUserID == user.Id))
            .Select(f => new { Id = f.FirstUserID == user.Id ? f.SecondUserID : f.FirstUserID,
                Name = f.FirstUserID == user.Id ? f.SecondUser.UserName : f.FirstUser.UserName })
            .ToListAsync();
        return Ok(new { user.UserName, user.Bio, Skills = skills, Friends = friends });
    }

    [HttpGet("catalog")]
    public async Task<IActionResult> Catalog() => Ok(await db.Skills.OrderBy(s => s.Name)
        .Select(s => new { s.Id, s.Name }).ToListAsync());

    public record BioRequest([MaxLength(500)] string? Bio);
    [HttpPut("bio")]
    public async Task<IActionResult> UpdateBio(BioRequest request)
    {
        var user = await users.GetUserAsync(User);
        if (user is null) return Unauthorized();
        user.Bio = request.Bio?.Trim();
        var result = await users.UpdateAsync(user);
        return result.Succeeded ? NoContent() : Problem("Opis nije moguće spremiti.");
    }

    public record SkillRequest(int SkillId, int Status, int Level);
    [HttpPost("skills")]
    public async Task<IActionResult> AddSkill(SkillRequest request)
    {
        var userId = users.GetUserId(User);
        if (userId is null) return Unauthorized();
        if (request.Status is not (1 or 2) || request.Level is < 1 or > 3 ||
            !await db.Skills.AnyAsync(s => s.Id == request.SkillId))
            return BadRequest("Odaberi postojeću vještinu i razinu.");
        if (await db.Set<UserSkill>().AnyAsync(s => s.UserID == userId && s.SkillID == request.SkillId && s.WantedStatusId == request.Status))
            return Conflict("Vještina već postoji na ovom popisu.");
        db.Set<UserSkill>().Add(new UserSkill { UserID = userId, SkillID = request.SkillId,
            WantedStatusId = request.Status, SkillLevelID = request.Level });
        await db.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("skills/{id:int}")]
    public async Task<IActionResult> DeleteSkill(int id)
    {
        var userId = users.GetUserId(User);
        if (userId is null) return Unauthorized();
        var skill = await db.Set<UserSkill>().SingleOrDefaultAsync(s => s.Id == id && s.UserID == userId);
        if (skill is null) return NotFound();
        db.Remove(skill);
        await db.SaveChangesAsync();
        return NoContent();
    }
}
