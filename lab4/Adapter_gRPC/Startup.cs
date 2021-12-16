﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Server
{
	public class Startup
	{
		// this method gets called by the runtime; use this method to add services to the container
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddGrpc();
			services.AddSingleton(new Service());	//if you do not use this, service will be instance-per-call
		}

		// this method gets called by the runtime; use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			app.UseRouting();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapGrpcService<Service>();
			});
		}
	}
}
