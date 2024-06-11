using RulesEngineWrapper.Dashboard;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseStaticFiles(); 

app.UseHttpsRedirection();

app.UseRulesEngineDashboard();

app.Run();
