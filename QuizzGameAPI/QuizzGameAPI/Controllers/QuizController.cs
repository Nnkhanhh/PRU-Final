using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuizAPI.Data;

namespace QuizAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class QuizController : ControllerBase
{
    private readonly AppDbContext _context;

    public QuizController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/quiz/map/1/random
    [HttpGet("map/{level}/random")]
    public async Task<IActionResult> GetRandomByLevel(int level)
    {
        var questions = await _context.QuizQuestions
            .Where(q => q.MapLevel == level)
            .OrderBy(q => Guid.NewGuid())
            .Take(20)
            .ToListAsync();

        return Ok(questions);
    }
}
