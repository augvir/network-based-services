using System.ServiceModel;

namespace Server
{

    [ServiceContract]
    public interface ILake
    {
        // adds a fish to the list of fishes in the lake
        [OperationContract]
        public int AddFish();

        // changes fish's hunger status
        [OperationContract]
        public bool ChangeHungry(int index, bool change);

        // changes fish's caught status
        [OperationContract]
        public bool ChangeCaught(int index);

        // performs fisherman's attempt at fishing
        [OperationContract]
        public bool TryToFish();
    }
}
