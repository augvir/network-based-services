using System;
using System.Threading;
using Grpc.Net.Client;

//this comes from GRPC generated code
using Services;


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
		private void Run() {
			var channel = GrpcChannel.ForAddress("http://127.0.0.1:5000");
			var client = new Service.ServiceClient(channel);

			var emptyRequest = new EmptyRequest();

			while (true)
			{
				Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss")}  I'm hungry again");

				bool fishSuccess = client.TryFishing(emptyRequest).Success;
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
