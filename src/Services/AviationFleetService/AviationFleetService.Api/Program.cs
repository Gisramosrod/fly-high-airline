using AviationFleetService.Infrastructure;
using AviationFleetService.Application;
using Microsoft.EntityFrameworkCore;
using AviationFleetService.Infrastructure.Common;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    using IServiceScope scope = app.Services.CreateScope();
    ApplyMigration<AppDbContext>(scope);
}

app.UseAuthorization();

app.MapControllers();

app.Run();

static void ApplyMigration<TDbContext>(IServiceScope scope)
    where TDbContext : DbContext
{
    using TDbContext context = scope.ServiceProvider
        .GetRequiredService<TDbContext>();

    context.Database.Migrate();
}

