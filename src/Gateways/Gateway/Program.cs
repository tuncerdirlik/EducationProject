using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Configuration.SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile($"configuration.{builder.Environment.EnvironmentName.ToLower()}.json", false, true).AddEnvironmentVariables();

builder.Services.AddOcelot(builder.Configuration);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

await app.UseOcelot();

app.UseAuthorization();

app.MapControllers();

app.Run();