using System;
using System.Net.Http;
using System.Threading;
using ServiceReference;

namespace Client
{
	/// <summary>
	/// client's (fisherman's) class
	/// </summary>
	class Client
	{
		/// <summary>
		/// class' body
		/// </summary>
		private void Run()
		{
			//connect to server
			var client = new ServiceClient(new HttpClient());

			while (true)
			{
				try
                {
					Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss")}  I'm hungry again");

					bool fishSuccess = client.TryToFish();
					if (fishSuccess == true)
					{
						Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss")}  I CAUGHT a fish :)");
						Console.WriteLine();

						Thread.Sleep(3000);
					}
					else
					{
						Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss")}  I didn't catch a fish :(");
						Console.WriteLine();

						Thread.Sleep(4000);
					}
                }
				catch (Exception e)
				{
					//log exceptions
					Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss")}  {e} Unhandled exception caught. Restarting.");

					//prevent console spamming
					Thread.Sleep(2000);
				}
			}
		}

		/// <summary>
		/// class' entry point
		/// </summary>
		/// <param name="args"> arguments </param>
		static void Main(string[] args)
		{
			var self = new Client();
			self.Run();
		}
	}
}
