using System;
using System.Net;
using System.Threading;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;


namespace Server
{
	// application entry point
	public class Server
	{
		// program body
		void Run(string[] args)
		{
			// configure and start the server
			var builder = CreateWebHostBuilder(args);
			builder.Build().RunAsync();

			// suspend the main thread
			Console.WriteLine();
			Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss")}  LAKE HAS APPEARED");
			Console.WriteLine();
            while( true ) {
                Thread.Sleep(1000);
            }
		}

		// creates and configures web host builder
		public static IWebHostBuilder CreateWebHostBuilder(string[] args)
		{
			var builder =
				WebHost
					.CreateDefaultBuilder(args)
					.UseKestrel(options => {
						options.Listen(IPAddress.Loopback, 5000);
					})
					.UseStartup<Startup>();

			return builder;
		}

		// program's entry point
		static void Main(string[] args)
        {
            var self = new Server();
            self.Run(args);
        }
	}
}
