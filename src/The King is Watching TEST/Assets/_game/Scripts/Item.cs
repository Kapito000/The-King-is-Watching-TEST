using System.Collections.Generic;
using UnityEngine;

public sealed class Item : MonoBehaviour, IItem
{
	[SerializeField] Vector2Int[] _cells;

	[field: SerializeField] public ItemOrientation Orientation { get; set; }

	List<IItemCell> _itemCells = new();

	public Vector2Int[] Cells
	{
		get => _cells;
		set => _cells = value.Clone() as Vector2Int[];
	}

	public void AddItemCell(IItemCell itemCell)
	{
		_itemCells.Add(itemCell);
	}

	public void Capture()
	{
		_itemCells.ForEach(x => x.SetLayer(Constant.SortingLayers.CapturedItem));
	}

	public void Uncapture()
	{
		_itemCells.ForEach(x => x.SetLayer(Constant.SortingLayers.Item));
	}

	public void MoveTo(Vector2 pos)
	{
		transform.position = pos;
	}
}