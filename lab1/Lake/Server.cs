using System;
using System.Threading;
using System.Net;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;


namespace Server
{
	// application entry point
	public class Server
	{
		// program body
		void Run(string[] args)
		{
			// configure and start the server
			CreateHostBuilder(args).Build().RunAsync();

			// suspend the main thread
			Console.WriteLine();
			Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss")}  LAKE HAS APPEARED");
			Console.WriteLine();
            while( true ) {
                Thread.Sleep(1000);
            }
		}

		// configures and runs the server
		public static IHostBuilder CreateHostBuilder(string[] args) 
		{
			return 
				Host
					.CreateDefaultBuilder(args)
					.ConfigureWebHostDefaults(hcfg => {
						hcfg.UseKestrel();
						hcfg.ConfigureKestrel(kcfg => {
							kcfg.Listen(IPAddress.Loopback, 5000);
						});
						hcfg.UseStartup<Startup>();
					});
		}

        // program's entry point
        static void Main(string[] args)
        {
            var self = new Server();
            self.Run(args);
        }
	}
}
