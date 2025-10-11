using System;
using System.Collections.Generic;
using Extensions;
using Map;
using ProductionCells;
using ProductionCells.StaticData;
using TetrisFields.Items;
using UnityEngine;

namespace TetrisFields
{
	public sealed class TetrisField : MonoBehaviour, ITetrisField
	{
		Grid<IItem> _itemsGrid;
		Grid<FieldCell> _fieldCellsGrid;
		Grid<IProductionCell> _productionCellsGrid;

		List<IItem> _items = new();

		public IGrid<IItem> ItemsGrid => _itemsGrid;
		public Vector2Int Size => _itemsGrid.Size;

		public void CreateProductionCell(Vector2Int pos, IProductionCellData data)
		{
			var fieldCell = _fieldCellsGrid[pos.x, pos.y];
			if (fieldCell.TryGetComponent<ProductionCell>(out var productionCell))
			{
				productionCell.Init(data);
			}

			_productionCellsGrid[pos.x, pos.y] = productionCell;
		}

		public void Init(Vector2Int size)
		{
			_itemsGrid = new Grid<IItem>(size.x, size.y);
			_fieldCellsGrid = new Grid<FieldCell>(size.x, size.y);
			_productionCellsGrid = new Grid<IProductionCell>(size.x, size.y);
		}

		public bool CanPutItem(Vector2Int[] cells, Vector2Int pos)
		{
			foreach (var itemCellPos in cells)
			{
				var gridCell = pos + itemCellPos;

				if (_itemsGrid.HasCell(gridCell) == false)
					return false;

				if (_itemsGrid[gridCell.x, gridCell.y] != null)
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

		public void SetFieldCell(FieldCell cell, Vector2Int pos)
		{
			_fieldCellsGrid[pos.x, pos.y] = cell;
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

		public IEnumerable<IFieldCell> AllFields() =>
			_fieldCellsGrid;

		public IEnumerable<IProductionCell> AllProductionCells() =>
			_productionCellsGrid;

		public IEnumerable<IProductionCell> AllProductionCells(
			Func<IProductionCell, bool> where)
		{
			if (where == null)
			{
				Debug.LogError("The condition is null.");
				yield break;
			}

			foreach (var productionCell in AllProductionCells())
				if (where.Invoke(productionCell))
					yield return productionCell;
		}

		public IEnumerable<Vector2Int> AllProductionCellsCoordinates(
			Func<IProductionCell, bool> where)
		{
			if (where == null)
			{
				Debug.LogError("The condition is null.");
				yield break;
			}

			foreach (var pos in _productionCellsGrid.AllCoordinates())
			{
				if (where.Invoke(_productionCellsGrid[pos.x, pos.y]))
					yield return pos;
			}
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