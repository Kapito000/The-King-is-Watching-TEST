using Extensions;
using Infrastructure;
using TetrisFields.Items.StaticData;
using UnityEngine;
using Zenject;

namespace ItemSpawners
{
	public sealed class FreeItemSpawner : IFreeItemSpawner
	{
		[Inject] IGameData _gameData;
		[Inject] IItemSpawnService _itemSpawnService;
		[Inject] IItemDataCollection _itemDataCollection;

		[Inject(Id = InjectId.ItemsParent)]
		Transform _itemsParent;

		public void SpawnItems()
		{
			var field = _gameData.FreeItemsField;
			var itemData = _itemDataCollection.Items.Random();

			foreach (var fieldCell in field.AllFields())
			{
				var fieldPos = fieldCell.FieldPos;

				if (field.CanPutItem(itemData.Cells, fieldPos) == false)
					continue;

				_itemSpawnService.Spawn(field, itemData, fieldPos);
				return;
			}
		}
	}
}