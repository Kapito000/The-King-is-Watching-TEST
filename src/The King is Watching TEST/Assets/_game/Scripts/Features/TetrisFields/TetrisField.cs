using System.Collections.Generic;
using Extensions;
using Map;
using TetrisFields.Items;
using UnityEngine;

namespace TetrisFields
{
	public sealed class TetrisField : MonoBehaviour, ITetrisField
	{
		Grid<IItem> _itemsGrid;
		Grid<IFieldCell> _fieldCellGrid;

		List<IItem> _items = new();

		public IGrid<IItem> ItemsGrid => _itemsGrid;

		public void Init(Vector2Int size)
		{
			_itemsGrid = new Grid<IItem>(size.x, size.y);
			_fieldCellGrid = new Grid<IFieldCell>(size.x, size.y);
		}

		public bool CanPutItem(Vector2Int pos, IItem item)
		{
			var (x, y) = pos.Deconstruct();

			foreach (var itemCellPos in item.Cells)
			{
				var gridCell = pos + itemCellPos;

				if (_itemsGrid.HasCell(gridCell) == false)
					return false;

				if (_itemsGrid[x, y] == null)
					return false;
			}

			return true;
		}

		public void PutItem(IItem item, Vector2Int pos)
		{
			foreach (var itemCellPos in item.Cells)
			{
				var (x, y) = (pos + itemCellPos).Deconstruct();
				_itemsGrid[x, y] = item;
			}

			item.ReplaceTo(transform.position + pos.AsVector3());
			_items.Add(item);
		}

		public void ExtractItem(IItem item)
		{
			foreach (var v in _itemsGrid)
				_itemsGrid[v.x, v.y] = null;

			_items.Remove(item);
		}

		public void SetFieldCell(IFieldCell cell, Vector2Int pos)
		{
			_fieldCellGrid[pos.x, pos.y] = cell;
		}
	}
}