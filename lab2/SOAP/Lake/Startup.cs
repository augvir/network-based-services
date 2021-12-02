using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using SoapCore;

namespace Server
{
	public class Startup
	{
		// this method gets called by the runtime; use this method to add services to the container
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddSoapCore();
			services.TryAddSingleton<Service>();
			services.AddMvc();
		}

		// this method gets called by the runtime; use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			app.UseRouting();

			app.UseEndpoints(eps => {
				eps.UseSoapEndpoint<Service>("/Service", new SoapEncoderOptions(), SoapSerializer.DataContractSerializer);
			});

		}
	}
}
