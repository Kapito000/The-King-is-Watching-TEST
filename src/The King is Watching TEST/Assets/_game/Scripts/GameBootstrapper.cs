using Extensions;
using Infrastructure;
using Input;
using TetrisFields;
using TetrisFields.Items;
using TetrisFields.Items.StaticData;
using UnityEngine;
using Zenject;

public class GameBootstrapper : MonoBehaviour
{
	[SerializeField] Vector2Int _startGridSize = new(8, 8);
	[Space]
	[SerializeField] int _freeAssetCount = 6;
	[SerializeField] Vector2Int _freeItemsGridSize = new(6, 9);

	[Inject(Id = InjectId.GameFieldParent)]
	Transform _gameFieldParent;
	[Inject(Id = InjectId.FreeItemsFieldParent)]
	Transform _fireeItemsFieldParent;
	[Inject(Id = InjectId.ItemsParent)]
	Transform _itemsParent;

	[Inject] IItemFactory _itemFactory;
	[Inject] IInputService _inputService;
	[Inject] ITetrisFieldFactory _fieldFactory;
	[Inject] IItemDataCollection _itemDataCollection;

	ITetrisField _gameField;
	ITetrisField _freeItemsField;

	void Start()
	{
		CreateField();
		CreateFreeItemsField();
		CreateFreeItems();
		_inputService.Enable();
	}

	void CreateField()
	{
		_gameField = _fieldFactory.CreateField(_gameFieldParent, _startGridSize);
	}

	void CreateFreeItemsField()
	{
		_freeItemsField =
			_fieldFactory.CreateField(_fireeItemsFieldParent, _freeItemsGridSize);
	}

	void CreateFreeItems()
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
			var item = _itemFactory.CreateItem(_itemsParent, itemData.Cells);

			_freeItemsField.PutItem(item, gridPos);
		}
	}
}