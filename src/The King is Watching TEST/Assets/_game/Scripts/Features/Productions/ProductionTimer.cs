using System.Collections.Generic;
using GameResources;

namespace Productions
{
	public sealed class ProductionTimer : IProductionTimer
	{
		public int ProductionDataId { get; }
		public float TimeSpan { get; set; }
		public float TimeMoment { get; set; }
		public Dictionary<ResourceType, float> ResourceProductions =>
			_resourceProductions;

		Dictionary<ResourceType, float> _resourceProductions = new();

		public ProductionTimer(
			int productionDataId,
			IEnumerable<ResourceType> resourcesTypes)
		{
			ProductionDataId = productionDataId;

			foreach (var type in resourcesTypes)
			{
				_resourceProductions.Add(type, 0);
			}
		}
	}
}