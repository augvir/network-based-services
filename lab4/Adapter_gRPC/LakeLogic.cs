using System;
using System.Threading;
using System.Net.Http;
using ServiceReference;

namespace Server
{
    /// <summary>
    /// network-independant class for server's logic, implementing lake's interface
    /// </summary>
    public class LakeLogic : ILake
	{
		readonly ReaderWriterLock _lock = new();

		/// <summary>
		/// client
		/// </summary>
		/// <returns>Client.</returns>
		private static readonly HttpClient Client = new();

		/// <summary>
		/// connection address
		/// <summary>
		private static readonly string Endpoint = "http://127.0.0.1:5000";

		/// <summary>
		/// adds a new fish to the list of fishes in the lake
		/// </summary>
		/// <returns> fish's ID in the context of lake </returns>
		public int AddFish()
		{
			ServiceClient clientRef = new ServiceClient(new HttpClient());
			var result = clientRef.AddFish();
			return result;
		}

		/// <summary>
		/// changes fish's hunger status
		/// </summary>
		/// <param name="index"> fish's ID </param>
		/// <param name="change"> new fish's hunger status </param>
		/// <returns> success of changing fish's hunger status: true / false </returns>
		public bool ChangeHungry(int index, bool change)
		{
			Console.WriteLine();
			Console.WriteLine($"Forwarding ChangeHungry() request to from GRPC client to REST server");
			Console.WriteLine();

			ServiceClient clientRef = new ServiceClient(new HttpClient());
			var result = clientRef.ChangeHungry(index, change);
			return result;
		}

		/// <summary>
		/// changes fish's caught status
		/// </summary>
		/// <param name="index"> fish's ID </param>
		/// <returns> success of changing fish's caught status: true / false </returns>
		public bool ChangeCaught(int index)
		{
			Console.WriteLine();
			Console.WriteLine($"Forwarding ChangeCaught() request to from GRPC client to REST server");
			Console.WriteLine();

			ServiceClient clientRef = new ServiceClient(new HttpClient());
			var result = clientRef.ChangeCaught(index);
			return result;
		}
	}
}