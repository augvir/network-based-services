using System;
using System.Threading.Tasks;
using Grpc.Core;

//this comes from GRPC generated code
using Services;


namespace Server
{
	// service; this is made to run as a singleton instance
	public class Service : Services.Service.ServiceBase
	{
		// service logic implementation
		private LakeLogic logic = new LakeLogic();

		public override Task<FishIndex> AddFish(EmptyRequest request, ServerCallContext context)
		{
			var result = new FishIndex { Index = logic.AddFish() };
			return Task.FromResult(result);
		}

		public override Task<ChangeSuccess> ChangeHungry(FishInfo info, ServerCallContext context)
		{
			var result = new ChangeSuccess { Success = logic.ChangeHungry(info.Index, info.Change) };
			return Task.FromResult(result);
		}

		public override Task<ChangeSuccess> ChangeCaught(FishIndex index, ServerCallContext context)
		{
			var result = new ChangeSuccess { Success = logic.ChangeCaught(index.Index) };
			return Task.FromResult(result);
		}

		public override Task<ChangeSuccess> TryFishing(EmptyRequest request, ServerCallContext context)
		{
			var result = new ChangeSuccess { Success = logic.TryToFish() };
			return Task.FromResult(result);
		}
	}
}