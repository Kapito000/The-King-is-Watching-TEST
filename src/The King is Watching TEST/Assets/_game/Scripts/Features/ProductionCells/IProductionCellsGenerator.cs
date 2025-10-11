using TetrisFields;

namespace ProductionCells
{
	public interface IProductionCellsGenerator
	{
		void Generate(ITetrisField field);
	}
}