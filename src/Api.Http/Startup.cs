using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Repository;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.IO;
using System.Linq;

namespace Api.Http
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            Bootstrapper.Initialize(
                services,
                new BootstrapperOptions
                {
                    ConnectionString = Configuration.GetConnectionString("HackOverflowConnection"),
                    DatabaseName = "registrations"
                });

            services.AddSwaggerGen(swagger =>
            {
                swagger.SwaggerDoc(
                    "HackOverflowApi",
                    new Info
                    {
                        Title = "HackOverflow Api",
                        Version = "1.0.0"
                    });

                Directory.GetFiles(
                        AppContext.BaseDirectory,
                        "*.xml",
                        SearchOption.TopDirectoryOnly)
                    .ToList()
                    .ForEach(x =>
                    {
                        swagger.IncludeXmlComments(x);
                    });

                swagger.DescribeAllEnumsAsStrings();
            });

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", p =>
                {
                    p.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                });
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/HackOverflowApi/swagger.json", "HackOverflow API V1");
                c.RoutePrefix = string.Empty;
            });

            app.UseCors("AllowAll");

            app.UseMvc();
        }
    }
}
