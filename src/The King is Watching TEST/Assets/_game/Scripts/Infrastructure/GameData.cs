using TetrisFields;

namespace Infrastructure
{
	public sealed class GameData : IGameData
	{
		public ITetrisField GameField { get; set; }
		public ITetrisField FreeItemsField { get; set; }
	}
}