using Infrastructure;
using TetrisFields;
using TetrisFields.Items;
using TetrisFields.Items.StaticData;
using UnityEngine;
using Zenject;

namespace ItemSpawners
{
	public sealed class ItemSpawnService : IItemSpawnService
	{
		[Inject] IItemFactory _itemFactory;
		
		[Inject(Id = InjectId.ItemsParent)]
		Transform _itemsParent;

		public Item Spawn(ITetrisField field, ItemData itemData,
			Vector2Int fieldPos)
		{
			var item = _itemFactory.CreateItem(_itemsParent, itemData.Cells);
			field.PutItem(item, fieldPos);
			return item;
		}
	}
}