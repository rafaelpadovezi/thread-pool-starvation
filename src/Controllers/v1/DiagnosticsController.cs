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
    private const string SqlDelay = "WAITFOR DELAY '00:00:00.500'"; // 500 ms
    private readonly AppDbContext _context;

    public DiagnosticsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet("sync")]
    public IActionResult GetSync()
    {
        var result = DoQuery();
        return Ok(result);
    }

    [HttpGet("sync-over-async")]
    public IActionResult GetAsyncOverSync()
    {
        var result = DoQueryAsync().Result;
        return Ok(result);
    }

    [HttpGet("async")]
    public async Task<IActionResult> GetAsync()
    {
        var result = await DoQueryAsync();
        return Ok(result);
    }

    int DoQuery()
    {
        _context.Database.ExecuteSqlRaw(SqlDelay);
        return 1;
    }

    async ValueTask<int> DoQueryAsync()
    {
        await _context.Database.ExecuteSqlRawAsync(SqlDelay);
        return 1;
    }
}