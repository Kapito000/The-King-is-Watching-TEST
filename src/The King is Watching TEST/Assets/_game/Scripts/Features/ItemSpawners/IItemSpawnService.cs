using TetrisFields;
using TetrisFields.Items;
using TetrisFields.Items.StaticData;
using UnityEngine;

namespace ItemSpawners
{
	public interface IItemSpawnService
	{
		Item Spawn(ITetrisField field, ItemData itemData,
			Vector2Int fieldPos);
	}
}