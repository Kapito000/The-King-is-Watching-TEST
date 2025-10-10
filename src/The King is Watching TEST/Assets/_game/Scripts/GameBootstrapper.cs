using Infrastructure;
using Input;
using ItemSpawners;
using TetrisFields;
using TetrisFields.Items;
using TetrisFields.Items.StaticData;
using UnityEngine;
using Zenject;

public class GameBootstrapper : MonoBehaviour
{
	[SerializeField] Vector2Int _startGridSize = new(8, 8);
	[Space]
	[SerializeField] Vector2Int _freeItemsGridSize = new(6, 9);

	[Inject(Id = InjectId.GameFieldParent)]
	Transform _gameFieldParent;
	[Inject(Id = InjectId.FreeItemsFieldParent)]
	Transform _fireeItemsFieldParent;
	[Inject(Id = InjectId.ItemsParent)]
	Transform _itemsParent;

	[Inject] IGameData _gameData;
	[Inject] IItemFactory _itemFactory;
	[Inject] IInputService _inputService;
	[Inject] IBootItemSpawner _bootItemSpawner;
	[Inject] ITetrisFieldFactory _fieldFactory;
	[Inject] IItemDataCollection _itemDataCollection;


	void Start()
	{
		CreateField();
		CreateFreeItemsField();
		CreateFreeItems();
		_inputService.Enable();
	}

	void CreateField()
	{
		_gameData.GameField =
			_fieldFactory.CreateField(_gameFieldParent, _startGridSize);
	}

	void CreateFreeItemsField()
	{
		_gameData.FreeItemsField =
			_fieldFactory.CreateField(_fireeItemsFieldParent, _freeItemsGridSize);
	}

	void CreateFreeItems()
	{
		_bootItemSpawner.SpawnItems(_gameData.FreeItemsField);
	}
}