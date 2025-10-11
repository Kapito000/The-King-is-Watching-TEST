using Extensions;
using GameResources.StaticData;
using UnityEngine;
using Zenject;

namespace TetrisFields.Items
{
	public sealed class ItemFactory : IItemFactory
	{
		[Inject] Item _itemPrefab;
		[Inject] ItemCell _itemCellPrefab;
		[Inject] IInstantiator _instantiator;
		[Inject] IResourceCellDataCollection _resourceCellData;

		public Item CreateItem(Transform parent, Vector2Int[] cells)
		{
			var item = _instantiator
				.InstantiatePrefabForComponent<Item>(_itemPrefab, parent.position,
					Quaternion.identity, parent)
				.With(i => i.Cells = cells);

			var resourceCellIndex = Random.Range(0, item.Cells.Length);
			for (var i = 0; i < item.Cells.Length; i++)
			{
				var cellPos = item.Cells[i];
				var itemCell = CreateCell(cellPos, item);

				ProcessResourceCellCreation(i, resourceCellIndex, itemCell, item,
					cellPos);
			}

			return item;
		}

		void ProcessResourceCellCreation(int index, int resourceCellIndex,
			ItemCell itemCell, Item item, Vector2Int cellPos)
		{
			if (index != resourceCellIndex)
				return;

			var resourcesData = _resourceCellData.Data.Random();
			CreateResourceCell(itemCell, resourcesData);

			item.SetResourceCell(new ResourceCellInfo()
			{
				Pos = cellPos,
				Type = resourcesData.Type,
			});
		}

		ItemCell CreateCell(Vector2Int cellPos, Item item)
		{
			var pos = item.transform.position + cellPos.AsVector3();
			var parent = item.transform;

			var cell = _instantiator
				.InstantiatePrefabForComponent<ItemCell>(_itemCellPrefab, pos,
					Quaternion.identity, parent)
				.With(item.AddItemCell);

			return cell;
		}

		ResourceCell CreateResourceCell(ItemCell itemCell, ResourcesData data)
		{
			var prefab = data.Prefab;
			var pos = itemCell.transform.position;
			var parent = itemCell.transform;

			return _instantiator
					.InstantiatePrefabForComponent<ResourceCell>(prefab, pos,
						Quaternion.identity, parent)
					.With(c => c.Init(itemCell))
				;
		}
	}
}