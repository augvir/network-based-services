namespace Server
{
    public class ByValStruct
    {
        public int Index { get; set; }
        public bool Change { get; set; }
    }


    /// <summary>
    /// an interface class for server's (lake's) methods
    /// </summary>
    public interface ILake
    {
        /// <summary>
        /// adds a new fish to the list of fishes in the lake
        /// </summary>
        /// <returns> fish's ID in the context of lake </returns>
        public int AddFish();

        /// <summary>
        /// changes fish's hunger status
        /// </summary>
        /// <param name="index"> fish's ID </param>
        /// <param name="change"> new fish's hunger status </param>
        /// <returns> success of changing fish's hunger status: true / false </returns>
        public bool ChangeHungry(int index, bool change);

        /// <summary>
        /// changes fish's caught status
        /// </summary>
        /// <param name="index"> fish's ID </param>
        /// <returns> success of changing fish's caught status: true / false </returns>
        public bool ChangeCaught(int index);
    }
}
