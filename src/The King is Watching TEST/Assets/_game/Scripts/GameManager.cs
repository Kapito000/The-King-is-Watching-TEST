using Extensions;
using Map;
using StaticData;
using UnityEngine;
using UnityEngine.Assertions;

public class GameManager : MonoBehaviour
{
	[SerializeField] Vector2Int _startGridSize = new(8, 8);
	[Space]
	[SerializeField] GameObject _cellPrefab;
	[SerializeField] Transform _fieldParent;
	[Space]
	[SerializeField] Item _itemPrefab;
	[SerializeField] GameObject _itemCell;
	[SerializeField] Transform _freeItemsParent;
	[SerializeField] ItemDataCollection _itemDataCollection;

	IGrid<IItem> _grid;

	void Awake()
	{
		Assert.IsNotNull(_itemCell);
		Assert.IsNotNull(_itemPrefab);
		Assert.IsNotNull(_cellPrefab);
		Assert.IsNotNull(_fieldParent);
		Assert.IsNotNull(_freeItemsParent);
		Assert.IsNotNull(_itemDataCollection);
	}

	void Start()
	{
		CreateField();
		CreateItems();
	}

	void CreateField()
	{
		_grid = new BaseGrid<IItem>(_startGridSize.x, _startGridSize.y);
		foreach (var gridPos in _grid)
		{
			var cell = Instantiate(_cellPrefab, gridPos.AsVector3(), Quaternion.identity, new InstantiateParameters()
			{
				parent = _fieldParent,
				worldSpace = false,
			});
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

			foreach (var cellData in item.Cells)
			{
				Instantiate(_itemCell, cellData.AsVector3(), Quaternion.identity, new InstantiateParameters()
				{
					parent = item.transform,
					worldSpace = false,
				});
			}
		}
	}
}