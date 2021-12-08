using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers
{
	[Route("/service")]	[ApiController]

	// service; class must be marked public, otherwise ASP.NET core runtime will not find it
	public class ServiceController : ControllerBase
    {
		// service logic; use a singleton instance, since controller is instance-per-request
		private static readonly LakeLogic logic = new LakeLogic();

		[HttpPost]
		[Route("AddFish")]
		public ActionResult<int> AddFish()
		{
			return logic.AddFish();
		}

		[HttpPost]
		[Route("ChangeHungry")]
		public ActionResult<bool> ChangeHungry([FromBody] int fishIndex, bool hungerChange)
		{
			return logic.ChangeHungry(fishIndex, hungerChange);
		}

		[HttpPost]
		[Route("ChangeCaught")]
		public ActionResult<bool> ChangeCaught([FromBody] int fishIndex)
		{
			return logic.ChangeCaught(fishIndex);
		}

		[HttpPost]
		[Route("TryToFish")]
		public ActionResult<bool> TryToFish()
		{
			return logic.TryToFish();
		}
	}
}

