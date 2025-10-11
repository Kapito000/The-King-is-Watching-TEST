using System;
using System.Collections.Generic;
using Map;
using ProductionCells;
using ProductionCells.StaticData;
using TetrisFields.Items;
using UniRx;
using UnityEngine;

namespace TetrisFields
{
	public interface ITetrisField
	{
		bool CanPutItem(Vector2Int[] cells, Vector2Int pos);
		void PutItem(IItem item, Vector2Int pos);
		void ExtractItem(IItem item);
		bool HasItemAt(Vector2Int pos);
		IItem GetItemAt(Vector2Int pos);
		IEnumerable<IFieldCell> AllFields();
		Vector2Int Size { get; }
		IObservable<Unit> FieldChanged { get; }
		IGrid<IProductionCell> ProductionCellsGrid { get; }
		IGrid<IItem> ItemsGrid { get; }
		IReadOnlyList<IItem> Items { get; }
		Grid<ResourceCellInfo> ProductionItemCellGrid { get; }

		void CreateProductionCell(Vector2Int pos, IProductionCellData data,
			int productionDataId);

		IEnumerable<Vector2Int> AllProductionCellsCoordinates(
			Func<IProductionCell, bool> where);

		IProductionCell ProductionCells(Vector2Int pos);
	}
}