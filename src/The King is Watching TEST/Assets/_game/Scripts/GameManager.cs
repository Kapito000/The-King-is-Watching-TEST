using Extensions;
using Map;
using StaticData;
using TetrisField;
using UnityEngine;
using UnityEngine.Assertions;
using Zenject;

public class GameManager : MonoBehaviour, IInitializable
{
	[SerializeField] Vector2Int _startGridSize = new(8, 8);
	[Space]
	[SerializeField] FieldCell _fieldCellPrefab;
	[SerializeField] Transform _fieldParent;
	[Space]
	[SerializeField] Item _itemPrefab;
	[SerializeField] ItemCell _itemCell;
	[SerializeField] Transform _freeItemsParent;
	[SerializeField] ItemDataCollection _itemDataCollection;

	FieldGrid _grid;

	public void Initialize()
	{
		
	}
	
	void Awake()
	{
		Assert.IsNotNull(_itemCell);
		Assert.IsNotNull(_itemPrefab);
		Assert.IsNotNull(_fieldCellPrefab);
		Assert.IsNotNull(_fieldParent);
		Assert.IsNotNull(_freeItemsParent);
		Assert.IsNotNull(_itemDataCollection);
		
		
	}

	void Start()
	{
		CreateField();
		CreateItems();
	}

	public bool TryPlace(IFieldCell fieldCell, IItem item)
	{
		foreach (var itemCellPos in item.Cells)
		{
			var gridCell = fieldCell.FieldPos + itemCellPos;

			if (_grid.HasCell(gridCell) == false)
				return false;

			if (_grid.ContainsItem(gridCell))
				return false;
		}

		item.MoveTo(fieldCell.Pos);
		item.PutToField();

		foreach (var itemCellPos in item.Cells)
		{
			var gridCell = fieldCell.FieldPos + itemCellPos;
			var x = gridCell.x;
			var y = gridCell.y;
			_grid[x, y].Place(item);
		}

		return true;
	}

	void CreateField()
	{
		_grid = new FieldGrid(_startGridSize.x, _startGridSize.y);
		
		foreach (var gridPos in _grid)
		{
			var fieldCell = Instantiate(_fieldCellPrefab, gridPos.AsVector3(), Quaternion.identity,
				new InstantiateParameters()
				{
					parent = _fieldParent,
					worldSpace = false,
				});
			fieldCell.FieldPos = gridPos;

			if (_grid.TrySet(fieldCell, gridPos.x, gridPos.y) == false)
			{
				Debug.LogError($"Can't set field cell: {gridPos}");
			}
		}
	}

	void CreateItems()
	{
		var xOffset = Vector3.right * 2.5f;
		var yOffset = Vector3.down * 3;

		var x = 0;
		var y = -1;
		const int row = 3;
		var startPos = _freeItemsParent.position;

		foreach (var itemData in _itemDataCollection)
		{
			if (x >= row)
				x = 0;

			if (x == 0)
				y++;

			var newPos = startPos + xOffset * x + yOffset * y;

			var item = Instantiate(_itemPrefab, newPos, Quaternion.identity, new InstantiateParameters()
			{
				parent = _freeItemsParent,
				worldSpace = true,
			});

			x++;

			item.Cells = itemData.Cells;

			foreach (var cellPos in item.Cells)
			{
				var itemCell = Instantiate(_itemCell, cellPos.AsVector3(), Quaternion.identity, new InstantiateParameters()
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