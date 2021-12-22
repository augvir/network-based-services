using System;
using System.Threading;
using Grpc.Net.Client;

//this comes from GRPC generated code
using Services;

namespace Client
{
	/// <summary>
	/// client's (fish's) class
	/// </summary>
	class Client
	{
		/// <summary>
		/// class' body
		/// </summary>
		private void Run() {
			var channel = GrpcChannel.ForAddress("http://127.0.0.1:5001");
			var client = new Service.ServiceClient(channel);

			var random = new Random();
			var emptyRequest = new EmptyRequest();

			int index = client.AddFish(emptyRequest).Index;
			Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss")}  I'm the #{index + 1} fish in the lake");
			Console.WriteLine();

			while (true)
			{
				bool isHungry = random.Next(0, 2) > 0;				// fish's change in hunger is randomly chosen

				bool changeSuccess = client.ChangeHungry(new FishInfo { Index = index, Change = isHungry }).Success;
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
                {													// if fish's hunger couldn't change, the fish is caught
					Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss")}  I am CAUGHT :(");
					Console.WriteLine();
					
					Thread.Sleep(5000);								// eventually the fish comes back alive

					bool releaseSuccess = client.ChangeCaught(new FishIndex { Index = index }).Success;
					if (releaseSuccess == true)
						Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss")}  I was REVIVED :)");
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
