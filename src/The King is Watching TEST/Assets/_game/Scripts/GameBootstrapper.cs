using Infrastructure;
using Input;
using ItemSpawners;
using ProductionCells;
using Productions;
using TetrisFields;
using TetrisFields.Items;
using TetrisFields.Items.StaticData;
using UI.ResourcesView;
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
	[Inject] IProductionSystem _productionSystem;
	[Inject] ITetrisFieldFactory _fieldFactory;
	[Inject] IItemDataCollection _itemDataCollection;
	[Inject] IProductionCellsGenerator _productionCellsGenerator;
	[Inject] IResourcesStorageViewPanel[] _resourcesStorageViewPanels;

	void Start()
	{
		CreateGameField();
		CreateFreeItemsField();
		CreateFreeItems();
		_productionSystem.Init(_gameData.GameField);
		InitUI();
		_inputService.Enable();
	}

	void CreateGameField()
	{
		var field = _fieldFactory.CreateField(_gameFieldParent, _startGridSize);
		_productionCellsGenerator.Generate(field);
		_gameData.GameField = field;
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

	void InitUI()
	{
		foreach (var panel in _resourcesStorageViewPanels)
			panel.Init();
	}
}