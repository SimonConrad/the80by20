using Microsoft.OpenApi.Models;
using the80by20.Shared.Abstractions.DomainLayer.SharedKernel;
using the80by20.Shared.Infrastucture.Configuration;
using the80by20.Shared.Infrastucture;
using the80by20.Shared.Infrastucture.Exceptions;
using the80by20.Shared.Infrastucture.Time;

namespace the80by20.Bootstrapper
{
    public static class Extensions
    {
        public static WebApplication UseBootstartpper(this WebApplication app, IConfiguration configuration)
        {
            app.UseMiddleware<ExceptionMiddleware>();

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("v1/swagger.json", "The 80 by 20"));

            //todo
            //app.UseReDoc(reDoc =>
            //{
            //    reDoc.RoutePrefix = "docs";
            //    reDoc.SpecUrl("/swagger/v1/swagger.json");
            //    reDoc.DocumentTitle = "MySpot API";
            //});
            //app.UseAuthentication();

            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();


            app.UseCors();


            return app;
        }

        public static IServiceCollection AddBootsrapper(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();

            //var appOptions = configuration.GetOptions<AppOptions>(sectionName:"app");
            //services.AddSingleton(appOptions);

            services.Configure<AppOptions>(configuration.GetRequiredSection(key: "app"));

            services.AddSingleton<ExceptionMiddleware>();
            services.AddHttpContextAccessor();

            services.AddSingleton<IClock, Clock>();


            services.AddEndpointsApiExplorer(); // todo
            services.AddSwaggerGen(swagger =>
            {
                swagger.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "The 80 by 20",
                    Description = "system created from model to implementaion; model done used event storming, implemntaion done with ddd (strategic, tactical), architectural patterns based on modules drivers; high cohesion low coupling; tests; infrastructure"
                });
                swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description =
                        "Enter 'Bearer' [space] and then your valid token in the text input below." +
                        "\r\n\r\nExample: \"Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9\""
                });
                swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] { }
                }
                });
            });

            var appOptions = configuration.GetOptions<AppOptions>("app");

            // INFO Do cors in proper way https://learn.microsoft.com/en-us/aspnet/core/security/cors?view=aspnetcore-6.0
            // Default Policy
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    {
                        builder.WithOrigins(appOptions.FrontUrl)
                                            .AllowAnyHeader()
                                            .AllowAnyMethod();
                    });
            });


            return services;
        }
    }
}
