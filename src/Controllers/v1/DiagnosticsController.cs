using Asp.Versioning;
using AspnetTemplate.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AspnetTemplate.Controllers.v1;

[ApiVersion("1")]
[ApiController]
[Route("v{version:apiVersion}/[controller]")]
public class DiagnosticsController : ControllerBase
{
    private readonly AppDbContext _context;

    public DiagnosticsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet("sync")]
    public IActionResult GetSync()
    {
        _context.Database.ExecuteSqlRaw("WAITFOR DELAY '00:00:00.500'");
        return Ok();
    }

    [HttpGet("async")]
    public async Task<IActionResult> GetAsync()
    {
        await _context.Database.ExecuteSqlRawAsync("WAITFOR DELAY '00:00:00.500'");
        return Ok();
    }
}