using System;
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
		private void Run() {
			var client = new LakeClient();

			while (true)
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
