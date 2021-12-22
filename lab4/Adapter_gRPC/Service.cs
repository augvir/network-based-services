using System;
using System.Threading.Tasks;
using Grpc.Core;

// this comes from GRPC generated code
using Services;

namespace Server
{
    /// <summary>
    /// service's class that is made to run as a singleton instance
    /// connects server's logic to RPC methods
    /// </summary>
    public class Service : Services.Service.ServiceBase
    {
        // implementation of server's logic
        private LakeLogic logic = new LakeLogic();

        public override Task<FishIndex> AddFish(EmptyRequest request, ServerCallContext context)
        {
            lock (logic)
            {
                var result = new FishIndex { Index = logic.AddFish() };
                return Task.FromResult(result);
            }
        }

        public override Task<ChangeSuccess> ChangeHungry(FishInfo info, ServerCallContext context)
        {
            lock (logic)
            {
                var result = new ChangeSuccess { Success = logic.ChangeHungry(info.Index, info.Change) };
                return Task.FromResult(result);
            }
        }

        public override Task<ChangeSuccess> ChangeCaught(FishIndex index, ServerCallContext context)
        {
            lock (logic)
            {
                var result = new ChangeSuccess { Success = logic.ChangeCaught(index.Index) };
                return Task.FromResult(result);
            }
        }
    }
}