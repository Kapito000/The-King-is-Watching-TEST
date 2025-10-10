using Extensions;
using Infrastructure;
using TetrisFields;
using TetrisFields.Items.StaticData;
using UnityEngine;
using Zenject;

namespace ItemSpawners
{
	public sealed class BootItemSpawner : MonoBehaviour, IBootItemSpawner
	{
		[SerializeField] int _freeAssetCount = 6;

		[Inject] IItemSpawnService _itemSpawnService;
		[Inject] IItemDataCollection _itemDataCollection;
		
		[Inject(Id = InjectId.ItemsParent)]
		Transform _itemsParent;

		public void SpawnItems(ITetrisField field)
		{
			const int gridStep = 3;

			for (int i = 0; i < _freeAssetCount; i++)
			{
				var x = i % 2;
				var y = i % 3;
				var xOffset = x * gridStep;
				var yOffset = y * gridStep;

				var gridPos = new Vector2Int(xOffset, yOffset);
				var itemData = _itemDataCollection.Items.Random();
				_itemSpawnService.Spawn(field, itemData, gridPos);
			}
		}
	}
}