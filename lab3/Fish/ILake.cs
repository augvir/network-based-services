namespace Client
{
    public interface ILake
    {
        // adds a fish to the list of fishes in the lake
        public int AddFish();

        // changes fish's hunger status
        public bool ChangeHungry(int index, bool change);

        // changes fish's caught status
        public bool ChangeCaught(int index);

        // performs fisherman's attempt at fishing
        public bool TryToFish();
    }
}
