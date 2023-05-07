using EducationMicroService.Application;
using EducationMicroService.Application.DTOs;
using EducationMicroService.Persistence;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers()
    .ConfigureApiBehaviorOptions(options =>
    {
        options.InvalidModelStateResponseFactory = context =>
        {
            var response = new BaseResponse();
            response.Success = false;
            response.Errors = new List<string>();

            foreach (var keyModelStatePair in context.ModelState)
            {
                var errors = keyModelStatePair.Value.Errors;
                if (errors != null)
                {
                    foreach (var item in errors)
                    {
                        response.Errors.Add(item.ErrorMessage);
                    }
                }
            }

            return new BadRequestObjectResult(response);
        };
    });

builder.Services.ConfigureApplicationServices(builder.Configuration);
builder.Services.ConfigurePersistenceServices(builder.Configuration);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(o =>
{
    o.AddPolicy("CorsPolicy", builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.UseCors("CorsPolicy");
app.MapControllers();
app.MigrateDatabase();
app.Run();
