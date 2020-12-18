using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.CrossCutting.DependecyInjection;
using Api.Data.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace application
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            ConfigureService.ConfigureDependeciesService(services);
            ConfigureRepository.configureDependenceRepository(services);
            services.AddControllers();
            services.AddSwaggerGen(
                configuration =>
                {
                    configuration.SwaggerDoc("v1", new OpenApiInfo
                    {
                        Version = "v1",
                        Title = "Aprendendo DDD",
                        Description = "Arquitetura DDD",
                        TermsOfService = new Uri("http://www.cin.ufpe.br/~ans3"),
                        Contact = new OpenApiContact
                        {
                            Name = "Adriano Santana",
                            Email = "ans3@cin.ufpe.br",
                            Url = new Uri("http://www.cin.ufpe.br/~ans3")
                        }
                    });
                }
            );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(configuration =>
            {
                configuration.SwaggerEndpoint("/swagger/v1/swagger.json", "Dotnet DDD");
                configuration.RoutePrefix = string.Empty;
            });


            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
