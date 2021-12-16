using System.ServiceModel;

namespace Server
{
    /// <summary>
    /// an interface class for server's (lake's) methods with contracts
    /// </summary>
    [ServiceContract]
    public interface ILake
    {
        /// <summary>
        /// performs fisherman's attempt at fishing
        /// </summary>
        /// <returns> success of fishing: true / false </returns>
        [OperationContract]
        public bool TryToFish();
    }
}
