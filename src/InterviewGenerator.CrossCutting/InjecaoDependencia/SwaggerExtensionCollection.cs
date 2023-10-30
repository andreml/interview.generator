using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace InterviewGenerator.CrossCutting.InjecaoDependencia
{
    public static class SwaggerExtensionCollection
    {
        public static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                var xmlFile = "InterviewGenerator.Api.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath, includeControllerXmlComments: true);

                c.SwaggerDoc("v1", new OpenApiInfo()
                {
                    Title = "Interview Generator API",
                    Description = "APIs for IG"
                });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "Bearer {your token}",
                    Name = "Authorization",
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme { Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }},
                        Array.Empty<string>()
                    }
                });
            });

            return services;
        }
    }
}
