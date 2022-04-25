using Microsoft.AspNetCore.Mvc;
using graphqlpoc.Data;
using Microsoft.EntityFrameworkCore;

namespace graphqlpoc.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;
    private readonly IDbContextFactory<ReferenceTableContext> _contextFactory;
    private readonly ReferenceTableContext _context;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, 
        IDbContextFactory<ReferenceTableContext> contextFactory,
        ReferenceTableContext context)
    {
        _logger = logger;
        _contextFactory = contextFactory;
        _context = context;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        // var context = _contextFactory.CreateDbContext();
        var context = _context;
        var tablesQuery = from c in context.ReferenceTableColumns
        select c.Name;
        var columnList = tablesQuery.ToList();

        var columnCount = columnList.Count();

        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateTime.Now.AddDays(index),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = columnList.ElementAt(Random.Shared.Next(columnCount))
        })
        .ToArray();
    }
}
