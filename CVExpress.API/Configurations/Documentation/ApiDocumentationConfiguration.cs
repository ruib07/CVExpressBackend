﻿using CVExpress.API.Constants;
using Microsoft.OpenApi.Models;

namespace CVExpress.API.Configurations.Documentation
{
    #region API Documentation Config
    
    public static class ApiDocumentationConfiguration
    {
        public static void AddCustomApiDocumentation(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc(ApiConstants.DocumentationGroupAuthentication, new OpenApiInfo
                {
                    Title = "CVExpress Api - Authentication",
                    Description = "Authentication Services",
                });
                options.SwaggerDoc(ApiConstants.DocumentationGroupEntity, new OpenApiInfo
                {
                    Title = "CVExpress Api - Entity",
                    Description = "Entity Services",
                });
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement()
                    {
                        {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                        },
                        new List<string>()
                    }
                });
            });
        }

        public static void UseCustomApiDocumentation(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint($"/swagger/{ApiConstants.DocumentationGroupAuthentication}/swagger.json", "CVExpress Api - Authentication");
                options.SwaggerEndpoint($"/swagger/{ApiConstants.DocumentationGroupEntity}/swagger.json", "CVExpress Api - Entity");
            });
        }
    }

    #endregion
}
