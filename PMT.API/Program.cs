using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using PMT.Core;
using PMT.Infrastructure;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers()
    .AddJsonOptions
    (options =>
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter())
    );

builder.Services.AddCore().AddInfrastructure(builder.Configuration);

//Add API explorer services
builder.Services.AddEndpointsApiExplorer();

//Add swagger generation services to create swagger specification
builder.Services.AddSwaggerGen();

//Add cors service
builder.Services.AddCors
            (options => options.AddDefaultPolicy
            (builder => builder
                        .WithOrigins("http://localhost:4200")
                        .AllowAnyMethod()
                        .AllowAnyHeader()));

var app = builder.Build();

//Enable routing
app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "api/{controller}/{action}/{id?}");
});

app.UseSwagger(); //Adds endpoints that can serve the swagger.json
app.UseSwaggerUI(); //Adds swagger UI (interactive page to explore and test API endpoints)
app.UseCors();


// Configure the HTTP request pipeline.
app.UseHttpsRedirection();

app.UseAuthorization();

//Auth
app.UseAuthentication();
app.UseAuthorization();

//Mapping controller routes
app.MapControllers();

app.Run();
