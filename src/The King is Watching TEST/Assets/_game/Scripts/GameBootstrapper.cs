using Extensions;
using Infrastructure;
using StaticData;
using TetrisField;
using UnityEngine;
using UnityEngine.Assertions;
using Zenject;

public class GameBootstrapper : MonoBehaviour
{
	[SerializeField] Vector2Int _startGridSize = new(8, 8);
	[Space]
	[SerializeField] FieldCell _fieldCellPrefab;
	[Space]
	[SerializeField] Item _itemPrefab;
	[SerializeField] ItemCell _itemCell;
	[SerializeField] Transform _freeItemsParent;
	[SerializeField] ItemDataCollection _itemDataCollection;

	[Inject(Id = InjectId.GameFieldParent)]
	Transform _fieldParent;
	[Inject] ITetrisFieldFactory _fieldFactory;

	FieldGrid _grid;

	void Awake()
	{
		Assert.IsNotNull(_itemCell);
		Assert.IsNotNull(_itemPrefab);
		Assert.IsNotNull(_fieldCellPrefab);
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
		var gameField = _fieldFactory.CreateField(_fieldParent, _startGridSize);
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