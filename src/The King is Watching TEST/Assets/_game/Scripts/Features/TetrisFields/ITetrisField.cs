using System.Collections.Generic;
using ProductionCells.StaticData;
using TetrisFields.Items;
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
		void CreateProductionCell(Vector2Int pos, IProductionCellData data);
	}
}