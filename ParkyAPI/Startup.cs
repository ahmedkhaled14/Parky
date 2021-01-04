using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.Xml;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.VisualBasic;
using ParkyAPI.Data;
using ParkyAPI.Mapper;
using ParkyAPI.Repository;
using ParkyAPI.Repository.IRepository;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ParkyAPI
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
            services.AddControllers();

            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("Constr")));

            services.AddAutoMapper(typeof(ParkyMappings));
            services.AddApiVersioning(Options =>
            {
                Options.AssumeDefaultVersionWhenUnspecified = true;
                Options.DefaultApiVersion = new ApiVersion(1,0);
                Options.ReportApiVersions = true;


            });

            services.AddScoped<INationalParkRepository, NationalParkRepository>();
            services.AddScoped<ITrailRepository, TrailRepository>();
            services.AddVersionedApiExplorer(Options => Options.GroupNameFormat = "'Version Number'VVV");
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
            services.AddSwaggerGen();
        

            

             //services.AddSwaggerGen(Options =>
             //{

             //    Options.SwaggerDoc("ParkyOpenApiSpec", new Microsoft.OpenApi.Models.OpenApiInfo()
             //    {
             //        Title = "Parky API ",
             //        Version = "1",
             //        Description = "Parky API Documentation",
             //        Contact = new Microsoft.OpenApi.Models.OpenApiContact()
             //        {
             //            Name = "Ahmed Khaled",
             //            Email="elgaliador14@gmail.com"
             //        }

             //    });
                


                 //Options.SwaggerDoc("ParkyOpenApiSpecTrails", new Microsoft.OpenApi.Models.OpenApiInfo()
                 //{
                 //    Title = "Parky API Trails",
                 //    Version = "1",
                 //    Description = "Parky API Trails Documentation",
                 //    Contact = new Microsoft.OpenApi.Models.OpenApiContact()
                 //    {
                 //        Name = "Ahmed Khaled",
                 //        Email = "elgaliador14@gmail.com"
                 //    }

                 //});

            //     var XmlCommentsFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            //     var xmlCommentFullPath = Path.Combine(AppContext.BaseDirectory, XmlCommentsFile);
            //     Options.IncludeXmlComments(xmlCommentFullPath);

            // }    
            //);

           
            


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseSwagger();

            app.UseSwaggerUI(Options =>
            {
                //Options.SwaggerEndpoint("/swagger/ParkyOpenApiSpec/swagger.json", "parky api ");
                // Options.SwaggerEndpoint("/swagger/ParkyOpenApiSpecTrails/swagger.json", "parky api Trails");

                foreach (var desc in provider.ApiVersionDescriptions)
                {
                    Options.SwaggerEndpoint($"/swagger/{desc.GroupName}/swagger.json",desc.GroupName.ToUpperInvariant());

                }

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
