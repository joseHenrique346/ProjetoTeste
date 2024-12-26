using Microsoft.AspNetCore.Identity;
using ProjetoTeste.Api.Extensions;
using ProjetoTeste.Infrastructure.Persistence.Context;
using System.Text.Json.Serialization;


var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureContext(builder.Configuration);
builder.Services.ConfigureInjectionDependency();
builder.Services.AddControllers();

//builder.Services.AddControllers(options =>
//{
//    options.Filters.Add(new GlobalExceptionFilter());
//})
//.AddJsonOptions(options =>
//{
//    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
//});

builder.Services.AddEndpointsApiExplorer();
builder.Services.ConfigureSwagger();

//builder.Services.AddJWTAuthentication(builder.Configuration);

builder.Services.AddAuthorization();

var app = builder.Build();

app.ApplySwagger();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
