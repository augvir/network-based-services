using System;
using System.Net.Http;
using System.Threading;
using ServiceReference;

namespace Client
{
	class Client
	{
		private void Run()
		{
			//connect to server
			var client = new ServiceClient(new HttpClient());

			var random = new Random();

			int index = client.AddFish();
			Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss")}  I'm the #{index + 1} fish in the lake");
			Console.WriteLine();

			while (true)
			{
				try
                {
					bool isHungry = random.Next(0, 2) > 0;              // fish's change in hunger is randomly chosen

					bool changeSuccess = client.ChangeHungry(index, isHungry);
					if (changeSuccess == true)
					{
						if (isHungry)
							Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss")}  I'm hungry");
						else
							Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss")}  I'm NOT hungry");
						Console.WriteLine();

						Thread.Sleep(4000);
					}
					else
					{                                                   // if fish's hunger couldn't change, the fish is caught
						Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss")}  I am CAUGHT :(");
						Console.WriteLine();

						Thread.Sleep(5000);                             // eventually the fish comes back alive

						bool releaseSuccess = client.ChangeCaught(index);
						if (releaseSuccess == true)
							Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss")}  I was REVIVED :)");
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

		static void Main(string[] args)
		{
			var self = new Client();
			self.Run();
		}
	}
}
