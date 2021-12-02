using System;
using System.Threading;


namespace Server
{
	// application entry point
	public class Server
	{
		// program body
		void Run()
		{
			while (true)
			{
				try
				{
					// start the service
					var service = new Service();

					// suspend the main thread
					Console.WriteLine();
					Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss")}  LAKE HAS APPEARED");
					Console.WriteLine();
					while (true)
					{
						Thread.Sleep(1000);
					}
				}
				catch (Exception e)
				{
					Console.WriteLine();
					Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss")} {e}, Unhandled exception caught. Server will now restart");
					Console.WriteLine();

					//prevent console spamming
					Thread.Sleep(2000);
				}
			}
		}

        // program's entry point
        static void Main(string[] args)
        {
			var self = new Server();
			self.Run();
		}
	}
}
