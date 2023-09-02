using AspnetTemplate.Core.Models;
using AspnetTemplate.Infrastructure;
using Bogus;
using CliFx;
using CliFx.Attributes;
using CliFx.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Commands;

[Command("seed")]
public class SeedCommand : ICommand
{
    public ValueTask ExecuteAsync(IConsole console)
    {
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddEnvironmentVariables().Build();
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseSqlServer(configuration.GetConnectionString("AppDbContext"))
            .Options;
        using (var context = new AppDbContext(options))
        {
            console.Output.WriteLine("Getting migrations...");
            if (context.Database.GetMigrations().Any())
                context.Database.Migrate();
            var faker = CreateSampleFaker();

            var samples = faker.Generate(10000);

            context.Samples.AddRange(samples);
            console.Output.WriteLine("Seeding db...");
            context.SaveChanges();
        }

        console.Output.WriteLine("Done!");
        return new ValueTask();
    }

    private static Faker<Sample> CreateSampleFaker()
    {
        var random = new Randomizer();

        var sampleFaker = new Faker<Sample>()
            .RuleFor(x => x.Name, (f, _) => f.Lorem.Paragraph())
            .RuleFor(x => x.Value, (f, _) => f.Random.Number());
        return sampleFaker;
    }
}