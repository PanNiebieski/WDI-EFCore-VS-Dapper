using Microsoft.OpenApi.Models;
using WDIPaladins.Infrastructure.EFCore;
using WDIPaladins.Infrastructure.Dapper;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

ConfigurationManager configuration = builder.Configuration;

builder.Services.AddDapperServices(configuration);
//builder.Services.AddEFServices(configuration);

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "WDIPaladins.Api Dapper",
    });

});

var app = builder.Build();


app.UseHttpsRedirection();
app.UseRouting();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint
    ("/swagger/v1/swagger.json", "WDIPaladins.Api Dapper");
});


app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});



app.Run();
