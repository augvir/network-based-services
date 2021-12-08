﻿using System;
using System.Threading;

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
			
			try
            {
				// connect to server
				var lake = new LakeClient();
				var random = new Random();

				int index = lake.AddFish();
				Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss")}  I'm the #{index + 1} fish in the lake");
				Console.WriteLine();

				while (true)
				{
					bool isHungry = random.Next(0, 2) > 0;              // fish's change in hunger is randomly chosen

					bool changeSuccess = lake.ChangeHungry(index, isHungry);
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

						bool releaseSuccess = lake.ChangeCaught(index);
						if (releaseSuccess == true)
							Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss")}  I was REVIVED :)");
					}
				}
			}
			catch (Exception e)
            {
				Console.WriteLine();
				Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss")} {e}, Unhandled exception caught. Restarting");
				Console.WriteLine();

				// prevent console spamming
				Thread.Sleep(2000);
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
