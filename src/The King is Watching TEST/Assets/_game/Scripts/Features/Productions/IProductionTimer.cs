using System.Collections.Generic;
using GameResources;

namespace Productions
{
	public interface IProductionTimer
	{
		int ProductionDataId { get; }
		float TimeSpan { get; set; }
		float TimeMoment { get; set; }
		Dictionary<ResourceType, float> ResourceProductions { get; }
	}
}