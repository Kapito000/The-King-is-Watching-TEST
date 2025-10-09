using Extensions;
using UnityEngine;
using Zenject;

namespace TetrisFields.Items
{
	public sealed class ItemFactory : IItemFactory
	{
		[Inject] Item _itemPrefab;
		[Inject] ItemCell _itemCellPrefab;
		[Inject] IInstantiator _instantiator;

		public Item CreateItem(Transform parent, Vector2Int[] cells)
		{
			var item = _instantiator
				.InstantiatePrefabForComponent<Item>(_itemPrefab, parent.position,
					Quaternion.identity, parent)
				.With(i => i.Cells = cells);

			foreach (var cellPos in item.Cells)
			{
				CreateCell(cellPos, item);
			}

			return item;
		}

		ItemCell CreateCell(Vector2Int cellPos, Item item)
		{
			var pos = item.transform.position + cellPos.AsVector3();
			var parent = item.transform;

			var cell = _instantiator
				.InstantiatePrefabForComponent<ItemCell>(_itemCellPrefab, pos,
					Quaternion.identity, parent)
				.With(c => c.SetItem(item))
				.With(item.AddItemCell);
			
			return cell;
		}
	}
}