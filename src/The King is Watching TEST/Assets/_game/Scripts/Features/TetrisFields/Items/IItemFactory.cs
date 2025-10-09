using UnityEngine;

namespace TetrisFields.Items
{
	public interface IItemFactory
	{
		Item CreateItem(Transform parent, Vector2Int[] cells);
	}
}