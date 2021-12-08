using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace Server
{
    public class Startup
    {
        // configuration data; injected via constructor
        public IConfiguration Configuration { get; }

        // constructor
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // this method gets called by the runtime; use this method to add services to the container
        public void ConfigureServices(IServiceCollection services)
        {
            // adds and configures MVC services            
            services
                .AddMvc(opts => opts.EnableEndpointRouting = false)
                    .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            // adds service for swagger document generation
            services.AddSwaggerDocument();
        }

        // this method gets called by the runtime; use this method to configure the HTTP request pipeline
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // adds MVC services			
            app.UseMvc();

            // adds swagger services, see http://localhost:5000/swagger/ and http://localhost:5000/swagger/v1/swagger.json
            app.UseOpenApi();
            app.UseSwaggerUi3();
        }
    }
}
