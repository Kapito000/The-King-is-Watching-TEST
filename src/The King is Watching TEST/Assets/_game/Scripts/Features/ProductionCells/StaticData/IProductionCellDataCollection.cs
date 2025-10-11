using System.Collections.Generic;

namespace ProductionCells.StaticData
{
	public interface IProductionCellDataCollection : IEnumerable<ProductionCellData>
	{
		ProductionCellData[] ProductionData { get; }
	}
}