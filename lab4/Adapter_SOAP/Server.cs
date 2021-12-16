using System;
using System.Net;
using System.Threading;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;


namespace Server
{
	/// <summary>
	/// class of application's entry point
	/// </summary>
	public class Server
	{
		/// <summary>
		/// program's body
		/// </summary>
		/// <param name="args"> program's arguments </param>
		void Run(string[] args)
		{
			// configure and start the server
			var builder = CreateWebHostBuilder(args);
			builder.Build().RunAsync();

			// suspend the main thread
			Console.WriteLine();
			Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss")}  SOAP ADAPTER SERVER IS ON");
			Console.WriteLine();
            while( true ) {
                Thread.Sleep(1000);
            }
		}

		/// <summary>
        /// a method for creating and configuring web host builder
        /// </summary>
        /// <param name="args"> arguments </param>
        /// <returns> IWebHostBuilder object </returns>
		public static IWebHostBuilder CreateWebHostBuilder(string[] args)
		{
			var builder =
				WebHost
					.CreateDefaultBuilder(args)
					.UseKestrel(options => {
						options.Listen(IPAddress.Loopback, 5002);
					})
					.UseStartup<Startup>();

			return builder;
		}

		/// <summary>
		/// program's entry point
		/// </summary>
		/// <param name="args"> program's arguments </param>
		static void Main(string[] args)
        {
            var self = new Server();
            self.Run(args);
        }
	}
}
