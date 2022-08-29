using Microsoft.EntityFrameworkCore;
using PsqlAndDockerComposeExample;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHealthChecks();

builder.Services.AddDbContext<postgresContext>(_ =>
    _.UseNpgsql(Environment.GetEnvironmentVariable("DB_CONNECTIONSTRING")?? throw new Exception()));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapHealthChecks("/health");
});


// app.UseHttpsRedirection();

// app.UseAuthorization();

app.MapControllers();

app.Run();