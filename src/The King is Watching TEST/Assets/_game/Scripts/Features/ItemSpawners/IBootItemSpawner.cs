using TetrisFields;

namespace ItemSpawners
{
	public interface IBootItemSpawner
	{
		void SpawnItems(ITetrisField field);
	}
}