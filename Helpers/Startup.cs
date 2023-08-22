using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore.Design;

namespace Simple_Api.Helpers
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Configuration.GetConnectionString("DefaultConnection");

            services.AddControllers();
            // services.AddMvcCore().AddApiExplorer();
            services.AddDbContext<Database>(options => options.UseNpgsql(connectionString));
            // services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Sample Api",
                    Description = "A simple example ASP.NET Core Web API",
                });
                
                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
            });

            // Add CORS policy for Dev environment
            // services.AddCors(options =>
            //     options.AddPolicy("AllowAll", builder =>
            //     {
            //         builder.AllowAnyOrigin()
            //             .AllowAnyHeader()
            //             .AllowAnyMethod();
            //     }
            // ));

            // // If using IIS:
            // services.Configure<IISServerOptions>(options =>
            // {
            //     options.AllowSynchronousIO = true;
            // });
        }

        

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.EnvironmentName == "Development" || env.EnvironmentName == "Production")
            {
                app.UseDeveloperExceptionPage()
                    .UseSwagger() // Enable middleware to serve generated Swagger as a JSON endpoint.
                    .UseSwaggerUI(c =>
                    {
                        // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
                        // specifying the Swagger JSON endpoint.
                        c.SwaggerEndpoint("v1/swagger.json", "API.Test API V1");
                    })
                    .UseCors("AllowAll");
            }
            app.UseStaticFiles();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Sample Api V3.0");
            });
        }
    }
}