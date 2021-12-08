using System;
using System.Threading;
using System.Collections.Generic;

namespace Server
{
	/// <summary>
	/// network-independant class for server's logic, implementing lake's interface
	/// </summary>
	public class LakeLogic : ILake
	{
		readonly ReaderWriterLock _lock = new();

		// a list of fishes that are in the lake (dead or alive)
		List<Fish> fishesInLake = new List<Fish>();
		private int fishId = 0;

		/// <summary>
		/// adds a new fish to the list of fishes in the lake
		/// </summary>
		/// <returns> fish's ID in the context of lake </returns>
		public int AddFish()
        {
            try
            {
				_lock.AcquireWriterLock(-1);

				Fish newFish = new Fish(false, false, ++fishId);
				fishesInLake.Add(newFish);								// adds a fish to the list of fishes in the lake

				Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss")}  CURRENTLY: {fishesInLake.Count} fishes in the lake");
				Console.WriteLine();

				return (fishId - 1);
            }
			catch (Exception)
			{
				Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss")}  Failed to acquire writer lock");
				throw;
			}
			finally
			{
				_lock.ReleaseWriterLock();
			}

		}

		/// <summary>
		/// changes fish's hunger status
		/// </summary>
		/// <param name="index"> fish's ID </param>
		/// <param name="change"> new fish's hunger status </param>
		/// <returns> success of changing fish's hunger status: true / false </returns>
		public bool ChangeHungry(int index, bool change)
        {
			try
			{
				_lock.AcquireWriterLock(-1);

				if (fishesInLake[index].GetCaught() == true)			// if a fish is caught, changing hunger is unavailable
					return false;
				else
				{
					bool oldHunger = fishesInLake[index].GetHungry();

					fishesInLake[index].SetHungry(change);				// fish's hunger status is changed

					Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss")}  Fish's #{fishesInLake[index].GetId()} hunger changed from {oldHunger} to {change}");
					Console.WriteLine();

					return true;
				}
			}
			catch (Exception)
			{
				Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss")}  Failed to acquire writer lock");
				throw;
			}
			finally
			{
				_lock.ReleaseWriterLock();
			}
        }

		/// <summary>
		/// changes fish's caught status
		/// </summary>
		/// <param name="index"> fish's ID </param>
		/// <returns> success of changing fish's caught status: true / false </returns>
		public bool ChangeCaught (int index)
        {
			try
			{
				_lock.AcquireWriterLock(-1);

				fishesInLake[index].SetCaught(false);					// fish's caught status is changed to it being revived

				return true;
			}
			catch (Exception)
			{
				Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss")}  Failed to acquire writer lock");
				throw;
			}
			finally
			{
				_lock.ReleaseWriterLock();
			}
		}

		/// <summary>
		/// performs fisherman's attempt at fishing
		/// </summary>
		/// <returns> success of fishing: true / false </returns>
		public bool TryToFish ()
        {
			try
			{
				_lock.AcquireWriterLock(-1);

				if (fishesInLake.Count == 0)                            // if there are no fishes in the lake, fishing is unavailable
				{
					Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss")}  There are NO FISHES to catch");
					Console.WriteLine();
					return false;
				}

				List<Fish> aliveFishes = new List<Fish>();

				for (int i = 0; i < fishesInLake.Count; i++)			// currently alive fishes are added to another list
                {
					if (fishesInLake[i].GetCaught() == false)
						aliveFishes.Add(fishesInLake[i]);
                }
				
				if (aliveFishes.Count == 0)                             // if there are no alive fishes in the lake, fishing is unavailable
				{
					Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss")}  There are NO FISHES to catch");
					Console.WriteLine();
					return false;
                }
				
				var random = new Random();
				int fishToCatch = random.Next(0, (aliveFishes.Count));	// an alive fish is randomly chosen for a fisherman

				Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss")}  A fisherman is trying to catch fish #{aliveFishes[fishToCatch].GetId()}");

				if (aliveFishes[fishToCatch].GetHungry() == true)		// if the fish is hungry, it gets caught
				{
					aliveFishes[fishToCatch].SetCaught(true);

					Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss")}  Fish #{aliveFishes[fishToCatch].GetId()} was CAUGHT");
					Console.WriteLine();

					return true;
				}
				else
				{
					Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss")}  Fish #{aliveFishes[fishToCatch].GetId()} was NOT caught");
					Console.WriteLine();
					return false;
				}
			}
			catch (Exception)
			{
				Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss")}  Failed to acquire writer lock");
				throw;
			}
			finally
			{
				_lock.ReleaseWriterLock();
			}		
		}
	}
}