using Core.Dal;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using WebApi.Core;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOptions();
builder.Services.AddSingleton(_ => CoreSqLiteDbContext.CreateInMemoryDatabase());
builder.Services.AddDbContext<CoreSqLiteDbContext>();
builder.Services.AddTransient<DbContext>(ctx => ctx.GetRequiredService<CoreSqLiteDbContext>());

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "The 80 by 20", Version = "v1" });
});


CoreDependencies.AddTo(builder);

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("v1/swagger.json", "The 80 by 20"));

using (var serviceScope = app.Services.CreateScope())
{
    var context = serviceScope.ServiceProvider.GetRequiredService<CoreSqLiteDbContext>();
    await context.Database.EnsureCreatedAsync();
}

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
