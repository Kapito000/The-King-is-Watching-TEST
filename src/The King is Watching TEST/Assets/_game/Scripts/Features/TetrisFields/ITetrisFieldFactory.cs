using UnityEngine;

namespace TetrisFields
{
	public interface ITetrisFieldFactory
	{
		ITetrisField CreateField(Transform parent, Vector2Int size);
	}
}