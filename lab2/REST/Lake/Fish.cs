namespace Server
{
    /// <summary>
    /// a class for describing & manipulating fishes in the lake
    /// </summary>
    public class Fish
    {
        public bool IsHungry { get; set; }
        public bool IsCaught { get; set; }
        public int Id { get; set; }
        public Fish(bool isHungry, bool isCaught, int id)
        {
            IsHungry = isHungry;
            IsCaught = isCaught;
            Id = id;
        }

        /// <summary>
        /// a method for getting fish's hunger status
        /// </summary>
        /// <returns> hunger status: true / false </returns>
        public bool GetHungry()
        {
            return IsHungry;
        }

        /// <summary>
        /// a method for getting fish's caught status
        /// </summary>
        /// <returns> caught status: true / false </returns>
        public bool GetCaught()
        {
            return IsCaught;
        }

        /// <summary>
        /// a method for getting fish's ID in the lake
        /// </summary>
        /// <returns> fish's ID in the context of lake </returns>
        public int GetId()
        {
            return Id;
        }

        /// <summary>
        /// a method for setting fish's hunger status
        /// </summary>
        /// <param name="newHunger"> fish's new hunger status: true / false </param>
        public void SetHungry(bool newHunger)
        {
            IsHungry = newHunger;
        }

        /// <summary>
        /// a method for setting fish's caught status
        /// </summary>
        /// <param name="newCaught"> fish's new caught status: true / false </param>
        public void SetCaught(bool newCaught)
        {
            IsCaught = newCaught;
        }
    }
}
