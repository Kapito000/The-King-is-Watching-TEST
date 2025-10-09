using UnityEngine;

namespace TetrisField
{
	public interface ITetrisFieldFactory
	{
		ITetrisField CreateField(Transform parent, Vector2Int size);
	}
}