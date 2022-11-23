var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;
var environment = builder.Environment;
builder.Services.AddReverseProxy()
    .LoadFromConfig(configuration.GetSection("reverseProxy"));

var app = builder.Build();

if (environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapGet("/", () => "the80by20 API Gateway!");
    endpoints.MapReverseProxy();
});

app.Run();