using LugaresAPI.Data;
using LugaresAPI.Mapper;
using LugaresAPI.Repository;
using LugaresAPI.Repository.IRepository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Linq;
using System.Threading.Tasks;

namespace LugaresAPI
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
            //Conexion a Base De Datos
            services.AddDbContext<ConexionBD>(options => options.UseSqlServer(Configuration.GetConnectionString("Conexion")));

            services.AddScoped<ILugarRepository, LugarRepository>();

            services.AddAutoMapper(typeof(LugaresMappings));

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("InfoAPILugares",
                    new Microsoft.OpenApi.Models.OpenApiInfo()
                    {
                        Title = "Lugares API",
                        Version = "1",
                        Description = "Desarrollo de una API",
                        Contact = new Microsoft.OpenApi.Models.OpenApiContact()
                        {
                            Email = "betzaithaburto@gmail.com",
                            Name = "Betzait Aburto"
                        },
                        License = new Microsoft.OpenApi.Models.OpenApiLicense()
                        {
                            Name = "MIT License",
                            Url = new Uri("https://en.wikipedia.org/wiki/MIT_License")
                        }
                    });

                var xmlComentarios = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlComentariosFullPath = Path.Combine(AppContext.BaseDirectory, xmlComentarios);
                options.IncludeXmlComments(xmlComentariosFullPath);

            });

            

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseSwagger();

            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/InfoAPILugares/swagger.json", "Lugares API");
                options.RoutePrefix = "";
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
