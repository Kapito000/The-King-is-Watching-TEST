using Extensions;
using Infrastructure;
using TetrisFields;
using TetrisFields.Items;
using TetrisFields.Items.StaticData;
using UnityEngine;
using UnityEngine.Assertions;
using Zenject;

public class GameBootstrapper : MonoBehaviour
{
	[SerializeField] Vector2Int _startGridSize = new(8, 8);
	[SerializeField] Vector2Int _freeItemsGridSize = new(6, 9);
	[Space]
	[SerializeField] FieldCell _fieldCellPrefab;
	[Space]
	[SerializeField] Item _itemPrefab;
	[SerializeField] ItemCell _itemCell;
	[SerializeField] ItemDataCollection _itemDataCollection;

	[Inject(Id = InjectId.GameFieldParent)]
	Transform _gameFieldParent;
	[Inject(Id = InjectId.FreeItemsFieldParent)]
	Transform _fireeItemsFieldParent;
	[Inject] ITetrisFieldFactory _fieldFactory;

	ITetrisField _gameField;
	ITetrisField _freeItemsField;

	void Awake()
	{
		Assert.IsNotNull(_itemCell);
		Assert.IsNotNull(_itemPrefab);
		Assert.IsNotNull(_fieldCellPrefab);
		Assert.IsNotNull(_itemDataCollection);
	}

	void Start()
	{
		CreateField();
		CreateFreeItemsField();
		CreateFreeItems();
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
		var xOffset = Vector3.right * 2.5f;
		var yOffset = Vector3.down * 3;

		var x = 0;
		var y = -1;
		const int row = 3;
		var startPos = _fireeItemsFieldParent.position;

		foreach (var itemData in _itemDataCollection)
		{
			if (x >= row)
				x = 0;

			if (x == 0)
				y++;

			var newPos = startPos + xOffset * x + yOffset * y;

			var item = Instantiate(_itemPrefab, newPos, Quaternion.identity,
				new InstantiateParameters()
				{
					parent = _fireeItemsFieldParent,
					worldSpace = true,
				});

			x++;

			item.Cells = itemData.Cells;

			foreach (var cellPos in item.Cells)
			{
				var itemCell = Instantiate(_itemCell, cellPos.AsVector3(),
						Quaternion.identity, new InstantiateParameters()
						{
							parent = item.transform,
							worldSpace = false,
						})
					.With(ic => ic.SetItem(item));

				item.AddItemCell(itemCell);
			}
		}
	}
}