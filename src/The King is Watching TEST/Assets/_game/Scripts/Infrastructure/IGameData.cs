using TetrisFields;

namespace Infrastructure
{
	public interface IGameData
	{
		ITetrisField GameField { get; set; }
		ITetrisField FreeItemsField { get; set; }
	}
}