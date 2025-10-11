using UnityEngine;

namespace ProductionCells.StaticData
{
	public interface IProductionCellData
	{
		Color Color { get; }
		float ProductionModifier { get; }
		float ProductionTimer { get; }
	}
}