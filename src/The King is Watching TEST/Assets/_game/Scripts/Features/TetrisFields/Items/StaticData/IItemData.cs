using UnityEngine;

namespace TetrisFields.Items.StaticData
{
	public interface IItemData
	{
		Vector2Int[] Cells { get; }
	}
}