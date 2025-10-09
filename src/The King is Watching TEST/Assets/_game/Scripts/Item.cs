using System.Collections.Generic;
using Extensions;
using UnityEngine;

public sealed class Item : MonoBehaviour, IItem
{
	[SerializeField] Vector2Int[] _cells;

	List<ItemCell> _itemCells = new();

	public Vector2Int[] Cells
	{
		get => _cells;
		set => _cells = value.Clone() as Vector2Int[];
	}

	public void AddItemCell(ItemCell itemCell)
	{
		_itemCells.Add(itemCell);
	}

	public void Capture()
	{
		foreach (var x in _itemCells)
		{
			x.SetLayer(Constant.SortingLayers.CapturedItem);
			x.EnableCollider(false);
		}
	}

	public void Uncapture()
	{
		foreach (var x in _itemCells)
		{
			x.SetLayer(Constant.SortingLayers.Item);
		}
	}

	public void PutToField()
	{
		foreach (var x in _itemCells)
		{
			x.SetLayer(Constant.SortingLayers.Item);
			x.EnableCollider(false);
		}
	}

	public void MoveTo(Vector2 pos)
	{
		transform.position = pos;
	}

	public void Rotate()
	{
		for (var i = 0; i < _cells.Length; i++)
		{
			_cells[i] = _cells[i].Rotate90();
			_itemCells[i].transform.localPosition = _cells[i].AsVector3();
		}
	}
}