using Microsoft.EntityFrameworkCore;
using RulesEngine.Data;
using RulesEngineWrapper.presentation;
using static RulesEngineWrapper.presentation.Options.RulesEngineServiceOptions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddRulesEngineWrapper<RulesEngineContext>(options =>
{
    options.rulesEngineDataSource = RulesEngineDataSource.Database; 
    options.DbContextOptionsAction = dbContextOptionsBuilder =>
    {
        dbContextOptionsBuilder.UseSqlServer(builder.Configuration.GetConnectionString("Metro"));
    };
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var dbContext = services.GetRequiredService<RulesEngineContext>();
    dbContext.Database.EnsureCreated();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseRulesEngineWrapper();

app.Run();
