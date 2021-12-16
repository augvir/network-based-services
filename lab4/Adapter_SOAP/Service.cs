namespace Server
{
	/// <summary>
	/// service's class that is made to run as a singleton instance
	/// </summary>
	public class Service : ILake
	{
		// service logic implementation
		private LakeLogic logic = new LakeLogic();

		public bool TryToFish()
        {
			lock(logic)
            {
				return logic.TryToFish();
            }
			
        }
	}
}