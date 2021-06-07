using Google.Apis.Auth.AspNetCore3;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using NLog;
using salesNowBackend.ActionFilter;
using salesNowBackend.Extinsions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace salesNowBackend
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            var fullPath = Directory.GetFiles(Path.GetDirectoryName(Assembly.GetEntryAssembly()?.Location))
                     .FirstOrDefault(x => x.EndsWith("nlog.config", StringComparison.OrdinalIgnoreCase));
            Configuration = configuration;
            //LogManager.LoadConfiguration(fullPath);
            // test
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(option =>
            {
                option.SuppressModelStateInvalidFilter = true;
            });
           
            services.ConfigureCors();
            services.ConfigureLogger();
            services.ConfigureFirestoreDb();
            services.ConfigureCompany();
            services.ConfigureBusinessOpportunity();
            services.ConfigureActivity();
            services.ConfigureContactPerson();
            services.AddScoped<ValidationFilterAttribute>();
            services.AddScoped<ValidationCompanyExistsAttribute>();
            services.AddScoped<ValidationActivityExistsAttribute>();
            services.AddScoped<ValidationContactPersonExistsAttribute>();
            services.AddScoped<ValidationBusinessOpportunityExistsAttribute>();
            services.AddAutoMapper(typeof(Startup));
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "salesNowBackend", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
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
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
               .AddJwtBearer(jwtOptions =>
               {
                jwtOptions.Authority = @"https://login.microsoftonline.com/geshdo.com";
                jwtOptions.TokenValidationParameters.ValidateIssuer = true;
                jwtOptions.TokenValidationParameters.ValidateAudience = true;
                jwtOptions.TokenValidationParameters.ValidIssuer = "https://login.microsoftonline.com/cd20e4c9-f82c-4d3e-9224-90f2bc4be1a0/v2.0";
                jwtOptions.TokenValidationParameters.ValidAudience = "1a602afd-b047-4730-892f-715f551f9c97";
               });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
              
            }
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "salesNowBackend v1"));
            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors("CorsPolicy"); 
            app.UseAuthentication();
            app.UseAuthorization();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", context =>
                {
                    context.Response.Redirect("/swagger");
                    return Task.CompletedTask;
                });
                endpoints.MapControllers();
            });
        }
    }
}
