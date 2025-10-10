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

		public bool CanPutItem(IItem item, Vector2Int pos)
		{
			var (x, y) = pos.Deconstruct();

			foreach (var itemCellPos in item.Cells)
			{
				var gridCell = pos + itemCellPos;

				if (_itemsGrid.HasCell(gridCell) == false)
					return false;

				if (_itemsGrid[x, y] != null)
					return false;
			}

			return true;
		}

		public void PutItem(IItem item, Vector2Int pos)
		{
			foreach (var itemCellPos in item.Cells)
			{
				var (x, y) = (pos + itemCellPos).Deconstruct();
				PlaceItem(x, y, item);
			}

			item.ReplaceTo(transform.position + pos.AsVector3());
			_items.Add(item);
		}

		public void ExtractItem(IItem item)
		{
			foreach (var (pos, gridItem) in _itemsGrid.WithValues())
			{
				if (gridItem == item)
					ExtractItem(pos.x, pos.y);
			}

			_items.Remove(item);
		}

		public void SetFieldCell(IFieldCell cell, Vector2Int pos)
		{
			_fieldCellGrid[pos.x, pos.y] = cell;
		}

		public bool HasItemAt(Vector2Int pos)
		{
			if (_itemsGrid.HasCell(pos) == false)
				return false;

			if (_itemsGrid[pos.x, pos.y] == null)
				return false;

			return true;
		}

		public IItem GetItemAt(Vector2Int pos)
		{
			return _itemsGrid[pos.x, pos.y];
		}

		void PlaceItem(int x, int y, IItem item)
		{
			_itemsGrid[x, y] = item;
		}

		void ExtractItem(int x, int y)
		{
			_itemsGrid[x, y] = null;
		}
	}
}