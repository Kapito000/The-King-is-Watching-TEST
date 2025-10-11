using System.Collections.Generic;
using Extensions;
using GameResources.StaticData;
using UnityEngine;

namespace TetrisFields.Items
{
	public sealed class Item : MonoBehaviour, IItem
	{
		[SerializeField] Vector2Int[] _cells;
		[SerializeField] ResourceCellInfo _resourceCell;

		List<ItemCell> _itemCells = new();

		public Vector2Int[] Cells
		{
			get => _cells;
			set => _cells = value.Clone() as Vector2Int[];
		}

		public void ReplaceTo(Vector2 pos)
		{
			transform.position = pos;
		}

		public void AddItemCell(ItemCell itemCell)
		{
			_itemCells.Add(itemCell);
		}

		public void Capture()
		{
			foreach (var cell in _itemCells)
			{
				cell.SetRenderLayer(Constant.SortingLayers.CapturedItem);
			}
		}

		public void Uncapture()
		{
			foreach (var cell in _itemCells)
			{
				cell.SetRenderLayer(Constant.SortingLayers.Item);
			}
		}

		public void Rotate()
		{
			for (var i = 0; i < _cells.Length; i++)
			{
				_cells[i] = _cells[i].Rotate90();
				_itemCells[i].transform.localPosition = _cells[i].AsVector3();
			}
		}

		public void SetResourceCell(ResourceCellInfo info)
		{
			_resourceCell = info;
		}

		public void Destroy()
		{
			Destroy(gameObject);
		}
	}
}