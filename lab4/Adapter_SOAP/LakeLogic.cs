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
		/// performs fisherman's attempt at fishing
		/// </summary>
		/// <returns> success of fishing: true / false </returns>
		public bool TryToFish ()
        {
			Console.WriteLine();
			Console.WriteLine($"Forwarding TryToFish() request to from SOAP client to REST server");
			Console.WriteLine();

			ServiceClient clientRef = new ServiceClient(new HttpClient());
			var result = clientRef.TryToFish();
			return result;
		}
	}
}