namespace Server
{
	// service; this is made to run as a singleton instance
	public class Service : ILake
	{
		// service logic implementation
		private LakeLogic logic = new LakeLogic();

		public int AddFish()
        {
			return logic.AddFish();
        }

		public bool ChangeHungry(int fishIndex, bool hungerChange)
        {
			return logic.ChangeHungry(fishIndex, hungerChange);
        }

		public bool ChangeCaught(int fishIndex)
		{
			return logic.ChangeCaught(fishIndex);
		}

		public bool TryToFish()
        {
			return logic.TryToFish();
        }
	}
}